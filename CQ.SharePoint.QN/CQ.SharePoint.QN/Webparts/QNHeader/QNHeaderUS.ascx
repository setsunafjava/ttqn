<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QNHeaderUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.QNHeaderUS" %>

<div id="header">
	<div class="banner">
		<img src="images/Index_02.gif" />
	</div>
	<div class="top_menu">
		<div class="menu">
		    <ul id="nav">
				<li><a href="#" style="background:url(images/bg_menu_hover.jpg) top left repeat-x;">Trang chủ</a></li>
                <asp:Repeater ID="rptMenu" runat="server" OnItemDataBound="rptMenu_OnItemDataBound">
                    <ItemTemplate>
                        <li><a href="#"><%#Eval("Title") %></a>
                            <asp:Repeater ID="rptSubMenu" runat="server">
                                <HeaderTemplate><ul></HeaderTemplate>
                                <ItemTemplate>
                                    <li><a href="#"><%#Eval("Title") %></a></li>
                                </ItemTemplate>
                                <FooterTemplate></ul></FooterTemplate>
                            </asp:Repeater>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
		</div>
		<div class="search">
			<input type="text" id="txtData" name="q" onkeypress="return BBEnterPress();" style="border: 0px;"> <a href="#">Tìm kiếm</a>
		</div>
		<div class="language">
			<span><img src="images/english.jpg" /></span><span><a href="#">English</a></span>
		</div>
		<div class="cleaner"></div>
				
	</div>
	<div class="cleaner"></div>
	<div class="bg_bottom_top_menu">
		<div class="inner_content_bottom_topMenu">
			<div class="time_date">Hôm nay, ngày 22/02/2012 10:33:55 AM</div>
			<div class="set_hompage"><a href="#">Đặt làm trang chủ</a></div>
			<div class="RSS"><a href="#">RSS</a></div>
			<div class="cleaner"></div>
		</div>
	</div>
</div>