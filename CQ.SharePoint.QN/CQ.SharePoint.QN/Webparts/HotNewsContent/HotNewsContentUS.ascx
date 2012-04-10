<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotNewsContentUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.HotNewsContentUS" %>
<div class="News_top_pos">
    <div class="hot_news-content">
        <div class="artical_hottest">
            <img src="images/News_post.jpg" /></div>
        <div class="short_content-hottest">
            Trong khi ban quản lý công trình vẫn giữ quan điểm nứt khe nhiệt là bình thường
            thì các chuyên gia về đập, thủy lợi, địa chất đều khẳng định tình trạng đập thủy
            điện sông Tranh 2 nứt, rò nước là bất thường, tối kỵ.
        </div>
        <div class="time_update">
            (Ngày 20 - 03 - 2012)
        </div>
    </div>
    <div class="tab_content_News">
        <div class="info_tab_content">
            <ul id="countrytabs" class="shadetabs">
                <li><a href="#" rel="country1" class="selected">Mới nhất</a></li>
                <li><a href="#" rel="country2">Đọc nhiều</a></li>
            </ul>
            <div class="inner_content_tab">
                <div id="country1" class="tabcontent">
                    <ul>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                    </ul>
                </div>
                <div id="country2" class="tabcontent">
                    <ul>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                    </ul>
                </div>

                <script type="text/javascript">
						
						var countries=new ddtabcontent("countrytabs")
						countries.setpersist(true)
						countries.setselectedClassTarget("link") //"link" or "linkparent"
						countries.init()
						
                </script>

                <div class="more_info_tabcontent">
                    <div class="arrow_top">
                        <a href="#">
                            <img src="images/arrow_top.jpg" /></a></div>
                    <div class="arrow_bottom">
                        <a href="#">
                            <img src="images/arrow_bottom.jpg" /></a></div>
                </div>
                <div class="cleaner">
                </div>
            </div>
        </div>
    </div>
    <div class="cleaner">
    </div>
</div>
