using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    /// <summary>
    ///   A helper support create a new list instance
    /// </summary>
    public class ListHelper
    {
        private readonly List<BaseFieldCreator> creators;
        private readonly SPWeb web;
        private string archivedListName;

        public ListHelper(SPWeb web)
        {
            this.web = web;
            creators = new List<BaseFieldCreator>();
            ListTemplateType = SPListTemplateType.GenericList;
            EnableAttachments = true;
        }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SPListTemplateType ListTemplateType { get; set; }

        public bool OnQuickLaunch { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether attachments can be added to items in the list.
        /// </summary>
        public bool EnableAttachments { get; set; }

        /// <summary>
        /// Create a list with fields
        /// </summary>
        /// <returns></returns>
        public SPList Apply()
        {
            var list = CreateList(Name, Title, Description, ListTemplateType, OnQuickLaunch);

            if (!EnableAttachments)
            {
                list.EnableAttachments = false;
            }

            list.Update();

            return list;
        }

        private SPList CreateList(string name, string title, string description, SPListTemplateType listTemplateType, bool onQuickLaunch)
        {
            SPList list = null;

            try
            {
                string url = string.Empty;
                if (listTemplateType == SPListTemplateType.PictureLibrary ||
                    listTemplateType == SPListTemplateType.DocumentLibrary ||
                    listTemplateType == SPListTemplateType.WebPageLibrary ||
                    listTemplateType == SPListTemplateType.XMLForm)
                    url = web.Url + "/" + name;
                else
                    url = web.Url + "/Lists/" + name;

                list = web.GetList(url);
            }
            catch (Exception) { }

            if (list == null)
            {
                var id = web.Lists.Add(name, description, listTemplateType);
                list = web.Lists[id];

                try
                {
                    list.OnQuickLaunch = onQuickLaunch;

                    foreach (var creator in creators)
                    {
                        creator.CreateField(list);
                    }

                    list.Title = title;

                    list.Update();

                    return list;
                }
                catch (Exception ex)
                {
                    if (id != Guid.Empty)
                    {
                        web.Lists[id].Delete();
                    }

                    throw new ArgumentException(
                        string.Format(CultureInfo.InvariantCulture, "Cannot create a list '{0}' because: {1}", title, ex.Message), ex);
                }
            }
            
            return list;
        }

        public void AddField(BaseFieldCreator creator)
        {
            creators.Add(creator);
        }

        /// <summary>
        /// Indicates whether field will indexed.
        /// </summary>
        /// <param name="field"></param>
        public static void CreateIndexedField(SPField field)
        {
            if (!field.Indexed)
            {
                field.Indexed = true;
                field.Update();
            }
        }

        /// <summary>
        /// Indicates whether field will indexed.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fieldName"></param>
        public static void CreateIndexedField(SPList list, string fieldName)
        {
            var field = list.Fields[fieldName];
            CreateIndexedField(field);
        }
    }
}