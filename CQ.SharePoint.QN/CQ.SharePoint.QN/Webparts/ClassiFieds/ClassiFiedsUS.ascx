<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassiFiedsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.ClassiFiedsUS" %>
<div class="pos_MOD">
    <div class="bg_title_mod">
        <%=WebpartParent.ClassiFiedsTitle%>
    </div>
    <div class="inner_pos_Mod">
        <div class="img_adv">
            <asp:Repeater ID="rptFocusCompany" OnItemDataBound="OnItemDataBound_ClassiFieds" runat="server">
                <ItemTemplate>
                    <%--<asp:LinkButton ID="lbtImage" runat="server" OnClick="Link_OnClick" CommandArgument='<%#Eval("ID")%>'>
                        <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("Thumbnail")%>' />
                    </asp:LinkButton>--%>
                    <a href='<%#Eval("LinkUrl")%>'>
                        <asp:Literal ID="ltrFlash1" runat="server"></asp:Literal>
                    </a>
                    <%--<a href='<%#Eval("LinkUrl")%>'>
                        <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("Thumbnail")%>' />
                    </a>--%>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
