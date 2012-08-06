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
                CreateSubPage(web, Constants.PageInWeb.DownloadPage);
                CreateSubPage(web, Constants.PageInWeb.GalleryPage);
                CreateSubPage(web, Constants.PageInWeb.VideoPage);
                CreateSubPage(web, Constants.PageInWeb.Contact);
                CreateBlankPage(web, Constants.PageInWeb.ShowGalleryPage);
                CreateBlankPage(web, Constants.PageInWeb.ShowVideoPage);

                var rootWeb = web.RootFolder;
                rootWeb.WelcomePage = "default.aspx";
                rootWeb.Update();

                #region Header
                AddCustomWebpart("QNHeader.webpart", Constants.PageInWeb.DefaultPage, "QNHeader", web, "Header", 0);
                AddCustomWebpart("QNHeader.webpart", Constants.PageInWeb.DetailNews, "QNHeader", web, "Header", 0);
                AddCustomWebpart("QNHeader.webpart", Constants.PageInWeb.VideoPage, "QNHeader", web, "Header", 0);
                AddCustomWebpart("QNHeader.webpart", Constants.PageInWeb.SubPage, "QNHeader", web, "Header", 0);
                AddCustomWebpart("QNHeader.webpart", Constants.PageInWeb.DownloadPage, "QNHeader", web, "Header", 0);
                AddCustomWebpart("QNHeader.webpart", Constants.PageInWeb.GalleryPage, "QNHeader", web, "Header", 0);
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.DetailNews, "QNHeaderQNadv", web, "Header", 1);
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.VideoPage, "QNHeaderQNadv", web, "Header", 1);
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.SubPage, "QNHeaderQNadv", web, "Header", 1);
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.DownloadPage, "QNHeaderQNadv", web, "Header", 1);
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.GalleryPage, "QNHeaderQNadv", web, "Header", 1);
                #endregion

                #region ShouldToKnow
                AddCustomWebpart("ShouldToKnow.webpart", Constants.PageInWeb.DefaultPage, "ShouldToKnow", web, "ShouldToKnow", 0);
                AddCustomWebpart("ShouldToKnow.webpart", Constants.PageInWeb.DetailNews, "ShouldToKnow", web, "ShouldToKnow", 0);
                AddCustomWebpart("ShouldToKnow.webpart", Constants.PageInWeb.SubPage, "ShouldToKnow", web, "ShouldToKnow", 0);
                AddCustomWebpart("ShouldToKnow.webpart", Constants.PageInWeb.DownloadPage, "ShouldToKnow", web, "ShouldToKnow", 0);
                AddCustomWebpart("ShouldToKnow.webpart", Constants.PageInWeb.GalleryPage, "ShouldToKnow", web, "ShouldToKnow", 0);
                AddCustomWebpart("ShouldToKnow.webpart", Constants.PageInWeb.VideoPage, "ShouldToKnow", web, "ShouldToKnow", 0);
                #endregion

                #region Footer
                AddCustomWebpart("Footer.webpart", Constants.PageInWeb.DefaultPage, "Footer", web, "Footer", 0);
                AddCustomWebpart("Footer.webpart", Constants.PageInWeb.DetailNews, "Footer", web, "Footer", 0);
                AddCustomWebpart("Footer.webpart", Constants.PageInWeb.SubPage, "Footer", web, "Footer", 0);
                AddCustomWebpart("Footer.webpart", Constants.PageInWeb.DownloadPage, "Footer", web, "Footer", 0);
                AddCustomWebpart("Footer.webpart", Constants.PageInWeb.GalleryPage, "Footer", web, "Footer", 0);
                AddCustomWebpart("Footer.webpart", Constants.PageInWeb.VideoPage, "Footer", web, "Footer", 0);
                #endregion

                #region LeftContent
                //hotNewsContentWebpart news
                AddCustomWebpart("HotNewsContent.webpart", Constants.PageInWeb.DefaultPage, "HotNewsContent-posittion0", web, "LeftContent", 0);
                AddCustomWebpart("HotNewsContent.webpart", Constants.PageInWeb.SubPage, "HotNewsContent-posittion0", web, "LeftContent", 0);
                //Slideshow
                AddCustomWebpart("SlideShowHome.webpart", Constants.PageInWeb.DefaultPage, "HotNewsContent-posittion1", web, "LeftContent", 1);
                AddCustomWebpart("SlideShowHome.webpart", Constants.PageInWeb.SubPage, "HotNewsContent-posittion1", web, "LeftContent", 1);

                //Quang cao
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.DefaultPage, "QNadv-posittion0", web, "LeftContent", 2);

                //Mode news content
                AddCustomWebpart("ModeNewsContent.webpart", Constants.PageInWeb.DefaultPage, "ModeNewsContent-posittion0", web, "LeftContent", 3);

                //Quang cao
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.DefaultPage, "QNadv-posittion1", web, "LeftContent", 4);

                //Mode news content
                AddCustomWebpart("ModeNewsContent.webpart", Constants.PageInWeb.DefaultPage, "ModeNewsContent-posittion1", web, "LeftContent", 5);

                #endregion

                #region RightContent
                AddCustomWebpart("ProvinceInfo.webpart", Constants.PageInWeb.DefaultPage, "ProvinceInfo-posittion1", web, "RightContent", 0);

                AddCustomWebpart("ProvinceDocs.webpart", Constants.PageInWeb.DefaultPage, "ProvinceDocsWebpart-posittion1", web, "RightContent", 1);

                AddCustomWebpart("HomeVideoRight.webpart", Constants.PageInWeb.DefaultPage, "HomeVideoRight-posittion1", web, "RightContent", 2);

                AddCustomWebpart("FocusNews.webpart", Constants.PageInWeb.DefaultPage, "FocusNews-posittion1", web, "RightContent", 3);

                //Doanh nghiep tieu bieu
                AddCustomWebpart("FocusCompany.webpart", Constants.PageInWeb.DefaultPage, "FocusCompany-posittion1", web, "RightContent", 4);

                //doanh nghiệp theo catalogy 
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.DefaultPage, "CompanyListRight-posittion0", web, "RightContent", 5);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.DefaultPage, "CompanyListRight-posittion1", web, "RightContent", 6);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.DefaultPage, "CompanyListRight-posittion2", web, "RightContent", 7);

                //quảng cáo doanh nghiệp
                AddCustomWebpart("CompanyAdv.webpart", Constants.PageInWeb.DefaultPage, "CompanyAdv-posittion1", web, "RightContent", 8);

                AddCustomWebpart("ProvinceInfo.webpart", Constants.PageInWeb.DetailNews, "ProvinceInfo-posittion1", web, "RightContent", 0);

                AddCustomWebpart("ProvinceDocs.webpart", Constants.PageInWeb.DetailNews, "ProvinceDocsWebpart-posittion1", web, "RightContent", 1);

                AddCustomWebpart("HomeVideoRight.webpart", Constants.PageInWeb.DetailNews, "HomeVideoRight-posittion1", web, "RightContent", 2);

                AddCustomWebpart("FocusNews.webpart", Constants.PageInWeb.DetailNews, "FocusNews-posittion1", web, "RightContent", 3);

                //Doanh nghiep tieu bieu
                AddCustomWebpart("FocusCompany.webpart", Constants.PageInWeb.DetailNews, "FocusCompany-posittion1", web, "RightContent", 4);

                //doanh nghi?p theo catalogy 
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.DetailNews, "CompanyListRight-posittion0", web, "RightContent", 5);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.DetailNews, "CompanyListRight-posittion1", web, "RightContent", 6);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.DetailNews, "CompanyListRight-posittion2", web, "RightContent", 7);

                //qu?ng cáo doanh nghi?p
                AddCustomWebpart("CompanyAdv.webpart", Constants.PageInWeb.DetailNews, "CompanyAdv-posittion1", web, "RightContent", 8);

                AddCustomWebpart("ProvinceInfo.webpart", Constants.PageInWeb.SubPage, "ProvinceInfo-posittion1", web, "RightContent", 0);

                AddCustomWebpart("ProvinceDocs.webpart", Constants.PageInWeb.SubPage, "ProvinceDocsWebpart-posittion1", web, "RightContent", 1);

                AddCustomWebpart("HomeVideoRight.webpart", Constants.PageInWeb.SubPage, "HomeVideoRight-posittion1", web, "RightContent", 2);

                AddCustomWebpart("FocusNews.webpart", Constants.PageInWeb.SubPage, "FocusNews-posittion1", web, "RightContent", 3);

                //Doanh nghiep tieu bieu
                AddCustomWebpart("FocusCompany.webpart", Constants.PageInWeb.SubPage, "FocusCompany-posittion1", web, "RightContent", 4);

                //doanh nghi?p theo catalogy 
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.SubPage, "CompanyListRight-posittion0", web, "RightContent", 5);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.SubPage, "CompanyListRight-posittion1", web, "RightContent", 6);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.SubPage, "CompanyListRight-posittion2", web, "RightContent", 7);

                //qu?ng cáo doanh nghi?p
                AddCustomWebpart("CompanyAdv.webpart", Constants.PageInWeb.SubPage, "CompanyAdv-posittion1", web, "RightContent", 8);

                AddCustomWebpart("ProvinceInfo.webpart", Constants.PageInWeb.DownloadPage, "ProvinceInfo-posittion1", web, "RightContent", 0);

                AddCustomWebpart("ProvinceDocs.webpart", Constants.PageInWeb.DownloadPage, "ProvinceDocsWebpart-posittion1", web, "RightContent", 1);

                AddCustomWebpart("HomeVideoRight.webpart", Constants.PageInWeb.DownloadPage, "HomeVideoRight-posittion1", web, "RightContent", 2);

                AddCustomWebpart("FocusNews.webpart", Constants.PageInWeb.DownloadPage, "FocusNews-posittion1", web, "RightContent", 3);

                //Doanh nghiep tieu bieu
                AddCustomWebpart("FocusCompany.webpart", Constants.PageInWeb.DownloadPage, "FocusCompany-posittion1", web, "RightContent", 4);

                //doanh nghi?p theo catalogy 
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.DownloadPage, "CompanyListRight-posittion0", web, "RightContent", 5);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.DownloadPage, "CompanyListRight-posittion1", web, "RightContent", 6);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.DownloadPage, "CompanyListRight-posittion2", web, "RightContent", 7);

                //qu?ng cáo doanh nghi?p
                AddCustomWebpart("CompanyAdv.webpart", Constants.PageInWeb.DownloadPage, "CompanyAdv-posittion1", web, "RightContent", 8);

                AddCustomWebpart("ProvinceInfo.webpart", Constants.PageInWeb.GalleryPage, "ProvinceInfo-posittion1", web, "RightContent", 0);

                AddCustomWebpart("ProvinceDocs.webpart", Constants.PageInWeb.GalleryPage, "ProvinceDocsWebpart-posittion1", web, "RightContent", 1);

                AddCustomWebpart("HomeVideoRight.webpart", Constants.PageInWeb.GalleryPage, "HomeVideoRight-posittion1", web, "RightContent", 2);

                AddCustomWebpart("FocusNews.webpart", Constants.PageInWeb.GalleryPage, "FocusNews-posittion1", web, "RightContent", 3);

                //Doanh nghiep tieu bieu
                AddCustomWebpart("FocusCompany.webpart", Constants.PageInWeb.GalleryPage, "FocusCompany-posittion1", web, "RightContent", 4);

                //doanh nghi?p theo catalogy 
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.GalleryPage, "CompanyListRight-posittion0", web, "RightContent", 5);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.GalleryPage, "CompanyListRight-posittion1", web, "RightContent", 6);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.GalleryPage, "CompanyListRight-posittion2", web, "RightContent", 7);

                //qu?ng cáo doanh nghi?p
                AddCustomWebpart("CompanyAdv.webpart", Constants.PageInWeb.GalleryPage, "CompanyAdv-posittion1", web, "RightContent", 8);

                AddCustomWebpart("ProvinceInfo.webpart", Constants.PageInWeb.VideoPage, "ProvinceInfo-posittion1", web, "RightContent", 0);

                AddCustomWebpart("ProvinceDocs.webpart", Constants.PageInWeb.VideoPage, "ProvinceDocsWebpart-posittion1", web, "RightContent", 1);

                AddCustomWebpart("HomeVideoRight.webpart", Constants.PageInWeb.VideoPage, "HomeVideoRight-posittion1", web, "RightContent", 2);

                AddCustomWebpart("FocusNews.webpart", Constants.PageInWeb.VideoPage, "FocusNews-posittion1", web, "RightContent", 3);

                //Doanh nghiep tieu bieu
                AddCustomWebpart("FocusCompany.webpart", Constants.PageInWeb.VideoPage, "FocusCompany-posittion1", web, "RightContent", 4);

                //doanh nghi?p theo catalogy 
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.VideoPage, "CompanyListRight-posittion0", web, "RightContent", 5);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.VideoPage, "CompanyListRight-posittion1", web, "RightContent", 6);
                AddCustomWebpart("CompanyListRight.webpart", Constants.PageInWeb.VideoPage, "CompanyListRight-posittion2", web, "RightContent", 7);

                //qu?ng cáo doanh nghi?p
                AddCustomWebpart("CompanyAdv.webpart", Constants.PageInWeb.VideoPage, "CompanyAdv-posittion1", web, "RightContent", 8);
                #endregion

                #region LeftCorner
                //Quang cao
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.DefaultPage, "QNadv-posittion2", web, "LeftCorner", 0);

                //leftcontentconer
                AddCustomWebpart("LeftNewsContent.webpart", Constants.PageInWeb.DefaultPage, "LeftNewsContent-posittion0", web, "LeftCorner", 1);

                //Quang cao
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.DefaultPage, "QNadv-posittion3", web, "LeftCorner", 2);

                //leftcontentconer
                AddCustomWebpart("LeftNewsContent.webpart", Constants.PageInWeb.DefaultPage, "LeftNewsContent-posittion1", web, "LeftCorner", 3);

                //Quang cao
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.DefaultPage, "QNadv-posittion4", web, "LeftCorner", 4);

                //leftcontentconer
                AddCustomWebpart("LeftNewsContent.webpart", Constants.PageInWeb.DefaultPage, "LeftNewsContent-posittion2", web, "LeftCorner", 5);

                //Quang cao
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.DefaultPage, "QNadv-posittion5", web, "LeftCorner", 6);

                //leftcontentconer
                AddCustomWebpart("LeftNewsContent.webpart", Constants.PageInWeb.DefaultPage, "LeftNewsContent-posittion3", web, "LeftCorner", 7);

                //Quang cao
                AddCustomWebpart("QNadv.webpart", Constants.PageInWeb.DefaultPage, "QNadv-posittion6", web, "LeftCorner", 8);

                AddCustomWebpart("NewsList.webpart", Constants.PageInWeb.SubPage, "NewsList-posittion0", web, "LeftCorner", 0);
                AddCustomWebpart("OtherNews.webpart", Constants.PageInWeb.SubPage, "OtherNews-posittion0", web, "LeftCorner", 1);
                AddCustomWebpart("NextNews.webpart", Constants.PageInWeb.SubPage, "NextNews-posittion0", web, "LeftCorner", 2);

                AddCustomWebpart("NewsDetail.webpart", Constants.PageInWeb.DetailNews, "NewsDetail-posittion0", web, "LeftCorner", 0);
                AddCustomWebpart("OtherNews.webpart", Constants.PageInWeb.DetailNews, "OtherNews-posittion0", web, "LeftCorner", 1);
                AddCustomWebpart("NextNews.webpart", Constants.PageInWeb.DetailNews, "NextNews-posittion0", web, "LeftCorner", 2);

                AddCustomWebpart("Gallery.webpart", Constants.PageInWeb.GalleryPage, "Gallery-posittion0", web, "LeftCorner", 0);
                AddCustomWebpart("Video.webpart", Constants.PageInWeb.VideoPage, "Video-posittion0", web, "LeftCorner", 0);
                AddCustomWebpart("ShowDownload.webpart", Constants.PageInWeb.DownloadPage, "ShowDownload-posittion0", web, "LeftCorner", 0);
                AddCustomWebpart("Contact.webpart", Constants.PageInWeb.Contact, "Contact-posittion0", web, "LeftCorner", 0);

                #endregion

                #region RightCorner
                //Live tivi
                AddCustomWebpart("FactTV.webpart", Constants.PageInWeb.DefaultPage, "FactTV-posittion0", web, "RightCorner", 0);

                //Know information
                AddCustomWebpart("NeedToKnow.webpart", Constants.PageInWeb.DefaultPage, "NeedToKnow-posittion0", web, "RightCorner", 1);

                //Hom thu cong vu
                AddCustomWebpart("MailBox.webpart", Constants.PageInWeb.DefaultPage, "MailBox-posittion0", web, "RightCorner", 2);

                //Website link
                AddCustomWebpart("WebLink.webpart", Constants.PageInWeb.DefaultPage, "WebLink-posittion0", web, "RightCorner", 3);

                //TourInfo
                AddCustomWebpart("TourInfo.webpart", Constants.PageInWeb.DefaultPage, "TourInfo-posittion0", web, "RightCorner", 4);

                //Raovat
                AddCustomWebpart("ClassiFieds.webpart", Constants.PageInWeb.DefaultPage, "ClassiFieds-posittion0", web, "RightCorner", 5);

                //MostViewNews
                AddCustomWebpart("MostViewNews.webpart", Constants.PageInWeb.SubPage, "MostViewNews-posittion0", web, "RightCorner", 0);

                //Live tivi
                AddCustomWebpart("FactTV.webpart", Constants.PageInWeb.SubPage, "FactTV-posittion0", web, "RightCorner", 1);

                //Know information
                AddCustomWebpart("NeedToKnow.webpart", Constants.PageInWeb.SubPage, "NeedToKnow-posittion0", web, "RightCorner", 2);

                //Hom thu cong vu
                AddCustomWebpart("MailBox.webpart", Constants.PageInWeb.SubPage, "MailBox-posittion0", web, "RightCorner", 3);

                //Website link
                AddCustomWebpart("WebLink.webpart", Constants.PageInWeb.SubPage, "WebLink-posittion0", web, "RightCorner", 4);

                //TourInfo
                AddCustomWebpart("TourInfo.webpart", Constants.PageInWeb.SubPage, "TourInfo-posittion0", web, "RightCorner", 5);

                //Raovat
                AddCustomWebpart("ClassiFieds.webpart", Constants.PageInWeb.SubPage, "ClassiFieds-posittion0", web, "RightCorner", 6);

                //MostViewNews
                AddCustomWebpart("MostViewNews.webpart", Constants.PageInWeb.DetailNews, "MostViewNews-posittion0", web, "RightCorner", 0);

                //Live tivi
                AddCustomWebpart("FactTV.webpart", Constants.PageInWeb.DetailNews, "FactTV-posittion0", web, "RightCorner", 1);

                //Know information
                AddCustomWebpart("NeedToKnow.webpart", Constants.PageInWeb.DetailNews, "NeedToKnow-posittion0", web, "RightCorner", 2);

                //Hom thu cong vu
                AddCustomWebpart("MailBox.webpart", Constants.PageInWeb.DetailNews, "MailBox-posittion0", web, "RightCorner", 3);

                //Website link
                AddCustomWebpart("WebLink.webpart", Constants.PageInWeb.DetailNews, "WebLink-posittion0", web, "RightCorner", 4);

                //TourInfo
                AddCustomWebpart("TourInfo.webpart", Constants.PageInWeb.DetailNews, "TourInfo-posittion0", web, "RightCorner", 5);

                //Raovat
                AddCustomWebpart("ClassiFieds.webpart", Constants.PageInWeb.DetailNews, "ClassiFieds-posittion0", web, "RightCorner", 6);

                //Live tivi
                AddCustomWebpart("FactTV.webpart", Constants.PageInWeb.DownloadPage, "FactTV-posittion0", web, "RightCorner", 0);

                //Know information
                AddCustomWebpart("NeedToKnow.webpart", Constants.PageInWeb.DownloadPage, "NeedToKnow-posittion0", web, "RightCorner", 1);

                //Hom thu cong vu
                AddCustomWebpart("MailBox.webpart", Constants.PageInWeb.DownloadPage, "MailBox-posittion0", web, "RightCorner", 2);

                //Website link
                AddCustomWebpart("WebLink.webpart", Constants.PageInWeb.DownloadPage, "WebLink-posittion0", web, "RightCorner", 3);

                //TourInfo
                AddCustomWebpart("TourInfo.webpart", Constants.PageInWeb.DownloadPage, "TourInfo-posittion0", web, "RightCorner", 4);

                //Raovat
                AddCustomWebpart("ClassiFieds.webpart", Constants.PageInWeb.DownloadPage, "ClassiFieds-posittion0", web, "RightCorner", 5);

                //Live tivi
                AddCustomWebpart("FactTV.webpart", Constants.PageInWeb.GalleryPage, "FactTV-posittion0", web, "RightCorner", 0);

                //Know information
                AddCustomWebpart("NeedToKnow.webpart", Constants.PageInWeb.GalleryPage, "NeedToKnow-posittion0", web, "RightCorner", 1);

                //Hom thu cong vu
                AddCustomWebpart("MailBox.webpart", Constants.PageInWeb.GalleryPage, "MailBox-posittion0", web, "RightCorner", 2);

                //Website link
                AddCustomWebpart("WebLink.webpart", Constants.PageInWeb.GalleryPage, "WebLink-posittion0", web, "RightCorner", 3);

                //TourInfo
                AddCustomWebpart("TourInfo.webpart", Constants.PageInWeb.GalleryPage, "TourInfo-posittion0", web, "RightCorner", 4);

                //Raovat
                AddCustomWebpart("ClassiFieds.webpart", Constants.PageInWeb.GalleryPage, "ClassiFieds-posittion0", web, "RightCorner", 5);

                //Live tivi
                AddCustomWebpart("FactTV.webpart", Constants.PageInWeb.VideoPage, "FactTV-posittion0", web, "RightCorner", 0);

                //Know information
                AddCustomWebpart("NeedToKnow.webpart", Constants.PageInWeb.VideoPage, "NeedToKnow-posittion0", web, "RightCorner", 1);

                //Hom thu cong vu
                AddCustomWebpart("MailBox.webpart", Constants.PageInWeb.VideoPage, "MailBox-posittion0", web, "RightCorner", 2);

                //Website link
                AddCustomWebpart("WebLink.webpart", Constants.PageInWeb.VideoPage, "WebLink-posittion0", web, "RightCorner", 3);

                //TourInfo
                AddCustomWebpart("TourInfo.webpart", Constants.PageInWeb.VideoPage, "TourInfo-posittion0", web, "RightCorner", 4);

                //Raovat
                AddCustomWebpart("ClassiFieds.webpart", Constants.PageInWeb.VideoPage, "ClassiFieds-posittion0", web, "RightCorner", 5);

                #endregion

                AddCustomWebpart("ShowGallery.webpart", Constants.PageInWeb.ShowGalleryPage, "ShowGallery", web, "MainContent", 0);
                AddCustomWebpart("ShowVideo.webpart", Constants.PageInWeb.ShowVideoPage, "ShowGallery", web, "MainContent", 0);

                rootWeb.Update();
                web.Update();
            }
            catch (Exception)
            {
            }
        }

        public static void AddCustomWebpart(string webPartName, string pageName, string title, SPWeb web, string zoneId, int zoneIndex)
        {
            var webPart = WebPartHelper.GetWebPart(web, webPartName);
            if (string.IsNullOrEmpty(webPart.Title))
            {
                webPart.Title = title;
            }
            WebPartHelper.AddWebPart(web, string.Format("{0}.aspx", pageName), webPart, zoneId, zoneIndex);
        }

        private static void CreatePage(SPWeb web, string pageName)
        {
            WebPageHelper.CreateDefaultWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), false);
        }

        private static void CreateDetailPage(SPWeb web, string pageName)
        {
            WebPageHelper.CreateDetailWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), false);
        }

        public static void CreateNewDetailPage(SPWeb web, string pageName)
        {
            WebPageHelper.CreateNewDetailWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), true);
        }

        public static void CreateSubPage(SPWeb web, string pageName)
        {
            WebPageHelper.CreateSubWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), false);
        }

        public static void CreateBlankPage(SPWeb web, string pageName)
        {
            WebPageHelper.CreateBlankWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), false);
        }
    }
}
