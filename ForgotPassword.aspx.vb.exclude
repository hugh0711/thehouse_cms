Imports System.Net.Mail
Imports System.IO

Partial Class ForgotPassword
    Inherits System.Web.UI.Page


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Page.IsValid Then
            Try
                Dim UserName As String = txtUserName.Text
                Dim User As MembershipUser = Membership.GetUser(UserName)

                Dim Message As New MailMessage()

                Message.IsBodyHtml = True
                Message.From = New MailAddress(ConfigurationManager.AppSettings("EnquiryEmail"))
                Message.To.Add(New MailAddress(User.Email))
                Message.Subject = "Password Recovery from Winexpert.hk"

                Dim BaseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority) & Request.ApplicationPath
                Dim FullUrl As String = BaseUrl & "login.aspx"
                Dim sr As New StreamReader(Server.MapPath("~/email_template/ForgotPassword.txt"))
                Message.Body = sr.ReadToEnd()
                sr.Close()

                Dim DisplayName As String = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()).GetName(UserName)
                Message.Body = Message.Body.Replace("<%UserName%>", DisplayName)
                Message.Body = Message.Body.Replace("<%Password%>", User.GetPassword())
                Message.Body = Message.Body.Replace("<%LoginLink%>", FullUrl)

                Dim Client As New SmtpClient()
                Client.Send(Message)

                lblMessage.Text = "Password is sent to your mailbox"
            Catch ex As Exception
                lblMessage.Text = "Error: " & ex.Message
            End Try
        End If
    End Sub

    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        Dim User As MembershipUser = Membership.GetUser(txtUserName.Text)
        If User Is Nothing Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub
End Class
