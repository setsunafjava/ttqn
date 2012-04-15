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
    [Guid("9f438845-18d2-400a-908c-948ce9adf53a")]
    public class NewsList : System.Web.UI.WebControls.WebParts.WebPart
    {
        public NewsList()
        {
        }

        protected override void CreateChildControls()
        {
            try
            {
                NewsListUS control = (NewsListUS)this.Page.LoadControl("WebPartsUS/NewsListUS.ascx");
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
