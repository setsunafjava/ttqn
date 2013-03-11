using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class DocumentsManagerUS : UserControl
    {
        public DocumentsManager WebpartParent;
        public string NewsUrl = string.Empty;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var dtNow = DateTime.Now;
                string companyListQuery = string.Format(@"<Where>
                                                          <And>
                                                             <Leq>
                                                                <FieldRef Name='{0}' />
                                                                <Value IncludeTimeValue='FALSE' Type='DateTime'>{1}</Value>
                                                             </Leq>
                                                             <And>
                                                                <Geq>
                                                                   <FieldRef Name='{2}' />
                                                                   <Value IncludeTimeValue='FALSE' Type='DateTime'>{3}</Value>
                                                                </Geq>
                                                                <Neq>
                                                                   <FieldRef Name='{4}' />
                                                                   <Value Type='Boolean'>1</Value>
                                                                </Neq>
                                                             </And>
                                                          </And>
                                                       </Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>",
                                                                FieldsName.QuangCaoRaoVat.English.NgayBatDau,
                                                                SPUtility.CreateISO8601DateTimeFromSystemDateTime(dtNow),
                                                                FieldsName.QuangCaoRaoVat.English.NgayKetThuc,
                                                                SPUtility.CreateISO8601DateTimeFromSystemDateTime(dtNow),
                                                                FieldsName.QuangCaoRaoVat.English.Status);
                uint newsNumber = 5;

                if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                {
                    newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                }
                var doanhNghiepTieuBieu = Utilities.GetDocLibRecords(companyListQuery, newsNumber, ListsName.English.DoanhNghiepTieuBieu);

                if (doanhNghiepTieuBieu != null && doanhNghiepTieuBieu.Rows.Count > 0)
                {
                    //rptFocusCompany.DataSource = GetTableWithCorrectUrlDoclib(SPContext.Current.Web, doanhNghiepTieuBieu);
                    //rptFocusCompany.DataBind();
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
        }

        public static int GetWebpartPropertiesValue(string value)
        {
            int result = 100;
            try
            {
                var numbertemp = Convert.ToInt32(value);
                if (numbertemp > 0) result = numbertemp;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        protected void OnItemDataBound_DocumentsManager(object sender, RepeaterItemEventArgs e)
        {
            var t = e.Item.DataItem as DataRowView;
        }
    }
}
