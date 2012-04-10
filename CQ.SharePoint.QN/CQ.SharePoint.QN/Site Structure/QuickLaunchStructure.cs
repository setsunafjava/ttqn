using System.Web;
using Microsoft.SharePoint;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN
{
    public class QuickLaunchStructure
    {
        public static void CreateQuickLaunch(SPWeb web)
        {
            var quickLaunch = new QuickLaunchHelper(web);
        }
    }
}
