using System;
using System.Data;
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
    public partial class HomeVideoRightUS : UserControl
    {
        protected string VideoUrl;
        protected string ImageUrl;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
//            if (!IsPostBack)
//            {
//                var videoStr =
//                    "$(document).ready(function() {jwplayer(\"video-right-player-div\").setup({'flashplayer': '" +
//                    SPContext.Current.Web.Url + "/" + ListsName.English.CQQNResources +
//                    "/player.swf','width': 285,'playlist.position': 'bottom','playlist.size': '100','skin': '" +
//                    SPContext.Current.Web.Url + "/" + ListsName.English.CQQNResources + "/stylish_slim.swf',";
//                               //"_layouts/player.swf',width: 285,";
//                SPSecurity.RunWithElevatedPrivileges(() =>
//                {
//                    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
//                    {
//                        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
//                        {
//                            try
//                            {
//                                var webUrl = "";
//                                if (!web.ServerRelativeUrl.Equals("/"))
//                                {
//                                    webUrl = web.ServerRelativeUrl;
//                                }
//                                SPQuery spQuery = new SPQuery
//                                {
//                                    Query = "<OrderBy><FieldRef Name='Title' Ascending='TRUE' /></OrderBy>",
//                                    RowLimit = 5
//                                };
//                                SPList list = Utilities.GetDocListFromUrl(web, ListsName.English.VideosList);
//                                if (list != null)
//                                {
//                                    SPListItemCollection items = list.GetItems(spQuery);
//                                    if (items != null && items.Count > 0)
//                                    {
//                                        videoStr += "'playlist': [";
//                                        foreach (SPListItem item in items)
//                                        {
//                                            videoStr +=
//                                                @"{
//                                                    'file': '" + SPContext.Current.Web.Url + "/" + item.Url +
//                                                @"', 
//                                                    'image': '" + web.Url + "/" + ListsName.English.CQQNResources + "/images.jpg" +
//                                                @"',
//                                                    'title': '" + item.Title + @"'
//                                                },";
//                                        }
//                                        if (videoStr.EndsWith(","))
//                                        {
//                                            videoStr = videoStr.Substring(0, videoStr.Length - 1);
//                                        }
//                                        videoStr += @"],
//                                                        repeat: 'list'
//                                                    });});";
//                                    }
//                                }
//                            }
//                            catch (Exception ex)
//                            {
                                
//                            }
//                        }
//                    }
//                });

//                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "video-right-player-script",
//                                                        videoStr, true);
//            }

            if (!IsPostBack)
            {
                //SPSecurity.RunWithElevatedPrivileges(() =>
                //{
                //    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                //    {
                //        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                //        {
                //            try
                //            {
                //                SPQuery spQuery = new SPQuery
                //                {
                //                    Query = "<OrderBy><FieldRef Name='Position' Ascending='TRUE' /></OrderBy>"
                //                };
                //                SPList list = Utilities.GetDocListFromUrl(web, ListsName.English.VideosList);
                //                if (list != null)
                //                {
                //                    SPListItemCollection items = list.GetItems(spQuery);
                //                    if (items != null && items.Count > 0)
                //                    {
                //                        VideoUrl = SPContext.Current.Web.Url + "/" + items[0].Url;
                //                        ImageUrl = SPContext.Current.Web.Url + "/" + items[0]["ImageUrl"];
                //                        var tblTV = items.GetDataTable();
                //                        rptTVLink.DataSource = tblTV;
                //                        rptTVLink.DataBind();
                //                    }
                //                }
                //            }
                //            catch (Exception ex)
                //            {

                //            }
                //        }
                //    }
                //});
                try
                {
                    SPQuery spQuery = new SPQuery
                    {
                        Query = "<OrderBy><FieldRef Name='Position' Ascending='TRUE' /></OrderBy>"
                    };
                    SPList list = Utilities.GetDocListFromUrl(SPContext.Current.Web, ListsName.English.VideosList);
                    if (list != null)
                    {
                        SPListItemCollection items = list.GetItems(spQuery);
                        if (items != null && items.Count > 0)
                        {
                            VideoUrl = SPContext.Current.Web.Url + "/" + items[0].Url;
                            ImageUrl = SPContext.Current.Web.Url + "/" + items[0]["ImageUrl"];
                            var tblTV = items.GetDataTable();
                            rptTVLink.DataSource = tblTV;
                            rptTVLink.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void rptTVLink_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
        {

            // This event is raised for the header, the footer, separators, and items.

            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                var aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = "javascript:void(0)";
                aLink.Title = Convert.ToString(drv["Title"]);
                var tvCode = "<embed flashvars=\"file=" + SPContext.Current.Web.Url + "/" + ListsName.English.VideosList + "/" + Convert.ToString(drv["FileLeafRef"]) + "&image=" + SPContext.Current.Web.Url + "/" + Convert.ToString(drv["ImageUrl"]) + "&autostart=false\" allowfullscreen=\"true\" allowscripaccess=\"always\" id=\"qn-video-div-player\" name=\"qn-video-div-player\" src=\"/QNResources/player.swf\" width=\"285\" />";
                tvCode = tvCode.Replace("\r\n", "");
                tvCode = tvCode.Replace("\n", "");
                tvCode = tvCode.Replace("\r", "");
                tvCode = tvCode.Replace("'", "\\'");
                aLink.Attributes.Add("onclick", string.Format("javascript:setVideoPlay('{0}','{1}');return false;", Convert.ToString(drv["ID"]), tvCode));
            }
        }
    }
}
