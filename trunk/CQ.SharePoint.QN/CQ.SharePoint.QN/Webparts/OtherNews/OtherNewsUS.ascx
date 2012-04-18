<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OtherNewsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.OtherNewsUS" %>
<div class="other_News">
    <div class="top_F">
        Các tin khác
    </div>
    <div class="inner_F_otherNew">        
        <asp:Repeater ID="rptOtherNews" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                    <%#Eval("ShortContent")%></a><span class="time_update">(Ngày
                        <%#Eval("Modified")%>)</span></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>        
    </div>
    <div>        
        <table>
            <tr>
                <td><a href="#" style=" color:Red; font-weight:bold">Xem tin tiếp theo...</a></td>
                <td>
                    <asp:DropDownList ID="ddlDays" runat="server" Width="40px"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMonths" runat="server" Width="40px"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlYears" runat="server" Width="60px"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
</div>
