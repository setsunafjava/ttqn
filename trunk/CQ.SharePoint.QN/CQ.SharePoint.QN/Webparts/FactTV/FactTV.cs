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
    [Guid("201619bd-891d-498a-90a4-1acd8dacbf9a")]
    public class FactTV : System.Web.UI.WebControls.WebParts.WebPart
    {
        public FactTV()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                FactTVUS control = (FactTVUS)this.Page.LoadControl("WebPartsUS/FactTVUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
