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
                pnlOtherNews.Visible = true;
                try
                {//cai nay chua lay duoc du lieu khi ma user xem 1 ban ghi => khog get duoc categoryID
                    var newsId = Convert.ToInt32(Request.QueryString[Constants.NewsId]);
                    var cateId = Convert.ToInt32(Request.QueryString[Constants.CategoryId]);
                    var listName = Request.QueryString[Constants.ListName];
                    var listCategoryName = Request.QueryString[Constants.ListCategoryName];
                    var chuyende = Request.QueryString["chuyende"];
                    if (string.IsNullOrEmpty(chuyende))
                    {
                        NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                                                SPContext.Current.Web.Url,
                                                Constants.PageInWeb.DetailNews,
                                                listCategoryName,
                                                listName,
                                                Constants.NewsId);
                        //Bind data to top view
                        DataTable otherNewsTable = null;

                        if (cateId > 0)
                        {
                            Utilities.GetNewsByCatID(listName, listCategoryName, Convert.ToString(cateId),
                                                     ref otherNewsTable);
                        }
                        else if (newsId > 0)
                        {
                            string categoryId = Utilities.GetCategoryIdByItemId(newsId, listName);
                            Utilities.GetNewsByCatID(listName, listCategoryName, categoryId, ref otherNewsTable);
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
                            if (overTenItems != null && overTenItems.Rows.Count > 0)
                            {
                                Utilities.AddCategoryIdToTable(listCategoryName, FieldsName.CategoryName,
                                                               ref overTenItems);
                                rptOtherNews.DataSource = overTenItems;
                                rptOtherNews.DataBind();
                            }
                        }
                        else
                        {
                            lblItemsNotFound.Text = "Không tìm thấy thêm bài viết nào thuộc mục này!";
                        }
                    }
                    else //if chuyende != null
                    {
                        pnlOtherNews.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    Utilities.LogToUls(ex);
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
