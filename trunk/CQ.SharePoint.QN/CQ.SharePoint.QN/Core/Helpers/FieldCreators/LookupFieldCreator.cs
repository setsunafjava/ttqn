using System;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class LookupFieldCreator : BaseFieldCreator
    {
        public LookupFieldCreator(string internalName, string displayName) : base(internalName, displayName)
        {
        }

        public override string ValidationFormula
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether multiple values can be used in the lookup field.
        /// </summary>
        public bool AllowMultipleValues { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether the lookup field is discoverable from the list to which it looks for its value.
        /// </summary>
        public bool IsRelationship { get; set; }

        /// <summary>
        /// Gets or sets a field name for lookup.
        /// </summary>
        public string LookupField { get; set; }

        /// <summary>
        /// Gets or sets a list name for lookup.
        /// </summary>
        public string LookupList { get; set; }

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var targetList = list.ParentWeb.Lists[LookupList];
            var name = list.Fields.AddLookup(InternalName, targetList.ID, Required);
            var field = (SPFieldLookup) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.LookupField = targetList.Fields[LookupField].InternalName;
            field.AllowMultipleValues = AllowMultipleValues;

            //if (EnforceUniqueValues)
            //{
            //    field.Indexed = true;
            //    field.EnforceUniqueValues = true;
            //}

            field.Title = Name;

            field.Update();
        }
    }
}