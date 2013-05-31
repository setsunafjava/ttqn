using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.UserControls
{
    /// <summary>
    /// ExpenseClaim
    /// </summary>
    public partial class NewsForm : UserControl, IValidator
    {
        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// IsValid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Validate
        /// </summary>
        public void Validate()
        {
            IsValid = true;
        }

        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e">EventArgs e</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            SPContext.Current.FormContext.OnSaveHandler += CustomSaveHandler;
            Page.Validators.Add(this);

        }
        
        /// <summary>
        /// CustomSaveHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CustomSaveHandler(object sender, EventArgs e)
        {
            SPContext.Current.Web.AllowUnsafeUpdates = true;
            //Save item to list
            SaveButton.SaveItem(SPContext.Current, false, string.Empty);
        }

        /// <summary>
        /// OnPreRender
        /// </summary>
        /// <param name="e">EventArgs e</param>
        protected override void OnPreRender(EventArgs e)
        {
            
            base.OnPreRender(e);
        }
    }
}
