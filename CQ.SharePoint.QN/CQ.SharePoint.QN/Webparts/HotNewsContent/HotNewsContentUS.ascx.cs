using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
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
                    //Bind data to latest news
                    NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews);

                    var categoryId = Request.QueryString["CategoryId"];
                    string latestNewsQuery = string.Empty;
                    //if (!string.IsNullOrEmpty(categoryId))
                    //{
                    //    latestNewsQuery = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='LookupMulti'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.CategoryName, categoryId);
                    //}
                    //else
                    //{
                        latestNewsQuery = string.Format("<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>");
                    //}

                    var latestNewsTable = Utilities.GetNewsRecords(latestNewsQuery, 5, ListsName.English.NewsRecord);
                    if (latestNewsTable != null && latestNewsTable.Rows.Count > 0)
                    {
                        rptLatestNews.DataSource = latestNewsTable;
                        rptLatestNews.DataBind();
                    }
                    //Bind data to top view
                    string topNewsQuery = string.Empty;
                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        topNewsQuery = string.Format(" <Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='LookupMulti'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='ViewsCount' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.CategoryName, categoryId);
                    }
                    else
                    {
                        topNewsQuery = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.ViewsCount);
                    }

                    var topViewsTable = Utilities.GetNewsRecords(topNewsQuery, 5, ListsName.English.NewsRecord);
                    if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                    {
                        rptTopViews.DataSource = topViewsTable;
                        rptTopViews.DataBind();
                    }

                    string mainItemQuery = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Boolean'>1</Value></Eq></Where>", FieldsName.NewsRecord.English.ShowInHomePage);
                    var mainItem = Utilities.GetNewsRecords(mainItemQuery, 1, ListsName.English.NewsRecord);
                    if (mainItem != null && mainItem.Rows.Count > 0)
                    {
                        Linktoitem = string.Format("{0}/{1}.aspx?NewsId={2}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Convert.ToString(mainItem.Rows[0][FieldsName.Id])); 
                        string imagePath = Convert.ToString(mainItem.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);
                        imgMainImage.ImageUrl = imagePath.Trim().Substring(0, imagePath.Length - 2);
                        lblShortContent.Text = Convert.ToString(mainItem.Rows[0][FieldsName.NewsRecord.English.ShortContent]);
                    }

                    if ("0".Equals(WebPartParent.WebpartName))
                    {
                        lblLatest.Visible = false;
                        rptTopViews.Visible = false;
                        lblReadMost.Text = "Tin mới nhận";
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
