<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeVideoRightUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.HomeVideoRightUS" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <div class="video_R">
                    <a href="javascript:void(0);">Video Clip</a></div>
            </div>
        </div>
        <div class="content_F_Right">
            <div id='qn-video-div'>
                <embed
                  flashvars="file=<%=VideoUrl%>&image=<%=ImageUrl%>&autostart=false"
                  allowfullscreen="true"
                  allowscripaccess="always"
                  id="qn-video-div-player"
                  name="qn-video-div-player"
                  src="/QNResources/player.swf"
                  width="285"
                />
           </div>
           <asp:Repeater ID="rptTVLink" runat="server" OnItemDataBound="rptTVLink_OnItemDataBound">
                <HeaderTemplate><div class="list_video"><ul></HeaderTemplate>
                <ItemTemplate><li><a runat="server" id="aLink"><%#Eval("Title") %></a></li></ItemTemplate>
				<FooterTemplate></ul></div></FooterTemplate>
			</asp:Repeater>
        </div>
    </div>
</div>
<script type="text/javascript">
    function setVideoPlay(strID, value){
        document.getElementById("qn-video-div").innerHTML = value;
    }
</script>
