using System;
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
    [Guid("43772fc9-7f81-4ae5-bf69-6626353f7f7e")]
    public class WebLink : System.Web.UI.WebControls.WebParts.WebPart
    {
        public WebLink()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                WebLinkUS control = (WebLinkUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/WebLinkUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
