<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NextNewsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.NextNewsUS" %>
<div class="select_date">    
    <table>
        <tr>
            <td>
                <a href="#" style="color: Red; font-weight: bold">Xem tin tiếp theo...</a>
            </td>
            <td>
                Ngày<asp:DropDownList ID="ddlDays" runat="server" Width="40px">
                </asp:DropDownList>
            </td>
            <td>
                Tháng<asp:DropDownList ID="ddlMonths" runat="server" Width="40px">
                </asp:DropDownList>
            </td>
            <td>
                Năm<asp:DropDownList ID="ddlYears" runat="server" Width="60px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
