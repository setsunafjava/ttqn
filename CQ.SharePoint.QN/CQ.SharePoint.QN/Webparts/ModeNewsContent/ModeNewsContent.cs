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
    [Guid("ab67ce11-c8af-4c75-8ffe-0427841454cd")]
    public class ModeNewsContent : Microsoft.SharePoint.WebPartPages.WebPart
    {
        [WebBrowsable(true)]
        [FriendlyName("Số tin tức muốn hiển thị")]
        [Description("Số tin tức muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NewsNumber { get; set; }

        [WebBrowsable(false)]
        [FriendlyName("")]
        [Description("")]
        [Category("")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NewsCategoryName1 { get; set; }
        public string NewsCategoryName2 { get; set; }
        public string NewsCategoryName3 { get; set; }

        [WebBrowsable(false)]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NewsCategoryId1 { get; set; }

        [WebBrowsable(false)]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NewsCategoryId2 { get; set; }

        [WebBrowsable(false)]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NewsCategoryId3 { get; set; }

        public ModeNewsContent()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try
            {
                var control = (ModeNewsContentUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/ModeNewsContentUS.ascx");
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
            var toolparts = new ToolPart[3];
            var wptp = new WebPartToolPart();
            var cptp = new CustomPropertyToolPart();
            var ctp = new DropDownList1();
            toolparts[0] = wptp;
            toolparts[1] = cptp;
            toolparts[2] = ctp;
            return toolparts;
        }
    }

    public class DropDownList1 : ToolPart
    {
        DropDownList ddlTypes1 = new DropDownList();
        DropDownList ddlTypes2 = new DropDownList();
        DropDownList ddlTypes3 = new DropDownList();
        public DropDownList1()
        {
            Title = "Chọn kiểu hiển thị";
        }

        protected override void CreateChildControls()
        {
            string companyCaml = string.Format("<Where><IsNotNull><FieldRef Name='Title' /></IsNotNull></Where>");
            var table = Utilities.GetNewsRecords(companyCaml, 100, ListsName.English.SubNewsCategory);

            if (table != null && table.Rows.Count > 0)
            {
                ddlTypes1.DataSource = table;
                ddlTypes1.ID = FieldsName.NewsCategory.FieldValuesDefault.SelectType1;
                ddlTypes1.DataTextField = FieldsName.Title;
                ddlTypes1.DataValueField = FieldsName.Id;
                ddlTypes1.DataBind();

                ddlTypes2.DataSource = table;
                ddlTypes2.ID = FieldsName.NewsCategory.FieldValuesDefault.SelectType2;
                ddlTypes2.DataTextField = FieldsName.Title;
                ddlTypes2.DataValueField = FieldsName.Id;
                ddlTypes2.DataBind();

                ddlTypes3.DataSource = table;
                ddlTypes3.ID = FieldsName.NewsCategory.FieldValuesDefault.SelectType3;
                ddlTypes3.DataTextField = FieldsName.Title;
                ddlTypes3.DataValueField = FieldsName.Id;
                ddlTypes3.DataBind();
            }
            Controls.Add(ddlTypes1);
            Controls.Add(ddlTypes2);
            Controls.Add(ddlTypes3);
        }
        public override void ApplyChanges()
        {
            ModeNewsContent parentWebPart = (ModeNewsContent)this.ParentToolPane.SelectedWebPart;
            RetrievePropertyValues(Controls, parentWebPart);
        }

        private void RetrievePropertyValues(ControlCollection controls, ModeNewsContent parentWebPart)
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

        private void RetrievePropertyValue(Control ctl, ModeNewsContent parentWebPart)
        {
            if (ctl is DropDownList)
            {
                if (FieldsName.NewsCategory.FieldValuesDefault.SelectType1.Equals(ctl.ID))
                {
                    DropDownList drp = (DropDownList)ctl;
                    if (drp.SelectedItem.Value.Trim() != "")
                    {
                        parentWebPart.NewsCategoryName1 = drp.SelectedItem.Text;
                        parentWebPart.NewsCategoryId1 = drp.SelectedItem.Value;
                    }
                }
                else if (FieldsName.NewsCategory.FieldValuesDefault.SelectType2.Equals(ctl.ID))
                {
                    DropDownList drp2 = (DropDownList)ctl;
                    if (drp2.SelectedItem.Value.Trim() != "")
                    {
                        parentWebPart.NewsCategoryName2 = drp2.SelectedItem.Text;
                        parentWebPart.NewsCategoryId2 = drp2.SelectedItem.Value;
                    }
                }
                else if (FieldsName.NewsCategory.FieldValuesDefault.SelectType3.Equals(ctl.ID))
                {
                    DropDownList drp3 = (DropDownList)ctl;
                    if (drp3.SelectedItem.Value.Trim() != "")
                    {
                        parentWebPart.NewsCategoryName3 = drp3.SelectedItem.Text;
                        parentWebPart.NewsCategoryId3 = drp3.SelectedItem.Value;
                    }
                }
            }
        }
    }
}
