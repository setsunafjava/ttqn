using System;
using System.Data;
using System.Web.UI;
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
//                string imagepath;
//                string focusCompanyQuery = string.Format(@"<Where>
//                                                              <And>
//                                                                 <Eq>
//                                                                    <FieldRef Name='{0}' />
//                                                                    <Value Type='Boolean'>1</Value>
//                                                                 </Eq>
//                                                                 <And>
//                                                                    <Neq>
//                                                                       <FieldRef Name='{1}' />
//                                                                       <Value Type='Boolean'>1</Value>
//                                                                    </Neq>
//                                                                    <And>
//                                                                       <Lt>
//                                                                          <FieldRef Name='ArticleStartDate' />
//                                                                          <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
//                                                                       </Lt>
//                                                                       <Contains>
//                                                                          <FieldRef Name='Approve' />
//                                                                          <Value Type='Lookup'>{3}</Value>
//                                                                       </Contains>
//                                                                    </And>
//                                                                 </And>
//                                                              </And>
//                                                           </Where><OrderBy>
//                                                              <FieldRef Name='ID' Ascending='False' />
//                                                           </OrderBy>",
//                                                                   FieldsName.CompanyRecord.English.FocusCompany,
//                                                                   FieldsName.NewsRecord.English.Status,
//                                                                   SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
//                                                                   Constants.Published);

//                uint numberOfNews = 5;
//                if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
//                {
//                    try
//                    {
//                        numberOfNews = Convert.ToUInt16(WebpartParent.NumberOfNews);
//                    }
//                    catch (Exception ex)
//                    { }
//                }
                
//                var focusNewsTable = Utilities.GetNewsRecordItems(focusCompanyQuery, Convert.ToUInt16(numberOfNews), ListsName.English.CompanyRecord);
//                var companyList = Utilities.GetTableWithCorrectUrlHotNews(ListsName.English.CompanyCategory, focusNewsTable);

//                if (companyList.Rows.Count > 0)
//                {
//                    companyList.Columns.Add("LinkToItem", typeof(String));
//                    for (int i = 0; i < companyList.Rows.Count; i++)
//                    {
//                        imagepath = Convert.ToString(companyList.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage]);
//                        if (imagepath.Length > 2)
//                            companyList.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length);

//                        companyList.Rows[i]["LinkToItem"] = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}={5}&{6}={7}",
//                                                               SPContext.Current.Web.Url,
//                                                               Constants.PageInWeb.DetailNews,
//                                                               ListsName.English.CompanyCategory,
//                                                               ListsName.English.CompanyRecord,
//                                                               Constants.NewsId,
//                                                               Convert.ToString(companyList.Rows[i]["ID"]),
//                                                               FieldsName.CategoryId,
//                                                               Convert.ToString(companyList.Rows[i]["CategoryId"]));
//                    }
//                    rptFocusCompany.DataSource = companyList;
//                    rptFocusCompany.DataBind();
//                }

                string query = string.Format(@"<Where>
                                                  <And>
                                                     <Neq>
                                                        <FieldRef Name='Status' />
                                                        <Value Type='Boolean'>1</Value>
                                                     </Neq>
                                                     <Leq>
                                                        <FieldRef Name='ArticleStartDates' />
                                                        <Value IncludeTimeValue='FALSE' Type='DateTime'>{0}</Value>
                                                     </Leq>
                                                  </And>
                                               </Where>
                                               <OrderBy>
                                                  <FieldRef Name='ID' Ascending='False' />
                                               </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
                uint numberOfNews = 5;
                if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                {
                    try
                    {
                        numberOfNews = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }
                    catch (Exception ex)
                    {
                        numberOfNews = 5;
                    }
                }
                var provinceTable = Utilities.GetNewsRecordItems(query, numberOfNews, ListsName.English.DoanhNghiepTieuBieu);
                var correctTable = Utilities.GetTableWithCorrectUrlHotNews(provinceTable);

                if (correctTable.Rows.Count > 0)
                {
                    rptFocusCompany.DataSource = correctTable;
                    rptFocusCompany.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
