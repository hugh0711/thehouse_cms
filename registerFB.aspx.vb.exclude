Imports System.Threading
Imports System.Globalization
Imports System.IO
Imports Utility
Imports System.Net.Mail
Imports Facebook
Imports Facebook.Web

Partial Class registerFB
    Inherits System.Web.UI.Page

    Protected Overrides Sub InitializeCulture()
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(Session("MyCulture").ToString())
        MyBase.InitializeCulture()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        LoadFacebookInfo()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.User.Identity.IsAuthenticated Then
            Logined()
        End If

        If Not IsPostBack Then
            'If Not String.IsNullOrEmpty(Request("link")) Then
            '    btnLinkFB_Click(Nothing, Nothing)
            'End If
        End If
        'LoadFacebookInfo()
    End Sub

   

    Protected Sub Logined()
        If Membership.GetUser IsNot Nothing Then
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

    Protected Sub LoadFacebookInfo()
        'Dim FBClient As New FacebookWebClient()
        Dim FBContext As FacebookWebContext
        FBContext = New FacebookWebContext()

        If FBContext.IsAuthenticated Then
            Dim FBClient As New FacebookWebClient()
            Dim result = CType(FBClient.Get("me?fields=id,name,link"), Facebook.JsonObject)
            Dim ID As String = result("id")
            hfdFacebookID.Value = result("id")
            imgFacebook.ImageUrl = String.Format("http://graph.facebook.com/{0}/picture?type=square", result("id"))
            lblFacebook.Text = result("name")

            Dim Username As String = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()).GetUserID(ID)
            If String.IsNullOrEmpty(Username) Then
                lblFBLinkStatus.Text = "(沒有連結)"
            Else
                lblFBLinkStatus.Text = "(已經連結到另一個會員)"
                btnLinkFB.Visible = False
            End If
            btnUnlinkFB.Visible = False
            pnlFacebook.Visible = True
            pnlFacebookLogin.Visible = False
        Else
            pnlFacebook.Visible = False
            pnlFacebookLogin.Visible = True
        End If

    End Sub

  
    Protected Sub btnLinkFB_Click(sender As Object, e As System.EventArgs) Handles btnLinkFB.Click
        Response.Redirect("~/register.aspx?link=facebook")
    End Sub
End Class
