<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvinceInfoUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.ProvinceInfoUS" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <%= WebpartParent.NewsType%></div>
        </div>
        <div class="content_F_Right">
            <asp:Repeater ID="rptProvinceInfo" runat="server" OnItemDataBound="rptProvinceInfo_OnItemDataBound">
                <HeaderTemplate>
                    <marquee direction="up" scrolldelay="50" scrollamount="1" truespeed="true" onmouseover="this.stop()"
                             onmouseout="this.start()" height="200px">						
						<ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
					    <a href='<%= NewsUrl%><%#Eval("ID") %>'><%#Eval("Title") %></a>
					    <span class="time_update">Ngày <%#Eval("Created") %></span>
					</li>
                </ItemTemplate>
                <FooterTemplate></ul></marquee></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
