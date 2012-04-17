<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NeedToKnowUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.NeedToKnowUS" %>
    
    <script type="text/javascript">
   
   /**
 * jQuery.ajax mid - CROSS DOMAIN AJAX 
 * ---
 * @author James Padolsey (http://james.padolsey.com)
 * @version 0.11
 * @updated 12-JAN-10
 * ---
 * Note: Read the README!
 * ---
 * @info http://james.padolsey.com/javascript/cross-domain-requests-with-jquery/
 */

jQuery.ajax = (function(_ajax){
    
    var protocol = location.protocol,
        hostname = location.hostname,
        exRegex = RegExp(protocol + '//' + hostname),
        YQL = 'http' + (/^https/.test(protocol)?'s':'') + '://query.yahooapis.com/v1/public/yql?callback=?',
        query = 'select * from html where url="{URL}" and xpath="*"';
    
    function isExternal(url) {
        return !exRegex.test(url) && /:\/\//.test(url);
    }
    
    return function(o) {
        
        var url = o.url;
        
        if ( /get/i.test(o.type) && !/json/i.test(o.dataType) && isExternal(url) ) {
            
            // Manipulate options so that JSONP-x request is made to YQL
            
            o.url = YQL;
            o.dataType = 'json';
            
            o.data = {
                q: query.replace(
                    '{URL}',
                    url + (o.data ?
                        (/\?/.test(url) ? '&' : '?') + jQuery.param(o.data)
                    : '')
                ),
                format: 'xml'
            };
            
            // Since it's a JSONP request
            // complete === success
            if (!o.success && o.complete) {
                o.success = o.complete;
                delete o.complete;
            }
            
            o.success = (function(_success){
                return function(data) {
                    
                    if (_success) {
                        // Fake XHR callback.
                        _success.call(this, {
                            responseText: data.results[0]
                                // YQL screws with <script>s
                                // Get rid of them
                                .replace(/<script[^>]+?\/>|<script(.|\s)*?\/script>/gi, '')
                        }, 'success');
                    }
                    
                };
            })(o.success);
            
        }
        
        return _ajax.apply(this, arguments);
        
    };
    
})(jQuery.ajax);
   
    
    function ShowWeatherBox(vId){
    
	var sLink = '';
	sLink = 'http://vnexpress.net/ListFile/Weather/';
	switch (parseInt(vId)){	    	
		case 1: sLink = sLink.concat('Sonla.xml');break;
		case 2: sLink = sLink.concat('Viettri.xml');break;
		case 3: sLink = sLink.concat('Haiphong.xml');break;
		case 4: sLink = sLink.concat('Hanoi.xml');break;
		case 5: sLink = sLink.concat('Vinh.xml');break;
		case 6: sLink = sLink.concat('Danang.xml');break;
		case 7: sLink = sLink.concat('Nhatrang.xml');break;
		case 8: sLink = sLink.concat('Pleicu.xml');break;		
		case 9: sLink = sLink.concat('HCM.xml');break;	
		default: sLink = sLink.concat('Hanoi.xml');break;
	}
//	alert(sLink);
	$.ajax({
        type: "GET",
        url: sLink,
        success: function(res) {
        var headline = $(res.responseText).text();
        parseXml(headline);
            }
      });


//jQuery.getJSON(sLink + "callback=?", function(data) {
//    alert("Symbol: " + data);
//});

}

function parseXml(xmlDoc)
{
var xml = $.parseXML( xmlDoc )
alert($(xml).find("AdImg").text());
  var vAdImg, vAdImg1, vAdImg2, vAdImg3, vAdImg4, vAdImg5, vWeather;
				vAdImg = $(xml).find("AdImg").text();
				vAdImg1 = $(xml).find("AdImg1").text();
				if($(xml).find("AdImg2") != null)
					vAdImg2 = $(xml).find("AdImg2").text();
				if($(xml).find("AdImg3") != null)
					vAdImg3 = $(xml).find("AdImg3").text();
				if($(xml).find("AdImg4") != null)
					vAdImg4 = $(xml).find("AdImg4").text();
				if($(xml).find("AdImg5") != null)
					vAdImg5 = $(xml).find("AdImg5").text();
				vWeather = $(xml).find("Weather").text();
				vWeather = vWeather.replace(/<br>/g,"&nbsp;.&nbsp;&nbsp;");
				GetWeatherBox(vAdImg, vAdImg1, vAdImg2, vAdImg3, vAdImg4, vAdImg5, vWeather);
}

function GetWeatherBox(vImg, vImg1, vImg2, vImg3, vImg4, vImg5, vWeather){
	var sHTML = '';
	sHTML = sHTML.concat('<img src="http://vnexpress.net/Images/Weather/').concat(vImg).concat('" class="img-weather" alt="" />&nbsp;');
	sHTML = sHTML.concat('<img src="http://vnexpress.net/Images/Weather/').concat(vImg1).concat('" class="img-weather" alt="" />');
	if(vImg2!=null) sHTML = sHTML.concat('<img src="http://vnexpress.net/Images/Weather/').concat(vImg2).concat('" class="img-weather" alt="" />');
	if(vImg3!=null) sHTML = sHTML.concat('<img src="http://vnexpress.net/Images/Weather/').concat(vImg3).concat('" class="img-weather" alt="" />');
	if(vImg4!=null) sHTML = sHTML.concat('<img src="http://vnexpress.net/Images/Weather/').concat(vImg4).concat('" class="img-weather" alt="" />');
	if(vImg5!=null) sHTML = sHTML.concat('<img src="http://vnexpress.net/Images/Weather/').concat(vImg5).concat('" class="img-weather" alt="" />');
	sHTML = sHTML.concat('<img src="http://vnexpress.net/Images/Weather/c.gif" class="img-weather" alt="" />');
	
	gmobj('img-Do').innerHTML = sHTML;
	gmobj('txt-Weather').innerHTML = vWeather;
}
    </script>
    
<div class="pos_MOD">
    <div class="bg_title_mod">
        Thông tin cần biết</div>
    <div class="inner_pos_Mod">
        <div class="wheather">
            <div class="area">
                <img style="float:left" src="http://vnexpress.net/Images/search.gif" alt="">
                <select class="txt_s" style="width: 190px;" onchange="ShowWeatherBox(this.value);">
                    <option value="1">Sơn La</option>
			        <option value="2">Việt Trì</option>
			        <option value="3">Hải Phòng</option>
			        <option value="4" selected="selected">Hà Nội</option>
			        <option value="5">Vinh</option>
			        <option value="6">Ðà Nẵng</option>
			        <option value="7">Nha Trang</option>
			        <option value="8">Pleiku</option>
			        <option value="9">TP HCM</option>
                </select>
            </div>
            <div class="info_wheather">
                <p id="img-Do"></p>
                <p id="txt-Weather"></p>
                <script type="text/javascript" language="javascript">ShowWeatherBox(4);</script>
            </div>
            <div class="gold_rate">
                Tỷ Giá
            </div>
            <div>
                <img src="images/info_rate.jpg" /></div>
            <div class="ball">
                <a href="#">Bóng đá</a>
            </div>
            <div class="resul">
                <a href="#">Kết quả Xổ Số</a>
            </div>
        </div>
    </div>
</div>
