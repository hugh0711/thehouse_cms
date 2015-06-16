Imports System.Net.Mail
Imports System.Net
Imports System.IO


Partial Class backoffice_member
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim MasterPage As master_Admin = CType(Me.Master, master_Admin)
        'MasterPage.SetPane("Pane2")

        If Not Page.IsPostBack Then
            If Request.QueryString("member") IsNot Nothing Then
                txtUsername.Text = Request.QueryString("member")
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

        Dim roles__1 As String() = Roles.GetAllRoles()

        UsersRoleList.DataSource = roles__1
        UsersRoleList.DataBind()

        CheckRolesForSelectedUser(Request.QueryString("member"))

        'ddlRegion.DataBind()
        'cblInterested.DataBind()
    End Sub


    Public Function CheckAdminForSelectedUser(selectedUserName As String) As Boolean
        ' Determine what roles the selected user belongs to 

        Dim check_permission As String = "Admin"

        Dim selectedUsersRoles As String() = Roles.GetRolesForUser(selectedUserName)

        ' Loop through the Repeater's Items and check or uncheck the checkbox as needed 



        If selectedUsersRoles.Contains(check_permission) Then
            Return True
        Else
            Return False
        End If


    End Function

    Protected Sub link_deleteRole_Click(sender As Object, e As CommandEventArgs)
        Roles.DeleteRole(e.CommandArgument.ToString(), False)
        CheckRolesForSelectedUser(Request.QueryString("member"))
        LoadRoles()
    End Sub


    Protected Sub btn_createRole_Click(sender As Object, e As EventArgs) Handles btn_createRole.Click
        Dim createRole As String = txt_role.Text
        Try
            If Roles.RoleExists(createRole) Then
                lbl_error.Text = "Role '" & Server.HtmlEncode(createRole) & "' already exists. Please specify a different role name."
                Return
            End If

            Roles.CreateRole(createRole)

            lbl_error.Text = "Role '" & Server.HtmlEncode(createRole) & "' created."

            ' Re-bind roles to GridView.

            CheckRolesForSelectedUser(Request.QueryString("member"))

        Catch
            lbl_error.Text = "Role '" & Server.HtmlEncode(createRole) & "' <u>not</u> created."
        End Try

    End Sub


    Public Sub CheckRolesForSelectedUser(selectedUserName As String)
        ' Determine what roles the selected user belongs to 
        'Dim selectedUserName As String = Request.QueryString("member")
        Dim selectedUsersRoles As String() = Roles.GetRolesForUser(selectedUserName)

        ' Loop through the Repeater's Items and check or uncheck the checkbox as needed 

        For Each ri As RepeaterItem In UsersRoleList.Items
            ' Programmatically reference the CheckBox 
            Dim RoleCheckBox As CheckBox = TryCast(ri.FindControl("RoleCheckBox"), CheckBox)
            ' See if RoleCheckBox.Text is in selectedUsersRoles 

            If selectedUsersRoles.Contains(RoleCheckBox.Text) Then
                RoleCheckBox.Checked = True
            Else
                RoleCheckBox.Checked = False
            End If
        Next
    End Sub


    Protected Sub RoleCheckBox_CheckChanged(sender As Object, e As EventArgs)
        ' Reference the CheckBox that raised this event 
        Dim RoleCheckBox As CheckBox = TryCast(sender, CheckBox)

        ' Get the currently selected user and role 
        Dim selectedUserName As String = Request.QueryString("member")

        Dim roleName As String = RoleCheckBox.Text

        ' Determine if we need to add or remove the user from this role 
        If RoleCheckBox.Checked Then
            ' Add the user to the role 
            Roles.AddUserToRole(selectedUserName, roleName)
            ' Display a status message 
            ActionStatus.Text = String.Format("User {0} was added to role {1}.", selectedUserName, roleName)
        Else
            ' Remove the user from the role 
            Roles.RemoveUserFromRole(selectedUserName, roleName)
            ' Display a status message 

            ActionStatus.Text = String.Format("User {0} was removed from role {1}.", selectedUserName, roleName)
        End If
    End Sub

    Protected Sub LoadData()
        Dim User As MembershipUser
        Dim roleItem As ListItem
        Dim UserName As String
        'Dim profile As New ProfileCommon()

        If txtUsername.Text <> "" Then
            User = Membership.GetUser(txtUsername.Text)
            If User Is Nothing Then
                Response.Redirect("~/backoffice/members.aspx")
            End If
            If Not User Is Nothing Then
                txtUsername.Text = User.UserName
                UserName = User.UserName
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
                t = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter).GetDataByUserID(UserName)
                If t.Rows.Count > 0 Then
                    r = t.Rows(0)
                    With r
                        rblGender.SelectedValue = .Gender
                        txtDisplayName.Text = .Name
                        txtContact.Text = .ContactNo
                        txtDeliveryAddress.Text = .DeliveryAddress
                        'radCountry.SelectedValue = .Country
                        txtFacebookID.Text = .FacebookUserID
                        txtBirthday.Text = .Birthday
                        If Not .IsUserPicUrlNull Then
                            imgSaved.ImageUrl = Path.Combine(ConfigurationManager.AppSettings("UserImagePath"), .UserPicUrl)

                        End If
                        'ddlCenter.SelectedValue = .CenterID
                    End With
                End If


                ' Chech this user has admin rights
                pnlAdminInfo.Visible = true
                pnlAdminNotFound.Visible = False
                
            End If

            txtUsername.Enabled = False
            chkNew.Checked = False
            btnDelete.Visible = True
            pnlMoreInfo.Visible = True
        Else
            txtUsername.Enabled = True
            chkNew.Checked = True
            btnDelete.Visible = False
            pnlLockedOut.Visible = False
            pnlMoreInfo.Visible = False
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

        UserPic = saveUserImage()

        If chkNew.Checked Then
            User = Membership.CreateUser(Username, txtPassword.Text)
            MemberAdaptor.Insert(Username, rblGender.SelectedValue, txtDisplayName.Text, txtEmail.Text, ContactNumber, txtDeliveryAddress.Text, radCountry.SelectedValue, Birthday, DateTime.Now, "", UserPic)
        Else
            User = Membership.GetUser(Username)
            If pnlPassword.Visible Then
                User.ChangePassword(User.GetPassword, txtPassword.Text)
                Membership.UpdateUser(User)
            End If
            Dim getSavedfile = Path.GetFileName(imgSaved.ImageUrl)
            If Not UserPic = getSavedfile Then
                UserPic = getSavedfile
            End If
            MemberAdaptor.Update(rblGender.SelectedValue, txtDisplayName.Text, txtEmail.Text, ContactNumber, txtDeliveryAddress.Text, radCountry.SelectedValue, Birthday, Utility.StringToDateTime(lblCreationDate.Text), txtFacebookID.Text, UserPic, Username)
        End If

        User.Email = txtEmail.Text
        User.IsApproved = chkApproved.Checked
        Membership.UpdateUser(User)

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click


        If Page.IsValid() Then
            Try
                SaveData()
                LoadData()
                lblMessage.Text = "用戶已成功儲存"
            Catch ex As Exception
                lblMessage.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("~/backoffice/members.aspx")
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim MemberAdaptor As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter

        Try
            Dim Username As String = txtUsername.Text
            'MemberAdaptor.DeleteQuery(txtUsername.Text)
            Membership.DeleteUser(Username, True)
            
            'Dim da2 As New UserDataSetTableAdapters.UserDetailTableAdapter()
            'da2.DeleteByUsername(Username)
            MemberAdaptor.Delete(Username)

            Response.Redirect("~/backoffice/members.aspx")
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


    'Protected Sub SendEmail()
    '    Dim FromAddress, ToAddress As MailAddress
    '    Dim Body As String = ""
    '    Dim User As MembershipUser = Membership.GetUser(txtUsername.Text)

    '    If User IsNot Nothing Then
    '        Try
    '            FromAddress = New MailAddress(ConfigurationManager.AppSettings("adminEmail"))
    '            ToAddress = New MailAddress(txtEmail.Text)

    '            Dim content As String
    '            content = File.ReadAllText(Server.MapPath("~/template/password.htm"))
    '            Body = content.Replace("<%Username%>", txtUsername.Text)
    '            Body = Body.Replace("<%Password%>", User.GetPassword())
    '            Body = Body.Replace("<%URL%>", String.Format("http://{0}/login.aspx", Request.Url.Host))


    '            EmailClass.SendEmail(FromAddress, ToAddress, "Password for eCover", Body)
    '        Catch ex As Exception
    '            lblMessage.Text = ex.Message
    '        End Try
    '    End If
    'End Sub

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
        If Page.IsValid() Then
            Try
                SaveData()
                Response.Redirect("~/backoffice/members.aspx")
            Catch ex As Exception
                lblMessage.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub btnSetPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetPassword.Click
        pnlPassword.Visible = True
        pnlSetPassword.Visible = False
    End Sub

    Protected Function PopupPerson(ByVal PersonID As Integer) As String
        Dim Url As String = ResolveUrl(String.Format("~/backoffice/person/detail.aspx?id={0}", PersonID))
        Return "TINY.box.show({iframe:""" & Url & """, width: 750, height: 500})"
    End Function


    Protected Function saveUserImage() As String
        Dim UploadPath As String = ConfigurationManager.AppSettings("UploadPath")
        Dim FilePath As String = ""
        Dim FileOK As Boolean = False
        Dim FileSaved As Boolean = False
        Dim imageFilename As String = ""

        If FileUpload1.HasFile Then
            Session("UserImage") = FileUpload1.FileName
            Dim FileExtension As String = Path.GetExtension(Session("UserImage")).ToLower()
            Select Case FileExtension
                Case ".jpg", ".jpeg", ".png", ".gif", ".bmp"
                    FileOK = True
            End Select
        End If

        If FileOK Then
            Try
                FilePath = Path.Combine(UploadPath, Session("UserImage"))
                FileUpload1.SaveAs(Server.MapPath(FilePath))
                FileSaved = True
            Catch ex As Exception
                lblMessage.Text = "檔案上傳失敗. " & ex.Message
                FileSaved = False
            End Try
        Else
            lblMessage.Text = "上傳檔案必須為影像檔案"
        End If

        If FileSaved Then
            imageFilename = ResizeImage()
            'If chkCrop.Checked Then
            '    ShowPanel(2)
            '    imgCrop.ImageUrl = FilePath & "?" & Now().Ticks
            '    imgPreview.ImageUrl = FilePath & "?" & Now().Ticks
            'Else
            '    ResizeImage()
            'End If
        End If

        Return imageFilename

    End Function

    Protected Function ResizeImage() As String
        Dim Filename As String = Session("UserImage")
        Dim NewFilename As String = String.Format("{0}.jpg", System.Guid.NewGuid().ToString())
        Dim JpegCompression As Integer = CInt(ConfigurationManager.AppSettings("JpegCompression"))
        Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("UploadPath"), Filename)))
        Dim userImageWidth As Integer = 300
        Dim userImageHeight As Integer = 300

        Dim Size As New System.Drawing.Size(userImageWidth, userImageHeight)
        img = ImageClass.ResizeImage(img, Size)
        Dim SavePath As String = Path.Combine(ConfigurationManager.AppSettings("UserImagePath"), NewFilename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(SavePath), JpegCompression)
        imgSaved.ImageUrl = SavePath
        Dim width As Integer = img.Width
        Dim height As Integer = img.Height
        img.Dispose()

        Return NewFilename

    End Function

End Class
