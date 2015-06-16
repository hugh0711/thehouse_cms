Imports Facebook
Imports Facebook.Web

Partial Class login2
    Inherits System.Web.UI.Page

    'Protected Sub LoginButton_Click(sender As Object, e As System.EventArgs)
    '    If Membership.ValidateUser(Login1.UserName, Login1.Password) Then
    '        FormsAuthentication.SetAuthCookie(Login1.UserName, Login1.RememberMeSet)
    '        'ClientScript.RegisterStartupScript(Me.GetType, "RefreshParent", "<script type=text/javascript>RefreshParent();</script>", False)
    '        Dim Url As String = Request("ReturnUrl")
    '        If Url = "" Then Url = "~/Portfolio.aspx"
    '        Response.Redirect(Url)
    '    End If
    'End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        '    'LoadFacebookInfo()

        'Else
        '    ClientScript.RegisterStartupScript(Me.GetType, "RefreshParent", "<script type=text/javascript>RefreshParent();</script>", False)
        '    'CheckLogin()

        'End If

    End Sub


    'Protected Sub CheckLogin()
    '    Dim FBContext As New FacebookWebContext()

    '    If FBContext.IsAuthenticated Then
    '        Dim FBClient As New FacebookWebClient()
    '        Dim result = CType(FBClient.Get("me?fields=id"), Facebook.JsonObject)
    '        Dim ID As String = result("id")
    '        Dim Username As String = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()).GetUserID(ID)

    '        If String.IsNullOrEmpty(Username) Then
    '            Response.Redirect("~/register.aspx")
    '        End If

    '        Dim User As MembershipUser = Membership.GetUser(Username)
    '        If User Is Nothing Then
    '            ' Create User Profile for this app
    '            Response.Redirect("~/register.aspx")
    '        Else
    '            'FormsAuthentication.RedirectFromLoginPage(Username, False)
    '            FormsAuthentication.SetAuthCookie(Username, False)
    '            'CType(LoginView1.FindControl("fbLogin"), Panel).Visible = False
    '            'Response.Redirect(ViewState("referrer"))
    '            Response.Redirect("~/default.aspx")
    '        End If

    '    End If

    'End Sub

    'Protected Sub LoadFacebookInfo()
    '    'Dim FBClient As New FacebookWebClient()
    '    Dim FBContext As FacebookWebContext
    '    FBContext = New FacebookWebContext()

    '    If FBContext.IsAuthenticated Then
    '        Dim FBClient As New FacebookWebClient()
    '        Dim result = CType(FBClient.Get("me?fields=id,name,link"), Facebook.JsonObject)
    '        Dim ID As String = result("id")
    '        hfdFacebookID.Value = result("id")
    '        imgFacebook.ImageUrl = String.Format("http://graph.facebook.com/{0}/picture?type=square", result("id"))
    '        lblFacebook.Text = result("name")

    '        Dim Username As String = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()).GetUserID(ID)
    '        If String.IsNullOrEmpty(Username) Then
    '            lblFBLinkStatus.Text = "(沒有連結)"
    '            btnFBLogin.Visible = False
    '        Else
    '            lblFBLinkStatus.Text = "" '"(已經連結到另一個會員)"
    '            btnFBRegister.Visible = False
    '        End If
    '        pnlFacebook.Visible = True
    '        pnlFacebookLogin.Visible = False
    '    Else
    '        pnlFacebook.Visible = False
    '        pnlFacebookLogin.Visible = True
    '    End If

    'End Sub


    'Protected Sub btnFBRegister_Click(sender As Object, e As System.EventArgs) Handles btnFBRegister.Click
    '    Response.Redirect("~/register.aspx?link=facebook")
    'End Sub

    'Protected Sub btnFBLogin_Click(sender As Object, e As System.EventArgs) Handles btnFBLogin.Click
    '    Dim FBContext As New FacebookWebContext()

    '    If FBContext.IsAuthenticated Then
    '        Dim FBClient As New FacebookWebClient()
    '        Dim result = CType(FBClient.Get("me?fields=id"), Facebook.JsonObject)
    '        Dim ID As String = result("id")
    '        Dim Username As String = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()).GetUserID(ID)

    '        If Not String.IsNullOrEmpty(Username) Then
    '            FormsAuthentication.SetAuthCookie(Login1.UserName, Login1.RememberMeSet)
    '            Dim Url As String = Request("ReturnUrl")
    '            If Url = "" Then Url = "~/"
    '            Response.Redirect(Url)
    '        End If

    '    End If
    'End Sub

End Class
