using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class ShowDownloadUS : UserControl
    {
        public ShowDownload ParentWP;
        protected string AdvStyle;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string query = "<OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
                rptDownload.DataSource = Utilities.GetDocLibRecords(query, ListsName.English.DownloadList);
                rptDownload.DataBind();
            }
        }

        protected void rptDownload_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
        {

            // This event is raised for the header, the footer, separators, and items.

            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var webUrl = "";
                if (!SPContext.Current.Web.ServerRelativeUrl.Equals("/"))
                {
                    webUrl = SPContext.Current.Web.ServerRelativeUrl;
                }
                DataRowView drv = (DataRowView)e.Item.DataItem;
                var aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = webUrl + "/" + ListsName.English.DownloadList + "/" +
                             Convert.ToString(drv["FileLeafRef"], CultureInfo.InvariantCulture);
                             Convert.ToString(drv["ID"], CultureInfo.InvariantCulture);
                aLink.Title = Convert.ToString(drv["Title"], CultureInfo.InvariantCulture);
            }
        }
    }
}
