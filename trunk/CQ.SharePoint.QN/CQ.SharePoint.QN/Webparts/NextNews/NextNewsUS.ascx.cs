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
        /// <param name="maxvalue"></param>
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
            var listName = Request.QueryString[Constants.ListName];
            var listCategoryName = Request.QueryString[Constants.ListCategoryName];

            CategoryUrl = string.Format("{0}/{1}.aspx?ListCategoryName={2}&ListName={3}&Page=1&CategoryId=-1&&Day={4}&&Month={5}&&Year={6}",
                SPContext.Current.Web.Url,
                Constants.PageInWeb.SubPage,
                listCategoryName,
                listName,
                ddlDays.SelectedItem.Text,
                ddlMonths.SelectedItem.Text,
                ddlYears.SelectedItem.Text);
            Response.Redirect(CategoryUrl);
        }
    }
}
