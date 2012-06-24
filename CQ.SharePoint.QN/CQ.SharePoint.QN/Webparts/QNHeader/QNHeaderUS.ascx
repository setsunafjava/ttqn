<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QNHeaderUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.QNHeaderUS" %>
<script type="text/javascript">
function setHomepage() 
{ 
 if (document.all) 
    { 
        document.body.style.behavior='url(#default#homepage)'; 
        document.body.setHomePage(location.href); 
 

    } 
    else if (window.sidebar) 
    { 
    if(window.netscape) 
    { 
         try 
   { 
            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect"); 
         } 
         catch(e) 
         { 
    alert("this action was aviod by your browser，if you want to enable，please enter about:config in your address line,and change the value of signed.applets.codebase_principal_support to true");

         } 
    } 
    var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components. interfaces.nsIPrefBranch);

    prefs.setCharPref('browser.startup.homepage',location.href);

 } 
} 
</script>
<div id="header">
	<div class="banner">
		<object width="100%" height="130px" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
			codebase="http://fpdownload.macromedia.com/
			pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" alt='Trung tâm thông tin công nghệ tỉnh Quảng Ninh' 
	title='Trung tâm thông tin công nghệ tỉnh Quảng Ninh'>
			<param name="movie" value="/QNResources/banerNew.swf" />
			<embed src="/QNResources/banerNew.swf" width="100%" height="130px" alt='Trung tâm thông tin công nghệ tỉnh Quảng Ninh' 
	title='Trung tâm thông tin công nghệ tỉnh Quảng Ninh'>
			</embed>
		</object>
	</div>
	<div class="top_menu">
		<div class="menu">
		    <ul id="nav">
				<li <%=CurrentStyle%>><a href="/"><%= ParentWP.HomePageTitle %></a></li>
                <asp:Repeater ID="rptMenu" runat="server" OnItemDataBound="rptMenu_OnItemDataBound">
                    <ItemTemplate>
                        <li <asp:Literal ID="ltrStyle" runat="server"></asp:Literal>><a href='<%#Eval("Url") %>'><%#Eval("Title") %></a>
                            <asp:Repeater ID="rptSubMenu" runat="server">
                                <HeaderTemplate><ul><li style="list-style:none;display:inline;float:left;margin-top:-1px;margin-right:2px;"><%= ParentWP.TodayIsTitle %><%=DateTime.Now %></li></HeaderTemplate>
                                <ItemTemplate>
                                    <li><a href='<%#Eval("Url") %>'><%#Eval("Title") %></a></li>
                                </ItemTemplate>
                                <FooterTemplate></ul></FooterTemplate>
                            </asp:Repeater>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
		</div>
		<div class="search">
			<input type="text" id="txtData" name="q" onkeypress="return BBEnterPress();" style="border: 0px;"> <a href="javascript:void(0)" onclick="javascript:timkiem();"><%= ParentWP.SearchTitle %></a>
		</div>
		<div class="language">
			<span><a href="/<%=LangUrl%>"><img src="/QNResources/<%=LangImg%>" /></a></span><span><a href="/<%=LangUrl%>"><%=LangTitle%></a></span>
		</div>
		<div class="cleaner"></div>
				
	</div>
	<div class="cleaner"></div>
	<div class="bg_bottom_top_menu">
		<div class="inner_content_bottom_topMenu">
			<div class="time_date"><%= ParentWP.TodayIsTitle %><%=DateTime.Now %></div>
			<div class="set_hompage"><a href="javascript:void(0)" onclick="javascript:setHomepage();"><%= ParentWP.SetAsHomePageTitle %></a></div>
			<div class="RSS"><%--<asp:LinkButton ID="lbRSS" runat="server" OnClick="lbRSS_OnClick">RSS</asp:LinkButton>--%><a href="/RSS.aspx?CategoryId=<%=CategoryId%>" target="_blank">RSS</a></div>
			<div class="cleaner"></div>
		</div>
	</div>
</div>

<script type="text/javascript">
    $('#nav > li').hover(onOver, onOut);
	function onOver() {
        $('#nav > li.current').each(function(index) {
            $(this).removeClass('current').addClass('current-temp');
        });
	};

	function onOut() {
		$('#nav > li.current-temp').each(function(index) {
            $(this).removeClass('current-temp').addClass('current');
        });
	};
	
	function urlencode( str ) {

      

      var ret = str;



      ret = ret.toString();



      ret = encodeURIComponent(ret);



      ret = ret.replace(/%20/g, '+');

      

      ret = ret.replace(/%22/g, "");

      ret = ret.replace(/\'/g, "");

      ret = ret.replace(/%2F/g, "");

      ret = ret.replace(/%3C/g, "");

      ret = ret.replace(/%3E/g, "");

      ret = ret.replace(/%3F/g, "");

      ret = ret.replace(/%25/g, "");

      ret = ret.replace(/\*/g, "");

      ret = ret.replace(/%7C/g, "");



      return ret;

      }

      

    function timkiem()

    {

        var link;

        var tk = document.getElementById("txtData").value;

        if(tk==""){

            link = "TimKiem.aspx?KeyWord=" + urlencode(tk);
        }

        else{

            link = "TimKiem.aspx?KeyWord=" + urlencode(tk);
        }

        //alert(link);

        location.href=link;

    }
    
    function ganValue(t){

        document.getElementById("txtData").value = t;

    }
</script>