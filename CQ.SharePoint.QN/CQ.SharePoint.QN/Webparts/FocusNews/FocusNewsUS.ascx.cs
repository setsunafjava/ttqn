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
    public partial class FocusNewsUS : UserControl
    {
        public FocusNews WebpartParent;
        public string NewsUrl = string.Empty;
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
                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                       SPContext.Current.Web.Url,
                       Constants.PageInWeb.DetailNews,
                       ListsName.English.NewsCategory,
                       ListsName.English.NewsRecord,
                       Constants.NewsId);

                    string focusNewsQuery = string.Format(@"<Where>
                                                              <And>
                                                                 <Eq>
                                                                    <FieldRef Name='{0}' />
                                                                    <Value Type='Boolean'>1</Value>
                                                                 </Eq>
                                                                 <And>
                                                                    <Neq>
                                                                       <FieldRef Name='{1}' />
                                                                       <Value Type='Boolean'>1</Value>
                                                                    </Neq>
                                                                    <And>
                                                                       <Lt>
                                                                          <FieldRef Name='ArticleStartDates' />
                                                                          <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                       </Lt>
                                                                       <Contains>
                                                                          <FieldRef Name='Approve' />
                                                                          <Value Type='Lookup'>{3}</Value>
                                                                       </Contains>
                                                                    </And>
                                                                 </And>
                                                              </And>
                                                           </Where><OrderBy>
                                                              <FieldRef Name='ID' Ascending='False' />
                                                           </OrderBy>",
                                                                    FieldsName.NewsRecord.English.FocusNews,
                                                                    FieldsName.NewsRecord.English.Status,
                                                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                    Constants.Published);

                    uint numberOfNews = 5;
                    if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                    {
                        try
                        {
                            numberOfNews = Convert.ToUInt16(WebpartParent.NumberOfNews);
                        }
                        catch (Exception ex)
                        { }
                    }

                    var focusNewsTable = Utilities.GetNewsRecordItems(focusNewsQuery, Convert.ToUInt16(numberOfNews), ListsName.English.NewsRecord);
                    CategoryUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}=1",
                       SPContext.Current.Web.Url,
                       Constants.PageInWeb.SubPage,
                       ListsName.English.NewsCategory,
                       ListsName.English.NewsRecord,
                       "FocusNews");
                    var tableFocus = Utilities.GetTableWithCorrectUrl(ListsName.English.NewsCategory, focusNewsTable);
                    //Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory,FieldsName.NewsRecord.English.CategoryName, ref tableFocus);
                    if (focusNewsTable != null && focusNewsTable.Count > 0)
                    {
                        rptFocusNews.DataSource = tableFocus;
                        rptFocusNews.DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void FocusNews_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    DataRowView drv = (DataRowView)e.Item.DataItem;
            //}
        }
    }
}
