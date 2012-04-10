using System;
using System.Collections.Specialized;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class MultipleChoiceFieldCreator : BaseFieldCreator
    {
        public MultipleChoiceFieldCreator(string internalName, string displayName) : base(internalName, displayName)
        {
            Choices = new StringCollection();
        }

        /// <summary>
        /// Gets or sets a Boolean value that determines whether a text box for typing an alternative value is provided for the multichoice field. 
        /// </summary>
        public bool FillInChoice { get; set; }

        [Obsolete("Use FillInChoice")]
        public bool AllowFillIn { get; set; }

        public StringCollection Choices { get; set; }

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

            var name = list.Fields.Add(InternalName, SPFieldType.MultiChoice, Required);
            var field = (SPFieldMultiChoice) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            foreach (var choice in Choices)
            {
                field.Choices.Add(choice);
            }

            field.FillInChoice = FillInChoice;
            field.DefaultValue = DefaultValue;
            field.Title = Name;
            field.Update();
        }
    }
}