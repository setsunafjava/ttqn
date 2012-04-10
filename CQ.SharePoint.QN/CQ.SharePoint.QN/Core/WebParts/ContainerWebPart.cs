using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CQ.SharePoint.QN.Core.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using System.Runtime.InteropServices;

namespace CQ.SharePoint.QN.Core.WebParts
{
    [Guid("71546CC6-4FB4-4abe-A255-D8CC0E55FD83")]
    [ToolboxItem(false)]
    public class ContainerWebPart : WebPart, IPostBackEventHandler, IValidator
    {
        protected Control userControl;
        private bool? renderFolderCreator;
        private string folderName;
        private CreatedModifiedInfo createdModifiedInfo;
        
        /// <summary>
        ///   Override the chrome type to hide the border
        /// </summary>
        public override PartChromeType ChromeType
        {
            get { return PartChromeType.None; }
            set { base.ChromeType = value; }
        }

        public string ErrorMessage { get; set; }

        public bool IsValid { get; set; }

        /// <summary>
        ///   Contains the path to the user control to load
        /// </summary>
        [WebBrowsable]
        [WebDisplayName("User Control Path")]
        [Personalizable(PersonalizationScope.Shared)]
        public string UserControlPath { get; set; }

        /// <summary>
        ///   Load the user control required
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (SPContext.Current.IsDesignTime)
            {
                Controls.Clear();
                Controls.Add(new Literal
                                 {
                                     Text = string.Format("Container WebPart {0}", string.IsNullOrEmpty(UserControlPath) ? "(No User Control Specified)" : UserControlPath)
                                 });
            }
            else
            {
                if (WebPartManager.DisplayMode == WebPartManager.BrowseDisplayMode)
                {
                    if (!MustRenderFolderCreator())
                    {
                        if (string.IsNullOrEmpty(UserControlPath))
                        {
                            Controls.Clear();
                            Controls.Add(new Literal { Text = "(No User Control Specified)" });
                        }
                        else
                        {
                            try
                            {
                                userControl = Page.LoadControl(UserControlPath);
                                Controls.Add(userControl);
                            }
                            catch (Exception ex)
                            {
                                Controls.Clear();
                                Controls.Add(new Literal { Text = string.Format("Container WebPart ({0})", ex.Message) });
                            }        
                        }
                    }
                    else
                    {
                        renderFolderCreator = true;
                        createdModifiedInfo = new CreatedModifiedInfo();
                        Controls.Add(createdModifiedInfo);
                    }
                }
                else
                {
                    Controls.Clear();
                    Controls.Add(new Literal
                                     {
                                         Text = string.Format("Container WebPart {0}", string.IsNullOrEmpty(UserControlPath) ? "(No User Control Specified)" : UserControlPath)
                                     });
                }
            }
        }

        private bool MustRenderFolderCreator()
        {
            if (renderFolderCreator.HasValue)
            {
                return renderFolderCreator.Value;
            }

            if (WebPartManager.DisplayMode != WebPartManager.BrowseDisplayMode)
            {
                renderFolderCreator = false;
                return false;
            }

            var context = SPContext.Current;
            if (context.FormContext.FormMode != SPControlMode.Invalid)
            {
                if (context.FormContext.FormMode == SPControlMode.New)
                {
                    var type = Page.Request.QueryString["Type"];
                    if ((!string.IsNullOrEmpty(type) && type == "1"))
                    {
                        renderFolderCreator = true;
                        return true;
                    }    
                }
                else
                {
                    if (context.ListItem != null && context.List.BaseTemplate == SPListTemplateType.GenericList && context.ListItem.FileSystemObjectType == SPFileSystemObjectType.Folder)
                    {
                        renderFolderCreator = true;
                        return true;
                    }    
                }
            }

            if (!renderFolderCreator.HasValue)
            {
                renderFolderCreator = false;
                return false;
            }

            return renderFolderCreator.Value;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (MustRenderFolderCreator())
            {
                SPContext.Current.FormContext.OnSaveHandler += CustomHandler;
                Page.Validators.Add(this);
            }
        }

        protected void CustomHandler(object sender, EventArgs e)
        {
            SaveFolder();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (renderFolderCreator == true)
            {
                RenderFolderCreator(writer);
            }
            else
            {
                base.Render(writer);    
            }

            // Fix style for webpart
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            writer.Write("$(document).ready(function(){");
            writer.Write("$('div.ms-WPBody').removeClass('ms-WPBody');");
            writer.Write("});");
            writer.RenderEndTag();
        }

