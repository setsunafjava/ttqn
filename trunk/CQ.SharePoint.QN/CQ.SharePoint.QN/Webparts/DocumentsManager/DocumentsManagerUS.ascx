<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentsManagerUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.DocumentsManagerUS" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <%= WebpartParent.FocusCompanyTitle %>
            </div>
        </div>
        <div class="list_carousel responsive">
            <ul id="foo5">
                <asp:Repeater ID="rptFocusCompany" runat="server" OnItemDataBound="OnItemDataBound_DocumentsManager">
                    <ItemTemplate>
                        <li><a href='<%#Eval("LinkUrl")%>'>
                            <%--<asp:Image CssClass="ImageThumnail" ID="img" runat="server" ImageUrl='<%#Eval("Thumbnail")%>' />--%>
                            <asp:Literal ID="ltrFlash1" runat="server"></asp:Literal> </li>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>
