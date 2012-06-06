<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FactTVUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.FactTVUS" %>
<div class="pos_MOD">
    <div class="bg_title_mod">
        Truyền hình internet</div>
    <div class="inner_pos_Mod">
        <div class="video">
            <asp:Repeater ID="rptTV" runat="server">
                <ItemTemplate>
                   <div id='qn-tv-div'>
                        <%#Eval("Value")%>
                   </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="channel">
            <div class="arrow_circle_next">
                <a href="#">
                    <img src="images/arrow_next_circle.jpg" /></a></div>
            <div class="inner_channel">
                <asp:Repeater ID="rptTVLink" runat="server" OnItemDataBound="rptTVLink_OnItemDataBound">
                <ItemTemplate>
                   <a runat="server" id="aLink"><img runat="server" id="imgLink" /></a>
                </ItemTemplate>
            </asp:Repeater>
            </div>
            <div class="arrow_circle_back">
                <a href="#">
                    <img src="images/arrow_back_circle.jpg" /></a></div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function setTVPlay(strID, value){
        document.getElementById("qn-tv-div").innerHTML = value;
    }
</script>