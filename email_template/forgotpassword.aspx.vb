
Partial Class template_forgotpassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request("user") IsNot Nothing AndAlso Request("user") <> "" Then
            Dim m As MembershipUser = Membership.GetUser(Request("user"))
            lblUsername.Text = m.UserName
            lblPassword.Text = m.GetPassword()
            Dim Url As String = String.Format("{0}/login.aspx", Request.Url.GetLeftPart(UriPartial.Authority))
        End If
    End Sub


End Class
