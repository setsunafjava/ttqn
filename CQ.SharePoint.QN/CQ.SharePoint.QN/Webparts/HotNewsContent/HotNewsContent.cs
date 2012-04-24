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
    public class HotNewsContent : System.Web.UI.WebControls.WebParts.WebPart
    {
        public HotNewsContent()
        {
        }

        [WebBrowsable(true)]
        [FriendlyName("WebpartName")]
        [Description("WebpartName")]
        [Category("QN")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string WebpartName
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
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
