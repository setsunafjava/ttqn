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
        <div style=" margin:0; width: 300px; height: 400px;">
            <ul id="foo5">
                <asp:Repeater ID="rptFocusCompany" runat="server" OnItemDataBound="OnItemDataBound_FocusCompany">
                    <ItemTemplate>
                        <li><a href='<%#Eval("LinkUrl")%>'>
                            <asp:Literal ID="ltrFlash1" runat="server"></asp:Literal>
                        </a>
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
						height: 100,
						visible: {
							min: 2,
							max: 6
						}
					},
					direction:	'up'
				});

			});
		</script>

