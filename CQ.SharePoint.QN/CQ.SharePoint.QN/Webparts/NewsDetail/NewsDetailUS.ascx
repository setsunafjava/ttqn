<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsDetailUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.NewsDetailUS" %>
<div class="detail_artical">
    <div class="path">
        <div class="arr_B">
            <asp:Label ID="lblBreadCrum" runat="server"></asp:Label>
        </div>
    </div>
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
                                <asp:Label ID="lblCurrentDate" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <a href="#">
                                    <img src="images/icon_adobereader.jpg" /></a>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/icon_print.jpg" /></a>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/ico_email.jpg" /></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <p>
        <asp:Literal ID="ltrNewsContent" runat="server"></asp:Literal>
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
</div>
