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
        public string CategoryUrl = string.Empty;

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
                    NewsUrl = string.Format("{0}/{1}.aspx?{2}=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId);
                    if (!string.IsNullOrEmpty(WebpartParent.NewsCategoryId1))
                    {
                        hplFirstGroup.Text = WebpartParent.NewsCategoryName1;
                        hplFirstGroup.NavigateUrl = string.Format("{0}/{1}.aspx?CategoryId={2}", SPContext.Current.Web.Url, Constants.PageInWeb.SubPage, WebpartParent.NewsCategoryId1);

                        string group1Query = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='Lookup'>{1}</Value></Eq></Where>", FieldsName.NewsRecord.English.CategoryName, WebpartParent.NewsCategoryId1);
                        var group1Table = Utilities.GetNewsRecords(group1Query, GetNewsNumber(WebpartParent.NewsNumber), ListsName.English.NewsRecord);

                        if (group1Table != null && group1Table.Rows.Count > 0)
                        {
                            var table1 = Utilities.GetTableWithCorrectUrl(group1Table);
                            lblHeaderTinhUy.Text = Convert.ToString(group1Table.Rows[0][FieldsName.Title]);
                            img1.ImageUrl = Convert.ToString(table1.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl1 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(group1Table.Rows[0][FieldsName.Id]));
                            group1Table.Rows.RemoveAt(0);
                            rptTinhUy.DataSource = table1;
                            rptTinhUy.DataBind();
                        }

                    }
                    if (!string.IsNullOrEmpty(WebpartParent.NewsCategoryId2))
                    {
                        hplSecondGroup.Text = WebpartParent.NewsCategoryName2;
                        hplSecondGroup.NavigateUrl = string.Format("{0}/{1}.aspx?CategoryId={2}", SPContext.Current.Web.Url, Constants.PageInWeb.SubPage, WebpartParent.NewsCategoryId2);
                        string group2Query = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='Lookup'>{1}</Value></Eq></Where>", FieldsName.NewsRecord.English.CategoryName, WebpartParent.NewsCategoryId2);
                        var group2Table = Utilities.GetNewsRecords(group2Query, GetNewsNumber(WebpartParent.NewsNumber), ListsName.English.NewsRecord);

                        if (group2Table != null && group2Table.Rows.Count > 0)
                        {
                            var table2 = Utilities.GetTableWithCorrectUrl(group2Table);
                            lblHeaderHoiDongNhanDan.Text = Convert.ToString(group2Table.Rows[0][FieldsName.Title]);
                            Img2.ImageUrl = Convert.ToString(table2.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl2 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(group2Table.Rows[0][FieldsName.Id]));
                            group2Table.Rows.RemoveAt(0);
                            rptHoiDongNhanDan.DataSource = table2;
                            rptHoiDongNhanDan.DataBind();
                        }
                    }
                    if (!string.IsNullOrEmpty(WebpartParent.NewsCategoryId3))
                    {
                        hplThirdGroup.Text = WebpartParent.NewsCategoryName3;
                        hplThirdGroup.NavigateUrl = string.Format("{0}/{1}.aspx?CategoryId={2}", SPContext.Current.Web.Url, Constants.PageInWeb.SubPage, WebpartParent.NewsCategoryId3);
                        string group3Query = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='Lookup'>{1}</Value></Eq></Where>", FieldsName.NewsRecord.English.CategoryName, WebpartParent.NewsCategoryId3);
                        var group3Table = Utilities.GetNewsRecords(group3Query, GetNewsNumber(WebpartParent.NewsNumber), ListsName.English.NewsRecord);

                        if (group3Table != null && group3Table.Rows.Count > 0)
                        {
                            var table3 = Utilities.GetTableWithCorrectUrl(group3Table);
                            lblHeaderUyBanNhanDan.Text = Convert.ToString(group3Table.Rows[0][FieldsName.Title]);
                            Img3.ImageUrl = Convert.ToString(table3.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                            NewsFirstUrl3 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(group3Table.Rows[0][FieldsName.Id]));
                            group3Table.Rows.RemoveAt(0);
                            rptUyBanNhanDan.DataSource = table3;
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
