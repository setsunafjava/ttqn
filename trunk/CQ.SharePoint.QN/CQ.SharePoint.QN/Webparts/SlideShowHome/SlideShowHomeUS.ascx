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
                <asp:Repeater ID="rptImages" runat="server" OnItemDataBound="rptImages_OnItemDataBound">
                    <HeaderTemplate><marquee width="600px" onmouseout="this.start()" onmouseover="this.stop()"
                    scrolldelay="50" scrollamount="1" truespeed="true"></HeaderTemplate>
                    <ItemTemplate>
                        <div class="img_typ_Album">
							<div class="img_album"><img src="<%#Eval("Description") %>" /></div>
							<div class="name_album"><a href="#"><%#Eval("Title")%></a></div>
						</div>
                    </ItemTemplate>
                    <FooterTemplate></marquee></FooterTemplate>
                </asp:Repeater>
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
