using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using Microsoft.SharePoint.WebPartPages;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class LeftNewsContentUS : UserControl
    {
        public LeftNewsContent WebpartParent;
        public string NewsUrl = string.Empty;
        public string CategoryUrl = string.Empty;
        public string NewsFirstUrl1 = string.Empty;

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

                    string newsGroupQuery = string.Format(@"<Where>
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
                                                                    <And>
                                                                       <Lt>
                                                                          <FieldRef Name='ArticleStartDate' />
                                                                          <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                       </Lt>
                                                                       <Contains>
                                                                          <FieldRef Name='Approve' />
                                                                          <Value Type='Lookup'>{3}</Value>
                                                                       </Contains>
                                                                    </And>
                                                                 </And>
                                                              </And>
                                                           </Where>
                                                           <OrderBy>
                                                              <FieldRef Name='ID' Ascending='False' />
                                                           </OrderBy>",
                                                                      FieldsName.NewsRecord.English.CategoryName,
                                                                      SPHttpUtility.HtmlEncode(WebpartParent.NewsGroupID),
                                                                      SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                      Constants.Approved);

                    var newsGroups = Utilities.GetNewsRecordItems(newsGroupQuery, Convert.ToUInt16(WebpartParent.NumberOfNews), ListsName.English.NewsRecord);
                    if (newsGroups != null && newsGroups.Count > 0)
                    {
                        var tempTable = Utilities.GetTableWithCorrectUrl(ListsName.English.NewsCategory, newsGroups);
                        lblHeader.Text = Convert.ToString(tempTable.Rows[0][FieldsName.Title]);
                        imgThumb.ImageUrl = Convert.ToString(tempTable.Rows[0][FieldsName.NewsRecord.English.ThumbnailImage]);

                        lblShortContent.Text = Convert.ToString(tempTable.Rows[0][FieldsName.NewsRecord.English.ShortContent]);

                        Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.NewsRecord.English.CategoryName, ref tempTable);
                        if (tempTable.Rows.Count > 2)
                        {

                            tempTable.Rows.RemoveAt(0);

                            rptCaiCachThuTucHanhChinh.DataSource = tempTable;
                            NewsFirstUrl1 = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}={5}&CategoryId={6}",
                                   SPContext.Current.Web.Url,
                                   Constants.PageInWeb.DetailNews,
                                   ListsName.English.NewsCategory,
                                   ListsName.English.NewsRecord,
                                   Constants.NewsId,
                                   Convert.ToString(tempTable.Rows[0][FieldsName.Id]),
                                   Convert.ToString(tempTable.Rows[0][Constants.CategoryId]));

                            rptCaiCachThuTucHanhChinh.DataBind();
                        }
                    }
                    CategoryUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}=",
                       SPContext.Current.Web.Url,
                       Constants.PageInWeb.SubPage,
                       ListsName.English.NewsCategory,
                       ListsName.English.NewsRecord,
                       Constants.CategoryId);

                    string newsTitle = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='{2}' Ascending='True' /></OrderBy>",
                        FieldsName.NewsCategory.English.ParentName,
                        WebpartParent.GroupName,
                        FieldsName.Id);

                    var newsTitleItems = Utilities.GetNewsRecords(newsTitle, 4, ListsName.English.NewsCategory);
                    if (newsTitleItems != null && newsTitleItems.Rows.Count > 0)
                    {
                        rptNewsGroup.DataSource = newsTitleItems;
                        rptNewsGroup.DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        /// <summary>
        /// OnpreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            WebPartManager webPartManager = WebPartManager.GetCurrentWebPartManager(Page);
        }

        protected void lbRSS_OnClick(object sender, EventArgs e)
        {
        }
    }
}
