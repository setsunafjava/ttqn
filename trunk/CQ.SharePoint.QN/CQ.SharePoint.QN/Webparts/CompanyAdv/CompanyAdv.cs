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
    [Guid("9aa4e640-b74d-4397-b5e2-98d3aa93b1d4")]
    public class CompanyAdv : System.Web.UI.WebControls.WebParts.WebPart
    {
        public CompanyAdv()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                CompanyAdvUS control = (CompanyAdvUS)this.Page.LoadControl("WebPartsUS/CompanyAdvUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
