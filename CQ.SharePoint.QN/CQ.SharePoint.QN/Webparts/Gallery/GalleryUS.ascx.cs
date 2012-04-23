using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class GalleryUS : UserControl
    {
        public Gallery ParentWP;
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
                string query = "<OrderBy><FieldRef Name='ImageCreateDate' Ascending='FALSE' /></OrderBy>";
                rptCat.DataSource = Utilities.GetDocLibRecords(query, ListsName.English.ImageCatList);
                rptCat.DataBind();
                rptAlbum.DataSource = Utilities.GetDocLibRecords(query, ListsName.English.ImageAlbumList);
                rptAlbum.DataBind();
            }
        }

        protected void rptCat_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
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
                var aLink = (HtmlAnchor) e.Item.FindControl("aLink");
                aLink.HRef = webUrl + "/" + Constants.PageInWeb.ShowGalleryPage + ".aspx?FieldName=CatID&FieldValue=" +
                             Convert.ToString(drv["ID"], CultureInfo.InvariantCulture);
                aLink.Title = Convert.ToString(drv["Title"], CultureInfo.InvariantCulture);
                var imgLink = (HtmlImage)e.Item.FindControl("imgLink");
                imgLink.Src = webUrl + "/" + ListsName.English.ImageCatList + "/" +
                              Convert.ToString(drv["FileLeafRef"], CultureInfo.InvariantCulture);
                imgLink.Alt = Convert.ToString(drv["Title"], CultureInfo.InvariantCulture);
            }
        }

        protected void rptAlbum_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
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
                aLink.HRef = webUrl + "/" + Constants.PageInWeb.ShowGalleryPage + ".aspx?FieldName=AlbumID&FieldValue=" +
                             Convert.ToString(drv["ID"], CultureInfo.InvariantCulture);
                aLink.Title = Convert.ToString(drv["Title"], CultureInfo.InvariantCulture);
                var imgLink = (HtmlImage)e.Item.FindControl("imgLink");
                imgLink.Src = webUrl + "/" + ListsName.English.ImageAlbumList + "/" +
                              Convert.ToString(drv["FileLeafRef"], CultureInfo.InvariantCulture);
                imgLink.Alt = Convert.ToString(drv["Title"], CultureInfo.InvariantCulture);
            }
        }   
    }
}
