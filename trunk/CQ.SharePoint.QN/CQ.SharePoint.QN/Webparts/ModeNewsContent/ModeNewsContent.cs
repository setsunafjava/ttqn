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
    [Guid("ab67ce11-c8af-4c75-8ffe-0427841454cd")]
    public class ModeNewsContent : System.Web.UI.WebControls.WebParts.WebPart
    {
        public ModeNewsContent()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                ModeNewsContentUS control = (ModeNewsContentUS)this.Page.LoadControl("WebPartsUS/ModeNewsContentUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
