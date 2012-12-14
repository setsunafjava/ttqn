using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using System.Web;
using System.Web.UI.HtmlControls;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class QNHeaderUS : UserControl
    {
        protected string CurrentStyle = string.Empty;
        protected string CategoryId = string.Empty;
        protected string LangUrl = "english";
        protected string LangTitle = "English";
        protected string LangImg = "en-lang.png";
        protected string HomeUrl = string.Empty;
        private DataTable dtMenu;
        private DataTable dtMenuType;
        public QNHeader ParentWP;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.Url.ToLower().Contains("/english"))
            {
                LangUrl = "";
                LangImg = "vn-lang.png";
                LangTitle = "VietNam";
                HomeUrl = "english";
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
                    //SPSecurity.RunWithElevatedPrivileges(() =>
                    //{
                    //    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                    //    {
                    //        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    //        {
                    //            try
                    //            {
                    //                SPList list = Utilities.GetListFromUrl(web, ListsName.English.MenuList);
                    //                var items = list.Items;
                    //                dtMenu = items.GetDataTable();
                    //                dtMenu.Columns.Add("MenuTypeID");
                    //                dtMenu.Columns.Add("MnParentID");
                    //                for (int i = 0; i < items.Count; i++)
                    //                {
                    //                    if (!string.IsNullOrEmpty(Convert.ToString(items[i]["MenuType"])))
                    //                    {
                    //                        SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i]["MenuType"]));
                    //                        dtMenu.Rows[i]["MenuTypeID"] = catLK.LookupId;
                    //                    }
                    //                    if (!string.IsNullOrEmpty(Convert.ToString(items[i]["ParentId"])))
                    //                    {
                    //                        SPFieldLookupValue mnLK = new SPFieldLookupValue(Convert.ToString(items[i]["ParentId"]));
                    //                        dtMenu.Rows[i]["MnParentID"] = mnLK.LookupId;
                    //                    }
                    //                    if (!string.IsNullOrEmpty(Convert.ToString(items[i]["tintuc"])))
                    //                    {
                    //                        SPFieldLookupValue ttLK = new SPFieldLookupValue(Convert.ToString(items[i]["tintuc"]));
                    //                        dtMenu.Rows[i]["tintuc"] = ttLK.LookupId;
                    //                    }
                    //                    if (!string.IsNullOrEmpty(Convert.ToString(items[i]["doanhnghiep"])))
                    //                    {
                    //                        SPFieldLookupValue dnLK = new SPFieldLookupValue(Convert.ToString(items[i]["doanhnghiep"]));
                    //                        dtMenu.Rows[i]["doanhnghiep"] = dnLK.LookupId;
                    //                    }
                    //                    if (!string.IsNullOrEmpty(Convert.ToString(items[i]["ttcdct"])))
                    //                    {
                    //                        SPFieldLookupValue ttLK = new SPFieldLookupValue(Convert.ToString(items[i]["ttcdct"]));
                    //                        dtMenu.Rows[i]["ttcdct"] = ttLK.LookupId;
                    //                    }
                    //                    if (!string.IsNullOrEmpty(Convert.ToString(items[i]["dulich"])))
                    //                    {
                    //                        SPFieldLookupValue dlLK = new SPFieldLookupValue(Convert.ToString(items[i]["dulich"]));
                    //                        dtMenu.Rows[i]["dulich"] = dlLK.LookupId;
                    //                    }
                    //                    if (!string.IsNullOrEmpty(Convert.ToString(items[i]["ttnb"])))
                    //                    {
                    //                        SPFieldLookupValue nbLK = new SPFieldLookupValue(Convert.ToString(items[i]["ttnb"]));
                    //                        dtMenu.Rows[i]["ttnb"] = nbLK.LookupId;
                    //                    }
                    //                }

                    //                dtMenuType = Utilities.GetListFromUrl(web, "MenuType").Items.GetDataTable();
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                Utilities.LogToUls(ex);
                    //            }
                    //        }

                    //    }
                    //});

                    SPList list = Utilities.GetListFromUrl(SPContext.Current.Web, ListsName.English.MenuList);
                    var items = list.Items;
                    dtMenu = items.GetDataTable();
                    dtMenu.Columns.Add("MenuTypeID");
                    dtMenu.Columns.Add("MnParentID");
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(items[i]["MenuType"])))
                        {
                            SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i]["MenuType"]));
                            dtMenu.Rows[i]["MenuTypeID"] = catLK.LookupId;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(items[i]["ParentId"])))
                        {
                            SPFieldLookupValue mnLK = new SPFieldLookupValue(Convert.ToString(items[i]["ParentId"]));
                            dtMenu.Rows[i]["MnParentID"] = mnLK.LookupId;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(items[i]["tintuc"])))
                        {
                            SPFieldLookupValue ttLK = new SPFieldLookupValue(Convert.ToString(items[i]["tintuc"]));
                            dtMenu.Rows[i]["tintuc"] = ttLK.LookupId;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(items[i]["doanhnghiep"])))
                        {
                            SPFieldLookupValue dnLK = new SPFieldLookupValue(Convert.ToString(items[i]["doanhnghiep"]));
                            dtMenu.Rows[i]["doanhnghiep"] = dnLK.LookupId;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(items[i]["ttcdct"])))
                        {
                            SPFieldLookupValue ttLK = new SPFieldLookupValue(Convert.ToString(items[i]["ttcdct"]));
                            dtMenu.Rows[i]["ttcdct"] = ttLK.LookupId;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(items[i]["dulich"])))
                        {
                            SPFieldLookupValue dlLK = new SPFieldLookupValue(Convert.ToString(items[i]["dulich"]));
                            dtMenu.Rows[i]["dulich"] = dlLK.LookupId;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(items[i]["ttnb"])))
                        {
                            SPFieldLookupValue nbLK = new SPFieldLookupValue(Convert.ToString(items[i]["ttnb"]));
                            dtMenu.Rows[i]["ttnb"] = nbLK.LookupId;
                        }
                    }

                    dtMenuType = Utilities.GetListFromUrl(SPContext.Current.Web, "MenuType").Items.GetDataTable();

                    //Bind data to latest news
                    if (dtMenu != null && dtMenu.Rows.Count > 0)
                    {
                        rptMenu.DataSource = dtMenu.Select("ParentId=''", "Position asc");
                        rptMenu.DataBind();
                    }
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
                DataRow drv = (DataRow)e.Item.DataItem;
                Repeater rptSubMenu = (Repeater) e.Item.FindControl("rptSubMenu");
                rptSubMenu.ItemDataBound += new RepeaterItemEventHandler(rptSubMenu_ItemDataBound);
                Literal ltrStyle = (Literal) e.Item.FindControl("ltrStyle");
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");

                Utilities.SetMenuLink(drv, ltrStyle, aLink, dtMenuType, HttpContext.Current);

                //Bind data to latest news
                rptSubMenu.DataSource = dtMenu.Select("MnParentID='" + Convert.ToString(drv["ID"]) + "'", "Position asc");
                rptSubMenu.DataBind();
            }
        }

        void rptSubMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow drv = (DataRow)e.Item.DataItem;
                Literal ltrStyle = (Literal)e.Item.Parent.Parent.FindControl("ltrStyle");
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");

                Utilities.SetMenuLink(drv, ltrStyle, aLink, dtMenuType, HttpContext.Current);
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
