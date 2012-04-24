<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShouldToKnowUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.ShouldToKnowUS" %>
<div class="contact_adv">
    Liên hệ quảng cáo: Hotline 0904 555 888</div>
<div class="info_more">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_title_typ_News">
                <%= WebpartParent.NewsType%></div>
            <div class="link_cate_more">
                <ul>
                    <li><a href="#">Thông tin doanh nghiệp</a></li>|
                    <li><a href="#">Tư vấn tiêu dùng</a></li>|
                    <li><a href="#">Nhà hàng - Khách sạn</a></li>
                    <li><a href="#">Tuyển dụng</a></li>
                    <li><a href="#">Mua bán</a></li>
                    
                    <asp:Repeater ID="rptNewsGroup" runat="server">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><a href='<%= CategoryUrl%><%#Eval("ID") %>'><%#Eval("Title")%></a></li>|                        
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                </ul>
            </div>
            <div class="cleaner">
            </div>
        </div>
        <div class="inner_infoMore">            
            <div class="P1">
                <asp:Repeater ID="rptShouldYouKnow" runat="server">
                    <ItemTemplate>
                        <div class="name_P">
                            <a href='<%= NewsUrl%><%#Eval("ID") %>'>
                                <%#Eval("Title") %></a>
                            <div class="link_web_P">
                                <a href='<%#Eval("LinkAdv") %>'><%#Eval("LinkAdv") %></a></div>
                        </div>
                        <div class="img_short_content">
                            <div class="img_thumb">
                                <img src='<%#Eval("Thumbnail") %>' /></div>
                            <div class="short_info">
                                <%#Eval("ShortContent") %>
                            </div>
                            <div class="cleaner">
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
