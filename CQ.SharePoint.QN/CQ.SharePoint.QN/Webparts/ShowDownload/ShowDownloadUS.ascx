<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowDownloadUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.ShowDownloadUS" %>
<asp:Repeater ID="rptDownload" runat="server" OnItemDataBound="rptDownload_OnItemDataBound">
    <ItemTemplate>
        <div class="inner_content_subpage">
            <div class="cont_artical">
                <div class="name_artical">
                    <a runat="server" id="aLink"><%#Eval("Title") %></a> <span class="time_update">
                        (ngày <%#Eval("Modified")%>)</span></div>
                <div class="interpre">
                    <div class="short_content">
                        <%#Eval("Description")%></div>
                    <div class="cleaner">
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
