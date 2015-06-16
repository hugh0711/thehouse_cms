<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="register" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
	td, th { vertical-align:top; text-align:left; }
	
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="section-title">會員登記</div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
        
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
          /*FB.login(function (response) {
            alert("hi");
            /*
            if (response.authResponse) {
                var access_token = response.authResponse.accessToken;
                alert(access_token);
            } else {
                alert('User is logged out');
            }
            
        });*/
          FB.Event.subscribe('auth.login', 
            function(response) {
                //alert("hi");
                //__doPostBack('btnCheckLogin', '');
                //alert("login");
                /*
                var url = window.location;
                if (url.indexOf("?") < 0) {
                    url = url + "?";
                }
                else {
                    url = url + "&";
                };
                url = url + "facebook=true";
                */
                //alert("logged in: " );
                //window.location = "register.aspx";
                //__doPostBack('btnFBLogin', '');
            }
          );
        };
        
        var getAccessToken = function () {
            var accessToken = null;
            var session = FB.getSession();

            if (session != null) {
                accessToken = session.access_token;
            }

            return accessToken;
        };

        (function(d){
           var js, id = 'facebook-jssdk'; if (d.getElementById(id)) {return;}
           js = d.createElement('script'); js.id = id; js.async = true;
           js.src = "//connect.facebook.net/en_US/all.js";
           d.getElementsByTagName('head')[0].appendChild(js);
         }(document));
         
      </script>
	

    <div style="height:30px;">&nbsp;</div>

    <%--<div style="border:Solid 1px #cbcdca; padding:10px; width:868px;">--%>
	<%--<asp:Label ID="lblRegHeading" runat="server" Text="會員註冊" ></asp:Label>--%>
	<div style="margin: 0px; clear: both; float: left; color: #000000; font-size: 14px; padding-top: 10px;">
		<asp:Label ID="lblRegMsg1" runat="server" Text="你只需要填寫以下個人資料，便能成為 "></asp:Label>
		<span style="color: #34a3db"><asp:Label ID="lblRegMsg2" runat="server" Text="MajiTV"></asp:Label></span>
		<asp:Label ID="lblRegMsg3" runat="server" Text="會員！費用全免！"></asp:Label>
		<br />
	</div>

    <div class="section-title">登入資料</div>

		<asp:Label ID="lblRegMsg4" runat="server" Text="(* 為必須填寫的資料)" ></asp:Label>

	<table border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td colspan="3" height="20"></td>
		</tr>
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
				<asp:Localize ID="Localize1" runat="server" Text="登入ID*"></asp:Localize>
			</td>
			<td valign="top">
				<asp:TextBox ID="txtLoginID" runat="server" Height="25px" Width="300px"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidatorLogin" runat="server" ErrorMessage="登入ID 必須填寫" ControlToValidate="txtLoginID" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:CustomValidator ID="custvalLoginID" runat="server" ErrorMessage="" Display="Dynamic"></asp:CustomValidator>
				<br />
				<asp:Label ID="lblLoginID" runat="server" Text="(最小 5 個字)" ForeColor="#999999"></asp:Label>
				<asp:HiddenField runat="server" ID="hfdFacebookIUserD"></asp:HiddenField>
			</td>
		</tr>
		<tr>
			<td valign="top" height="60"></td>
			<td valign="top">
				<asp:Localize ID="Localize2" runat="server" Text="密碼*"></asp:Localize>
				
			</td>
			<td valign="top">
				<asp:TextBox ID="txtPassword" runat="server" Height="25px" Width="300px" TextMode="Password"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvalPassword" runat="server" ErrorMessage="密碼 必須填寫" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:CustomValidator ID="custvalPassword" runat="server" ErrorMessage="CustomValidator" Display="Dynamic"></asp:CustomValidator>
				<br />
				<asp:Label ID="Label1" runat="server" Text="(最小 6 - 20 個字)" ForeColor="#999999"></asp:Label>
			</td>
		</tr>
		<tr>
			<td valign="top" height="60"></td>
			<td valign="top">
				<asp:Localize ID="Localize3" runat="server" Text="重複輸入密碼*"></asp:Localize>
			</td>
			<td valign="top">
				<asp:TextBox ID="txtPassword2" runat="server" Height="25px" Width="300px" TextMode="Password"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvalPassword2" runat="server" ErrorMessage="密碼 必須填寫" ControlToValidate="txtPassword2" Display="Dynamic"></asp:RequiredFieldValidator>
			</td>
		</tr>
	</table>

    <div class="section-title">連結</div>

    <table border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td colspan="3" height="20"></td>
		</tr>
		<tr>
		    <td width="70"></td>
		    <td width="150">Facebook連結
		    </td>
		    <td>
		    
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
        <asp:Button runat="server" ID="btnFBLogin" Text="Login with Facebook" style="display:none;" />
    </asp:Panel>
            </td>
        </tr>
    </table>

    <div class="section-title">個人資料</div>

		<asp:Label ID="Label3" runat="server" Text="(* 為必須填寫的資料)" ></asp:Label>

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
				<asp:RequiredFieldValidator ID="reqvalEmail" runat="server" ErrorMessage="電郵 必須填寫" ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
					ControlToValidate="txtEmail" ErrorMessage="請填寫有效的電郵" Display="Dynamic"></asp:RegularExpressionValidator>
				<asp:CustomValidator ID="custvalEmail" runat="server" ErrorMessage="電郵 不可重覆登記" ControlToValidate="txtEmail" Display="Dynamic"></asp:CustomValidator>
				<br />
				<asp:Label ID="Label2" runat="server" Text="(作確認信之用，請小心填寫)" ForeColor="#999999"></asp:Label>
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
				<asp:RequiredFieldValidator ID="reqvaltxtName" runat="server" ErrorMessage="名稱 必須填寫" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
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
		<asp:PlaceHolder runat="server" Visible="false">
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
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
			</td>
			<td valign="top">
				<asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" />
			</td>
		</tr>
		<tr>
			<td width="70" valign="top" height="60"></td>
			<td width="150" valign="top">
			</td>
			<td valign="top" style="display: inline">
				<asp:Button ID="btnSubmit" runat="server" BorderStyle="Solid" Width="100px" Text="登記"
					BorderColor="#D5D5D5" BorderWidth="2px" BackColor="#BFBFBF" ForeColor="#666" style="padding:5px" />
				&nbsp;
				<asp:Button ID="btnCancel" runat="server"  CausesValidation="False" 
                    BorderStyle="Solid" Width="100px" Text="取消"
					BorderColor="#D5D5D5" BorderWidth="2px" BackColor="#BFBFBF" ForeColor="White" 
                    style="padding:5px" Visible="False"/>
			</td>
		</tr>
	</table>



    
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" TargetControlID="UpdateProgress1" CssClass="updateProgress" ControlToOverlayID="UpdatePanel1" />
    
</asp:Content>

