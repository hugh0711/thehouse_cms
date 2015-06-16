<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="false" CodeFile="registerFB.aspx.vb" Inherits="registerFB" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
	td, th { vertical-align:top; text-align:left; }
	
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="section-title">會員登記</div>


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
                window.location = "register.aspx?link=facebook";
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
        <div class="fb-login-button" data-scope="email,user_birthday,publish_stream" >以Facebook登記</div>
        <asp:Button runat="server" ID="btnFBLogin" Text="Login with Facebook" style="display:none;" />
    </asp:Panel>
            </td>
        </tr>
    </table>


    

</asp:Content>

