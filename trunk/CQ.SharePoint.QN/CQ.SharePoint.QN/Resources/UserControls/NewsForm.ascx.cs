using System;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            if (SPContext.Current.FormContext.FormMode.Equals(SPControlMode.Display))
            {
                ddlCat.Visible = false;
                lblCat.Visible = true;
                if (!IsPostBack)
                {

                }
            }
            else
            {
                ddlCat.Visible = true;
                lblCat.Visible = false;
            }
            if (!IsPostBack)
            {
                BindCat();
                if (SPContext.Current.FormContext.FormMode.Equals(SPControlMode.Edit))
                {
                    var item = SPContext.Current.Item;
                    if (!string.IsNullOrEmpty(Convert.ToString(item[FieldsName.NewsRecord.English.CategoryName])))
                    {
                        SPFieldLookupValueCollection returnVal = new SPFieldLookupValueCollection(Convert.ToString(item[FieldsName.NewsRecord.English.CategoryName]));
                        foreach (SPFieldLookupValue lookupValue in returnVal)
                        {
                            ListItem bitem = ddlCat.Items.FindByValue(lookupValue.LookupId.ToString());
                            if (bitem != null)
                            {
                                bitem.Selected = true;
                            }
                        }
                    }
                }
            }
        }

        private void BindCat()
        {
            var lkField =
                (SPFieldLookup)SPContext.Current.List.Fields.GetFieldByInternalName(FieldsName.NewsRecord.English.CategoryName);
            GetAvailableValues(lkField, ddlCat);
        }

        private void GetAvailableValues(SPFieldLookup f, ListBox ddl)
        {
            var lookupWeb = SPContext.Current.Web;

            SPList lookupList = lookupWeb.Lists[new Guid(f.LookupList)];
            string caml = @"<Where><IsNull><FieldRef Name='ParentName' /></IsNull></Where><OrderBy><FieldRef Name='Title' /></OrderBy>";
            var query = new SPQuery()
            {
                Query = caml
            };
            var items = lookupList.GetItems(query);
            if (items != null && items.Count > 0)
            {
                foreach (SPListItem item in items)
                {
                    ddl.Items.Add(new ListItem(item[f.LookupField].ToString(), item.ID.ToString()));
                    GetAvailableValues(lookupList, f.LookupField, ddl, item.ID.ToString());
                }
            }
        }

        private void GetAvailableValues(SPList lookupList, string fId, ListBox ddl, string parentValue)
        {
            string caml = @"<Where><Eq><FieldRef Name='ParentName' LookupId='TRUE' /><Value Type='Lookup'>" + parentValue + "</Value></Eq></Where><OrderBy><FieldRef Name='Title' /></OrderBy>";
            var query = new SPQuery()
            {
                Query = caml
            };
            var items = lookupList.GetItems(query);
            if (items != null && items.Count > 0)
            {
                foreach (SPListItem item in items)
                {
                    ddl.Items.Add(new ListItem("--- " + item[fId].ToString(), item.ID.ToString()));
                    GetAvailableValues1(lookupList, fId, ddl, item.ID.ToString());
                }
            }
        }

        private void GetAvailableValues1(SPList lookupList, string fId, ListBox ddl, string parentValue)
        {
            string caml = @"<Where><Eq><FieldRef Name='ParentName' LookupId='TRUE' /><Value Type='Lookup'>" + parentValue + "</Value></Eq></Where><OrderBy><FieldRef Name='Title' /></OrderBy>";
            var query = new SPQuery()
            {
                Query = caml
            };
            var items = lookupList.GetItems(query);
            if (items != null && items.Count > 0)
            {
                foreach (SPListItem item in items)
                {
                    ddl.Items.Add(new ListItem("------ " + item[fId].ToString(), item.ID.ToString()));
                    GetAvailableValues2(lookupList, fId, ddl, item.ID.ToString());
                }
            }
        }

        private void GetAvailableValues2(SPList lookupList, string fId, ListBox ddl, string parentValue)
        {
            string caml = @"<Where><Eq><FieldRef Name='ParentName' LookupId='TRUE' /><Value Type='Lookup'>" + parentValue + "</Value></Eq></Where><OrderBy><FieldRef Name='Title' /></OrderBy>";
            var query = new SPQuery()
            {
                Query = caml
            };
            var items = lookupList.GetItems(query);
            if (items != null && items.Count > 0)
            {
                foreach (SPListItem item in items)
                {
                    ddl.Items.Add(new ListItem("--------- " + item[fId].ToString(), item.ID.ToString()));
                    GetAvailableValues3(lookupList, fId, ddl, item.ID.ToString());
                }
            }
        }

        private void GetAvailableValues3(SPList lookupList, string fId, ListBox ddl, string parentValue)
        {
            string caml = @"<Where><Eq><FieldRef Name='ParentName' LookupId='TRUE' /><Value Type='Lookup'>" + parentValue + "</Value></Eq></Where><OrderBy><FieldRef Name='Title' /></OrderBy>";
            var query = new SPQuery()
            {
                Query = caml
            };
            var items = lookupList.GetItems(query);
            if (items != null && items.Count > 0)
            {
                foreach (SPListItem item in items)
                {
                    ddl.Items.Add(new ListItem("------------ " + item[fId].ToString(), item.ID.ToString()));
                    GetAvailableValues3(lookupList, fId, ddl, item.ID.ToString());
                }
            }
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

            var item = SPContext.Current.Item;

            SPFieldLookupValueCollection returnVal = new SPFieldLookupValueCollection();
            foreach (ListItem lItem in ddlCat.Items)
            {
                if (lItem.Selected)
                {
                    if (lItem.Value != "0" && lItem.Text != "(None)")
                    {
                        returnVal.Add((new SPFieldLookupValue(
                          int.Parse(lItem.Value), lItem.Text)));
                    }
                }
            }
            if (returnVal.Count > 0)
            {
                item[FieldsName.NewsRecord.English.CategoryName] = returnVal;
            }

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
