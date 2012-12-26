using System;
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
    [Guid("99a7b668-6516-4c68-93c4-8bc1b4d6d15d")]
    public class ShouldToKnow : Microsoft.SharePoint.WebPartPages.WebPart
    {
        [WebBrowsable(true)]
        [FriendlyName("Nhập số tin muốn hiển thị")]
        [Description("Số tin muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NumberOfNews { get; set; }

        [WebBrowsable(false)]
        [FriendlyName("Thông tin muốn hiển thị")]
        [Description("Thông tin muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NewsType { get; set; }

        [WebBrowsable(false)]
        [FriendlyName("Kiểu doanh nghiệp được chọn")]
        [Description("Kiểu doanh nghiệp được chọn")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string CategoryId { get; set; }

        [WebBrowsable(true)]
        [FriendlyName("Thiết lập ngôn ngữ = 'Liên hệ quảng cáo: Hotline 0904 555 888'")]
        [Description("Thiết lập ngôn ngữ = 'Liên hệ quảng cáo: Hotline 0904 555 888'")]
        [Category("Ngôn ngữ")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("Liên hệ quảng cáo: Hotline 0904 555 888")]
        public string ShouldToKnowTitle
        {
            get;
            set;
        }

        public ShouldToKnow()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try
            {
                ShouldToKnowUS control = (ShouldToKnowUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/ShouldToKnowUS.ascx");
                control.WebpartParent = this;
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
        public override ToolPart[] GetToolParts()
        {
            ToolPart[] toolparts = new ToolPart[3];
            WebPartToolPart wptp = new WebPartToolPart();
            CustomPropertyToolPart cptp = new CustomPropertyToolPart();
            SelectAdvType ctp = new SelectAdvType();
            toolparts[0] = wptp;
            toolparts[1] = cptp;
            toolparts[2] = ctp;
            return toolparts;
        }
    }

    public class SelectAdvType : ToolPart
    {
        DropDownList ddlTypes = new DropDownList();
        public SelectAdvType()
        {
            Title = "Chọn kiểu thông tin muốn hiển thị";
        }

        protected override void CreateChildControls()
        {
            string companyCaml = string.Format("<OrderBy><FieldRef Name='ArticleStartDates' Ascending='False' /></OrderBy>");
            var table = Utilities.GetNewsRecords(companyCaml, ListsName.English.ShouldToKnowCategory);

            if (table != null && table.Rows.Count > 0)
            {
                ddlTypes.DataSource = table;
                ddlTypes.ID = FieldsName.NewsCategory.FieldValuesDefault.SelectType;
                ddlTypes.DataTextField = FieldsName.Title;
                ddlTypes.DataValueField = FieldsName.Id;
                ddlTypes.DataBind();
            }
            Controls.Add(ddlTypes);
        }

        public override void ApplyChanges()
        {
            ShouldToKnow parentWebPart = (ShouldToKnow)this.ParentToolPane.SelectedWebPart;
            RetrievePropertyValues(this.Controls, parentWebPart);
        }

        private void RetrievePropertyValues(ControlCollection controls, ShouldToKnow parentWebPart)
        {
            foreach (Control ctl in controls)
            {
                RetrievePropertyValue(ctl, parentWebPart);
                if (ctl.HasControls())
                {
                    RetrievePropertyValues(ctl.Controls, parentWebPart);
                }
            }
        }

        private void RetrievePropertyValue(Control ctl, ShouldToKnow parentWebPart)
        {
            if (ctl is DropDownList)
            {
                if (FieldsName.NewsCategory.FieldValuesDefault.SelectType.Equals(ctl.ID))
                {
                    DropDownList drp = (DropDownList)ctl;
                    if (drp.SelectedItem.Value != "")
                    {
                        parentWebPart.NewsType = drp.SelectedItem.Text;
                        parentWebPart.CategoryId = drp.SelectedItem.Value;
                    }
                }
            }
        }
    }
}
