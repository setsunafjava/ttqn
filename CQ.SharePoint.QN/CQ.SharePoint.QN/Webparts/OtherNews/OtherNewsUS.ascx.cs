using System;
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
                    var newsId = Request.QueryString["NewsID"];
                    //if (!string.IsNullOrEmpty(newsId))
                    //{
                        NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url,
                                                Constants.PageInWeb.DetailNews);
                        //Bind data to top view
                        string topNewsQuery =
                            string.Format("<OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>",
                                          FieldsName.NewsRecord.English.ViewsCount);
                        var topViewsTable = Utilities.GetNewsRecords(topNewsQuery,
                                                                     Convert.ToUInt16(WebpartParent.NumberOfNews),
                                                                     ListsName.English.NewsRecord);
                        if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                        {
                            rptOtherNews.DataSource = topViewsTable;
                            rptOtherNews.DataBind();
                        }

                    //}
                }
                catch (Exception ex)
                {

                }
            }
        }


    }
}
