<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModeNewsContentUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.ModeNewsContentUS" %>
<div class="mod_content_News_1">
    <div class="bg_title_ModNews">
        <div class="cate_News_Mod1">
            <div>
                <asp:Label ID="lblFirstGroup" runat="server" /></div>
            <div>
                <asp:Label ID="lblSecondGroup" runat="server" /></div>
            <div>
                <asp:Label ID="lblThirdGroup" runat="server" /></div>
            <div class="cleaner">
            </div>
        </div>
        <div class="inner_content_ModNews1">
            <div class="cont_News">
                <div class="img_thumb_News">
                    <img src="images/images.jpg" /></div>
                <div class="intro_short_content_News">
                    <%--<a href="#">Hội nghị xúc tiến thương mại...</a>--%>
                    <a href="#"><asp:Label ID="lblHeaderTinhUy" runat="server" ></asp:Label></a>
                </div>
                <div class="list_other_news">
                    <asp:Repeater ID="rptTinhUy" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                                <%#Eval("ShortContent")%></a></li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="cont_News">
                <div class="img_thumb_News">
                    <img src="images/images.jpg" /></div>
                <div class="intro_short_content_News">
                    <%--<a href="#">Hội nghị xúc tiến thương mại...</a>--%>
                    <a href="#"><asp:Label ID="lblHeaderHoiDongNhanDan" runat="server" ></asp:Label></a>
                </div>
                <div class="list_other_news">
                    <asp:Repeater ID="rptHoiDongNhanDan" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                                <%#Eval("ShortContent")%></a></li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="cont_News">
                <div class="img_thumb_News">
                    <img src="images/images.jpg" /></div>
                <div class="intro_short_content_News">
                    <%--<a href="#">Hội nghị xúc tiến thương mại...</a>--%>
                     <a href="#"><asp:Label ID="lblHeaderUyBanNhanDan" runat="server" ></asp:Label></a>
                </div>
                <div class="list_other_news">
                    <asp:Repeater ID="rptUyBanNhanDan" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                                <%#Eval("ShortContent")%></a></li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
