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
    [Guid("65f398a5-829a-4aaf-882e-9d2d88fd2623")]
    public class SlideShowHome : System.Web.UI.WebControls.WebParts.WebPart
    {
        public SlideShowHome()
        {
        }
        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Thư viện ảnh Quảng Ninh'")]
        [Description("Thiết lập ngôn ngữ = 'Thư viện ảnh Quảng Ninh'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Thư viện ảnh Quảng Ninh")]
        public string SlideShowHomeTitle { get; set; }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                SlideShowHomeUS control = (SlideShowHomeUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/SlideShowHomeUS.ascx");
                control.ParentWP = this;
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
