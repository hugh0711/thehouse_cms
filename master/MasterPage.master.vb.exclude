Imports Utility
Imports System.Threading
Imports System.Globalization
Imports System.Data
Imports Facebook.Web
Imports System.Net
Imports System.IO

Partial Class master_MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        hfdVideoFunctionID.Value = ConfigurationManager.AppSettings("VideoFunctionID")
        hfdMainPageID.Value = ConfigurationManager.AppSettings("MainPageID")
        hfdLegalPageID.Value = ConfigurationManager.AppSettings("LegalPageID")
        'CheckLogin()
    End Sub


    'Protected Sub InitializeCulture()
    '	Thread.CurrentThread.CurrentUICulture = New CultureInfo(Session("MyCulture").ToString())
    '	'MyBase.InitializeCulture()
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Bind()
        End If
    End Sub

    Protected Sub Bind()

    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FormsAuthentication.SignOut()
        'Dim FBContext As New FacebookWebContext()
        'Dim FBClient As New FacebookWebClient()
        'Dim Url As String = String.Format("https://www.facebook.com/logout.php?next={0}&access_token={1}", Server.UrlEncode(Request.Url.GetLeftPart(UriPartial.Authority)), FBContext.AccessToken)
        'Dim w As New WebClient()
        'Dim s As Stream = w.OpenRead(Url)
        'Dim r As New StreamReader(s)
        'Dim rep As String = r.ReadToEnd()
        ''Dim FBAuth As New FacebookWebAuthorizer(FBContext)
        ''Dim LogoutUrl As String = FBAuth.
        Response.Redirect("~/")
    End Sub


    Protected Sub CheckLogin()
        Dim FBContext As New FacebookWebContext()

        If FBContext.IsAuthenticated AndAlso Not Page.User.Identity.IsAuthenticated Then
            Dim FBClient As New FacebookWebClient()
            Dim result = CType(FBClient.Get("me?fields=id"), Facebook.JsonObject)
            Dim ID As String = result("id")
            Dim Username As String = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()).GetUserID(ID)

            Dim User As MembershipUser = Membership.GetUser(Username)
            If User IsNot Nothing Then
                'FormsAuthentication.RedirectFromLoginPage(Username, False)
                FormsAuthentication.SetAuthCookie(Username, False)
                Response.Redirect(Request.Url.ToString())
                'CType(LoginView1.FindControl("fbLogin"), Panel).Visible = False
            End If

        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Response.Redirect(String.Format("~/videos.aspx?q={0}", Server.UrlEncode(txtSearch.Text)))
    End Sub

End Class

