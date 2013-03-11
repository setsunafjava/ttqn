<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllDocumentsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.AllDocumentsUS" %>
<table>
    <tr>
        <td>
            Lựa chọn chuyên đề
        </td>
        <td>
            <asp:DropDownList ID="ddlChuyenDe" runat="server">
            </asp:DropDownList>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Loại văn bản:
        </td>
        <td>
            <asp:DropDownList ID="ddlLoaiVanBan" runat="server">
            </asp:DropDownList>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Loại văn bản:
        </td>
        <td>
            <asp:TextBox ID="txtTrichYeu" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Năm ban hành:
        </td>
        <td>
            <asp:DropDownList ID="ddlNamBanHanh" runat="server">
            </asp:DropDownList>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Ký hiệu văn bản:
        </td>
        <td>
            <asp:TextBox ID="txtKyHieuVanBan" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Từ ngày:
        </td>
        <td>
            <asp:TextBox ID="txtTuNgay" runat="server"></asp:TextBox>(dd-mm-yyyy)
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Đến ngày:
        </td>
        <td>
            <asp:TextBox ID="txtDenNgay" runat="server"></asp:TextBox>(dd-mm-yyyy)
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Button ID="btnSearch" runat="server" Text="Tìm Kiếm" />
            &nbsp;&nbsp;
            <asp:Button ID="btnRefresh" runat="server" Text="Làm Lại" />
        </td>
    </tr>
</table>
<form id="form1">
<div>
    <asp:DataList ID="dlPaginationSample" runat="server">
        <ItemTemplate>
            <%# Eval("Title")%>            |            <%# Eval("Created")%>
            <br />
        </ItemTemplate>
    </asp:DataList>
</div>
<br />
<asp:Panel runat="server" ID="pnlPager" CssClass="pagination">
</asp:Panel>
</form>
