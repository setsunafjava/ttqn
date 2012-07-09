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
    [Guid("19465c9a-0e03-4766-b949-f30971d76d0b")]
    public class ProvinceInfo : Microsoft.SharePoint.WebPartPages.WebPart
    {
        public ProvinceInfo()
        {
        }

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

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                ProvinceInfoUS control = (ProvinceInfoUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/ProvinceInfoUS.ascx");
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
            SelectNewsType ctp = new SelectNewsType();
            toolparts[0] = wptp;
            toolparts[1] = cptp;
            toolparts[2] = ctp;
            return toolparts;
        }
    }
    public class SelectNewsType : ToolPart
    {
        DropDownList ddlTypes = new DropDownList();
        public SelectNewsType()
        {
            Title = "Chọn kiểu thông tin muốn hiển thị";

        }

        protected override void CreateChildControls()
        {
            string companyCaml = string.Format("<OrderBy><FieldRef Name='Title' Ascending='True' /></OrderBy>");
            var table = Utilities.GetNewsRecords(companyCaml, ListsName.English.ProvinceInfoCategory);

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
            ProvinceInfo parentWebPart = (ProvinceInfo)this.ParentToolPane.SelectedWebPart;
            RetrievePropertyValues(Controls, parentWebPart);
        }

        private void RetrievePropertyValues(ControlCollection controls, ProvinceInfo parentWebPart)
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

        private void RetrievePropertyValue(Control ctl, ProvinceInfo parentWebPart)
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
