using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls.WebParts;
using CQ.SharePoint.QN.Common;
using CQ.SharePoint.QN.Webparts;
using System.Web;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebPartPages;

namespace CQ.SharePoint.QN.Webparts
{
    [Guid("00dc0549-75c7-4699-97cc-666639b9336b")]
    public class QNHeader : System.Web.UI.WebControls.WebParts.WebPart
    {
        public QNHeader()
        {
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Trang chủ'")]
        [Description("Tiêu đề 'Trang chủ'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Trang chủ")]
        public string HomePageTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Hôm nay, ngày'")]
        [Description("Tiêu đề 'Hôm nay, ngày'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Hôm nay, ngày ")]
        public string TodayIsTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Đặt làm trang chủ'")]
        [Description("Tiêu đề 'Đặt làm trang chủ'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Đặt làm trang chủ")]
        public string SetAsHomePageTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Tìm kiếm'")]
        [Description("Tiêu đề 'Tìm kiếm'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Tìm kiếm")]
        public string SearchTitle
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            
            try
            {
                QNHeaderUS control = (QNHeaderUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/QNHeaderUS.ascx");
                control.ParentWP = this;
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);

            var cLoginName = string.Empty;
            var cDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now);
            var cURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString();

            if (SPContext.Current.Web.CurrentUser == null || string.IsNullOrEmpty(SPContext.Current.Web.CurrentUser.LoginName))
            {
                cLoginName = "Annonymous";
            }
            else
            {
                cLoginName = SPContext.Current.Web.CurrentUser.LoginName;
            }
            var cIP = HttpContext.Current.Request.UserHostAddress;
            var cBrowser = HttpContext.Current.Request.Browser.Browser;
            var camlQuery = "<Where><And><Eq>" +
                            "<FieldRef Name='Url' />" +
                            "<Value Type='Text'>" + cURL + "</Value></Eq>" +
                            "<And><Eq><FieldRef Name='Title' />" +
                            "<Value Type='Text'>" + cLoginName + "</Value></Eq>" +
                            "<And><Eq><FieldRef Name='DateHit' /><Value Type='DateTime' IncludeTime='FALSE'>" + cDate +
                            "</Value></Eq>" +
                            "<And><Eq><FieldRef Name='Browser' /><Value Type='Text'>" + cBrowser +
                            "</Value></Eq><Eq><FieldRef Name='IP' /><Value Type='Text'>" + cIP +
                            "</Value></Eq>" +
                            "</And></And></And></And></Where>";

            if (!this.Page.IsPostBack)
            {
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
                                    Query = camlQuery,
                                    RowLimit = 1
                                };
                                SPList list = Utilities.GetListFromUrl(web, ListsName.English.StatisticsList);
                                if (list != null)
                                {
                                    SPListItemCollection items = list.GetItems(spQuery);
                                    if (items == null || items.Count <= 0)
                                    {
                                        var item = list.Items.Add();
                                        item["Title"] = cLoginName;
                                        item["Url"] = cURL;
                                        item["IP"] = cIP;
                                        item["DateHit"] = DateTime.Now;
                                        item["Browser"] = cBrowser;
                                        web.AllowUnsafeUpdates = true;
                                        item.Update();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                });
            }
        }
    }
}
