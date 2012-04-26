using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class OtherNewsUS : UserControl
    {
        public OtherNews WebpartParent;
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
                    var newsId = Convert.ToInt32(Request.QueryString["NewsID"]);
                    //if (!string.IsNullOrEmpty(newsId))
                    //{
                    NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews);
                    //Bind data to top view

                    var categoryId = Utilities.GetCategoryIdByItemId(newsId, ListsName.English.NewsRecord);

                    //string topNewsQuery = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.ViewsCount);
                    string topNewsQuery = string.Format("<Where><And><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='LookupMulti'>{1}</Value></Eq><Neq><FieldRef Name='{2}' /><Value Type='Counter'>{3}</Value></Neq></And></Where>",
                        FieldsName.NewsRecord.English.CategoryName, categoryId, FieldsName.Id, newsId);
                    uint newsNumber = 5;

                    if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                    {
                        newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }
                    var topViewsTable = Utilities.GetNewsRecords(topNewsQuery, newsNumber, ListsName.English.NewsRecord);
                    if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                    {
                        rptOtherNews.DataSource = topViewsTable;
                        rptOtherNews.DataBind();
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
