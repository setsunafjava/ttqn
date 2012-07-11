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

                    string latestNewsQuery = string.Empty;


                    #region Latest News
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
                           </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));

                    var latestNewsTable = Utilities.GetNewsRecords(latestNewsQuery, GetNewsNumber(WebPartParent.LatestNewsNumber), ListsName.English.NewsRecord);
                    if (latestNewsTable != null && latestNewsTable.Rows.Count > 0)
                    {
                        rptLatestNews.DataSource = latestNewsTable;
                        rptLatestNews.DataBind();
                    }
                    #endregion

                    #region Top News
                    string topNewsQuery = string.Format(
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
                           </OrderBy>", FieldsName.NewsRecord.English.ViewsCount);

                    var topViewsTable = Utilities.GetNewsRecords(topNewsQuery, GetNewsNumber(WebPartParent.ReadMoreNumber), ListsName.English.NewsRecord);
                    if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                    {
                        rptTopViews.DataSource = topViewsTable;
                        rptTopViews.DataBind();
                    }
                    #endregion
                    string mainItemQuery = string.Format(@" <Where>
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
                                                           </OrderBy>", FieldsName.NewsRecord.English.ShowInHomePage,
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
                    var mainItem = Utilities.GetNewsRecordItems(mainItemQuery, 3, ListsName.English.NewsRecord);
                    if (mainItem != null && mainItem.Count > 0)
                    {
                        rptImages.DataSource = Utilities.GetTableWithCorrectUrl(mainItem);
                        rptImages.DataBind();
                    }

                    #region # tin tuc phia duoi
                    string threeeItemsBellow = string.Format(@" <Where>
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
                                                           </OrderBy>", FieldsName.NewsRecord.English.ShowInHomePage,
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
                    var threeItemsTable = Utilities.GetNewsRecords(threeeItemsBellow, 6, ListsName.English.NewsRecord);
                    if (threeItemsTable != null && threeItemsTable.Rows.Count > 0)
                    {
                        rptThreeItem.DataSource = GetItemFromThreePosition(threeItemsTable);
                        rptThreeItem.DataBind();
                    }
                    #endregion



                    if ("0".Equals(WebPartParent.WebpartName))
                    {
                        rptTopViews.Visible = false;
                        pnlIndex.Visible = false;
                        pnlSubPage.Visible = true;
                        DataTable tempTable;
                        DataTable otherNewsTable = null;
                        var categoryId = Request.QueryString["CategoryId"];

                        if (!string.IsNullOrEmpty(categoryId))
                        {
                            Utilities.GetNewsByCatID(ListsName.English.NewsRecord, ListsName.English.NewsCategory, Convert.ToString(categoryId), ref otherNewsTable);
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
                catch (Exception ex)
                {
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


            if (dataTable != null && dataTable.Rows.Count > 3)
            {
                if (dataTable.Rows.Count >= 6)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        DataRow newRow = dataTableTemp.NewRow();
                        newRow[FieldsName.Title] = Convert.ToString(dataTable.Rows[i][FieldsName.Title]);
                        newRow[FieldsName.Id] = dataTable.Rows[i][FieldsName.Id];

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
