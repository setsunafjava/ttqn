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
    public partial class ShowGalleryUS : UserControl
    {
        public ShowGallery ParentWP;
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
                try
                {
                    //Bind data to latest news
                    string latestNewsQuery = string.Format("<OrderBy><FieldRef Name='Title' Ascending='TRUE' /></OrderBy>");
                    var fieldName = Convert.ToString(Request.QueryString["FieldName"]);
                    var fieldValue = Convert.ToString(Request.QueryString["FieldValue"]);
                    if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(fieldValue))
                    {
                        latestNewsQuery =
                            string.Format(
                                "<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='Lookup'>{1}</Value></Eq></Where>", fieldName, fieldValue) + latestNewsQuery;
                    }
                    rptImages.DataSource = GetImages(latestNewsQuery);
                    rptImages.DataBind();
                }
                catch (Exception ex)
                {
                }
            }

            try
            {
                //Bind data to latest news
                string latestNewsQuery = string.Format("<OrderBy><FieldRef Name='Title' Ascending='TRUE' /></OrderBy>");
                rptCat.DataSource = GetNewsRecords(latestNewsQuery);
                rptCat.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

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
                            SPList list = Utilities.GetDocListFromUrl(web, ListsName.English.ImageCatList);
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

        public DataTable GetImages(string query)
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
                            SPList list = Utilities.GetDocListFromUrl(web, ListsName.English.ImagesList);
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

        protected void rptImages_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
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
                aLink.HRef = webUrl + "/" + ListsName.English.ImagesList + "/" +
                              Convert.ToString(drv["FileLeafRef"], CultureInfo.InvariantCulture);
                aLink.Title = Convert.ToString(drv["Title"], CultureInfo.InvariantCulture);

                var filename = Convert.ToString(drv["FileLeafRef"], CultureInfo.InvariantCulture);
                filename = filename.Replace('.', '_');
                filename += ".jpg";

                var imgLink = (HtmlImage)e.Item.FindControl("imgLink");
                imgLink.Src = webUrl + "/" + ListsName.English.ImagesList + "/_t/" + filename;
                imgLink.Alt = Convert.ToString(drv["Description"], CultureInfo.InvariantCulture);
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
                var aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = webUrl + "/" + Constants.PageInWeb.ShowGalleryPage + ".aspx?FieldName=CatID&FieldValue=" +
                             Convert.ToString(drv["ID"], CultureInfo.InvariantCulture);
                aLink.Title = Convert.ToString(drv["Title"], CultureInfo.InvariantCulture);
                var imgLink = (HtmlImage)e.Item.FindControl("imgLink");
                imgLink.Src = webUrl + "/" + ListsName.English.ImageCatList + "/" +
                              Convert.ToString(drv["FileLeafRef"], CultureInfo.InvariantCulture);
                imgLink.Alt = Convert.ToString(drv["Title"], CultureInfo.InvariantCulture);
            }
        }   
    }
}
