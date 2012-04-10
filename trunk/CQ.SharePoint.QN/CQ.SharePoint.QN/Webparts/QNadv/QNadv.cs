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
    [Guid("30024d95-df66-4bed-8192-0878c2fe3cbd")]
    public class QNadv : System.Web.UI.WebControls.WebParts.WebPart
    {
        public QNadv()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                QNadvUS control = (QNadvUS)this.Page.LoadControl("WebPartsUS/QNadvUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
