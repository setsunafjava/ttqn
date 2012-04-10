using System;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class MultipleLinesTextFieldCreator : BaseFieldCreator
    {
        public MultipleLinesTextFieldCreator(string internalName, string displayName)
            : base(internalName, displayName)
        {
            NumberOfLines = 6;
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
        /// Gets or sets the number of lines to display in the field. 
        /// </summary>
        public int NumberOfLines { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether rich text formatting can be used in the field. 
        /// </summary>
        public bool RichText { get; set; }

        /// <summary>
        /// Gets or sets the rich text mode for the field.
        /// </summary>
        public SPRichTextMode RichTextMode { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether to append changes to the existing text.
        /// </summary>
        public bool AppendOnly { get; set; }

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.Note, Required);
            var field = (SPFieldMultiLineText) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            field.NumberOfLines = NumberOfLines;
            field.RichText = RichText;
            if (RichText)
            {
                field.RichTextMode = RichTextMode;    
            }
            
            field.AppendOnly = AppendOnly;
            field.Title = Name;
            field.Update();
        }
    }
}