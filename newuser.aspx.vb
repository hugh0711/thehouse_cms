
Partial Class newuser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack And Not String.IsNullOrEmpty(Request.QueryString("username")) Then
            Dim User = Membership.GetUser(Request.QueryString("username"))
            If User IsNot Nothing Then
                Dim newNserID = User.ProviderUserKey.ToString()
                btnNewuser.HRef = String.Format("thehouse://{0}", newNserID)
            End If
        End If
    End Sub
End Class
