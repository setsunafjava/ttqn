using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class OtherNewsUS : UserControl
    {
        public OtherNews WebpartParent;
        public NewsList newslistwp;
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
                    var newsId = Convert.ToInt32(Request.QueryString[Constants.NewsId]);
                    var cateId = Convert.ToInt32(Request.QueryString[Constants.CategoryId]);

                    NewsUrl = string.Format("{0}/{1}.aspx?{2}=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Constants.NewsId);
                    //Bind data to top view
                    DataTable otherNewsTable = null;

                    if (cateId > 0)
                    {
                        Utilities.GetNewsByCatID(Convert.ToString(cateId), ref otherNewsTable);
                    }
                    else if (newsId > 0)
                    {
                        string categoryId = Utilities.GetCategoryIdByItemId(newsId, ListsName.English.NewsRecord);
                        Utilities.GetNewsByCatID(categoryId, ref otherNewsTable);
                    }

                    if (otherNewsTable != null && otherNewsTable.Rows.Count > 0)
                    {
                        DataTable overTenItems = null;
                        if (otherNewsTable.Rows.Count > 10)
                        {
                            overTenItems = otherNewsTable.Clone();
                            if (otherNewsTable.Rows.Count > 15)
                            {
                                for (int i = 10; i < 15; i++)
                                {
                                    overTenItems.ImportRow(otherNewsTable.Rows[i]);
                                }
                            }
                            else
                            {
                                for (int i = 10; i < otherNewsTable.Rows.Count; i++)
                                {
                                    overTenItems.ImportRow(otherNewsTable.Rows[i]);
                                }
                            }
                        }
                        else if (GetCurrentPageName().Contains(Constants.PageInWeb.DetailNews))
                        {
                            overTenItems = otherNewsTable;
                        }

                        rptOtherNews.DataSource = overTenItems;
                        rptOtherNews.DataBind();
                    }
                    else
                    {
                        lblItemsNotFound.Text = "Không tìm thấy thêm bài viết nào thuộc mục này!";
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }


        public string GetCurrentPageName()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            return oInfo.Name;
        }

    }
}
