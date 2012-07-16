<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SlideShowHomeUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.SlideShowHomeUS" %>
	
<style>
	.bg_title_tva {
    background: url("../images/bg_title_right_corner.jpg") repeat-x scroll left top transparent;
    border: 1px solid #B4E7F6;
    color: #666666;
    font-family: tahoma;
    font-size: 11px;
    font-weight: bold;
    height: 27px;
    margin-top: -16px;
	padding-top: 8px;
	margin-left: -5px;
	margin-right: 2px;
    text-align: center;
    text-transform: uppercase;
}
</style>
	
<div class="scroll_slide">
    <div class="bg_top_scroll">
    </div>
    <div class="img_typ_SC" align="center">
        <asp:Repeater ID="rptImages" runat="server" OnItemDataBound="rptImages_OnItemDataBound">
            <HeaderTemplate>
                <div id="slider">
                    <div class="bg_title_tva">
                        <%= ParentWP.SlideShowHomeTitle %></div>
                    <img class="scrollButtons left" src="QNResources/leftarrow.png" />
                    <div style="overflow: hidden;" class="scroll">
                        <div class="scrollContainer">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="panel" id="panel_1">
                    <div class="inside">
                        <img runat="server" id="imgLink" title='<%#Eval("Title") %>' height='92' />
                        <a runat="server" id="aLink">
                            <%#Eval("Title") %></a>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div> </div>
                <img class="scrollButtons right" src="QNResources/rightarrow.png" />
                </div>
            </FooterTemplate>
        </asp:Repeater>
        <div class="cleaner">
        </div>
    </div>
    <div class="cleaner">
        <div class="bg_bottom_scroll">
        </div>
    </div>

    <script type="text/javascript" src="/QNResources/slider.js"></script>
