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
    public partial class TourInfoUS : UserControl
    {
        public TourInfo WebpartParent;
        public string CategoryUrl = string.Empty;
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
                    CategoryUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}=",
                       SPContext.Current.Web.Url,
                       Constants.PageInWeb.SubPage,
                       ListsName.English.TourInforCategory,
                       ListsName.English.TourInforRecord,
                       Constants.CategoryId);

                    string companyListQuery = string.Format(@"<Where>
                                                                  <Neq>
                                                                     <FieldRef Name='Status' />
                                                                     <Value Type='Boolean'>1</Value>
                                                                  </Neq>
                                                               </Where>
                                                               <OrderBy>
                                                                  <FieldRef Name='ID' Ascending='False' />
                                                               </OrderBy>");
                    uint newsNumber = 5;

                    if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                    {
                        newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }

                    var companyList = Utilities.GetNewsRecordsCategory(companyListQuery, newsNumber, ListsName.English.TourInforCategory);
                    if (companyList != null && companyList.Rows.Count > 0)
                    {
                        rptTourInfo.DataSource = companyList;
                        rptTourInfo.DataBind();
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
        protected void rptTourInfo_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
            }
        }
    }
}
