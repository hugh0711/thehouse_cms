
Partial Class changepassword
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ShowPanel(1)
        End If
    End Sub

    Protected Sub ShowPanel(ByVal Index As Integer)
        Panel1.Visible = False
        Panel2.Visible = False

        Select Case Index
            Case 1
                Panel1.Visible = True
            Case 2
                Panel2.Visible = True
        End Select
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Page.IsValid Then
            Dim User As MembershipUser = Membership.GetUser()
            User.ChangePassword(txtOldPassword.Text, txtPassword.Text)
            Membership.UpdateUser(User)
            ShowPanel(2)
        End If
    End Sub


    Protected Sub valOldPassword_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valOldPassword.ServerValidate
        Dim User As MembershipUser = Membership.GetUser()
        If Membership.ValidateUser(User.UserName, txtOldPassword.Text) Then
            args.IsValid = True
        Else
            args.IsValid = False
        End If

    End Sub

End Class
