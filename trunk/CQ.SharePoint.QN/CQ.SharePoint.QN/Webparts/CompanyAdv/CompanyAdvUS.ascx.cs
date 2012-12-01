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
                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                        SPContext.Current.Web.Url,
                        Constants.PageInWeb.DetailNews,
                        ListsName.English.CompanyCategory,
                        ListsName.English.CompanyRecord,
                        Constants.NewsId);

                    //string companyListQuery = string.Format("<Where><And><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq><Lt><FieldRef Name='ArticleStartDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value></Lt></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>",
                    //    FieldsName.CompanyRecord.English.CategoryName,
                    //    WebpartParent.CompanyType,
                    //    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));

                    string companyListQuery = string.Format(@"<Where>
                                                                  <And>
                                                                     <Neq>
                                                                        <FieldRef Name='Status' />
                                                                        <Value Type='Boolean'>1</Value>
                                                                     </Neq>
                                                                     <And>
                                                                        <Eq>
                                                                           <FieldRef Name='Approve' />
                                                                           <Value Type='Lookup'>{0}</Value>
                                                                        </Eq>
                                                                        <And>
                                                                           <Eq>
                                                                              <FieldRef Name='CategoryName' />
                                                                              <Value Type='Lookup'>{1}</Value>
                                                                           </Eq>
                                                                           <And>
                                                                              <Geq>
                                                                                 <FieldRef Name='_EndDate' />
                                                                                 <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                              </Geq>
                                                                              <Leq>
                                                                                 <FieldRef Name='ArticleStartDate' />
                                                                                 <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                              </Leq>
                                                                           </And>
                                                                        </And>
                                                                     </And>
                                                                  </And>
                                                               </Where>", Constants.Published, WebpartParent.CompanyType, SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));

                    uint newsNumber = 5;

                    if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                    {
                        newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }

                    var companyList = Utilities.GetNewsRecords(companyListQuery, newsNumber, ListsName.English.CompanyRecord);
                    //Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.NewsRecord.English.CategoryName, ref companyList);
                    if (companyList != null && companyList.Rows.Count > 0)
                    {
                        rptCompanyAdv.DataSource = companyList;
                        rptCompanyAdv.DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }
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
