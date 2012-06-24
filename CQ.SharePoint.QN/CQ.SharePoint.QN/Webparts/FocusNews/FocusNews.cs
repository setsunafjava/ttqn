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
    [Guid("43766268-a956-4dd4-95e1-bdbb7bba88b7")]
    public class FocusNews : System.Web.UI.WebControls.WebParts.WebPart
    {
        private string _readmore = "";
        [WebBrowsable(true)]
        [FriendlyName("Nhập số tin muốn hiển thị")]
        [Description("Số tin muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NumberOfNews { get; set; }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Tin tiêu điểm'")]
        [Description("Tiêu đề 'Tin tiêu điểm'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Tin tiêu điểm")]
        public string FocusNewsTitle { get; set; }


        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Xem thêm'")]
        [Description("Tiêu đề 'Xem thêm'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Xem thêm")]
        public string ReadMore { get; set; }

        public FocusNews()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                FocusNewsUS control = (FocusNewsUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/FocusNewsUS.ascx");
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
