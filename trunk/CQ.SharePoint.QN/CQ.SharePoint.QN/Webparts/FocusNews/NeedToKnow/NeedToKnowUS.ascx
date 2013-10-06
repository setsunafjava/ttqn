<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NeedToKnowUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.NeedToKnowUS" %>
<style type="text/css">
    .img-weather
    {
        display: inline;
        vertical-align: top;
    }
    .tbl-tygia th
    {
        text-align: left;
        background-color: #E7F3FF;
        color: #1028A5;
    }
    #img-Do img
    {
        display: inline;
    }
</style>

<script type="text/javascript">
var interactions_url    = 'http://interactions.vnexpress.net';
var domain_image        = 'http://st.f1.vnecdn.net/i/v101';
var base_url            = 'http://vnexpress.net';
var css_url             = 'http://st.f3.vnecdn.net/c/v107';
var img_url             = 'http://st.f1.vnecdn.net/i/v101';
var image_cloud        = 'http://l.f29.img.vnecdn.net';

 var tygia = {
    data: {},
    arr: [],
    run : function(){
        var self = this;
        if(self.arr.length){
            $.get(base_url+'/block/crawler',{arrKeys: self.arr}).done(function(data) {
                self.data = $.parseJSON(data);
                $.each(self.arr, function(index, value) {
                    switch (value){
                        case 'thoi_tiet':
                            self._getWeatherData();
                            break;
                        case 'gia_vang':
                            self._showBoxGoldPrice();
                            break;
                        case 'ty_gia':
                            self._showBoxTyGia();
                            break;   
                        case 'rao_vat_0':
                            self._showBoxRaoVat();
                            break; 
                        case 'chung_khoan_hose':
                            self._showLiveStock('chung_khoan_hose');
                            break;                                        
                        case 'chung_khoan_hnx':
                            self._showLiveStock('chung_khoan_hnx');
                            break;     
                        case 'co_phieu':
                            self._showTraCuuCoPhieu();
                            break;   
                    }
                });
            },"json");
        }
    },
    getWeatherData: function (){
        this.arr.push('thoi_tiet');
    },
    showBoxGoldPrice: function() {
        this.arr.push('gia_vang');
    },
    showBoxTyGia: function(){
        this.arr.push('ty_gia');    
    }, 
    showBoxRaoVat: function() {
        this.arr.push('rao_vat_0');  
    },
    showLiveStock: function(name){
        this.arr.push(name);
    },
    showTraCuuCoPhieu: function(){
        this.arr.push('co_phieu');
    },
    ////////////////////////////////////////// WEATHER BOX /////////////////////////////
    weather_show: function(location){
        //console.log(this.data.thoi_tiet);
        obj = this.data.thoi_tiet[location];
        if (typeof(obj) == 'undefined'){
            return false;
        }
        var box = $("#boxweather");
        // push name tu city box
        box.find("#img-Do").empty(); //empty box image
        var cityName ;
        cityName = obj.city_name;
        if(cityName=='T.P Hồ Chí Minh'){cityName='TP HCM';}
        if(cityName=='T.P Hà Nội'){cityName='Hà Nội';}
        box.find("#txt-City").text(cityName);
        box.find("#txt-Weather").text(obj.weather);
        box.find("#img-Do").prepend('<img src="'+img_url + '/weather/' +obj.weather_code + '.gif">');
        // push temp number
        var temp = obj.temp.split("");
        $.each(temp, function(index, value) {
            box.find("#img-Do").append('<img src="'+img_url + '/weather/' +value + '.gif">');
        });
        box.find("#img-Do").append('<img src="'+img_url + '/weather/c.gif">');
    },
    _getWeatherData:function(){
        var self = this;
        var obj = this.data.thoi_tiet;
        //alert('5');
        // push city to box
        //var city = $("#ulWeather");

        var arr_city = [];
        $.each(obj, function(index, value) {
            //T.P Hồ Chí Minh
            var cityName ;
            // pust to array city
            arr_city.push(index);
            cityName = value.city_name;
            if(cityName=='T.P Hồ Chí Minh'){cityName='TP HCM';}
            if(cityName=='T.P Hà Nội'){cityName='Hà Nội';}
            //city.append('<li rel="'+index+'">' + cityName + '</li>');
            // show temp when click
        });
//        if (jQuery.inArray("ha_noi", arr_city)){
//            self.weather_show('ha_noi');
//        }
//        else{
//            var length = arr_city.length;
//            var j = Math.floor( Math.random() * ( length + 1 ) );
//            self.weather_show[arr_city[j]];
//        }
//        
//        city.find("li").bind("click",function(){
//            self.weather_show($(this).attr("rel"));
//            $('#hideCity').hide();
//        });
//        // show hide city
//        $("#boxweather").find(".divRightW").bind("click",function(){
//              $('#hideCity').toggle('slow', function() {
//                // Animation complete.
//              });
//        });
        // default show 
        GetWeatherBox('ha_noi');
    },
    /////////////////////////////// BOX GIA VANG ///////////////////////////////
    _showBoxGoldPrice: function() {
    	//var data = crawlerdata.responseText;
        var obj = this.data.gia_vang;
        if (typeof(obj) == 'undefined'){
            return false;
        }
        var box = $("#tbl-goldprice");
        var html = '';
        $.each(obj, function(index, value) {
            if (typeof(value) == 'undefined'){
                return false;
            }
            html = '<tr>' +
                        '<td class="td-weather-title gold_name">'+ value.name + '</td>'+	
                        '<td class="td-weather-data txtr gold_buy">'+ value.buy +'</td>'+
                        '<td class="td-weather-data txtr gold_sell">'+ value.sell +'</td>'+
                    '</tr>';
            box.append(html);
        });
        //console.log(obj);
    },
    ////////////////////////////// BOX TY GIA //////////////////////////////////////
    _showBoxTyGia: function () {
        var obj = this.data.ty_gia.data;
        //console.log(obj);
        var box = $("#eForex").find(".goldtable");
        $.each(obj, function(index, value) {
            if (index >= 1 && value.sell){
                var typeName;
                typeName = value.typename;
                switch (typeName){
                    case 'Đô-la Mỹ':
                        typeName = 'USD';
                        break;
                    case 'Bảng Anh':
                        typeName = 'GBP';
                        break;
                    case 'Đô-la Hồng Kông':
                        typeName = 'HKD';
                        break;
                    case 'Franc Thụy Sĩ':
                        typeName = 'CHF';
                        break;
                    case 'Yên Nhật':
                        typeName = 'JPY';
                        break;
                    case 'Ðô-la Úc':
                        typeName = 'AUD';
                        break;
                    case 'Ðô-la Canada':
                        typeName = 'CAD';
                        break;
                    case 'Ðô-la Singapore':
                        typeName = 'SGD';
                        break;
                    case 'Đồng Euro':
                        typeName = 'EUR';
                        break;
                    case 'Ðô-la New Zealand':
                        typeName = 'NZD';
                        break;
                    case 'Bat Thái Lan':
                        typeName = 'Bat Thái Lan';
                        break;
                }
                box.append('<tr><td class="td-weather-title">'+typeName+'</td><td class="td-weather-data txtr">'+value.sell+'</td></tr>');
                // show temp when click
            }
        });
        jQuery('#eForex').jScrollPane({ showArrows: true });// scroll promotion 
        // using bind
        $('#eForex').bind('mousewheel', function(event, delta, deltaX, deltaY) {
            console.log(delta, deltaX, deltaY);
        });

        // using the event helper
        $('#eForex').mousewheel(function(event, delta, deltaX, deltaY) {
            console.log(delta, deltaX, deltaY);
        });
    },
    /////////////////////////////////////// RAO VAT ////////////////////////////////
    _showdataRaovat: function(obj){
        var obj = obj.XML.I;
        var box = $("#show_raovat_content");
        box.empty();
        $.each(obj, function(index, value) {
            box.append('<li><a href="'+value.PN+'" class="link-otheradword">'+value.T+'</a></li>');   
        });
        jQuery("#AdWord").jScrollPane({ showArrows: true });


    },
    _showBoxRaoVat: function() {
        var self = this;
        $("#fCate").bind("change", function(){
            var newCateRV = $(this).val();
            $.post(base_url+'/block/getjsonraovat',{id: newCateRV}, function(data) {
                data = $.parseJSON(data);
                self._showdataRaovat(data);
            },"json");
        });
        if (typeof(this.data.rao_vat_0) == 'undefined'){
            return false;
        }
        
        self._showdataRaovat(this.data.rao_vat_0);
    },
    /////////////////////////////////////// CHUNG KHOAN //////////////////////////////
    _showLiveStock: function(name){
        var box;
        var obj;
        if(name=='chung_khoan_hnx'){
            obj = this.data.chung_khoan_hnx.data;
            box = $("#QuoteRutGonHnx");
        }else{
            obj = this.data.chung_khoan_hose.data;
            box = $("#QuoteRutGonHose");
        }
        var sHTML;
        sHTML = '';
    	//console.log(obj);
        box.append('  <tbody>');
        $.each(obj, function(index, value) {
            var congtru;
            congtru = value.CongTru;
            box.append('      <tr>');
            box.append('          <td class="S" style="width: 44px;">'+value.MaCK+'</td>');
            box.append('          <td class="V" style="width: 46px;">'+value.TC+'</td>');
            box.append('          <td class="V" style="width: 45px;">'+value.GiaKhop+'</td>');
            box.append('          <td class="V" style="width: 45px;">'+value.KLKhop+'</td>');
            if (congtru > 0){
                box.append('          <td class="G" style="width: 26px;">'+congtru+'</td>');
            }else if (congtru < 0){
                box.append('          <td class="R" style="width: 26px;">'+congtru+'</td>');
            }else{
                box.append('          <td class="Y" style="width: 26px;">'+congtru+'</td>');
            }
            box.append('      </tr>');
        });
        box.append('  </tbody>');    
    },
    /////////////////////////////////////// TRA CUU CO PHIEU TRUC TUYEN //////////////////////////////
    _showTraCuuCoPhieu: function(){
        var obj;
        obj = this.data.co_phieu;
        //console.log(obj.ho);
        //console.log(objRv.total_klgd);
        var HOvalue = $.parseJSON(obj.ho[0]);
        $("#changeHoValue").append('<p style="width:130px">Thay đổi: <span>'+HOvalue.change_value+'</span><span class="updown"><i class="down"></i>'+HOvalue.change_percent+'</span></p>');
        $("#changeHoValue").append('<p class="c2" style="width:155px">Thay đổi từ đầu năm: <span>'+HOvalue.change_value+'</span></p>');
        $("#changeHoValue").append('<p style="width:160px">Tổng KLGD: <span>'+HOvalue.total_klgd+'(cp)</span></p>');
        $("#changeHoValue").append('<p class="c2" style="width:125px">Tổng GTGD: <span>'+HOvalue.total_gtgd+'</span></p>');

        var HAvalue = $.parseJSON(obj.ha[0]);
        $("#changeHaValue").append('<p style="width:130px">Thay đổi: <span>'+HAvalue.change_value+'</span><span class="updown"><i class="down"></i>'+HAvalue.change_percent+'</span></p>');
        $("#changeHaValue").append('<p class="c2" style="width:155px">Thay đổi từ đầu năm: <span>'+HAvalue.change_value+'</span></p>');
        $("#changeHaValue").append('<p style="width:160px">Tổng KLGD: <span>'+HAvalue.total_klgd+'(cp)</span></p>');
        $("#changeHaValuchangeHoValuee").append('<p class="c2" style="width:125px">Tổng GTGD: <span>'+HAvalue.total_gtgd+'</span></p>');

        //console.log(obj.images_url.ha[0]);
        var HAimage = obj.images_url.ha;
        this.buildImageCoPhieu('ImgHA', HAimage);
        var HOimage = obj.images_url.ho;
        this.buildImageCoPhieu('ImgHO', HOimage);
        //console.log(HAimage[0]);
    },
    buildImageCoPhieu: function(id, obj){
        var box = $("#" + id);
        box.append('<img class="fl chr chrdd hide" src="' + image_cloud + '/' + obj[0] + '" alt="" border="0" usemap="#planetmap"/>');
        box.append('<img class="fl chr chrmm" src="' + image_cloud + '/'  + obj[1] + '" alt="" border="0" usemap="#planetmap"/>');
        box.append('<img class="fl chr chrqq hide" src="' + image_cloud + '/'  + obj[2] + '" alt="" border="0" usemap="#planetmap"/>');
        box.append('<img class="fl chr chryy hide" src="' + image_cloud + '/'  + obj[3] + '" alt="" border="0" usemap="#planetmap"/>');
        box.append('<img class="fl chr chrall hide" src="' + image_cloud + '/'  + obj[4] + '" alt="" border="0" usemap="#planetmap"/>');
    }
}                    

