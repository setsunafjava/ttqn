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
                        topNewsQuery = string.Format(@"<Where>
                                                              <And>
                                                                 <Eq>
                                                                    <FieldRef Name='{0}' LookupId='TRUE' />
                                                                    <Value Type='Lookup'>{1}</Value>
                                                                 </Eq>
                                                                 <And>
                                                                    <Neq>
                                                                       <FieldRef Name='Status' />
                                                                       <Value Type='Boolean'>1</Value>
                                                                    </Neq>
                                                                    <Lt>
                                                                       <FieldRef Name='ArticleStartDate' />
                                                                       <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                    </Lt>
                                                                 </And>
                                                              </And>
                                                           </Where>
                                                           <OrderBy>
                                                              <FieldRef Name='ViewsCount' Ascending='False' />
                                                           </OrderBy>",
                                                                      FieldsName.NewsRecord.English.CategoryName,
                                                                      categoryId,
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
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
                                                                <FieldRef Name='ArticleStartDate' />
                                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                             </Lt>
                                                          </And>
                                                       </Where>
                                                       <OrderBy>
                                                          <FieldRef Name='ViewsCount' Ascending='False' />
                                                       </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
                    }
                    
                    var topViewsTable = Utilities.GetNewsRecords(topNewsQuery, GetNewsNumber(WebpartParent.NumberOfNews), listName);
                    Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.NewsRecord.English.CategoryName, ref topViewsTable);
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
                                                                <FieldRef Name='ArticleStartDate' />
                                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                             </Lt>
                                                          </And>
                                                       </Where>
                                                       <OrderBy>
                                                          <FieldRef Name='ViewsCount' Ascending='False' />
                                                       </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
                        topViewsTable = Utilities.GetNewsRecords(topNewsQuery, GetNewsNumber(WebpartParent.NumberOfNews), listName);
                        ItemUrl = NewsUrl;
                        Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.NewsRecord.English.CategoryName, ref topViewsTable);
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
