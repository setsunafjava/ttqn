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
    public partial class MostViewNewsUS : UserControl
    {
        public MostViewNews WebpartParent;
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
                    if (!string.IsNullOrEmpty(newsId))
                    {
                        //Bind data to top view
                        NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url,
                                                Constants.PageInWeb.DetailNews);
                        string topNewsQuery =
                            string.Format("<OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>",
                                          FieldsName.NewsRecord.English.ViewsCount);
                        var topViewsTable = Utilities.GetNewsRecords(topNewsQuery,
                                                                     Convert.ToUInt16(WebpartParent.NumberOfNews),
                                                                     ListsName.English.NewsRecord);
                        if (topViewsTable != null && topViewsTable.Rows.Count > 0)
                        {
                            rptTopViews.DataSource = topViewsTable;
                            rptTopViews.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
