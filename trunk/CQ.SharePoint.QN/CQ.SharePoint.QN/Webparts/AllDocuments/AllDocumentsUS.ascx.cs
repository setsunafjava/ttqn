using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class AllDocumentsUS : UserControl
    {
        public AllDocuments WebpartParent;
        public string NewsUrl = string.Empty;

        /// <summary/>
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindData();
                }
                else
                {
                    BuildPagination();
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToUls(ex);
            }

        }

        private void bindDataWithPaging(Control bindControl, DataTable table)
        {
            if (table != null && table.Rows.Count > 0)
            {
                DataView dv = table.DefaultView;

                PagedDataSource dsP = new PagedDataSource();
                dsP.AllowPaging = true;
                dsP.DataSource = dv;
                dsP.CurrentPageIndex = CurrentPageIndex;
                dsP.PageSize = PageSize;

                //Binding data to the controls
                if (bindControl is DataList)
                {
                    ((DataList)bindControl).DataSource = dsP;
                    ((DataList)bindControl).DataBind();
                }
                else if (bindControl is Repeater)
                {
                    ((Repeater)bindControl).DataSource = dsP;
                    ((Repeater)bindControl).DataBind();
                }

                //saving the total page count in Viewstate for later use
                PageCount = dsP.PageCount;

                //create the linkbuttons for pagination
                BuildPagination();
            }
        }

        private int CurrentPageIndex //to store the current page index
        {
            get { return ViewState["CurrentPageIndex"] == null ? 0 : int.Parse(ViewState["CurrentPageIndex"].ToString()); }
            set { ViewState["CurrentPageIndex"] = value; }
        }
        private int PageCount  //total number of pages needed to display the data
        {
            get { return ViewState["PageCount"] == null ? 0 : int.Parse(ViewState["PageCount"].ToString()); }
            set { ViewState["PageCount"] = value; }
        }

        private int PageSize //total rows per page
        {
            get { return 4; }
        }

        private int ButtonsCount //how many total linkbuttons shld be shown
        {
            get { return 10; }
        }

        private string FirstPageText
        {
            get { return "&lt;&lt;"; }
        }
        private string LastPageText
        {
            get { return "&gt;&gt;"; }
        }

        protected void BuildPagination()
        {
            pnlPager.Controls.Clear(); //

            if (PageCount <= 1) return; // at least two pages should be there to show the pagination

            //finding the first linkbutton to be shown in the current display
            int start = CurrentPageIndex - (CurrentPageIndex % ButtonsCount);

            //finding the last linkbutton to be shown in the current display
            int end = CurrentPageIndex + (ButtonsCount - (CurrentPageIndex % ButtonsCount));

            //if the start button is more than the number of buttons. If the start button is 11 we have to show the <<First link
            if (start > ButtonsCount - 1)
            {
                pnlPager.Controls.Add(createButton(FirstPageText, 0));
                pnlPager.Controls.Add(createButton("..", start - 1));
            }

            int i = 0, j = 0;

            for (i = start; i < end; i++)
            {
                LinkButton lnk;
                if (i < PageCount)
                {
                    if (i == CurrentPageIndex) //if its the current page
                    {
                        Label lbl = new Label();
                        lbl.Text = (i + 1).ToString();
                        pnlPager.Controls.Add(lbl);
                    }
                    else
                    {
                        pnlPager.Controls.Add(createButton((i + 1).ToString(), i));
                    }
                }
                j++;
            }

            //If the total number of pages are greaer than the end page we have to show Last>> link
            if (PageCount > end)
            {
                pnlPager.Controls.Add(createButton("..", i));
                pnlPager.Controls.Add(createButton("&gt;&gt;", PageCount - 1));
            }


        }

        private LinkButton createButton(string title, int index)
        {
            LinkButton lnk = new LinkButton();
            lnk.ID = index.ToString();
            lnk.Text = title;
            lnk.CommandArgument = index.ToString();
            lnk.Click += new EventHandler(lnkPager_Click);
            return lnk;
        }

        private void bindData()
        {
            bindDataWithPaging(dlPaginationSample, GetAllData());
        }

        protected void lnkPager_Click(object sender, EventArgs e) //Page index changed function
        {
            LinkButton lnk = (LinkButton)sender;
            CurrentPageIndex = int.Parse(lnk.CommandArgument);

            bindData();
        }

        protected DataTable GetAllData()
        {
            return Utilities.GetListFromUrl(ListsName.English.AllDocuments);
        }
    }
}
