using System;
using System.Data;
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
                                             <Lt>
                                                <FieldRef Name='ArticleStartDate' />
                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                             </Lt>
                                          </And>
                                       </Where>
                                       <OrderBy>
                                          <FieldRef Name='ID' Ascending='False' />
                                       </OrderBy>",
                                SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
        public string QueryAllItemsSortByViewCount = string.Format(@"<Where>
                                                          <And>
                                                             <Neq>
                                                                <FieldRef Name='Status' />
                                                                <Value Type='Boolean'>1</Value>
                                                             </Neq>
                                                             <Lt>
                                                                <FieldRef Name='ArticleStartDate' />
                                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                             </Lt>
                                                          </And>
                                                       </Where>
                                                       <OrderBy>
                                                          <FieldRef Name='ViewsCount' Ascending='False' />
                                                       </OrderBy>",
                                                        SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));

        public string QueryAllItemsByShowInHomePage = string.Format(@"<Where>
                                                                          <And>
                                                                             <Eq>
                                                                                <FieldRef Name='{0}' />
                                                                                <Value Type='Boolean'>1</Value>
                                                                             </Eq>
                                                                             <And>
                                                                                <Lt>
                                                                                   <FieldRef Name='ArticleStartDate' />
                                                                                   <Value IncludeTimeValue='TRUE' Type='DateTime'>{1}</Value>
                                                                                </Lt>
                                                                                <Neq>
                                                                                   <FieldRef Name='Status' />
                                                                                   <Value Type='Boolean'>1</Value>
                                                                                </Neq>
                                                                             </And>
                                                                          </And>
                                                                       </Where>
                                                                       <OrderBy>
                                                                          <FieldRef Name='ID' Ascending='False' />
                                                                       </OrderBy>",
                                                                    FieldsName.NewsRecord.English.ShowInHomePage,
                                                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
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
                    if (!"0".Equals(WebPartParent.WebpartName))
                    {
                        #region Latest News

                        //CAML query will get all item 
                        latestNewsQuery = QueryAllItemsSortById;

                        var latestNewsTable = Utilities.GetNewsRecordItems(latestNewsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), listName);
                        if (latestNewsTable != null && latestNewsTable.Count > 0)
                        {
                            var latestNewsTableTemp = Utilities.GetTableWithCorrectUrl(listCategoryName, latestNewsTable);
                            Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref latestNewsTableTemp);
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
                            rptTopViews.DataSource = Utilities.GetTableWithCorrectUrl(listCategoryName, topViewsTable);
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
                                                                        <FieldRef Name='FocusNews' />
                                                                        <Value Type='Boolean'>1</Value>
                                                                     </Eq>
                                                                     <And>
                                                                        <Neq>
                                                                           <FieldRef Name='Status' />
                                                                           <Value Type='Boolean'>1</Value>
                                                                        </Neq>
                                                                        <Lt>
                                                                           <FieldRef Name='ArticleStartDate' />
                                                                           <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                                        </Lt>
                                                                     </And>
                                                                  </And>
                                                               </Where>
                                                               <OrderBy>
                                                                  <FieldRef Name='ID' Ascending='False' />
                                                               </OrderBy>",
                                                                          SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));

                            var companyList = Utilities.GetNewsRecords(newsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), listName);
                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref companyList);
                                RptLatestNewsUrl = ItemUrl;
                                rptLatestNews.DataSource = companyList;
                                rptLatestNews.DataBind();
                            }
                            else //If not newsfocus => get all
                            {
                                newsQuery = QueryAllItemsSortByViewCount;

                                companyList = Utilities.GetNewsRecords(newsQuery,GetNewsNumber(WebPartParent.LatestNewsNumber),  listName);
                                if (companyList != null && companyList.Rows.Count > 0)
                                {
                                    Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref companyList);
                                    RptLatestNewsUrl = ItemUrl;
                                    rptLatestNews.DataSource = companyList;
                                    rptLatestNews.DataBind();
                                }
                                else //Get item from News list
                                {
                                    newsQuery = QueryAllItemsSortByViewCount;
                                    companyList = Utilities.GetNewsRecords(newsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), ListsName.English.NewsRecord);
                                    if (companyList != null && companyList.Rows.Count > 0)
                                    {
                                        Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.CategoryName, ref companyList);
                                        RptLatestNewsUrl = NewsUrl;
                                        rptLatestNews.DataSource = companyList;
                                        rptLatestNews.DataBind();
                                    }
                                }
                            }
                        }
                        else //Khong phai la tin tieu bieu
                        {
                            //Bind data to Latest news
                            string latestNewsQuery1 = QueryAllItemsSortByViewCount;

                            var companyList = Utilities.GetNewsRecords(latestNewsQuery1, GetNewsNumber(WebPartParent.LatestNewsNumber), listName);
                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName, ref companyList);
                                RptLatestNewsUrl = ItemUrl;
                                rptLatestNews.DataSource = companyList;
                                rptLatestNews.DataBind();
                            }
                            else //Get all news from NewsRecord
                            {
                                companyList = Utilities.GetNewsRecords(latestNewsQuery1, GetNewsNumber(WebPartParent.LatestNewsNumber), ListsName.English.NewsRecord);
                                if (companyList != null && companyList.Rows.Count > 0)
                                {
                                    Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.CategoryName, ref companyList);
                                    RptLatestNewsUrl = NewsUrl;
                                    rptLatestNews.DataSource = companyList;
                                    rptLatestNews.DataBind();
                                }
                            }
                            //End
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
                mainItemQuery =
                    string.Format(@"<Where>
                                      <And>
                                         <Eq>
                                            <FieldRef Name='{0}' />
                                            <Value Type='Boolean'>1</Value>
                                         </Eq>
                                         <And>
                                            <Eq>
                                               <FieldRef Name='{1}' />
                                               <Value Type='Lookup'>1</Value>
                                            </Eq>
                                            <And>
                                               <Lt>
                                                  <FieldRef Name='ArticleStartDate' />
                                                  <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                               </Lt>
                                               <Neq>
                                                  <FieldRef Name='Status' />
                                                  <Value Type='Boolean'>1</Value>
                                               </Neq>
                                            </And>
                                         </And>
                                      </And>
                                   </Where>
                                   <OrderBy>
                                      <FieldRef Name='ID' Ascending='False' />
                                   </OrderBy>",
                        FieldsName.NewsRecord.English.ShowInHomePage,
                        FieldsName.NewsRecord.English.CategoryName,
                        SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
            }
            else
            {
                mainItemQuery = QueryAllItemsByShowInHomePage;
            }

            var mainItem = Utilities.GetNewsRecordItems(mainItemQuery, 3, _listName);

            if (mainItem != null && mainItem.Count > 0)
            {
                RptImagesUrl = ItemUrl;
                rptImages.DataSource = Utilities.GetTableWithCorrectUrl(_listCategoryName, mainItem);
                rptImages.DataBind();
            }
            else
            {
                mainItemQuery = QueryAllItemsByShowInHomePage;

                mainItem = Utilities.GetNewsRecordItems(mainItemQuery, 3, _listName);

                if (mainItem != null && mainItem.Count > 0)
                {
                    RptImagesUrl = ItemUrl;
                    rptImages.DataSource = Utilities.GetTableWithCorrectUrl(_listCategoryName, mainItem);
                    rptImages.DataBind();
                }
                else
                {
                    mainItemQuery = QueryAllItemsByShowInHomePage;
                    mainItem = Utilities.GetNewsRecordItems(mainItemQuery, 3, ListsName.English.NewsRecord);
                    if (mainItem != null && mainItem.Count > 0)
                    {
                        RptImagesUrl = NewsUrl;
                        rptImages.DataSource = Utilities.GetTableWithCorrectUrl(ListsName.English.NewsCategory, mainItem);
                        rptImages.DataBind();
                    }
                }
            }
            #endregion
            #region  tin tuc phia duoi

            string threeeItemsBellow = string.Empty;
            if (!string.IsNullOrEmpty(categoryId) && !"-1".Equals(categoryId)) //if categoryId !=null
            {
                threeeItemsBellow =
                    string.Format(
                        @"<Where>
                                                          <And>
                                                             <Eq>
                                                                <FieldRef Name='{0}' />
                                                                <Value Type='Boolean'>1</Value>
                                                             </Eq>
                                                             <And>
                                                                <Eq>
                                                                   <FieldRef Name='{1}' />
                                                                   <Value Type='Lookup'>1</Value>
                                                                </Eq>
                                                                <And>
                                                                   <Lt>
                                                                      <FieldRef Name='ArticleStartDate' />
                                                                      <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                   </Lt>
                                                                   <Neq>
                                                                      <FieldRef Name='Status' />
                                                                      <Value Type='Boolean'>1</Value>
                                                                   </Neq>
                                                                </And>
                                                             </And>
                                                          </And>
                                                       </Where>
                                                       <OrderBy>
                                                          <FieldRef Name='ID' Ascending='False' />
                                                       </OrderBy>",
                        FieldsName.NewsRecord.English.ShowInHomePage,
                        FieldsName.NewsRecord.English.CategoryName,
                        SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
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
            else
            {
                threeeItemsBellow = QueryAllItemsByShowInHomePage;

                threeItemsTable = Utilities.GetNewsRecords(threeeItemsBellow, 6, _listName);
                if (threeItemsTable != null && threeItemsTable.Rows.Count > 0)
                {
                    RptThreeItemUrl = ItemUrl;
                    rptThreeItem.DataSource = GetItemFromThreePosition(threeItemsTable, _listCategoryName);
                    rptThreeItem.DataBind();
                }
                else //Se bind du lieu la news
                {
                    threeeItemsBellow = QueryAllItemsByShowInHomePage;
                    threeItemsTable = Utilities.GetNewsRecords(threeeItemsBellow, 6, ListsName.English.NewsRecord);
                    if (threeItemsTable != null && threeItemsTable.Rows.Count > 0)
                    {
                        RptThreeItemUrl = NewsUrl;
                        rptThreeItem.DataSource = GetItemFromThreePosition(threeItemsTable, ListsName.English.NewsCategory);
                        rptThreeItem.DataBind();
                    }
                }
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


            if (dataTable != null && dataTable.Rows.Count > 3)
            {
                if (dataTable.Rows.Count >= 6)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        DataRow newRow = dataTableTemp.NewRow();
                        newRow[FieldsName.Title] = Convert.ToString(dataTable.Rows[i][FieldsName.Title]);
                        newRow[FieldsName.Id] = dataTable.Rows[i][FieldsName.Id];
                        newRow[FieldsName.CategoryId] = Utilities.GetCategoryIdByCategoryName(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.CategoryName]), _listCategoryName);
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
                        newRow[FieldsName.CategoryId] = Utilities.GetCategoryIdByCategoryName(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.CategoryName]), _listCategoryName);
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
