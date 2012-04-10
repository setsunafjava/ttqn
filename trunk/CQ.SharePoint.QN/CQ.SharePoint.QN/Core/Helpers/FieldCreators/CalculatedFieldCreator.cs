using System;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class CalculatedFieldCreator : BaseFieldCreator
    {
        private SPFieldType outputType;

        public CalculatedFieldCreator(string internalName, string displayName)
            : base(internalName, displayName)
        {
            OutputType = SPFieldType.Text;
        }

        /// <summary>
        /// Gets or sets the formula that is used for calculation in the field. 
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// Gets or sets how values are formatted in the field. 
        /// </summary>
        public SPFieldType OutputType
        {
            get { return outputType; }
            set
            {
                if (value == SPFieldType.Text || value == SPFieldType.Number || value == SPFieldType.Currency ||
                    value == SPFieldType.DateTime || value == SPFieldType.Boolean)
                {
                    outputType = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value",
                                                          @"OutputType must be Text/Number/Currency/DateTime/Boolean");
                }
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that determines whether values in the field are displayed as percentages. 
        /// </summary>
        public bool ShowAsPercentage { get; set; }

        /// <summary>
        /// Gets or sets the number format that is displayed in the field. 
        /// </summary>
        public SPNumberFormatTypes NumberFormat { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time formatting that is used in the field. 
        /// </summary>
        public SPDateTimeFieldFormatType DateFormat { get; set; }

        /// <summary>
        /// Gets or sets the locale ID that is used for currency on the Web site.
        /// </summary>
        public int CurrencyLocaleId { get; set; }

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

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.Calculated, Required);
            var field = (SPFieldCalculated) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.Formula = Formula;
            field.OutputType = OutputType;

            switch (OutputType)
            {
                case SPFieldType.DateTime:
                    field.DateFormat = DateFormat;
                    break;
                case SPFieldType.Number:
                    field.DisplayFormat = NumberFormat;
                    field.ShowAsPercentage = ShowAsPercentage;
                    break;
                case SPFieldType.Currency:
                    field.DisplayFormat = NumberFormat;
                    field.CurrencyLocaleId = CurrencyLocaleId;
                    break;
            }

            field.Title = Name;

            field.Update();
        }
    }
}