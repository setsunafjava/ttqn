using System;
using System.Web.UI;
using Microsoft.SharePoint;
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
                    var categoryId = Request.QueryString["CategoryId"];

                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                        SPContext.Current.Web.Url,
                        Constants.PageInWeb.DetailNews,
                        ListsName.English.NewsCategory,
                        ListsName.English.NewsRecord,
                        Constants.NewsId);

                    string topNewsQuery = string.Empty;

                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        topNewsQuery = string.Format("<Where><And><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='LookupMulti'>{1}</Value></Eq><Neq><FieldRef Name='Status' /><Value Type='Boolean'>1</Value></Neq></And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>",
                            FieldsName.NewsRecord.English.CategoryName, categoryId);
                    }
                    else
                    {
                        topNewsQuery = string.Format("<Where><Neq><FieldRef Name='Status' /><Value Type='Boolean'>1</Value></Neq></Where><OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>",
                            FieldsName.NewsRecord.English.ViewsCount);
                    }

                    var topViewsTable = Utilities.GetNewsRecords(topNewsQuery, GetNewsNumber(WebpartParent.NumberOfNews), ListsName.English.NewsRecord);
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
