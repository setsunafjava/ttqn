using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class FocusCompanyUS : UserControl
    {
        public FocusCompany WebpartParent;
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
                DataTable companyList = null;

                Utilities.GetNewsByCatID(Convert.ToString(WebpartParent.CategoryId), ref companyList);
                string imagepath;

                if (companyList.Rows.Count > 0)
                {
                    companyList.Columns.Add("LinkToItem", typeof(String));
                    for (int i = 0; i < companyList.Rows.Count; i++)
                    {
                        imagepath = Convert.ToString(companyList.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage]);
                        if (imagepath.Length > 2)
                            companyList.Rows[i][FieldsName.NewsRecord.English.ThumbnailImage] = imagepath.Trim().Substring(0, imagepath.Length - 2);

                        companyList.Rows[i]["LinkToItem"] = string.Format("{0}/{1}.aspx?NewsId={2}", SPContext.Current.Web.Url, Constants.PageInWeb.DetailNews, companyList.Rows[i][FieldsName.Id]);
                    }
                    rptFocusCompany.DataSource = companyList;
                    rptFocusCompany.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
