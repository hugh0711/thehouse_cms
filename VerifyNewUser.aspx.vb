
Partial Class VerifyNewUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If String.IsNullOrEmpty(Request("id")) Then
                lblMessage.Text = String.Format("Invalid Url or activation code. Please check the email and try again or contact our customer service <a href='mailto:{0}'>{0}</a>", ConfigurationManager.AppSettings("EnquiryEmail"))
            Else
                Dim UserID As Guid
                Try
                    UserID = New Guid(Request("id"))

                    Dim User As MembershipUser = Membership.GetUser(UserID)
                    If User Is Nothing Then
                        lblMessage.Text = String.Format("Invalid Url or activation code. Please check the email and try again or contact our customer service <a href='mailto:{0}'>{0}</a>", ConfigurationManager.AppSettings("EnquiryEmail"))
                    ElseIf User.IsApproved Then
                        lblMessage.Text = String.Format("This user is already activated. Please check the email and try again or contact our customer service <a href='mailto:{0}'>{0}</a>", ConfigurationManager.AppSettings("EnquiryEmail"))
                    Else
                        User.IsApproved = True
                        Membership.UpdateUser(User)
                        lblMessage.Text = "Your membership account is activated. Please login to <a href='login.aspx'>here</a>."
                    End If

                Catch ex As Exception
                    lblMessage.Text = String.Format("Invalid Url or activation code. Please check the email and try again or contact our customer service <a href='{0}'>{0}</a>", ConfigurationManager.AppSettings("EnquiryEmail"))
                End Try
            End If
        End If
    End Sub
End Class
