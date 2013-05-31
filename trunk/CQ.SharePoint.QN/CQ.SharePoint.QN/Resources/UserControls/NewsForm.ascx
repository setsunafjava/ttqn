<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsForm.ascx.cs" Inherits="CQ.SharePoint.QN.UserControls.NewsForm" %>
<%@ Register Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.WebControls" TagPrefix="SharePoint" %>
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
            <td class="ms-formlabel">
                <SharePoint:fieldlabel id="lblTitle" runat="server" fieldname="Title" />
            </td>
            <td class="ms-formbody">
                <SharePoint:textfield runat="server" id="txtTitle" fieldname="Title" />
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
                    <tbody>
                        <tr>
                            <td width="100%" nowrap="" align="right">
                                <SharePoint:savebutton runat="server" id="saveButton1" />
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
                                <SharePoint:gobackbutton id="goBackButton1" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
</div>