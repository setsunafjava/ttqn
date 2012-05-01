using System;
using System.ComponentModel;
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
    [Guid("5e9a4b2d-c1c5-49ec-9283-d1914f6ce854")]
    public class NeedToKnow : System.Web.UI.WebControls.WebParts.WebPart
    {
        public NeedToKnow()
        {
        }

        [WebBrowsable(true)]
        [FriendlyName("Link kết quả xổ số")]
        [Description("Link kết quả xổ số")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string KQXSUrl
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Link kết quả bóng đá")]
        [Description("Link kết quả bóng đá")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string BDUrl
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                NeedToKnowUS control = (NeedToKnowUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/NeedToKnowUS.ascx");
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
