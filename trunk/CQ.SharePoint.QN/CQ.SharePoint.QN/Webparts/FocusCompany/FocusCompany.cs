﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Serialization;
using CQ.SharePoint.QN.Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;

namespace CQ.SharePoint.QN.Webparts
{
    [Guid("ca8428e6-4c64-4a00-9c89-b860ac4c46d9")]
    public class FocusCompany : Microsoft.SharePoint.WebPartPages.WebPart
    {
        [WebBrowsable(true)]
        [FriendlyName("Nhập số tin muốn hiển thị")]
        [Description("Số tin muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NumberOfNews { get; set; }

        [WebBrowsable(true)]
        [FriendlyName("Nhập kích thước chiều cao của ảnh")]
        [Description("Chiều cao của các ảnh")]
        [Category("Cấu hình")]
        [DefaultValue(150)]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string ImageHeight { get; set; }

        [WebBrowsable(true)]
        [FriendlyName("Nhập kích thước chiều rộng của ảnh")]
        [Description("Chiều rộng của các ảnh")]
        [Category("Cấu hình")]
        [DefaultValue(305)]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string ImageWidth { get; set; }


        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Doanh nghiệp tiêu biểu'")]
        [Description("Thiết lập ngôn ngữ = 'Doanh nghiệp tiêu biểu'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Doanh nghiệp tiêu biểu")]
        public string FocusCompanyTitle { get; set; }

        public FocusCompany()
        {
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try
            {
                FocusCompanyUS control = (FocusCompanyUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/FocusCompanyUS.ascx");
                control.WebpartParent = this;
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }

        
    }
}
