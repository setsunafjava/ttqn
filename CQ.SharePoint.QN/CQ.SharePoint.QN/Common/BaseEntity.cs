using System;
using System.Globalization;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN
{
    public class BaseEntity
    {
        public SPListItem InnerItem { get; set; }

        public int Id
        {
            get { return InnerItem.ID; }
            set { InnerItem["ID"] = value; }
        }

        public BaseEntity()
        {
        }

        public BaseEntity(SPListItem item)
        {
            InnerItem = item;
        }

        public void Update()
        {
            InnerItem.Update();
        }

        public void SystemUpdate()
        {
            InnerItem.SystemUpdate();
        }

        protected T GetLookupValue<T>(string primary, string lookupField)
        {
            string fieldName = string.Format(CultureInfo.InvariantCulture,"{0}:{1}", primary, lookupField);
            string[] values = Convert.ToString(InnerItem[fieldName], CultureInfo.InvariantCulture).Split(new[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length > 1)
            {
                return (T)Convert.ChangeType(values[1], typeof(T));
            }

            return default(T);
        }

        protected T GetLookupValue<T>(string primary)
        {
            string fieldName = primary;
            string[] values = Convert.ToString(InnerItem[fieldName], CultureInfo.InvariantCulture).Split(new[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length > 1)
            {
                return (T)Convert.ChangeType(values[1], typeof(T));
            }

            return default(T);
        }
    }
}