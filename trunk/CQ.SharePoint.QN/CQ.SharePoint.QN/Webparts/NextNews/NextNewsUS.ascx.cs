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
        public string CategoryUrl = string.Empty;
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
                    lblDay.Text = "Ngày";
                    lblMonth.Text = "Tháng";
                    lblYear.Text = "Năm";

                    var nextNews = Request.QueryString["NextNews"];

                    CategoryUrl = string.Format("{0}/{1}.aspx?CategoryId=", SPContext.Current.Web.Url, Constants.PageInWeb.SubPage);
                    BindDataToDropDownList(1, 31, ddlDays, Convert.ToString(DateTime.Now.Day));
                    BindDataToDropDownList(1, 12, ddlMonths, Convert.ToString(DateTime.Now.Month));
                    BindDataToDropDownList(2000, 2013, ddlYears, Convert.ToString(DateTime.Now.Year));

                }
                catch (Exception ex)
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

        protected void NextNewsClick(object sender, EventArgs e)
        {
            CategoryUrl = string.Format("{0}/{1}.aspx?CategoryId=-1&&Day={2}&&Month={3}&&Year={4}", SPContext.Current.Web.Url, Constants.PageInWeb.SubPage, ddlDays.SelectedItem.Text, ddlMonths.SelectedItem.Text, ddlYears.SelectedItem.Text);
            Response.Redirect(CategoryUrl);
        }
    }
}
