<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassiFiedsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.ClassiFiedsUS" %>
<div class="pos_MOD">
    <div class="bg_title_mod">
        <%=WebpartParent.CategoryType%>
    </div>
    <div class="inner_pos_Mod">
        <div class="img_adv">
            <%--<img src="images/qc_raovat.jpg" />--%>
            <asp:Repeater ID="rptFocusCompany" runat="server">
                <ItemTemplate>
                    <a href='<%= NewsUrl%><%#Eval("ID") %>'>
                        <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("Thumbnail")%>' /></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
