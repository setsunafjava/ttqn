<%@ Assembly Name="CQ.SharePoint.QN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FocusCompanyUS.ascx.cs"
    Inherits="CQ.SharePoint.QN.Webparts.FocusCompanyUS" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <%= WebpartParent.FocusCompanyTitle %>
            </div>
        </div>
        <%--<div class="content_F_Right">
            <div class="img_logo_company_ex">
                <asp:Repeater ID="rptFocusCompany" runat="server">
                    <ItemTemplate>
                        <a href='<%#Eval("LinkToItem")%>'>
                            <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("Thumbnail")%>' /></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>--%>
        <div class="list_carousel responsive" >
            <ul id="foo5">
                <asp:Repeater ID="rptFocusCompany" runat="server">
                    <ItemTemplate>
                    <li>
                         <a href='<%#Eval("LinkToItem")%>'><asp:Image style="width: 300px; height:160px;" ID="img" runat="server" ImageUrl='<%#Eval("Thumbnail")%>' /></a>
                    </li>                       
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>

<script type="text/javascript" language="javascript">
			$(function() {
				$('#foo5').carouFredSel({
					width: '100%',
					responsive: true,
					scroll: 1,
					items: {
						width: 300,
						height: 160,	//	optionally resize item-height
						visible: {
							min: 4,
							max: 6
						}
					},
					direction:	'up'
				});

			});
		</script>
