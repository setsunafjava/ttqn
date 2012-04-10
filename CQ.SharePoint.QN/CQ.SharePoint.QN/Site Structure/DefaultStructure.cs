using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using CQ.SharePoint.QN.Common;
using CQ.SharePoint.QN.Core.Helpers;

namespace CQ.SharePoint.QN
{
    public class DefaultStructure
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="web"></param>
        public static void Create(SPWeb web)
        {
            //web.BreakRoleInheritance(true);
            Console.WriteLine("CreateUserGroupDefault...");
            CreateUserGroupDefault(web);
            //Console.WriteLine("AssignPermissionForWeb...");
            //AssignPermissionForWeb(web);
        }

        /// <summary>
        /// CreateUserGroupDefault
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="web"></param>
        public static void CreateUserGroupDefault(SPWeb web)
        {
            SPUser defaultUser = web.Site.SystemAccount;
            SPGroup admin = SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.FullControlGroup, defaultUser, PermissionCQQN.GroupDescription.FullControlGroup);

            SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.FullControlGroup, admin, PermissionCQQN.GroupDescription.FullControlGroup);
            SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.ManagementGroup, admin, PermissionCQQN.GroupDescription.ManagementGroup);
            SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.DeleteGroup, admin, PermissionCQQN.GroupDescription.DeleteGroup);
            SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.EditGroup, admin, PermissionCQQN.GroupDescription.EditGroup);
            SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.SubmitterGroup, admin, PermissionCQQN.GroupDescription.SubmitterGroup);
            SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.ViewGroup, admin, PermissionCQQN.GroupDescription.ViewGroup);
            SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.AccountingDepartmentGroup, admin, PermissionCQQN.GroupDescription.AccountingDepartmentGroup);
            SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.SystemAdmin, admin, PermissionCQQN.GroupDescription.SystemAdmin);
            SecurityHelper.AddGroup(web, PermissionCQQN.GroupName.Approver, admin, PermissionCQQN.GroupDescription.Approver);
        }

        
    }
}
