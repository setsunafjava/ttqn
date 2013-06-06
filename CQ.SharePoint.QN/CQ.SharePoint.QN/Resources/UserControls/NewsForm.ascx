<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsForm.ascx.cs" Inherits="CQ.SharePoint.QN.UserControls.NewsForm" %>
<%@ Register Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.WebControls" TagPrefix="SharePoint" %>
<%@ Register Tagprefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div id="part1">
    <table width="100%" cellspacing="0" cellpadding="2" border="0" class="ms-formtoolbar">
        <tr>
            <td width="99%" nowrap="" class="ms-toolbar">
                <img width="1" height="18" alt="" src="/_layouts/images/blank.gif">
            </td>
            <td nowrap="true" class="ms-toolbar">
                <table width="100%" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <td width="100%" nowrap="" align="right">
                                <SharePoint:savebutton runat="server" id="saveButton" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td class="ms-separator">
                &nbsp;
            </td>
            <td nowrap="true" class="ms-toolbar">
                <table width="100%" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <td width="100%" nowrap="" align="right">
                                <SharePoint:gobackbutton id="goBackButton" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    
    <SharePoint:FormToolBar runat="server" />

    
    <table width="100%" cellspacing="0" cellpadding="0" border="0" style="margin-top: 8px;"
        class="ms-formtable">
        <tr>
            <td class="ms-formlabel" style="width:190px">
                <SharePoint:fieldlabel id="lblTitle" runat="server" fieldname="Title" />
            </td>
            <td class="ms-formbody">
                <SharePoint:textfield runat="server" id="txtTitle" fieldname="Title" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblCategoryName" runat="server" fieldname="CategoryName" />
            </td>
            <td class="ms-formbody">
                <asp:ListBox runat="server" ID="ddlCat" SelectionMode="Multiple" Width="300" Height="200"></asp:ListBox>
                <asp:Label runat="server" ID="lblCat"></asp:Label>
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblShortContent" runat="server" fieldname="ShortContent" />
            </td>
            <td class="ms-formbody">
                <SharePoint:RichTextField runat="server" id="txtShortContent" fieldname="ShortContent" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblPublishingPageImage" runat="server" fieldname="PublishingPageImage" />
            </td>
            <td class="ms-formbody">
                <PublishingWebControls:RichImageField ID="txtRichImageField" FieldName="PublishingPageImage" runat="server"></PublishingWebControls:RichImageField>
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblPublishingPageContent" runat="server" fieldname="PublishingPageContent" />
            </td>
            <td class="ms-formbody">
                <PublishingWebControls:RichHtmlField ID="txtRichHtmlField" FieldName="PublishingPageContent" runat="server"></PublishingWebControls:RichHtmlField>
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblFocusNews" runat="server" fieldname="FocusNews" />
            </td>
            <td class="ms-formbody">
                <SharePoint:BooleanField runat="server" id="txtFocusNews" fieldname="FocusNews" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblShowInHomePage" runat="server" fieldname="ShowInHomePage" />
            </td>
            <td class="ms-formbody">
                <SharePoint:BooleanField runat="server" id="txtShowInHomePage" fieldname="ShowInHomePage" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblStatus" runat="server" fieldname="Status" />
            </td>
            <td class="ms-formbody">
                <SharePoint:BooleanField runat="server" id="txtStatus" fieldname="Status" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblSource" runat="server" fieldname="Source" />
            </td>
            <td class="ms-formbody">
                <SharePoint:TextField runat="server" id="txtSource" fieldname="Source" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblArticleByLine" runat="server" fieldname="ArticleByLine" />
            </td>
            <td class="ms-formbody">
                <SharePoint:TextField runat="server" id="txtArticleByLine" fieldname="ArticleByLine" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblArticleStartDates" runat="server" fieldname="ArticleStartDates" />
            </td>
            <td class="ms-formbody">
                <SharePoint:DateTimeField runat="server" id="txtArticleStartDates" fieldname="ArticleStartDates" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblCommentForImage" runat="server" fieldname="CommentForImage" />
            </td>
            <td class="ms-formbody">
                <SharePoint:TextField runat="server" id="txtCommentForImage" fieldname="CommentForImage" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblShowOnCategory" runat="server" fieldname="ShowOnCategory" />
            </td>
            <td class="ms-formbody">
                <SharePoint:BooleanField runat="server" id="txtShowOnCategory" fieldname="ShowOnCategory" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblLatestNewsOnHomePage" runat="server" fieldname="LatestNewsOnHomePage" />
            </td>
            <td class="ms-formbody">
                <SharePoint:BooleanField runat="server" id="txtLatestNewsOnHomePage" fieldname="LatestNewsOnHomePage" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblLatestNewsOnCategory" runat="server" fieldname="LatestNewsOnCategory" />
            </td>
            <td class="ms-formbody">
                <SharePoint:BooleanField runat="server" id="txtLatestNewsOnCategory" fieldname="LatestNewsOnCategory" />
             </td>
        </tr>
        <tr>
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblNewsOfCategory" runat="server" fieldname="NewsOfCategory" />
            </td>
            <td class="ms-formbody">
                <SharePoint:BooleanField runat="server" id="txtNewsOfCategory" fieldname="NewsOfCategory" />
             </td>
        </tr>
    </table>
    
    
    
    
    
    
    <table width="100%" cellspacing="0" cellpadding="2" border="0" class="ms-formtoolbar">
        <tr>
            <td width="99%" nowrap="" class="ms-toolbar">
                <img width="1" height="18" alt="" src="/_layouts/images/blank.gif">
            </td>
            <td nowrap="true" class="ms-toolbar">
                <table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="100%" nowrap="" align="right">
                                <SharePoint:savebutton runat="server" id="saveButton1" />
                            </td>
                        </tr>
                </table>
            </td>
            <td class="ms-separator">
                &nbsp;
            </td>
            <td nowrap="true" class="ms-toolbar">
                <table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="100%" nowrap="" align="right">
                                <SharePoint:gobackbutton id="goBackButton1" runat="server" />
                            </td>
                        </tr>
                </table>
            </td>
        </tr>
    </table>
</div>