<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPageInner2.master" AutoEventWireup="false" CodeFile="memberinfo.aspx.vb" Inherits="memberinfo" %>
<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        th, td { vertical-align:top; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div id="fb-root"></div>
      <script>
        var login = false;
        window.fbAsyncInit = function() {
          FB.init({
            appId      : '<%=ConfigurationManager.AppSettings("FacebookAppID") %>',
            status     : true, 
            cookie     : true,
            xfbml      : true,
            oauth      : true,
          });
          FB.Event.subscribe('auth.login', 
            function(response) {
                window.location.href = window.location.href;
            }
          );
        };

        (function(d){
           var js, id = 'facebook-jssdk'; if (d.getElementById(id)) {return;}
           js = d.createElement('script'); js.id = id; js.async = true;
           js.src = "//connect.facebook.net/en_US/all.js";
           d.getElementsByTagName('head')[0].appendChild(js);
         }(document));
         
      </script>


    <div >
	<%--<asp:Label ID="lblRegHeading" runat="server" Text="會員註冊" ></asp:Label>--%>


<div class="section-title">連結</div>

<%--	<div class="registration-form">
		<asp:Localize ID="Localize1" runat="server" Text=""></asp:Localize>
	</div>--%>
	<table border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td colspan="3" height="20"></td>
		</tr>
		<tr>
		    <td width="70"></td>
		    <td width="150">Facebook連結
		    </td>
		    <td>
		    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="true">
		        <ContentTemplate>
		        
    <asp:Panel runat="server" ID="pnlFacebook" CssClass="fb-panel">
 
        <asp:Image runat="server" ID="imgFacebook"  />
        <div>
            <asp:Label runat="server" ID="lblFacebook"></asp:Label>
            <asp:Label runat="server" ID="lblFBLinkStatus"></asp:Label>
        </div>
        <div><asp:Button runat="server" ID="btnLinkFB" Text="連結Facebook" CausesValidation="false" />
        <asp:button runat="server" ID="btnUnlinkFB" Text="不連結Facebook" CausesValidation="false" />
        </div>
        <div>
        <asp:Label runat="server" ID="lblFBMessage"></asp:Label>
        </div>
        <asp:HiddenField runat="server" ID="hfdFacebookID" />
        
    </asp:Panel>
    
    <asp:Panel runat="server" ID="pnlFacebookLogin" CssClass="fb-panel">
        <div class="fb-login-button" data-scope="email,user_birthday,publish_stream" >Login with Facebook</div>
        
    </asp:Panel>
		        </ContentTemplate>
		    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" TargetControlID="UpdateProgress1" CssClass="updateProgress" ControlToOverlayID="UpdatePanel1" />
    
		    </td>
		</tr>
	</table>

<div class="section-title">個人資料</div>

<%--	<div class="registration-form">
		<asp:Localize ID="Localize4" runat="server" Text=""></asp:Localize>
	</div>--%>
	<asp:Label ID="Label1" runat="server" Text="(* 必須填寫)" ></asp:Label>

	<table border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td colspan="3" height="20"></td>
		</tr>
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="Localize5" runat="server" Text="電郵*"></asp:Localize>
			</td>
			<td valign="top">
				<asp:TextBox ID="txtEmail" runat="server" Height="25px" Width="300px"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvalEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"><br />電郵 必須填寫</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
					ControlToValidate="txtEmail" Display="Dynamic"><br />請填寫有效的電郵</asp:RegularExpressionValidator>
				<asp:CustomValidator ID="custvalEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"><br />電郵 不可重覆登記</asp:CustomValidator>
				<br />
				<asp:Label ID="Label2" runat="server" Text="(作確認信之用，請小心填寫)" ForeColor="#999999" Visible="false"></asp:Label>
			</td>
		</tr>
		<%--<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="Localize6" runat="server" Text="重複電郵﹡"></asp:Localize>
			</td>
			<td valign="top">
				<asp:TextBox ID="txtEmail2" runat="server" Height="25px" Width="300px"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvalEmail2" runat="server" ErrorMessage="重複電郵 必須填寫" ControlToValidate="txtEmail2" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:CompareValidator ID="compavalEmail" runat="server" ErrorMessage="重複電郵必須等於電郵" ControlToValidate="txtEmail2" ControlToCompare="txtEmail" Display="Dynamic"></asp:CompareValidator>
			</td>
		</tr>--%>
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="Localize16" runat="server" Text="名稱*"></asp:Localize>
			</td>
			<td valign="top">
				<asp:TextBox ID="txtName" runat="server" Height="25px" Width="300px"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvaltxtName" runat="server" ControlToValidate="txtName" Display="Dynamic"><br />名稱 必須填寫</asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="Localize7" runat="server" Text="性別"></asp:Localize>
			</td>
			<td valign="top">
				<asp:RadioButtonList ID="radGender" runat="server" RepeatDirection="Horizontal" Width="150px">
					<asp:ListItem Value="M" Text="男"></asp:ListItem>
					<asp:ListItem Value="F" Text="女"></asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="Localize9" runat="server" Text="出生日期"></asp:Localize>
			</td>
			<td valign="top">
				<asp:DropDownList runat="server" ID="ddlYear"></asp:DropDownList>年
				<asp:DropDownList runat="server" ID="ddlMonth"></asp:DropDownList>月
				<asp:DropDownList runat="server" ID="ddlDay"></asp:DropDownList>日
                <asp:CustomValidator ID="valBirthday" runat="server" ControlToValidate="ddlDay" ErrorMessage="出生日期不正確"></asp:CustomValidator>
			</td>
		</tr>
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="Localize6" runat="server" Text="聯絡電話"></asp:Localize>
			</td>
			<td valign="top">
				<asp:TextBox ID="txtContactNo" runat="server" Height="25px" Width="300px"></asp:TextBox>
				<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="名稱 必須填寫" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>--%>
			</td>
		</tr>
		<asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false">
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="Localize8" runat="server" Text="地址"></asp:Localize>
			</td>
			<td valign="top" height="100">
				<asp:TextBox ID="txtDeliveryAddress" runat="server" Width="300px" TextMode="MultiLine" Rows="5"></asp:TextBox>
				<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="名稱 必須填寫" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>--%>
			</td>
		</tr>


		<%--<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="Localize9" runat="server" Text="語言"></asp:Localize>
			</td>
			<td valign="top">
				<asp:RadioButtonList ID="radLang" runat="server" RepeatDirection="Horizontal" Width="300px">
					<asp:ListItem Value="zh-hk" Text="繁體中文"></asp:ListItem>
					<asp:ListItem Value="zh-cn" Text="簡體中文"></asp:ListItem>
					<asp:ListItem Value="en-us" Text="English"></asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>--%>
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="locRegion" runat="server" Text="國家"></asp:Localize>
			</td>
			<td valign="top" height="100">
				<asp:RadioButtonList ID="radRegion" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="Local" Text="香港"></asp:ListItem>
					<asp:ListItem Value="Overseas" Text="海外"></asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		</asp:PlaceHolder>

		<tr>
			<td width="70" valign="top" ></td>
			<td width="150" valign="top">
			</td>
			<td valign="top" >
				<asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
			</td>
			<td valign="top" style="display: inline">
				<asp:Button ID="btnSubmit" runat="server" Text="更新" CssClass="submit-button" />
			</td>
		</tr>
	</table>
</div>





</asp:Content>

