using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Serialization;

using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;

namespace CQ.SharePoint.QN.Webparts
{
    [Guid("b9ccb1db-d923-460d-a0f8-738cba6355b2")]
    public class MailBox : System.Web.UI.WebControls.WebParts.WebPart
    {
        public MailBox()
        {
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Hộp thư công vụ'")]
        [Description("Tiêu đề 'Hộp thư công vụ'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Hộp thư công vụ")]
        public string MailBoxTitle { get; set; }

        [WebBrowsable(true)]
        [FriendlyName("Nhập đường link tới web mail")]
        [Description("Nhập đường link tới web mail")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string WebMailPath { get; set; }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try
            {
                MailBoxUS control = (MailBoxUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/MailBoxUS.ascx");
                control.WebpartParent = this;
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
