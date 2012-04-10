using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class SingleLineTextFieldCreator : BaseFieldCreator
    {
        public SingleLineTextFieldCreator(string internalName, string displayName)
            : base(internalName, displayName)
        {
            MaxLength = 255;
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be typed in the field. 
        /// </summary>
        public int MaxLength { get; set; }

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(InternalName)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.Text, Required);
            var field = (SPFieldText) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.MaxLength = MaxLength;
            field.DefaultValue = DefaultValue;
            field.Title = Name;
            field.Update();
        }
    }
}