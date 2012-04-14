using System;
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
    public partial class LeftNewsContentUS : UserControl
    {
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
                    string caiCachThuTucQuery = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.CategoryName, SPHttpUtility.HtmlEncode(FieldsName.NewsRecord.FieldValuesDefault.CaiCachHanhChinh));
                    var caiCachThuTucNews = Utilities.GetNewsRecords(caiCachThuTucQuery, 3, ListsName.English.NewsRecord);
                    if (caiCachThuTucNews != null && caiCachThuTucNews.Rows.Count > 0)
                    {
                        lblHeader.Text = Convert.ToString(caiCachThuTucNews.Rows[0][FieldsName.Title]);
                        lblShortContent.Text = Convert.ToString(caiCachThuTucNews.Rows[0][FieldsName.NewsRecord.English.ShortContent]);
                        rptCaiCachThuTucHanhChinh.DataSource = caiCachThuTucNews;
                        rptCaiCachThuTucHanhChinh.DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
