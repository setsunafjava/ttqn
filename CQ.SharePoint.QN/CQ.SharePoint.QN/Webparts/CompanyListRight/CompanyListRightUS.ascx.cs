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
    public partial class CompanyListRightUS : UserControl
    {
        public CompanyListRight WebpartParent;
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
                    NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews);
                    string companyListQuery = GetQuery(WebpartParent.CompanyType);
                    uint newsNumber = 5;

                    if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                    {
                        newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }

                    var companyList = Utilities.GetNewsRecords(companyListQuery, newsNumber, ListsName.English.CompanyRecord);
                    lblcompanyTypeTitle.Text = WebpartParent.CompanyType;
                    if (companyList != null && companyList.Rows.Count > 0)
                    {
                        rptCompanyList.DataSource = companyList;
                        rptCompanyList.DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// companytype = 1 =>doanh nghiệp mới thành lập 
        /// companytype = 2 =>doanh nghiệp thay đổi thông tin  
        /// companytype = 3 =>doanh nghiệp Giải thể  
        /// </summary>
        /// <param name="companytype"></param>
        /// <returns></returns>
        protected string GetQuery(string companytype)
        {
            string query = string.Empty;
            switch (companytype)
            {
                case "1":
                    query = string.Format("<Where><And><Neq><FieldRef Name='{0}' /><Value Type='Boolean'>1</Value></Neq><Neq><FieldRef Name='{1}' /><Value Type='Boolean'>1</Value></Neq></And></Where>", FieldsName.CompanyRecord.English.ChangeInformation, FieldsName.CompanyRecord.English.Dissolved);
                    break;
                case "2":
                    query = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Boolean'>1</Value></Eq></Where>", FieldsName.CompanyRecord.English.ChangeInformation);
                    break;
                case "3":
                    query = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Boolean'>1</Value></Eq></Where>", FieldsName.CompanyRecord.English.Dissolved);
                    break;
            }
            return query;
        }
    }
}
