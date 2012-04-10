using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class CurrencyFieldCreator : NumberFieldCreator
    {
        public CurrencyFieldCreator(string internalName, string displayName)
            : base(internalName, displayName)
        {
        }

        /// <summary>
        /// Gets or sets the currency symbol that is used to format the field's value, and also the position of the currency symbol (for example, whether before or after numeric values).
        /// </summary>
        public int CurrencyLocaleId { get; set; }
        
        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.Currency, Required);
            var field = (SPFieldCurrency) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.MinimumValue = MinimumValue;
            field.MaximumValue = MaximumValue;
            field.DisplayFormat = DisplayFormat;
            field.DefaultValue = DefaultValue;
            field.CurrencyLocaleId = CurrencyLocaleId;
            field.Title = Name;
            field.Update();
        }
    }
}