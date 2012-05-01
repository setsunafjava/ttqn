using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class FooterUS : UserControl
    {
        public Footer ParentWP;
        protected string WebsiteInfo = string.Empty;
        protected int HitCount = 1;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    var userName = SPContext.Current.Web.CurrentUser.Name;
            //    aUser.Title = "Đăng xuất";
            //    aUser.HRef = "/_layouts/SignOut.aspx";
            //    aUser.InnerText= "Đăng xuất";
            //}
            //catch (Exception)
            //{
            //    aUser.Title = "Đăng nhập";
            //    aUser.HRef = "/_layouts/Authenticate.aspx";
            //    aUser.InnerText = "Đăng nhập";
            //}
            if (!IsPostBack)
            {
                try
                {
                    //Bind data to latest news
                    string latestNewsQuery = string.Format("<Where><IsNull><FieldRef Name='ParentId' /></IsNull></Where><OrderBy><FieldRef Name='Position' Ascending='TRUE' /></OrderBy>");
                    rptMenu.DataSource = GetNewsRecords(latestNewsQuery);
                    rptMenu.DataBind();
                    WebsiteInfo = Utilities.GetConfigValue(ParentWP.ConfigKey);
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Get news record form NewsRecord table
        /// </summary>
        /// <param name="query">SPquery for query items</param>
        /// <returns>News record Datatable</returns>
        public DataTable GetNewsRecords(string query)
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
                            HitCount = Utilities.GetListFromUrl(web, ListsName.English.StatisticsList).ItemCount;
                        }
                        catch (Exception)
                        {

                        }
                        try
                        {
                            SPQuery spQuery = new SPQuery
                            {
                                Query = query
                            };
                            SPList list = Utilities.GetListFromUrl(web, ListsName.English.MenuList);
                            if (list != null)
                            {
                                SPListItemCollection items = list.GetItems(spQuery);
                                table = items.GetDataTable();
                            }
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
