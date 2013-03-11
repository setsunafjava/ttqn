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
        #region Set language
        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Thông tin cần biết'")]
        [Description("Tiêu đề 'Thông tin cần biết'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Thông tin cần biết")]
        public string NeedToKnowTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Tỷ Giá'")]
        [Description("Tiêu đề 'Tỷ Giá'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Tỷ Giá")]
        public string RateTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Vàng'")]
        [Description("Tiêu đề 'Vàng'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Vàng")]
        public string GoldTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Mua'")]
        [Description("Tiêu đề 'Mua'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Mua")]
        public string BuyTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Bán'")]
        [Description("Tiêu đề 'Bán'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Bán")]
        public string SaleTitle
        {
            get;
            set;
        }


        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Ngoại tệ'")]
        [Description("Tiêu đề 'Ngoại tệ'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Ngoại tệ")]
        public string CurrencyTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Bóng đá'")]
        [Description("Tiêu đề 'Bóng đá'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Bóng đá")]
        public string FootballTitle
        {
            get;
            set;
        }

        [WebBrowsable(true)]
        [FriendlyName("Tiêu đề 'Kết quả Xổ Số'")]
        [Description("Tiêu đề 'Kết quả Xổ Số'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Kết quả Xổ Số")]
        public string LotoTitle
        {
            get;
            set;
        }
        #endregion

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
