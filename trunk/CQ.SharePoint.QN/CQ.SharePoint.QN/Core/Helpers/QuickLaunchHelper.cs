using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;

namespace CQ.SharePoint.QN.Common
{
    public class QuickLaunchHelper
    {
        private SPNavigationNodeCollection nodes = null;

        public QuickLaunchHelper(SPWeb web)
        {
            nodes = web.Navigation.QuickLaunch;

            // before create new delete all old items
            // leave 02 node cannot be deleted

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes[i].Delete();
            }
        }

        public SPNavigationNode AddHeading(string title, string url, string group)
        {
            //if (nodes != null)
            //{
            //    SPNavigationNode nodeItem =
            //        SPNavigationSiteMapNode.CreateSPNavigationNode(title, url,
            //                                                       Microsoft.SharePoint.
            //                                                           Publishing.NodeTypes.
            //                                                           Heading, nodes);
            //    nodeItem.Properties.Add("Audience", ";;;;" + group);
            //    nodeItem.Update();

            //    return nodeItem;
            //}
            return null;
        }

        public SPNavigationNode AddNavigationLink(SPNavigationNode parentNode, string title, string url, string group)
        {
            var subNode = new SPNavigationNode(title, url);
            parentNode.Children.Add(subNode, parentNode);
            subNode.Properties.Add("Audience", ";;;;" + group);
            subNode.Update();
            parentNode.Update();

            return subNode;
        }
    }
}