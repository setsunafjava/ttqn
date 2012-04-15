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
    [Guid("bf205d59-1f9b-47e0-83b3-95629a13eb6a")]
    public class OtherNews : System.Web.UI.WebControls.WebParts.WebPart
    {
        public OtherNews()
        {
        }

        protected override void CreateChildControls()
        {
            try
            {
                OtherNewsUS control = (OtherNewsUS)this.Page.LoadControl("WebPartsUS/OtherNewsUS.ascx");
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
