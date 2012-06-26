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
    [Guid("b2b31986-18c6-4ef5-bf66-16ef189ac553")]
    public class ClassiFieds : Microsoft.SharePoint.WebPartPages.WebPart
    {
        public ClassiFieds()
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
        [FriendlyName("Kiểu doanh nghiệp được chọn")]
        [Description("Kiểu doanh nghiệp được chọn")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string CategoryType { get; set; }

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
                ClassiFiedsUS control = (ClassiFiedsUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/ClassiFiedsUS.ascx");
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
            SelectClassiFields ctp = new SelectClassiFields();
            toolparts[0] = wptp;
            toolparts[1] = cptp;
            toolparts[2] = ctp;
            return toolparts;
        }
    }

    public class SelectClassiFields : ToolPart
    {
        DropDownList ddlTypes = new DropDownList();

        public SelectClassiFields()
        {
            this.Title = "Chọn kiểu hiển thị";
        }

        protected override void CreateChildControls()
        {
            string companyCaml = string.Format("<Where><IsNotNull><FieldRef Name='Title' /></IsNotNull></Where>");
            var table = Utilities.GetNewsRecords(companyCaml, 1000, ListsName.English.NewsCategory);

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
            ClassiFieds parentWebPart = (ClassiFieds)this.ParentToolPane.SelectedWebPart;
            RetrievePropertyValues(this.Controls, parentWebPart);
        }

        private void RetrievePropertyValues(ControlCollection controls, ClassiFieds parentWebPart)
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

        private void RetrievePropertyValue(Control ctl, ClassiFieds parentWebPart)
        {
            if (ctl is DropDownList)
            {
                if (FieldsName.NewsCategory.FieldValuesDefault.SelectType.Equals(ctl.ID))
                {
                    DropDownList drp = (DropDownList)ctl;
                    if (drp.SelectedItem.Value != "")
                    {
                        parentWebPart.CategoryType = drp.SelectedItem.Text;
                        parentWebPart.CategoryId = drp.SelectedItem.Value;
                    }
                }
            }
        }
    }
}
