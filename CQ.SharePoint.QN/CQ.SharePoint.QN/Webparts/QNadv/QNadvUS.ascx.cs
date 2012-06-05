using System;
using System.Web;
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
    public partial class QNadvUS : UserControl
    {
        public QNadv ParentWP;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ParentWP.QCID)))
                {
                    var qcItem = Utilities.GetDocListItemFromUrl("QuangCao", ParentWP.QCID);
                    if (qcItem != null)
                    {
                        aLink.CommandArgument = ParentWP.QCID.ToString();
                        if ("Image".Equals(Convert.ToString(qcItem["Type"])))
                        {
                            ltrQC.Text = "<img src='" + SPContext.Current.Web.Url + "/" + qcItem.Url + "' width='" + Convert.ToString(qcItem["Width"]) + "' height='" + Convert.ToString(qcItem["Height"]) + "' alt='" + qcItem.Title + "' title='" + qcItem.Title + "' />";
                        }
                        else if ("Flash".Equals(Convert.ToString(qcItem["Type"])))
                        {
                            ltrQC.Text=@"<embed width='" + Convert.ToString(qcItem["Width"]) + "' height='" + Convert.ToString(qcItem["Height"]) + @"' align='middle' quality='high' wmode='transparent' allowscriptaccess='always' 
                                        type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' alt='' 
                                        src='" + SPContext.Current.Web.Url + "/" + qcItem.Url + "' />";
                        }
                        else if ("Video".Equals(Convert.ToString(qcItem["Type"])))
                        {
                            ltrQC.Text =
                                @"<embed
                                  flashvars='file=" + SPContext.Current.Web.Url + "/" + ListsName.English.CQQNResources + @"/stylish_slim.swf&autostart=true'
                                  allowfullscreen='true'
                                  allowscripaccess='always'
                                  id='" + this.ID + "-quangcao-" + ParentWP.QCID + @"'
                                  name='" + this.ID + "-quangcao-" + ParentWP.QCID + @"'
                                  src='" + SPContext.Current.Web.Url + "/" + qcItem.Url + @"'
                                  width='" + Convert.ToString(qcItem["Width"]) + @"'
                                  height='" + Convert.ToString(qcItem["Height"]) + @"'
                                />";
                        }
                        else
                        {
                            aLink.Visible = false;
                        }
                    }
                    else
                    {
                        aLink.Visible = false;
                    }
                }
                else
                {
                    aLink.Visible = false;
                }
            }
        }

        protected void aLink_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((LinkButton)sender).CommandArgument))
            {
                var qcItem = Utilities.GetDocListItemFromUrl("QuangCao", Convert.ToInt32(((LinkButton)sender).CommandArgument));
                if (qcItem != null)
                {
                    SPSecurity.RunWithElevatedPrivileges(() =>
                    {
                        using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                        {
                            using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                            {
                                try
                                {
                                    string listUrl = web.Url + "/Lists/QuangCaoReport";
                                    var result = web.GetList(listUrl);
                                    var item = result.Items.Add();
                                    item["Title"] = qcItem.Title;
                                    item["QuangCao"] = new SPFieldLookupValue(qcItem.ID, qcItem.Title);
                                    item["IP"] = HttpContext.Current.Request.UserHostAddress;
                                    item["Browser"] = HttpContext.Current.Request.Browser.Browser;
                                    item["Url"] = HttpContext.Current.Request.Url.AbsoluteUri;
                                    web.AllowUnsafeUpdates = true;
                                    item.Update();
                                    var qcList = Utilities.GetDocListFromUrl(web, "QuangCao");
                                    var qItem = qcList.GetItemById(qcItem.ID);
                                    qItem["CountClick"] = Convert.ToInt32(qItem["CountClick"]) + 1;
                                    web.AllowUnsafeUpdates = true;
                                    qItem.Update();
                                }
                                catch (Exception ex)
                                {
                                    Utilities.LogToUls(ex);
                                }
                            }

                        }
                    });
                    HttpContext.Current.Response.Redirect(Convert.ToString(qcItem["LinkUrl"]));
                }
            }
        }
    }
}
