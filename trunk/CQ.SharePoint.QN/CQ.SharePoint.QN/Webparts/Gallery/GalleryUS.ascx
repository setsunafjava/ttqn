<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalleryUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.GalleryUS" %>
<asp:Repeater ID="rptCat" runat="server" OnItemDataBound="rptCat_OnItemDataBound">
    <ItemTemplate>
        <div class="inner_content_subpage">
            <div class="cont_artical">
                <div class="name_artical">
                    <a runat="server" id="aLink"><%#Eval("Title") %></a> <span class="time_update">
                        (ngày <%#Eval("ArticleStartDates")%>)</span></div>
                <div class="interpre">
                    <div class="img_thumb">
                        <img runat="server" id="imgLink" title='<%#Eval("Title") %>' /></div>
                    <div class="short_content">
                        <%#Eval("Description")%></div>
                    <div class="cleaner">
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:Repeater ID="rptAlbum" runat="server" OnItemDataBound="rptAlbum_OnItemDataBound">
    <ItemTemplate>
        <div class="inner_content_subpage">
            <div class="cont_artical">
                <div class="name_artical">
                    <a runat="server" id="aLink"><%#Eval("Title") %></a> <span class="time_update">
                        (ngày <%#Eval("ArticleStartDates")%>)</span></div>
                <div class="interpre">
                    <div class="img_thumb">
                        <img runat="server" id="imgLink" title='<%#Eval("Title") %>' /></div>
                    <div class="short_content">
                        <%#Eval("Description")%></div>
                    <div class="cleaner">
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
