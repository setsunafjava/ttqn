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
            
        }
    }
}
