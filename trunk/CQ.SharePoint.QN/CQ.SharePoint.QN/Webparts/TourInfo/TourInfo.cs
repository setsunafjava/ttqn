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
    [Guid("42a479b7-17ef-421a-b341-5b28788cb6ba")]
    public class TourInfo : System.Web.UI.WebControls.WebParts.WebPart
    {
        public TourInfo()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                TourInfoUS control = (TourInfoUS)this.Page.LoadControl("WebPartsUS/TourInfoUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
