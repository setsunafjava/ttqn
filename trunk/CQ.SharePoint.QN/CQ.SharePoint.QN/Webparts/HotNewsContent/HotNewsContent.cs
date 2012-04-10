using System;
using System.Runtime.InteropServices;
using System.Web;

namespace CQ.SharePoint.QN.Webparts
{
    [Guid("8b4fb7d8-b280-4d98-8921-9e137f6f7dae")]
    public class HotNewsContent : System.Web.UI.WebControls.WebParts.WebPart
    {
        public HotNewsContent()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                HotNewsContentUS control = (HotNewsContentUS)this.Page.LoadControl("WebPartsUS/HotNewsContentUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
