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
                       ListsName.English.SubNewsCategory,
                       ListsName.English.SubNewsRecord,
                       Constants.NewsId);

                    var newsNumber = GetNewsNumber(WebpartParent.NewsNumber);

                    if (!string.IsNullOrEmpty(WebpartParent.NewsCategoryId1))
                    {
                        hplFirstGroup.Text = WebpartParent.NewsCategoryName1;
                        hplFirstGroup.NavigateUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}={5}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.SubPage,
                                                       ListsName.English.SubNewsCategory,
                                                       ListsName.English.SubNewsRecord,
                                                       Constants.CategoryId,
                                                       WebpartParent.NewsCategoryId1);


                        var group1Table = Utilities.GetNewsByCatID(ListsName.English.SubNewsRecord, WebpartParent.NewsCategoryId1, newsNumber);
                        if (group1Table != null && group1Table.Rows.Count > 0)
                        {
                            var table1 = Utilities.GetTableWithCorrectUrl(group1Table, true);
                            lblHeaderTinhUy.Text = Convert.ToString(table1.Rows[0][FieldsName.Title]);
                            img1.ImageUrl = Utilities.GetThumbnailImagePath(Convert.ToString(table1.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]));

                            NewsFirstUrl1 = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}={5}&CategoryId={6}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.DetailNews,
                                                       ListsName.English.SubNewsCategory,
                                                       ListsName.English.SubNewsRecord,
                                                       Constants.NewsId,
                                                       Convert.ToString(table1.Rows[0][FieldsName.Id]),
                                                       Convert.ToString(table1.Rows[0][FieldsName.CategoryId]));

                            table1.Rows.RemoveAt(0);
                            var defaulViews1 = table1.DefaultView;
                            rptTinhUy.DataSource = defaulViews1;
                            rptTinhUy.DataBind();
                        }

                    }
                    if (!string.IsNullOrEmpty(WebpartParent.NewsCategoryId2))
                    {
                        hplSecondGroup.Text = WebpartParent.NewsCategoryName2;
                        hplSecondGroup.NavigateUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}={5}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.SubPage,
                                                       ListsName.English.SubNewsCategory,
                                                       ListsName.English.SubNewsRecord,
                                                       Constants.CategoryId,
                                                       WebpartParent.NewsCategoryId2);

                        var group2Table = Utilities.GetNewsByCatID(ListsName.English.SubNewsRecord, WebpartParent.NewsCategoryId2, newsNumber);
                        if (group2Table != null && group2Table.Rows.Count > 0)
                        {
                            var table2 = Utilities.GetTableWithCorrectUrl(group2Table, true);
                            lblHeaderHoiDongNhanDan.Text = Convert.ToString(table2.Rows[0][FieldsName.Title]);
                            Img2.ImageUrl = Utilities.GetThumbnailImagePath(Convert.ToString(table2.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]));
                            NewsFirstUrl2 = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}={5}&CategoryId={6}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.DetailNews,
                                                       ListsName.English.SubNewsCategory,
                                                       ListsName.English.SubNewsRecord,
                                                       Constants.NewsId,
                                                       Convert.ToString(table2.Rows[0][FieldsName.Id]),
                                                       Convert.ToString(table2.Rows[0][FieldsName.CategoryId]));

                            table2.Rows.RemoveAt(0);
                            var defaulViews2 = table2.DefaultView;
                            rptHoiDongNhanDan.DataSource = defaulViews2;
                            rptHoiDongNhanDan.DataBind();
                        }
                    }
                    if (!string.IsNullOrEmpty(WebpartParent.NewsCategoryId3))
                    {
                        hplThirdGroup.Text = WebpartParent.NewsCategoryName3;
                        hplThirdGroup.NavigateUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}={5}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.SubPage,
                                                       ListsName.English.SubNewsCategory,
                                                       ListsName.English.SubNewsRecord,
                                                       Constants.CategoryId,
                                                       WebpartParent.NewsCategoryId3);
                        var group3Table = Utilities.GetNewsByCatID(ListsName.English.SubNewsRecord, WebpartParent.NewsCategoryId3, newsNumber);
                        if (group3Table != null && group3Table.Rows.Count > 0)
                        {
                            var table3 = Utilities.GetTableWithCorrectUrl(group3Table, true);
                            lblHeaderUyBanNhanDan.Text = Convert.ToString(table3.Rows[0][FieldsName.Title]);
                            Img3.ImageUrl = Utilities.GetThumbnailImagePath(Convert.ToString(table3.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]));
                            NewsFirstUrl3 = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}={5}&CategoryId={6}",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.DetailNews,
                                                       ListsName.English.SubNewsCategory,
                                                       ListsName.English.SubNewsRecord,
                                                       Constants.NewsId,
                                                       Convert.ToString(table3.Rows[0][FieldsName.Id]),
                                                       Convert.ToString(table3.Rows[0][FieldsName.CategoryId]));

                            table3.Rows.RemoveAt(0);
                            var newTable = GetFiveRows(table3);
                            var defaulViews3 = newTable.DefaultView;
                            rptUyBanNhanDan.DataSource = defaulViews3;
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
