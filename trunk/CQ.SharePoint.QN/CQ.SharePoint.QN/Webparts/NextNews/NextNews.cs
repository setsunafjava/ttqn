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
    [Guid("c916f489-4949-47b1-a628-5d265875076a")]
    public class NextNews : System.Web.UI.WebControls.WebParts.WebPart
    {
        public NextNews()
        {
        }

        protected override void CreateChildControls()
        {
            try
            {
                NextNewsUS control = (NextNewsUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/NextNewsUS.ascx");
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
