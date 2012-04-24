using System;
using System.Text;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class NewsDetailUS : UserControl
    {
        public NewsDetail ParentWP;
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
                    var newsId = Request.QueryString["NewsID"];
                    if (!string.IsNullOrEmpty(newsId))
                    {
                        string newsQuery = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Counter'>{1}</Value></Eq></Where>", FieldsName.Id, newsId);
                        var newsItem = Utilities.GetNewsRecords(newsQuery, 1, ListsName.English.NewsRecord);
                        if (newsItem != null)
                        {
                            string categoryName = Convert.ToString(newsItem.Rows[0][FieldsName.NewsRecord.English.CategoryName]);
                            ltrNewsContent.Text = Convert.ToString(newsItem.Rows[0][FieldsName.NewsRecord.English.Content]);
                            lblCurrentDate.Text = Convert.ToString(newsItem.Rows[0][FieldsName.Modified]);


                            newsQuery = string.Format("<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{0}</Value></Eq></Where>", categoryName);
                            var categoryItem = Utilities.GetNewsRecords(newsQuery, 1, ListsName.English.NewsCategory);

                            if (categoryItem != null && categoryItem.Rows.Count > 0)
                            {
                                string parentCategory = Convert.ToString(categoryItem.Rows[0][FieldsName.NewsCategory.English.TypeCategory]);
                                parentCategory = parentCategory.Replace(";#", "");
                                lblBreadCrum.Text = string.Format("{0} &nbsp; &gt;&gt;&nbsp; &nbsp; {1}", parentCategory, categoryName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {


                }

            }
        }

        public string BuilBreadcrumb(string categoryName)
        {
            string result = string.Empty;

            string newsQuery = string.Format("<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{0}</Value></Eq></Where>", categoryName);
            var categoryItem = Utilities.GetNewsRecords(newsQuery, 1, ListsName.English.NewsCategory);

            if (categoryItem != null && categoryItem.Rows.Count > 0)
            {
                string parentName = Convert.ToString(categoryItem.Rows[0][FieldsName.NewsCategory.English.ParentName]);

                if (string.IsNullOrEmpty(parentName))
                {
                    string parentCategory = Convert.ToString(categoryItem.Rows[0][FieldsName.NewsCategory.English.TypeCategory]);
                    parentCategory = parentCategory.Replace(";#", "");
                    result = string.Format("{0} &nbsp; &gt;&gt;&nbsp; &nbsp; {1}", parentCategory, categoryName);
                }
                else
                {
                    
                }


            }

            return result;
        }
    }
}
