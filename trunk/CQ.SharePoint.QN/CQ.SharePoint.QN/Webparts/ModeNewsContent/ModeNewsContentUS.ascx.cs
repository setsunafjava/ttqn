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
                        NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url,
                                                Constants.PageInWeb.DetailNews);
                        lblFirstGroup.Text = FieldsName.NewsRecord.FieldValuesDefault.TinhUy;
                        lblSecondGroup.Text = FieldsName.NewsRecord.FieldValuesDefault.HoiDongNhanDan;
                        lblThirdGroup.Text = FieldsName.NewsRecord.FieldValuesDefault.UyBanNhanDan;
                        //Tinh uy
                        string tinhUyNewsQuery =
                            string.Format(
                                "<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.TinhUy));
                        var tinhUyNewsTable = Utilities.GetNewsRecords(tinhUyNewsQuery, 5, ListsName.English.NewsRecord);

                        if (tinhUyNewsTable != null && tinhUyNewsTable.Rows.Count > 0)
                        {
                            lblHeaderTinhUy.Text =
                                Convert.ToString(tinhUyNewsTable.Rows[0][FieldsName.NewsRecord.English.ShortContent]);

                            tinhUyNewsTable.Rows.RemoveAt(0);
                            rptTinhUy.DataSource = tinhUyNewsTable;
                            rptTinhUy.DataBind();
                        }

                        //Hoi dong nhan dan
                        string hoiDongNhanDanNewsQuery =
                            string.Format(
                                "<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.HoiDongNhanDan));
                        var hoiDongNhanDanNewsTable = Utilities.GetNewsRecords(hoiDongNhanDanNewsQuery, 5,
                                                                               ListsName.English.NewsRecord);
                        if (hoiDongNhanDanNewsTable != null && hoiDongNhanDanNewsTable.Rows.Count > 0)
                        {
                            lblHeaderHoiDongNhanDan.Text =
                                Convert.ToString(
                                    hoiDongNhanDanNewsTable.Rows[0][FieldsName.NewsRecord.English.ShortContent]);
                            hoiDongNhanDanNewsTable.Rows.RemoveAt(0);
                            rptHoiDongNhanDan.DataSource = hoiDongNhanDanNewsTable;
                            rptHoiDongNhanDan.DataBind();
                        }
                        //Ủy ban nhân dân
                        string uyBanNhanDanNewsQuery =
                            string.Format(
                                "<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.UyBanNhanDan));
                        var uyBanNhanDanNewsTable = Utilities.GetNewsRecords(uyBanNhanDanNewsQuery, 5,
                                                                             ListsName.English.NewsRecord);
                        if (uyBanNhanDanNewsTable != null && uyBanNhanDanNewsTable.Rows.Count > 0)
                        {
                            lblHeaderUyBanNhanDan.Text =
                                Convert.ToString(
                                    uyBanNhanDanNewsTable.Rows[0][FieldsName.NewsRecord.English.ShortContent]);
                            uyBanNhanDanNewsTable.Rows.RemoveAt(0);
                            rptUyBanNhanDan.DataSource = uyBanNhanDanNewsTable;
                            rptUyBanNhanDan.DataBind();
                        }
                    }
                    else if ("2".Equals(WebpartParent.NewsType))
                    {
                        NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url,
                                                Constants.PageInWeb.DetailNews);

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
                            lblHeaderTinhUy.Text =
                                Convert.ToString(tinhUyNewsTable.Rows[0][FieldsName.NewsRecord.English.ShortContent]);

                            tinhUyNewsTable.Rows.RemoveAt(0);
                            rptTinhUy.DataSource = tinhUyNewsTable;
                            rptTinhUy.DataBind();
                        }

                        //Hoi dong nhan dan
                        string hoiDongNhanDanNewsQuery =
                            string.Format(
                                "<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.DiaPhuong));
                        var hoiDongNhanDanNewsTable = Utilities.GetNewsRecords(hoiDongNhanDanNewsQuery, 5,
                                                                               ListsName.English.NewsRecord);
                        if (hoiDongNhanDanNewsTable != null && hoiDongNhanDanNewsTable.Rows.Count > 0)
                        {
                            lblHeaderHoiDongNhanDan.Text =
                                Convert.ToString(
                                    hoiDongNhanDanNewsTable.Rows[0][FieldsName.NewsRecord.English.ShortContent]);
                            hoiDongNhanDanNewsTable.Rows.RemoveAt(0);
                            rptHoiDongNhanDan.DataSource = hoiDongNhanDanNewsTable;
                            rptHoiDongNhanDan.DataBind();
                        }
                        //Ủy ban nhân dân
                        string uyBanNhanDanNewsQuery =
                            string.Format(
                                "<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                                FieldsName.NewsRecord.English.CategoryName,
                                SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.DoanhNghiep));
                        var uyBanNhanDanNewsTable = Utilities.GetNewsRecords(uyBanNhanDanNewsQuery, 5,
                                                                             ListsName.English.NewsRecord);
                        if (uyBanNhanDanNewsTable != null && uyBanNhanDanNewsTable.Rows.Count > 0)
                        {
                            lblHeaderUyBanNhanDan.Text =
                                Convert.ToString(
                                    uyBanNhanDanNewsTable.Rows[0][FieldsName.NewsRecord.English.ShortContent]);
                            uyBanNhanDanNewsTable.Rows.RemoveAt(0);
                            rptUyBanNhanDan.DataSource = uyBanNhanDanNewsTable;
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
