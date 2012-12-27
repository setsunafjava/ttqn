﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using Microsoft.SharePoint.WebPartPages;
using System.Globalization;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class LeftNewsContentUS : UserControl
    {
        public LeftNewsContent WebpartParent;
        public string NewsUrl = string.Empty;
        public string CategoryUrl = string.Empty;
        public string NewsFirstUrl1 = string.Empty;

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
                    NewsUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}=",
                       SPContext.Current.Web.Url,
                       Constants.PageInWeb.DetailNews,
                       ListsName.English.NewsCategory,
                       ListsName.English.NewsRecord,
                       Constants.NewsId);
                    var newsLimit = Utilities.GetNewsNumber(WebpartParent.NumberOfNews);
                    DataTable newsGroups = null;
                    Utilities.GetNewsByCatID(ListsName.English.NewsRecord, ListsName.English.NewsCategory, WebpartParent.NewsGroupID, ref newsGroups);
                    if (newsGroups != null && newsGroups.Rows.Count > 0)
                    {
                        var t1 = Utilities.GetTableWithCorrectUrl(newsGroups, false);
                        var tempTable = Utilities.SelectTopDataRow(t1, (int)newsLimit);
                        var table = tempTable.DefaultView;
                        table.Sort = "ArticleStartDates DESC";

                        lblHeader.Text = Convert.ToString(table[0][FieldsName.Title]);
                        imgThumb.ImageUrl = Convert.ToString(table[0][FieldsName.NewsRecord.English.ThumbnailImage]);

                        lblShortContent.Text = Convert.ToString(table[0][FieldsName.NewsRecord.English.ShortContent]);

                        NewsFirstUrl1 = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&{4}={5}&CategoryId={6}",
                                   SPContext.Current.Web.Url,
                                   Constants.PageInWeb.DetailNews,
                                   ListsName.English.NewsCategory,
                                   ListsName.English.NewsRecord,
                                   Constants.NewsId,
                                   Convert.ToString(table[0][FieldsName.Id]),
                                   Convert.ToString(table[0][Constants.CategoryId]));
                        table[0].Delete();

                        //var temp1Table = Utilities.SelectTopDataRow(tempTable, Convert.ToInt32(tempTable.Rows.Count-1)););
                        if (table.Count > 0)
                        {
                            rptCaiCachThuTucHanhChinh.DataSource = table;
                            rptCaiCachThuTucHanhChinh.DataBind();
                        }
                    }
                    CategoryUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&{4}=",
                       SPContext.Current.Web.Url,
                       Constants.PageInWeb.SubPage,
                       ListsName.English.NewsCategory,
                       ListsName.English.NewsRecord,
                       Constants.CategoryId);

                    string newsTitle = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='{2}' Ascending='False' /></OrderBy>",
                        FieldsName.Title,
                        WebpartParent.GroupName,
                        FieldsName.Id);

                    

                    var newsTitleItems = Utilities.GetNewsRecords(newsTitle, newsLimit, ListsName.English.NewsCategory);
                    if (newsTitleItems != null && newsTitleItems.Rows.Count > 0)
                    {
                        rptNewsGroup.DataSource = newsTitleItems;
                        rptNewsGroup.DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// OnpreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            WebPartManager webPartManager = WebPartManager.GetCurrentWebPartManager(Page);
        }

        protected void lbRSS_OnClick(object sender, EventArgs e)
        {
        }
    }
}
