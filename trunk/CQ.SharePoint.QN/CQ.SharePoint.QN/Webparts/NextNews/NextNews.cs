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
    [Guid("c916f489-4949-47b1-a628-5d265875076a")]
    public class NextNews : System.Web.UI.WebControls.WebParts.WebPart
    {
        public NextNews()
        {
        }
        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Ngày'")]
        [Description("Thiết lập ngôn ngữ = 'Ngày'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Ngày")]
        public string DayTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Tháng'")]
        [Description("Thiết lập ngôn ngữ = 'Tháng'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Tháng")]
        public string MonthTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Năm'")]
        [Description("Thiết lập ngôn ngữ = 'Năm'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Năm")]
        public string YearTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Xem tin tiếp theo...'")]
        [Description("Thiết lập ngôn ngữ = 'Xem tin tiếp theo...'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Xem tin tiếp theo...")]
        public string SeeMore
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            try
            {
                NextNewsUS control = (NextNewsUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/NextNewsUS.ascx");
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
