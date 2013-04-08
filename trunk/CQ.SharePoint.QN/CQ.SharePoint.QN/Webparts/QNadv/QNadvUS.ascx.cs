using System;
using System.Web;
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
    public partial class QNadvUS : UserControl
    {
        public QNadv ParentWP;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ParentWP.QCType)) && Convert.ToString(ParentWP.QCType).Equals("1"))
                {
                    var categoryId = Convert.ToString(Request.QueryString["CategoryId"]);
                    var listCategoryName = Convert.ToString(Request.QueryString["ListCategoryName"]);
                    if (!string.IsNullOrEmpty(categoryId) && !string.IsNullOrEmpty(listCategoryName))
                    {
                        var menuType = "tintuc";
                        switch (listCategoryName)
                        {
                            case "NewsCategory":
                                {
                                    menuType = "tintuc";
                                    break;
                                }
                            case "CompanyCategory":
                                {
                                    menuType = "doanhnghiep";
                                    break;
                                }
                            case "ShouldToKnowCategory":
                                {
                                    menuType = "ttnb";
                                    break;
                                }
                            case "TourInforCategory":
                                {
                                    menuType = "dulich";
                                    break;
                                }
                            case "ProvinceInfoCategory":
                                {
                                    menuType = "ttcdct";
                                    break;
                                }
                        }
                        var qcItem = Utilities.GetQuangCao("quangcaochuyende", categoryId, menuType);
                        if (qcItem != null)
                        {
                            //var startDate = Convert.ToDateTime(qcItem["NgayBatDau"]);
                            //var endDate = Convert.ToDateTime(qcItem["NgayKetThuc"]);
                            //if (startDate <= DateTime.Now && DateTime.Now <= endDate)
                            //{
                                aLink.CommandArgument = Convert.ToString(qcItem["LinkUrl"]);
                                var qcFile = Convert.ToString(qcItem[FieldsName.NewsRecord.English.PublishingPageImage]).Split(' ')[3];
                                qcFile = qcFile.Trim().Substring(5, qcFile.Length - 6);
                                if ("Image".Equals(Convert.ToString(qcItem["QCType"])))
                                {
                                    ltrQC.Text = "<img src='" + qcFile + "' width='" + Convert.ToString(qcItem["Width"]) + "' height='" + Convert.ToString(qcItem["Height"]) + "' alt='" + qcItem.Title + "' title='" + qcItem.Title + "' />";
                                }
                                else if ("Flash".Equals(Convert.ToString(qcItem["QCType"])))
                                {
                                    ltrQC.Text = @"<embed width='" + Convert.ToString(qcItem["Width"]) + "' height='" + Convert.ToString(qcItem["Height"]) + @"' align='middle' quality='high' wmode='transparent' allowscriptaccess='always' 
                                        type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' alt='' 
                                        src='" + qcFile + "' />";
                                }
                                else if ("Video".Equals(Convert.ToString(qcItem["QCType"])))
                                {
                                    ltrQC.Text =
                                        @"<embed
                                  flashvars='file=" + SPContext.Current.Web.Url + "/" + ListsName.English.CQQNResources + @"/stylish_slim.swf&autostart=true'
                                  allowfullscreen='true'
                                  allowscripaccess='always'
                                  id='" + this.ID + "-quangcao-" + ParentWP.QCID + @"'
                                  name='" + this.ID + "-quangcao-" + ParentWP.QCID + @"'
                                  src='" + qcFile + @"'
                                  width='" + Convert.ToString(qcItem["Width"]) + @"'
                                  height='" + Convert.ToString(qcItem["Height"]) + @"'
                                />";
                                }
                                else
                                {
                                    aLink.Visible = false;
                                }
                            //}
                            //else
                            //{
                            //    aLink.Visible = false;
                            //}
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(Convert.ToString(ParentWP.QCID)))
                {
                    //var qcItem = Utilities.GetDocListItemFromUrl("QuangCao", ParentWP.QCID);
                    var qcItem = Utilities.GetQuangCao("QuangCao", ParentWP.QCID);
                    if (qcItem != null)
                    {
                        //var startDate = Convert.ToDateTime(qcItem["NgayBatDau"]);
                        //var endDate = Convert.ToDateTime(qcItem["NgayKetThuc"]);
                        //if (startDate <= DateTime.Now && DateTime.Now <= endDate)
                        //{
                            aLink.CommandArgument = ParentWP.QCID.ToString();
                            if ("Image".Equals(Convert.ToString(qcItem["Type"])))
                            {
                                ltrQC.Text = "<img src='" + SPContext.Current.Web.Url + "/" + qcItem.Url + "' width='" + Convert.ToString(qcItem["Width"]) + "' height='" + Convert.ToString(qcItem["Height"]) + "' alt='" + qcItem.Title + "' title='" + qcItem.Title + "' />";
                            }
                            else if ("Flash".Equals(Convert.ToString(qcItem["Type"])))
                            {
                                ltrQC.Text = @"<embed width='" + Convert.ToString(qcItem["Width"]) + "' height='" + Convert.ToString(qcItem["Height"]) + @"' align='middle' quality='high' wmode='transparent' allowscriptaccess='always' 
                                        type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' alt='' 
                                        src='" + SPContext.Current.Web.Url + "/" + qcItem.Url + "' />";
                            }
                            else if ("Video".Equals(Convert.ToString(qcItem["Type"])))
                            {
                                ltrQC.Text =
                                    @"<embed
                                  flashvars='file=" + SPContext.Current.Web.Url + "/" + ListsName.English.CQQNResources + @"/stylish_slim.swf&autostart=true'
                                  allowfullscreen='true'
                                  allowscripaccess='always'
                                  id='" + this.ID + "-quangcao-" + ParentWP.QCID + @"'
                                  name='" + this.ID + "-quangcao-" + ParentWP.QCID + @"'
                                  src='" + SPContext.Current.Web.Url + "/" + qcItem.Url + @"'
                                  width='" + Convert.ToString(qcItem["Width"]) + @"'
                                  height='" + Convert.ToString(qcItem["Height"]) + @"'
                                />";
                            }
                            else
                            {
                                aLink.Visible = false;
                            }
                        //}
                        //else
                        //{
                        //    aLink.Visible = false;
                        //}
                    }
                    else
                    {
                        aLink.Visible = false;
                    }
                }
                else
                {
                    aLink.Visible = false;
                }
            }
        }

        protected void aLink_OnClick(object sender, EventArgs e)
        {
            var qcUrl = string.Empty;
            if (!string.IsNullOrEmpty(((LinkButton)sender).CommandArgument))
            {
                try
                {
                    var qcid = Convert.ToInt32(((LinkButton) sender).CommandArgument);
                    var qcItem = Utilities.GetDocListItemFromUrl("QuangCao", qcid);
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
                                        string listUrl = web.Url + "/Lists/QuangCaoReport";
                                        var result = web.GetList(listUrl);
                                        var item = result.Items.Add();
                                        item["Title"] = qcItem.Title;
                                        item["QuangCao"] = new SPFieldLookupValue(qcItem.ID, qcItem.Title);
                                        item["IP"] = HttpContext.Current.Request.UserHostAddress;
                                        item["Browser"] = HttpContext.Current.Request.Browser.Browser;
                                        item["Url"] = HttpContext.Current.Request.Url.AbsoluteUri;
                                        web.AllowUnsafeUpdates = true;
                                        item.Update();
                                        var qcList = Utilities.GetDocListFromUrl(web, "QuangCao");
                                        var qItem = qcList.GetItemById(qcItem.ID);
                                        qcUrl = Convert.ToString(qcItem["LinkUrl"]);
                                        qItem["CountClick"] = Convert.ToInt32(qItem["CountClick"]) + 1;
                                        web.AllowUnsafeUpdates = true;
                                        qItem.Update();
                                    }
                                    catch (Exception ex)
                                    {
                                        Utilities.LogToUls(ex);
                                    }
                                }

                            }
                        });
                        if (!string.IsNullOrEmpty(qcUrl))
                        {
                            HttpContext.Current.Response.Redirect(qcUrl);
                            //HttpContext.Current.Response.Write("<script>location.href = '" + qcUrl + "';</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(qcUrl))
                    {
                        HttpContext.Current.Response.Redirect(qcUrl);
                        //HttpContext.Current.Response.Write("<script>location.href = '" + qcUrl + "';</script>");
                    }
                    HttpContext.Current.Response.Redirect(((LinkButton)sender).CommandArgument); 
                    //HttpContext.Current.Response.Write("<script>location.href = '" + ((LinkButton)sender).CommandArgument + "';</script>");
                }
                
            }
        }
    }
}
