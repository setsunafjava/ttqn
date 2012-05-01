<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.ContactUS" %>
<table cellspacing="0" cellpadding="4" id="FeedBackForm"> 
    <tr><td><span class="note">*</span>Họ tên:</td> <td><asp:TextBox ID="txtName" runat="server" CssClass="text-box"></asp:TextBox></td> </tr> 
    <tr> <td>Địa chỉ:</td> <td><asp:TextBox ID="txtAdd" runat="server" CssClass="text-box"></asp:TextBox></td> </tr> 
    <tr> <td><span class="note">*</span>Email:</td> <td><asp:TextBox ID="txtEmail" runat="server" CssClass="text-box"></asp:TextBox></td> </tr> 
    <tr> <td><span class="note">*</span>Tiêu đề:</td> <td><asp:TextBox ID="txtTitle" runat="server" CssClass="text-box"></asp:TextBox></td> </tr> 
    <tr> <td>Số điện thoại:</td> <td><asp:TextBox ID="txtMobile" runat="server" CssClass="text-box"></asp:TextBox></td> </tr>
    <tr> <td><span class="note">*</span>Nội dung</td> <td class="textarea"><asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="10" CssClass="text-box"></asp:TextBox></td> </tr> 
    <tr> <td></td> <td><asp:Button ID="btnSubmit" runat="server" Text="Gửi" CssClass="submit" OnClick="btnSubmit_OnClick" /></td> </tr> 
</table>
<style type="text/css">
    #FeedBackForm .note {
    color: #CF1210;
    }
    #FeedBackForm .text {
        font-style: italic;
    }
    #FeedBackForm textarea {
        height: 110px;
        width: 431px;
    }
    #feedback-form .title {
        color: #355183;
        font-size: 10pt;
        font-weight: bold;
        margin-bottom: 3px;
        text-transform: uppercase;
    }
    #feedback-form input.text-box {
        width: 230px;
    }
    #feedback-form input.submitButton {
        background: url("../image/bg-bt.jpg") no-repeat scroll 0 0 transparent;
        border: medium none;
        height: 21px;
        width: 72px;
    }
</style>
