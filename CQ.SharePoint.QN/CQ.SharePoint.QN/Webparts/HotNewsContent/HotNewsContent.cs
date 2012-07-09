using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;

namespace CQ.SharePoint.QN.Webparts
{
    [Guid("8b4fb7d8-b280-4d98-8921-9e137f6f7dae")]
    public class HotNewsContent : Microsoft.SharePoint.WebPartPages.WebPart
    {
        public HotNewsContent()
        {
        }

        [WebBrowsable(true)]
        [FriendlyName("Chọn loại mục tin - 1 trang chính, 0 trang còn lại")]
        [Description("Chọn loại mục tin - 1 trang chính, 0 trang còn lại")]
        [Category("Cấu hình Webpart")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string WebpartName
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Số tin mới nhất muốn hiển thị")]
        [Description("Số tin mới nhất muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string LatestNewsNumber
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Số tin đọc nhiều muốn hiển thị")]
        [Description("Số tin đọc nhiều muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string ReadMoreNumber
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Tin mới'")]
        [Description("Thiết lập ngôn ngữ = 'Tin mới'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Tin mới")]
        public string LatestNews
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Đọc nhiều'")]
        [Description("Thiết lập ngôn ngữ = 'Đọc nhiều'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Đọc nhiều")]
        public string MostViews
        {
            get;
            set;
        }

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

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Tin mới nhận'")]
        [Description("Thiết lập ngôn ngữ = 'Tin mới nhận'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Tin mới nhận")]
        public string LatestRecieved
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try
            {
                HotNewsContentUS control = (HotNewsContentUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/HotNewsContentUS.ascx");
                control.WebPartParent = this;
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
