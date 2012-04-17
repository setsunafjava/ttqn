﻿using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CQ.SharePoint.QN.Common;

namespace CQ.SharePoint.QN.Webparts
{
    /// <summary>
    /// QNHeaderUS
    /// </summary>
    public partial class QNadvUS : UserControl
    {
        public QNadv ParentWP;
        protected string AdvStyle;
        /// <summary>
        /// Page on Load
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ParentWP.ImageWidth))
            {
                AdvStyle += "width:" + ParentWP.ImageWidth + ";";
            }
            if (!string.IsNullOrEmpty(ParentWP.ImageHeight))
            {
                AdvStyle += "height:" + ParentWP.ImageHeight + ";";
            }
        }
    }
}
