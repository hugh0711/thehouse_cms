
Partial Class myvideo
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            hfdUsername.Value = Page.User.Identity.Name
            hfdTypeID.Value = CInt(ConfigurationManager.AppSettings("FavVideoTypeID"))
        End If
    End Sub
End Class
