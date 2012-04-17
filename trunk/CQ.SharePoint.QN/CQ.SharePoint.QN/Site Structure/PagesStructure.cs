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
                CreateDetailPage(web, Constants.PageInWeb.DetailNews);
                CreateSubPage(web, Constants.PageInWeb.SubPage);

                var rootWeb = web.RootFolder;
                rootWeb.WelcomePage = "default.aspx";
                rootWeb.Update();

                #region LeftContent
                //hotNewsContentWebpart news
                var hotNewsContentWebpart = WebPartHelper.GetWebPart(web, "HotNewsContent.webpart");
                AddCustomWebpart(hotNewsContentWebpart, Constants.PageInWeb.DefaultPage, "HotNewsContent-posittion0", web, "LeftContent", 0);
                //Slideshow
                var slideshowWebpart = WebPartHelper.GetWebPart(web, "SlideShowHome.webpart");
                AddCustomWebpart(slideshowWebpart, Constants.PageInWeb.DefaultPage, "HotNewsContent-posittion1", web, "LeftContent", 1);

                //Mung dang quang vinh logo
                var qnadvLogo1Webpart = WebPartHelper.GetWebPart(web, "QNadv.webpart");
                AddCustomWebpart(qnadvLogo1Webpart, Constants.PageInWeb.DefaultPage, "QNadv-posittion1", web, "LeftContent", 2);

                //Mode news content
                var modeNewsContentWebpart = WebPartHelper.GetWebPart(web, "ModeNewsContent.webpart");
                AddCustomWebpart(modeNewsContentWebpart, Constants.PageInWeb.DefaultPage, "ModeNewsContent-posittion1", web, "LeftContent", 3);

                #endregion

                #region RightContent
                var provinceInfoWebpart = WebPartHelper.GetWebPart(web, "ProvinceInfo.webpart");
                AddCustomWebpart(provinceInfoWebpart, Constants.PageInWeb.DefaultPage, "ProvinceInfo-posittion1", web, "RightContent", 0);

                var provinceDocsWebpart = WebPartHelper.GetWebPart(web, "ProvinceDocs.webpart");
                AddCustomWebpart(provinceDocsWebpart, Constants.PageInWeb.DefaultPage, "ProvinceDocsWebpart-posittion1", web, "RightContent", 1);

                var homeVideoRightWebpart = WebPartHelper.GetWebPart(web, "HomeVideoRight.webpart");
                AddCustomWebpart(homeVideoRightWebpart, Constants.PageInWeb.DefaultPage, "HomeVideoRight-posittion1", web, "RightContent", 2);

                var focusNewsWebpart = WebPartHelper.GetWebPart(web, "FocusNews.webpart");
                AddCustomWebpart(focusNewsWebpart, Constants.PageInWeb.DefaultPage, "FocusNews-posittion1", web, "RightContent", 3);

                //Doanh nghiep tieu bieu
                var focusCompanysWebpart = WebPartHelper.GetWebPart(web, "FocusCompany.webpart");
                AddCustomWebpart(focusCompanysWebpart, Constants.PageInWeb.DefaultPage, "FocusCompany-posittion1", web, "RightContent", 4);

                //doanh nghiệp mới thành lập 
                var newCompanysWebpart = WebPartHelper.GetWebPart(web, "CompanyListRight.webpart");
                AddCustomWebpart(newCompanysWebpart, Constants.PageInWeb.DefaultPage, "CompanyListRight-posittion1", web, "RightContent", 5);

                //doanh nghiệp thay đổi thông tin 
                var companyChangeWebpart = WebPartHelper.GetWebPart(web, "CompanyListRight.webpart");
                AddCustomWebpart(companyChangeWebpart, Constants.PageInWeb.DefaultPage, "CompanyListRight-posittion2", web, "RightContent", 6);

                //doanh nghiệp Giải thể 
                var companyDestroyWebpart = WebPartHelper.GetWebPart(web, "CompanyListRight.webpart");
                AddCustomWebpart(companyDestroyWebpart, Constants.PageInWeb.DefaultPage, "CompanyListRight-posittion3", web, "RightContent", 7);

                //quảng cáo doanh nghiệp
                var companyAdvWebpart = WebPartHelper.GetWebPart(web, "CompanyAdv.webpart");
                AddCustomWebpart(companyAdvWebpart, Constants.PageInWeb.DefaultPage, "CompanyAdv-posittion1", web, "RightContent", 8);
                #endregion

                #region LeftCorner
                //Tim kiem, sang kien, cai cach logo
                var qnadvLogo2Webpart = WebPartHelper.GetWebPart(web, "QNadv.webpart");
                AddCustomWebpart(qnadvLogo2Webpart, Constants.PageInWeb.DefaultPage, "QNadv-posittion2", web, "LeftCorner", 0);

                //leftcontentconer
                var leftNewsContentUsWebpart = WebPartHelper.GetWebPart(web, "LeftNewsContent.webpart");
                AddCustomWebpart(leftNewsContentUsWebpart, Constants.PageInWeb.DefaultPage, "LeftNewsContent-posittion1", web, "LeftCorner", 1);

                //Tim kiem, sang kien, cai cach logo
                var qnadvLogo3Webpart = WebPartHelper.GetWebPart(web, "QNadv.webpart");
                AddCustomWebpart(qnadvLogo3Webpart, Constants.PageInWeb.DefaultPage, "QNadv-posittion3", web, "LeftCorner", 2);

                //leftcontentconer
                var leftNewsContentUs2Webpart = WebPartHelper.GetWebPart(web, "LeftNewsContent.webpart");
                AddCustomWebpart(leftNewsContentUs2Webpart, Constants.PageInWeb.DefaultPage, "LeftNewsContent-posittion2", web, "LeftCorner", 3);

                //leftcontentconer
                var leftNewsContentUs3Webpart = WebPartHelper.GetWebPart(web, "LeftNewsContent.webpart");
                AddCustomWebpart(leftNewsContentUs3Webpart, Constants.PageInWeb.DefaultPage, "LeftNewsContent-posittion3", web, "LeftCorner", 4);

                //Tim kiem, sang kien, cai cach logo
                var qnadvLogo4Webpart = WebPartHelper.GetWebPart(web, "QNadv.webpart");
                AddCustomWebpart(qnadvLogo4Webpart, Constants.PageInWeb.DefaultPage, "QNadv-posittion4", web, "LeftCorner", 5);

                //leftcontentconer
                var leftNewsContentUs4Webpart = WebPartHelper.GetWebPart(web, "LeftNewsContent.webpart");
                AddCustomWebpart(leftNewsContentUs4Webpart, Constants.PageInWeb.DefaultPage, "LeftNewsContent-posittion4", web, "LeftCorner", 6);

                #endregion

                #region RightCorner
                //Live tivi
                var factTvWebpart = WebPartHelper.GetWebPart(web, "FactTV.webpart");
                AddCustomWebpart(factTvWebpart, Constants.PageInWeb.DefaultPage, "FactTV-posittion1", web, "RightCorner", 0);

                //Know information
                var needToKnowWebpart = WebPartHelper.GetWebPart(web, "NeedToKnow.webpart");
                AddCustomWebpart(needToKnowWebpart, Constants.PageInWeb.DefaultPage, "NeedToKnow-posittion1", web, "RightCorner", 1);

                //Hom thu cong vu
                var mailBoxWebpart = WebPartHelper.GetWebPart(web, "MailBox.webpart");
                AddCustomWebpart(mailBoxWebpart, Constants.PageInWeb.DefaultPage, "MailBox-posittion1", web, "RightCorner", 2);

                //Website link
                var webLinkWebpart = WebPartHelper.GetWebPart(web, "WebLink.webpart");
                AddCustomWebpart(webLinkWebpart, Constants.PageInWeb.DefaultPage, "WebLink-posittion1", web, "RightCorner", 3);

                //TourInfo
                var tourInfoWebpart = WebPartHelper.GetWebPart(web, "TourInfo.webpart");
                AddCustomWebpart(tourInfoWebpart, Constants.PageInWeb.DefaultPage, "TourInfo-posittion1", web, "RightCorner", 4);

                #endregion

                #region NewsDetail
                //TourInfo
                var newsDetailWebpart = WebPartHelper.GetWebPart(web, "NewsDetail.webpart");
                AddCustomWebpart(newsDetailWebpart, Constants.PageInWeb.DetailNews, "NewsDetail-position1", web, "LeftCorner", 0);
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
        private static void AddCustomWebpart(System.Web.UI.WebControls.WebParts.WebPart webPart, string pageName, string title, SPWeb web, string zoneId, int zoneIndex)
        {
            if (string.IsNullOrEmpty(webPart.Title))
            {
                webPart.Title = title;
            }
            WebPartHelper.AddWebPart(web, string.Format("{0}.aspx", pageName), webPart, zoneId, zoneIndex);
        }


        private static void CreatePage(SPWeb web, string pageName)
        {
            string notifyPageUrl = string.Concat(web.Url, string.Format(CultureInfo.InvariantCulture, "/{0}.aspx", pageName));
            SPFile pageFile = web.GetFile(notifyPageUrl);
            if (pageFile.Exists) pageFile.Delete();

            WebPageHelper.CreateDefaultWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), true);
        }

        private static void CreateDetailPage(SPWeb web, string pageName)
        {
            string notifyPageUrl = string.Concat(web.Url, string.Format(CultureInfo.InvariantCulture, "/{0}.aspx", pageName));
            SPFile pageFile = web.GetFile(notifyPageUrl);
            if (pageFile.Exists) pageFile.Delete();

            WebPageHelper.CreateDetailWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), true);
        }
        private static void CreateSubPage(SPWeb web, string pageName)
        {
            string notifyPageUrl = string.Concat(web.Url, string.Format(CultureInfo.InvariantCulture, "/{0}.aspx", pageName));
            SPFile pageFile = web.GetFile(notifyPageUrl);
            if (pageFile.Exists) pageFile.Delete();

            WebPageHelper.CreateSubWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), true);
        }
    }
}
