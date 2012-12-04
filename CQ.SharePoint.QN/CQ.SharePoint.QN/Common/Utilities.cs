﻿using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using CQ.SharePoint.QN.Core.Helpers;
using CQ.SharePoint.QN.Core.WebParts;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint.Publishing.Fields;

namespace CQ.SharePoint.QN.Common
{
    public class Utilities
    {
        /// <summary>
        /// Create a standard SharePoint view and add to given list
        /// </summary>
        /// <param name="list">List instance that contains added view</param>
        /// <param name="viewTitle"></param>
        /// <param name="viewFields">Fields display on view</param>
        /// <param name="query">Filter and Groupby query</param>
        /// <param name="rowLimit"></param>
        /// <param name="makeDefaultView">TRUE to make view is default view of list</param>
        public static void AddStandardView(SPList list, string viewTitle, string[] viewFields, string query, int rowLimit, bool makeDefaultView)
        {
            AddStandardView(list, viewTitle, viewFields, query, rowLimit, makeDefaultView, String.Empty);
        }

        public static DataTable GetTableWithCorrectUrl(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string thumbnailImage = string.Empty;
                string publishingPageImage = string.Empty;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    thumbnailImage = Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage]);
                    publishingPageImage = Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.PublishingPageImage]);

                    if (thumbnailImage.Length > 2)
                    {
                        dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = thumbnailImage.Trim().Substring(0, thumbnailImage.Length - 2);
                    }
                    else if (publishingPageImage.Length > 2)
                    {
                        var t = publishingPageImage.Split(' ')[3];
                        dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = t.Trim().Substring(5, t.Length - 6);
                    }
                }
            }
            return dataTable;
        }

        public static DataTable GetTableWithCorrectUrlDoclib(SPWeb web, DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string imagepath = string.Empty;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    imagepath = Convert.ToString(dataTable.Rows[i][FieldsName.QuangCaoRaoVat.English.LinkFileName]);
                    var extFile = imagepath.Substring(imagepath.Length - 3, 3);
                    var fileName = imagepath.Substring(0, imagepath.Length - 4);
                    dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = string.Format("{0}/{1}/_t/{2}_{3}.jpg", web.Url, ListsName.English.QuangCaoRaoVat, fileName, extFile);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Get and return table with correct url
        /// </summary>
        /// <param name="categoryListName">categoryListName</param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable GetTableWithCorrectUrl(string categoryListName, SPListItemCollection items)
        {
            var dataTable = items.GetDataTable();

            if (!dataTable.Columns.Contains(FieldsName.CategoryId))
            {
                dataTable.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
            }

            if (!dataTable.Columns.Contains(FieldsName.ArticleStartDateTemp))
            {
                dataTable.Columns.Add(FieldsName.ArticleStartDateTemp, Type.GetType("System.String"));
            }

            if (items != null && items.Count > 0)
            {
                string imagepath = string.Empty;
                ImageFieldValue imageIcon;
                SPFieldUrlValue advLink;
                DateTime time = new DateTime();

                for (int i = 0; i < items.Count; i++)
                {
                    imagepath = Convert.ToString(items[i][FieldsName.NewsRecord.English.ThumbnailImage]);

                    imageIcon = items[i][FieldsName.NewsRecord.English.PublishingPageImage] as ImageFieldValue;
                    if (imageIcon != null)
                    {
                        dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = GetThumbnailImagePath(imageIcon.ImageUrl);
                    }
                    else
                    {
                        if (imagepath.Length > 2)
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length - 2);
                        else
                        {
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath;
                        }
                    }
                    if (items[i].Fields.ContainsField(FieldsName.NewsRecord.English.LinkAdv))
                    {
                        advLink = new SPFieldUrlValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.LinkAdv]));
                        dataTable.Rows[i][FieldsName.NewsRecord.English.LinkAdv] = advLink.Url;
                    }
                    //dataTable.Rows[i][FieldsName.CategoryId] = GetCategoryIdByCategoryName(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.CategoryName]), categoryListName);
                    if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
                    {
                        SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
                        dataTable.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
                    }

                    time = Convert.ToDateTime(dataTable.Rows[i][FieldsName.Created]);
                    dataTable.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format("Ngày {0}/{1}/{2}", time.Day, time.Month, time.Year);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Get and return table with correct url
        /// </summary>
        /// <param name="categoryListName">categoryListName</param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable GetTableWithCorrectUrl(string categoryListName, SPListItemCollection items, bool thumbnail)
        {
            var dataTable = items.GetDataTable();

            if (!dataTable.Columns.Contains(FieldsName.CategoryId))
            {
                dataTable.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
            }

            if (!dataTable.Columns.Contains(FieldsName.ArticleStartDateTemp))
            {
                dataTable.Columns.Add(FieldsName.ArticleStartDateTemp, Type.GetType("System.String"));
            }

            if (items != null && items.Count > 0)
            {
                string imagepath = string.Empty;
                ImageFieldValue imageIcon;
                SPFieldUrlValue advLink;
                var time = new DateTime();

                for (int i = 0; i < items.Count; i++)
                {
                    imagepath = Convert.ToString(items[i][FieldsName.NewsRecord.English.ThumbnailImage]);

                    imageIcon = items[i][FieldsName.NewsRecord.English.PublishingPageImage] as ImageFieldValue;
                    if (imageIcon != null && thumbnail)
                    {
                        dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imageIcon.ImageUrl;
                    }
                    else
                    {
                        if (imagepath.Length > 2)
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length - 2);
                        else
                        {
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath;
                        }
                    }
                    if (items[i].Fields.ContainsField(FieldsName.NewsRecord.English.LinkAdv))
                    {
                        advLink = new SPFieldUrlValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.LinkAdv]));
                        dataTable.Rows[i][FieldsName.NewsRecord.English.LinkAdv] = advLink.Url;
                    }
                    //dataTable.Rows[i][FieldsName.CategoryId] = GetCategoryIdByCategoryName(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.CategoryName]), categoryListName);
                    if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
                    {
                        SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
                        dataTable.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
                    }

                    time = Convert.ToDateTime(dataTable.Rows[i][FieldsName.Created]);
                    dataTable.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Get and return table with correct url
        /// </summary>
        /// <param name="categoryListName">categoryListName</param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable GetTableWithCorrectUrlHotNews(string categoryListName, SPListItemCollection items)
        {
            var dataTable = items.GetDataTable();

            if (!dataTable.Columns.Contains(FieldsName.CategoryId))
            {
                dataTable.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
            }

            if (items != null && items.Count > 0)
            {
                string imagepath = string.Empty;
                ImageFieldValue imageIcon;
                SPFieldUrlValue advLink;

                for (int i = 0; i < items.Count; i++)
                {
                    imagepath = Convert.ToString(items[i][FieldsName.NewsRecord.English.ThumbnailImage]);

                    imageIcon = items[i][FieldsName.NewsRecord.English.PublishingPageImage] as ImageFieldValue;
                    if (imageIcon != null)
                    {
                        dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imageIcon.ImageUrl;
                    }
                    else
                    {
                        if (imagepath.Length > 2)
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length - 2);
                        else
                        {
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath;
                        }
                    }
                    if (items[i].Fields.ContainsField(FieldsName.NewsRecord.English.LinkAdv))
                    {
                        advLink = new SPFieldUrlValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.LinkAdv]));
                        dataTable.Rows[i][FieldsName.NewsRecord.English.LinkAdv] = advLink.Url;
                    }

                    dataTable.Rows[i][FieldsName.NewsRecord.English.ShortContent] = StripHtml(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.ShortContent]));

                    //dataTable.Rows[i][FieldsName.CategoryId] = GetCategoryIdByCategoryName(Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.CategoryName]), categoryListName);
                    if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
                    {
                        SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
                        dataTable.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
                    }
                }
            }
            return dataTable;
        }

        public static string StripHtml(string txt)
        {
            return Regex.Replace(txt, "<(.|\\n)*?>", string.Empty);
        }

        /// <summary>
        /// Get and return table with correct url
        /// </summary>
        /// <param name="categoryListName">categoryListName</param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable GetTableWithCorrectUrlHotNews(SPListItemCollection items)
        {
            var dataTable = items.GetDataTable();

            if (items != null && items.Count > 0)
            {
                string imagepath = string.Empty;
                ImageFieldValue imageIcon;
                SPFieldUrlValue advLink;

                for (int i = 0; i < items.Count; i++)
                {
                    imageIcon = items[i][FieldsName.NewsRecord.English.PublishingPageImage] as ImageFieldValue;
                    if (imageIcon != null)
                    {
                        dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imageIcon.ImageUrl;
                    }
                    else
                    {
                        if (imagepath.Length > 2)
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length - 2);
                        else
                        {
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath;
                        }
                    }
                    if (items[i].Fields.ContainsField(FieldsName.NewsRecord.English.LinkAdv))
                    {
                        advLink = new SPFieldUrlValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.LinkAdv]));
                        dataTable.Rows[i][FieldsName.NewsRecord.English.LinkAdv] = advLink.Url;
                    }
                }
            }
            return dataTable;
        }


        /// <summary>
        /// Get thumbnail path for image, it's have path like: /NewsImagesList/_t/gap%20SNV_jpg.jpg
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static string GetThumbnailImagePath(string imagePath)
        {
            //var thumbnailPath = string.Empty;
            //if (imagePath.Length > 0 && !imagePath.Contains("/_t/"))
            //{
            //    var imageArray = imagePath.Split('/');
            //    var extentFileName = imageArray[2].Substring(imageArray[2].Length - 3, 3);
            //    var filename = imageArray[2].Substring(0, imageArray[2].Length - 4);
            //    thumbnailPath = string.Format("/{0}/_t/{1}_{2}.jpg", imageArray[1], filename, extentFileName);
            //}
            return imagePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="sapo"></param>
        /// <returns></returns>
        public static DataTable GetTableWithCorrectUrl(SPListItemCollection items, bool sapo)
        {
            var dataTable = items.GetDataTable();

            if (items != null && items.Count > 0)
            {
                string imagepath = string.Empty;
                ImageFieldValue imageIcon;

                for (int i = 0; i < items.Count; i++)
                {
                    imagepath = Convert.ToString(items[i][FieldsName.NewsRecord.English.ThumbnailImage]);
                    imageIcon = items[i][FieldsName.NewsRecord.English.PublishingPageImage] as ImageFieldValue;
                    dataTable.Rows[i][FieldsName.Title] = GetTextForSapo(Convert.ToString(items[i][FieldsName.Title]), 55);

                    if (imageIcon != null)
                        dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imageIcon.ImageUrl;
                    else
                    {
                        if (imagepath.Length > 2)
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length - 2);
                        else
                        {
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath;
                        }
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sapo"></param>
        /// <returns></returns>
        public static DataTable GetTableWithCorrectUrl(DataTable dataTable, bool sapo)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string imagepath = string.Empty;
                string imageIcon = string.Empty;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    imagepath = Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage]);
                    var test = Convert.ToString(dataTable.Rows[i][FieldsName.NewsRecord.English.PublishingPageImage]);
                    if (test != null)
                    {
                        var te = test.Split(' ');
                        foreach (var s in te)
                        {
                            if (s.Contains("src="))
                            {
                                imageIcon = s.Substring(5, s.Length - 6);
                            }
                        }
                    }
                    //imageIcon = dataTable.Rows[i][FieldsName.NewsRecord.English.PublishingPageImage] as ImageFieldValue;
                    dataTable.Rows[i][FieldsName.Title] = GetTextForSapo(Convert.ToString(dataTable.Rows[i][FieldsName.Title]), 55);

                    if (!string.IsNullOrEmpty(imageIcon))
                        dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imageIcon;
                    else
                    {
                        if (imagepath.Length > 2)
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length - 2);
                        else
                        {
                            dataTable.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath;
                        }
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Cut many text and set text is sapo
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="textLength"></param>
        /// <returns></returns>
        public static string GetTextForSapo(string strInput, int textLength)
        {
            StringBuilder strResult = new StringBuilder();
            string[] inputArray = null;
            if (!string.IsNullOrEmpty(strInput))
            {
                if (strInput.Length > textLength)
                {
                    inputArray = strInput.Split(' ');
                    int numLength = 0;
                    int i = 0;
                    while (numLength < textLength)
                    {
                        strResult.Append(string.Format("{0} ", inputArray[i]));
                        numLength = strResult.Length;
                        i++;
                    }
                    strResult.Append("...");
                }
                else
                {
                    strResult.Append(strInput);
                }
            }
            return Convert.ToString(strResult);
        }

        /// <summary>
        /// Create a standard SharePoint view and add to given list
        /// </summary>
        /// <param name="list">List instance that contains added view</param>
        /// <param name="viewTitle"></param>
        /// <param name="viewFields">Fields display on view</param>
        /// <param name="query">Filter and Groupby query</param>
        /// <param name="rowLimit"></param>
        /// <param name="makeDefaultView">TRUE to make view is default view of list</param>
        /// <param name="aggregations">Aggregation Expression String</param>
        public static void AddStandardView(SPList list, string viewTitle, string[] viewFields, string query, int rowLimit, bool makeDefaultView, String aggregations)
        {
            SPViewCollection availableViews = list.Views;
            SPView view;

            try
            {
                view = availableViews[viewTitle];

                // If view exsited, update new view fields

                if (viewFields != null)
                {
                    view.ViewFields.DeleteAll();
                    foreach (string viewField in viewFields)
                        view.ViewFields.Add(viewField);
                }
            }
            catch
            {
                // If view not exsited, 
                // Create new view

                var colViewCollection = new StringCollection();
                if (viewFields != null) colViewCollection.AddRange(viewFields);
                view = availableViews.Add(viewTitle, colViewCollection, null, (uint)rowLimit, true, makeDefaultView);
            }

            if (view != null)
            {
                // Update view filter

                if (!string.IsNullOrEmpty(query))
                {
                    view.Query = query;
                }

                view.RowLimit = (uint)rowLimit;
                view.DefaultView = makeDefaultView;
                if (!String.IsNullOrEmpty(aggregations))
                {
                    view.Aggregations = aggregations;
                    view.AggregationsStatus = "No";
                }

                view.Update();
            }
        }

        public static void AddForms(SPWeb web, SPList list, string userControlPath)
        {
            ContainerWebPart containerWebPart = WebPartHelper.GetContainerWebPart(web);

            if (containerWebPart != null)
            {
                containerWebPart.Title = string.Format(CultureInfo.InvariantCulture, "{0} - Custom Form", list.Title);
                containerWebPart.UserControlPath = userControlPath;
                WebPartHelper.AddWebPartToNewPage(web, list, containerWebPart);
                WebPartHelper.AddWebPartToEditPage(web, list, containerWebPart);
                WebPartHelper.AddWebPartToDisplayPage(web, list, containerWebPart);
            }

            WebPartHelper.HideDefaultWebPartOnNewPage(web, list);
            WebPartHelper.HideDefaultWebPartOnEditPage(web, list);
            WebPartHelper.HideDefaultWebPartOnDisplayPage(web, list);
        }

        /// <summary>
        /// Gets User of People Picker field (Single choice)
        /// </summary>
        /// <param name="item">Current item</param>
        /// <param name="fieldName">Field Name</param>
        /// <returns>Return SPUser</returns>
        public static SPUser GetUserByField(SPListItem item, string fieldName)
        {
            var value = Convert.ToString(item[fieldName], CultureInfo.InvariantCulture);

            if (string.IsNullOrEmpty(value)) return null;

            var userFieldValue = new SPFieldUserValue(item.Web, value);
            if (userFieldValue != null && userFieldValue.User != null)
                return userFieldValue.User;

            return null;
        }

        /// <summary>
        /// Gets User of People Picker field (Multi )
        /// </summary>
        /// <param name="item">Current item</param>
        /// <param name="fieldName">Field Name</param>
        /// <returns>Return SPUserCollection</returns>
        public static SPFieldUserValueCollection GetUserValueCollectionByField(SPListItem item, string fieldName)
        {
            var value = Convert.ToString(item[fieldName], CultureInfo.InvariantCulture);

            if (string.IsNullOrEmpty(value)) return null;

            var userFieldValueCollection = new SPFieldUserValueCollection(item.Web, value);
            if (userFieldValueCollection != null && userFieldValueCollection.Count > 0)
            {
                return userFieldValueCollection;
            }

            return null;
        }
        /// <summary>
        /// Log exception to SharePoint log
        /// </summary>
        /// <param name="value"></param>
        public static void LogToUls(string value)
        {
            //SPDiagnosticsService diagnosticsService = SPDiagnosticsService.Local;
            //SPDiagnosticsCategory category = diagnosticsService.Areas["SharePoint Foundation"].Categories["General"];
            //diagnosticsService.(93, category, TraceSeverity.High, "CQ.SharePoint.QN: " + value, null);
        }

        /// <summary>
        /// LogToULS
        /// </summary>
        /// <param name="ex"></param>
        public static void LogToUls(Exception ex)
        {
            //var diagnosticsService = SPDiagnosticsService.Local;
            //var category = diagnosticsService.Areas["SharePoint Foundation"].Categories["General"];
            //diagnosticsService.WriteTrace(93, category, TraceSeverity.High, "CQ.SharePoint.QN: " + ex, null);
        }

        /// <summary>
        /// Get all metadata of choice field from SP List
        /// </summary>
        /// <param name="list">Current List</param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="defaultValue">Default value of field</param>
        /// <returns></returns>
        public static StringCollection GetMetaDataOfChoiceField(SPList list, string fieldName, out string defaultValue)
        {
            defaultValue = string.Empty;
            var values = new StringCollection();

            SPFieldCollection fields = list.Fields;

            if (fields.ContainsField(fieldName))
            {
                var fieldChoice = (SPFieldChoice)fields[fieldName];
                values = fieldChoice.Choices;

                defaultValue = fieldChoice.DefaultValue;
            }

            return values;
        }

        /// <summary>
        /// Apply permission for list
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="list">Current list</param>
        /// <param name="groupsAndPermissions">Text - Group Name; Array ListItem: Value - Permission Level</param>
        public static void AplyPermissionForLists(SPWeb web, SPList list, params ListItem[] groupsAndPermissions)
        {
            if (groupsAndPermissions.Length <= 0) return;

            if (!list.HasUniqueRoleAssignments)
            {
                web.AllowUnsafeUpdates = true;
                list.BreakRoleInheritance(false);
            }
            else
            {
                web.AllowUnsafeUpdates = true;
                list.ResetRoleInheritance();
                web.AllowUnsafeUpdates = true;
                list.BreakRoleInheritance(false);
            }

            SPRoleAssignmentCollection roleAssignments = list.RoleAssignments;

            foreach (ListItem item in groupsAndPermissions)
            {
                try
                {
                    SPRoleAssignment roleAssignment = new SPRoleAssignment(web.SiteGroups[item.Text]);
                    SPRoleDefinition roleDefinition = web.RoleDefinitions[item.Value];
                    roleAssignment.RoleDefinitionBindings.Add(roleDefinition);

                    roleAssignments.Add(roleAssignment);
                }
                catch
                {
                    continue;
                }
            }

            web.AllowUnsafeUpdates = true;
            list.Update();
        }

        /// <summary>
        /// AddPermissionForList
        /// </summary>
        /// <param name="web"></param>
        /// <param name="list"></param>
        /// <param name="groupName"></param>
        /// <param name="roleType"></param>
        public static void AddPermissionForList(SPWeb web, SPList list, string groupName, SPRoleType roleType)
        {
            if (!list.HasUniqueRoleAssignments)
            {
                web.AllowUnsafeUpdates = true;
                list.BreakRoleInheritance(false);
            }

            SPGroup group = web.SiteGroups[groupName];
            SPRoleDefinition roleDefinition = web.RoleDefinitions.GetByType(roleType);
            SPRoleAssignment roleAssignment = new SPRoleAssignment(group);
            roleAssignment.RoleDefinitionBindings.Add(roleDefinition);

            list.RoleAssignments.Add(roleAssignment);
            list.Update();
            web.Update();
        }

        /// <summary>
        /// Add Permission For Group
        /// </summary>
        /// <param name="web">current web</param>
        /// <param name="groupName">group name</param>
        /// <param name="permssionLevel">permission level name</param>
        public static void AddPermissionForGroup(SPWeb web, string groupName, string permssionLevel)
        {
            SPRoleDefinition roleDefinition = web.RoleDefinitions[permssionLevel];

            var assignment = new SPRoleAssignment(web.SiteGroups[groupName]);
            assignment.RoleDefinitionBindings.Add(roleDefinition);

            web.RoleAssignments.Add(assignment);
        }

        /// <summary>
        /// The method to add usercontrol to page
        /// </summary>
        /// <param name="web">Current web</param>
        /// <param name="pageName">Page Name: only name and except extension (.aspx)</param>
        /// <param name="pageTitle">Title of page</param>
        /// <param name="userControlName">UserControl name: only name and except extension (.ascx)</param>
        public static void AddUserControlToPage(SPWeb web, string pageName, string pageTitle, string userControlName)
        {
            ContainerWebPart containerWebPart = WebPartHelper.GetContainerWebPart(web);
            if (containerWebPart != null)
            {
                containerWebPart.Title = pageTitle;
                containerWebPart.UserControlPath = "UserControls/" + userControlName + ".ascx";
                WebPartHelper.AddWebPart(web, pageName + ".aspx", containerWebPart, "Main", 0);
            }
        }

        /// <summary>
        /// GetItemByField
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="typeValue"></param>
        /// <returns></returns>
        public static SPListItem GetItemByField(SPList list, string fieldName, string fieldValue, string typeValue)
        {
            var queryItem = new SPQuery
            {
                Query =
                    "<Where><Eq><FieldRef Name='" + fieldName + "' /><Value Type='" + typeValue + "'>" + fieldValue +
                    "</Value></Eq></Where>",
                RowLimit = 1
            };
            var itemArray = list.GetItems(queryItem);
            if (itemArray != null && itemArray.Count > 0)
            {
                return itemArray[0];
            }
            return null;
        }

        /// <summary>
        /// Add Event for lists
        /// </summary>
        /// <param name="list">Current list</param>
        /// <param name="eventReceiverType">Event Type parameter</param>
        /// <param name="className">className</param>
        public static void AddEventForList(SPList list, SPEventReceiverType eventReceiverType, string className)
        {
            list.EventReceivers.Add(eventReceiverType, typeof(Utilities).Assembly.FullName, "CQ.SharePoint.QN.EventReceivers." + className);
        }

        /// <summary>
        /// BreakItem
        /// </summary>
        /// <param name="web"></param>
        /// <param name="item"></param>
        public static void BreakItem(SPWeb web, SPListItem item)
        {
            if (!item.HasUniqueRoleAssignments)
            {
                web.AllowUnsafeUpdates = true;
                item.BreakRoleInheritance(false);
            }
            else
            {
                web.AllowUnsafeUpdates = true;
                item.ResetRoleInheritance();
                web.AllowUnsafeUpdates = true;
                item.BreakRoleInheritance(false);
            }
            web.AllowUnsafeUpdates = true;
            item.SystemUpdate();
        }

        /// <summary>
        /// ApplyPermissionForItem
        /// </summary>
        /// <param name="web"></param>
        /// <param name="item"></param>
        /// <param name="permissonLevel"></param>
        /// <param name="user"></param>
        public static void ApplyPermissionForItem(SPWeb web, SPListItem item, string permissonLevel, SPPrincipal user)
        {
            var roleAssignment = new SPRoleAssignment(user);
            SPRoleDefinition roleDefinition = web.RoleDefinitions[permissonLevel];
            roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
            item.RoleAssignments.Add(roleAssignment);
            web.AllowUnsafeUpdates = true;
            item.SystemUpdate();
        }

        public static bool CheckMenuType(string listName, int docID, ref string colName, ref string catName, ref string newsName)
        {
            bool result = false;
            var colNameT = string.Empty;
            var catNameT = string.Empty;
            var newsNameT = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            string listUrl = web.Url + "/Lists/" + listName;
                            var mtItem = web.GetList(listUrl).GetItemById(docID);
                            if (!string.IsNullOrEmpty(Convert.ToString(mtItem["MenuTypeColumn"])) &&
                                !string.IsNullOrEmpty(Convert.ToString(mtItem["CatName"])) &&
                                !string.IsNullOrEmpty(Convert.ToString(mtItem["NewsName"])))
                            {
                                result = true;
                                colNameT = Convert.ToString(mtItem["MenuTypeColumn"]);
                                catNameT = Convert.ToString(mtItem["CatName"]);
                                newsNameT = Convert.ToString(mtItem["NewsName"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToUls(ex);
                        }
                    }

                }
            });
            colName = colNameT;
            catName = catNameT;
            newsName = newsNameT;
            return result;
        }

        public static bool CheckMenuType(DataTable dtMenuType, string itemID, ref string colName, ref string catName, ref string newsName)
        {
            bool result = false;
            var colNameT = string.Empty;
            var catNameT = string.Empty;
            var newsNameT = string.Empty;
            try
            {
                var mtItems = dtMenuType.Select("ID='" + itemID + "'");
                if (mtItems != null && mtItems.Length > 0)
                {
                    var mtItem = mtItems[0];
                    if (!string.IsNullOrEmpty(Convert.ToString(mtItem["MenuTypeColumn"])) &&
                    !string.IsNullOrEmpty(Convert.ToString(mtItem["CatName"])) &&
                    !string.IsNullOrEmpty(Convert.ToString(mtItem["NewsName"])))
                    {
                        result = true;
                        colNameT = Convert.ToString(mtItem["MenuTypeColumn"]);
                        catNameT = Convert.ToString(mtItem["CatName"]);
                        newsNameT = Convert.ToString(mtItem["NewsName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
            colName = colNameT;
            catName = catNameT;
            newsName = newsNameT;
            return result;
        }

        public static void SetMenuLink(DataRow drv, Literal ltrStyle, HtmlAnchor aLink, DataTable dtMenuType, HttpContext ctx)
        {
            var itemUrl = Convert.ToString(drv["Url"]);
            var currentUrl = ctx.Request.Url.AbsoluteUri + "&";

            //Bind data to URL

            aLink.Title = Convert.ToString(drv["Title"]);
            aLink.InnerText = Convert.ToString(drv["Title"]);

            if (string.IsNullOrEmpty(Convert.ToString(drv["MenuType"])))
            {
                aLink.HRef = itemUrl;
                if (!string.IsNullOrEmpty(itemUrl) && currentUrl.Contains(itemUrl + "&"))
                {
                    ltrStyle.Text = " class='current'";
                }
            }
            else
            {
                aLink.HRef = itemUrl;
                var lkMenuType = Convert.ToString(drv["MenuTypeID"]);
                var colNameT = string.Empty;
                var catNameT = string.Empty;
                var newsNameT = string.Empty;
                var checkMT = Utilities.CheckMenuType(dtMenuType, lkMenuType, ref colNameT, ref catNameT, ref newsNameT);
                if (checkMT)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(drv[colNameT])))
                    {
                        aLink.HRef = SPContext.Current.Web.Url + "/" + Constants.PageInWeb.SubPage +
                                     ".aspx?CategoryId=" + Convert.ToInt32(drv[colNameT]) + "&ListCategoryName=" + catNameT +
                                     "&ListName=" + newsNameT;

                        var catID = ctx.Request.QueryString[Constants.CategoryId];
                        var catName = ctx.Request.QueryString[Constants.ListCategoryName];
                        var newsName = ctx.Request.QueryString[Constants.ListName];
                        if (!string.IsNullOrEmpty(catID))
                        {
                            if (Convert.ToString(drv[colNameT]).Equals(catID) && catNameT.Equals(catName) && newsNameT.Equals(newsName))
                            {
                                ltrStyle.Text = " class='current'";
                            }
                            else
                            {
                                //Utilities.LogToUls("chan");
                            }
                        }
                    }
                }
                else
                {
                    aLink.HRef = itemUrl;
                    if (!string.IsNullOrEmpty(itemUrl) && currentUrl.Contains(itemUrl + "&"))
                    {
                        ltrStyle.Text = " class='current'";
                    }
                }
            }
        }

        public static void SetMenuLink(DataRow drv, HtmlAnchor aLink, DataTable dtMenuType, HttpContext ctx)
        {
            var itemUrl = Convert.ToString(drv["Url"]);
            var currentUrl = ctx.Request.Url.AbsoluteUri + "&";

            //Bind data to URL

            aLink.Title = Convert.ToString(drv["Title"]);
            aLink.InnerText = Convert.ToString(drv["Title"]);

            if (string.IsNullOrEmpty(Convert.ToString(drv["MenuType"])))
            {
                aLink.HRef = itemUrl;
            }
            else
            {
                aLink.HRef = itemUrl;
                var lkMenuType = Convert.ToString(drv["MenuTypeID"]);
                var colNameT = string.Empty;
                var catNameT = string.Empty;
                var newsNameT = string.Empty;
                var checkMT = Utilities.CheckMenuType(dtMenuType, lkMenuType, ref colNameT, ref catNameT, ref newsNameT);
                if (checkMT)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(drv[colNameT])))
                    {
                        aLink.HRef = SPContext.Current.Web.Url + "/" + Constants.PageInWeb.SubPage +
                                     ".aspx?CategoryId=" + Convert.ToInt32(drv[colNameT]) + "&ListCategoryName=" + catNameT +
                                     "&ListName=" + newsNameT;
                    }
                }
            }
        }

        public static SPFieldLookupValue GetMenuType(string listName, int docID, string colName)
        {
            SPFieldLookupValue result = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            string listUrl = web.Url + "/Lists/" + listName;
                            var mtItem = web.GetList(listUrl).GetItemById(docID);
                            result = new SPFieldLookupValue(Convert.ToString(mtItem[colName]));
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToUls(ex);
                        }
                    }

                }
            });
            return result;
        }

        public static string GetMenuType(DataTable dtMenu, int docID, string colName)
        {
            var mtItems = dtMenu.Select("ID='" + docID + "'");
            if (mtItems != null && mtItems.Length > 0)
            {
                var mtItem = mtItems[0];
                return Convert.ToString(mtItem[colName]);
            }
            return string.Empty;
        }

        /// <summary>
        /// Get item from list by userID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fieldName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static SPListItem GetItemdByUserId(int userId, string fieldName, SPList list)
        {
            var spQuery = new SPQuery
                              {
                                  Query = string.Format(CultureInfo.InvariantCulture, "<Where><Eq><FieldRef Name='{0}' LookupId='TRUE' /><Value Type='User'>{1}</Value></Eq></Where>", fieldName, userId)
                              };
            var itemArray = list.GetItems(spQuery);
            if (itemArray != null && itemArray.Count > 0)
            {
                return itemArray[0];
            }
            return null;
        }


        /// <summary>
        /// GetListFromUrl
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        public static DataTable GetListFromUrl(string listName)
        {
            DataTable table = null;
            //SPSecurity.RunWithElevatedPrivileges(() =>
            //{
            //    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
            //    {
            //        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
            //        {
            //            try
            //            {
            //                string listUrl = web.Url + "/Lists/" + listName;
            //                var result = web.GetList(listUrl);
            //                SPQuery query = new SPQuery();
            //                var items = result.GetItems(query);
            //                if (items != null && items.Count > 0)
            //                    table = items.GetDataTable();
            //            }
            //            catch (Exception ex)
            //            {
            //                Utilities.LogToUls(ex);
            //            }
            //        }

            //    }
            //});
            try
            {
                string listUrl = SPContext.Current.Web.Url + "/Lists/" + listName;
                var result = SPContext.Current.Web.GetList(listUrl);
                SPQuery query = new SPQuery();
                var items = result.GetItems(query);
                if (items != null && items.Count > 0)
                    table = items.GetDataTable();
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
            return table;
        }

        /// <summary>
        /// GetListItemFromUrlByID
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public static DataRow GetListItemFromUrlByID(string listName, string itemID)
        {
            DataRow row = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            string listUrl = web.Url + "/Lists/" + listName;
                            var result = web.GetList(listUrl);
                            SPQuery query = new SPQuery();
                            query.RowLimit = 1;
                            query.Query = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='Counter'>{1}</Value></Eq></Where>", "ID", itemID);
                            var items = result.GetItems(query);
                            if (items != null && items.Count > 0)
                                row = items.GetDataTable().Rows[0];
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToUls(ex);
                        }
                    }

                }
            });
            return row;
        }

        /// <summary>
        /// GetListFromUrl
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <returns></returns>
        public static SPList GetListFromUrl(SPWeb web, string listName)
        {
            SPList result = null;
            try
            {
                string listUrl = web.Url + "/Lists/" + listName;
                result = web.GetList(listUrl);
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
            return result;
        }

        /// <summary>
        /// GetDocListFromUrl
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <returns></returns>
        public static SPList GetDocListFromUrl(SPWeb web, string listName)
        {
            SPList result = null;
            try
            {
                string listUrl = web.Url + "/" + listName;
                result = web.GetList(listUrl);
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
            return result;
        }

        public static SPList GetDocListFromUrl(string listName)
        {
            SPList result = null;
            //SPSecurity.RunWithElevatedPrivileges(() =>
            //{
            //    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
            //    {
            //        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
            //        {
            //            try
            //            {
            //                string listUrl = web.Url + "/" + listName;
            //                result = web.GetList(listUrl);
            //            }
            //            catch (Exception ex)
            //            {
            //                Utilities.LogToUls(ex);
            //            }
            //        }

            //    }
            //});
            try
            {
                string listUrl = SPContext.Current.Web.Url + "/" + listName;
                result = SPContext.Current.Web.GetList(listUrl);
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
            return result;
        }

        public static SPListItem GetDocListItemFromUrl(string listName, int docID)
        {
            SPListItem result = null;
            //SPSecurity.RunWithElevatedPrivileges(() =>
            //{
            //    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
            //    {
            //        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
            //        {
            //            try
            //            {
            //                string listUrl = web.Url + "/" + listName;
            //                result = web.GetList(listUrl).GetItemById(docID);
            //            }
            //            catch (Exception ex)
            //            {
            //                Utilities.LogToUls(ex);
            //            }
            //        }

            //    }
            //});
            try
            {
                string listUrl = SPContext.Current.Web.Url + "/" + listName;
                result = SPContext.Current.Web.GetList(listUrl).GetItemById(docID);
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
            return result;
        }

        public static SPListItem GetQuangCao(string listName, string catID, string catType)
        {
            SPListItem result = null;
//            SPSecurity.RunWithElevatedPrivileges(() =>
//            {
//                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
//                {
//                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
//                    {
//                        try
//                        {
//                            string listUrl = web.Url + "/Lists/" + listName;
//                            var camlquery = string.Format(@"<Where>
//                                                          <Eq>
//                                                            <FieldRef Name='{0}' LookupId='TRUE' />
//                                                            <Value Type='LookupMulti'>{1}</Value>
//                                                          </Eq>
//                                                       </Where>", catType, catID);
//                            var query = new SPQuery();
//                            query.Query = camlquery;
//                            query.RowLimit = 1;
//                            var items = web.GetList(listUrl).GetItems(query);
//                            if (items != null && items.Count > 0)
//                            {
//                                result = items[0];
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            Utilities.LogToUls(ex);
//                        }
//                    }

//                }
//            });
            try
            {
                string listUrl = SPContext.Current.Web.Url + "/Lists/" + listName;
                var camlquery = string.Format(@"<Where>
                                                          <Eq>
                                                            <FieldRef Name='{0}' LookupId='TRUE' />
                                                            <Value Type='LookupMulti'>{1}</Value>
                                                          </Eq>
                                                       </Where>", catType, catID);
                var query = new SPQuery();
                query.Query = camlquery;
                query.RowLimit = 1;
                var items = SPContext.Current.Web.GetList(listUrl).GetItems(query);
                if (items != null && items.Count > 0)
                {
                    result = items[0];
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }
            return result;
        }

        public static DataTable GetNewsRecords(string query, uint newsNumber, string listName)
        {
            DataTable table = new DataTable();
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            SPQuery spQuery = new SPQuery
                            {
                                Query = query,
                                RowLimit = newsNumber
                            };
                            SPList list = Utilities.GetListFromUrl(web, listName);
                            if (list != null)
                            {
                                SPListItemCollection items = list.GetItems(spQuery);
                                if (items != null && items.Count > 0)
                                {
                                    table = items.GetDataTable();

                                    if (!table.Columns.Contains(FieldsName.CategoryId))
                                    {
                                        table.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
                                    }

                                    if (!table.Columns.Contains(FieldsName.ArticleStartDateTemp))
                                    {
                                        table.Columns.Add(FieldsName.ArticleStartDateTemp, Type.GetType("System.String"));
                                    }

                                    for (int i = 0; i < items.Count; i++)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
                                        {
                                            SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
                                            table.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
                                        }
                                        var time = Convert.ToDateTime(items[i][FieldsName.Created]);
                                        table.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            table = null;
                        }
                    }

                }
            });
            //try
            //{
            //    SPQuery spQuery = new SPQuery
            //    {
            //        Query = query,
            //        RowLimit = newsNumber
            //    };
            //    SPList list = Utilities.GetListFromUrl(SPContext.Current.Web, listName);
            //    if (list != null)
            //    {
            //        SPListItemCollection items = list.GetItems(spQuery);
            //        if (items != null && items.Count > 0)
            //        {
            //            table = items.GetDataTable();

            //            if (!table.Columns.Contains(FieldsName.CategoryId))
            //            {
            //                table.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
            //            }

            //            if (!table.Columns.Contains(FieldsName.ArticleStartDateTemp))
            //            {
            //                table.Columns.Add(FieldsName.ArticleStartDateTemp, Type.GetType("System.String"));
            //            }

            //            for (int i = 0; i < items.Count; i++)
            //            {
            //                if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
            //                {
            //                    SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
            //                    table.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
            //                }
            //                var time = Convert.ToDateTime(items[i][FieldsName.Created]);
            //                table.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    table = null;
            //}
            return table;
        }

        public static DataTable GetNewsRecords(string query, string listName)
        {
            DataTable table = new DataTable();
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            SPQuery spQuery = new SPQuery
                            {
                                Query = query,
                                DatesInUtc = false
                            };
                            SPList list = Utilities.GetListFromUrl(web, listName);
                            if (list != null)
                            {
                                SPListItemCollection items = list.GetItems(spQuery);
                                if (items != null && items.Count > 0)
                                {
                                    table = items.GetDataTable();

                                    if (!table.Columns.Contains(FieldsName.CategoryId))
                                    {
                                        table.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
                                    }

                                    if (!table.Columns.Contains(FieldsName.ArticleStartDateTemp))
                                    {
                                        table.Columns.Add(FieldsName.ArticleStartDateTemp, Type.GetType("System.String"));
                                    }

                                    for (int i = 0; i < items.Count; i++)
                                    {
                                        if (table.Columns.Contains(FieldsName.NewsRecord.English.CategoryName))
                                        {
                                            if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
                                            {
                                                SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
                                                table.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
                                            }
                                        }
                                        
                                        var time = Convert.ToDateTime(items[i][FieldsName.Created]);
                                        table.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            table = null;
                        }
                    }

                }
            });
            //try
            //{
            //    SPQuery spQuery = new SPQuery
            //    {
            //        Query = query,
            //        DatesInUtc = false
            //    };
            //    SPList list = Utilities.GetListFromUrl(SPContext.Current.Web, listName);
            //    if (list != null)
            //    {
            //        SPListItemCollection items = list.GetItems(spQuery);
            //        if (items != null && items.Count > 0)
            //        {
            //            table = items.GetDataTable();

            //            if (!table.Columns.Contains(FieldsName.CategoryId))
            //            {
            //                table.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
            //            }

            //            if (!table.Columns.Contains(FieldsName.ArticleStartDateTemp))
            //            {
            //                table.Columns.Add(FieldsName.ArticleStartDateTemp, Type.GetType("System.String"));
            //            }

            //            for (int i = 0; i < items.Count; i++)
            //            {
            //                if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
            //                {
            //                    SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
            //                    table.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
            //                }
            //                var time = Convert.ToDateTime(items[i][FieldsName.Created]);
            //                table.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    table = null;
            //}
            return table;
        }


        public static SPListItemCollection GetNewsRecordItems(string query, uint newsNumber, string listName)
        {
            SPListItemCollection allItems = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            SPQuery spQuery = new SPQuery
                            {
                                Query = query,
                                RowLimit = newsNumber
                            };
                            SPList list = Utilities.GetListFromUrl(web, listName);
                            if (list != null)
                            {
                                SPListItemCollection items = list.GetItems(spQuery);
                                if (items != null && items.Count > 0)
                                {
                                    allItems = items;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            allItems = null;
                        }
                    }

                }
            });
            //try
            //{
            //    SPQuery spQuery = new SPQuery
            //    {
            //        Query = query,
            //        RowLimit = newsNumber
            //    };
            //    SPList list = Utilities.GetListFromUrl(SPContext.Current.Web, listName);
            //    if (list != null)
            //    {
            //        SPListItemCollection items = list.GetItems(spQuery);
            //        if (items != null && items.Count > 0)
            //        {
            //            allItems = items;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    allItems = null;
            //}
            return allItems;
        }

        public static string GetCategoryIdByItemId(int itemId, string listName)
        {
            string categoryId = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            SPList list = GetListFromUrl(web, listName);
                            if (list != null)
                            {
                                SPItem items = list.GetItemById(itemId);
                                if (items != null)
                                {
                                    string categoryName = Convert.ToString(items[FieldsName.NewsRecord.English.CategoryName]);
                                    categoryId = categoryName.Substring(0, categoryName.IndexOf(";#"));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //categoryId = 0;
                        }
                    }
                }
            });
            //try
            //{
            //    SPList list = GetListFromUrl(SPContext.Current.Web, listName);
            //    if (list != null)
            //    {
            //        SPItem items = list.GetItemById(itemId);
            //        if (items != null)
            //        {
            //            string categoryName = Convert.ToString(items[FieldsName.NewsRecord.English.CategoryName]);
            //            categoryId = categoryName.Substring(0, categoryName.IndexOf(";#"));
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //categoryId = 0;
            //}
            return categoryId;
        }

        public static DataTable GetDocLibRecords(string query, string listName)
        {
            DataTable table = new DataTable();
            //SPSecurity.RunWithElevatedPrivileges(() =>
            //{
            //    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
            //    {
            //        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
            //        {
            //            try
            //            {
            //                SPQuery spQuery = new SPQuery
            //                {
            //                    Query = query,
            //                    ViewFields = "<FieldRef Name='ID' /><FieldRef Name='Title' /><FieldRef Name='ImageCreateDate' /><FieldRef Name='Description' /><FieldRef Name='FileLeafRef' />"
            //                };
            //                SPList list = Utilities.GetDocListFromUrl(web, listName);
            //                if (list != null)
            //                {
            //                    SPListItemCollection items = list.GetItems(spQuery);
            //                    if (items != null && items.Count > 0)
            //                    {
            //                        table = items.GetDataTable();
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                table = null;
            //            }
            //        }

            //    }
            //});
            try
            {
                SPQuery spQuery = new SPQuery
                {
                    Query = query,
                    ViewFields = "<FieldRef Name='ID' /><FieldRef Name='Title' /><FieldRef Name='ImageCreateDate' /><FieldRef Name='Description' /><FieldRef Name='FileLeafRef' />"
                };
                SPList list = Utilities.GetDocListFromUrl(SPContext.Current.Web, listName);
                if (list != null)
                {
                    SPListItemCollection items = list.GetItems(spQuery);
                    if (items != null && items.Count > 0)
                    {
                        table = items.GetDataTable();
                    }
                }
            }
            catch (Exception ex)
            {
                table = null;
            }
            return table;
        }

        public static DataTable GetDocLibRecords(string query, uint itemsNumber, string listName)
        {
            DataTable table = new DataTable();
            //SPSecurity.RunWithElevatedPrivileges(() =>
            //{
            //    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
            //    {
            //        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
            //        {
            //            try
            //            {
            //                SPQuery spQuery = new SPQuery
            //                {
            //                    Query = query,
            //                    RowLimit = itemsNumber
            //                };
            //                SPList list = GetDocListFromUrl(web, listName);
            //                if (list != null)
            //                {
            //                    SPListItemCollection items = list.GetItems(spQuery);
            //                    if (items != null && items.Count > 0)
            //                    {
            //                        table = items.GetDataTable();
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                table = null;
            //            }
            //        }

            //    }
            //});
            try
            {
                SPQuery spQuery = new SPQuery
                {
                    Query = query,
                    RowLimit = itemsNumber
                };
                SPList list = GetDocListFromUrl(SPContext.Current.Web, listName);
                if (list != null)
                {
                    SPListItemCollection items = list.GetItems(spQuery);
                    if (items != null && items.Count > 0)
                    {
                        table = items.GetDataTable();
                    }
                }
            }
            catch (Exception ex)
            {
                table = null;
            }
            return table;
        }

        public static string GetParentUrl(int parentID)
        {
            string result = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            SPList list = Utilities.GetListFromUrl(web, ListsName.English.MenuList);
                            if (list != null)
                            {
                                var item = list.GetItemById(parentID);
                                result = Convert.ToString(item["Url"]);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            });
            return result;
        }

        /// <summary>
        /// Get value in resource file by key
        /// </summary>
        /// <param name="resourceFileName">Resource File Name</param>
        /// <param name="resourceKey">Resource Key</param>
        /// <returns></returns>
        public static string GetMessageFromResourceFile(string resourceFileName, string resourceKey)
        {
            try
            {
                var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
                return SPUtility.GetLocalizedString(string.Format(CultureInfo.InvariantCulture, "$Resources:{0}", resourceKey), resourceFileName, (uint)lang);
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }

            return "";
        }

        /// <summary>
        /// This method will return string not have spical character
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertReturnString(string value)
        {
            value = value.Trim();
            value = value.Replace("'", "\\'");
            value = value.Replace("\r\n", "");
            value = value.Replace("\n\r", "");
            value = value.Replace("\n", "");
            value = value.Replace("\r", "");
            return value;
        }

        /// <summary>
        /// GetDateString
        /// </summary>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static string GetDateString(DateTime inputDate)
        {
            var month = Convert.ToString(inputDate.Month, CultureInfo.InvariantCulture);
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            var day = Convert.ToString(inputDate.Day, CultureInfo.InvariantCulture);
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            return Convert.ToString(inputDate.Year, CultureInfo.InvariantCulture) + "/" + month + "/" + day;
        }

        /// <summary>
        /// LoadJS
        /// </summary>
        /// <param name="web"></param>
        /// <param name="page"></param>
        /// <param name="name"></param>
        public static void LoadJS(SPWeb web, Page page, string name)
        {
            // Load Js
            SPListItem js = Utilities.GetResourceByType(web, FieldsName.CQQNResources.FieldValuesDefault.Type.JS, name);

            if (js != null)
            {
                string jsUrl = web.Url + "/" + js.Url;
                page.ClientScript.RegisterClientScriptInclude(js.ID + "js", jsUrl);
            }
        }

        /// <summary>
        /// LoadCSS
        /// </summary>
        /// <param name="web"></param>
        /// <param name="page"></param>
        /// <param name="name"></param>
        public static void LoadCSS(SPWeb web, Page page, string name)
        {
            // Load Css
            SPListItem css = Utilities.GetResourceByType(web, FieldsName.CQQNResources.FieldValuesDefault.Type.CSS, name);
            if (css != null)
            {
                string cssUrl = web.Url + "/" + css.Url;

                HtmlLink styleSheet = new HtmlLink
                {
                    Href = cssUrl,
                    ID = css.ID + "_Css",

                };
                styleSheet.Attributes["rel"] = "stylesheet";
                styleSheet.Attributes["type"] = "text/css";
                styleSheet.Attributes["media"] = "all";

                page.Header.Controls.Add(styleSheet);
            }
        }

        /// <summary>
        /// GetResourceByType
        /// </summary>
        /// <param name="web"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SPListItem GetResourceByType(SPWeb web, string type, string name)
        {
            SPList resource = null;

            try
            {
                string listUrl = web.Url + "/" + ListsName.English.CQQNResources;
                resource = web.GetList(listUrl);
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }

            string CAML = @"<Where><And><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq><Eq><FieldRef Name='{2}' /><Value Type='Text'>{3}</Value></Eq></And></Where>";

            SPQuery query = new SPQuery()
            {
                Query = string.Format(CultureInfo.InvariantCulture, CAML, FieldsName.CQQNResources.English.ResourceType, type, FieldsName.CQQNResources.English.FileLeafRef, name),
                RowLimit = 1
            };

            SPListItem item = null;

            try
            {
                item = resource.GetItems(query)[0];
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
                throw ex;
            }

            return item;
        }

        public static string GetConfigValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            string result = "";
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            SPList list = GetListFromUrl(web, ListsName.English.QNConfigList);
                            if (list != null)
                            {
                                var query = new SPQuery();
                                query.Query = "<Where><Eq><FieldRef Name='Title' />" +
                                              "<Value Type='Text'>" + key + "</Value></Eq></Where>";
                                query.RowLimit = 1;
                                var items = list.GetItems(query);
                                if (items != null && items.Count > 0)
                                {
                                    result = Convert.ToString(items[0]["Value"]);
                                }
                                else
                                {
                                    var item = list.Items.Add();
                                    item["Title"] = key;
                                    item["Value"] = "Hãy vào list QNConfigList để nhập giá trị cho key " + key;
                                    result = "Hãy vào list QNConfigList để nhập giá trị cho key " + key;
                                    web.AllowUnsafeUpdates = true;
                                    item.Update();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToUls(ex);
                        }
                    }
                }
            });
            return result;
        }

        public static DataTable GetNewsByCatID(string listName, string catID)
        {
            string camlQuery = string.Format(@"<Where>
                                                  <And>
                                                     <Eq>
                                                        <FieldRef Name='{0}' LookupId='TRUE' />
                                                        <Value Type='CustomLookup'>{1}</Value>
                                                     </Eq>
                                                     <And>
                                                        <Neq>
                                                           <FieldRef Name='Status' />
                                                           <Value Type='Boolean'>1</Value>
                                                        </Neq>
                                                        <And>
                                                           <Leq>
                                                              <FieldRef Name='ArticleStartDates' />
                                                              <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                           </Leq>
                                                           <Contains>
                                                              <FieldRef Name='Approve' />
                                                              <Value Type='Lookup'>{3}</Value>
                                                           </Contains>
                                                        </And>
                                                     </And>
                                                  </And>
                                               </Where>", FieldsName.NewsRecord.English.CategoryName,
                                                catID, SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                Constants.Published);
            if (ListsName.English.CompanyRecord.Equals(listName))
            {
                camlQuery = string.Format(@"<Where>
                                                  <And>
                                                     <Eq>
                                                        <FieldRef Name='{0}' LookupId='TRUE' />
                                                        <Value Type='Lookup'>{1}</Value>
                                                     </Eq>
                                                     <And>
                                                        <Neq>
                                                           <FieldRef Name='Status' />
                                                           <Value Type='Boolean'>1</Value>
                                                        </Neq>
                                                        <And>
                                                           <Leq>
                                                              <FieldRef Name='ArticleStartDates' />
                                                              <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                           </Leq>
                                                           <And>
                                                              <Contains>
                                                                 <FieldRef Name='Approve' />
                                                                 <Value Type='Lookup'>{3}</Value>
                                                              </Contains>
                                                              <Geq>
                                                                 <FieldRef Name='_EndDate' />
                                                                 <Value IncludeTimeValue='TRUE' Type='DateTime'>{2}</Value>
                                                              </Geq>
                                                           </And>
                                                        </And>
                                                     </And>
                                                  </And>
                                               </Where>", FieldsName.NewsRecord.English.CategoryName,
                                                catID, SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                Constants.Published);
            }

            if (ListsName.English.NewsCategory.Equals(listName))
            {
                camlQuery = string.Format(@"<Where>
                                              <And>
                                                 <Eq>
                                                    <FieldRef Name='{0}' LookupId='TRUE' />
                                                    <Value Type='Lookup'>{1}</Value>
                                                 </Eq>
                                                 <Neq>
                                                    <FieldRef Name='Status' />
                                                    <Value Type='Boolean'>1</Value>
                                                 </Neq>
                                              </And>
                                           </Where>", FieldsName.NewsCategory.English.ParentName, catID);
            }

            if (ListsName.English.SubNewsCategory.Equals(listName))
            {
                camlQuery = string.Format(@"<Where>
                                              <And>
                                                 <Eq>
                                                    <FieldRef Name='{0}' LookupId='TRUE' />
                                                    <Value Type='Lookup'>{1}</Value>
                                                 </Eq>
                                                 <Neq>
                                                    <FieldRef Name='Status' />
                                                    <Value Type='Boolean'>1</Value>
                                                 </Neq>
                                              </And>
                                           </Where>", FieldsName.NewsCategory.English.ParentName, catID);
            }


            var query = new SPQuery();
            query.Query = camlQuery;
            DataTable table = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            string listUrl = web.Url + "/Lists/" + listName;
                            var list = web.GetList(listUrl);
                            if (list != null)
                            {
                                var items = list.GetItems(query);
                                if (items != null && items.Count > 0)
                                {
                                    table = items.GetDataTable();

                                    if (!table.Columns.Contains(FieldsName.CategoryId))
                                    {
                                        table.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
                                    }

                                    if (!table.Columns.Contains(FieldsName.ArticleStartDateTemp))
                                    {
                                        table.Columns.Add(FieldsName.ArticleStartDateTemp, Type.GetType("System.String"));
                                    }

                                    for (int i = 0; i < items.Count; i++)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
                                        {
                                            SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
                                            table.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
                                        }
                                        var time = Convert.ToDateTime(items[i][FieldsName.Created]);
                                        table.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToUls(ex);
                        }
                    }

                }
            });
            //try
            //{
            //    string listUrl = SPContext.Current.Web.Url + "/Lists/" + listName;
            //    var list = SPContext.Current.Web.GetList(listUrl);
            //    if (list != null)
            //    {
            //        var items = list.GetItems(query);
            //        if (items != null && items.Count > 0)
            //        {
            //            table = items.GetDataTable();

            //            if (!table.Columns.Contains(FieldsName.CategoryId))
            //            {
            //                table.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
            //            }

            //            if (!table.Columns.Contains(FieldsName.ArticleStartDateTemp))
            //            {
            //                table.Columns.Add(FieldsName.ArticleStartDateTemp, Type.GetType("System.String"));
            //            }

            //            for (int i = 0; i < items.Count; i++)
            //            {
            //                if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
            //                {
            //                    SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
            //                    table.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
            //                }
            //                var time = Convert.ToDateTime(items[i][FieldsName.Created]);
            //                table.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utilities.LogToUls(ex);
            //}
            return table;
        }

        public static DataTable GetNewsCatByParent(string listCategoryName, string catID)
        {
            string camlQuery = string.Format("<Where><Eq><FieldRef Name='{0}' LookupId='TRUE'/><Value Type='CustomLookup'>{1}</Value></Eq></Where>", FieldsName.NewsCategory.English.ParentName, catID);
            var query = new SPQuery();
            query.Query = camlQuery;
            DataTable table = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            string listUrl = web.Url + "/Lists/" + listCategoryName;
                            var list = web.GetList(listUrl);
                            if (list != null)
                            {
                                var items = list.GetItems(query);
                                if (items != null && items.Count > 0)
                                {
                                    table = items.GetDataTable();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToUls(ex);
                        }
                    }

                }
            });
            return table;
        }

        public static void GetNewsByCatID(string listName, string listCategoryName, string catID, ref DataTable dtNews)
        {
            var dt = GetNewsByCatID(listName, catID);
            if (dt != null && dtNews == null)
            {
                dtNews = dt.Clone();
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    dtNews.ImportRow(dataRow);
                }
            }
            var catTbl = GetNewsCatByParent(listCategoryName, catID);
            if (catTbl != null && catTbl.Rows.Count > 0)
            {
                foreach (DataRow row in catTbl.Rows)
                {
                    GetNewsByCatID(listName, listCategoryName, Convert.ToString(row["ID"]), ref dtNews);
                }
            }
        }

        public static void BuilChuyenDe(string listName, string listCategoryName, string catID, ref DataTable dtNews)
        {
            var dt = GetNewsByCatID(listName, catID);

            if (dt != null && dtNews == null)
            {
                dtNews = dt.Clone();
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow[FieldsName.Title] = string.Format("--{0}", Convert.ToString(dataRow[FieldsName.Title]));
                    dtNews.ImportRow(dataRow);

                    var catTbl = GetNewsCatByParent(listCategoryName, catID);
                    if (catTbl != null && catTbl.Rows.Count > 0)
                    {
                        foreach (DataRow row in catTbl.Rows)
                        {
                            BuilChuyenDe2(listName, listCategoryName, Convert.ToString(row["ID"]), ref dtNews);
                        }
                    }
                }
            }
        }


        public static void BuilChuyenDe2(string listName, string listCategoryName, string catID, ref DataTable dtNews)
        {
            var dt = GetNewsByCatID(listName, catID);
            if (dt != null && dtNews == null)
            {
                dtNews = dt.Clone();
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow[FieldsName.Title] = string.Format("----{0}", Convert.ToString(dataRow[FieldsName.Title]));
                    dtNews.ImportRow(dataRow);

                    var catTbl = GetNewsCatByParent(listCategoryName, catID);
                    if (catTbl != null && catTbl.Rows.Count > 0)
                    {
                        foreach (DataRow row in catTbl.Rows)
                        {
                            BuilChuyenDe3(listName, listCategoryName, Convert.ToString(row["ID"]), ref dtNews);
                        }
                    }
                }
            }
        }

        public static void BuilChuyenDe3(string listName, string listCategoryName, string catID, ref DataTable dtNews)
        {
            var dt = GetNewsByCatID(listName, catID);
            if (dt != null && dtNews == null)
            {
                dtNews = dt.Clone();
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow[FieldsName.Title] = string.Format("------{0}", Convert.ToString(dataRow[FieldsName.Title]));
                    dtNews.ImportRow(dataRow);

                    var catTbl = GetNewsCatByParent(listCategoryName, catID);
                    if (catTbl != null && catTbl.Rows.Count > 0)
                    {
                        foreach (DataRow row in catTbl.Rows)
                        {
                            BuilChuyenDe3(listName, listCategoryName, Convert.ToString(row["ID"]), ref dtNews);
                        }
                    }
                }
            }
        }

        public static string BuildSepertate(int count)
        {
            var result = new StringBuilder();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    result.Append("--");
                }
            }
            return result.ToString();
        }

        public static void GetRSS(string listName, string listCategoryName, string catID)
        {
            if (string.IsNullOrEmpty(catID))
            {
                string camlQuery = string.Format(@"<Where>
                                                      <And>
                                                         <Neq>
                                                            <FieldRef Name='Status' />
                                                            <Value Type='Boolean'>1</Value>
                                                         </Neq>
                                                         <And>
                                                            <Lt>
                                                               <FieldRef Name='ArticleStartDate' />
                                                               <Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value>
                                                            </Lt>
                                                            <Contains>
                                                               <FieldRef Name='Approve' />
                                                               <Value Type='Lookup'>{1}</Value>
                                                            </Contains>
                                                         </And>
                                                      </And>
                                                   </Where>
                                                   <OrderBy>
                                                      <FieldRef Name='ID' Ascending='False' />
                                                   </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                              Constants.Published);
                var query = new SPQuery();
                query.Query = camlQuery;
                query.RowLimit = 20;
                DataTable table = null;
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                    {
                        using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            try
                            {
                                string listUrl = web.Url + "/Lists/" + listName;
                                var list = web.GetList(listUrl);
                                if (list != null)
                                {
                                    var items = list.GetItems(query);
                                    if (items != null && items.Count > 0)
                                    {
                                        table = items.GetDataTable();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Utilities.LogToUls(ex);
                            }
                        }

                    }
                });
                GetRSS(table, "Trung tâm công nghệ thông tin tỉnh Quảng Ninh", SPContext.Current.Web.Url, "Trung tâm công nghệ thông tin tỉnh Quảng Ninh");
            }
            else
            {
                DataTable dtNews = null;
                GetNewsByCatID(listName, listCategoryName, catID, ref dtNews);
                var rssTitle = "Trung tâm công nghệ thông tin tỉnh Quảng Ninh";
                var rssDesc = "Trung tâm công nghệ thông tin tỉnh Quảng Ninh";
                var rssUrl = SPContext.Current.Web.Url;
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(() =>
                    {
                        using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                        {
                            using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                            {
                                try
                                {
                                    string listUrl = web.Url + "/Lists/" + listCategoryName;
                                    var list = web.GetList(listUrl);
                                    if (list != null)
                                    {
                                        var catItem = list.GetItemById(Convert.ToInt32(catID));
                                        rssTitle = catItem.Title;
                                        rssDesc = "Trung tâm công nghệ thông tin tỉnh Quảng Ninh - " + catItem.Title +
                                                  " - RSS Feed";
                                        rssUrl = string.Format("{0}/{1}.aspx?CategoryId={2}", SPContext.Current.Web.Url, Constants.PageInWeb.SubPage, catID);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Utilities.LogToUls(ex);
                                }
                            }

                        }
                    });
                }
                catch (Exception)
                {

                }
                GetRSS(dtNews, rssTitle, rssUrl, rssDesc);
            }
        }

        public static void GetRSS(DataTable dtNews, string rssTitle, string rssUrl, string rssDesc)
        {
            if (dtNews != null && dtNews.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "text/xml";
                XmlTextWriter feedWriter
                  = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);

                feedWriter.WriteStartDocument();

                // These are RSS Tags
                feedWriter.WriteStartElement("rss");
                feedWriter.WriteAttributeString("version", "2.0");

                feedWriter.WriteStartElement("channel");
                feedWriter.WriteElementString("title", rssTitle);
                feedWriter.WriteElementString("link", rssUrl);
                feedWriter.WriteElementString("description", rssDesc);
                feedWriter.WriteElementString("copyright", "Copyright 2012 Trung tâm thông tin công nghệ Quảng Ninh. All rights reserved.");
                foreach (DataRow row in dtNews.Rows)
                {
                    feedWriter.WriteStartElement("item");
                    feedWriter.WriteElementString("title", Convert.ToString(row["Title"]));
                    feedWriter.WriteElementString("description", Convert.ToString(row[FieldsName.NewsRecord.English.ShortContent]));
                    feedWriter.WriteElementString("link", string.Format("{0}/{1}.aspx?NewsId={2}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, Convert.ToString(row["ID"])));
                    feedWriter.WriteElementString("pubDate", Convert.ToString(row["Modified"]));
                    feedWriter.WriteEndElement();
                }
                // Close all open tags tags
                feedWriter.WriteEndElement();
                feedWriter.WriteEndElement();
                feedWriter.WriteEndDocument();
                feedWriter.Flush();
                feedWriter.Close();

                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "text/xml";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Public);

                HttpContext.Current.Response.End();
            }
        }

        public static SPFieldLookupValueCollection GetCatsByNewsID(string newsID)
        {
            var result = new SPFieldLookupValueCollection();
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            SPQuery spQuery = new SPQuery
                                                  {
                                                      Query = string.Format(@"<Where>
                                                                                  <And>
                                                                                     <Eq>
                                                                                        <FieldRef Name='ID' />
                                                                                        <Value Type='Counter'>{0}</Value>
                                                                                     </Eq>
                                                                                     <And>
                                                                                        <Lt>
                                                                                           <FieldRef Name='ArticleStartDate' />
                                                                                           <Value IncludeTimeValue='TRUE' Type='DateTime'>{1}</Value>
                                                                                        </Lt>
                                                                                        <And>
                                                                                           <Neq>
                                                                                              <FieldRef Name='Status' />
                                                                                              <Value Type='Boolean'>1</Value>
                                                                                           </Neq>
                                                                                           <Contains>
                                                                                              <FieldRef Name='Approve' />
                                                                                              <Value Type='Lookup'>{2}</Value>
                                                                                           </Contains>
                                                                                        </And>
                                                                                     </And>
                                                                                  </And>
                                                                               </Where>", newsID,
                                                                                        SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now), Constants.Published),
                                                      RowLimit = 1
                                                  };
                            SPList list = GetListFromUrl(web, ListsName.English.NewsRecord);
                            if (list != null)
                            {
                                SPListItemCollection items = list.GetItems(spQuery);
                                if (items != null && items.Count > 0)
                                {
                                    result = new SPFieldLookupValueCollection(Convert.ToString(items[0][FieldsName.NewsRecord.English.CategoryName]));
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }
            });
            return result;
        }

        public static DataTable SearchNews(string keyWord)
        {
            string camlQuery = string.Empty;
            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = HttpContext.Current.Server.UrlDecode(keyWord);
                keyWord = SPEncode.HtmlEncode(keyWord);
                camlQuery =
                string.Format(
                    "<Where><Or><Contains><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Contains><Or><Contains><FieldRef Name='{2}' /><Value Type='Note'>{1}</Value></Contains><Contains><FieldRef Name='{3}' /><Value Type='Note'>{1}</Value></Contains></Or></Or></Where>",
                    "Title", keyWord, FieldsName.NewsRecord.English.ShortContent, FieldsName.NewsRecord.English.PublishingPageContent);
                //                camlQuery = string.Format(@"<Where>
                //                                              <And>
                //                                                 <Eq>
                //                                                    <FieldRef Name='Approve' />
                //                                                    <Value Type='Lookup'>{0}</Value>
                //                                                 </Eq>
                //                                                 <And>
                //                                                    <Lt>
                //                                                       <FieldRef Name='ArticleStartDate' />
                //                                                       <Value IncludeTimeValue='TRUE' Type='DateTime'>{1}</Value>
                //                                                    </Lt>
                //                                                    <Or>
                //                                                       <Contains>
                //                                                          <FieldRef Name='Title' />
                //                                                          <Value Type='Text'>{2}</Value>
                //                                                       </Contains>
                //                                                       <Or>
                //                                                          <Contains>
                //                                                             <FieldRef Name='ShortContent' />
                //                                                             <Value Type='Note'>{2}</Value>
                //                                                          </Contains>
                //                                                          <Contains>
                //                                                             <FieldRef Name='PublishingPageContent' />
                //                                                             <Value Type='HTML'>{2}</Value>
                //                                                          </Contains>
                //                                                       </Or>
                //                                                    </Or>
                //                                                 </And>
                //                                              </And>
                //                                           </Where>", Constants.Published, DateTime.Now, keyWord);
            }

            var query = new SPQuery();
            query.Query = camlQuery;
            DataTable table = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            string listUrl = web.Url + "/Lists/" + ListsName.English.NewsRecord;
                            var list = web.GetList(listUrl);
                            if (list != null)
                            {
                                var items = list.GetItems(query);
                                if (items != null && items.Count > 0)
                                {
                                    table = items.GetDataTable();

                                    for (int i = 0; i < items.Count; i++)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
                                        {
                                            SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
                                            table.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
                                        }
                                        var time = Convert.ToDateTime(items[i][FieldsName.Created]);
                                        table.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToUls(ex);
                        }
                    }

                }
            });
            //try
            //{
            //    string listUrl = SPContext.Current.Web.Url + "/Lists/" + ListsName.English.NewsRecord;
            //    var list = SPContext.Current.Web.GetList(listUrl);
            //    if (list != null)
            //    {
            //        var items = list.GetItems(query);
            //        if (items != null && items.Count > 0)
            //        {
            //            table = items.GetDataTable();

            //            for (int i = 0; i < items.Count; i++)
            //            {
            //                if (!string.IsNullOrEmpty(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName])))
            //                {
            //                    SPFieldLookupValue catLK = new SPFieldLookupValue(Convert.ToString(items[i][FieldsName.NewsRecord.English.CategoryName]));
            //                    table.Rows[i][FieldsName.CategoryId] = catLK.LookupId;
            //                }
            //                var time = Convert.ToDateTime(items[i][FieldsName.Created]);
            //                table.Rows[i][FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utilities.LogToUls(ex);
            //}
            return table;
        }

        public static void ViewCountCalculated(SPWeb spWeb, string listName, string viewCountField, int newsId)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(spWeb.Site.ID))
                {
                    using (var web = site.OpenWeb(spWeb.ID))
                    {
                        try
                        {
                            SPList list = GetDocListFromUrl(web, listName);
                            int id = Convert.ToInt32(newsId);
                            SPListItem items = list.GetItemById(id);
                            if (items != null)
                            {
                                string viewcount = Convert.ToString(items[viewCountField]);
                                if (!string.IsNullOrEmpty(viewcount))
                                {
                                    int count = Convert.ToInt32(viewcount);
                                    items[viewCountField] = ++count;
                                    web.AllowUnsafeUpdates = true;
                                    items.Update();
                                }
                                else
                                {
                                    items[viewCountField] = 1;
                                    web.AllowUnsafeUpdates = true;
                                    items.Update();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToUls(ex);
                        }
                    }
                }
            });
        }

