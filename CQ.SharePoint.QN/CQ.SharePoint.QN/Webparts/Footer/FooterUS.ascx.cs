using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;

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
        private DataTable dtMenu;
        private DataTable dtMenuType;
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
            rptMenu.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(rptMenu_ItemDataBound);
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

                    try
                    {
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
                    }
                    catch (Exception ex)
                    {
                        Utilities.LogToUls(ex);
                    }

                    //Bind data to latest news
                    if (dtMenu != null && dtMenu.Rows.Count > 0)
                    {
                        rptMenu.DataSource = dtMenu.Select("ParentId=''", "Position asc");
                        rptMenu.DataBind();
                    }
                    WebsiteInfo = Utilities.GetConfigValue(ParentWP.ConfigKey);
                    HitCount = Utilities.GetListFromUrl(SPContext.Current.Web, ListsName.English.StatisticsList).ItemCount;
                }
                catch (Exception ex)
                {
                }
            }
        }

        void rptMenu_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow drv = (DataRow)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");

                Utilities.SetMenuLink(drv, aLink, dtMenuType, HttpContext.Current);
            }
        }
    }
}
