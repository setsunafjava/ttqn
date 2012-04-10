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
    [Guid("43766268-a956-4dd4-95e1-bdbb7bba88b7")]
    public class FocusNews : System.Web.UI.WebControls.WebParts.WebPart
    {
        public FocusNews()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                FocusNewsUS control = (FocusNewsUS)this.Page.LoadControl("WebPartsUS/FocusNewsUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
