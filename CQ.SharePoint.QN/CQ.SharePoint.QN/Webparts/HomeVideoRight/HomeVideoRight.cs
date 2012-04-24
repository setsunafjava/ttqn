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
    [Guid("8ac7fb10-5f2c-4dea-a79f-07b8f1b6ca59")]
    public class HomeVideoRight : System.Web.UI.WebControls.WebParts.WebPart
    {
        public HomeVideoRight()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                HomeVideoRightUS control = (HomeVideoRightUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/HomeVideoRightUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
