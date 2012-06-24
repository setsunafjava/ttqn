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
    [Guid("25a8316b-2e6f-4461-b3bb-71d6e72a8fd9")]
    public class Footer : System.Web.UI.WebControls.WebParts.WebPart
    {
        public Footer()
        {
        }

        [WebBrowsable(true)]
        [FriendlyName("Từ khóa cấu hình")]
        [Description("Nhập từ khóa để lấy thông tin mô tả website")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string ConfigKey{get;set;}

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Số lượt truy cập:'")]
        [Description("Thiết lập ngôn ngữ = 'Số lượt truy cập:'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Số lượt truy cập")]
        public string StatisticTitle { get; set; }


        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Thiết kế bởi VIETEC'")]
        [Description("Thiết lập ngôn ngữ = 'Thiết kế bởi VIETEC'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Thiết kế bởi VIETEC")]
        public string DesignByTitle { get; set; }


        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                FooterUS control = (FooterUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/FooterUS.ascx");
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
