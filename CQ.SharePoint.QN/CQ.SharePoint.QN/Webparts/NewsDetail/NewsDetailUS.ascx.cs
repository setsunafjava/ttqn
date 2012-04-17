using System;
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
         if(!IsPostBack)
         {
             var newsId = Request.QueryString["NewsID"];
             lblCurrentDate.Text = DateTime.Now.ToShortDateString();
             if (!string.IsNullOrEmpty(newsId))
             {
                 string newsQuery = string.Format("");
                 var newsItem = Utilities.GetNewsRecords(newsQuery, 1, ListsName.English.NewsRecord);
                 if (newsItem != null)
                 {
                     ltrNewsContent.Text = Convert.ToString(newsItem.Rows[0][FieldsName.NewsRecord.English.Content]);
                 }
             }
         }
        }
    }
}
