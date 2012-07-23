using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class ModeNewsContentUS : UserControl
    {
        public ModeNewsContent WebpartParent;
        public string NewsUrl = string.Empty;
        public string NewsFirstUrl1 = string.Empty;
        public string NewsFirstUrl2 = string.Empty;
        public string NewsFirstUrl3 = string.Empty;
        public string CategoryUrl = string.Empty;

        /// <summary>
        /// Get news of number
        /// </summary>
        /// <param name="newsNumber"></param>
        /// <returns></returns>
        protected uint GetNewsNumber(string newsNumber)
        {
            uint result = 6;
            try
            {
                var numbertemp = Convert.ToUInt32(newsNumber);
                if (numbertemp > 0) result = numbertemp;
            }
            catch (Exception ex)
            {
            }
            return result;
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
                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                       SPContext.Current.Web.Url,
                       Constants.PageInWeb.DetailNews,
                       ListsName.English.NewsCategory,
                       ListsName.English.NewsRecord,
                       Constants.NewsId);

                    if (!string.IsNullOrEmpty(WebpartParent.NewsCategoryId1))
                    {
                        hplFirstGroup.Text = WebpartParent.NewsCategoryName1;
                        hplFirstGroup.NavigateUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}={5}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.SubPage,
                                                       ListsName.English.NewsCategory,
                                                       ListsName.English.NewsRecord,
                                                       Constants.CategoryId,
                                                       WebpartParent.NewsCategoryId1);

                        string group1Query = string.Format(@"<Where>
                                                              <And>
                                                                 <Eq>
                                                                    <FieldRef Name='{0}' LookupId='TRUE' />
                                                                    <Value Type='Lookup'>{1}</Value>
                                                                 </Eq>
                                                                 <And>
                                                                    <Neq>
                                                                       <FieldRef Name='Status' />
                                                                       <Value Type='Boolean'>1</Value>
                                                                    </Neq>
                                                                    <And>
                                                                       <Lt>
                                                                          <FieldRef Name='ArticleStartDate' />
                                                                          <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                       </Lt>
                                                                       <Contains>
                                                                          <FieldRef Name='Approve' />
                                                                          <Value Type='Lookup'>{3}</Value>
                                                                       </Contains>
                                                                    </And>
                                                                 </And>
                                                              </And>
                                                           </Where>
                                                           <OrderBy>
                                                              <FieldRef Name='ID' Ascending='False' />
                                                           </OrderBy>",
                                                                      FieldsName.NewsRecord.English.CategoryName,
                                                                      WebpartParent.NewsCategoryId1,
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                      Constants.Published);
                        var group1Table = Utilities.GetNewsRecordItems(group1Query, GetNewsNumber(WebpartParent.NewsNumber), ListsName.English.NewsRecord);

                        if (group1Table != null && group1Table.Count > 0)
                        {
                            var table1 = Utilities.GetTableWithCorrectUrl(group1Table, true);
                            Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.CategoryName, ref table1);
                            lblHeaderTinhUy.Text = Convert.ToString(table1.Rows[0][FieldsName.Title]);
                            img1.ImageUrl = Convert.ToString(table1.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);

                            NewsFirstUrl1 = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}={5}&CategoryId={6}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.DetailNews,
                                                       ListsName.English.NewsCategory,
                                                       ListsName.English.NewsRecord,
                                                       Constants.NewsId,
                                                       Convert.ToString(table1.Rows[0][FieldsName.Id]),
                                                       Convert.ToString(table1.Rows[0][FieldsName.CategoryId]));

                            table1.Rows.RemoveAt(0);
                            Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.NewsRecord.English.CategoryName, ref table1);
                            rptTinhUy.DataSource = table1;
                            rptTinhUy.DataBind();
                        }

                    }
                    if (!string.IsNullOrEmpty(WebpartParent.NewsCategoryId2))
                    {
                        hplSecondGroup.Text = WebpartParent.NewsCategoryName2;
                        hplSecondGroup.NavigateUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}={5}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.SubPage,
                                                       ListsName.English.NewsCategory,
                                                       ListsName.English.NewsRecord,
                                                       Constants.CategoryId,
                                                       WebpartParent.NewsCategoryId2);

                        string group2Query = string.Format(@"<Where>
                                                              <And>
                                                                 <Eq>
                                                                    <FieldRef Name='{0}' LookupId='TRUE' />
                                                                    <Value Type='Lookup'>{1}</Value>
                                                                 </Eq>
                                                                 <And>
                                                                    <Neq>
                                                                       <FieldRef Name='Status' />
                                                                       <Value Type='Boolean'>1</Value>
                                                                    </Neq>
                                                                    <And>
                                                                       <Lt>
                                                                          <FieldRef Name='ArticleStartDate' />
                                                                          <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                       </Lt>
                                                                       <Contains>
                                                                          <FieldRef Name='Approve' />
                                                                          <Value Type='Lookup'>{3}</Value>
                                                                       </Contains>
                                                                    </And>
                                                                 </And>
                                                              </And>
                                                           </Where>
                                                           <OrderBy>
                                                              <FieldRef Name='ID' Ascending='False' />
                                                           </OrderBy>",
                                                                      FieldsName.NewsRecord.English.CategoryName,
                                                                      WebpartParent.NewsCategoryId2,
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                      Constants.Published);

                        var group2Table = Utilities.GetNewsRecordItems(group2Query, GetNewsNumber(WebpartParent.NewsNumber), ListsName.English.NewsRecord);

                        if (group2Table != null && group2Table.Count > 0)
                        {
                            var table2 = Utilities.GetTableWithCorrectUrl(group2Table, true);
                            Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.CategoryName, ref table2);
                            lblHeaderHoiDongNhanDan.Text = Convert.ToString(table2.Rows[0][FieldsName.Title]);
                            Img2.ImageUrl = Convert.ToString(table2.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl2 = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}={5}&CategoryId={6}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.DetailNews,
                                                       ListsName.English.NewsCategory,
                                                       ListsName.English.NewsRecord,
                                                       Constants.NewsId,
                                                       Convert.ToString(table2.Rows[0][FieldsName.Id]),
                                                       Convert.ToString(table2.Rows[0][FieldsName.CategoryId]));

                            table2.Rows.RemoveAt(0);
                            Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.NewsRecord.English.CategoryName, ref table2);
                            rptHoiDongNhanDan.DataSource = table2;
                            rptHoiDongNhanDan.DataBind();
                        }
                    }
                    if (!string.IsNullOrEmpty(WebpartParent.NewsCategoryId3))
                    {
                        hplThirdGroup.Text = WebpartParent.NewsCategoryName3;
                        hplThirdGroup.NavigateUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}={5}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.SubPage,
                                                       ListsName.English.NewsCategory,
                                                       ListsName.English.NewsRecord,
                                                       Constants.CategoryId,
                                                       WebpartParent.NewsCategoryId3);
                        string group3Query = string.Format(@"<Where>
                                                              <And>
                                                                 <Eq>
                                                                    <FieldRef Name='{0}' LookupId='TRUE' />
                                                                    <Value Type='Lookup'>{1}</Value>
                                                                 </Eq>
                                                                 <And>
                                                                    <Neq>
                                                                       <FieldRef Name='Status' />
                                                                       <Value Type='Boolean'>1</Value>
                                                                    </Neq>
                                                                    <And>
                                                                       <Lt>
                                                                          <FieldRef Name='ArticleStartDate' />
                                                                          <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                       </Lt>
                                                                       <Contains>
                                                                          <FieldRef Name='Approve' />
                                                                          <Value Type='Lookup'>{3}</Value>
                                                                       </Contains>
                                                                    </And>
                                                                 </And>
                                                              </And>
                                                           </Where>
                                                           <OrderBy>
                                                              <FieldRef Name='ID' Ascending='False' />
                                                           </OrderBy>",
                                                                      FieldsName.NewsRecord.English.CategoryName,
                                                                      WebpartParent.NewsCategoryId3,
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                      Constants.Published);

                        var group3Table = Utilities.GetNewsRecordItems(group3Query, GetNewsNumber(WebpartParent.NewsNumber), ListsName.English.NewsRecord);

                        if (group3Table != null && group3Table.Count > 0)
                        {
                            var table3 = Utilities.GetTableWithCorrectUrl(group3Table, true);
                            Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.CategoryName, ref table3);
                            lblHeaderUyBanNhanDan.Text = Convert.ToString(table3.Rows[0][FieldsName.Title]);
                            Img3.ImageUrl = Convert.ToString(table3.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl3 = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}={5}&CategoryId={6}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.DetailNews,
                                                       ListsName.English.NewsCategory,
                                                       ListsName.English.NewsRecord,
                                                       Constants.NewsId,
                                                       Convert.ToString(table3.Rows[0][FieldsName.Id]),
                                                       Convert.ToString(table3.Rows[0][FieldsName.CategoryId]));

                            table3.Rows.RemoveAt(0);
                            Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.NewsRecord.English.CategoryName, ref table3);
                            var newTable = GetFiveRows(table3);
                            rptUyBanNhanDan.DataSource = newTable;
                            rptUyBanNhanDan.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Get five items to display to ModelNewsContent
        /// </summary>
        /// <param name="oldTable">Old table</param>
        /// <returns>New table have 5 items</returns>
        public DataTable GetFiveRows(DataTable oldTable)
        {
            DataTable newTable = null;
            if (oldTable != null)
            {
                if (oldTable.Rows.Count > 5)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        newTable.Rows.Add(oldTable.Rows[i]);
                    }
                }
                else
                {
                    newTable = oldTable;
                }
            }
            return newTable;
        }
    }
}
