using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class NewsListUS : UserControl
    {
        public NewsList ParentWP;
        public string NewsUrl = string.Empty;
        public string CatID = string.Empty;
        public string NewsUrl1 = string.Empty;
        protected string BuildUrl(string pageorder)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var allkeys = Request.QueryString.AllKeys;
            for (int i = 0; i < allkeys.Length; i++)
            {
                if ("Page".Equals(allkeys[i]))
                {
                    stringBuilder.Append(string.Format("{0}={1}&", "Page", pageorder));
                }
                else
                {
                    stringBuilder.Append(string.Format("{0}={1}&", allkeys[i], Request.QueryString[allkeys[i]]));
                }
            }

            return stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1);
        }

        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            CatID = Request.QueryString["CategoryId"];
            if (!IsPostBack)
            {
                try
                {
                    var categoryId = Request.QueryString["CategoryId"];
                    var focusNews = Request.QueryString["FocusNews"];
                    var listName = Request.QueryString[Constants.ListName];
                    var listCategoryName = Request.QueryString[Constants.ListCategoryName];
                    lnkPrev.Text = ParentWP.Prev;
                    lnkNext.Text = ParentWP.Next;
                    var isChuyenDe = Request.QueryString["IsChuyenDe"];

                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                       SPContext.Current.Web.Url,
                       Constants.PageInWeb.DetailNews,
                       listCategoryName,
                       listName,
                       Constants.NewsId);

                    if ("1".Equals(focusNews))
                    {
                        string newsQuery = string.Format(@"<Where>
                                                                  <And>
                                                                     <Eq>
                                                                        <FieldRef Name='FocusNews' />
                                                                        <Value Type='Boolean'>1</Value>
                                                                     </Eq>
                                                                     <And>
                                                                        <Neq>
                                                                           <FieldRef Name='Status' />
                                                                           <Value Type='Boolean'>1</Value>
                                                                        </Neq>
                                                                        <And>
                                                                           <Leq>
                                                                              <FieldRef Name='{0}' />
                                                                              <Value IncludeTimeValue='TRUE' Type='DateTime'>{1}</Value>
                                                                           </Leq>
                                                                           <Eq>
                                                                              <FieldRef Name='{2}' />
                                                                              <Value Type='ModStat'>{3}</Value>
                                                                           </Eq>
                                                                        </And>
                                                                     </And>
                                                                  </And>
                                                               </Where>
                                                               <OrderBy>
                                                                  <FieldRef Name='{4}' Ascending='False' />
                                                               </OrderBy>",
                                                                      FieldsName.ArticleStartDates,
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                      FieldsName.ModerationStatus,
                                                                      Utilities.GetModerationStatus(402),
                                                                      FieldsName.ArticleStartDates);

                        var companyList = Utilities.GetNewsRecords(newsQuery, listName);

                        if (companyList != null && companyList.Rows.Count > 0)
                        {
                            var companyListTemp = Utilities.GetTableWithCorrectUrl(companyList);
                            //Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref companyListTemp);

                            var temp = companyListTemp.DefaultView;
                            temp.Sort = "ArticleStartDates desc";
                            companyListTemp = temp.ToTable();

                            //paging
                            PagedDataSource pageds = new PagedDataSource
                            {
                                DataSource = companyListTemp.DefaultView,
                                AllowPaging = true,
                                PageSize = 10
                            };
                            int curpage = 0;
                            var pageNum = Request.QueryString["Page"];
                            if (!string.IsNullOrEmpty(pageNum))
                            {
                                curpage = Convert.ToInt32(pageNum);
                            }
                            else
                            {
                                curpage = 1;
                            }
                            pageds.CurrentPageIndex = curpage - 1;
                            lblCurrpage.Text = string.Format("{0}: {1}", ParentWP.PageNumber, curpage);

                            if (!pageds.IsFirstPage)
                            {
                                lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage - 1));
                            }

                            if (!pageds.IsLastPage)
                            {
                                lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage + 1));
                            }
                            //end paging

                            rptListCategory.DataSource = pageds;
                            rptListCategory.DataBind();
                        }
                        else
                        {
                            lblItemNotExist.Text = Constants.ErrorMessage.Msg1;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(categoryId))
                        {
                            if (!"-1".Equals(categoryId))
                            {
                                if (!string.IsNullOrEmpty(isChuyenDe)) //
                                {
                                    pnlCategory.Visible = true;
                                    string newsQuery = string.Format(@"<Where>
                                                              <Eq>
                                                                 <FieldRef Name='ParentName' LookupId='TRUE'/>
                                                                 <Value Type='Lookup'>{0}</Value>
                                                              </Eq>
                                                           </Where>", categoryId);
                                    NewsUrl1 = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&CategoryId=",
                                                           SPContext.Current.Web.Url,
                                                           Constants.PageInWeb.SubPage,
                                                           ListsName.English.NewsCategory,
                                                           ListsName.English.NewsRecord);
                                    SPList list = Utilities.GetListFromUrl(SPContext.Current.Web, listCategoryName);
                                    SPListItem item = list.GetItemById(Convert.ToInt32(categoryId));
                                    lblCategoryTitle.Text = Convert.ToString(item[FieldsName.Title]);

                                    //var newsItem = Utilities.GetNewsRecords(newsQuery, ListsName.English.NewsCategory);
                                    SPQuery query = new SPQuery() { Query = newsQuery };
                                    var newsItem = list.GetItems(query);
                                    if (newsItem != null && newsItem.Count > 0)
                                    {
                                        rptCaregory.Visible = true;
                                        rptCaregory.DataSource = newsItem.GetDataTable();
                                        rptCaregory.DataBind();
                                    }
                                }


                                DataTable companyList = null;
                                Utilities.GetNewsByCatID(listName, listCategoryName, Convert.ToString(categoryId), ref companyList);
                                //Paging data
                                if (companyList != null && companyList.Rows.Count > 0)
                                {
                                    var companyListTemp = Utilities.GetTableWithCorrectUrl(companyList);
                                    //Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref companyListTemp);


                                    var temp = companyListTemp.DefaultView;
                                    temp.Sort = "ArticleStartDates desc";
                                    companyListTemp = temp.ToTable();

                                    PagedDataSource pageds = new PagedDataSource
                                    {
                                        DataSource = companyListTemp.DefaultView,
                                        AllowPaging = true,
                                        PageSize = 10
                                    };
                                    int curpage = 0;
                                    var pageNum = Request.QueryString["Page"];
                                    if (!string.IsNullOrEmpty(pageNum))
                                    {
                                        curpage = Convert.ToInt32(pageNum);
                                    }
                                    else
                                    {
                                        curpage = 1;
                                    }
                                    pageds.CurrentPageIndex = curpage - 1;
                                    lblCurrpage.Text = string.Format("{0}: {1}", ParentWP.PageNumber, curpage);

                                    if (!pageds.IsFirstPage)
                                    {
                                        lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage - 1));
                                    }

                                    if (!pageds.IsLastPage)
                                    {
                                        lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage + 1));
                                    }

                                    rptListCategory.DataSource = pageds;
                                    rptListCategory.DataBind();
                                }
                                else
                                {
                                    lblItemNotExist.Text = Constants.ErrorMessage.Msg1;
                                }
                            }
                            else //Search by Date time
                            {
                                var day = Convert.ToInt32(Request.QueryString["Day"]);
                                var month = Convert.ToInt32(Request.QueryString["Month"]);
                                var year = Convert.ToInt32(Request.QueryString["Year"]);
                                DateTime dt = new DateTime(year, month, day);

                                string categoryQuery = string.Format(@"<Where>
                                                                          <And>
                                                                             <Neq>
                                                                                <FieldRef Name='Status' />
                                                                                <Value Type='Boolean'>1</Value>
                                                                             </Neq>
                                                                             <And>
                                                                                <Eq>
                                                                                   <FieldRef Name='ArticleStartDates' />
                                                                                   <Value Type='DateTime'>{0}</Value>
                                                                                </Eq>
                                                                                <Eq>
                                                                                   <FieldRef Name='{1}' />
                                                                                   <Value Type='ModStat'>{2}</Value>
                                                                                </Eq>
                                                                             </And>
                                                                          </And>
                                                                       </Where>
                                                                       <OrderBy>
                                                                          <FieldRef Name='ArticleStartDates' Ascending='False' />
                                                                       </OrderBy>",
                                                                                  SPUtility.CreateISO8601DateTimeFromSystemDateTime(dt),
                                                                                  FieldsName.ModerationStatus,
                                                                                  Utilities.GetModerationStatus(402));
                                //var companyList = Utilities.GetNewsRecords(categoryQuery, listName);
                                var companyList = Utilities.GetNewsRecordItems(categoryQuery, 100, listName);

                                if (companyList != null && companyList.Count > 0)
                                {
                                    var companyListTemp = Utilities.GetTableWithCorrectUrl(listCategoryName, companyList);
                                    
                                    var temp = companyListTemp.DefaultView;
                                    temp.Sort = "ArticleStartDates desc";
                                    companyListTemp = temp.ToTable();


                                    PagedDataSource pageds = new PagedDataSource
                                    {
                                        DataSource = companyListTemp.DefaultView,
                                        AllowPaging = true,
                                        PageSize = 10
                                    };

                                    int curpage = 0;
                                    var pageNum = Request.QueryString["Page"];
                                    if (!string.IsNullOrEmpty(pageNum))
                                    {
                                        curpage = Convert.ToInt32(pageNum);
                                    }
                                    else
                                    {
                                        curpage = 1;
                                    }
                                    pageds.CurrentPageIndex = curpage - 1;
                                    lblCurrpage.Text = string.Format("{0}: {1}", ParentWP.PageNumber, curpage);

                                    if (!pageds.IsFirstPage)
                                    {
                                        lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage - 1));
                                    }

                                    if (!pageds.IsLastPage)
                                    {
                                        lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage + 1));
                                    }

                                    rptListCategory.DataSource = pageds;
                                    rptListCategory.DataBind();
                                }
                                else
                                {
                                    lblItemNotExist.Text = Constants.ErrorMessage.Msg1;
                                }
                            }
                        }
                        else //if categoryId == null
                        {
                            string allItemsQuery = string.Format(@"<Where>
                                                                      <And>
                                                                         <Neq>
                                                                            <FieldRef Name='Status' />
                                                                            <Value Type='Boolean'>1</Value>
                                                                         </Neq>
                                                                         <And>
                                                                                <Lt>
                                                                                   <FieldRef Name='ArticleStartDates' />
                                                                                   <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                                                </Lt>
                                                                                <Eq>
                                                                                   <FieldRef Name='{1}' />
                                                                                   <Value Type='ModStat'>{2}</Value>
                                                                                </Eq>
                                                                             </And>
                                                                      </And>
                                                                   </Where>
                                                                   <OrderBy>
                                                                      <FieldRef Name='ArticleStartDates' Ascending='False' />
                                                                   </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                              FieldsName.ModerationStatus,
                                                                              Utilities.GetModerationStatus(402));

                            var companyList = Utilities.GetNewsRecords(allItemsQuery, listCategoryName);

                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                var companyListTemp = Utilities.GetTableWithCorrectUrl(companyList);
                                //Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref companyListTemp);
                                var temp = companyListTemp.DefaultView;
                                temp.Sort = "ArticleStartDates desc";
                                companyListTemp = temp.ToTable();

                                PagedDataSource pageds = new PagedDataSource
                                {
                                    DataSource = companyListTemp.DefaultView,
                                    AllowPaging = true,
                                    PageSize = 10
                                };
                                int curpage = 0;
                                var pageNum = Request.QueryString["Page"];
                                if (!string.IsNullOrEmpty(pageNum))
                                {
                                    curpage = Convert.ToInt32(pageNum);
                                }
                                else
                                {
                                    curpage = 1;
                                }
                                pageds.CurrentPageIndex = curpage - 1;
                                lblCurrpage.Text = string.Format("{0}: {1}", ParentWP.PageNumber, curpage);

                                if (!pageds.IsFirstPage)
                                {
                                    lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage - 1));
                                }

                                if (!pageds.IsLastPage)
                                {
                                    lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage + 1));
                                }

                                rptListCategory.DataSource = pageds;
                                rptListCategory.DataBind();
                            }
                            else
                            {
                                lblItemNotExist.Text = Constants.ErrorMessage.Msg1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utilities.LogToUls(ex);
                }
            }
        }

        protected void OnItemDataBound_ListCategory(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                var imgPath = Convert.ToString(drv.Row["Thumbnail"]);
                var shortContent = Convert.ToString(drv.Row[FieldsName.NewsRecord.English.ShortContent]);

                if (!String.IsNullOrEmpty(imgPath))
                {
                    //((Literal)e.Item.FindControl("ltrImage")).Text = string.Format("<div class=\"img_thumb\"><img src=\"{0}\" /></div>", imgPath);
                    ((Literal)e.Item.FindControl("ltrImage")).Text = string.Format("<div class=\"interpre\"><div class=\"img_thumb\"><img src=\"{0}\" alt='' /></div><div class=\"short_content\"> {1}</div><div class=\"cleaner\"></div></div>", imgPath, shortContent);
                }
                else
                {
                    ((Literal)e.Item.FindControl("ltrImage")).Text = string.Format("<div class=\"interpre_Noimage\">{0}</div>", shortContent);
                }
            }
        }
    }
}