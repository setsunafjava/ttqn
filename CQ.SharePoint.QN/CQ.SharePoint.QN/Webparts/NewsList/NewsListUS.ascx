<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsListUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.NewsListUS" %>
<style type="text/css">
    .redtext
    {
        color: Red;
    }
</style>
<h2>
    <asp:Label ID="lblCategoryTitle" runat="server" />
</h2>
<div class="inner_list_company_adv">
    <asp:Panel ID="pnlCategory" runat="server" Visible="false">
        <asp:Repeater ID="rptCaregory" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><a href='<%= NewsUrl1%><%#Eval("ID") %>'>
                    <%#Eval("Title") %></a></li></ItemTemplate>
            <FooterTemplate>
                </ul></FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</div>
<hr />
<asp:Repeater ID="rptListCategory" runat="server" OnItemDataBound="OnItemDataBound_ListCategory">
    <ItemTemplate>
        <div class="inner_content_subpage">
            <div class="cont_artical">
                <div class="name_artical">
                    <a href='<%= NewsUrl%><%#Eval("ID") %>&CategoryId=<%#Eval("CategoryId") %>'>
                        <%#Eval("Title")%></a> <span class="time_update">(<%= ParentWP.Day %>
                            <%#Eval("ArticleStartDates")%>)</span></div>
                <div class="interpre">
                    <%--<div class="img_thumb">
                        <asp:Image ID="imgLogo" runat="server" Width="120px" Height="70px" ImageUrl='<%#Eval("Thumbnail") %>' />
                    </div>--%>
                    <asp:Literal ID="ltrImage" runat="server" Text=""></asp:Literal>
                    <div class="short_content">
                        <%#Eval("ShortContent")%></div>
                    <div class="cleaner">
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<table>
    <tr>
        <td>
            <asp:HyperLink ID="lnkPrev" runat="server" Text="Trước"></asp:HyperLink>
        </td>
        <td>
            <asp:Label ID="lblCurrpage" runat="server"></asp:Label>
        </td>
        <td>
            <asp:HyperLink ID="lnkNext" runat="server" Text="Sau"></asp:HyperLink>
        </td>
    </tr>
</table>
<asp:Label ID="lblItemNotExist" CssClass="redtext" runat="server"></asp:Label>