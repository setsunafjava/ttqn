using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing.Fields;
using Microsoft.SharePoint.Publishing.WebControls;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;


namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// HotNewsContentUS
    /// </summary>
    public partial class HotNewsContentUS : UserControl
    {
        public HotNewsContent WebPartParent;
        public string ItemUrl = string.Empty;
        public string NewsUrl = string.Empty;
        public string RptImagesUrl = string.Empty;
        public string RptThreeItemUrl = string.Empty;
        public string RptLatestNewsUrl = string.Empty;
        public string RptTopViewsUrl = string.Empty;

        public string Linktoitem = string.Empty;
        public string listName = ListsName.English.NewsRecord;
        public string listCategoryName = ListsName.English.NewsCategory;
        public string QueryAllItemsSortById = string.Format(@"<Where>
                                                                  <And>
                                                                     <Neq>
                                                                        <FieldRef Name='Status' />
                                                                        <Value Type='Boolean'>1</Value>
                                                                     </Neq>
                                                                     <And>
                                                                        <Lt>
                                                                           <FieldRef Name='{0}' />
                                                                           <Value IncludeTimeValue='TRUE' Type='DateTime'>{1}</Value>
                                                                        </Lt>
                                                                        <Eq>
                                                                           <FieldRef Name='{2}' />
                                                                           <Value Type='ModStat'>{3}</Value>
                                                                        </Eq>
                                                                     </And>
                                                                  </And>
                                                               </Where>
                                                               <OrderBy>
                                                                  <FieldRef Name='{4}' Ascending='False' />
                                                               </OrderBy>",
                                FieldsName.ArticleStartDates,
                                SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                FieldsName.ModerationStatus,
                                Utilities.GetModerationStatus(402), FieldsName.ArticleStartDates);

        public string QueryAllItemsSortByIdOnCategory = string.Format(@"<Where>
                                                                  <And>
                                                                     <Neq>
                                                                        <FieldRef Name='{5}' />
                                                                        <Value Type='Boolean'>1</Value>
                                                                     </Neq>
                                                                     <And>
                                                                        <Lt>
                                                                           <FieldRef Name='{0}' />
                                                                           <Value IncludeTimeValue='TRUE' Type='DateTime'>{1}</Value>
                                                                        </Lt>
                                                                        <Eq>
                                                                           <FieldRef Name='{2}' />
                                                                           <Value Type='ModStat'>{3}</Value>
                                                                        </Eq>
                                                                     </And>
                                                                  </And>
                                                               </Where>
                                                               <OrderBy>
                                                                  <FieldRef Name='{4}' Ascending='False' />
                                                               </OrderBy>",
                                FieldsName.ArticleStartDates,
                                SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                FieldsName.ModerationStatus,
                                Utilities.GetModerationStatus(402), FieldsName.ArticleStartDates,
                                FieldsName.NewsRecord.English.ShowOnCategory);

        public string QueryAllItemsSortByViewCount = string.Format(@"<Where>
                                                                  <And>
                                                                     <Neq>
                                                                        <FieldRef Name='Status' />
                                                                        <Value Type='Boolean'>1</Value>
                                                                     </Neq>
                                                                     <And>
                                                                        <Lt>
                                                                           <FieldRef Name='{0}' />
                                                                           <Value IncludeTimeValue='TRUE' Type='DateTime'>{1}</Value>
                                                                        </Lt>
                                                                        <Eq>
                                                                           <FieldRef Name='{2}' />
                                                                           <Value Type='ModStat'>{3}</Value>
                                                                        </Eq>
                                                                     </And>
                                                                  </And>
                                                               </Where>
                                                               <OrderBy>
                                                                  <FieldRef Name='{4}' Ascending='False' />
                                                               </OrderBy>", FieldsName.ArticleStartDates,
                                SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                FieldsName.ModerationStatus,
                                Utilities.GetModerationStatus(402), FieldsName.NewsRecord.English.ViewsCount);

        public string QueryAllItemsByShowInHomePage = string.Format(@"<Where>
                                                                          <And>
                                                                             <Eq>
                                                                                <FieldRef Name='{0}' />
                                                                                <Value Type='Boolean'>1</Value>
                                                                             </Eq>
                                                                             <And>
                                                                                <Lt>
                                                                                   <FieldRef Name='{1}' />
                                                                                   <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                                </Lt>
                                                                                <And>
                                                                                   <Neq>
                                                                                      <FieldRef Name='Status' />
                                                                                      <Value Type='Boolean'>1</Value>
                                                                                   </Neq>
                                                                                   <Eq>
                                                                                      <FieldRef Name='{3}' />
                                                                                      <Value Type='ModStat'>{4}</Value>
                                                                                   </Eq>
                                                                                </Eq>
                                                                             </And>
                                                                          </And>
                                                                       </Where>
                                                                       <OrderBy>
                                                                          <FieldRef Name='{5}' Ascending='False' />
                                                                       </OrderBy>",
                                                                    FieldsName.NewsRecord.English.ShowInHomePage, FieldsName.ArticleStartDates,
                                                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                    FieldsName.ModerationStatus, Utilities.GetModerationStatus(402),
                                                                    FieldsName.ArticleStartDates);
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
                    string latestNewsQuery = string.Empty;

                    var listNameTemp = Request.QueryString[Constants.ListName];
                    var listCategoryNameTemp = Request.QueryString[Constants.ListCategoryName];
                    var focusNews = Request.QueryString["FocusNews"];
                    if (!string.IsNullOrEmpty(listNameTemp)) listName = listNameTemp;
                    if (!string.IsNullOrEmpty(listCategoryNameTemp)) listCategoryName = listCategoryNameTemp;

                    ItemUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                                            SPContext.Current.Web.Url,
                                            Constants.PageInWeb.DetailNews,
                                            listCategoryName,
                                            listName,
                                            Constants.NewsId);
                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                                            SPContext.Current.Web.Url,
                                            Constants.PageInWeb.DetailNews,
                                            ListsName.English.NewsCategory,
                                            ListsName.English.NewsRecord,
                                            Constants.NewsId);

                    #region If webpart properties != 0
                    if (!"0".Equals(WebPartParent.WebpartName)) //Đây là trường hợp load ra đầu tiên
                    {
                        #region Latest News
                        if (!string.IsNullOrEmpty(categoryId) && !"-1".Equals(categoryId))
                        {
                            latestNewsQuery = QueryAllItemsSortByIdOnCategory;
                        }
                        else
                        {
                            //CAML query will get all item 
                            latestNewsQuery = QueryAllItemsSortById;
                        }

                        var latestNewsTable = Utilities.GetNewsRecordItems(latestNewsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), listName);
                        if (latestNewsTable != null && latestNewsTable.Count > 0)
                        {
                            var latestNewsTableTemp = Utilities.GetTableWithCorrectUrl(listCategoryName, latestNewsTable, true);
                            RptLatestNewsUrl = ItemUrl;
                            rptLatestNews.DataSource = latestNewsTableTemp;
                            rptLatestNews.DataBind();
                        }


                        #endregion

                        #region Top News
                        string topNewsQuery = string.Empty;
                        topNewsQuery = QueryAllItemsSortByViewCount;

                        var topViewsTable = Utilities.GetNewsRecordItems(topNewsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), listName);
                        if (topViewsTable != null && topViewsTable.Count > 0)
                        {
                            RptTopViewsUrl = ItemUrl;
                            rptTopViews.DataSource = Utilities.GetTableWithCorrectUrl(listCategoryName, topViewsTable, true);
                            rptTopViews.DataBind();
                        }

                        #endregion
                        LoadDataToHotNews(categoryId, listName, listCategoryName);
                    }
                    #endregion

                    #region Neu webpart duoc set propertie = 0
                    //Neu khi setup webpart la 0
                    else if ("0".Equals(WebPartParent.WebpartName))
                    {
                        rptTopViews.Visible = false;
                        pnlIndex.Visible = false;
                        pnlSubPage.Visible = true;

                        LoadDataToHotNews(categoryId, listName, listCategoryName);
                        if ("1".Equals(focusNews))
                        {
                            string newsQuery = string.Format(@"<Where>
                                                                  <And>
                                                                     <Eq>
                                                                        <FieldRef Name='{0}' />
                                                                        <Value Type='Boolean'>1</Value>
                                                                     </Eq>
                                                                     <And>
                                                                        <Neq>
                                                                           <FieldRef Name='Status' />
                                                                           <Value Type='Boolean'>1</Value>
                                                                        </Neq>
                                                                        <And>
                                                                           <Lt>
                                                                              <FieldRef Name='ArticleStartDates' />
                                                                              <Value IncludeTimeValue='TRUE' Type='DateTime'>{1}</Value>
                                                                           </Lt>
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
                                                                          FieldsName.NewsRecord.English.FocusNews,
                                                                          SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                          FieldsName.ModerationStatus, Utilities.GetModerationStatus(402), FieldsName.ArticleStartDates);

                            var companyList = Utilities.GetNewsRecords(newsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), listName);
                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                RptLatestNewsUrl = ItemUrl;
                                rptLatestNews.DataSource = companyList;
                                rptLatestNews.DataBind();
                            }
                        }
                        else //Khong phai la tin tieu bieu
                        {
                            var companyList = Utilities.GetNewsByCatID(listName, categoryId, GetNewsNumber(WebPartParent.LatestNewsNumber));
                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                RptLatestNewsUrl = ItemUrl;
                                rptLatestNews.DataSource = companyList;
                                rptLatestNews.DataBind();
                            }
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Utilities.LogToUls(ex);
                }
            }
        }

        /// <summary>
        /// This method will load image news on HotNewsContent
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="_listName"></param>
        /// <param name="_listCategoryName"></param>
        public void LoadDataToHotNews(string categoryId, string _listName, string _listCategoryName)
        {
            #region Tin tuc o phia tren voi cac images
            string mainItemQuery = string.Empty;
            if (!string.IsNullOrEmpty(categoryId) && !"-1".Equals(categoryId)) //if categoryId !=null
            {
                mainItemQuery = string.Format(@"<Where>
                                      <And>
                                         <Eq>
                                            <FieldRef Name='{0}' />
                                            <Value Type='Boolean'>1</Value>
                                         </Eq>
                                         <And>
                                            <Eq>
                                               <FieldRef Name='CategoryName' LookupId='TRUE'/>
                                               <Value Type='LookupMulti'>{1}</Value>
                                            </Eq>
                                            <And>
                                               <Lt>
                                                  <FieldRef Name='{2}' />
                                                  <Value IncludeTimeValue='TRUE' Type='DateTime'>{3}</Value>
                                               </Lt>
                                               <And>
                                                  <Neq>
                                                     <FieldRef Name='Status' />
                                                     <Value Type='Boolean'>1</Value>
                                                  </Neq>
                                                  <Eq>
                                                     <FieldRef Name='{4}' />
                                                     <Value Type='ModStat'>{5}</Value>
                                                  </Eq>
                                               </And>
                                            </And>
                                         </And>
                                      </And>
                                   </Where>
                                   <OrderBy>
                                      <FieldRef Name='{6}' Ascending='False' />
                                   </OrderBy>",
                    //FieldsName.NewsRecord.English.ShowInHomePage,
                        FieldsName.NewsRecord.English.ShowOnCategory,
                    //FieldsName.NewsRecord.English.CategoryName,
                        categoryId,
                        FieldsName.ArticleStartDates,
                        SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                        FieldsName.ModerationStatus,
                        Utilities.GetModerationStatus(402),
                        FieldsName.ArticleStartDates);

            }
            else
            {
                mainItemQuery = QueryAllItemsByShowInHomePage;
            }

            var mainItem = Utilities.GetNewsRecordItems(mainItemQuery, 3, _listName);

            if (mainItem != null && mainItem.Count > 0)
            {//se phai dung thuat toan get thumnail cua image
                var tempTable = Utilities.GetTableWithCorrectUrlHotNews(_listCategoryName, mainItem);
                Utilities.SetSapoTextLength(ref tempTable);
                RptImagesUrl = ItemUrl;
                rptImages.DataSource = tempTable;
                rptImages.DataBind();
            }
            #endregion
            #region  tin tuc phia duoi

            string threeeItemsBellow = string.Empty;
            if (!string.IsNullOrEmpty(categoryId) && !"-1".Equals(categoryId)) //if categoryId !=null
            {
                threeeItemsBellow =
                    string.Format(@"<Where>
                                      <And>
                                         <Eq>
                                            <FieldRef Name='{0}' />
                                            <Value Type='Boolean'>1</Value>
                                         </Eq>
                                         <And>
                                            <Eq>
                                               <FieldRef Name='CategoryName' LookupId='TRUE'/>
                                               <Value Type='LookupMulti'>{1}</Value>
                                            </Eq>
                                            <And>
                                               <Lt>
                                                  <FieldRef Name='{2}' />
                                                  <Value IncludeTimeValue='TRUE' Type='DateTime'>{3}</Value>
                                               </Lt>
                                               <And>
                                                  <Neq>
                                                     <FieldRef Name='Status' />
                                                     <Value Type='Boolean'>1</Value>
                                                  </Neq>
                                                  <Eq>
                                                     <FieldRef Name='{4}' />
                                                     <Value Type='Lookup'>{5}</Value>
                                                  </Eq>
                                               </And>
                                            </And>
                                         </And>
                                      </And>
                                   </Where>
                                   <OrderBy>
                                      <FieldRef Name='{6}' Ascending='False' />
                                   </OrderBy>",
                    //FieldsName.NewsRecord.English.ShowInHomePage,
                        FieldsName.NewsRecord.English.ShowOnCategory,
                    //FieldsName.NewsRecord.English.CategoryName,
                        categoryId,
                        FieldsName.ArticleStartDates,
                        SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                        FieldsName.ModerationStatus,
                        Utilities.GetModerationStatus(402),
                        FieldsName.ArticleStartDates);
            }
            else
            {
                threeeItemsBellow = QueryAllItemsByShowInHomePage;
            }

            var threeItemsTable = Utilities.GetNewsRecords(threeeItemsBellow, 6, _listName);
            if (threeItemsTable != null && threeItemsTable.Rows.Count > 0)
            {
                RptThreeItemUrl = ItemUrl;
                rptThreeItem.DataSource = GetItemFromThreePosition(threeItemsTable, _listCategoryName);
                rptThreeItem.DataBind();
            }
            #endregion
        }

        /// <summary>
        /// Ham nay se get tiep ra 3 item de bind vao phia duoi tin chinh
        /// </summary>
        /// <returns></returns>
        public DataTable GetItemFromThreePosition(DataTable dataTable, string _listCategoryName)
        {
            DataTable dataTableTemp = new DataTable("TableTemp");
            dataTableTemp.Columns.Add(FieldsName.Title);
            dataTableTemp.Columns.Add(FieldsName.Id);
            dataTableTemp.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
            dataTableTemp.Columns.Add(FieldsName.NewsRecord.English.ShortContent, Type.GetType("System.String"));


            if (dataTable != null && dataTable.Rows.Count > 3)
            {
                if (dataTable.Rows.Count >= 6)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        DataRow newRow = dataTableTemp.NewRow();
                        newRow[FieldsName.Title] = Convert.ToString(dataTable.Rows[i][FieldsName.Title]);
                        newRow[FieldsName.Id] = dataTable.Rows[i][FieldsName.Id];
                        newRow[FieldsName.CategoryId] = dataTable.Rows[i][FieldsName.CategoryId];
                        newRow[FieldsName.NewsRecord.English.ShortContent] = Utilities.StripHtml(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.ShortContent]));
                        dataTableTemp.Rows.Add(newRow);
                    }
                }
                else
                {
                    for (int i = 3; i < dataTable.Rows.Count; i++)
                    {
                        DataRow newRow = dataTableTemp.NewRow();
                        newRow[FieldsName.Title] = Convert.ToString(dataTable.Rows[i][FieldsName.Title]);
                        newRow[FieldsName.Id] = dataTable.Rows[i][FieldsName.Id];
                        newRow[FieldsName.CategoryId] = dataTable.Rows[i][FieldsName.CategoryId];
                        newRow[FieldsName.NewsRecord.English.ShortContent] = Utilities.StripHtml(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.ShortContent]));
                        dataTableTemp.Rows.Add(newRow);
                    }
                }
            }
            return dataTableTemp;
        }

        /// <summary>
        /// Get news number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static uint GetNewsNumber(string value)
        {
            uint newsNumber = 5;
            try
            {
                newsNumber = Convert.ToUInt16(value);
            }
            catch (Exception)
            {
                return 5;
            }
            return newsNumber;
        }
    }
}
