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
    [Guid("99a7b668-6516-4c68-93c4-8bc1b4d6d15d")]
    public class ShouldToKnow : System.Web.UI.WebControls.WebParts.WebPart
    {
        public ShouldToKnow()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                ShouldToKnowUS control = (ShouldToKnowUS)this.Page.LoadControl("WebPartsUS/ShouldToKnowUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
