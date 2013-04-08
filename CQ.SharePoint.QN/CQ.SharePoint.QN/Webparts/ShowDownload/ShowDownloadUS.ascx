 <%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowDownloadUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.ShowDownloadUS" %>
<style type="text/css">
    .redtext
    {
        color: Red;
    }
</style>
<asp:Repeater ID="rptListCategory" runat="server" OnItemDataBound="rptSearch_OnItemDataBound">
    <ItemTemplate>
        <div class="inner_content_subpage">
            <div class="cont_artical">
                <div class="name_artical">
                    <a href='<%=NewsUrl%><%#Eval("ListCategoryName")%>&ListName=<%#Eval("ListNewsName")%>&NewsId=<%#Eval("ID")%>&CategoryId=<%#Eval("CategoryId") %>'>
                        <%#Eval("Title")%></a><br />
                    <span class="datetimeText">Ngày
                            <%#Eval("ArticleStartDates")%>)</span></div>
                <div class="interpre">
                    <div class="img_thumb">
                        <%--<img src="images/logo.jpg" />--%>
                        <asp:Image ID="imgLogo" runat="server" Width="120px" Height="70px" ImageUrl='<%#Eval("Thumbnail") %>' onError='this.parentNode.style.display="none";this.parentNode.parentNode.getElementsByClassName("short_content")[0].style.width="420px";' />
                    </div>
                    <div class="short_content">
                        <%#Eval("ShortContent")%></div>
                    <div class="cleaner">
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<table>
    <tr>
        <td>
            <asp:HyperLink ID="lnkPrev" runat="server" Text="Trước"></asp:HyperLink>
        </td>
        <td>
            <asp:Label ID="lblCurrpage" runat="server"></asp:Label>
        </td>
        <td>
            <asp:HyperLink ID="lnkNext" runat="server" Text="Sau"></asp:HyperLink>
        </td>
    </tr>
</table>
<asp:Label ID="lblItemNotExist" CssClass="redtext" runat="server"></asp:Label>
