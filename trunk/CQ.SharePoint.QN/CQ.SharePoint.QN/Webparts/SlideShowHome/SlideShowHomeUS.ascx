<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SlideShowHomeUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.SlideShowHomeUS" %>
<div class="scroll_slide">
    <div class="bg_top_scroll">
    </div>
    <div class="img_typ_SC" align="center">
        <asp:Repeater ID="rptImages" runat="server" OnItemDataBound="rptImages_OnItemDataBound">
            <HeaderTemplate>
                <div id="slider">
                    <p style="text-align: center; font-family: tahoma; font-size: 11px; text-transform: uppercase;
                        font-weight: bold;">
                        <%= ParentWP.SlideShowHomeTitle %></p>
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
