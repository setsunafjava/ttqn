using System.Web.UI;

namespace CQ.SharePoint.QN.Core.WebControls
{
    internal class ValidatorControl : IValidator
    {
        public ValidatorControl()
        {
            IsValid = true;
        }

        #region IValidator Members

        public string ErrorMessage { get; set; }

        public bool IsValid { get; set; }

        public void Validate()
        {
        }

        #endregion

        public void RenderControl(HtmlTextWriter writer)
        {
            if (IsValid)
            {
                return;
            }

            writer.Write("<br/>");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-formvalidation");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.AddAttribute("role", "alert");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(ErrorMessage);
            writer.RenderEndTag(); // span
            writer.RenderEndTag(); // span
        }
    }
}