        private void RenderFolderCreator(HtmlTextWriter writer)
        {
            var controlMode = SPContext.Current.FormContext.FormMode;

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "part1");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-formtable");
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddStyleAttribute(HtmlTextWriterStyle.MarginTop, "8px");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            // Td
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-formlabel");
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "190px");
            writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-standardheader");
            writer.RenderBeginTag(HtmlTextWriterTag.H3);    

            writer.Write("Name");

            if (controlMode != SPControlMode.Display)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Title, "This is a required field.");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-formvalidation");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(" *");
                writer.RenderEndTag(); // span    
            }

            writer.RenderEndTag(); // h3
            writer.RenderEndTag(); // td

            // Td
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-formbody");
            writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            SPListItem folder = null;
            if (controlMode != SPControlMode.New)
            {
                folder = SPContext.Current.List.GetItemById(Convert.ToInt32(Page.Request.QueryString["ID"], CultureInfo.InvariantCulture));    
            }

            if (controlMode == SPControlMode.Display)
            {
                var viewUrl = Page.Request.QueryString["Source"];
                if (string.IsNullOrEmpty(viewUrl))
                {
                    viewUrl = SPContext.Current.List.DefaultViewUrl;
                }

                var urlBuilder = new UrlBuilder(viewUrl);
                urlBuilder.ClearQueryString();
                urlBuilder.AddQueryString("RootFolder", folder.Folder.ServerRelativeUrl);
                
                writer.AddAttribute(HtmlTextWriterAttribute.Rel, "sp_DialogLinkNavigate");
                writer.AddAttribute(HtmlTextWriterAttribute.Href, urlBuilder.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(SPEncode.HtmlEncode(folder.Name));
                writer.RenderEndTag(); // a
            }
            else
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Span);

                if (!Page.IsPostBack && SPContext.Current.FormContext.FormMode == SPControlMode.Edit)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, folder.Name);
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, folderName);
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Title, "Name");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-long");
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + "$onetidIOFile");
                writer.RenderBeginTag(HtmlTextWriterTag.Input);

                if (!IsValid)
                {
                    writer.Write("<br/>");
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-formvalidation");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);

                    writer.AddAttribute("role", "alert");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(ErrorMessage);
                    writer.RenderEndTag(); // span
                    writer.RenderEndTag(); // span
                }

                writer.RenderEndTag(); // input

                writer.RenderEndTag(); // span
            }
            
            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr

            writer.RenderEndTag(); // table

            // Table
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-formline");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<img width=\"1\" height=\"1\" alt=\"\" src=\"/_layouts/images/blank.gif\" complete=\"complete\"/>");
            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // table

            // Table
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingTop, "7px");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            // Table
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-formtoolbar");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "2");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            // Td
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            if (controlMode == SPControlMode.New)
            {
                writer.Write("&nbsp;");
            }
            else
            {
                createdModifiedInfo.ControlMode = controlMode;
                createdModifiedInfo.RenderControl(writer);
            }
            writer.RenderEndTag(); // td

            // Td
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "99%");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-toolbar");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<img width=\"1\" height=\"18\" alt=\"\" src=\"/_layouts/images/blank.gif\" complete=\"complete\"/>");
            writer.RenderEndTag(); // td

            if (controlMode == SPControlMode.Display)
            {
                // Td
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-toolbar");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                RenderButton(writer, "Close", "C", "SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.cancel, null);");
                writer.RenderEndTag(); // td
            }
            else
            {
                // Td
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-toolbar");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                RenderButton(writer, "Save", "O", Page.ClientScript.GetPostBackEventReference(this, "ADD_OR_EDIT_FOLDER"));
                writer.RenderEndTag(); // td

                // Td
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-separator");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("&nbsp;");
                writer.RenderEndTag(); // td

                // Td
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-toolbar");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                RenderButton(writer, "Cancel", "C", "SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.cancel, null);");
                writer.RenderEndTag(); // td
            }

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // table

            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // table

            writer.RenderEndTag(); // div

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            writer.Write("SetUploadPageTitle();");
            writer.RenderEndTag(); // script
        }

        private static void RenderButton(HtmlTextWriter writer, string text, string accessKey, string script)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "right");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-ButtonHeightWidth");
            writer.AddAttribute(HtmlTextWriterAttribute.Accesskey,  accessKey);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, text);
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, script);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input

            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // table
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument == "ADD_OR_EDIT_FOLDER")
            {
                Validate();
                if (IsValid)
                {
                    SaveFolder();
                    Page.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
                    Page.Response.Flush();
                    Page.Response.End();
                }
            }
        }

        private void SaveFolder()
        {
            folderName = Page.Request.Form[UniqueID + "$onetidIOFile"].Trim();
            
            var contentTypeId = Page.Request.QueryString["ContentTypeId"];
            if (string.IsNullOrEmpty(contentTypeId))
            {
                // Create new folder
                var rootFolder = Page.Request.QueryString["RootFolder"];
                var folder = SPContext.Current.List.Items.Add(rootFolder, SPFileSystemObjectType.Folder, folderName);
                folder[SPBuiltInFieldId.Title] = folderName;
                folder.Update();    
            }
            else
            {
                // Edit exist folder
                var folder = SPContext.Current.List.GetItemById(Convert.ToInt32(Page.Request.QueryString["ID"], CultureInfo.InvariantCulture));
                folder[SPBuiltInFieldId.Title] = folderName;
                folder[SPBuiltInFieldId.FileLeafRef] = folderName;
                folder.Update();
            }
        }

        public void Validate()
        {
            IsValid = true;
            if (MustRenderFolderCreator())
            {
                folderName = Page.Request.Form[UniqueID + "$onetidIOFile"].Trim();
                IsValid = !string.IsNullOrEmpty(folderName);
                if (!IsValid)
                {
                    ErrorMessage = SPResource.GetString("MissingRequiredField", new object[0]);
                }
            }
        }
    }
}