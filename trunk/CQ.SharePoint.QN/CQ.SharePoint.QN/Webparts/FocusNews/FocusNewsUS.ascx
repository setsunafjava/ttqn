<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FocusNewsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.FocusNewsUS" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <%=WebpartParent.FocusNewsTitle %>
            </div>
        </div>
        <div class="content_F_Right">
            <asp:Repeater ID="rptFocusNews" runat="server" OnItemDataBound="FocusNews_OnItemDataBound">
                <ItemTemplate>
                    <asp:Literal ID="ltrImage" runat="server" Text=""></asp:Literal>
                </ItemTemplate>
            </asp:Repeater>
            <div class="read_more">
                <a href='<%= CategoryUrl %>'>&raquo;
                    <%= WebpartParent.ReadMore %></a></div>
        </div>
    </div>
</div>
