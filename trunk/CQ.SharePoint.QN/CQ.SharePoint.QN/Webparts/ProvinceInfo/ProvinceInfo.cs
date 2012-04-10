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
    [Guid("19465c9a-0e03-4766-b949-f30971d76d0b")]
    public class ProvinceInfo : System.Web.UI.WebControls.WebParts.WebPart
    {
        public ProvinceInfo()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                ProvinceInfoUS control = (ProvinceInfoUS)this.Page.LoadControl("WebPartsUS/ProvinceInfoUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
