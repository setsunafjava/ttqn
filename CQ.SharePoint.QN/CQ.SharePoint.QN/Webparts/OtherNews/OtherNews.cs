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
    [Guid("bf205d59-1f9b-47e0-83b3-95629a13eb6a")]
    public class OtherNews : System.Web.UI.WebControls.WebParts.WebPart
    {
        public OtherNews()
        {
        }
        [WebBrowsable(true)]
        [FriendlyName("Nhập số tin muốn hiển thị")]
        [Description("Số tin muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NumberOfNews { get; set; }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Các tin khác'")]
        [Description("Thiết lập ngôn ngữ = 'Các tin khác'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Các tin khác")]
        public string OtherNewsTitle { get; set; }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Ngày'")]
        [Description("Thiết lập ngôn ngữ = 'Ngày'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Ngày")]
        public string Day
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            try
            {
                OtherNewsUS control = (OtherNewsUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/OtherNewsUS.ascx");
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
