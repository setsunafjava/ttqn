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
    [Guid("30024d95-df66-4bed-8192-0878c2fe3cbd")]
    public class QNadv : System.Web.UI.WebControls.WebParts.WebPart
    {
        public QNadv()
        {
        }

        [WebBrowsable(true)]
        [FriendlyName("Đường dẫn")]
        [Description("Đường dẫn file ảnh")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string ImageUrl
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Độ rộng")]
        [Description("Độ rộng quảng cáo")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string ImageWidth
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Độ cao")]
        [Description("Độ cao quảng cáo")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string ImageHeight
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Link")]
        [Description("Link quảng cáo")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string ImageLink
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề")]
        [Description("Tiêu đề quảng cáo")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string ImageTitle
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            try
            {
                QNadvUS control = (QNadvUS)this.Page.LoadControl("WebPartsUS/QNadvUS.ascx");
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
