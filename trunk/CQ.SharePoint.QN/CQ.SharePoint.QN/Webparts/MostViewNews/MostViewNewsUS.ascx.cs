using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class MostViewNewsUS : UserControl
    {
        public MostViewNews WebpartParent;
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
                    var newsId = Request.QueryString[Constants.NewsId];
                    var categoryId = Request.QueryString["CategoryId"];
                    //if (!string.IsNullOrEmpty(newsId))
                    //{
                    //Bind data to top view
                    NewsUrl = string.Format("{0}/{1}.aspx?{2}=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId);
                    string topNewsQuery = string.Empty;

                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        topNewsQuery = string.Format(" <Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='LookupMulti'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.CategoryName, categoryId);
                    }
                    else
                    {
                        topNewsQuery = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.ViewsCount);    
                    }
                    
                    uint newsNumber = 5;

                    if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                    {
                        newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }

                    var topViewsTable = Utilities.GetNewsRecords(topNewsQuery, newsNumber, ListsName.English.NewsRecord);
                    if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                    {
                        rptTopViews.DataSource = topViewsTable;
                        rptTopViews.DataBind();
                    }
                    else
                    {
                        lblItemsNotFound.Text = "Không tìm thấy thêm bài viết nào thuộc mục này!";
                    }
                    //}
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
