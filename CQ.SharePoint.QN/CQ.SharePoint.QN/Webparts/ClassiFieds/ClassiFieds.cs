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
    [Guid("b2b31986-18c6-4ef5-bf66-16ef189ac553")]
    public class ClassiFieds : Microsoft.SharePoint.WebPartPages.WebPart
    {
        public ClassiFieds()
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
        [FriendlyName("Tiêu đề 'Quảng cáo rao vặt'")]
        [Description("Tiêu đề 'Quảng cáo rao vặt'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Quảng cáo rao vặt")]
        public string ClassiFiedsTitle { get; set; }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try
            {
                ClassiFiedsUS control = (ClassiFiedsUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/ClassiFiedsUS.ascx");
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
