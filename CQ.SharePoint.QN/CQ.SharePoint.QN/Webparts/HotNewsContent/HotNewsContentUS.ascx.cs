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
        public string NewsUrl = string.Empty;
        public string Linktoitem = string.Empty;
        public string listName = ListsName.English.NewsRecord;
        public string listCategoryName = ListsName.English.NewsCategory;
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
                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                                            SPContext.Current.Web.Url,
                                            Constants.PageInWeb.DetailNews,
                                            ListsName.English.NewsCategory,
                                            ListsName.English.NewsRecord,
                                            Constants.NewsId);
                    var categoryId = Request.QueryString["CategoryId"];
                    string latestNewsQuery = string.Empty;

                    var listNameTemp = Request.QueryString[Constants.ListName];
                    var listCategoryNameTemp = Request.QueryString[Constants.ListCategoryName];
                    var focusNews = Request.QueryString["FocusNews"];
                    if (!string.IsNullOrEmpty(listNameTemp)) listName = listNameTemp;
                    if (!string.IsNullOrEmpty(listCategoryNameTemp)) listCategoryName = listCategoryNameTemp;

                    if (!"0".Equals(WebPartParent.WebpartName))
                    {
                        #region Latest News

                        if (!string.IsNullOrEmpty(categoryId)) //if categoryId !=null
                        {
                            //CAML query will get all item with category id = categoryId
                            latestNewsQuery =
                                string.Format(@"<Where>
                                                  <And>
                                                     <Neq>
                                                        <FieldRef Name='Status' />
                                                        <Value Type='Boolean'>1</Value>
                                                     </Neq>
                                                     <And>
                                                        <Eq>
                                                           <FieldRef Name='{0}' LookupId='TRUE' />
                                                           <Value Type='Lookup'>{1}</Value>
                                                        </Eq>
                                                        <Lt>
                                                           <FieldRef Name='ArticleStartDate' />
                                                           <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                        </Lt>
                                                     </And>
                                                  </And>
                                               </Where>
                                               <OrderBy>
                                                  <FieldRef Name='ID' Ascending='False' />
                                               </OrderBy>",
                                    FieldsName.NewsRecord.English.CategoryName,
                                    categoryId,
                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
                        }
                        else
                        {
                            //CAML query will get all item 
                            latestNewsQuery =
                                string.Format(
                                    @"<Where>
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
                        }


                        var latestNewsTable = Utilities.GetNewsRecordItems(latestNewsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), listName);
                        if (latestNewsTable != null && latestNewsTable.Count > 0)
                        {
                            rptLatestNews.DataSource = Utilities.GetTableWithCorrectUrl(listCategoryName, latestNewsTable);
                            rptLatestNews.DataBind();
                        }

                        #endregion

                        #region Top News

                        string topNewsQuery = string.Empty;
                        if (!string.IsNullOrEmpty(categoryId)) //if categoryId !=null
                        {
                            topNewsQuery =
                                string.Format(
                                    @"<Where>
                                                              <And>
                                                                 <Neq>
                                                                    <FieldRef Name='Status' />
                                                                    <Value Type='Boolean'>1</Value>
                                                                 </Neq>
                                                                 <And>
                                                                    <Eq>
                                                                       <FieldRef Name='{0}' LookupId='TRUE' />
                                                                       <Value Type='Lookup'>{1}</Value>
                                                                    </Eq>
                                                                    <Lt>
                                                                       <FieldRef Name='ArticleStartDate' />
                                                                       <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                    </Lt>
                                                                 </And>
                                                              </And>
                                                           </Where>
                                                           <OrderBy>
                                                              <FieldRef Name='ViewsCount' Ascending='False' />
                                                           </OrderBy>",
                                    FieldsName.NewsRecord.English.CategoryName,
                                    categoryId,
                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
                        }
                        else
                        {
                            topNewsQuery = string.Format(
                                @"<Where>
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
                        }

                        var topViewsTable = Utilities.GetNewsRecordItems(topNewsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), listName);
                        if (topViewsTable != null && topViewsTable.Count > 0)
                        {
                            rptTopViews.DataSource = Utilities.GetTableWithCorrectUrl(listCategoryName, topViewsTable);
                            rptTopViews.DataBind();
                        }

                        #endregion

                        string mainItemQuery = string.Empty;
                        if (!string.IsNullOrEmpty(categoryId)) //if categoryId !=null
                        {
                            mainItemQuery =
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
                            mainItemQuery =
                                string.Format(
                                    @" <Where>
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
                        }
                        //tai sao lai get duoc moi 1 item, trong khi do camlquery la 3?
                        var mainItem = Utilities.GetNewsRecordItems(mainItemQuery, 3, listName);

                        if (mainItem != null && mainItem.Count > 0)
                        {
                            rptImages.DataSource = Utilities.GetTableWithCorrectUrl(listCategoryName, mainItem);
                            rptImages.DataBind();
                        }

                        #region # tin tuc phia duoi

                        string threeeItemsBellow = string.Empty;
                        if (!string.IsNullOrEmpty(categoryId)) //if categoryId !=null
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
                            threeeItemsBellow =
                                string.Format(
                                    @" <Where>
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
                        }

                        var threeItemsTable = Utilities.GetNewsRecords(threeeItemsBellow, 6, listName);
                        if (threeItemsTable != null && threeItemsTable.Rows.Count > 0)
                        {
                            rptThreeItem.DataSource = GetItemFromThreePosition(threeItemsTable);
                            rptThreeItem.DataBind();
                        }

                        #endregion
                    }

                    //Neu khi setup webpart la 0
                    else if ("0".Equals(WebPartParent.WebpartName))
                    {
                        rptTopViews.Visible = false;
                        pnlIndex.Visible = false;
                        pnlSubPage.Visible = true;


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

                            var companyList = Utilities.GetNewsRecords(newsQuery, listName);
                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                rptLatestNews.DataSource = companyList;
                                rptLatestNews.DataBind();
                            }
                        }
                        else //Khong phai la tin tieu bieu
                        {
                            DataTable tempTable;
                            DataTable otherNewsTable = null;

                            if (!string.IsNullOrEmpty(categoryId))
                            {
                                Utilities.GetNewsByCatID(listName, listCategoryName, Convert.ToString(categoryId), ref otherNewsTable);
                            }
                            else
                            {
                                latestNewsQuery = string.Format(
                                                               @"<Where>
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
                                                               </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));



                                otherNewsTable = Utilities.GetNewsRecords(latestNewsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), listName);
                            }

                            if (otherNewsTable != null && otherNewsTable.Rows.Count > 0)
                            {
                                otherNewsTable.Rows.RemoveAt(0);
                                //end
                                tempTable = otherNewsTable.Clone();
                                if (otherNewsTable.Rows.Count > 5)
                                {
                                    for (int i = 0; i < 5; i++)
                                    {
                                        tempTable.ImportRow(otherNewsTable.Rows[i]);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < otherNewsTable.Rows.Count; i++)
                                    {
                                        tempTable.ImportRow(otherNewsTable.Rows[i]);
                                    }
                                }
                                if (tempTable.Rows.Count > 0)
                                {
                                    Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryId, ref tempTable);
                                    rptLatestNews.DataSource = tempTable;
                                    rptLatestNews.DataBind();
                                }
                                else
                                {
                                    rptLatestNews.DataSource = null;
                                    rptLatestNews.DataBind();
                                }
                            }
                            else
                            {
                                rptLatestNews.DataSource = null;
                                rptLatestNews.DataBind();
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

        /// <summary>
        /// Ham nay se get tiep ra 3 item de bind vao phia duoi tin chinh
        /// </summary>
        /// <returns></returns>
        public DataTable GetItemFromThreePosition(DataTable dataTable)
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
                        newRow[FieldsName.CategoryId] = Utilities.GetCategoryIdByCategoryName(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.CategoryName]), listCategoryName);
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
                        newRow[FieldsName.CategoryId] = Utilities.GetCategoryIdByCategoryName(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.CategoryName]), listCategoryName);
                        dataTableTemp.Rows.Add(newRow);
                    }
                }
            }
            return dataTableTemp;
        }

        private uint GetNewsNumber(string value)
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