/************************ CO PHIEU ****************/
function initvsi() {
    $(".tvsistxt").focus(function() {
        if($(this).val() == "Nhập mã chứng khoán cần tìm")
            $(this).val("");
    });
	
    $(".tvsistxt").blur(function() {
        if($(this).val() == "")
            $(this).val("Nhập mã chứng khoán cần tìm");
    });
	 $(".txtsearch ").focus(function() {
        if($(this).val() == "Từ khóa tìm kiếm")
            $(this).val("");
    });
	
	$(".tvsistxt").bind("click", function(){
		$(this).val("");
	});
	
    $(".txtsearch ").blur(function() {
        if($(this).val() == "")
            $(this).val("Từ khóa tìm kiếm");
    });
    tabCktvHome();
}
/*tvsi box homepage*/
function tabCktvHome() {
    // tin moi nhat
    $(function(){
    	$(".litab1").bind("click", function() {
    		$(".litab1").removeClass('litab-active1');
    		$('.dbtthose').hide();		
        var referId = $(this).attr('id');
    		$('#'+referId).addClass('litab-active1');
    		$('.'+referId).show();		
        });		
    });
     var idtabsort = "chrdd";
     $(".navview li a").click(function() {
        var $this = $(this);
            $this.parents(".navview").find("a").removeClass("active");
            $this.addClass("active");
            idtabsort = $this.attr("rel");
            var $charwrap = $this.parents(".navview").prev();
            $charwrap.children("img").hide();
            $charwrap.children("." + idtabsort).fadeIn(0);
    });
    return false;
}

