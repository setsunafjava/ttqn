using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
    public partial class NewsDetailUS : UserControl
    {
        public NewsDetail ParentWP;
        public DataTable attachMentFiles = null;
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
                    var newsId = Request.QueryString[Constants.NewsId];
                    var listName = Request.QueryString[Constants.ListName];
                    var listCategoryName = Request.QueryString[Constants.ListCategoryName];
                    var chuyende = Request.QueryString["chuyende"];
                    var catId = Request.QueryString["CategoryId"];


                    if (string.IsNullOrEmpty(chuyende))
                    {
                        pnlNewsDetail.Visible = true;
                        pnlChuyenDe.Visible = false;
                        if (!string.IsNullOrEmpty(newsId) && !string.IsNullOrEmpty(listName))
                        {
                            string newsQuery =
                                string.Format(
                                    @"<Where>
                                                              <And>
                                                                 <Eq>
                                                                    <FieldRef Name='ID' />
                                                                    <Value Type='Counter'>{0}</Value>
                                                                 </Eq>
                                                                 <And>
                                                                    <Neq>
                                                                       <FieldRef Name='Status' />
                                                                       <Value Type='Boolean'>1</Value>
                                                                    </Neq>
                                                                    <And>
                                                                        <Lt>
                                                                           <FieldRef Name='{1}' />
                                                                           <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                                        </Lt>
                                                                        <Contains>
                                                                           <FieldRef Name='{3}' />
                                                                           <Value Type='ModStat'>{4}</Value>
                                                                        </Contains>
                                                                     </And>
                                                                 </And>
                                                              </And>
                                                           </Where>",
                                                                    newsId,
                                                                    FieldsName.ArticleStartDates,
                                                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                    FieldsName.ModerationStatus,
                                                                    Utilities.GetModerationStatus(402));
                            var newsItem = Utilities.GetNewsRecords(newsQuery, 1, listName);
                            if (newsItem != null && newsItem.Rows.Count > 0)
                            {
                                string categoryName = Convert.ToString(newsItem.Rows[0][FieldsName.NewsRecord.English.CategoryName]);
                                ltrNewsContent.Text = Convert.ToString(newsItem.Rows[0][FieldsName.NewsRecord.English.PublishingPageContent]);

                                lblAuthor.Text = Convert.ToString(newsItem.Rows[0]["ArticleByLine"]);
                                string source = Convert.ToString(newsItem.Rows[0]["Source"]);
                                if (!string.IsNullOrEmpty(source))
                                {
                                    lblSource.Text = string.Format("(Nguồn: {0})", source);
                                }

                                lblTitle.Text = Convert.ToString(newsItem.Rows[0][FieldsName.Title]);
                                ltrShortDescription.Text = Convert.ToString(newsItem.Rows[0][FieldsName.NewsRecord.English.ShortContent]);
                                DateTime dateTime = Convert.ToDateTime(newsItem.Rows[0][FieldsName.ArticleStartDates]);
                                lblCreatedDate.Text = string.Format("{0}/{1}/{2} {3}:{4}", dateTime.Day, dateTime.Month, dateTime.Year, dateTime.Hour, dateTime.Minute);

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
                                             string listUrl = web.Url + "/Lists/" + listName;
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
                                                 else
                                                 {
                                                     items[FieldsName.NewsRecord.English.ViewsCount] = 1;
                                                     web.AllowUnsafeUpdates = true;
                                                     items.Update();
                                                 }
                                                 //Get attachment file
                                                 if (items.Attachments.Count > 0)
                                                 {
                                                     foreach (var attachment in items.Attachments)
                                                     {
                                                         attachMentFiles.Rows.Add(Convert.ToString(attachment), string.Format("{0}{1}",
                                                                         items.Attachments.UrlPrefix.Replace("qni-wsus", "news.qnp.vn"), Convert.ToString(attachment)));
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
                                    pnlAttachment.Visible = true;
                                    rptAttachment.DataSource = attachMentFiles;
                                    rptAttachment.DataBind();
                                }
                                else
                                {
                                    pnlAttachment.Visible = false;
                                }

                                //end update

                                if (!string.IsNullOrEmpty(catId))
                                {
                                    int catIdValue = Convert.ToInt16(catId);
                                    SPList list = Utilities.GetListFromUrl(SPContext.Current.Web, listCategoryName);
                                    var item = list.GetItemById(catIdValue);
                                    if (item != null)
                                    {
                                        string parentCategory = Convert.ToString(item[FieldsName.NewsCategory.English.ParentName]);
                                        parentCategory = parentCategory.Substring(parentCategory.IndexOf("#") + 1);
                                        lblBreadCrum.Text = string.Format("{0} &nbsp; &gt;&gt;&nbsp; &nbsp; {1}", parentCategory, categoryName.Substring(categoryName.IndexOf("#") + 1));
                                    }
                                }
                                else
                                {
                                    //Create breadcrumb
                                    if (categoryName != null && !string.IsNullOrEmpty(categoryName))
                                    {
                                        newsQuery = string.Format("<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{0}</Value></Eq></Where>", categoryName.Substring(categoryName.IndexOf("#") + 1));
                                        var categoryItem = Utilities.GetNewsRecords(newsQuery, 1, listCategoryName);

                                        if (categoryItem != null && categoryItem.Rows.Count > 0)
                                        {
                                            string parentCategory = Convert.ToString(categoryItem.Rows[0][FieldsName.NewsCategory.English.ParentName]);
                                            parentCategory = parentCategory.Replace(";#", string.Empty);
                                            lblBreadCrum.Text = string.Format("{0} &nbsp; &gt;&gt;&nbsp; &nbsp; {1}", parentCategory, categoryName.Substring(categoryName.IndexOf("#") + 1));
                                        }
                                    }
                                }

                            }
                            else
                            {
                                lblCreatedDate.Visible = false;
                                lblAttachFiles.Visible = false;
                                lblItemDoesNotExist.Visible = true;
                                lblItemDoesNotExist.Text = "Bản ghi không tồn tại hoặc đã bị xóa!";
                            }
                        }
                    }
                    else //Code chuyen de
                    {
                        pnlNewsDetail.Visible = false;
                        pnlChuyenDe.Visible = true;
                        lblBreadCrum.Text = "Danh sách các chuyên đề";
                        string newsQuery = string.Format(@"<Where>
                                                              <Eq>
                                                                 <FieldRef Name='ParentName' LookupId='TRUE'/>
                                                                 <Value Type='Lookup'>{0}</Value>
                                                              </Eq>
                                                           </Where>", chuyende);

                        NewsUrl = string.Format("{0}/{1}.aspx?IsChuyenDe=1&ListCategoryName={2}&ListName={3}&Page=1&CategoryId=",
                                                       SPContext.Current.Web.Url,
                                                       Constants.PageInWeb.SubPage,
                                                       ListsName.English.NewsCategory,
                                                       ListsName.English.NewsRecord);

                        var newsItem = Utilities.GetNewsRecordsCategory(newsQuery, ListsName.English.NewsCategory);
                        if (newsItem != null && newsItem.Rows.Count > 0)
                        {
                            rptChuyenDe.DataSource = newsItem;
                            rptChuyenDe.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Build beadcrumb for news
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="categoryListName"></param>
        /// <returns></returns>
        public string BuilBreadcrumb(string categoryName, string categoryListName)
        {
            string result = string.Empty;

            string newsQuery = string.Format("<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{0}</Value></Eq></Where>", categoryName);
            var categoryItem = Utilities.GetNewsRecords(newsQuery, 1, categoryListName);

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
