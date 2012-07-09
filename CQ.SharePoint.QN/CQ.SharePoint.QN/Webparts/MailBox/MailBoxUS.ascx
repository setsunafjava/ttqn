<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailBoxUS.ascx.cs" Inherits="CQ.SharePoint.QN.Webparts.MailBoxUS" %>
<div class="pos_MOD">
    <div class="bg_title_mod">
        <div class="mailbox">
            <a href='<%= WebpartParent.WebMailPath %>'>
                <%= WebpartParent.MailBoxTitle %></a></div>
    </div>
</div>
