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
    [Guid("b9ccb1db-d923-460d-a0f8-738cba6355b2")]
    public class MailBox : System.Web.UI.WebControls.WebParts.WebPart
    {
        public MailBox()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                MailBoxUS control = (MailBoxUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/MailBoxUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
