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