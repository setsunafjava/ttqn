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
    public partial class NewsListUS : UserControl
    {
        public NewsList ParentWP;
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
                    var focusNews = Request.QueryString["FocusNews"];
                    NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews);
                    if (!string.IsNullOrEmpty(categoryId))
                    {

                        if (!"-1".Equals(categoryId))
                        {

                            string categoryQuery =
                                string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE' /><Value Type='LookupMulti'>{1}</Value></Eq></Where>",
                                    FieldsName.NewsRecord.English.CategoryName, categoryId);
                            uint newsNumber = 10;

                            var companyList = Utilities.GetNewsRecords(categoryQuery, newsNumber,
                                                                       ListsName.English.NewsRecord);
                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                rptListCategory.DataSource = companyList;
                                rptListCategory.DataBind();
                            }
                            else
                            {
                                lblItemNotExist.Text = Constants.ErrorMessage.Msg1;
                            }
                        }
                        else
                        {
                            var day = Convert.ToInt32(Request.QueryString["Day"]);
                            var month = Convert.ToInt32(Request.QueryString["Month"]);
                            var year = Convert.ToInt32(Request.QueryString["Year"]);
                            DateTime dt = new DateTime(year, month, day);

                            string categoryQuery = string.Format("<Where><Eq><FieldRef Name='Created' /><Value IncludeTimeValue='FALSE' Type='DateTime'>{0}</Value></Eq></Where>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(dt));
                            uint newsNumber = 10;


                            var companyList = Utilities.GetNewsRecords(categoryQuery, newsNumber, ListsName.English.NewsRecord);
                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                rptListCategory.DataSource = companyList;
                                rptListCategory.DataBind();
                            }
                            else
                            {
                                lblItemNotExist.Text = Constants.ErrorMessage.Msg1;
                            }
                        }
                    }
                    else
                    {
                        if ("1".Equals(focusNews))
                        {
                            string newsQuery = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Boolean'>1</Value></Eq></Where>", FieldsName.NewsRecord.English.FocusNews);
                            uint newsNumber = 10;

                            var companyList = Utilities.GetNewsRecords(newsQuery, newsNumber, ListsName.English.NewsRecord);
                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                rptListCategory.DataSource = companyList;
                                rptListCategory.DataBind();
                            }
                            else
                            {
                                lblItemNotExist.Text = Constants.ErrorMessage.Msg1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {


                }
            }
        }
    }
}
