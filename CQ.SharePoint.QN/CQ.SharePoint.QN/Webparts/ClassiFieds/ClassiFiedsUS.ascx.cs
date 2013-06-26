using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class ClassiFiedsUS : UserControl
    {
        public ClassiFieds WebpartParent;
        public string NewsUrl = string.Empty;

        /// <summary/>
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var dtNow = DateTime.Now;
                string companyListQuery = string.Format(@"<Where>
                                                          <And>
                                                             <Leq>
                                                                <FieldRef Name='{0}' />
                                                                <Value IncludeTimeValue='FALSE' Type='DateTime'>{1}</Value>
                                                             </Leq>
                                                             <And>
                                                                <Geq>
                                                                   <FieldRef Name='{2}' />
                                                                   <Value IncludeTimeValue='FALSE' Type='DateTime'>{3}</Value>
                                                                </Geq>
                                                                <Neq>
                                                                   <FieldRef Name='{4}' />
                                                                   <Value Type='Boolean'>1</Value>
                                                                </Neq>
                                                             </And>
                                                          </And>
                                                       </Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>",
                                                                FieldsName.QuangCaoRaoVat.English.NgayBatDau,
                                                                SPUtility.CreateISO8601DateTimeFromSystemDateTime(dtNow),
                                                                FieldsName.QuangCaoRaoVat.English.NgayKetThuc,
                                                                SPUtility.CreateISO8601DateTimeFromSystemDateTime(dtNow),
                                                                FieldsName.QuangCaoRaoVat.English.Status);
                uint newsNumber = 5;

                if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                {
                    newsNumber = Convert.ToUInt16(WebpartParent.NumberOfNews);
                }
                var quangCaoRaovatList = Utilities.GetDocLibRecords(companyListQuery, newsNumber, ListsName.English.QuangCaoRaoVat);

                if (quangCaoRaovatList != null && quangCaoRaovatList.Rows.Count > 0)
                {
                    rptFocusCompany.DataSource = Utilities.GetTableWithCorrectUrlDoclib(SPContext.Current.Web, quangCaoRaovatList);
                    rptFocusCompany.DataBind();
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }

        }
        protected void OnItemDataBound_ClassiFieds(object sender, RepeaterItemEventArgs e)
        {
            var t = e.Item.DataItem as DataRowView;
            if (t != null)
            {
                string imagePath = Convert.ToString(t.Row[18]);
                int height = 100;
                var heightSet = Convert.ToInt32(t.Row[21]);
                if (heightSet > 0)
                {
                    height = heightSet;
                }

                if (!String.IsNullOrEmpty(imagePath) && imagePath.Length > 3)
                {
                    var extent = imagePath.Substring(imagePath.Length - 3);
                    var literalTemp = (Literal)e.Item.FindControl("ltrFlash1");

                    switch (extent)
                    {
                        case "swf":
                            {
                                if (literalTemp != null)
                                {
                                    literalTemp.Visible = true;
                                    literalTemp.Text = string.Format("<embed bgcolor=\"#FFFFFF\" height=\"{0}\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"{1}\" src=\"{2}\" ></embed>", height, 232, imagePath);
                                }
                                break;
                            }
                        default:
                            {
                                if (literalTemp != null)
                                {
                                    literalTemp.Visible = true;
                                    literalTemp.Text = string.Format("<img src=\"{0}\" height=\"{1}\" width=\"{2}\" />", imagePath, height, 232);
                                }
                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Event click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        protected void Link_OnClick(object sender, EventArgs eventArgs)
        {
            try
            {
                if (!string.IsNullOrEmpty(((LinkButton)sender).CommandArgument))
                {
                    var qcItem = Utilities.GetDocListItemFromUrl(ListsName.English.QuangCaoRaoVat, Convert.ToInt32(((LinkButton)sender).CommandArgument));
                    if (qcItem != null)
                    {
                        SPSecurity.RunWithElevatedPrivileges(() =>
                        {
                            using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                            {
                                using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                                {
                                    try
                                    {
                                        var qcList = Utilities.GetDocListFromUrl(web, ListsName.English.QuangCaoRaoVat);
                                        var qItem = qcList.GetItemById(qcItem.ID);
                                        string viewcount = Convert.ToString(qItem[FieldsName.QuangCaoRaoVat.English.CountClick]);
                                        if (!string.IsNullOrEmpty(viewcount))
                                        {
                                            int count = Convert.ToInt32(viewcount);
                                            qItem[FieldsName.QuangCaoRaoVat.English.CountClick] = ++count;
                                            web.AllowUnsafeUpdates = true;
                                            qItem.Update();
                                        }
                                        else
                                        {
                                            qItem[FieldsName.QuangCaoRaoVat.English.CountClick] = 1;
                                            web.AllowUnsafeUpdates = true;
                                            qItem.Update();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Utilities.LogToUls(ex);
                                    }
                                }
                            }
                        });
                        HttpContext.Current.Response.Redirect(Convert.ToString(qcItem["LinkUrl"]));
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
        }
    }
}
