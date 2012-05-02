<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotNewsContentUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.HotNewsContentUS" %>
<div class="hot_news-content">
    <div class="artical_hottest">
        <asp:Image ID="imgMainImage" runat="server" Width="396px" Height="305px" />
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
            <li><a href="#" rel="country1" class="selected">
                <asp:Label ID="lblLatest" runat="server" Text="Mới nhất"></asp:Label></li>
            <li><a href="#" rel="country2">
                <asp:Label ID="lblReadMost" runat="server" Text="Đọc nhiều"></asp:Label></a></li>
        </ul>
        <div class="inner_content_tab">
            <div id="country1" class="tabcontent">
                <asp:Repeater ID="rptLatestNews" runat="server">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                            <%#Eval("Title")%></a> <span>(ngày
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
                            <%#Eval("ShortContent")%></a><span>(ngày
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
