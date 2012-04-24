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
    [Guid("9cfb6b2a-8a9c-404a-bac8-c12f2bc9876a")]
    public class CompanyListRight : Microsoft.SharePoint.WebPartPages.WebPart
    {
        public CompanyListRight()
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
        public string CompanyType { get; set; }

        [WebBrowsable(false)]
        [FriendlyName("Kiểu doanh nghiệp được chọn")]
        [Description("Kiểu doanh nghiệp được chọn")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string CompanyId { get; set; }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                CompanyListRightUS control = (CompanyListRightUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/CompanyListRightUS.ascx");
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
            SelectCompanyStatus ctp = new SelectCompanyStatus();
            toolparts[0] = wptp;
            toolparts[1] = cptp;
            toolparts[2] = ctp;
            return toolparts;
        }
    }


    public class SelectCompanyStatus : ToolPart
    {
        DropDownList ddlTypes = new DropDownList();
        //CompanyListRight _myParent = null;
        public SelectCompanyStatus()
        {
            this.Title = "Chọn kiểu hiển thị";

        }

        protected override void CreateChildControls()
        {
            string companyCaml = string.Format(" <Where><Eq><FieldRef Name='{0}' /><Value Type='MultiChoice'>{1}</Value></Eq></Where>", FieldsName.NewsCategory.English.TypeCategory, FieldsName.NewsCategory.FieldValuesDefault.DoanhNghiep);
            var table = Utilities.GetNewsRecords(companyCaml, 20, ListsName.English.NewsCategory);

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
            CompanyListRight parentWebPart = (CompanyListRight)this.ParentToolPane.SelectedWebPart;
            //base.ApplyChanges();
            //            parentWebPart.CompanyType = ddlTypes.SelectedItem.Text;
            RetrievePropertyValues(this.Controls, parentWebPart);
        }

        private void RetrievePropertyValues(ControlCollection controls, CompanyListRight parentWebPart)
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

        private void RetrievePropertyValue(Control ctl, CompanyListRight parentWebPart)
        {
            if (ctl is DropDownList)
            {
                if (FieldsName.NewsCategory.FieldValuesDefault.SelectType.Equals(ctl.ID))
                {
                    DropDownList drp = (DropDownList)ctl;
                    if (drp.SelectedItem.Value != "")
                    {
                        parentWebPart.CompanyType = drp.SelectedItem.Text;
                        parentWebPart.CompanyId = drp.SelectedItem.Value;
                    }
                }
            }
        }
    }
}
