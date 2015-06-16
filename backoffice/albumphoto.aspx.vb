
Partial Class backoffice_albumphoto
    Inherits System.Web.UI.Page

    Protected AuthCookie As String
    Protected AlbumID As String

    Public Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session.Clear()

            Dim auth_cookie As HttpCookie = Request.Cookies(FormsAuthentication.FormsCookieName)
            If Not auth_cookie Is Nothing Then
                AuthCookie = auth_cookie.Value
            End If

            If Not String.IsNullOrWhiteSpace(Request("album")) Then
                AlbumID = Request("album")
                hfdAlbumID.Value = Request("album")
            End If
        End If

    End Sub

End Class
