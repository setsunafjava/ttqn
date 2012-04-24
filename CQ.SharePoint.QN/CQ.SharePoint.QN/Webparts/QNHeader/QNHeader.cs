using System;
using System.Runtime.InteropServices;
using CQ.SharePoint.QN.Webparts;
using System.Web;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Webparts
{
    [Guid("00dc0549-75c7-4699-97cc-666639b9336b")]
    public class QNHeader : System.Web.UI.WebControls.WebParts.WebPart
    {
        public QNHeader()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            
            try
            {
                QNHeaderUS control = (QNHeaderUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/QNHeaderUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
