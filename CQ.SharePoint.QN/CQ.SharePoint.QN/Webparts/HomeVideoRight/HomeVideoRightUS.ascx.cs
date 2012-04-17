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
                var videoStr = "jwplayer(\"video-right-player-div\").setup({'flashplayer': '" +
                               SPContext.Current.Web.Url + "/" + ListsName.English.CQQNResources + "/jwplayer.swf',";
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
                                    Query = "<OrderBy><FieldRef Name='Title' Ascending='TRUE' /></OrderBy>"
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
                                                    'file': '" + item.Url +
                                                @"', 
                                                    'image': '" + web.Url + "/" + ListsName.English.CQQNResources + "/video.jpg" +
                                                @"',
                                                    'title': '" + item.Title + @"'
                                                },";
                                        }
                                        videoStr += @"],
                                                        repeat: 'list'
                                                    });";
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
