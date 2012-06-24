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
    [Guid("599964a2-1745-484b-8b04-37dcb32901f6")]
    public class MostViewNews : System.Web.UI.WebControls.WebParts.WebPart
    {
        public MostViewNews()
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
        [FriendlyName("Thiết lập ngôn ngữ = 'Bài đọc nhiều nhất'")]
        [Description("Thiết lập ngôn ngữ = 'Bài đọc nhiều nhất'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Đọc nhiều")]
        public string MostViews
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            try
            {
                MostViewNewsUS control = (MostViewNewsUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/MostViewNewsUS.ascx");
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
