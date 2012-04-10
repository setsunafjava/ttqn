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
    [Guid("25a8316b-2e6f-4461-b3bb-71d6e72a8fd9")]
    public class Footer : System.Web.UI.WebControls.WebParts.WebPart
    {
        public Footer()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                FooterUS control = (FooterUS)this.Page.LoadControl("WebPartsUS/FooterUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
