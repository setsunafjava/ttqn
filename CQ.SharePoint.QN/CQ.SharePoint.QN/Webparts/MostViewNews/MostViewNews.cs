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
    [Guid("599964a2-1745-484b-8b04-37dcb32901f6")]
    public class MostViewNews : System.Web.UI.WebControls.WebParts.WebPart
    {
        public MostViewNews()
        {
        }

        protected override void CreateChildControls()
        {
            try
            {
                MostViewNewsUS control = (MostViewNewsUS)this.Page.LoadControl("WebPartsUS/MostViewNewsUS.ascx");
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
