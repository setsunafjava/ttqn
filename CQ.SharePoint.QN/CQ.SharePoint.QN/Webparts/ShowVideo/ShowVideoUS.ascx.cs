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
    public partial class ShowVideoUS : UserControl
    {
        public ShowVideo ParentWP;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCreate_OnClick(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                    {
                        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            try
                            {
                                PagesStructure.CreateSubPage(web, Constants.PageInWeb.TimKiem);
                                PagesStructure.CreateBlankPage(web, Constants.PageInWeb.RSS);

                                AddWebpart(web, Constants.PageInWeb.TimKiem, "ShowDownload.webpart", "LeftCorner");
                                PagesStructure.AddCustomWebpart("Video.webpart", Constants.PageInWeb.RSS, "RSS", web, "MainContent", 0);
                            }
                            catch (Exception ex)
                            {
                                
                            }
                        }
                    }
                });
        }
        private void AddWebpart(SPWeb web, string pageName, string webPartName, string zoneID)
        {
            PagesStructure.AddCustomWebpart("QNHeader.webpart", pageName, "QNHeader", web, "Header", 0);
            PagesStructure.AddCustomWebpart("QNadv.webpart", pageName, "QNHeaderQNadv", web, "Header", 1);
            PagesStructure.AddCustomWebpart("ShouldToKnow.webpart", pageName, "ShouldToKnow", web, "ShouldToKnow", 0);
            PagesStructure.AddCustomWebpart("Footer.webpart", pageName, "Footer", web, "Footer", 0);
            PagesStructure.AddCustomWebpart("ProvinceInfo.webpart", pageName, "ProvinceInfo-posittion1", web, "RightContent", 0);
            PagesStructure.AddCustomWebpart("ProvinceDocs.webpart", pageName, "ProvinceDocsWebpart-posittion1", web, "RightContent", 1);
            PagesStructure.AddCustomWebpart("HomeVideoRight.webpart", pageName, "HomeVideoRight-posittion1", web, "RightContent", 2);
            PagesStructure.AddCustomWebpart("CompanyListRight.webpart", pageName, "CompanyListRight-posittion0", web, "RightContent", 3);
            PagesStructure.AddCustomWebpart(webPartName, pageName, pageName + "-posittion0", web, zoneID, 0);
            //Live tivi
            PagesStructure.AddCustomWebpart("FactTV.webpart", pageName, "FactTV-posittion0", web, "RightCorner", 0);

            //Know information
            PagesStructure.AddCustomWebpart("NeedToKnow.webpart", pageName, "NeedToKnow-posittion0", web, "RightCorner", 1);

            //Hom thu cong vu
            PagesStructure.AddCustomWebpart("MailBox.webpart", pageName, "MailBox-posittion0", web, "RightCorner", 2);

            //Website link
            PagesStructure.AddCustomWebpart("WebLink.webpart", pageName, "WebLink-posittion0", web, "RightCorner", 3);

            //TourInfo
            PagesStructure.AddCustomWebpart("TourInfo.webpart", pageName, "TourInfo-posittion0", web, "RightCorner", 4);

            //Raovat
            PagesStructure.AddCustomWebpart("ClassiFieds.webpart", pageName, "ClassiFieds-posittion0", web, "RightCorner", 5);
        }
    }
}
