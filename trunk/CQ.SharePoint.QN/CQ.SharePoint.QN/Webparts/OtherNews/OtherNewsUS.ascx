<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, 
PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OtherNewsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.OtherNewsUS" %>
<div class="other_News">
    <div class="top_F">
        <%=WebpartParent.OtherNewsTitle %>
        <asp:Label ID="OtherNews" runat="server"></asp:Label>
    </div>
    <div class="inner_F_otherNew">
        <asp:Repeater ID="rptOtherNews" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><a href='<%= NewsUrl%><%#Eval("ID")%>'>
                    <%#Eval("Title")%></a><span style="color: #003399"> (<%=WebpartParent.Day %> <%#Eval("Modified")%>)</span></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Label ID="lblItemsNotFound" runat="server" ForeColor="Red" />
    </div>
</div>
