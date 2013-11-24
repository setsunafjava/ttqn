<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooterUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.FooterUS" %>
<div class="bottom_menu">
    <ul>
        <asp:Repeater ID="rptMenu" runat="server">
            <ItemTemplate>
                <li><a runat="server" id="aLink" style="color: White"></a></li>
            </ItemTemplate>
        </asp:Repeater>
		<li><a style="color: White" href='/_layouts/Authenticate.aspx?Source=%2F%5Flayouts%2Fviewlsts%2Easpx'>Admin</a></li>
    </ul>
</div>
<div class="info_footer">
    <div class="info_website">
        <%=WebsiteInfo%>
    </div>
    <div class="statistic">
        <div style='display:none'>
            <%= ParentWP.StatisticTitle %>
            <div style="background-image: url('/QNResources/statistic.jpg'); width: 118px; height: 35px;">
                <div style="color: #ffffff; text-align: center; position: relative; top: 9px;">
                    <%=HitCount%></div>
            </div>
        </div>
        <div class="design_by">
            <%= ParentWP.DesignByTitle %></div>
    </div>
    <div class="cleaner">
    </div>
</div>
