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
                                                                           <Eq>
                                                                              <FieldRef Name='ArticleStartDate' />
                                                                              <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                                           </Eq>
                                                                           <Contains>
                                                                              <FieldRef Name='Approve' />
                                                                              <Value Type='Lookup'>{1}</Value>
                                                                           </Contains>
                                                                        </And>
                                                                     </And>
                                                                  </And>
                                                               </Where>
                                                               <OrderBy>
                                                                  <FieldRef Name='ID' Ascending='False' />
                                                               </OrderBy>",
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                      Constants.Approved);

                        var companyList = Utilities.GetNewsRecords(newsQuery, listName);
                        
                        if (companyList != null && companyList.Rows.Count > 0)
                        {
                            var companyListTemp = Utilities.GetTableWithCorrectUrl(companyList);
                            Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref companyListTemp);
                            rptListCategory.DataSource = companyListTemp;
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
                                DataTable companyList = null;
                                Utilities.GetNewsByCatID(listName, listCategoryName, Convert.ToString(categoryId), ref companyList);
                                //Paging data
                                if (companyList != null && companyList.Rows.Count > 0)
                                {
                                    var companyListTemp = Utilities.GetTableWithCorrectUrl(companyList);
                                    Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref companyListTemp);
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
                            else
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
                                                                                <Lt>
                                                                                   <FieldRef Name='ArticleStartDate' />
                                                                                   <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                                                </Lt>
                                                                                <Contains>
                                                                                   <FieldRef Name='Approve' />
                                                                                   <Value Type='LookupMulti'>{1}</Value>
                                                                                </Contains>
                                                                             </And>
                                                                          </And>
                                                                       </Where>
                                                                       <OrderBy>
                                                                          <FieldRef Name='ArticleStartDate' Ascending='False' />
                                                                       </OrderBy>",
                                                                SPUtility.CreateISO8601DateTimeFromSystemDateTime(dt),
                                                                Constants.Approved);

                                var companyList = Utilities.GetNewsRecords(categoryQuery, listCategoryName);
                                if (companyList != null && companyList.Rows.Count > 0)
                                {
                                    var companyListTemp = Utilities.GetTableWithCorrectUrl(companyList);

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
                                                                                   <FieldRef Name='ArticleStartDate' />
                                                                                   <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                                                </Lt>
                                                                                <Contains>
                                                                                   <FieldRef Name='Approve' />
                                                                                   <Value Type='LookupMulti'>{1}</Value>
                                                                                </Contains>
                                                                             </And>
                                                                      </And>
                                                                   </Where>
                                                                   <OrderBy>
                                                                      <FieldRef Name='ID' Ascending='False' />
                                                                   </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                              Constants.Approved);

                            var companyList = Utilities.GetNewsRecords(allItemsQuery, listCategoryName);

                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                var companyListTemp = Utilities.GetTableWithCorrectUrl(companyList);
                                Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref companyListTemp);
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
    }
}