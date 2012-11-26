 <%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotNewsContentUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.HotNewsContentUS" %>
<%@ Import Namespace="Microsoft.SharePoint.Utilities" %>
<div class="hot_news-content">
    <div class="artical_hottest">
        <div id="number_slideshow1" class="number_slideshow">
            <asp:Repeater ID="rptImages" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                 <ItemTemplate>
                    <li><a href='<%= RptImagesUrl%><%#Eval("ID") %>&CategoryId=<%#Eval("CategoryId") %>'>
                        <div class="boxgrid captionfull">
                            <img src='<%#Eval("Thumbnail") %>' alt='<%#Eval("Title")%>' title='<b><%#Eval("Title")%></b>'
                                style="width: 400px; height: 330px" />
                            <div class="cover boxcaption">
                                <p style="font-weight: bold; ">
                                    <%#Eval("Title")%>
								</p>
								
                                <p>      
								<br /><br />							
                                    <%# Server.HtmlEncode((string)Eval("ShortContent"))%>
								</p>	
                            </div>
                        </div>
                    </a></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                    <ul class="number_slideshow_nav">
                        <li><a href="#">1</a></li>
                        <li><a href="#">2</a></li>
                        <li><a href="#">3</a></li>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
            <div style="clear: both">
            </div>
        </div>
    </div>
    <div class="inner_list_company_new">
        <asp:Repeater ID="rptThreeItem" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><a href='<%= RptThreeItemUrl%><%#Eval("ID") %>&CategoryId=<%#Eval("CategoryId") %>'>
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
                    <li class="title_news_hot">
                        <%=WebPartParent.LatestRecieved %></li>
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
                        <li style=" text-align:justify"><a href='<%= RptLatestNewsUrl%><%#Eval("ID") %>&CategoryId=<%#Eval("CategoryId") %>'>
                            <%#Eval("Title")%></a> <span style="color: #003399">(<%=WebPartParent.Day %><%#Eval("ArticleStartDateTemp")%>)</span>
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
                        <li><a href='<%= RptTopViewsUrl%><%#Eval("ID") %>&CategoryId=<%#Eval("CategoryId") %>'>
                            <%#Eval("Title")%></a><span style="color: #003399">(<%=WebPartParent.Day %><%#Eval("ArticleStartDateTemp")%>)</span></li>
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

<script language="javascript" type="text/javascript">
		$(document).ready(function(){
				
				//Full Caption Sliding (Hidden to Visible)
				$('.boxgrid.captionfull').hover(function(){
					$(".cover", this).stop().animate({top:'200px'},{queue:false,duration:160});
				}, function() {
					$(".cover", this).stop().animate({top:'272px'},{queue:false,duration:160});
				});				
				
				$(function() {
                $("#number_slideshow1").number_slideshow({
                    slideshow_autoplay: 'enable',//enable disable
                    slideshow_time_interval: '5000',
                    slideshow_window_background_color: "#CCFFCC",
                    slideshow_window_padding: '0',
                    slideshow_window_width: '400',
                    slideshow_window_height: '330',
                    slideshow_border_size: '0',
                    slideshow_border_color: '#006600',
                    slideshow_show_button: 'enable',//enable disable
                    slideshow_show_title: 'disable',//enable disable
                    slideshow_button_text_color: '#FFF',
                    slideshow_button_background_color: '#333',
                    slideshow_button_current_background_color: '#006600',
                    slideshow_button_border_color: '#006600',
                    slideshow_loading_gif: 'loading.gif',//loading pic position, you can replace it use youself gif.
                    slideshow_button_border_size: '0'
                });
			});
			});
</script>

<script type="text/javascript" src="/QNResources/javascript.js"></script>

<script type="text/javascript" src="/QNResources/number_slideshow.js"></script>