//        /// <summary>
//        /// Get 
//        /// </summary>
//        /// <param name="categoryNameValue"></param>
//        /// <param name="listCategoryName"></param>
//        /// <returns></returns>
//        public static int GetCategoryIdByCategoryName(string categoryNameValue, string listCategoryName)
//        {
//            if (categoryNameValue != null && categoryNameValue.Contains(";#"))
//            {
//                categoryNameValue = categoryNameValue.Substring(categoryNameValue.IndexOf("#") + 1);
//            }
//            string camlQuery = string.Format(@"<Where>
//                                                  <Eq>
//                                                     <FieldRef Name='Title' />
//                                                     <Value Type='Text'>{0}</Value>
//                                                  </Eq>
//                                               </Where>", categoryNameValue);
//            var query = new SPQuery { Query = camlQuery, RowLimit = 1 };
//            int categoryId = 0;
//            SPSecurity.RunWithElevatedPrivileges(() =>
//            {
//                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
//                {
//                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
//                    {
//                        try
//                        {
//                            string listUrl = web.Url + "/Lists/" + listCategoryName;
//                            var list = web.GetList(listUrl);
//                            if (list != null)
//                            {
//                                var items = list.GetItems(query);
//                                if (items != null && items.Count > 0)
//                                {
//                                    categoryId = Convert.ToInt16(items[0][FieldsName.Id]);
//                                }
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            LogToUls(ex);
//                        }
//                    }
//                }
//            });
//            return categoryId;
//        }

        ///// <summary>
        ///// Get 
        ///// </summary>
        ///// <param name="newsId"></param>
        ///// <param name="listName"></param>
        ///// <param name="listNameCategory"></param>
        ///// <returns></returns>
        //public static int GetCategoryIdByItemId(int newsId, string listName, string listNameCategory)
        //{
        //    string categoryName = string.Empty;
        //    int categoryId = 0;

        //    SPSecurity.RunWithElevatedPrivileges(() =>
        //    {
        //        using (var site = new SPSite(SPContext.Current.Web.Site.ID))
        //        {
        //            using (var web = site.OpenWeb(SPContext.Current.Web.ID))
        //            {
        //                try
        //                {
        //                    //get listName information by newsId => CategoryName
        //                    string listUrl = web.Url + "/Lists/" + listNameCategory;
        //                    var list = web.GetList(listUrl);
        //                    if (list != null)
        //                    {
        //                        var items = list.GetItemById(newsId);
        //                        if (items != null)
        //                        {
        //                            categoryName = Convert.ToString(items[FieldsName.CategoryName]);
        //                            categoryId = GetCategoryIdByCategoryName(categoryName, listNameCategory);
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    LogToUls(ex);
        //                }
        //            }
        //        }
        //    });
        //    return categoryId;
        //}

        ///// <summary>
        ///// Update CategoryID field
        ///// </summary>
        ///// <param name="listCategoryName"></param>
        ///// <param name="categoryName"></param>
        ///// <param name="dataTable"></param>
        //public static void AddCategoryIdToTable(string listCategoryName, string categoryName, ref DataTable dataTable)
        //{
        //    if (dataTable != null && dataTable.Rows.Count > 0)
        //    {
        //        if (!dataTable.Columns.Contains(FieldsName.CategoryId))
        //        {
        //            dataTable.Columns.Add(FieldsName.CategoryId, Type.GetType("System.String"));
        //        }

        //        if (!dataTable.Columns.Contains(FieldsName.ArticleStartDateTemp))
        //        {
        //            dataTable.Columns.Add(FieldsName.ArticleStartDateTemp, Type.GetType("System.String"));
        //        }
        //        var time = new DateTime();
        //        foreach (DataRow row in dataTable.Rows)
        //        {
        //            row[FieldsName.CategoryId] = GetCategoryIdByCategoryName(Convert.ToString(row[categoryName]), listCategoryName);
        //            time = Convert.ToDateTime(row[FieldsName.Created]);
        //            row[FieldsName.ArticleStartDateTemp] = string.Format(" {0}/{1}/{2}", time.Day, time.Month, time.Year);
        //        }
        //    }
        //}

        /// <summary>
        /// Set max length when display sapo
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static void SetSapoTextLength(ref DataTable table)
        {
            if (table != null && table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    table.Rows[i][FieldsName.Title] = GetTextForSapo(Convert.ToString(table.Rows[i][FieldsName.Title]), 129);
                    table.Rows[i][FieldsName.NewsRecord.English.ShortContent] = GetTextForSapo(Convert.ToString(table.Rows[i][FieldsName.NewsRecord.English.ShortContent]), 200);
                }
            }
        }
    }
}
