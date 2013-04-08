using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
    public partial class ShowDownloadUS : UserControl
    {
        public ShowDownload ParentWP;
        public string NewsUrl = string.Empty;
        ///// <summary>
        ///// Page on Load
        ///// </summary>
        ///// <param name="sender">Object sender</param>
        ///// <param name="e">EventArgs e</param>
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        //NewsUrl = string.Format("{0}/{1}.aspx?{2}=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId);
        //        //var id = Request.QueryString["id"];
        //        //string camlQuery = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='LookupMulti'>{1}</Value></Eq></Where>", FieldsName.NewsRecord.English.CategoryName, id);

        //        //var downloadTable = Utilities.GetNewsRecords(camlQuery, 20, ListsName.English.NewsRecord);
        //        //rptDownload.DataSource = downloadTable;
        //        //rptDownload.DataBind();

        //        //string query = "<OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
        //        //rptDownload.DataSource = Utilities.GetDocLibRecords(query, ListsName.English.DownloadList);
        //        //rptDownload.DataBind();
        //    }
        //}

        protected void rptSearch_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
        {

            // This event is raised for the header, the footer, separators, and items.

            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                var imgLogo = (Image) e.Item.FindControl("imgLogo");
                if (imgLogo != null && string.IsNullOrEmpty(Convert.ToString(drv["Thumbnail"])))
                {
                    //imgLogo.Visible = false;
                }
            }
        }

        protected string BuildUrl(string pageorder)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var allkeys = Request.QueryString.AllKeys;
            for (int i = 0; i < allkeys.Length; i++)
            {
                if ("Page".Equals(allkeys[i]))
                {
                    stringBuilder.Append(string.Format("{0}={1}&", "Page", pageorder));
                }
                else
                {
                    stringBuilder.Append(string.Format("{0}={1}&", allkeys[i], Request.QueryString[allkeys[i]]));
                }
            }

            return stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1);
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
                    //var categoryId = Request.QueryString["CategoryId"];
                    var keyWord = Request.QueryString["KeyWord"];

                    //NewsUrl = string.Format("{0}/{1}.aspx?{2}=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId);
                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName=",
                                            SPContext.Current.Web.Url,
                                            Constants.PageInWeb.DetailNews);
                       //,
                       //ListsName.English.NewsCategory,
                       //ListsName.English.NewsRecord,
                       //Constants.NewsId);
                    DataTable companyList = Utilities.SearchNews(keyWord);
                    var companyListTemp = Utilities.GetTableWithCorrectUrl(companyList);
                    //Utilities.AddCategoryIdToTable(ListsName.English.NewsCategory, FieldsName.CategoryName, ref companyListTemp);
                    string imagepath;
                    if (companyList.Rows.Count > 0)
                    {
                        //for (int i = 0; i < companyList.Rows.Count; i++)
                        //{
                        //    imagepath = Convert.ToString(companyList.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage]);
                        //    if (imagepath.Length > 2)
                        //        companyList.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length - 2);
                        //}

                        PagedDataSource pageds = new PagedDataSource
                        {
                            DataSource = companyListTemp.DefaultView,
                            AllowPaging = true,
                            PageSize = 10
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
                        lblCurrpage.Text = "Trang: " + curpage;

                        if (!pageds.IsFirstPage)
                        {
                            lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage - 1));
                        }

                        if (!pageds.IsLastPage)
                        {
                            lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?" + BuildUrl(Convert.ToString(curpage + 1));
                        }

                        rptListCategory.DataSource = pageds;
                        rptListCategory.DataBind();
                    }
                    else
                    {
                        lblItemNotExist.Text = Constants.ErrorMessage.Msg1;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
