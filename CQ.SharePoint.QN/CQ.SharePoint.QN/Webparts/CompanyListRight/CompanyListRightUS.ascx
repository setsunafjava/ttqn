<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyListRightUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.CompanyListRightUS" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <%= WebpartParent.CompanyType%>
            </div>
        </div>
        <div class="content_F_Right">
            <div class="inner_list_company_new">
                <asp:Repeater ID="rptCompanyList" runat="server">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><a href='<%= NewsUrl%><%#Eval("ID") %>'>
                            <%#Eval("Title")%></a></li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <div class="read_more">
                    <a href='<%= CategoryUrl%><%=WebpartParent.CompanyId %>'>&raquo;
                        <%= WebpartParent.ReadMore %></a></div>
            </div>
        </div>
    </div>
</div>
