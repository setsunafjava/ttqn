using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class ClassiFiedsUS : UserControl
    {
        public ClassiFieds WebpartParent;
        public string NewsUrl = string.Empty;
        public string CategoryUrl = string.Empty;
        /// <summary>
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                NewsUrl = string.Format("{0}/{1}.aspx?{2}=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId);
                CategoryUrl = string.Format("{0}/{1}.aspx?CategoryId=", SPContext.Current.Web.Url, Constants.PageInWeb.SubPage);
                string companyListQuery = string.Format("<Where><And><Eq><FieldRef Name='{0}' LookupId='TRUE' /><Value Type='Lookup'>{1}</Value></Eq><Neq><FieldRef Name='Status' /><Value Type='Boolean'>1</Value></Neq></And></Where>", FieldsName.NewsRecord.English.CategoryName, WebpartParent.CategoryId);
                uint newsNumber = 5;

                if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                {
                    newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                }

                var companyList = Utilities.GetNewsRecords(companyListQuery, newsNumber, ListsName.English.NewsRecord);
                if (companyList != null && companyList.Rows.Count > 0)
                {
                    rptFocusCompany.DataSource = Utilities.GetTableWithCorrectUrl(companyList);
                    rptFocusCompany.DataBind();
                }
            }
            catch (Exception ex)
            {
            }

        }
    }
}
