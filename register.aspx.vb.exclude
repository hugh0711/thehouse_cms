Imports System.Threading
Imports System.Globalization
Imports System.IO
Imports Utility
Imports System.Net.Mail
Imports Facebook
Imports Facebook.Web

Partial Class register
    Inherits System.Web.UI.Page

    Protected Overrides Sub InitializeCulture()
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(Session("MyCulture").ToString())
        MyBase.InitializeCulture()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim i As Integer
        For i = 1 To 31
            ddlDay.Items.Add(i)
        Next
        For i = 1 To 12
            ddlMonth.Items.Add(i)
        Next
        For i = Now.Year To 1900 Step -1
            ddlYear.Items.Add(i)
        Next
        'LoadFacebookInfo()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.User.Identity.IsAuthenticated Then
            Logined()
        End If

        If Not IsPostBack Then
            Bind()

            LoadFacebookInfo()
            If Not String.IsNullOrWhiteSpace(Request("link")) Then
                Select Case Request("link").ToLower()
                    Case "facebook"
                        btnLinkFB_Click(Nothing, Nothing)
                End Select
            End If
            'If Not String.IsNullOrEmpty(Request("link")) Then
            '    btnLinkFB_Click(Nothing, Nothing)
            'End If
        End If
    End Sub

    Protected Sub Bind()
        radGender.SelectedIndex = 0
        radRegion.SelectedIndex = 0
    End Sub

    Protected Sub custvalLoginID_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles custvalLoginID.ServerValidate
        args.IsValid = True
        Dim adapter As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter
        If txtLoginID.Text.Length < 5 Then
            args.IsValid = False
            If MyCulture() = MyCulture_HK Then
                custvalLoginID.ErrorMessage = "登入ID長度必須大於 5 個字元"
            Else
                custvalLoginID.ErrorMessage = "Login ID must greater than 5 chars"
            End If

        End If
        If adapter.GetCountByUserID(txtLoginID.Text) > 0 Then
            args.IsValid = False
            If MyCulture() = MyCulture_HK Then
                custvalLoginID.ErrorMessage = "登入ID已經被使用, 請重新輸入"
            Else
                custvalLoginID.ErrorMessage = "Login ID already exists, please input again"
            End If
        End If
    End Sub

    Protected Sub custvalPassword_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles custvalPassword.ServerValidate
        args.IsValid = True
        If txtPassword.Text.Length < 6 Or txtPassword.Text.Length > 20 Then
            args.IsValid = False
            custvalPassword.ErrorMessage = "密碼必須為 6 - 20 個字元"
        End If
        If txtPassword.Text <> txtPassword2.Text Then
            args.IsValid = False
            custvalPassword.ErrorMessage = "密碼必須與重複輸相同"
        End If
    End Sub

    'Protected Sub custvalValidate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles custvalValidate.ServerValidate
    '	args.IsValid = True
    '	If Request.Cookies("CheckCode").Value Is Nothing Then
    '		custvalValidate.ErrorMessage = "無法取得Cookie，請檢察瀏覽器是否有封鎖Cookie!!"
    '		args.IsValid = False
    '	End If
    '	If String.Compare(txtValidate.Text.ToUpper, Request.Cookies("CheckCode").Value) <> 0 Then
    '		custvalValidate.ErrorMessage = "驗證碼錯誤!!"
    '		args.IsValid = False
    '	End If
    'End Sub

    'Protected Sub custvalAgree_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles custvalAgree.ServerValidate
    '	args.IsValid = (chkAgree.Checked = True)
    'End Sub

    Protected Sub SaveData()
        Dim MemberAdaptor As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter
        Dim User As MembershipUser
        Dim Username As String
        Dim Birthday As New Date(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), CInt(ddlDay.SelectedValue))

        Username = txtLoginID.Text

        User = Membership.CreateUser(Username, txtPassword.Text, txtEmail.Text)
        'User.IsApproved = False
        Membership.UpdateUser(User)
        MemberAdaptor.Insert(Username, radGender.SelectedValue, txtName.Text, txtEmail.Text, txtContactNo.Text, "", "", Birthday, Now(), hfdFacebookIUserD.Value)

        Roles.AddUserToRole(Username, "Member")

        'SendVerification(User)
        SendConfirmation(User)
        FormsAuthentication.SetAuthCookie(Username, False)
    End Sub

    Protected Sub SendConfirmation(ByVal User As MembershipUser)
        Dim BaseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority) & Request.ApplicationPath

        Dim sr As New StreamReader(Server.MapPath("~/email_template/WelcomeNewUser.txt"))
        Dim message As New MailMessage()

        message.IsBodyHtml = True
        message.From = New MailAddress(ConfigurationManager.AppSettings("EnquiryEmail"))
        message.To.Add(New MailAddress(txtEmail.Text, txtName.Text))
        message.Subject = "Welcome from MajiTV"
        message.Body = sr.ReadToEnd()
        sr.Close()

        message.Body = message.Body.Replace("<%UserName%>", txtName.Text)
        message.Body = message.Body.Replace("<%HomeUrl%>", BaseUrl)

        Dim Client As New SmtpClient()
        Client.Send(message)
    End Sub

    Protected Sub SendVerification(ByVal User As MembershipUser)
        Dim newUser As Guid = CType(User.ProviderUserKey, Guid)
        Dim BaseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority) & Request.ApplicationPath
        Dim FullPath As String = BaseUrl & "VerifyNewUser.aspx?id=" & newUser.ToString()

        Dim sr As New StreamReader(Server.MapPath("~/email_template/VerifyNewUser.txt"))
        Dim message As New MailMessage()

        message.IsBodyHtml = True
        message.From = New MailAddress(ConfigurationManager.AppSettings("EnquiryEmail"))
        message.To.Add(New MailAddress(txtEmail.Text, txtName.Text))
        message.Subject = "Email Verification from MajiTV"
        message.Body = sr.ReadToEnd()
        sr.Close()

        message.Body = message.Body.Replace("<%UserName%>", txtName.Text)
        message.Body = message.Body.Replace("<%VerificationUrl%>", FullPath)

        Dim Client As New SmtpClient()
        Client.Send(message)
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Page.IsValid Then
            Try
                SaveData()
                Response.Redirect("~/registerthankyou.aspx")
            Catch ex As Exception
                lblMessage.Text = "Error: " & ex.Message
            End Try
        End If
    End Sub

    'Protected Function Birthday() As DateTime
    '	Return StringToDateTime(String.Format("{0}-{1}-{2}", ddlYear.SelectedValue, ddlMonth.SelectedValue, ddlDay.SelectedValue), False)
    'End Function

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Default.aspx")
    End Sub

    'Protected Sub custvalCountry_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles custvalCountry.ServerValidate
    '	args.IsValid = (ddlCountry.SelectedValue <> "")
    'End Sub

    'Protected Sub btnFBConnect_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnFBConnect.Click
    '	Dim MySession As New SessionClass
    '	MySession.LastPage = Request.Url.AbsoluteUri
    '	Dim oFB = New oAuthFacebook
    '	Response.Redirect(oFB.AuthorizationLinkGet)
    'End Sub

    Protected Sub custvalEmail_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles custvalEmail.ServerValidate
        args.IsValid = True

        If Membership.GetUserNameByEmail(txtEmail.Text) IsNot Nothing Then
            args.IsValid = False
        End If
    End Sub

    Protected Sub Logined()
        If Membership.GetUser IsNot Nothing Then
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

    Protected Sub valBirthday_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valBirthday.ServerValidate
        Dim d As Date

        Try
            d = New Date(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), CInt(ddlDay.SelectedValue))
            args.IsValid = True
        Catch ex As Exception
            args.IsValid = False
        End Try
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

    Protected Sub btnLinkFB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLinkFB.Click
        Dim FBContext As FacebookWebContext
        FBContext = New FacebookWebContext()

        If FBContext.IsAuthenticated Then
            Dim FBClient As New FacebookWebClient()

            Dim result = CType(FBClient.Get("me?fields=id,name,birthday,email,gender"), Facebook.JsonObject)
            Dim ID As String = result("id")
            hfdFacebookIUserD.Value = result("id")
            Try
                txtEmail.Text = result("email")
            Catch
                txtEmail.Text = ""
            End Try
            Dim Temp As String
            Try
                Temp = result("birthday")
            Catch
                Temp = ""
            End Try
            If IsDate(Temp) Then
                Dim Bday As Date = Convert.ToDateTime(Temp)
                ddlYear.SelectedValue = Bday.Year
                ' Month and Day is swapped since the US date and HK date
                ddlMonth.SelectedValue = Bday.Day
                ddlDay.SelectedValue = Bday.Month
            End If
            Temp = result("gender")
            If Temp <> "" Then
                If Temp.ToLower() = "male" Then
                    radGender.SelectedValue = "M"
                Else
                    radGender.SelectedValue = "F"
                End If
            End If
            'imgFacebook.ImageUrl = String.Format("http://graph.facebook.com/{0}/picture?type=square", result("id"))
            txtName.Text = result("name")
            lblFBLinkStatus.Text = "(連結)"
            pnlFacebook.Visible = True
            pnlFacebookLogin.Visible = False
            btnLinkFB.Visible = False
            btnUnlinkFB.Visible = True
        Else
            pnlFacebook.Visible = False
            pnlFacebookLogin.Visible = True
        End If
    End Sub


    Protected Sub btnUnlinkFB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnlinkFB.Click
        hfdFacebookIUserD.Value = ""
        lblFBLinkStatus.Text = "(沒有連結)"
        btnLinkFB.Visible = True
        btnUnlinkFB.Visible = False
    End Sub

    Protected Sub btnFBLogin_Click(sender As Object, e As System.EventArgs) Handles btnFBLogin.Click
        LoadFacebookInfo()
    End Sub

End Class
