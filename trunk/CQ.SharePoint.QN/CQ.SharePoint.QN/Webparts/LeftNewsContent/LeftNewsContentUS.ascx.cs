using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using Microsoft.SharePoint.WebPartPages;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class LeftNewsContentUS : UserControl
    {
        public LeftNewsContent WebpartParent;
        private string _newsCategoryTitle;
        public string NewsUrl = string.Empty;
        public string CategoryUrl = string.Empty;
        public string NewsFirstUrl1 = string.Empty;

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

                    if (!string.IsNullOrEmpty(WebpartParent.GroupName))
                    {
                        lbRSS.Text = WebpartParent.GroupName;
                    }
                    else
                    {
                        lbRSS.Text = "&nbsp;";
                    }
                    string newsGroupQuery = string.Format("<Where><And><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq><Neq><FieldRef Name='Status' /><Value Type='Boolean'>1</Value></Neq></And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.CategoryName, SPHttpUtility.HtmlEncode(WebpartParent.GroupName));
                    var newsGroups = Utilities.GetNewsRecords(newsGroupQuery, Convert.ToUInt16(WebpartParent.NumberOfNews), ListsName.English.NewsRecord);
                    if (newsGroups != null && newsGroups.Rows.Count > 0)
                    {
                        lblHeader.Text = Convert.ToString(newsGroups.Rows[0][FieldsName.Title]);
                        var tempTable = Utilities.GetTableWithCorrectUrl(newsGroups);
                        imgThumb.ImageUrl = Convert.ToString(tempTable.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);

                        lblShortContent.Text = Convert.ToString(newsGroups.Rows[0][FieldsName.NewsRecord.English.ShortDescription]);
                        NewsFirstUrl1 = string.Format("{0}/{1}.aspx?{2}={3}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId, Convert.ToString(newsGroups.Rows[0][FieldsName.Id]));
                        if (newsGroups.Rows.Count > 2)
                        {
                            newsGroups.Rows.RemoveAt(0);
                            rptCaiCachThuTucHanhChinh.DataSource = newsGroups;
                            rptCaiCachThuTucHanhChinh.DataBind();
                        }
                    }

                    CategoryUrl = string.Format("{0}/{1}.aspx?CategoryId=", SPContext.Current.Web.Url, Constants.PageInWeb.SubPage);
                    string newsTitle = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='{2}' Ascending='True' /></OrderBy>", FieldsName.NewsCategory.English.ParentName, WebpartParent.NewsGroupID, FieldsName.Title);
                    var newsTitleItems = Utilities.GetNewsRecords(newsTitle, 4, ListsName.English.NewsCategory);
                    if (newsTitleItems != null && newsTitleItems.Rows.Count > 0)
                    {
                        rptNewsGroup.DataSource = newsTitleItems;
                        rptNewsGroup.DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        /// <summary>
        /// OnpreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            WebPartManager webPartManager = WebPartManager.GetCurrentWebPartManager(Page);
        }

        protected void lbRSS_OnClick(object sender, EventArgs e)
        {
            Utilities.GetRSS(WebpartParent.NewsGroupID);
        }
    }
}
