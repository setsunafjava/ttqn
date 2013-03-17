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
    [Guid("fc902e1f-2ae5-494e-9f3f-96257f072c04")]
    public class NewsDetail : System.Web.UI.WebControls.WebParts.WebPart
    {
        [WebBrowsable(true)]
        [FriendlyName("Số tin tức muốn hiển thị trong mục chuyên đề")]
        [Description("Số tin tức muốn hiển thị trong mục chuyên đề")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NumberOfCategory { get; set; }

        public NewsDetail()
        {
        }

        protected override void CreateChildControls()
        {
            try
            {
                NewsDetailUS control = (NewsDetailUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/NewsDetailUS.ascx");
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
