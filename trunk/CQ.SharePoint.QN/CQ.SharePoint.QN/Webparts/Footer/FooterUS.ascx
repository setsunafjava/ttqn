<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooterUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.FooterUS" %>
<div class="bottom_menu">
    <ul>
        <asp:Repeater ID="rptMenu" runat="server">
            <ItemTemplate>
                <li><a style=" color:White" href='<%#Eval("Url") %>'><%#Eval("Title") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
        <li><a style=" color:White" href="/_layouts/Authenticate.aspx">Đăng nhập</a></li>
    </ul>
</div>
<div class="info_footer">
    <div class="info_website">
        <%--<b>TRUNG TÂM THÔNG TIN - VĂN PHÒNG UBND TỈNH QUẢNG NINH</b><br />
        GPXB: số 143/GP-TTĐT của Bộ TT&TT cấp ngày 04/08/2011 - Chiụ trách nhiệm chính:
        Ông Lê Quang Ngọc<br />
        <b>Giám đốc TRUNG TÂM THÔNG TIN</b><br />
        Địa chỉ : Số 219 Đường Nguyễn Văn Cừ - Tp. Hạ Long - Quảng Ninh<br />
        TEL: (033)3836088 - 08033116. FAX: (033)3636622 - Email: qnp@quangninh.gov.vn--%>
        <%=WebsiteInfo%>
    </div>
    <div class="statistic">
        <div>
            Số lượt truy cập:
            <div style="background-image:url('/QNResources/statistic.jpg'); width:118px; height:35px;">
                <div style="color:#ffffff;text-align:center;position:relative;top:9px;"><%=HitCount%></div>
            </div>
        </div>
        <div class="design_by">
            Thiết kế bởi VIETEC</div>
    </div>
    <div class="cleaner">
    </div>
</div>
