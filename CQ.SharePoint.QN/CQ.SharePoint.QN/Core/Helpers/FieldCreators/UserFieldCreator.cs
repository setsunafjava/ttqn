using System;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class UserFieldCreator : BaseFieldCreator
    {
        public UserFieldCreator(string internalName, string displayName) : base(internalName, displayName)
        {
        }

        public override string ValidationFormula
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public SPFieldUserSelectionMode SelectionMode { get; set; }
        public bool AllowMultipleValues { get; set; }
        public int SelectionGroup { get; set; }

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.User, Required);
            var field = (SPFieldUser) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.SelectionMode = SelectionMode;
            field.AllowMultipleValues = AllowMultipleValues;
            field.LookupField = "ImnName";
            if (SelectionGroup > 0)
            {
                field.SelectionGroup = SelectionGroup;
            }
            field.Title = Name;
            field.Update();            
        }
    }
}