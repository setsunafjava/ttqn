using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using Microsoft.SharePoint.Utilities;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// ProvinceInfoUS
    /// </summary>
    public partial class ProvinceInfoUS : UserControl
    {
        public ProvinceInfo WebpartParent;
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
                        ListsName.English.ProvinceInfoCategory,
                        ListsName.English.ProvinceInfoRecord,
                        Constants.NewsId);

//                    string companyListQuery = string.Format(@"<Where>
//                                                                  <And>
//                                                                     <Eq>
//                                                                        <FieldRef Name='{0}' />
//                                                                        <Value Type='CustomLookup'>{1}</Value>
//                                                                     </Eq>
//                                                                     <And>
//                                                                        <Neq>
//                                                                           <FieldRef Name='Status' />
//                                                                           <Value Type='Boolean'>1</Value>
//                                                                        </Neq>
//                                                                        <And>
//                                                                            <Lt>
//                                                                               <FieldRef Name='{2}' />
//                                                                               <Value IncludeTimeValue='TRUE' Type='DateTime'>{3}</Value>
//                                                                            </Lt>
//                                                                            <Eq>
//                                                                               <FieldRef Name='{4}' />
//                                                                               <Value Type='ModStat'>{5}</Value>
//                                                                            </Eq>
//                                                                         </And>
//                                                                     </And>
//                                                                  </And>
//                                                               </Where>",
//                                                                        FieldsName.ProvinceInfoRecord.English.CategoryName,
//                                                                        WebpartParent.NewsType,
//                                                                        FieldsName.ArticleStartDates,
//                                                                        SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
//                                                                        FieldsName.ModerationStatus,
//                                                                        Utilities.GetModerationStatus(402));

                    string companyListQuery = string.Format(@"<Where>
                                                                  <And>
                                                                    <Neq>
                                                                       <FieldRef Name='Status' />
                                                                       <Value Type='Boolean'>1</Value>
                                                                    </Neq>
                                                                    <And>
                                                                        <Lt>
                                                                           <FieldRef Name='{0}' />
                                                                           <Value IncludeTimeValue='TRUE' Type='DateTime'>{1}</Value>
                                                                        </Lt>
                                                                        <Eq>
                                                                           <FieldRef Name='{2}' />
                                                                           <Value Type='ModStat'>{3}</Value>
                                                                        </Eq>
                                                                     </And>
                                                                 </And>
                                                               </Where><OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>",
                                                                        FieldsName.ArticleStartDates,
                                                                        SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                        FieldsName.ModerationStatus,
                                                                        Utilities.GetModerationStatus(402));

                    uint newsNumber = 5;
                    if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                    {
                        newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }

                    var companyList = Utilities.GetNewsRecords(companyListQuery, newsNumber, ListsName.English.ProvinceInfoRecord);
                    if (companyList != null && companyList.Rows.Count > 0)
                    {
                        //Utilities.AddCategoryIdToTable(ListsName.English.ProvinceInfoCategory, FieldsName.CategoryName, ref companyList);

                        rptProvinceInfo.DataSource = companyList;
                        rptProvinceInfo.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Utilities.LogToUls(ex);
                }
            }
        }

        /// <summary>
        /// rptMenu_OnItemDataBound
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        protected void rptProvinceInfo_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
            }
        }
    }
}
