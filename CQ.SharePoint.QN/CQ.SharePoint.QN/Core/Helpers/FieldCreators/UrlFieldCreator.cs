using System;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class UrlFieldCreator : BaseFieldCreator
    {
        public UrlFieldCreator(string internalName, string displayName) : base(internalName, displayName)
        {
        }

        public override bool EnforceUniqueValues
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override string ValidationFormula
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public SPUrlFieldFormatType DisplayFormat { get; set; }

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.URL, Required);
            var field = (SPFieldUrl) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.DisplayFormat = DisplayFormat;
            field.Title = Name;
            field.Update();
        }
    }
}