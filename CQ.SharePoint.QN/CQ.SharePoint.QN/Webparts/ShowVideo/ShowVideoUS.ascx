<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowVideoUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.ShowVideoUS" %>
<div class="cleaner"></div>
<div>
    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
    <asp:Button ID="btnCreate" runat="server" Text="Tạo page" OnClick="btnCreate_OnClick" />
    <asp:Button ID="btnCopyResource" runat="server" Text="Copy Resource" OnClick="btnCopyResource_OnClick" />
    <asp:Button ID="btnCopyCat" runat="server" Text="Copy News" OnClick="btnCopyCat_OnClick" />
</div>
<div class="cleaner"></div>
