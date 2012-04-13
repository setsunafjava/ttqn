using System;
using System.Data;
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
    public partial class QNHeaderUS : UserControl
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
                    string latestNewsQuery = string.Format("<Where><IsNull><FieldRef Name='ParentId' /></IsNull></Where><OrderBy><FieldRef Name='Position' Ascending='FALSE' /></OrderBy>");
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

        protected void rptMenu_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
        {

            // This event is raised for the header, the footer, separators, and items.

            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView) e.Item.DataItem;
                Repeater rptSubMenu = (Repeater) e.Item.FindControl("rptSubMenu");

                //Bind data to latest news
                string latestNewsQuery = string.Format("<Where><Eq><FieldRef Name='ParentId' LookupId='TRUE' />" +
                   "<Value Type='Lookup'>" + Convert.ToString(drv["ID"]) + "</Value></Eq></Where><OrderBy><FieldRef Name='Position' Ascending='FALSE' /></OrderBy>");
                rptSubMenu.DataSource = GetNewsRecords(latestNewsQuery);
                rptSubMenu.DataBind();
            }
        }   
    }
}
