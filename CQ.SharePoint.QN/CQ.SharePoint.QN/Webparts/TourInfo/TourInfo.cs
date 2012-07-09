using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Serialization;
using CQ.SharePoint.QN.Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;

namespace CQ.SharePoint.QN.Webparts
{
    [Guid("42a479b7-17ef-421a-b341-5b28788cb6ba")]
    public class TourInfo : Microsoft.SharePoint.WebPartPages.WebPart
    {
        public TourInfo()
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
        [FriendlyName("Thiết lập ngôn ngữ = 'Thông tin du lịch'")]
        [Description("Thiết lập ngôn ngữ = 'Thông tin du lịch'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Thông tin du lịch")]
        public string TourInfoTitle
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                TourInfoUS control = (TourInfoUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/TourInfoUS.ascx");
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
