using System;
using System.Web.UI;
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
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var videoStr =
                    "$(document).ready(function() {jwplayer(\"video-right-player-div\").setup({'flashplayer': '" +
                    SPContext.Current.Web.Url + "/" + ListsName.English.CQQNResources +
                    "/player.swf','width': 285,'playlist.position': 'bottom','playlist.size': '100','skin': '" +
                    SPContext.Current.Web.Url + "/" + ListsName.English.CQQNResources + "/stylish_slim.swf',";
                               //"_layouts/player.swf',width: 285,";
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                    {
                        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            try
                            {
                                var webUrl = "";
                                if (!web.ServerRelativeUrl.Equals("/"))
                                {
                                    webUrl = web.ServerRelativeUrl;
                                }
                                SPQuery spQuery = new SPQuery
                                {
                                    Query = "<OrderBy><FieldRef Name='Title' Ascending='TRUE' /></OrderBy>",
                                    RowLimit = 5
                                };
                                SPList list = Utilities.GetDocListFromUrl(web, ListsName.English.VideosList);
                                if (list != null)
                                {
                                    SPListItemCollection items = list.GetItems(spQuery);
                                    if (items != null && items.Count > 0)
                                    {
                                        videoStr += "'playlist': [";
                                        foreach (SPListItem item in items)
                                        {
                                            videoStr +=
                                                @"{
                                                    'file': '" + SPContext.Current.Web.Url + "/" + item.Url +
                                                //@"', 
                                                    //'image': '" + web.Url + "/" + ListsName.English.CQQNResources + "/images.jpg" +
                                                @"',
                                                    'title': '" + item.Title + @"'
                                                },";
                                        }
                                        if (videoStr.EndsWith(","))
                                        {
                                            videoStr = videoStr.Substring(0, videoStr.Length - 1);
                                        }
                                        videoStr += @"],
                                                        repeat: 'list'
                                                    });});";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                
                            }
                        }
                    }
                });

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "video-right-player-script",
                                                        videoStr, true);
            }
        }
    }
}
