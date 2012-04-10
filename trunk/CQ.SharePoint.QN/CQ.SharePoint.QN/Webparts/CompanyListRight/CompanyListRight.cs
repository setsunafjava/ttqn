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
    [Guid("9cfb6b2a-8a9c-404a-bac8-c12f2bc9876a")]
    public class CompanyListRight : System.Web.UI.WebControls.WebParts.WebPart
    {
        public CompanyListRight()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                CompanyListRightUS control = (CompanyListRightUS)this.Page.LoadControl("WebPartsUS/CompanyListRightUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
