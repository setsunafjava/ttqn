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
            ////System.Diagnostics.Debugger.Launch();
            //CreatePage(web, "default");
            //var rootWeb = web.RootFolder;
            //rootWeb.WelcomePage = "default.aspx";
            //rootWeb.Update();
            //web.Update();
            try
            {
                //System.Diagnostics.Debugger.Break();
                CreatePage(web, "default");
                var rootWeb = web.RootFolder;
                rootWeb.WelcomePage = "default.aspx";
                rootWeb.Update();

                web.Update();
                var focusNews = WebPartHelper.GetWebPart(web, "FocusNews.webpart");
                if (string.IsNullOrEmpty(focusNews.Title))
                {
                    focusNews.Title = "focusNews-vitridautien";
                }
                WebPartHelper.AddWebPart(web, "default.aspx", focusNews, "LeftCorner", 1);
                rootWeb.Update();
                web.Update();
            }
            catch (Exception)
            {
            }
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
