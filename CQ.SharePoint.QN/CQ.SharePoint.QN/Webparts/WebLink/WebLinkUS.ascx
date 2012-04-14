<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebLinkUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.WebLinkUS" %>
<div class="pos_MOD">
    <div class="bg_title_mod">
        Liên kết website</div>
    <div class="inner_pos_Mod">
        <div class="link_website">
            <select class="txt_s" style="width: 190px;" onchange='location.href=this.value'>
                <asp:Repeater ID="rptWebLink" runat="server" OnItemDataBound="rptWebLink_OnItemDataBound">
                    <ItemTemplate><option value='<%#Eval("Url") %>'><%#Eval("Title") %></option></ItemTemplate>
                </asp:Repeater>
            </select>
        </div>
    </div>
</div>