$(document).ready(function() {
    tygia.showBoxGoldPrice();
	tygia.getWeatherData();
	tygia.run();
});
function GetWeatherBox(location){
    //alert(location);
	var obj = tygia.data.thoi_tiet[location];
    if (typeof(obj) == 'undefined'){
        return false;
    }

    //var box = $("#boxweather");
    // push name tu city box
    //box.find("#img-Do").empty(); //empty box image
    var cityName ;
    cityName = obj.city_name;
    if(cityName=='T.P Hồ Chí Minh'){cityName='TP HCM';}
    if(cityName=='T.P Hà Nội'){cityName='Hà Nội';}
    //box.find("#txt-City").text(cityName);
    //box.find("#txt-Weather").text(obj.weather);
    //box.find("#img-Do").prepend('<img src="'+img_url + '/weather/' +obj.weather_code + '.gif">');
    
    // push temp number
    var temp = obj.temp.split("");
    var sHTML = '';
	sHTML = sHTML.concat('<img src="'+img_url + '/weather/' +obj.weather_code + '.gif" alt="" />&nbsp;');
	
	// push temp number
    var temp = obj.temp.split("");
    $.each(temp, function(index, value) {
        sHTML = sHTML.concat('<img src="'+img_url + '/weather/' +value + '.gif" alt="" />');
    });
    sHTML = sHTML.concat('<img src="'+img_url + '/weather/c.gif" alt="" />');
	
	sHTML = sHTML.concat('<img src="http://vnexpress.net/Images/Weather/c.gif" class="img-weather" alt="" />');
	
	gmobj('img-Do').innerHTML = sHTML;
	gmobj('txt-Weather').innerHTML = obj.weather;
}

