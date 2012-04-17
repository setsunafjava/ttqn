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
    [Guid("bb40f5b0-6a79-413f-838b-edadf6c70415")]
    public class LeftNewsContent : Microsoft.SharePoint.WebPartPages.WebPart
    {
        public LeftNewsContent()
        {
            this.ExportMode = WebPartExportMode.All;
        }
        public string GroupName { get; set; }

        [WebBrowsable(true)]
        [FriendlyName("Chọn số tin hiển thị")]
        [Description("Số tin muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NumberOfNews { get; set; }

        [WebBrowsable(false)]
        [FriendlyName("Chọn số tin hiển thị")]
        [Description("Số tin muốn hiển thị")]
        [Category("Cấu hình")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public string NewsGroupID { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try
            {
                LeftNewsContentUS control = (LeftNewsContentUS)this.Page.LoadControl("WebPartsUS/LeftNewsContentUS.ascx");
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
            ToolpartDropDownList ctp = new ToolpartDropDownList();
            toolparts[0] = wptp;
            toolparts[1] = cptp;
            toolparts[2] = ctp;
            return toolparts;
        }
    }


    public class ToolpartDropDownList : ToolPart
    {
        DropDownList ddlTypes = new DropDownList();
        //LeftNewsContent _myParent = null;
        public ToolpartDropDownList()
        {
            this.Title = "Chọn nhóm tin";

        }

        protected void BindDataToDropdown(DropDownList dropDownList)
        {
            dropDownList.ID = "SelectType";
            dropDownList.DataSource = Utilities.GetListFromUrl(ListsName.English.NewsCategory);
            dropDownList.DataTextField = FieldsName.Title;
            dropDownList.DataValueField = FieldsName.Id;
            dropDownList.DataBind();
        }

        protected override void CreateChildControls()
        {
            BindDataToDropdown(ddlTypes);
            //_myParent = (LeftNewsContent)ParentToolPane.SelectedWebPart;
            Controls.Add(ddlTypes);
        }
        public override void ApplyChanges()
        {
            LeftNewsContent parentWebPart = (LeftNewsContent)this.ParentToolPane.SelectedWebPart;
            //base.ApplyChanges();
            //_myParent.GroupName = ddlTypes.SelectedItem.Text;
            RetrievePropertyValues(this.Controls, parentWebPart);
        }

        private void RetrievePropertyValues(ControlCollection controls, LeftNewsContent parentWebPart)
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

        private void RetrievePropertyValue(Control ctl, LeftNewsContent parentWebPart)
        {
            if (ctl is DropDownList)
            {
                if ("SelectType".Equals(ctl.ID))
                {
                    DropDownList drp = (DropDownList)ctl;
                    if (drp.SelectedItem.Value != "")
                    {
                        parentWebPart.GroupName = drp.SelectedItem.Text;
                        parentWebPart.NewsGroupID = drp.SelectedItem.Value;
                    }
                }
            }
        }
    }
}
