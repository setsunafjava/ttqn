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
    [Guid("65f398a5-829a-4aaf-882e-9d2d88fd2623")]
    public class SlideShowHome : System.Web.UI.WebControls.WebParts.WebPart
    {
        public SlideShowHome()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                SlideShowHomeUS control = (SlideShowHomeUS)this.Page.LoadControl("WebPartsUS/SlideShowHomeUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
