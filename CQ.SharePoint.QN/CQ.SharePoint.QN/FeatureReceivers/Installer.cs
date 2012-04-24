using System;
using Microsoft.SharePoint;
using System.Runtime.InteropServices;

namespace CQ.SharePoint.QN
{
    [Guid("ad0ab4d0-bb06-4d7f-813d-7cbc5a4315a9")]
    public class Installer : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;

            if (web == null) return;
            //System.Diagnostics.Debugger.Launch();

            //Change master page
            var webUrl = web.Site.ServerRelativeUrl;
            if (webUrl.Equals("/"))
            {
                webUrl = "";
            }
            //web.MasterUrl = webUrl + "/_catalogs/masterpage/CqHome.master";
            web.CustomMasterUrl = webUrl + "/_catalogs/masterpage/CqHome.master";
            web.AllowUnsafeUpdates = true;
            web.Update();

            web.AnonymousState = SPWeb.WebAnonymousState.On;
            web.AllowUnsafeUpdates = true;
            web.Update();

             //Create Site Structure                
            SiteStructure.CreateSiteStructure(web);
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            // To do

            // -- Delete Event --
        }

        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {
            // To do
        }

        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {
            // To do
        }
    }
}
