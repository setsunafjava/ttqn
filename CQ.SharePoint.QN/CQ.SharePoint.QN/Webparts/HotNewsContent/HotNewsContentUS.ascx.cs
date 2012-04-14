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
                    string latestNewsQuery =
                        string.Format("<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>");
                    var latestNewsTable = Utilities.GetNewsRecords(latestNewsQuery, 5, ListsName.English.NewsRecord);
                    if (latestNewsTable != null && latestNewsTable.Rows.Count > 0)
                    {
                        rptLatestNews.DataSource = latestNewsTable;
                        rptLatestNews.DataBind();
                    }
                    //Bind data to top view
                    string topNewsQuery = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>",
                                                        FieldsName.NewsRecord.English.ViewsCount);
                    var topViewsTable = Utilities.GetNewsRecords(topNewsQuery, 5, ListsName.English.NewsCategory);
                    if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                    {
                        rptTopViews.DataSource = topViewsTable;
                        rptTopViews.DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
