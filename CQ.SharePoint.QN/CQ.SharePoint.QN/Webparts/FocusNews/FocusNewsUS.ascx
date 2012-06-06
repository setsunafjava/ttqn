<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FocusNewsUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.FocusNewsUS" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <%--Tin tiêu điểm--%>
                <asp:Label ID="lblTitle" Text="Tin tiêu điểm" runat="server"></asp:Label>
            </div>
        </div>
        <div class="content_F_Right">
            <%--<marquee direction="up" scrolldelay="50" scrollamount="1" truespeed="true" onmouseover="this.stop()"
                onmouseout="this.start()" height="350px">     --%>                   
                        <asp:Repeater ID="rptFocusNews" runat="server" OnItemDataBound="FocusNews_OnItemDataBound">                        
                        <ItemTemplate>
                            <div class="line_news">
							<div class="thumb_img"><img src="images/images.jpg" /></div>
							<div class="name_news">
								<a href='<%= NewsUrl%><%#Eval("ID") %>'><%#Eval("ShortContent")%></a>
								<span style="color:#003399">(<asp:Label ID="lblDate" runat="server"></asp:Label> <%#Eval("Modified")%>)</span>
							</div>
							<div class="cleaner"></div>
						</div>
                        </ItemTemplate>                       
                    </asp:Repeater>
						<%--</marquee>--%>
            <div class="read_more">
                <a href='<%= CategoryUrl %>'>&raquo; <asp:Label ID="lblSeeMore" runat="server"></asp:Label></a></div>
        </div>
    </div>
</div>
