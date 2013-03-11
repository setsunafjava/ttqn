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
    public partial class CompanyAdvUS : UserControl
    {
        public CompanyAdv WebpartParent;
        public string NewsUrl = string.Empty;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                    var companyList = Utilities.GetDocLibRecords(companyListQuery, newsNumber, ListsName.English.QuangCaoDoanhNghiep);
                    var quangcaodoanhnghiep = GetTableWithCorrectUrlDoclib(SPContext.Current.Web, companyList);
                    //Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.NewsRecord.English.CategoryName, ref companyList);
                    if (quangcaodoanhnghiep != null && quangcaodoanhnghiep.Rows.Count > 0)
                    {
                        rptCompanyAdv.DataSource = quangcaodoanhnghiep;
                        rptCompanyAdv.DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
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
                    dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = string.Format("{0}/{1}/{2}.{3}", web.Url, ListsName.English.QuangCaoDoanhNghiep, fileName, extFile);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// rptMenu_OnItemDataBound
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        protected void rptCompanyAdv_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
        {

            // This event is raised for the header, the footer, separators, and items.

            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
            }
        }
    }
}
