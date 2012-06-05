<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotNewsContentUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.HotNewsContentUS" %>
<div class="hot_news-content">
    <div class="artical_hottest">
        <div id="wowslider-container1">
            <div class="ws_images">
                <asp:Repeater ID="rptImages" runat="server">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                            <img src='<%#Eval("Thumbnail") %>' alt='<%#Eval("Title")%>' title='<%#Eval("Title")%>' style="width: 400px;
                                height: 330px" /></a><%#Eval("ShortDescription")%></li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="ws_bullets">
                <div>
                    <a href="#" title="004">1</a> <a href="#" title="Anh thu 2">2</a> <a href="#" title="Anh So 3">
                        3</a>
                </div>
            </div>
        </div>
    </div>
    <div class="short_content-hottest">
        <a href='<%=Linktoitem %>'>
            <asp:Label ID="lblShortContent" runat="server"></asp:Label>
        </a>
    </div>
    <div class="time_update">
        <asp:Label ID="lblTimeUpdate" runat="server" />
    </div>
</div>
<div class="tab_content_News">
    <div class="info_tab_content">
        <ul id="countrytabs" class="shadetabs">
            <asp:Panel ID="pnlIndex" runat="server">
                <li><a href="#" rel="country1" class="selected">
                    <asp:Label ID="lblLatestNews" runat="server" Text="Tin mới"></asp:Label>
                </a></li>
                <li><a href="#" rel="country2">
                    <asp:Label ID="lblReadMost" Text="Đọc nhiều" runat="server"></asp:Label>
                </a></li>
            </asp:Panel>
            <asp:Panel ID="pnlSubPage" Visible="false" runat="server">
                <a rel="country1" class="title_news_hot">
                    <asp:Label ID="lblNewsLatestSend" Text="Tin mới nhận" runat="server"></asp:Label>
                </a>
            </asp:Panel>
        </ul>
        <div class="inner_content_tab">
            <div id="country1" class="tabcontent">
                <asp:Repeater ID="rptLatestNews" runat="server">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                            <%#Eval("Title")%></a> <span style="color: #003399">(
                                <asp:Label ID="lblDay" Text="Ngày" runat="server"></asp:Label>
                                <%#Eval("Created")%>)</span> </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div id="country2" class="tabcontent">
                <asp:Repeater ID="rptTopViews" runat="server">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                            <%#Eval("ShortContent")%></a><span style="color: #003399">(
                                <asp:Label ID="lblDay2" Text="Ngày" runat="server"></asp:Label>
                                <%#Eval("Created")%>)</span></li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <script type="text/javascript">
						
						var countries=new ddtabcontent("countrytabs")
						countries.setpersist(true)
						countries.setselectedClassTarget("link") //"link" or "linkparent"
						countries.init()
						
            </script>

            <div class="more_info_tabcontent">
                <div class="arrow_top">
                    <a href="#">
                        <img src="images/arrow_top.jpg" /></a></div>
                <div class="arrow_bottom">
                    <a href="#">
                        <img src="images/arrow_bottom.jpg" /></a></div>
            </div>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
<div class="cleaner">
</div>

<script type="text/javascript" src="/sites/demo/QNResources/wowslider.js"></script>

<script type="text/javascript" src="/sites/demo/QNResources/javascript.js"></script>

