<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NeedToKnowUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.NeedToKnowUS" %>
    <style type="text/css">
        .img-weather{display:inline; vertical-align:top;}
        .tbl-tygia th{text-align:left; background-color:#E7F3FF;color:#1028A5}
    </style>
    <script type="text/javascript">
 // ==================================================================
// Author: Matt Kruse <matt@ajaxtoolbox.com>
// WWW: http://www.AjaxToolbox.com/
//
// NOTICE: You may use this code for any purpose, commercial or
// private, without any further permission from the author. You may
// remove this notice from your final code if you wish, however it is
// appreciated by the author if at least my web site address is kept.
//
// You may *NOT* re-distribute this code in any way except through its
// use. That means, you can include it in your product, or your web
// site, or any other form where the code is actually being used. You
// may not put the plain javascript up on your site for download or
// include it in your javascript libraries for download. 
// If you wish to share this code with others, please just point them
// to the URL instead.
// Please DO NOT link directly to my .js files from your site. Copy
// the files to your server and use them there. Thank you.
// ==================================================================

function AjaxRequest(){var req =new Object();
req.timeout =null;
req.generateUniqueUrl =false;
req.url =window.location.href;
req.method ="GET";
req.async =true;
req.username =null;
req.password =null;
req.parameters =new Object();
req.requestIndex =AjaxRequest.numAjaxRequests++;
req.responseReceived =false;
req.groupName =null;
req.queryString ="";
req.responseText =null;
req.responseXML =null;
req.status =null;
req.statusText =null;
req.aborted =false;
req.xmlHttpRequest =null;
req.onTimeout=null;
req.onLoading=null;
req.onLoaded=null;
req.onInteractive=null;
req.onComplete=null;
req.onSuccess=null;
req.onError=null;
req.onGroupBegin=null;
req.onGroupEnd=null;
req.xmlHttpRequest =AjaxRequest.getXmlHttpRequest();
if(req.xmlHttpRequest==null){return null;}req.xmlHttpRequest.onreadystatechange =
function(){if(req==null || req.xmlHttpRequest==null){return;}if(req.xmlHttpRequest.readyState==1){req.onLoadingInternal(req);}if(req.xmlHttpRequest.readyState==2){req.onLoadedInternal(req);}if(req.xmlHttpRequest.readyState==3){req.onInteractiveInternal(req);}if(req.xmlHttpRequest.readyState==4){req.onCompleteInternal(req);}};
req.onLoadingInternalHandled=false;
req.onLoadedInternalHandled=false;
req.onInteractiveInternalHandled=false;
req.onCompleteInternalHandled=false;
req.onLoadingInternal=
function(){if(req.onLoadingInternalHandled){return;}AjaxRequest.numActiveAjaxRequests++;
if(AjaxRequest.numActiveAjaxRequests==1 && typeof(window['AjaxRequestBegin'])=="function"){AjaxRequestBegin();}if(req.groupName!=null){if(typeof(AjaxRequest.numActiveAjaxGroupRequests[req.groupName])=="undefined"){AjaxRequest.numActiveAjaxGroupRequests[req.groupName] =0;}AjaxRequest.numActiveAjaxGroupRequests[req.groupName]++;
if(AjaxRequest.numActiveAjaxGroupRequests[req.groupName]==1 && typeof(req.onGroupBegin)=="function"){req.onGroupBegin(req.groupName);}}if(typeof(req.onLoading)=="function"){req.onLoading(req);}req.onLoadingInternalHandled=true;};
req.onLoadedInternal=
function(){if(req.onLoadedInternalHandled){return;}if(typeof(req.onLoaded)=="function"){req.onLoaded(req);}req.onLoadedInternalHandled=true;};
req.onInteractiveInternal=
function(){if(req.onInteractiveInternalHandled){return;}if(typeof(req.onInteractive)=="function"){req.onInteractive(req);}req.onInteractiveInternalHandled=true;};
req.onCompleteInternal=
function(){if(req.onCompleteInternalHandled || req.aborted){return;}req.onCompleteInternalHandled=true;
AjaxRequest.numActiveAjaxRequests--;
if(AjaxRequest.numActiveAjaxRequests==0 && typeof(window['AjaxRequestEnd'])=="function"){AjaxRequestEnd(req.groupName);}if(req.groupName!=null){AjaxRequest.numActiveAjaxGroupRequests[req.groupName]--;
if(AjaxRequest.numActiveAjaxGroupRequests[req.groupName]==0 && typeof(req.onGroupEnd)=="function"){req.onGroupEnd(req.groupName);}}req.responseReceived =true;
req.status =req.xmlHttpRequest.status;
req.statusText =req.xmlHttpRequest.statusText;
req.responseText =req.xmlHttpRequest.responseText;
req.responseXML =req.xmlHttpRequest.responseXML;
if(typeof(req.onComplete)=="function"){req.onComplete(req);}if(req.xmlHttpRequest.status==200 && typeof(req.onSuccess)=="function"){req.onSuccess(req);}else if(typeof(req.onError)=="function"){req.onError(req);}delete req.xmlHttpRequest['onreadystatechange'];
req.xmlHttpRequest =null;};
req.onTimeoutInternal=
function(){if(req!=null && req.xmlHttpRequest!=null && !req.onCompleteInternalHandled){req.aborted =true;
req.xmlHttpRequest.abort();
AjaxRequest.numActiveAjaxRequests--;
if(AjaxRequest.numActiveAjaxRequests==0 && typeof(window['AjaxRequestEnd'])=="function"){AjaxRequestEnd(req.groupName);}if(req.groupName!=null){AjaxRequest.numActiveAjaxGroupRequests[req.groupName]--;
if(AjaxRequest.numActiveAjaxGroupRequests[req.groupName]==0 && typeof(req.onGroupEnd)=="function"){req.onGroupEnd(req.groupName);}}if(typeof(req.onTimeout)=="function"){req.onTimeout(req);}delete req.xmlHttpRequest['onreadystatechange'];
req.xmlHttpRequest =null;}};
req.process =
function(){if(req.xmlHttpRequest!=null){if(req.generateUniqueUrl && req.method=="GET"){req.parameters["AjaxRequestUniqueId"] =new Date().getTime() + "" + req.requestIndex;}var content =null;
for(var i in req.parameters){if(req.queryString.length>0){req.queryString +="&";}req.queryString +=encodeURIComponent(i) + "=" + encodeURIComponent(req.parameters[i]);}if(req.method=="GET"){if(req.queryString.length>0){req.url +=((req.url.indexOf("?")>-1)?"&":"?") + req.queryString;}}req.xmlHttpRequest.open(req.method,req.url,req.async,req.username,req.password);
if(req.method=="POST"){if(typeof(req.xmlHttpRequest.setRequestHeader)!="undefined"){req.xmlHttpRequest.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');}content =req.queryString;}if(req.timeout>0){setTimeout(req.onTimeoutInternal,req.timeout);}req.xmlHttpRequest.send(content);}};
req.handleArguments =
function(args){for(var i in args){if(typeof(req[i])=="undefined"){req.parameters[i] =args[i];}else{req[i] =args[i];}}};
req.getAllResponseHeaders =
function(){if(req.xmlHttpRequest!=null){if(req.responseReceived){return req.xmlHttpRequest.getAllResponseHeaders();}alert("Cannot getAllResponseHeaders because a response has not yet been received");}};
req.getResponseHeader =
function(headerName){if(req.xmlHttpRequest!=null){if(req.responseReceived){return req.xmlHttpRequest.getResponseHeader(headerName);}alert("Cannot getResponseHeader because a response has not yet been received");}};
return req;}AjaxRequest.getXmlHttpRequest =function(){if(window.XMLHttpRequest){return new XMLHttpRequest();}else if(window.ActiveXObject){/*@cc_on @*/
/*@if(@_jscript_version >=5)
try{return new ActiveXObject("Msxml2.XMLHTTP");}catch(e){try{return new ActiveXObject("Microsoft.XMLHTTP");}catch(E){return null;}}@end @*/}else{return null;}};
AjaxRequest.isActive =function(){return(AjaxRequest.numActiveAjaxRequests>0);};
AjaxRequest.get =function(args){AjaxRequest.doRequest("GET",args);};
AjaxRequest.post =function(args){AjaxRequest.doRequest("POST",args);};
AjaxRequest.doRequest =function(method,args){if(typeof(args)!="undefined" && args!=null){var myRequest =new AjaxRequest();
myRequest.method =method;
myRequest.handleArguments(args);
myRequest.process();}};
AjaxRequest.submit =function(theform, args){var myRequest =new AjaxRequest();
if(myRequest==null){return false;}var serializedForm =AjaxRequest.serializeForm(theform);
myRequest.method =theform.method.toUpperCase();
myRequest.url =theform.action;
myRequest.handleArguments(args);
myRequest.queryString =serializedForm;
myRequest.process();
return true;};
AjaxRequest.serializeForm =function(theform){var els =theform.elements;
var len =els.length;
var queryString ="";
this.addField =
function(name,value){if(queryString.length>0){queryString +="&";}queryString +=encodeURIComponent(name) + "=" + encodeURIComponent(value);};
for(var i=0;i<len;i++){var el =els[i];
if(!el.disabled){switch(el.type){case 'text': case 'password': case 'hidden': case 'textarea':
this.addField(el.name,el.value);
break;
case 'select-one':
if(el.selectedIndex>=0){this.addField(el.name,el.options[el.selectedIndex].value);}break;
case 'select-multiple':
for(var j=0;j<el.options.length;j++){if(el.options[j].selected){this.addField(el.name,el.options[j].value);}}break;
case 'checkbox': case 'radio':
if(el.checked){this.addField(el.name,el.value);}break;}}}return queryString;};
AjaxRequest.numActiveAjaxRequests =0;
AjaxRequest.numActiveAjaxGroupRequests =new Object();
AjaxRequest.numAjaxRequests =0;


   
    
    function ShowWeatherBox(vId){
	var sLink = '';
	sLink = '/QNResources/';
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
	
	AjaxRequest.get(
		{
			'url':sLink
			,'onSuccess':function(req){
				var vAdImg, vAdImg1, vAdImg2, vAdImg3, vAdImg4, vAdImg5, vWeather;
				vAdImg = req.responseXML.getElementsByTagName('AdImg').item(0).firstChild.nodeValue;
				vAdImg1 = req.responseXML.getElementsByTagName('AdImg1').item(0).firstChild.nodeValue;
				if(req.responseXML.getElementsByTagName('AdImg2').item(0).firstChild != null)
					vAdImg2 = req.responseXML.getElementsByTagName('AdImg2').item(0).firstChild.nodeValue;
				if(req.responseXML.getElementsByTagName('AdImg3').item(0).firstChild != null)
					vAdImg3 = req.responseXML.getElementsByTagName('AdImg3').item(0).firstChild.nodeValue;
				if(req.responseXML.getElementsByTagName('AdImg4').item(0).firstChild != null)
					vAdImg4 = req.responseXML.getElementsByTagName('AdImg4').item(0).firstChild.nodeValue;
				if(req.responseXML.getElementsByTagName('AdImg5').item(0).firstChild != null)
					vAdImg5 = req.responseXML.getElementsByTagName('AdImg5').item(0).firstChild.nodeValue;
				vWeather = req.responseXML.getElementsByTagName('Weather').item(0).firstChild.nodeValue;
				vWeather = vWeather.replace(/<br>/g,"&nbsp;.&nbsp;&nbsp;");
				GetWeatherBox(vAdImg, vAdImg1, vAdImg2, vAdImg3, vAdImg4, vAdImg5, vWeather);				
				}
			,'onError':function(req){}
		}
	)
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

function gmobj(o){
	if(document.getElementById){ m=document.getElementById(o); }
	else if(document.all){ m=document.all[o]; }
	else if(document.layers){ m=document[o]; }
	return m;
}
    </script>
    
<div class="pos_MOD">
    <div class="bg_title_mod">
        Thông tin cần biết</div>
    <div class="inner_pos_Mod">
        <div class="wheather">
            <div class="area">
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
                <script type="text/javascript" language="javascript" src="http://vnexpress.net/Service/Forex_Content.js"></script>
                <script type="text/javascript" language="JavaScript" src="http://vnexpress.net/Service/Gold_Content.js"></Script>
                <table width="100%" style="margin:0;" cellpadding="4" cellspacing="0" class="tbl-tygia">
                    <tr>
                        <th align='left'>Vàng</th>
                        <th>Mua</th>
                        <th>Bán</th>
                    </tr>
                    <tr>
                        <td>SJC</td>
                        <td><script type="text/javascript">document.write(vGoldSbjBuy);</script></td>
                        <td><script type="text/javascript">document.write(vGoldSbjSell);</script></td>
                    </tr>
                    <tr>
                        <th>Ngoại tệ</th>
                        <th>Mua</th>
                        <th>Bán</th>
                    </tr>
                    <asp:Repeater ID="rptTiGia" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("CurrencyCode") %></td>
                                <td><%#Eval("Transfer")%></td>
                                <td><%#Eval("Sell")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="ball">
                <a href="#">Bóng đá</a>
            </div>
            <div class="resul">
                <a href="#">Kết quả Xổ Số</a>
            </div>
        </div>
    </div>
</div>
