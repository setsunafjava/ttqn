using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using Microsoft.SharePoint;
using CQ.SharePoint.QN.Common;
using CQ.SharePoint.QN.Core.Helpers;
using System.Web.UI.WebControls;

namespace CQ.SharePoint.QN
{
    public class ExpenseClaimStructure
    {
        public static void CreateListStructure(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.Japanese.ExpenseClaim,
                Name = ListsName.English.ExpenseClaim,
                OnQuickLaunch = false
            };

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ExpenseClaim.English.DeptCode, FieldsName.ExpenseClaim.Japanese.DeptCode));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ExpenseClaim.English.TotalAmount, FieldsName.ExpenseClaim.Japanese.TotalAmount));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.ExpenseClaim.English.EmployeeNumber, FieldsName.ExpenseClaim.Japanese.EmployeeNumber));

            SPList list = helper.Apply();

            list.EnableAttachments = true;

            list.Update();
        }
    }
}
