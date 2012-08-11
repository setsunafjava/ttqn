<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvinceDocsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.ProvinceDocsUS" %>
<div class="Notice">
    <asp:Repeater ID="rptImages" runat="server">
        <ItemTemplate>
            <a href='<%#Eval("LinkAdv")%>'>
                <asp:Image style="width: 305px; height:54px;" ID="img" runat="server" ImageUrl='<%#Eval("Thumbnail")%>' /></a>
        </ItemTemplate>
    </asp:Repeater>
</div>
