using System;
using System.Data;
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
                    //if select tinh uy
                    if ("1".Equals(WebpartParent.NewsType))
                    {
                        NewsUrl = string.Format("{0}/{1}.aspx?{2}=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId);
                        lblFirstGroup.Text = FieldsName.NewsRecord.FieldValuesDefault.TinhUy;
                        lblSecondGroup.Text = FieldsName.NewsRecord.FieldValuesDefault.HoiDongNhanDan;
                        lblThirdGroup.Text = FieldsName.NewsRecord.FieldValuesDefault.UyBanNhanDan;
                        //Tinh uy
                        string tinhUyNewsQuery =
                            string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.TinhUy));
                        var tinhUyNewsTable = Utilities.GetNewsRecords(tinhUyNewsQuery, 5, ListsName.English.NewsRecord);

                        if (tinhUyNewsTable != null && tinhUyNewsTable.Rows.Count > 0)
                        {
                            var table1 = Utilities.GetTableWithCorrectUrl(tinhUyNewsTable);
                            lblHeaderTinhUy.Text = Convert.ToString(tinhUyNewsTable.Rows[0][FieldsName.Title]);
                            img1.ImageUrl = Convert.ToString(table1.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl1 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(tinhUyNewsTable.Rows[0][FieldsName.Id]));
                            tinhUyNewsTable.Rows.RemoveAt(0);
                            rptTinhUy.DataSource = table1;
                            rptTinhUy.DataBind();
                        }

                        //Hoi dong nhan dan
                        string hoiDongNhanDanNewsQuery =
                            string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.HoiDongNhanDan));
                        var hoiDongNhanDanNewsTable = Utilities.GetNewsRecords(hoiDongNhanDanNewsQuery, 5, ListsName.English.NewsRecord);
                        if (hoiDongNhanDanNewsTable != null && hoiDongNhanDanNewsTable.Rows.Count > 0)
                        {
                            var table2 = Utilities.GetTableWithCorrectUrl(hoiDongNhanDanNewsTable);
                            lblHeaderHoiDongNhanDan.Text = Convert.ToString(hoiDongNhanDanNewsTable.Rows[0][FieldsName.Title]);
                            Img2.ImageUrl = Convert.ToString(table2.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl2 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(hoiDongNhanDanNewsTable.Rows[0][FieldsName.Id]));
                            hoiDongNhanDanNewsTable.Rows.RemoveAt(0);
                            rptHoiDongNhanDan.DataSource = table2;
                            rptHoiDongNhanDan.DataBind();
                        }
                        //Ủy ban nhân dân
                        string uyBanNhanDanNewsQuery =
                            string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.UyBanNhanDan));
                        var uyBanNhanDanNewsTable = Utilities.GetNewsRecords(uyBanNhanDanNewsQuery, 5, ListsName.English.NewsRecord);
                        if (uyBanNhanDanNewsTable != null && uyBanNhanDanNewsTable.Rows.Count > 0)
                        {
                            var table3 = Utilities.GetTableWithCorrectUrl(uyBanNhanDanNewsTable);
                            lblHeaderUyBanNhanDan.Text = Convert.ToString(uyBanNhanDanNewsTable.Rows[0][FieldsName.Title]);
                            Img3.ImageUrl = Convert.ToString(table3.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl3 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(uyBanNhanDanNewsTable.Rows[0][FieldsName.Id]));
                            uyBanNhanDanNewsTable.Rows.RemoveAt(0);
                            rptUyBanNhanDan.DataSource = table3;
                            rptUyBanNhanDan.DataBind();
                        }
                    }
                    else if ("2".Equals(WebpartParent.NewsType))
                    {
                        NewsUrl = string.Format("{0}/{1}.aspx?{2}=", SPContext.Current.Web.Url,
                                                Constants.PageInWeb.DetailNews, Constants.NewsId);

                        lblFirstGroup.Text = FieldsName.NewsRecord.FieldValuesDefault.SoBanNganh;
                        lblSecondGroup.Text = FieldsName.NewsRecord.FieldValuesDefault.DiaPhuong;
                        lblThirdGroup.Text = FieldsName.NewsRecord.FieldValuesDefault.DoanhNghiep;
                        //Tinh uy
                        string tinhUyNewsQuery =
                            string.Format(
                                "<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.SoBanNganh));
                        var tinhUyNewsTable = Utilities.GetNewsRecords(tinhUyNewsQuery, 5, ListsName.English.NewsRecord);

                        if (tinhUyNewsTable != null && tinhUyNewsTable.Rows.Count > 0)
                        {
                            var table4 = Utilities.GetTableWithCorrectUrl(tinhUyNewsTable);
                            lblHeaderTinhUy.Text = Convert.ToString(tinhUyNewsTable.Rows[0][FieldsName.Title]);
                            img1.ImageUrl = Convert.ToString(table4.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl1 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(tinhUyNewsTable.Rows[0][FieldsName.Id]));

                            tinhUyNewsTable.Rows.RemoveAt(0);
                            rptTinhUy.DataSource = table4;
                            rptTinhUy.DataBind();
                        }

                        //Hoi dong nhan dan
                        string hoiDongNhanDanNewsQuery =
                            string.Format(
                                "<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.DiaPhuong));
                        var hoiDongNhanDanNewsTable = Utilities.GetNewsRecords(hoiDongNhanDanNewsQuery, 5, ListsName.English.NewsRecord);
                        if (hoiDongNhanDanNewsTable != null && hoiDongNhanDanNewsTable.Rows.Count > 0)
                        {
                            var table5 = Utilities.GetTableWithCorrectUrl(hoiDongNhanDanNewsTable);
                            lblHeaderHoiDongNhanDan.Text = Convert.ToString(hoiDongNhanDanNewsTable.Rows[0][FieldsName.Title]);
                            Img2.ImageUrl = Convert.ToString(table5.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl2 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(hoiDongNhanDanNewsTable.Rows[0][FieldsName.Id]));

                            hoiDongNhanDanNewsTable.Rows.RemoveAt(0);
                            rptHoiDongNhanDan.DataSource = table5;
                            rptHoiDongNhanDan.DataBind();
                        }
                        //Ủy ban nhân dân
                        string uyBanNhanDanNewsQuery =
                            string.Format(
                                "<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.DoanhNghiep));
                        var uyBanNhanDanNewsTable = Utilities.GetNewsRecords(uyBanNhanDanNewsQuery, 5, ListsName.English.NewsRecord);
                        if (uyBanNhanDanNewsTable != null && uyBanNhanDanNewsTable.Rows.Count > 0)
                        {
                            var table6 = Utilities.GetTableWithCorrectUrl(hoiDongNhanDanNewsTable);
                            lblHeaderUyBanNhanDan.Text = Convert.ToString(uyBanNhanDanNewsTable.Rows[0][FieldsName.Title]);
                            Img3.ImageUrl = Convert.ToString(table6.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl3 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(uyBanNhanDanNewsTable.Rows[0][FieldsName.Id]));
                            uyBanNhanDanNewsTable.Rows.RemoveAt(0);
                            rptUyBanNhanDan.DataSource = table6;
                            rptUyBanNhanDan.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
