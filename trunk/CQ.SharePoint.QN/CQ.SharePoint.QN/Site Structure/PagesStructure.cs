using System;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;
using CQ.SharePoint.QN.Common;
using CQ.SharePoint.QN.Core.Helpers;
using CQ.SharePoint.QN.Core.WebParts;

namespace CQ.SharePoint.QN
{
    public class PagesStructure
    {
        public static void Create(SPWeb web)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                CreatePage(web, "default");
                var rootWeb = web.RootFolder;
                rootWeb.WelcomePage = "default.aspx";
                rootWeb.Update();

                #region LeftContent
                //Focus news
                var focusNewsWebpart = WebPartHelper.GetWebPart(web, "HotNewsContent.webpart");
                AddCustomWebpart(focusNewsWebpart, "HotNewsContent-vitri0", web, "LeftContent", 0);
                //Slideshow
                var slideshowWebpart = WebPartHelper.GetWebPart(web, "SlideShowHome.webpart");
                AddCustomWebpart(slideshowWebpart, "HotNewsContent-vitri1", web, "LeftContent", 1);

                //Mung dang quang vinh logo
                var qnadvLogo1Webpart = WebPartHelper.GetWebPart(web, "QNadv.webpart");
                AddCustomWebpart(qnadvLogo1Webpart, "QNadv-vitri1", web, "LeftContent", 2);

                //Mode news content
                var modeNewsContentWebpart = WebPartHelper.GetWebPart(web, "ModeNewsContent.webpart");
                AddCustomWebpart(modeNewsContentWebpart, "ModeNewsContent-vitri1", web, "LeftContent", 3);

                #endregion


                #region LeftCorner
                //Tim kiem, sang kien, cai cach logo
                var qnadvLogo2Webpart = WebPartHelper.GetWebPart(web, "QNadv.webpart");
                AddCustomWebpart(qnadvLogo2Webpart, "QNadv-vitri2", web, "LeftCorner", 0);

                //leftcontentconer
                var leftNewsContentUsWebpart = WebPartHelper.GetWebPart(web, "LeftNewsContent.webpart");
                AddCustomWebpart(leftNewsContentUsWebpart, "LeftNewsContent-vitri1", web, "LeftCorner", 1);

                //Tim kiem, sang kien, cai cach logo
                var qnadvLogo3Webpart = WebPartHelper.GetWebPart(web, "QNadv.webpart");
                AddCustomWebpart(qnadvLogo3Webpart, "QNadv-vitri3", web, "LeftCorner", 2);

                //leftcontentconer
                var leftNewsContentUs2Webpart = WebPartHelper.GetWebPart(web, "LeftNewsContent.webpart");
                AddCustomWebpart(leftNewsContentUs2Webpart, "LeftNewsContent-vitri1", web, "LeftCorner", 3);

                //leftcontentconer
                var leftNewsContentUs3Webpart = WebPartHelper.GetWebPart(web, "LeftNewsContent.webpart");
                AddCustomWebpart(leftNewsContentUs3Webpart, "LeftNewsContent-vitri1", web, "LeftCorner", 4);

                //Tim kiem, sang kien, cai cach logo
                var qnadvLogo4Webpart = WebPartHelper.GetWebPart(web, "QNadv.webpart");
                AddCustomWebpart(qnadvLogo4Webpart, "QNadv-vitri4", web, "LeftCorner", 5);

                //leftcontentconer
                var leftNewsContentUs4Webpart = WebPartHelper.GetWebPart(web, "LeftNewsContent.webpart");
                AddCustomWebpart(leftNewsContentUs4Webpart, "LeftNewsContent-vitri1", web, "LeftCorner", 6);

                #endregion

                #region RightCorner
                //Live tivi
                var factTvWebpart = WebPartHelper.GetWebPart(web, "FactTV.webpart");
                AddCustomWebpart(factTvWebpart, "FactTV-vitri1", web, "RightCorner", 0);

                //Know information
                var needToKnowWebpart = WebPartHelper.GetWebPart(web, "NeedToKnow.webpart");
                AddCustomWebpart(needToKnowWebpart, "NeedToKnow-vitri1", web, "RightCorner", 1);

                //Website link
                var webLinkWebpart = WebPartHelper.GetWebPart(web, "WebLink.webpart");
                AddCustomWebpart(webLinkWebpart, "WebLink-vitri1", web, "RightCorner", 2);

                //TourInfo
                var tourInfoWebpart = WebPartHelper.GetWebPart(web, "TourInfo.webpart");
                AddCustomWebpart(tourInfoWebpart, "TourInfo-vitri1", web, "RightCorner", 3);

                #endregion
                rootWeb.Update();
                web.Update();
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// Add webpart to defaultpage
        /// </summary>
        /// <param name="webPart"></param>
        /// <param name="title"></param>
        /// <param name="web"></param>
        /// <param name="zoneId"></param>
        /// <param name="zoneIndex"></param>
        private static void AddCustomWebpart(System.Web.UI.WebControls.WebParts.WebPart webPart, string title, SPWeb web, string zoneId, int zoneIndex)
        {
            if (string.IsNullOrEmpty(webPart.Title))
            {
                webPart.Title = title;
            }
            WebPartHelper.AddWebPart(web, "default.aspx", webPart, zoneId, zoneIndex);
        }


        private static void CreatePage(SPWeb web, string pageName)
        {
            string notifyPageUrl = string.Concat(web.Url, string.Format(CultureInfo.InvariantCulture, "/{0}.aspx", pageName));
            SPFile pageFile = web.GetFile(notifyPageUrl);
            if (pageFile.Exists) pageFile.Delete();

            WebPageHelper.CreateDefaultWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), true);
        }
    }
}
