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
    public partial class NextNewsUS : UserControl
    {
        public NextNews ParentWP;
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
                        BindDataToDropDownList(1, 31, ddlDays, Convert.ToString(DateTime.Now.Day));
                        BindDataToDropDownList(1, 12, ddlMonths, Convert.ToString(DateTime.Now.Month));
                        BindDataToDropDownList(2000, 2013, ddlYears, Convert.ToString(DateTime.Now.Year));
                    }
                }
                catch(Exception ex)
                {
                }
            }
        }
        /// <summary>
        /// Bind Days, Months, Year to dropdownlist
        /// </summary>
        /// <param name="values"></param>
        /// <param name="maxvalues"></param>
        /// <param name="dropDownList"></param>
        /// <param name="minvalue"></param>
        /// <param name="selectedvalue"></param>
        protected void BindDataToDropDownList(int minvalue, int maxvalue, DropDownList dropDownList, string selectedvalue)
        {
            for (int i = minvalue; i < maxvalue; i++)
            {
                dropDownList.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
            }
            dropDownList.SelectedValue = selectedvalue;
        }
    }
}
