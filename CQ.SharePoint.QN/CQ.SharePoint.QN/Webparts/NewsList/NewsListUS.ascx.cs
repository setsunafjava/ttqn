using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
    public partial class NewsListUS : UserControl
    {
        public NewsList ParentWP;
        public string NewsUrl = string.Empty;

        /// <summary>
        /// Get all id of category's child
        /// </summary>
        /// <param name="parentId"></param>
        public Dictionary<int, string> GetAllSubCategoryId(int parentId)
        {
            string query = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='Lookup'>{1}</Value></Eq></Where>", FieldsName.NewsCategory.English.ParentName, parentId);
            uint newsNumber = 30;
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            var newCategoryItems = Utilities.GetNewsRecords(query, newsNumber, ListsName.English.NewsCategory);
            if (newCategoryItems != null && newCategoryItems.Rows.Count > 0)
            {
                int j = 0;
                for (int i = 0; i < newCategoryItems.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(newCategoryItems.Rows[i][FieldsName.NewsCategory.English.ParentName])))
                    {
                        dictionary.Add(j, Convert.ToString(newCategoryItems.Rows[i][FieldsName.Id]));
                        j++;
                    }//Xet xem cac chau' cua no co khogn = cach de quy
                }
            }
            return dictionary;
        }

        private static string CreateCamlQuery(Dictionary<int, string> dictionary)
        {
            StringBuilder sb = new StringBuilder();

            if (dictionary.Count == 0)
            {
                // perhaps set a default query?
                AppendEq(sb, "all");
            }

            // "AND" each parameter to the query
            for (int i = 0; i < dictionary.Count; i++)
            {
                AppendEq(sb, dictionary[i]);

                if (i > 0)
                {
                    sb.Insert(0, "<Or>");
                    sb.Append("</Or>");
                }
            }

            sb.Insert(0, "<Where>");
            sb.Append("</Where>");

            return sb.ToString();
        }

        private static void AppendEq(StringBuilder sb, string value)
        {
            // put your field's internal name in place of Category
            sb.Append("<Eq>");
            sb.Append("<FieldRef Name='CategoryName' LookupId='TRUE'/>");
            sb.AppendFormat("<Value Type='LookupMulti'>{0}</Value>", value);
            sb.Append("</Eq>");
        }
        
        protected void RepeaterPaging(DataTable table, Repeater rp)
        {
            PagedDataSource pageds = new PagedDataSource
                                         {
                                             DataSource = table.DefaultView, AllowPaging = true, PageSize = 15
                                         };
            int curpage = 0;
            var pageNum = Request.QueryString["Page"];
            if (string.IsNullOrEmpty(pageNum))
            {
                curpage = Convert.ToInt32(pageNum);
            }
            else
            {
                curpage = 1;
            }
            pageds.CurrentPageIndex = curpage - 1;

            if (!pageds.IsFirstPage)
            {
                lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=" + Convert.ToString(curpage - 1);
            }

            if (!pageds.IsLastPage)
            {
                lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=" + Convert.ToString(curpage + 1);
            }

            rp.DataSource = pageds;
            rp.DataBind();
        }

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
                    var isMenu = Request.QueryString["isMenu"];
                    NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews);
                    if (!string.IsNullOrEmpty(categoryId))
                    {

                        if (!"-1".Equals(categoryId))
                        {

                            string categoryQuery = string.Empty;
                            uint newsNumber = 30;

                            if ("1".Equals(isMenu))
                            {
                                var cateId = Convert.ToInt32(categoryId);
                                categoryQuery = CreateCamlQuery(GetAllSubCategoryId(cateId));
                            }
                            else
                            {
                                categoryQuery = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE' /><Value Type='LookupMulti'>{1}</Value></Eq></Where>",
                                    FieldsName.NewsRecord.English.CategoryName, categoryId);
                            }
                            var companyList = Utilities.GetNewsRecords(categoryQuery, newsNumber, ListsName.English.NewsRecord);



                            if (companyList != null && companyList.Rows.Count > 0)
                            {
                                //rptListCategory.DataSource = companyList;
                                //rptListCategory.DataBind();

                                PagedDataSource pageds = new PagedDataSource
                                {
                                    DataSource = companyList.DefaultView,
                                    AllowPaging = true,
                                    PageSize = 2
                                };
                                int curpage = 0;
                                var pageNum = Request.QueryString["Page"];
                                if (!string.IsNullOrEmpty(pageNum))
                                {
                                    curpage = Convert.ToInt32(pageNum);
                                }
                                else
                                {
                                    curpage = 1;
                                }
                                pageds.CurrentPageIndex = curpage - 1;

                                if (!pageds.IsFirstPage)
                                {
                                    lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=" + Convert.ToString(curpage - 1);
                                }

                                if (!pageds.IsLastPage)
                                {
                                    lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=" + Convert.ToString(curpage + 1);
                                }

                                rptListCategory.DataSource = pageds;
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
