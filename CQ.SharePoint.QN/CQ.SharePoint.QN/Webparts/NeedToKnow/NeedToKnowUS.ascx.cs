using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class NeedToKnowUS : UserControl
    {
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Sonla.xml", "Sonla");
                    //SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Viettri.xml", "Viettri");
                    //SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Haiphong.xml", "Haiphong");
                    //SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Hanoi.xml", "Hanoi");
                    //SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Vinh.xml", "Vinh");
                    //SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Danang.xml", "Danang");
                    //SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Nhatrang.xml", "Nhatrang");
                    //SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Pleicu.xml", "Pleicu");
                    //SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/HCM.xml", "HCM");
                }
                catch (Exception)
                {
                    
                }
            }
        }

        private void SaveFileToDocLib(string url, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)
                        WebRequest.Create(url);

            // execute the request
            HttpWebResponse response = (HttpWebResponse)
            request.GetResponse();

            // we will read data via the response stream
            Stream ReceiveStream = response.GetResponseStream();

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            using (MemoryStream stream = new MemoryStream())
                            {
                                // Create a 4K buffer to chunk the file

                                byte[] MyBuffer = new byte[4096];

                                int BytesRead;

                                // Read the chunk of the web response into the buffer

                                while (0 < (BytesRead = ReceiveStream.Read(MyBuffer, 0, MyBuffer.Length)))
                                {

                                    // Write the chunk from the buffer to the file

                                    stream.Write(MyBuffer, 0, BytesRead);

                                }
                                web.AllowUnsafeUpdates = true;
                                SPFile file =
                                    web.Files.Add(string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", web.Url,
                                                                ListsName.English.CQQNResources, fileName + ".xml"),
                                                  stream, true, "Get latest file", false);
                                file.Item[FieldsName.CQQNResources.English.ResourceType] = FieldsName.CQQNResources.FieldValuesDefault.Type.XML;
                                file.Item[SPBuiltInFieldId.FileLeafRef] = fileName;
                                web.AllowUnsafeUpdates = true;
                                file.Item.Update();
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }
            });
        }
    }
}
