<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsDetailUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.NewsDetailUS" %>
<div class="detail_artical">
    <div class="path">
        <div class="arr_B">
            <asp:Label ID="lblBreadCrum" runat="server"></asp:Label>
        </div>
    </div>
    
    <asp:Panel ID="pnlNewsDetail" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <table>
                            <tr>
                                <td>
                                    <a>
                                        <img src="images/icon_date.jpg" /></a>
                                </td>
                                <td>
                                    <asp:Label ID="lblCreatedDate" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <h2>
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
            <%--<asp:Label ID="lblCreatedDate" runat="server"></asp:Label>--%>
        </h2>
        <br />
        <p>
            <b>
                <asp:Literal ID="ltrShortDescription" runat="server"></asp:Literal>
            </b>
        </p>
        <br />
        <p>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Literal ID="ltrNewsContent" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="right" style=" font-weight:bold">
                        <asp:Label ID="lblAuthor" runat="server"></asp:Label><br />
                        <asp:Label ID="lblSource" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </p>
        <br />
        <asp:Panel ID="pnlAttachment" runat="server">
            <asp:Label ID="lblAttachFiles" Text="Tài liệu kèm theo:" runat="server"></asp:Label>
            <asp:Repeater ID="rptAttachment" runat="server">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:HyperLink ID="hplAttachment" runat="server" NavigateUrl='<%#Eval("value") %>'
                                Text='<%#Eval("key") %>'></asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlChuyenDe" runat="server">
        <div class="inner_list_company_adv">
            <asp:Repeater ID="rptChuyenDe" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                        <%#Eval("Title") %></a></li></ItemTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>
</div>
