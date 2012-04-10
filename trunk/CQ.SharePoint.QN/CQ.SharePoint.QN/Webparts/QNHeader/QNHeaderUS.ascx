<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QNHeaderUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.QNHeaderUS" %>

<div id="header">
	<div class="banner">
		<img src="images/Index_02.gif" />
	</div>
	<div class="top_menu">
		<div class="menu">
			<div id="hotrolaptrinh_submenungangpro_menu">
					<dl>
						<dt><a href="#" class="home_page">Trang Chủ</a></dt>					
					</dl>
					<dl>
						<dt onmouseover="hotrolaptrinh_submenufunction('sub2');"><a href="#">Giới Thiệu</a></dt>
					</dl>
					<dl>
						<dt onmouseover="hotrolaptrinh_submenufunction('sub2');"><a href="#">Chuyên Đề</a></dt>
						
					</dl>
					<dl>
						<dt onmouseover="hotrolaptrinh_submenufunction('sub2');"><a href="#">VB mới ban hành</a></dt>
						
					</dl>
					<dl>
						<dt onmouseover="hotrolaptrinh_submenufunction('sub2');"><a href="#">Chuyên đề</a></dt>
						
					</dl>
					<dl>
						<dt onmouseover="hotrolaptrinh_submenufunction('sub2');"><a href="#">Doanh Nghiệp</a></dt>
						<dd id="sub2" class="sub_menu">
							<ul>
								<li><a href="#">Mới thành lập</a></li>
								<li><a href="#">Thay đổi ĐKKD</a></li>
								<li><a href="#">Giải thể</a></li>
								<li><a href="#">Đăng tin</a></li>
								<li><a href="#">Khác</a></li>
							</ul>
						</dd>
					</dl>
					<dl>
						<dt onmouseover="hotrolaptrinh_submenufunction('sub2');"><a href="#">Dịch vụ</a></dt>					
					</dl>
					<dl>
						<dt onmouseover="hotrolaptrinh_submenufunction('sub2');"><a href="#">Download</a></dt>					
					</dl>
					<dl>
						<dt onmouseover="hotrolaptrinh_submenufunction('sub2');"><a href="#">Liên hệ</a></dt>					
					</dl>
					
				</div>	
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