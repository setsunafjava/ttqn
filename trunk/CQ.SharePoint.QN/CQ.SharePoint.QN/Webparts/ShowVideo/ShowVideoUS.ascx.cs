using System;
using System.Collections;
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

        protected void btnCopyResource_OnClick(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var enWeb = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        if (enWeb.Url.Contains("/en"))
                        {
                            var vnWeb = site.RootWeb;
                            var vnList = vnWeb.GetList(vnWeb.Url + "/" + ListsName.English.CQQNResources);
                            var enList = enWeb.GetList(enWeb.Url + "/" + ListsName.English.CQQNResources);
                            foreach (SPListItem currentSourceDocument in vnList.Items)
                            {
                                byte[] fileBytes = currentSourceDocument.File.OpenBinary();
                                string relativeDestinationUrl = enList.RootFolder.Url + "/" + currentSourceDocument.File.Name;
                                SPFile destinationFile = enList.RootFolder.Files.Add(relativeDestinationUrl, fileBytes, true);
                            }
                        }
                    }
                }
            });
        }

        protected void btnCopyCat_OnClick(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var enWeb = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        if (enWeb.Url.Contains("/en"))
                        {
                            var vnWeb = site.RootWeb;
                            var vnList = vnWeb.GetList(vnWeb.Url + "/Lists/" + ListsName.English.NewsCategory);
                            var enList = enWeb.GetList(enWeb.Url + "/Lists/" + ListsName.English.NewsCategory);
                            for (int i = 0; i < enList.ItemCount; i++)
                            {
                                var itemToDelete = enList.Items[0];
                                itemToDelete.Delete();
                            }
                            enList.Update();
                            var vnNewsList = vnWeb.GetList(vnWeb.Url + "/Lists/" + ListsName.English.NewsRecord);
                            var enNewsList = enWeb.GetList(enWeb.Url + "/Lists/" + ListsName.English.NewsRecord);
                            var catQuery = "<Where><IsNull><FieldRef Name='" +
                                           FieldsName.NewsCategory.English.ParentName + "' /></IsNull></Where>";
                            var query = new SPQuery();
                            query.Query = catQuery;
                            var vnListItems = vnList.GetItems(query);
                            foreach (SPListItem vnItem in vnListItems)
                            {
                                var enItem = enList.Items.Add();
                                enItem["Title"] = vnItem["Title"];
                                enItem[FieldsName.NewsCategory.English.Status] = vnItem[FieldsName.NewsCategory.English.Status];
                                enWeb.AllowUnsafeUpdates = true;
                                enItem.Update();
                                enWeb.AllowUnsafeUpdates = true;
                                vnWeb.AllowUnsafeUpdates = true;
                                CopyNews(vnItem.ID, enItem.ID, enList, vnNewsList, enNewsList);
                                enWeb.AllowUnsafeUpdates = true;
                                CopyCat(vnItem.ID, enItem.ID, vnList, enList, vnNewsList, enNewsList);
                            }
                        }
                    }
                }
            });
        }

        private void CopyCat(int vnCatID, int enCatID, SPList vnCatList, SPList enCatList, SPList vnNewsList, SPList enNewsList)
        {
            var enParentCat = new SPFieldLookupValue(enCatID, enCatList.GetItemById(enCatID).Title);
            string camlQuery = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='Lookup'>{1}</Value></Eq></Where>", FieldsName.NewsCategory.English.ParentName, vnCatID);
            var query = new SPQuery();
            query.Query = camlQuery;
            var vnListItems = vnCatList.GetItems(query);
            foreach (SPListItem item in vnListItems)
            {
                var enItem = enCatList.Items.Add();
                enItem["Title"] = item["Title"];
                enItem[FieldsName.NewsCategory.English.ParentName] = enParentCat;
                enItem[FieldsName.NewsCategory.English.Status] = item[FieldsName.NewsCategory.English.Status];
                enItem[FieldsName.NewsCategory.English.TypeCategory] = item[FieldsName.NewsCategory.English.TypeCategory];
                enItem.Update();
                CopyNews(item.ID, enItem.ID, enCatList, vnNewsList, enNewsList);
                CopyCat(item.ID, enItem.ID, vnCatList, enCatList, vnNewsList, enNewsList);
            }
        }

        private void CopyNews(int vnCatID, int enCatID, SPList enCatList, SPList vnNewsList, SPList enNewsList)
        {
            var enParentCat = new SPFieldLookupValue(enCatID, enCatList.GetItemById(enCatID).Title);
            string camlQuery = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='Lookup'>{1}</Value></Eq></Where>", FieldsName.NewsRecord.English.CategoryName, vnCatID);
            var query = new SPQuery();
            query.Query = camlQuery;
            var vnListItems = vnNewsList.GetItems(query);
            foreach (SPListItem item in vnListItems)
            {
                var enItem = enNewsList.Items.Add();
                enItem["Title"] = item["Title"];
                enItem[FieldsName.NewsRecord.English.CategoryName] = enParentCat;
                enItem[FieldsName.NewsRecord.English.PublishingPageContent] = item[FieldsName.NewsRecord.English.PublishingPageContent];
                enItem[FieldsName.NewsRecord.English.FocusNews] = item[FieldsName.NewsRecord.English.FocusNews];
                enItem[FieldsName.NewsRecord.English.LinkAdv] = item[FieldsName.NewsRecord.English.LinkAdv];
                enItem[FieldsName.NewsRecord.English.PublishingPageImage] = item[FieldsName.NewsRecord.English.PublishingPageImage];
                if (!string.IsNullOrEmpty(Convert.ToString(item[FieldsName.NewsRecord.English.PublishingPageContent])))
                {
                    enItem[FieldsName.NewsRecord.English.PublishingPageContent] = item[FieldsName.NewsRecord.English.PublishingPageContent];
                }
                else
                {
                    enItem[FieldsName.NewsRecord.English.PublishingPageContent] = item[FieldsName.NewsRecord.English.PublishingPageContent];
                    item[FieldsName.NewsRecord.English.PublishingPageContent] = item[FieldsName.NewsRecord.English.PublishingPageContent];
                    item.Update();
                }
                enItem[FieldsName.NewsRecord.English.ShortContent] = item[FieldsName.NewsRecord.English.ShortContent];
                enItem[FieldsName.NewsRecord.English.ShowInHomePage] = item[FieldsName.NewsRecord.English.ShowInHomePage];
                enItem[FieldsName.NewsRecord.English.Status] = item[FieldsName.NewsRecord.English.Status];
                enItem[FieldsName.NewsRecord.English.ThumbnailImage] = item[FieldsName.NewsRecord.English.ThumbnailImage];
                enItem.Update();
            }
        }
    }
}
