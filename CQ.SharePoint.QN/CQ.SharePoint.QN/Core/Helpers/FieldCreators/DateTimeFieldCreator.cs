using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class DateTimeFieldCreator : BaseFieldCreator
    {
        public DateTimeFieldCreator(string internalName, string displayName)
            : base(internalName, displayName)
        {
        }

        /// <summary>
        /// Gets or sets the type of date and time format that is used in the field. 
        /// </summary>
        public SPDateTimeFieldFormatType DisplayFormat { get; set; }

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.DateTime, Required);
            var field = (SPFieldDateTime) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.DisplayFormat = DisplayFormat;
            field.DefaultValue = DefaultValue;
            field.Title = Name;
            field.Update();
        }
    }
}