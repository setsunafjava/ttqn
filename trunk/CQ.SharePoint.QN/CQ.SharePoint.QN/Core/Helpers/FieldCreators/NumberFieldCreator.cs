using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class NumberFieldCreator : BaseFieldCreator
    {
        public NumberFieldCreator(string internalName, string displayName)
            : base(internalName, displayName)
        {
            MinimumValue = double.MinValue;
            MaximumValue = double.MaxValue;
        }

        /// <summary>
        /// Gets or sets the number of decimal places to use when displaying the field. 
        /// </summary>
        public SPNumberFormatTypes DisplayFormat { get; set; }

        /// <summary>
        /// Gets or sets a maximum value for the field. 
        /// </summary>
        public double MaximumValue { get; set; }

        /// <summary>
        /// Gets or sets a minimum value for the field. 
        /// </summary>
        public double MinimumValue { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether to render the field as a percentage. 
        /// </summary>
        public virtual bool ShowAsPercentage { get; set; }

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.Number, Required);
            var field = (SPFieldNumber) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.MinimumValue = MinimumValue;
            field.MaximumValue = MaximumValue;
            field.DisplayFormat = DisplayFormat;
            field.DefaultValue = DefaultValue;
            field.ShowAsPercentage = ShowAsPercentage;
            field.Title = Name;
            field.Update();
        }
    }
}