function gmobj(o){
	if(document.getElementById){ m=document.getElementById(o); }
	else if(document.all){ m=document.all[o]; }
	else if(document.layers){ m=document[o]; }
	return m;
}
</script>

<div class="pos_MOD">
    <div class="bg_title_mod">
        <%=ParentWP.NeedToKnowTitle %></div>
    <div class="inner_pos_Mod">
        <div class="wheather">
            <div class="area">
                <select class="txt_s" style="width: 190px;" onchange="GetWeatherBox(this.value);">
                    <option value="son_la">Sơn La</option>
                    <option value="hai_phong">Hải Phòng</option>
                    <option value="ha_noi" selected="selected">Hà Nội</option>
                    <option value="vinh">Vinh</option>
                    <option value="da_nang">Ðà Nẵng</option>
                    <option value="nha_trang">Nha Trang</option>
                    <option value="pleiku">Pleiku</option>
                    <option value="tp_hcm">TP HCM</option>
                </select>
            </div>
            <div class="info_wheather">
                <p id="img-Do">
                </p>
                <p id="txt-Weather">
                </p>
            </div>
            <div class="gold_rate">
                <%=ParentWP.RateTitle %>
            </div>
            <div>

                <script type="text/javascript" language="javascript" src="http://vnexpress.net/Service/Forex_Content.js"></script>

                <script type="text/javascript" language="JavaScript" src="http://vnexpress.net/Service/Gold_Content.js"></script>

                <table id="tbl-goldprice" width="100%" style="margin: 0;" cellpadding="4" cellspacing="0" class="tbl-tygia">
                    <tr>
                        <th align='left'>
                            <%=ParentWP.GoldTitle %>
                        </th>
                        <th>
                            <%=ParentWP.BuyTitle %>
                        </th>
                        <th>
                            <%=ParentWP.SaleTitle %>
                        </th>
                    </tr>
                </table>
                <table width="100%" style="margin: 0;" cellpadding="4" cellspacing="0" class="tbl-tygia">
                    <tr>
                        <th>
                            <%=ParentWP.CurrencyTitle %>
                        </th>
                        <th>
                            <%=ParentWP.BuyTitle %>
                        </th>
                        <th>
                            <%=ParentWP.SaleTitle %>
                        </th>
                    </tr>
                    <asp:Repeater ID="rptTiGia" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("CurrencyCode") %>
                                </td>
                                <td>
                                    <%#Eval("Transfer")%>
                                </td>
                                <td>
                                    <%#Eval("Sell")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="ball">
                <asp:LinkButton ID="lbBD" runat="server"><%=ParentWP.FootballTitle %></asp:LinkButton>
            </div>
            <div class="resul">
                <asp:LinkButton ID="lbKQXS" runat="server"><%=ParentWP.LotoTitle %></asp:LinkButton>
            </div>
        </div>
    </div>
</div>
