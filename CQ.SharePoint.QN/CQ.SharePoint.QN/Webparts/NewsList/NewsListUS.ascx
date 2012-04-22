<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsListUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.NewsListUS" %>
    <style type="text/css">
    .redtext
    {
    	color:Red;
    	}
    </style>
<%--<div class="inner_content_subpage">
    <div class="cont_artical">
        <div class="name_artical">
            <a href="#">Thông báo thành lập Công Ty Trần Phát Linh</a> <span class="time_update">
                (ngày 20/02/2012)</span></div>
        <div class="interpre">
            <div class="img_thumb">
                <img src="images/logo.jpg" /></div>
            <div class="short_content">
                Giấy chứng nhận doanh nghiệp đăng ký số 00000.... Đăng ký ngày 20/02/2012</div>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
<div class="inner_content_subpage">
    <div class="cont_artical">
        <div class="name_artical">
            <a href="#">Thông báo thành lập Công Ty Trần Phát Linh</a> <span class="time_update">
                (ngày 20/02/2012)</span></div>
        <div class="interpre">
            <div class="img_thumb">
                <img src="images/logo.jpg" /></div>
            <div class="short_content">
                Giấy chứng nhận doanh nghiệp đăng ký số 00000.... Đăng ký ngày 20/02/2012</div>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
<div class="inner_content_subpage">
    <div class="cont_artical">
        <div class="name_artical">
            <a href="#">Thông báo thành lập Công Ty Trần Phát Linh</a> <span class="time_update">
                (ngày 20/02/2012)</span></div>
        <div class="interpre">
            <div class="img_thumb">
                <img src="images/logo.jpg" /></div>
            <div class="short_content">
                Giấy chứng nhận doanh nghiệp đăng ký số 00000.... Đăng ký ngày 20/02/2012</div>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
<div class="inner_content_subpage">
    <div class="cont_artical">
        <div class="name_artical">
            <a href="#">Thông báo thành lập Công Ty Trần Phát Linh</a> <span class="time_update">
                (ngày 20/02/2012)</span></div>
        <div class="interpre">
            <div class="img_thumb">
                <img src="images/logo.jpg" /></div>
            <div class="short_content">
                Giấy chứng nhận doanh nghiệp đăng ký số 00000.... Đăng ký ngày 20/02/2012</div>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
<div class="inner_content_subpage">
    <div class="cont_artical">
        <div class="name_artical">
            <a href="#">Thông báo thành lập Công Ty Trần Phát Linh</a> <span class="time_update">
                (ngày 20/02/2012)</span></div>
        <div class="interpre">
            <div class="img_thumb">
                <img src="images/logo.jpg" /></div>
            <div class="short_content">
                Giấy chứng nhận doanh nghiệp đăng ký số 00000.... Đăng ký ngày 20/02/2012</div>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>--%>

<asp:Repeater ID="rptListCategory" runat="server">
    <ItemTemplate>
        <div class="inner_content_subpage">
            <div class="cont_artical">
                <div class="name_artical">
                    <a href='<%= NewsUrl%><%#Eval("ID") %>'>
                        <%#Eval("Title")%></a> <span class="time_update">(ngày
                            <%#Eval("Modified")%>)</span></div>
                <div class="interpre">
                    <div class="img_thumb">
                        <img src="images/logo.jpg" /></div>
                    <div class="short_content">
                        <%#Eval("ShortContent")%></div>
                    <div class="cleaner">
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

<asp:Label ID="lblItemNotExist" CssClass="redtext" runat="server"></asp:Label>