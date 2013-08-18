using System;
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
    public partial class MostViewNewsUS : UserControl
    {
        public MostViewNews WebpartParent;
        public string ItemUrl = string.Empty;
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
                    var categoryId = Request.QueryString[Constants.CategoryId];
                    var listName = Request.QueryString[Constants.ListName];
                    var listCategoryName = Request.QueryString[Constants.ListCategoryName];
                    ItemUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                        SPContext.Current.Web.Url,
                        Constants.PageInWeb.DetailNews,
                        listCategoryName,
                        listName,
                        Constants.NewsId);
                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                                            SPContext.Current.Web.Url,
                                            Constants.PageInWeb.DetailNews,
                                            ListsName.English.NewsCategory,
                                            ListsName.English.NewsRecord,
                                            Constants.NewsId);

                    string topNewsQuery = string.Empty;

                    if (!string.IsNullOrEmpty(categoryId) && !"-1".Equals(categoryId))
                    {
                        //Gia tri luc truoc : <Value Type='CustomLookup'>{1}</Value>
                        topNewsQuery = string.Format(@"<Where>
                                                              <And>
                                                                 <Eq>
                                                                    <FieldRef Name='{0}' LookupId='TRUE' />
                                                                    <Value Type='LookupMulti'>{1}</Value>
                                                                 </Eq>
                                                                 <And>
                                                                    <Neq>
                                                                       <FieldRef Name='Status' />
                                                                       <Value Type='Boolean'>1</Value>
                                                                    </Neq>
                                                                    <And>
                                                                       <Lt>
                                                                          <FieldRef Name='{2}' />
                                                                          <Value IncludeTimeValue='TRUE' Type='DateTime'>{3}</Value>
                                                                       </Lt>
                                                                       <Eq>
                                                                          <FieldRef Name='{4}' />
                                                                          <Value Type='Number'>{5}</Value>
                                                                       </Eq>
                                                                    </And>
                                                                 </And>
                                                              </And>
                                                           </Where>
                                                           <OrderBy>
                                                              <FieldRef Name='ViewsCount' Ascending='False' />
                                                           </OrderBy>",
                                                                      FieldsName.NewsRecord.English.CategoryName,
                                                                      categoryId,
                                                                      FieldsName.ArticleStartDates,
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                      FieldsName.ModerationStatus,
                                                                      Utilities.GetModerationStatus(402));
                    }
                    else
                    {
                        topNewsQuery = string.Format(@"<Where>
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
                                                                   <Value Type='Number'>{3}</Value>
                                                                </Eq>
                                                             </And>
                                                          </And>
                                                       </Where>
                                                       <OrderBy>
                                                          <FieldRef Name='ViewsCount' Ascending='False' />
                                                       </OrderBy>", 
                                                                  FieldsName.ArticleStartDates,
                                                                  SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                  FieldsName.ModerationStatus,
                                                                  Utilities.GetModerationStatus(402));
                    }
                    
                    var topViewsTable = Utilities.GetNewsRecords(topNewsQuery, GetNewsNumber(WebpartParent.NumberOfNews), listName);
                    if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                    {
                        rptTopViews.DataSource = topViewsTable;
                        rptTopViews.DataBind();
                    }
                    else
                    {
                        topNewsQuery = string.Format(@"<Where>
                                                          <And>
                                                             <Neq>
                                                                <FieldRef Name='Status' />
                                                                <Value Type='Boolean'>1</Value>
                                                             </Neq>
                                                             <Lt>
                                                                <FieldRef Name='ArticleStartDates' />
                                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                             </Lt>
                                                          </And>
                                                       </Where>
                                                       <OrderBy>
                                                          <FieldRef Name='ViewsCount' Ascending='False' />
                                                       </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
                        topViewsTable = Utilities.GetNewsRecords(topNewsQuery, GetNewsNumber(WebpartParent.NumberOfNews), listName);
                        ItemUrl = NewsUrl;
                        if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                        {
                            rptTopViews.DataSource = topViewsTable;
                            rptTopViews.DataBind();
                        }
                        else
                        {
                            lblItemsNotFound.Text = "Không tìm thấy thêm bài viết nào thuộc mục này!";
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Utilities.LogToUls(ex);
                }
            }
        }

        /// <summary>
        /// Get news number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private uint GetNewsNumber(string value)
        {
            uint newsNumber = 5;
            try
            {
                newsNumber = Convert.ToUInt16(value);
            }
            catch (Exception)
            {
                return 5;
            }
            return newsNumber;
        }
    }
}
