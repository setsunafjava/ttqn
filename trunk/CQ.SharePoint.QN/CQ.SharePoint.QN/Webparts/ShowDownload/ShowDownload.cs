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
    [Guid("a86a663f-e2bd-4f48-b35d-4e693c41b159")]
    public class ShowDownload : System.Web.UI.WebControls.WebParts.WebPart
    {
        public ShowDownload()
        {
        }

        protected override void CreateChildControls()
        {
            try
            {
                ShowDownloadUS control = (ShowDownloadUS)this.Page.LoadControl("WebPartsUS/ShowDownloadUS.ascx");
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
