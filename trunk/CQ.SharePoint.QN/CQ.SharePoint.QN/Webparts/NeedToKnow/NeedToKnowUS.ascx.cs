using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;
using Microsoft.SharePoint.Utilities;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class NeedToKnowUS : UserControl
    {
        private delegate void MethodInvoker(SPWeb web);
        public NeedToKnow ParentWP;
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
                    if (string.IsNullOrEmpty(ParentWP.KQXSUrl))
                    {
                        lbKQXS.OnClientClick = "javascript:location.href='http://kqxs.vn';return false;";
                    }
                    else
                    {
                        lbKQXS.OnClientClick = "javascript:location.href='" + ParentWP.KQXSUrl + "';return false;";
                    }
                    if (string.IsNullOrEmpty(ParentWP.BDUrl))
                    {
                        lbBD.OnClientClick = "javascript:location.href='http://bongdaso.vn/livescore.aspx';return false;";
                    }
                    else
                    {
                        lbBD.OnClientClick = "javascript:location.href='" + ParentWP.BDUrl + "';return false;";
                    }
                }
                catch (Exception)
                {

                }

                try
                {
                    var docLib = Utilities.GetDocListFromUrl(SPContext.Current.Web, ListsName.English.CQQNResources);
                    string CAML = @"<Where>
                                        <And>
                                            <Eq>
                                                <FieldRef Name='{0}' />
                                                <Value Type='Choice'>{1}</Value>
                                            </Eq>
                                            <And>
                                                <Eq>
                                                    <FieldRef Name='{2}' />
                                                    <Value Type='Text'>{3}</Value>
                                                </Eq>
                                                <Leq>
                                                    <FieldRef Name='{4}' />
                                                    <Value Type='DateTime' IncludeTimeValue ='True'>{5}</Value>
                                                </Leq>
                                            </And>
                                        </And>
                                    </Where>";

                    SPQuery query = new SPQuery()
                    {
                        Query = string.Format(CultureInfo.InvariantCulture, CAML, FieldsName.CQQNResources.English.ResourceType, "XML", 
                            FieldsName.CQQNResources.English.FileLeafRef, "Sonla.xml",Constants.Modified,
                            SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now.AddHours(-2))),
                        RowLimit = 1
                    };
                    SPListItemCollection items = docLib.GetItems(query);
                    if (items != null && items.Count > 0)
                    {
                        MethodInvoker runSaveFileToDocLib = new MethodInvoker(SaveFileToDocLibAll);
                        runSaveFileToDocLib.BeginInvoke(SPContext.Current.Web, null, null);
                        //SaveFileToDocLib(SPContext.Current.Web, "http://vnexpress.net/ListFile/Weather/Sonla.xml", "Sonla");
                        //SaveFileToDocLib(SPContext.Current.Web, "http://vnexpress.net/ListFile/Weather/Viettri.xml", "Viettri");
                        //SaveFileToDocLib(SPContext.Current.Web, "http://vnexpress.net/ListFile/Weather/Haiphong.xml", "Haiphong");
                        //SaveFileToDocLib(SPContext.Current.Web, "http://vnexpress.net/ListFile/Weather/Hanoi.xml", "Hanoi");
                        //SaveFileToDocLib(SPContext.Current.Web, "http://vnexpress.net/ListFile/Weather/Vinh.xml", "Vinh");
                        //SaveFileToDocLib(SPContext.Current.Web, "http://vnexpress.net/ListFile/Weather/Danang.xml", "Danang");
                        //SaveFileToDocLib(SPContext.Current.Web, "http://vnexpress.net/ListFile/Weather/Nhatrang.xml", "Nhatrang");
                        //SaveFileToDocLib(SPContext.Current.Web, "http://vnexpress.net/ListFile/Weather/Pleicu.xml", "Pleicu");
                        //SaveFileToDocLib(SPContext.Current.Web, "http://vnexpress.net/ListFile/Weather/HCM.xml", "HCM");
                        //SaveFileToDocLib(SPContext.Current.Web, "http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx", "giavang");
                    }
                }
                catch (Exception)
                {

                }

                try
                {
                    //string Url = "http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx";
                    string Url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", SPContext.Current.Web.Url,
                                                                ListsName.English.CQQNResources, "giavang.xml");
                    DataSet ds = new DataSet();
					string currencyString = "USD SGD JPY EUR RUB";
                    ds.ReadXml(Url);

                    if (ds.Tables.Count > 0)
                    {

                        DataTable dt = new DataTable();
                        DataTable result = new DataTable("Result");
                        result.Columns.Add(new DataColumn("CurrencyCode"));
                        result.Columns.Add(new DataColumn("Transfer"));
                        result.Columns.Add(new DataColumn("Sell"));
                        DataRow row;
                        dt = ds.Tables["Exrate"];

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (currencyString.Contains(Convert.ToString(dr["CurrencyCode"])))
                            {
                                row = result.NewRow();
                                row["CurrencyCode"] = Convert.ToString(dr["CurrencyCode"]);
                                row["Transfer"] = Convert.ToString(dr["Transfer"]);
                                row["Sell"] = Convert.ToString(dr["Sell"]);

                                result.Rows.Add(row);

                            }
                        }

                        rptTiGia.DataSource = result;
                        rptTiGia.DataBind();
                    }

                }
                catch (Exception)
                {

                }
            }
        }

        private void SaveFileToDocLibAll(SPWeb currentWeb) {
            SaveFileToDocLib(currentWeb, "http://vnexpress.net/ListFile/Weather/Sonla.xml", "Sonla");
            SaveFileToDocLib(currentWeb, "http://vnexpress.net/ListFile/Weather/Viettri.xml", "Viettri");
            SaveFileToDocLib(currentWeb, "http://vnexpress.net/ListFile/Weather/Haiphong.xml", "Haiphong");
            SaveFileToDocLib(currentWeb, "http://vnexpress.net/ListFile/Weather/Hanoi.xml", "Hanoi");
            SaveFileToDocLib(currentWeb, "http://vnexpress.net/ListFile/Weather/Vinh.xml", "Vinh");
            SaveFileToDocLib(currentWeb, "http://vnexpress.net/ListFile/Weather/Danang.xml", "Danang");
            SaveFileToDocLib(currentWeb, "http://vnexpress.net/ListFile/Weather/Nhatrang.xml", "Nhatrang");
            SaveFileToDocLib(currentWeb, "http://vnexpress.net/ListFile/Weather/Pleicu.xml", "Pleicu");
            SaveFileToDocLib(currentWeb, "http://vnexpress.net/ListFile/Weather/HCM.xml", "HCM");
            SaveFileToDocLib(currentWeb, "http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx", "giavang");
        }

        private void SaveFileToDocLib(SPWeb currentWeb, string url, string fileName)
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
                using (var site = new SPSite(currentWeb.Site.ID))
                {
                    using (var web = site.OpenWeb(currentWeb.ID))
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

