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
    [Guid("4907161d-f054-40be-a766-93f3904d9f46")]
    public class Contact : System.Web.UI.WebControls.WebParts.WebPart
    {
        public Contact()
        {
        }

        protected override void CreateChildControls()
        {
            try
            {
                ContactUS control = (ContactUS)this.Page.LoadControl("WebPartsUS/ContactUS.ascx");
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
