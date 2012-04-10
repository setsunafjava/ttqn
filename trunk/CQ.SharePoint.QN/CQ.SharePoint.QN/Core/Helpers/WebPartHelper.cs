using System;
using System.Web.UI.WebControls.WebParts;
using CQ.SharePoint.QN.Core.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;
using WebPart = System.Web.UI.WebControls.WebParts.WebPart;

namespace CQ.SharePoint.QN.Core.Helpers
{
    /// <summary>
    /// </summary>
    public static class WebPartHelper
    {
        /// <summary>
        ///   Get a WebPart instance from WebPart gallery.
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "webPartName"></param>
        /// <returns></returns>
        public static WebPart GetWebPart(SPWeb web, string webPartName)
        {
            var query = new SPQuery
            {
                Query =
                    "<Where><Eq><FieldRef Name='FileLeafRef'/><Value Type='File'>" + webPartName +
                    "</Value></Eq></Where>"
            };
            var webPartGallery = web.Site.RootWeb.GetCatalog(SPListTemplateType.WebPartCatalog);
            var webParts = webPartGallery.GetItems(query);
            if (webParts.Count == 0)
            {
                return null;
            }

            var typeName = webParts[0].GetFormattedValue("WebPartTypeName");
            var assemblyName = webParts[0].GetFormattedValue("WebPartAssembly");

            var webPartHandle = Activator.CreateInstance(assemblyName, typeName);
            if (webPartHandle == null)
            {
                return null;
            }

            var webPart = (WebPart)webPartHandle.Unwrap();
            return webPart;
        }

        /// <summary>
        ///   Get container web part instance
        /// </summary>
        /// <param name = "web"></param>
        /// <returns></returns>
        public static ContainerWebPart GetContainerWebPart(SPWeb web)
        {
            return new ContainerWebPart();
        }

        public static string AddWebPart(SPWeb web, string pageUrl, string webPartName, string zoneId, int zoneIndex)
        {
            var webPart = GetWebPart(web, webPartName);
            if (webPart == null)
            {
                return null;
            }

            return AddWebPart(web, pageUrl, webPart, zoneId, zoneIndex);
        }

        public static string AddWebPart(SPWeb web, string pageUrl, WebPart webPart, string zoneId, int zoneIndex)
        {
            if (string.IsNullOrEmpty(webPart.Title))
            {
                throw new ArgumentException("The WebPart must be has title.");
            }

            var page = web.GetFile(pageUrl);

            using (var webPartManager = page.GetLimitedWebPartManager(PersonalizationScope.Shared))
            {
                try
                {
                    var oldWebParts = webPartManager.WebParts;
                    foreach (WebPart oldWebPart in oldWebParts)
                    {
                        if (oldWebPart.Title.Equals(webPart.Title))
                        {
                            webPartManager.DeleteWebPart(oldWebPart);
                        }
                    }

                    webPartManager.AddWebPart(webPart, zoneId, zoneIndex);

                    return webPart.ID;
                }
                finally
                {
                    webPartManager.Web.Dispose();
                }
            }
        }

        /// <summary>
        ///   Add web part to new page
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "list"></param>
        /// <param name = "webPart"></param>
        /// <returns></returns>
        public static string AddWebPartToNewPage(SPWeb web, SPList list, WebPart webPart)
        {
            var url = list.Forms[PAGETYPE.PAGE_NEWFORM].Url;
            return AddWebPart(web, url, webPart, "Main", 0);
        }

        /// <summary>
        ///   Add web part to display page
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "list"></param>
        /// <param name = "webPart"></param>
        /// <returns></returns>
        public static string AddWebPartToDisplayPage(SPWeb web, SPList list, WebPart webPart)
        {
            var url = list.Forms[PAGETYPE.PAGE_DISPLAYFORM].Url;
            return AddWebPart(web, url, webPart, "Main", 0);
        }

        /// <summary>
        ///   Add web part to edit page
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "list"></param>
        /// <param name = "webPart"></param>
        /// <returns></returns>
        public static string AddWebPartToEditPage(SPWeb web, SPList list, WebPart webPart)
        {
            var url = list.Forms[PAGETYPE.PAGE_EDITFORM].Url;
            return AddWebPart(web, url, webPart, "Main", 0);
        }

