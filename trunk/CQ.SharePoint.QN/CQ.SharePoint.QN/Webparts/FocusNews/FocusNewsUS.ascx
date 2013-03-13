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
                    <div class="line_news">
                        <div class="thumb_img">
                            <asp:Literal ID="ltrImage" runat="server" Text=""></asp:Literal></div>
                        <div class="name_news">
                            <a href='<%= NewsUrl%><%#Eval("ID") %>&CategoryId=<%#Eval("CategoryId") %>'>
                                <%#Eval("Title")%></a><br />
                            <span class="datetimeText">
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                                <%#Eval("ArticleStartDates")%></span>
                        </div>
                        <div class="cleaner">
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="read_more">
                <a href='<%= CategoryUrl %>'>&raquo;
                    <%= WebpartParent.ReadMore %></a></div>
        </div>
    </div>
</div>
