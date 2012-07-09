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
                            <img src='<%#Eval("Thumbnail") %>' alt='<%#Eval("Title")%>' title='<b><%#Eval("Title")%></b>'
                                style="width: 400px; height: 330px" /></a><%#Eval("ShortContent")%></li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="ws_bullets">
                <div>
                     <a href="#" title="Anh thu 1">1</a> <a href="#" title="Anh thu 2">2</a> <a href="#"
                        title="Anh So 3">3</a>
                </div>
            </div>
        </div>
    </div>
    <div class="inner_list_company_new">
        <asp:Repeater ID="rptThreeItem" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                    <%#Eval("Title")%></a></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
<div class="tab_content_News">
    <div class="info_tab_content">
        <ul id="countrytabs" class="shadetabs">
            <asp:Panel ID="pnlIndex" runat="server">
                <li><a href="#" rel="country1" class="selected">
                     <%=WebPartParent.LatestNews %>
                </a></li>
                <li><a href="#" rel="country2">
                    <%=WebPartParent.MostViews %>
                </a></li>
            </asp:Panel>
            <asp:Panel ID="pnlSubPage" Visible="false" runat="server">
                <a rel="country1" class="title_news_hot">
                    <%=WebPartParent.LatestRecieved %>
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
                            <%#Eval("Title")%></a> <span style="color: #003399">(<%=WebPartParent.Day %><%#Eval("Created")%>)</span>
                        </li>
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
                             <%#Eval("Title")%></a><span style="color: #003399">(<%=WebPartParent.Day %><%#Eval("Created")%>)</span></li>
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
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
<div class="cleaner">
</div>

<script type="text/javascript" src="/QNResources/wowslider.js"></script>
<script type="text/javascript" src="/QNResources/javascript.js"></script>

