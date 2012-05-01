using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using System.Web.UI.HtmlControls;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class FactTVUS : UserControl
    {
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                    {
                        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            try
                            {
                                SPQuery spQuery = new SPQuery
                                {
                                    Query = "<OrderBy><FieldRef Name='Position' Ascending='TRUE' /></OrderBy>"
                                };
                                SPList list = Utilities.GetListFromUrl(web, ListsName.English.QNTVList);
                                if (list != null)
                                {
                                    SPListItemCollection items = list.GetItems(spQuery);
                                    if (items != null && items.Count > 0)
                                    {
                                        var tblTV = items.GetDataTable();
                                        rptTVLink.DataSource = tblTV;
                                        rptTVLink.DataBind();
                                        var tblShowTV = tblTV.Clone();
                                        tblShowTV.ImportRow(tblTV.Rows[0]);
                                        rptTV.DataSource = tblShowTV;
                                        rptTV.DataBind();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                });
            }
        }

        protected void rptTVLink_OnItemDataBound(object Sender, RepeaterItemEventArgs e)
        {

            // This event is raised for the header, the footer, separators, and items.

            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                var aLink = (HtmlAnchor) e.Item.FindControl("aLink");
                aLink.HRef = "javascript:void(0)";
                aLink.Title = Convert.ToString(drv["Title"]);
                var tvCode = Convert.ToString(drv["Value"]);
                tvCode = tvCode.Replace("\r\n", "");
                tvCode = tvCode.Replace("\n", "");
                tvCode = tvCode.Replace("\r", "");
                tvCode = tvCode.Replace("'", "\\'");
                aLink.Attributes.Add("onclick", string.Format("javascript:setTVPlay('{0}','{1}')", Convert.ToString(drv["ID"]), tvCode));

                var imgLink = (HtmlImage)e.Item.FindControl("imgLink");
                if (!string.IsNullOrEmpty(Convert.ToString(drv["Logo"])))
                {
                    imgLink.Src = Convert.ToString(drv["Logo"]);
                }
                else
                {
                    imgLink.Src = SPContext.Current.Web.Url + "/" + ListsName.English.CQQNResources + "/qtv.jpg";
                }
                imgLink.Alt = Convert.ToString(drv["Title"]);
            }
        }
    }
}
