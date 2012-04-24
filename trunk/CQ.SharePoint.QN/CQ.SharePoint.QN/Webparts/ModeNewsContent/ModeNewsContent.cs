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
        [FriendlyName("Chọn kiểu thông tin muốn hiển thị")]
        [Description("Chọn kiểu thông tin muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NewsType { get; set; }

        public ModeNewsContent()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {
                ModeNewsContentUS control = (ModeNewsContentUS)this.Page.LoadControl("WebPartsUS/ModeNewsContentUS.ascx");
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
            NewsTypeDropdownlist ctp = new NewsTypeDropdownlist();
            toolparts[0] = wptp;
            toolparts[1] = cptp;
            toolparts[2] = ctp;
            return toolparts;
        }
    }

    public class NewsTypeDropdownlist : ToolPart
    {
        DropDownList ddlTypes = new DropDownList();
        //ModeNewsContent _myParent = null;
        public NewsTypeDropdownlist()
        {
            this.Title = "Chọn nhóm tin";

        }

        protected void BindDataToDropdown(DropDownList dropDownList)
        {
            dropDownList.ID = "SelectType";
            //dropDownList.DataSource = Utilities.GetListFromUrl(ListsName.English.NewsCategory);
            //dropDownList.DataTextField = FieldsName.Title;
            //dropDownList.DataValueField = FieldsName.Id;
            //dropDownList.DataBind();
            dropDownList.Items.Add(new ListItem("Tỉnh Ủy", "1"));
            dropDownList.Items.Add(new ListItem("Sở Ban Nghành", "2"));
            dropDownList.DataBind();
        }

        protected override void CreateChildControls()
        {
            BindDataToDropdown(ddlTypes);
            //_myParent = (ModeNewsContent)ParentToolPane.SelectedWebPart;
            Controls.Add(ddlTypes);
        }
        public override void ApplyChanges()
        {
            ModeNewsContent parentWebPart = (ModeNewsContent)this.ParentToolPane.SelectedWebPart;
            //base.ApplyChanges();
            //_myParent.GroupName = ddlTypes.SelectedItem.Text;
            RetrievePropertyValues(this.Controls, parentWebPart);
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
                if ("SelectType".Equals(ctl.ID))
                {
                    DropDownList drp = (DropDownList)ctl;
                    if (drp.SelectedItem.Value != "")
                    {
                        parentWebPart.NewsType = drp.SelectedItem.Value;
                    }
                }
            }
        }
    }
}
