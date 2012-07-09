<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourInfoUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.TourInfoUS" %>
<div class="pos_MOD">
    <div class="bg_title_mod">
        <div class="info_travel">
            <%= WebpartParent.TourInfoTitle%></div>
    </div>
    <div class="inner_pos_Mod">
        <div class="inner_infoTravel">
            <asp:Repeater ID="rptTourInfo" runat="server" OnItemDataBound="rptTourInfo_OnItemDataBound">
                <HeaderTemplate><ul></HeaderTemplate>
                <ItemTemplate><li><a href='<%= CategoryUrl%><%#Eval("ID") %>'><%#Eval("Title") %></a></li></ItemTemplate>
                <FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
