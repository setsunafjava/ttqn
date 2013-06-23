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
    public partial class FocusCompanyUS : UserControl
    {
        public FocusCompany WebpartParent;
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
                    rptFocusCompany.DataSource = GetTableWithCorrectUrlDoclib(SPContext.Current.Web, doanhNghiepTieuBieu);
                    rptFocusCompany.DataBind();
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
        }

        public static DataTable GetTableWithCorrectUrlDoclib(SPWeb web, DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string imagepath = string.Empty;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    imagepath = Convert.ToString(dataTable.Rows[i][FieldsName.QuangCaoRaoVat.English.LinkFileName]);
                    var extFile = imagepath.Substring(imagepath.Length - 3, 3);
                    var fileName = imagepath.Substring(0, imagepath.Length - 4);
                    dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = string.Format("{0}/{1}/{2}.{3}", web.Url, ListsName.English.DoanhNghiepTieuBieu, fileName, extFile);
                }
            }
            return dataTable;
        }

        protected void OnItemDataBound_FocusCompany(object sender, RepeaterItemEventArgs e)
        {
            var t = e.Item.DataItem as DataRowView;
            if (t != null)
            {
                string imagePath = Convert.ToString(t.Row[18]);
                int height = 100;
                var heightSet = Convert.ToInt32(t.Row[21]);
                if (heightSet>0)
                {
                    height = heightSet;
                }

                if (!String.IsNullOrEmpty(imagePath) && imagePath.Length > 3)
                {
                    var extent = imagePath.Substring(imagePath.Length - 3);
                    var literalTemp = (Literal)e.Item.FindControl("ltrFlash1");
                    
                    switch (extent)
                    {
                        case "swf":
                            {
                                if (literalTemp != null)
                                {
                                    literalTemp.Visible = true;
                                    literalTemp.Text = string.Format("<embed bgcolor=\"#FFFFFF\" height=\"{0}\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"{1}\" src=\"{2}\" ></embed>", height, 300, imagePath);
                                }
                                break;
                            }
                        default:
                            {
                                if (literalTemp != null)
                                {
                                    literalTemp.Visible = true;
                                    literalTemp.Text = string.Format("<img src=\"{0}\" height=\"{1}\" width=\"{2}\" />", imagePath, height, 300);
                                }
                                break;
                            }
                    }
                }
            }
        }
    }
}
