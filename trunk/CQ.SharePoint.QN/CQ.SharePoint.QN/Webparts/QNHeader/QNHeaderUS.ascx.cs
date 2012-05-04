using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using System.Web;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class QNHeaderUS : UserControl
    {
        protected string CurrentStyle = string.Empty;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUrl = HttpContext.Current.Request.Url.AbsolutePath;
            if (!currentUrl.Contains(".aspx") || currentUrl.Contains("default.aspx"))
            {
                CurrentStyle = " class='current'";
            }
            if (!IsPostBack)
            {
                try
                {
                    //Bind data to latest news
                    string latestNewsQuery = string.Format("<Where><IsNull><FieldRef Name='ParentId' /></IsNull></Where><OrderBy><FieldRef Name='Position' Ascending='TRUE' /></OrderBy>");
                    rptMenu.DataSource = GetNewsRecords(latestNewsQuery);
                    rptMenu.DataBind();
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

        /// <summary>
        /// rptMenu_OnItemDataBound
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        protected void rptMenu_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
        {

            // This event is raised for the header, the footer, separators, and items.

            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView) e.Item.DataItem;
                Repeater rptSubMenu = (Repeater) e.Item.FindControl("rptSubMenu");
                Literal ltrStyle = (Literal) e.Item.FindControl("ltrStyle");

                var currentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(Convert.ToString(drv["Url"])) && currentUrl.Contains(Convert.ToString(drv["Url"])))
                {
                    ltrStyle.Text = " class='current'";
                }

                //Bind data to latest news
                string latestNewsQuery = string.Format("<Where><Eq><FieldRef Name='ParentId' LookupId='TRUE' />" +
                   "<Value Type='Lookup'>" + Convert.ToString(drv["ID"]) + "</Value></Eq></Where><OrderBy><FieldRef Name='Position' Ascending='TRUE' /></OrderBy>");
                rptSubMenu.DataSource = GetNewsRecords(latestNewsQuery);
                rptSubMenu.DataBind();
            }
        }   

        protected void lbRSS_OnClick(object sender, EventArgs e)
        {
            var categoryId = Convert.ToString(Request.QueryString["CategoryId"]);
            Utilities.GetRSS(categoryId);
        }
    }
}
