using System;
using System.Web;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class ContactUS : UserControl
    {
        public Contact ParentWP;
        protected string AdvStyle;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            SPList list = Utilities.GetListFromUrl(web, ListsName.English.ContactList);
                            if (list != null)
                            {
                                var item = list.Items.Add();
                                item["IP"] = HttpContext.Current.Request.UserHostAddress;
                                item["Browser"] = HttpContext.Current.Request.Browser.Browser;
                                item["Title"] = txtTitle.Text;
                                item["Email"] = txtEmail.Text;
                                item["Mobile"] = txtMobile.Text;
                                item["FullName"] = txtName.Text;
                                item["Address"] = txtAdd.Text;
                                item["Content"] = txtContent.Text;
                                web.AllowUnsafeUpdates = true;
                                item.Update();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "video-right-player-script",
                                                        "$(document).ready(function() {alert('Bạn đã gửi liên hệ thành công');location.href = '/'});", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "video-right-player-script",
                                                        "$(document).ready(function() {alert('Có lỗi xảy ra');location.href = '/'});", true);
                        }
                    }
                }
            });
        }
    }
}
