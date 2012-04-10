<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SlideShowHomeUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.SlideShowHomeUS" %>
<div class="scroll_slide">
    <div class="bg_top_scroll">
    </div>
    <div class="inner_scroll_slide">
        <h3>
            Thư viện ảnh quảng ninh</h3>
        <div>
            <div class="arrow_left">
                <a href="#">
                    <img src="images/arrow_next.jpg" /></a></div>
            <div class="img_typ_SC" align="center">
                <marquee marquee width="600px" onmouseout="this.start()" onmouseover="this.stop()"
                    scrolldelay="50" scrollamount="1" truespeed="true">
							<div class="img_typ_Album">
								<div class="img_album"><img src="images/images.jpg" /></div>
								<div class="name_album"><a href="#">Những hình ảnh đặc sắc về Quảng Ninh</a></div>
							</div>
							<div class="img_typ_Album">
								<div class="img_album"><img src="images/images.jpg" /></div>
								<div class="name_album"><a href="#">Những hình ảnh đặc sắc về Quảng Ninh</a></div>
							</div>
							<div class="img_typ_Album">
								<div class="img_album"><img src="images/images.jpg" /></div>
								<div class="name_album"><a href="#">Những hình ảnh đặc sắc về Quảng Ninh</a></div>
							</div>
							<div class="img_typ_Album">
								<div class="img_album"><img src="images/images.jpg" /></div>
								<div class="name_album"><a href="#">Những hình ảnh đặc sắc về Quảng Ninh</a></div>
							</div>
							
						</marquee>
                <div class="cleaner">
                </div>
            </div>
            <div class="arrow_right">
                <a href="#">
                    <img src="images/arrow_back.jpg" /></a></div>
            <div class="cleaner">
            </div>
        </div>
    </div>
    <div class="bg_bottom_scroll">
    </div>
</div>
