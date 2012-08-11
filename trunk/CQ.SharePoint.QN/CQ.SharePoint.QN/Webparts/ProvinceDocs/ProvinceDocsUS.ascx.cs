using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class ProvinceDocsUS : UserControl
    {
        public ProvinceDocs WebpartParent;
        public string NewsUrl = string.Empty;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string query = string.Format(@"<Where>
                                                  <And>
                                                     <Neq>
                                                        <FieldRef Name='Status' />
                                                        <Value Type='Boolean'>1</Value>
                                                     </Neq>
                                                     <Leq>
                                                        <FieldRef Name='ArticleStartDate' />
                                                        <Value IncludeTimeValue='FALSE' Type='DateTime'>{0}</Value>
                                                     </Leq>
                                                  </And>
                                               </Where>
                                               <OrderBy>
                                                  <FieldRef Name='ID' Ascending='False' />
                                               </OrderBy>", SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now));
                uint numberOfNews = 5;
                if (!string.IsNullOrEmpty(WebpartParent.NumberOfNews))
                {
                    try
                    {
                        numberOfNews = Convert.ToUInt16(WebpartParent.NumberOfNews);
                    }
                    catch (Exception ex)
                    {
                        numberOfNews = 5;
                    }
                }
                var provinceTable = Utilities.GetNewsRecordItems(query, numberOfNews, ListsName.English.BannerAdvRight);
                var correctTable = Utilities.GetTableWithCorrectUrlHotNews(provinceTable);

                //DataTable newTable = null;
                //newTable = correctTable.Clone();
                //if (correctTable.Rows.Count > numberOfNews)
                //{
                //    for (int i = 0; i < numberOfNews; i++)
                //    {
                //        newTable.ImportRow(correctTable.Rows[i]);
                //    }
                //}
                //else
                //{
                //    for (int i = 0; i < correctTable.Rows.Count; i++)
                //    {
                //        newTable.ImportRow(correctTable.Rows[i]);
                //    }
                //}

                if (correctTable.Rows.Count > 0)
                {
                    rptImages.DataSource = correctTable;
                    rptImages.DataBind();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