        public static string AddWebPartToViewPage(SPWeb web, SPView view, WebPart webPart)
        {
            return AddWebPart(web, view.Url, webPart, "Main", 0);
        }

        public static string AddWebPartToViewPage(SPWeb web, SPList list, string viewName, WebPart webPart)
        {
            var view = list.Views[viewName];
            return AddWebPartToViewPage(web, view, webPart);
        }

        public static void HideWebPart(SPWeb web, string pageUrl, string webPartTitle)
        {
            var page = web.GetFile(pageUrl);
            using (var webPartManager = page.GetLimitedWebPartManager(PersonalizationScope.Shared))
            {
                try
                {
                    foreach (WebPart webPart in webPartManager.WebParts)
                    {
                        if (webPart.Title == webPartTitle && !webPart.Hidden)
                        {
                            webPart.Hidden = true;
                            webPartManager.SaveChanges(webPart);
                        }
                    }
                }
                finally
                {
                    webPartManager.Web.Dispose();
                }
            }
        }

        public static void HideWebPart<TW>(SPWeb web, string pageUrl) where TW : WebPart
        {
            var page = web.GetFile(pageUrl);
            using (var webPartManager = page.GetLimitedWebPartManager(PersonalizationScope.Shared))
            {
                try
                {
                    foreach (WebPart webPart in webPartManager.WebParts)
                    {
                        if (webPart.GetType() == typeof(TW) && !webPart.Hidden)
                        {
                            webPart.Hidden = true;
                            webPartManager.SaveChanges(webPart);
                        }
                    }
                }
                finally
                {
                    webPartManager.Web.Dispose();
                }
            }
        }

        /// <summary>
        ///   Hide default web part on new page
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "list"></param>
        public static void HideDefaultWebPartOnNewPage(SPWeb web, SPList list)
        {
            var url = list.Forms[PAGETYPE.PAGE_NEWFORM].Url;
            HideWebPart<ListFormWebPart>(web, url);
        }

        /// <summary>
        ///   Hide default web part on display page
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "list"></param>
        public static void HideDefaultWebPartOnDisplayPage(SPWeb web, SPList list)
        {
            var url = list.Forms[PAGETYPE.PAGE_DISPLAYFORM].Url;
            HideWebPart<ListFormWebPart>(web, url);
        }

        /// <summary>
        ///   Hide default web part on edit page
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "list"></param>
        public static void HideDefaultWebPartOnEditPage(SPWeb web, SPList list)
        {
            var url = list.Forms[PAGETYPE.PAGE_EDITFORM].Url;
            HideWebPart<ListFormWebPart>(web, url);
        }

        /// <summary>
        ///   Show default web part on new page
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "list"></param>
        public static void ShowDefaultWebPartOnNewPage(SPWeb web, SPList list)
        {
            var url = list.Forms[PAGETYPE.PAGE_NEWFORM].Url;
            ShowWebPart<ListFormWebPart>(web, url);
        }

        /// <summary>
        ///   Show default web part on display page
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "list"></param>
        public static void ShowDefaultWebPartOnDisplayPage(SPWeb web, SPList list)
        {
            var url = list.Forms[PAGETYPE.PAGE_DISPLAYFORM].Url;
            ShowWebPart<ListFormWebPart>(web, url);
        }

        /// <summary>
        ///   Show default web part on edit page
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "list"></param>
        public static void ShowDefaultWebPartOnEditPage(SPWeb web, SPList list)
        {
            var url = list.Forms[PAGETYPE.PAGE_EDITFORM].Url;
            ShowWebPart<ListFormWebPart>(web, url);
        }

        public static void ShowWebPart<TW>(SPWeb web, string pageUrl) where TW : WebPart
        {
            var page = web.GetFile(pageUrl);
            using (var webPartManager = page.GetLimitedWebPartManager(PersonalizationScope.Shared))
            {
                try
                {
                    foreach (WebPart webPart in webPartManager.WebParts)
                    {
                        if (webPart.GetType() == typeof(TW) && webPart.Hidden)
                        {
                            webPart.Hidden = false;
                            webPartManager.SaveChanges(webPart);
                        }
                    }
                }
                finally
                {
                    webPartManager.Web.Dispose();
                }
            }
        }
    }
}