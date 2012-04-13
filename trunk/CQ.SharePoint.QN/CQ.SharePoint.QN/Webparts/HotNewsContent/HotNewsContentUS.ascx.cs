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
            try
            {
                //Bind data to latest news
                string latestNewsQuery = string.Format("<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>");
                rptLatestNews.DataSource = GetNewsRecords(latestNewsQuery, 5);
                rptLatestNews.DataBind();

                //Bind data to top view
                string topNewsQuery = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.ViewsCount);
                rptTopViews.DataSource = GetNewsRecords(topNewsQuery, 5);
                rptTopViews.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Get news record form NewsRecord table
        /// </summary>
        /// <param name="query">SPquery for query items</param>
        /// <param name="newsNumber">number of news want to get</param>
        /// <returns>News record Datatable</returns>
        public DataTable GetNewsRecords(string query, uint newsNumber)
        {
            DataTable table = new DataTable();
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            SPQuery spQuery = new SPQuery
                                                {
                                                    Query = query,
                                                    RowLimit = newsNumber
                                                };
                            SPList list = web.Lists[ListsName.English.NewsRecord];
                            SPListItemCollection items = list.GetItems(spQuery);
                            table = items.GetDataTable();
                        }
                        catch (Exception ex)
                        {
                            table = null;
                        }
                    }

                }
            });
            return table;
        }
    }
}
