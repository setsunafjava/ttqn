using System.Collections.Specialized;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public class ChoiceFieldCreator : BaseFieldCreator
    {
        public ChoiceFieldCreator(string internalName, string displayName) : base(internalName, displayName)
        {
            Choices = new StringCollection();
        }

        /// <summary>
        /// Determines whether to display the choice field as radio buttons or as a drop-down list. 
        /// </summary>
        public SPChoiceFormatType EditFormat { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that determines whether a text box for typing an alternative value is provided for the multichoice field.
        /// </summary>
        public bool FillInChoice { get; set; }

        /// <summary>
        /// Gets the choices that are used in the multichoice field.
        /// </summary>
        public StringCollection Choices { get; set; }

        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var name = list.Fields.Add(InternalName, SPFieldType.Choice, Required);
            var field = (SPFieldChoice) list.Fields.GetFieldByInternalName(name);
            field.Description = Description;
            foreach (var choice in Choices)
            {
                field.Choices.Add(choice);
            }
            field.EditFormat = EditFormat;
            field.FillInChoice = FillInChoice;
            field.DefaultValue = DefaultValue;
            field.Title = Name;
            field.Update();
        }
    }
}