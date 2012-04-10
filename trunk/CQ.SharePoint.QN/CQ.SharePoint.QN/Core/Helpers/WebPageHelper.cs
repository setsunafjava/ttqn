using System;
using System.Text;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Core.Helpers
{
    public static class WebPageHelper
    {
        public static void CreateWebPage(SPWeb web, string fileName, byte[] fileContent)
        {
            web.RootFolder.Files.Add(fileName, fileContent);
        }

        public static void CreateDefaultWebPage(SPWeb web, string fileName, bool overwrite)
        {
            CreateDefaultWebPage(web, fileName, overwrite, "Microsoft.SharePoint.Publishing.PublishingLayoutPage, Microsoft.SharePoint.Publishing,Version=12.0.0.0,Culture=neutral, PublicKeyToken=71e9bce111e9429c");
        }

        public static void CreateDefaultWebPage(SPWeb web, string fileName, bool overwrite, string inherits)
        {
            var exists = false;
            try
            {
                var checkFile = web.RootFolder.Files[fileName];
                exists = true;
            }
            catch (Exception)
            {
                exists = false;
            }

            if (exists && !overwrite)
            {
                return;
            }

            if (exists)
            {
                var file = web.RootFolder.Files[fileName];
                file.Delete();
            }
            
            var fileContent = BuildDefaultPageContent(inherits);
            var fileData = Encoding.UTF8.GetBytes(fileContent);
            CreateWebPage(web, fileName, fileData);
        }

        private static string BuildDefaultPageContent(string inherits)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("<%@ Page language=\"C#\" MasterPageFile=\"~masterurl/default.master\" Inherits=\"{0}\" meta:webpartpageexpansion=\"full\" %>", inherits));
            sb.AppendLine("<%@ Register Tagprefix=\"SharePoint\" Namespace=\"Microsoft.SharePoint.WebControls\" Assembly=\"Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
            sb.AppendLine("<%@ Register Tagprefix=\"WebPartPages\" Namespace=\"Microsoft.SharePoint.WebPartPages\" Assembly=\"Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
            sb.AppendLine("<%@ Register Tagprefix=\"PublishingWebControls\" Namespace=\"Microsoft.SharePoint.Publishing.WebControls\" Assembly=\"Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
            sb.AppendLine("<%@ Register Tagprefix=\"PublishingNavigation\" Namespace=\"Microsoft.SharePoint.Publishing.Navigation\" Assembly=\"Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
            sb.AppendLine("<%@ Import Namespace=\"Microsoft.SharePoint\" %>");
            sb.AppendLine("<asp:Content ID=\"PlaceHolderPageTitle\" ContentPlaceHolderId=\"PlaceHolderPageTitle\" runat=\"server\"></asp:Content>");
            sb.AppendLine("<asp:Content ID=\"PlaceHolderPageTitleInTitleArea\" ContentPlaceHolderId=\"PlaceHolderPageTitleInTitleArea\" runat=\"server\"></asp:Content>");
            sb.AppendLine("<asp:Content ID=\"PlaceHolderPageDescription\" ContentPlaceHolderId=\"PlaceHolderPageDescription\" runat=\"server\"></asp:Content>");
            sb.AppendLine("<asp:Content ID=\"PlaceHolderMain\" ContentPlaceHolderId=\"PlaceHolderMain\" runat=\"server\">");
            sb.AppendLine("<div id=\"container_content\">");
            sb.AppendLine("<div class=\"left_content\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:LeftContent\" ID=\"LeftContent\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("<div class=\"container_left\">");
            sb.AppendLine("<div class=\"left_corner_COL\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:LeftCorner\" ID=\"LeftCorner\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"right_corner_COL\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:RightCorner\" ID=\"RightCorner\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"cleaner\"></div>");
            sb.AppendLine("</div></div>");
            sb.AppendLine("<div class=\"right_content\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:RightContent\" ID=\"RightContent\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"cleaner\"></div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</asp:Content>");
            return sb.ToString();
        }

        /// <summary>
        /// Delete a page in root web by file name
        /// </summary>
        /// <param name="web"></param>
        /// <param name="fileName">A file name like Default.aspx</param>
        public static void DeleteWebPage(SPWeb web, string fileName)
        {
            var exists = false;
            try
            {
                var checkFile = web.RootFolder.Files[fileName];
                exists = true;
            }
            catch (Exception)
            {
                exists = false;
            }
            if (exists)
            {
                web.RootFolder.Files.Delete(fileName);
            }
        }
    }
}
