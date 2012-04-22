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
    [Guid("2fb13299-518e-4042-8c0d-9ccaedfb4e4c")]
    public class Gallery : System.Web.UI.WebControls.WebParts.WebPart
    {
        public Gallery()
        {
        }

        protected override void CreateChildControls()
        {
            try
            {
                GalleryUS control = (GalleryUS)this.Page.LoadControl("WebPartsUS/GalleryUS.ascx");
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
