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
    public partial class FocusNewsUS : UserControl
    {
        public FocusNews WebpartParent;
        public string NewsUrl = string.Format("{0}/{1}.aspx?NewsId=", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews);
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    string focusNewsQuery = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Boolean'>1</Value></Eq></Where>", FieldsName.NewsRecord.English.FocusNews);
                    var focusNewsTable = Utilities.GetNewsRecords(focusNewsQuery, Convert.ToUInt16(WebpartParent.NumberOfNews), ListsName.English.NewsRecord);
                    if (focusNewsTable != null && focusNewsTable.Rows.Count > 0)
                    {
                        rptFocusNews.DataSource = focusNewsTable;
                        rptFocusNews.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    
                    
                }
            }
        }
    }
}
