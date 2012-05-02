using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using CQ.SharePoint.QN.Common;
using CQ.SharePoint.QN.Core.Helpers;
using CQ.SharePoint.QN.Core.WebParts;
using System.Globalization;

namespace CQ.SharePoint.QN
{
    [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
    [SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
    class SiteStructure
    {
        public static void CreateSiteStructure(SPWeb web)
        {
            //Create Base Permission
            Console.WriteLine("Create Base Permission...");
            //CreateBasePermission(web);

            // Add group
            Console.WriteLine("Create user group...");

            DefaultStructure.Create(web);

            Console.WriteLine("Create lists:");

            Console.WriteLine("Create metadata lists...");

            Console.WriteLine("Create main lists...");

            NewsCategoryStructure.CreateListStructure(web);

            Console.WriteLine("Create pages...");

            PagesStructure.Create(web);

            Console.WriteLine("Store resource: JS & CSS...");
            UpdateResources(web);

            // Add QuichLaunch
            Console.WriteLine("Process CreateQuichLaunch..");
            QuickLaunchStructure.CreateQuickLaunch(web);

            Console.WriteLine("Deploy Successful!");
        }

        #region Create DocLib store JS & CSS
        private static void UpdateResources(SPWeb web)
        {
            SPList sitePages = null;
            try
            {
                string listUrl = web.Url + "/" + ListsName.English.CQQNResources;
                sitePages = web.GetList(listUrl);
            }
            catch (Exception ex)
            {
                //Utilities.LogToUls(ex);
            }

            if (sitePages == null)
            {
                Guid listId = web.Lists.Add(ListsName.English.CQQNResources, string.Empty, SPListTemplateType.DocumentLibrary);
                sitePages = web.Lists[listId];
                sitePages.Title = ListsName.VietNamese.CQQNResources;
                sitePages.Update();
            }

            if (!sitePages.Fields.ContainsField(FieldsName.CQQNResources.Japanese.ResourceType))
            {
                sitePages.Fields.Add(FieldsName.CQQNResources.English.ResourceType, SPFieldType.Choice, true);
                var field = (SPFieldChoice)sitePages.Fields.GetFieldByInternalName(FieldsName.CQQNResources.English.ResourceType);

                StringCollection Choices = new StringCollection
                                               {
                                                   FieldsName.CQQNResources.FieldValuesDefault.Type.JS,
                                                   FieldsName.CQQNResources.FieldValuesDefault.Type.CSS,
                                                   FieldsName.CQQNResources.FieldValuesDefault.Type.IMAGE,
                                                   FieldsName.CQQNResources.FieldValuesDefault.Type.TEMPLATE,
                                                   FieldsName.CQQNResources.FieldValuesDefault.Type.XML
                                               };
                foreach (var choice in Choices)
                {
                    field.Choices.Add(choice);
                }

                field.Title = FieldsName.CQQNResources.Japanese.ResourceType;
                field.Update();
            }

            // Upload files
            string resourcesPathCss = SPUtility.GetGenericSetupPath("TEMPLATE\\FEATURES\\CQ.SharePoint.QN\\Resources\\Css");
            string resourcesPathJavascript = SPUtility.GetGenericSetupPath("TEMPLATE\\FEATURES\\CQ.SharePoint.QN\\Resources\\Javascript");
            string resourcesPathImage = SPUtility.GetGenericSetupPath("TEMPLATE\\FEATURES\\CQ.SharePoint.QN\\Resources\\Images");

            UploadFileToDocumentLibrary(resourcesPathCss, web, FieldsName.CQQNResources.FieldValuesDefault.Type.CSS);
            UploadFileToDocumentLibrary(resourcesPathJavascript, web, FieldsName.CQQNResources.FieldValuesDefault.Type.JS);
            UploadFileToDocumentLibrary(resourcesPathImage, web, FieldsName.CQQNResources.FieldValuesDefault.Type.IMAGE);
        }

        /// <summary>
        /// This function is used to upload file document library
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetDocumentLibraryPath"></param>
        /// <param name="web"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void UploadFileToDocumentLibrary(string sourceFilePath, string targetDocumentLibraryPath, SPWeb web, string type, string name)
        {
            // Create buffer to transfer file
            byte[] fileBuffer = new byte[1024];
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    //Load the content from local file to stream
                    using (FileStream fsWorkbook = File.Open(sourceFilePath, FileMode.Open, FileAccess.Read))
                    {
                        //Get the start point
                        int startBuffer = fsWorkbook.Read(fileBuffer, 0, fileBuffer.Length);
                        for (int i = startBuffer; i > 0; i = fsWorkbook.Read(fileBuffer, 0, fileBuffer.Length))
                        {
                            stream.Write(fileBuffer, 0, i);
                        }
                    }

                    web.AllowUnsafeUpdates = true;
                    SPFile file = web.Files.Add(targetDocumentLibraryPath, stream.ToArray());
                    file.Item[FieldsName.CQQNResources.English.ResourceType] = type;
                    file.Item[SPBuiltInFieldId.FileLeafRef] = name;
                    web.AllowUnsafeUpdates = true;
                    file.Item.Update();
                }
            }
            catch (Exception ex)
            {
                //Utilities.LogToUls(ex);
            }
        }

        public static void UploadFileToDocumentLibrary(string sourceFolderPath, SPWeb web, string type)
        {
            var sourceFolder = new DirectoryInfo(sourceFolderPath);
            var sourceFile = sourceFolder.GetFiles();
            if (sourceFile.Length > 0)
            {
                foreach (FileInfo file in sourceFolder.GetFiles())
                {
                    UploadFileToDocumentLibrary(file.FullName,
                                                string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", web.Url,
                                                              ListsName.English.CQQNResources, file.Name),
                                                web,
                                                type, file.Name);
                }
            }

        }
        #endregion
    }
}