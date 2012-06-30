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
        protected string CategoryId = string.Empty;
        protected string LangUrl = "en";
        protected string LangTitle = "English";
        protected string LangImg = "en-lang.png";
        protected string HomeUrl = string.Empty;
        public QNHeader ParentWP;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.Url.Contains("/en"))
            {
                LangUrl = "";
                LangImg = "vn-lang.png";
                LangTitle = "VietNam";
                HomeUrl = "en";
            }
            CategoryId = Convert.ToString(Request.QueryString["CategoryId"]);
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

            var tkStr =
                "$(document).ready(function() {ganValue('" + Request.QueryString["KeyWord"] + "')});";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "tkStr-script", tkStr, true);
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
                rptSubMenu.ItemDataBound += new RepeaterItemEventHandler(rptSubMenu_ItemDataBound);
                Literal ltrStyle = (Literal) e.Item.FindControl("ltrStyle");
                var itemUrl = Convert.ToString(drv["Url"]);
                var currentUrl = HttpContext.Current.Request.Url.AbsoluteUri + "&";
                if (!string.IsNullOrEmpty(itemUrl) && currentUrl.Contains(itemUrl + "&"))
                {
                    ltrStyle.Text = " class='current'";
                }
                else
                {
                    var newsId = Request.QueryString[Constants.NewsId];
                    if (!string.IsNullOrEmpty(newsId))
                    {
                        var catValue = Utilities.GetCatsByNewsID(newsId);
                        foreach (SPFieldLookupValue value in catValue)
                        {
                            var catUrl = "/" + Constants.PageInWeb.SubPage + ".aspx?CategoryId=" + value.LookupId + "&";
                            if (!string.IsNullOrEmpty(itemUrl) && (itemUrl + "&").Contains(catUrl))
                            {
                                ltrStyle.Text = " class='current'";
                                break;
                            }
                        }
                    }
                }

                //Bind data to latest news
                string latestNewsQuery = string.Format("<Where><Eq><FieldRef Name='ParentId' LookupId='TRUE' />" +
                   "<Value Type='Lookup'>" + Convert.ToString(drv["ID"]) + "</Value></Eq></Where><OrderBy><FieldRef Name='Position' Ascending='TRUE' /></OrderBy>");
                rptSubMenu.DataSource = GetNewsRecords(latestNewsQuery);
                rptSubMenu.DataBind();
            }
        }

        void rptSubMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // This event is raised for the header, the footer, separators, and items.

            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                Literal ltrStyle = (Literal)e.Item.Parent.Parent.FindControl("ltrStyle");
                var itemUrl = Convert.ToString(drv["Url"]);
                var currentUrl = HttpContext.Current.Request.Url.AbsoluteUri + "&";
                if (!string.IsNullOrEmpty(itemUrl) && currentUrl.Contains(itemUrl + "&"))
                {
                    ltrStyle.Text = " class='current'";
                }
                else
                {
                    var newsId = Request.QueryString[Constants.NewsId];
                    if (!string.IsNullOrEmpty(newsId))
                    {
                        var catValue = Utilities.GetCatsByNewsID(newsId);
                        foreach (SPFieldLookupValue value in catValue)
                        {
                            var catUrl = "/" + Constants.PageInWeb.SubPage + ".aspx?CategoryId=" + value.LookupId + "&";
                            if (!string.IsNullOrEmpty(itemUrl) && (itemUrl + "&").Contains(catUrl))
                            {
                                ltrStyle.Text = " class='current'";
                                break;
                            }
                        }
                    }
                }
            }
        }

        protected void lbRSS_OnClick(object sender, EventArgs e)
        {
            //var categoryId = Convert.ToString(Request.QueryString["CategoryId"]);
            //Utilities.GetRSS(categoryId);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("PlaceHolderPageTitle");
            contentPlaceHolder.Controls.Clear();
            LiteralControl control = new LiteralControl();
            control.Text = Constants.GetHomeTitle();
            var currentUrl = HttpContext.Current.Request.Url.AbsolutePath;
            if (!currentUrl.Contains(".aspx") || currentUrl.Contains("default.aspx"))
            {
                CurrentStyle = " class='current'";
                control.Text = Constants.GetHomeTitle();
            }
            else if (currentUrl.Contains(Constants.PageInWeb.SubPage + ".aspx"))
            {
                var catID = Convert.ToString(Request.QueryString["CategoryId"]);
                var catItem = Utilities.GetListItemFromUrlByID(ListsName.English.NewsCategory ,catID);
                if (catItem != null)
                {
                    control.Text += " - " + Convert.ToString(catItem["Title"]);
                }
            }
            else if (currentUrl.Contains(Constants.PageInWeb.DetailNews + ".aspx"))
            {
                var newsId = Request.QueryString[Constants.NewsId];
                var newsItem = Utilities.GetListItemFromUrlByID(ListsName.English.NewsRecord, newsId);
                if (newsItem != null)
                {
                    var catItem = Utilities.GetListItemFromUrlByID(ListsName.English.NewsCategory, Utilities.GetCategoryIdByItemId(Convert.ToInt32(newsId),
                                                                    ListsName.English.NewsRecord));
                    if (catItem != null)
                    {
                        control.Text += " - " + Convert.ToString(catItem["Title"]);
                    }
                    control.Text += " - " + Convert.ToString(newsItem["Title"]);
                }
            }
            else
            {
                control.Text = Constants.GetHomeTitle();
            }
            contentPlaceHolder.Controls.Add(control);
        }
    }
}
