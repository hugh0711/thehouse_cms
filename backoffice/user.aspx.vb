Imports System.Net.Mail
Imports System.Net
Imports System.IO

Partial Class backoffice_user
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("user") IsNot Nothing Then
                txtUsername.Text = Request.QueryString("user")
            End If
            LoadRoles()
            LoadData()
        End If
    End Sub

    Protected Sub LoadRoles()
        Dim roleitem As String

        With cblRole.Items
            .Clear()
            For Each roleitem In Roles.GetAllRoles()
                .Add(roleitem)
            Next
        End With

        'ddlRegion.DataBind()
        'cblInterested.DataBind()
    End Sub

    Protected Sub LoadData()
        Dim User As MembershipUser
        Dim roleItem As ListItem
        'Dim profile As New ProfileCommon()

        If txtUsername.Text <> "" Then
            User = Membership.GetUser(txtUsername.Text)
            If User Is Nothing Then
                Response.Redirect("~/backoffice/users.aspx")
            End If
			If Not User Is Nothing Then
				txtUsername.Text = User.UserName
				txtEmail.Text = User.Email
				chkApproved.Checked = User.IsApproved
				lblCreationDate.Text = Utility.DateTimeToString(User.CreationDate)
				lblLastLoginDate.Text = Utility.DateTimeToString(User.LastLoginDate)
				pnlLockedOut.Visible = User.IsLockedOut

				For Each roleItem In cblRole.Items
					If Roles.IsUserInRole(User.UserName, roleItem.Text) Then
						roleItem.Selected = True
					Else
						roleItem.Selected = False
					End If
				Next

				pnlPassword.Visible = False
				pnlSetPassword.Visible = True

				'Read Table
				Dim r As MemberDetailDataSet.MemberDetailRow
				Dim t As New MemberDetailDataSet.MemberDetailDataTable
				t = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter).GetDataByUserID(txtUsername.Text)
				If t.Rows.Count > 0 Then
					r = t.Rows(0)
					With r
						rblGender.SelectedValue = .Gender
						txtDisplayName.Text = .Name
						txtContact.Text = .ContactNo
						txtDeliveryAddress.Text = .DeliveryAddress
                        radCountry.SelectedValue = .Country
                        txtFacebookID.Text = .FacebookUserID
                        txtBirthday.Text = .Birthday
					End With
				End If
			End If

            txtUsername.Enabled = False
            chkNew.Checked = False
            btnDelete.Visible = True
        Else
            txtUsername.Enabled = True
            chkNew.Checked = True
            btnDelete.Visible = False
            pnlLockedOut.Visible = False
        End If
    End Sub

    Protected Sub SaveData()
		Dim MemberAdaptor As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter
        Dim User As MembershipUser
        Dim roleItem As ListItem
        Dim rolename As String
        Dim Username As String
        Dim StartDate, EndDate As Date
        Dim Interested As String = ""
		Dim ContactNumber As String = txtContact.Text
        Dim Gender As String = ""
        Dim Region As String = ""
        Dim Birthday As Date
        Dim UserPic As String = ""

        Username = txtUsername.Text
        StartDate = #1/1/1900#
        EndDate = #1/1/2999#
        If IsDate(txtBirthday.Text) Then
            Birthday = CDate(txtBirthday.Text)
        Else
            Birthday = Utility.NoBirthday
        End If
        'Dim c As New ArrayList()
        'For Each item As ListItem In cblInterested.Items
        '    If item.Selected Then
        '        c.Add(item.Value)
        '    End If
        'Next
        'Interested = Join(c.ToArray(), ",")

        If chkNew.Checked Then
            'User = Membership.CreateUser(Username, GenPassword(8))
            User = Membership.CreateUser(Username, txtPassword.Text)
			'User = Membership.CreateUser(Username, "111111")
            MemberAdaptor.Insert(Username, rblGender.SelectedValue, txtDisplayName.Text, txtEmail.Text, ContactNumber, txtDeliveryAddress.Text, radCountry.SelectedValue, Birthday, DateTime.Now, "", UserPic)
        Else
            User = Membership.GetUser(Username)
            If pnlPassword.Visible Then
                User.ChangePassword(User.GetPassword, txtPassword.Text)
                Membership.UpdateUser(User)
			End If
            MemberAdaptor.Update(Username, rblGender.SelectedValue, txtDisplayName.Text, txtEmail.Text, ContactNumber, txtDeliveryAddress.Text, radCountry.SelectedValue, Birthday, Utility.StringToDateTime(lblCreationDate.Text), txtFacebookID.Text, UserPic, Username)
		End If

        User.Email = txtEmail.Text
        User.IsApproved = chkApproved.Checked
        Membership.UpdateUser(User)

        For Each roleItem In cblRole.Items
            rolename = roleItem.Text
            If roleItem.Selected Then
                If Not Roles.IsUserInRole(Username, rolename) Then
                    Roles.AddUserToRole(Username, rolename)
                End If
            Else
                If Roles.IsUserInRole(Username, rolename) Then
                    Roles.RemoveUserFromRole(Username, rolename)
                End If
            End If
        Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click


        If Page.IsValid() Then
            Try
                SaveData()
                Response.Redirect("~/backoffice/users.aspx")
            Catch ex As Exception
                lblMessage.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("~/backoffice/users.aspx")
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
		Dim MemberAdaptor As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter

        Try
            'MemberAdaptor.DeleteQuery(txtUsername.Text)
            Membership.DeleteUser(txtUsername.Text)
			MemberAdaptor.Delete(txtUsername.Text)
            Response.Redirect("~/backoffice/users.aspx")
        Catch ex As Exception
            lblMessage.Text = ex.Message
        End Try
    End Sub


    'Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
    '    Try
    '        SendEmail()
    '        lblMessage.Text = "Email is sent"
    '    Catch ex As Exception
    '        lblMessage.Text = ex.Message
    '    End Try
    'End Sub


    Protected Sub SendEmail()
        Dim FromAddress, ToAddress As MailAddress
        Dim Body As String = ""
        Dim User As MembershipUser = Membership.GetUser(txtUsername.Text)

        If User IsNot Nothing Then
            Try
                FromAddress = New MailAddress(ConfigurationManager.AppSettings("adminEmail"))
                ToAddress = New MailAddress(txtEmail.Text)

                Dim content As String
                content = File.ReadAllText(Server.MapPath("~/template/password.htm"))
                Body = content.Replace("<%Username%>", txtUsername.Text)
                Body = Body.Replace("<%Password%>", User.GetPassword())
                Body = Body.Replace("<%URL%>", String.Format("http://{0}/login.aspx", Request.Url.Host))


                EmailClass.SendEmail(FromAddress, ToAddress, "Password for eCover", Body)
            Catch ex As Exception
                lblMessage.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Function GenPassword(ByVal Length As Integer) As String
        Dim seed As New ArrayList
        Dim str As New StringBuilder()
        Dim ran As New Random()
        For i = Asc("a") To Asc("z")
            seed.Add(Chr(i))
        Next
        For i = Asc("A") To Asc("Z")
            seed.Add(Chr(i))
        Next
        For i = Asc("0") To Asc("9")
            seed.Add(Chr(i))
        Next

        For i = 1 To Length
            str.Append(seed(ran.Next(seed.Count)))
        Next

        Return str.ToString()
    End Function

    Protected Sub btnUnlock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnlock.Click
        Dim User As MembershipUser = Membership.GetUser(txtUsername.Text)
        If User IsNot Nothing Then
            Try
                User.UnlockUser()
                pnlLockedOut.Visible = False
                lblMessage.Text = "This member is Unlocked"
            Catch ex As Exception
                lblMessage.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub btnSaveClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveClose.Click
        btnSave_Click(sender, e)
        btnClose_Click(sender, e)
    End Sub

    Protected Sub btnSetPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetPassword.Click
        pnlPassword.Visible = True
        pnlSetPassword.Visible = False
    End Sub
End Class
