<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NextNewsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.NextNewsUS" %>
<style type="text/css">
    .linkcss
    {
        color: Red;
        font-weight: bold;
    }
</style>
<asp:Panel ID="pnlNextNews" runat="server">
    <div class="select_date">
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="lnkNextNews" runat="server" CssClass="linkcss" Text="Xem tin tiếp theo..."
                        OnClick="NextNewsClick"></asp:LinkButton>
                </td>
                <td>
                    Ngày
                    <asp:DropDownList ID="ddlDays" runat="server" Width="40px">
                    </asp:DropDownList>
                </td>
                <td>
                    Tháng
                    <asp:DropDownList ID="ddlMonths" runat="server" Width="40px">
                    </asp:DropDownList>
                </td>
                <td>
                    Năm
                    <asp:DropDownList ID="ddlYears" runat="server" Width="60px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
