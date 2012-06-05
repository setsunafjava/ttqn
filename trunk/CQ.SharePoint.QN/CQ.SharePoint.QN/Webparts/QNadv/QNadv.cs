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
    [Guid("30024d95-df66-4bed-8192-0878c2fe3cbd")]
    public class QNadv : Microsoft.SharePoint.WebPartPages.WebPart
    {
        public QNadv()
        {
        }

        [WebBrowsable(false)]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        public int QCID
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            try
            {
                QNadvUS control = (QNadvUS)this.Page.LoadControl(SPContext.Current.Web.Site.ServerRelativeUrl.TrimEnd('/') + "/WebPartsUS/QNadvUS.ascx");
                control.ParentWP = this;
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
            var ctp = new AdvDdl();
            toolparts[0] = wptp;
            toolparts[1] = cptp;
            toolparts[2] = ctp;
            return toolparts;
        }
    }

    public class AdvDdl : ToolPart
    {
        DropDownList ddlAdv = new DropDownList();
        public AdvDdl()
        {
            Title = "Chọn quảng cáo";
        }

        protected override void CreateChildControls()
        {
            var table = Utilities.GetDocListFromUrl("QuangCao").Items.GetDataTable();

            if (table != null && table.Rows.Count > 0)
            {
                ddlAdv.DataSource = table;
                ddlAdv.ID = FieldsName.NewsCategory.FieldValuesDefault.QCID;
                ddlAdv.DataTextField = FieldsName.Title;
                ddlAdv.DataValueField = FieldsName.Id;
                ddlAdv.DataBind();
            }
            Controls.Add(ddlAdv);
        }
        public override void ApplyChanges()
        {
            QNadv parentWebPart = (QNadv)this.ParentToolPane.SelectedWebPart;
            RetrievePropertyValues(Controls, parentWebPart);
        }

        private void RetrievePropertyValues(ControlCollection controls, QNadv parentWebPart)
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

        private void RetrievePropertyValue(Control ctl, QNadv parentWebPart)
        {
            if (ctl is DropDownList)
            {
                if (FieldsName.NewsCategory.FieldValuesDefault.QCID.Equals(ctl.ID))
                {
                    DropDownList drp = (DropDownList)ctl;
                    if (drp.SelectedItem.Value.Trim() != "")
                    {
                        parentWebPart.QCID = Convert.ToInt32(drp.SelectedValue);
                    }
                }
            }
        }
    }
}
