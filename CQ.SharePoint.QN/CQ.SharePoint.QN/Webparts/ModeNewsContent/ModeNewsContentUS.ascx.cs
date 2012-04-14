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
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    //Tinh uy
                    string tinhUyNewsQuery = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.CategoryName, SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.TinhUy));
                    var tinhUyNewsTable = Utilities.GetNewsRecords(tinhUyNewsQuery, 5, ListsName.English.NewsRecord);
                    if (tinhUyNewsTable != null && tinhUyNewsTable.Rows.Count > 0)
                    {
                        rptTinhUy.DataSource = tinhUyNewsTable;
                        rptTinhUy.DataBind();
                    }

                    //Hoi dong nhan dan
                    string hoiDongNhanDanNewsQuery = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.CategoryName, SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.HoiDongNhanDan));
                    var hoiDongNhanDanNewsTable = Utilities.GetNewsRecords(hoiDongNhanDanNewsQuery, 5,ListsName.English.NewsRecord);
                    if (hoiDongNhanDanNewsTable != null && hoiDongNhanDanNewsTable.Rows.Count > 0)
                    {
                        rptHoiDongNhanDan.DataSource = hoiDongNhanDanNewsTable;
                        rptHoiDongNhanDan.DataBind();
                    }
                    //Ủy ban nhân dân
                    string uyBanNhanDanNewsQuery = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.CategoryName, SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.UyBanNhanDan));
                    var uyBanNhanDanNewsTable = Utilities.GetNewsRecords(uyBanNhanDanNewsQuery, 5,ListsName.English.NewsRecord);
                    if (uyBanNhanDanNewsTable != null && uyBanNhanDanNewsTable.Rows.Count > 0)
                    {
                        rptUyBanNhanDan.DataSource = uyBanNhanDanNewsTable;
                        rptUyBanNhanDan.DataBind();
                    }

                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
