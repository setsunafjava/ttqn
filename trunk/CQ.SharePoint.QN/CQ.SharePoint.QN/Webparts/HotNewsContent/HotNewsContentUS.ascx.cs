using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;


namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// HotNewsContentUS
    /// </summary>
    public partial class HotNewsContentUS : UserControl
    {
        public HotNewsContent WebPartParent;
        public string NewsUrl = string.Empty;
        public string Linktoitem = string.Empty;
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
                    //Set language
                    //if language is VietNamese
                    //lblDay.Text = "Ngày";
                    //lblDay2.Text = "Ngày";
                    lblLatestNews.Text = "Tin mới";
                    lblReadMost.Text = "Đọc nhiều";
                    lblNewsLatestSend.Text = "Tin mới nhận";

                    //Bind data to latest news
                    NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews);
                    string latestNewsQuery = string.Empty;

                    #region Latest News
                    latestNewsQuery = string.Format("<Where><Neq><FieldRef Name='Status' /><Value Type='Boolean'>1</Value></Neq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>");
                    var latestNewsTable = Utilities.GetNewsRecords(latestNewsQuery, 5, ListsName.English.NewsRecord);
                    if (latestNewsTable != null && latestNewsTable.Rows.Count > 0)
                    {
                        rptLatestNews.DataSource = latestNewsTable;
                        rptLatestNews.DataBind();
                    }
                    #endregion

                    #region Top News
                    string topNewsQuery = string.Format("<Where><Neq><FieldRef Name='Status' /><Value Type='Boolean'>1</Value></Neq></Where><OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>", FieldsName.NewsRecord.English.ViewsCount);


                    var topViewsTable = Utilities.GetNewsRecords(topNewsQuery, 5, ListsName.English.NewsRecord);
                    if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                    {
                        rptTopViews.DataSource = topViewsTable;
                        rptTopViews.DataBind();
                    }
                    #endregion
                    string mainItemQuery = string.Format(@"<Where>
                                                              <And>
                                                                 <Eq>
                                                                    <FieldRef Name='{0}' />
                                                                    <Value Type='Boolean'>1</Value>
                                                                 </Eq>
                                                                 <Neq>
                                                                    <FieldRef Name='{1}' />
                                                                    <Value Type='Boolean'>1</Value>
                                                                 </Neq>
                                                              </And>
                                                           </Where>
                                                           <OrderBy>
                                                              <FieldRef Name='Created' Ascending='False' />
                                                           </OrderBy>", FieldsName.NewsRecord.English.ShowInHomePage,
                                                                      FieldsName.NewsRecord.English.Status);
                    var mainItem = Utilities.GetNewsRecords(mainItemQuery, 3, ListsName.English.NewsRecord);
                    if (mainItem != null && mainItem.Rows.Count > 0)
                    {
                        rptImages.DataSource = GetImageDataTablePath(mainItem);
                        rptImages.DataBind();
                    }

                    if ("0".Equals(WebPartParent.WebpartName))
                    {
                        rptTopViews.Visible = false;
                        pnlIndex.Visible = false;
                        pnlSubPage.Visible = true;
                        DataTable tempTable;
                        DataTable otherNewsTable = null;
                        var categoryId = Request.QueryString["CategoryId"];

                        if (!string.IsNullOrEmpty(categoryId))
                        {
                            Utilities.GetNewsByCatID(Convert.ToString(categoryId), ref otherNewsTable);
                        }

                        if (otherNewsTable != null && otherNewsTable.Rows.Count > 0)
                        {
                            //Bind data to main screen
                            //BinDataToMainScreen(otherNewsTable.Rows[0]);
                            otherNewsTable.Rows.RemoveAt(0);
                            //end
                            tempTable = otherNewsTable.Clone();
                            if (otherNewsTable.Rows.Count > 5)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    tempTable.ImportRow(otherNewsTable.Rows[i]);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < otherNewsTable.Rows.Count; i++)
                                {
                                    tempTable.ImportRow(otherNewsTable.Rows[i]);
                                }
                            }
                            if (tempTable.Rows.Count > 0)
                            {
                                rptLatestNews.DataSource = tempTable;
                                rptLatestNews.DataBind();
                            }
                            else
                            {
                                rptLatestNews.DataSource = null;
                                rptLatestNews.DataBind();
                            }
                        }
                        else
                        {
                            //BinDataToMainScreen(null);
                            rptLatestNews.DataSource = null;
                            rptLatestNews.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }


        public void BinDataToMainScreen(DataRow row)
        {
            Linktoitem = string.Format("{0}/{1}.aspx?NewsId={2}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Convert.ToString(row[FieldsName.Id]));
            string imagePath = Convert.ToString(row[FieldsName.NewsRecord.English.ThumbnailImage]);
            if (!string.IsNullOrEmpty(imagePath))
            {
                imgMainImage.ImageUrl = imagePath.Trim().Substring(0, imagePath.Length - 2);
            }
            else
            {
                imgMainImage.ImageUrl = string.Empty;
            }
            lblShortContent.Text = Convert.ToString(row[FieldsName.NewsRecord.English.ShortDescription]);
        }

        /// <summary>
        /// Get image path form thumbnail field, default the path is "path...;" => function will return "path..."
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public DataTable GetImageDataTablePath(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string imagepath = string.Empty;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    imagepath = Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage]);
                    if (imagepath.Length > 2)
                        dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length - 2);
                }
            }
            return dataTable;
        }
    }
}
