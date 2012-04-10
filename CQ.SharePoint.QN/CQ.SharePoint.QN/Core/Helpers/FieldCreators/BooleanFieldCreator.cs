using System;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    /// <summary>
    /// Represents a Boolean field type. 
    /// </summary>
    public class BooleanFieldCreator : BaseFieldCreator
    {
        public BooleanFieldCreator(string internalName, string displayName)
            : base(internalName, displayName)
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

        /// <summary>
        /// Gets or sets the default value for a field is Yes.
        /// </summary>
        public bool DefaultYesValue { get; set; }

        public override string DefaultValue
        {
            get { return DefaultYesValue ? "1" : "0"; }
            set
            {
                if (value == "1" || value == "0")
                {
                    DefaultYesValue = value == "1";
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value", @"DefaultValue must be is 0 or 1");
                }
            }
        }

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.Boolean, Required);
            var field = (SPFieldBoolean) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.DefaultValue = DefaultYesValue ? "1" : "0";
            field.Title = Name;
            field.Update();
        }
    }
}