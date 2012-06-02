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
        <div class="time_up">
            <asp:Label ID="lblCurrentDate" runat="server" />
            <span><a href="#">
                <img src="images/icon_adobereader.jpg" /></a> | <a href="#">
                    <img src="images/icon_print.jpg" /></a> | <a href="#">
                        <img src="images/ico_email.jpg" /></a></span></div>
    </div>
    <p>
        <asp:Literal ID="ltrNewsContent" runat="server"></asp:Literal>
    </p>
    <br />
    <asp:Panel ID="pnlAttachment" runat="server">
        
        <asp:Label ID="lblAttachFiles" runat="server"></asp:Label>
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
