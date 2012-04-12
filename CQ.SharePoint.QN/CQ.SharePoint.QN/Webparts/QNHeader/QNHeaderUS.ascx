<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QNHeaderUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.QNHeaderUS" %>

<div id="header">
	<div class="banner">
		<img src="images/Index_02.gif" />
	</div>
	<div class="top_menu">
		<div class="menu">
			<ul id="nav">
						<li><a href="#" style="background:url(images/bg_menu_hover.jpg) top left repeat-x;">Trang chủ</a>							
						</li>
						
						<li><a href="#">Giới thiệu</a>
							<ul>
							<li><a href="#">Di tích lịch sử</a></li>
							<li><a href="#">Điều kiện KT-XH</a></li>
							<li><a href="#">Đơn vị hành chính</a></li>
							</ul>
						</li>
						
						<li><a href="#">Chuyên đề</a>
							<ul>
							<li><a href="#">Định hướng phát triển</a></li>
							<li><a href="#">Số liệu thống kê</a></li>
							<li><a href="#">Thông tin KT-XH</a></li>
							</ul>
						</li>
						<li><a href="#">VB mới ban hành</a>
							<ul>
							<li><a href="#">Văn bản 1</a></li>
							<li><a href="#">Văn bản 2</a></li>
							<li><a href="#">Văn bản 3</a></li>
							</ul>
						</li>	
						<li><a href="#">Doanh nghiệp</a>
							<ul>
							<li><a href="#">Mới thành lập</a></li>
							<li><a href="#">Thay đổi ĐKKD</a></li>
							<li><a href="#">Giải thể</a></li>
							<li><a href="#">Đăng tin</a></li>
							<li><a href="#">Khác</a></li>
							</ul>
						</li>	
						<li><a href="#">Dịch vụ</a>
							<ul>
							<li><a href="#">Dịch vụ 1</a></li>
							<li><a href="#">Dịch vụ 2</a></li>
							<li><a href="#">Dịch vụ 3</a></li>
							</ul>
						</li>	
						<li><a href="#">Download</a>
						</li>		
						<li><a href="#">Liên hệ</a>
					
						</li>	
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