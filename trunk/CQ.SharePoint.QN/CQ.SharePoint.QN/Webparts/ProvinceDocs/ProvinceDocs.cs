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
    [Guid("93962b62-17e4-4c35-a216-b2369a19b26b")]
    public class ProvinceDocs : System.Web.UI.WebControls.WebParts.WebPart
    {
        public ProvinceDocs()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                ProvinceDocsUS control = (ProvinceDocsUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/ProvinceDocsUS.ascx");
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}
