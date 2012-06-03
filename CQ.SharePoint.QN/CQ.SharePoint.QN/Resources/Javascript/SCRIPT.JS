// JavaScript Document
window.onload=hotrolaptrinh_submenufunction;
function hotrolaptrinh_submenufunction(obj) {
    var HTLT = document.getElementById(obj);
    for (var i = 1; i<=4; i++) {
        if (document.getElementById('sub'+i)) {
            document.getElementById('sub'+i).style.display='none';
        }
    }
    if (HTLT) {
        HTLT.style.display='block';
    }
}
function ws_basic(c,a,b){this.go=function(d){b.find("ul").stop(true).animate({left:(d?-d+"00%":0)},c.duration,"easeInOutExpo");return d}};
jQuery("#wowslider-container1").wowSlider({effect:"basic",prev:"prev",next:"next",duration:20*100,delay:80*100,width:402,height:317,autoPlay:true,stopOnHover:false,loop:false,bullets:true,caption:true,controls:false,logo:"engine1/loading.gif",images:0});