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
    public partial class ShouldToKnowUS : UserControl
    {
        public ShouldToKnow WebpartParent;
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
                        ListsName.English.ShouldToKnowCategory,
                        ListsName.English.ShouldToKnowRecord,
                        Constants.NewsId);

                    string advQuery = string.Format(@"<Where>
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
                                                          <FieldRef Name='ID' Ascending='False' />
                                                       </OrderBy>", FieldsName.ShouldToKnowRecord.English.CategoryName,
                                                                    WebpartParent.CategoryId,
                                                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));

                    uint newsNumber = 5;

                    if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                    {
                        newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }

                    var companyList = Utilities.GetNewsRecordItems(advQuery, newsNumber, ListsName.English.ShouldToKnowRecord);
                    if (companyList != null && companyList.Count > 0)
                    {
                        rptShouldYouKnow.DataSource = Utilities.GetTableWithCorrectUrl(ListsName.English.ShouldToKnowCategory, companyList);
                        rptShouldYouKnow.DataBind();
                    }

                    CategoryUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                       SPContext.Current.Web.Url,
                       Constants.PageInWeb.SubPage,
                       ListsName.English.ShouldToKnowCategory,
                       ListsName.English.ShouldToKnowRecord,
                       Constants.CategoryId);

                    string newsTitle = string.Format(@"<Where>
                                                          <Eq>
                                                             <FieldRef Name='{0}' />
                                                             <Value Type='Lookup'>{1}</Value>
                                                          </Eq>
                                                       </Where>", 
                                                                FieldsName.NewsCategory.English.ParentName, 
                                                                WebpartParent.NewsType);

                    var newsTitleItems = Utilities.GetNewsRecordItems(newsTitle, 5, ListsName.English.ShouldToKnowCategory);
                    if (newsTitleItems != null && newsTitleItems.Count > 0)
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
    }
}
