<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyAdvUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.CompanyAdvUS" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <%= WebpartParent.CompanyType%>
            </div>
        </div>
        <div class="content_F_Right">
            <div class="inner_list_company_adv">
                <asp:Repeater ID="rptCompanyAdv" runat="server" OnItemDataBound="rptCompanyAdv_OnItemDataBound">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <a href='<%#Eval("LinkUrl") %>'>
                            <img src="<%#Eval("Thumbnail") %>" height="<%#Eval("Height") %>" width="<%#Eval("Width") %>" /></a>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul></FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
