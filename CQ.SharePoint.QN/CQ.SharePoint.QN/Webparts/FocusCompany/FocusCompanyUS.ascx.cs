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
            //            try
            //            {
            //                string query = string.Format(@"<Where>
            //                                                  <And>
            //                                                     <Neq>
            //                                                        <FieldRef Name='Status' />
            //                                                        <Value Type='Boolean'>1</Value>
            //                                                     </Neq>
            //                                                     <Leq>
            //                                                        <FieldRef Name='ArticleStartDates' />
            //                                                        <Value IncludeTimeValue='FALSE' Type='DateTime'>{0}</Value>
            //                                                     </Leq>
            //                                                  </And>
            //                                               </Where>
            //                                               <OrderBy>
            //                                                  <FieldRef Name='ID' Ascending='False' />
            //                                               </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
            //                uint numberOfNews = 5;
            //                if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
            //                {
            //                    try
            //                    {
            //                        numberOfNews = Convert.ToUInt16(WebpartParent.NumberOfNews);
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        numberOfNews = 5;
            //                    }
            //                }
            //                var provinceTable = Utilities.GetNewsRecordItems(query, numberOfNews, ListsName.English.DoanhNghiepTieuBieu);
            //                var correctTable = Utilities.GetTableWithCorrectUrlHotNews(provinceTable);

            //                if (correctTable.Rows.Count > 0)
            //                {
            //                    rptFocusCompany.DataSource = correctTable;
            //                    rptFocusCompany.DataBind();
            //                }
            //            }
            //            catch (Exception ex)
            //            {

            //            }
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
                string link = Convert.ToString(t.Row[9]);
                if (!String.IsNullOrEmpty(link) && link.Length > 3)
                {
                    var extent = link.Substring(link.Length - 3);
                    var imageTemp = (Image)e.Item.FindControl("img");
                    var literalTemp = (Literal)e.Item.FindControl("ltrFlash1");

                    switch (extent)
                    {
                        case "swf":
                            {
                                
                                if (imageTemp != null)
                                    imageTemp.Visible = false;
                                
                                if (literalTemp != null)
                                {
                                    literalTemp.Visible = true;
                                    literalTemp.Text = string.Format("<embed bgcolor=\"#FFFFFF\" height=\"150\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"305\" src=\"{0}\" ></embed>", link);
                                }
                                break;
                            }

                        default:
                            if (imageTemp != null)
                                imageTemp.Visible = true;
                            if (literalTemp != null)
                                literalTemp.Visible = false;
                            break;
                    }
                }
            }
        }
    }
}
