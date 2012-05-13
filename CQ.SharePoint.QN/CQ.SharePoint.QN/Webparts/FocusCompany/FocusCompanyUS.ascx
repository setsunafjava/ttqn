<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FocusCompanyUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.FocusCompanyUS" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <%--Doanh nghiệp tiêu biểu--%>
                <asp:Label ID="lblFocusCompany" runat="server"></asp:Label>
            </div>
        </div>
        <div class="content_F_Right">
            <marquee direction="up" scrollamount="1" truespeed="true" onmouseover="this.stop()"
                onmouseout="this.start()" height="350px">				
						<div class="img_logo_company_ex">							
							<asp:Repeater ID="rptFocusCompany" runat="server">
							    <ItemTemplate>							       
							        <a href='<%#Eval("LinkToItem")%>'><asp:Image ID="img" runat="server" ImageUrl='<%#Eval("Thumbnail")%>'/></a>
							    </ItemTemplate>
							</asp:Repeater>
						</div>
						</marquee>
        </div>
    </div>
</div>
