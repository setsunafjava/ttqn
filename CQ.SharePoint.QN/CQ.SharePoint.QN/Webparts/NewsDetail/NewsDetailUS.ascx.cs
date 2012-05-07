using System;
using System.Collections.Generic;
using System.Data;
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
        public DataTable attachMentFiles = null;
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
                    var newsId = Request.QueryString[Constants.NewsId];
                    if (!string.IsNullOrEmpty(newsId))
                    {
                        string newsQuery = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Counter'>{1}</Value></Eq></Where>", FieldsName.Id, newsId);
                        var newsItem = Utilities.GetNewsRecords(newsQuery, 1, ListsName.English.NewsRecord);
                        if (newsItem != null)
                        {
                            string categoryName = Convert.ToString(newsItem.Rows[0][FieldsName.NewsRecord.English.CategoryName]);
                            ltrNewsContent.Text = Convert.ToString(newsItem.Rows[0][FieldsName.NewsRecord.English.Content]);
                            lblCurrentDate.Text = Convert.ToString(newsItem.Rows[0][FieldsName.Modified]);

                            attachMentFiles = new DataTable();
                            attachMentFiles.Columns.Add("key", typeof(string));
                            attachMentFiles.Columns.Add("value", typeof(string));

                            //Update viewcount
                            SPSecurity.RunWithElevatedPrivileges(() =>
                            {
                                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                                {
                                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                                    {
                                        try
                                        {
                                            string listUrl = web.Url + "/Lists/" + ListsName.English.NewsRecord;
                                            var result = web.GetList(listUrl);
                                            int id = Convert.ToInt32(newsId);
                                            SPListItem items = result.GetItemById(id);
                                            if (items != null)
                                            {
                                                string viewcount = Convert.ToString(items[FieldsName.NewsRecord.English.ViewsCount]);
                                                if (!string.IsNullOrEmpty(viewcount))
                                                {
                                                    int count = Convert.ToInt32(viewcount);
                                                    items[FieldsName.NewsRecord.English.ViewsCount] = ++count;
                                                    web.AllowUnsafeUpdates = true;
                                                    items.Update();
                                                }
                                                //Get attachment file
                                                if (items.Attachments.Count > 0)
                                                {
                                                    foreach (var attachment in items.Attachments)
                                                    {
                                                        attachMentFiles.Rows.Add(Convert.ToString(attachment), items.Attachments.UrlPrefix);
                                                    }
                                                }

                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Utilities.LogToUls(ex);
                                        }
                                    }
                                }
                            });

                            if (attachMentFiles.Rows.Count > 0)
                            {
                                rptAttachment.DataSource = attachMentFiles;
                                rptAttachment.DataBind();
                            }

                            //end update
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
