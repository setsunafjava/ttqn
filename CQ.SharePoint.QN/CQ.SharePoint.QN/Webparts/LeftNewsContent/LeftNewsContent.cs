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
    [Guid("bb40f5b0-6a79-413f-838b-edadf6c70415")]
    public class LeftNewsContent : System.Web.UI.WebControls.WebParts.WebPart
    {
        public LeftNewsContent()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                LeftNewsContentUS control = (LeftNewsContentUS)this.Page.LoadControl("WebPartsUS/LeftNewsContentUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
