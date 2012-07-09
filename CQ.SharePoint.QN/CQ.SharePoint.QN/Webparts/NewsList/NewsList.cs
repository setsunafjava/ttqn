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
    [Guid("9f438845-18d2-400a-908c-948ce9adf53a")]
    public class NewsList : System.Web.UI.WebControls.WebParts.WebPart
    {
        public NewsList()
        {
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
        [FriendlyName("Thiết lập ngôn ngữ = 'Trước'")]
        [Description("Thiết lập ngôn ngữ = 'Trước'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Trước")]
        public string Prev
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Sau'")]
        [Description("Thiết lập ngôn ngữ = 'Sau'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Sau")]
        public string Next
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Trang'")]
        [Description("Thiết lập ngôn ngữ = 'Trang'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Trang")]
        public string PageNumber
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            try
            {
                NewsListUS control = (NewsListUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/NewsListUS.ascx");
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
