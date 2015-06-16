
Partial Class admin
    Inherits System.Web.UI.Page

    Protected Sub LoginButton_Click(sender As Object, e As System.EventArgs)
        If Membership.ValidateUser(Login1.UserName, Login1.Password) Then
            FormsAuthentication.SetAuthCookie(Login1.UserName, Login1.RememberMeSet)
            Dim Url As String = Request("ReturnUrl")
            If Url = "" Then Url = "~/backoffice/admin.aspx"
            Response.Redirect(Url)
        End If
    End Sub


End Class
