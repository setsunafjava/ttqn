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
                    NewsUrl = string.Format("{0}/{1}.aspx?{2}=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId);
                    string advQuery = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE' /><Value Type='LookupMulti'>{1}</Value></Eq></Where>", FieldsName.NewsRecord.English.CategoryName, WebpartParent.CategoryId);
                    uint newsNumber = 5;


                    if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                    {
                        newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }

                    var companyList = Utilities.GetNewsRecords(advQuery, newsNumber, ListsName.English.NewsRecord);
                    if (companyList != null && companyList.Rows.Count > 0)
                    {
                        rptShouldYouKnow.DataSource = Utilities.GetTableWithCorrectUrl(companyList);
                        rptShouldYouKnow.DataBind();
                    }

                    CategoryUrl = string.Format("{0}/{1}.aspx?CategoryId=", SPContext.Current.Web.Url, Constants.PageInWeb.SubPage);
                    string newsTitle = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='{2}' Ascending='True' /></OrderBy>", FieldsName.NewsCategory.English.ParentName, WebpartParent.CategoryId, FieldsName.Title);
                    var newsTitleItems = Utilities.GetNewsRecords(newsTitle, 5, ListsName.English.NewsCategory);
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
    }
}
