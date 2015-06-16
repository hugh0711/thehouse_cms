Imports Facebook
Imports Facebook.Web

Partial Class memberinfo
    Inherits System.Web.UI.Page

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
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadData()
        End If
    End Sub

    Protected Sub LoadData()
        Dim User As MembershipUser = Membership.GetUser()
        If User IsNot Nothing Then
            Dim dsMember As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()
            Dim dtMember As MemberDetailDataSet.MemberDetailDataTable
            Dim drMember As MemberDetailDataSet.MemberDetailRow

            dtMember = dsMember.GetDataByUserID(User.UserName)
            If dtMember.Rows.Count > 0 Then
                drMember = dtMember.Rows(0)

                With drMember
                    txtEmail.Text = .Email
                    txtName.Text = .Name
                    radGender.SelectedValue = .Gender
                    txtContactNo.Text = .ContactNo
                    ddlYear.SelectedValue = .Birthday.Year
                    ddlMonth.SelectedValue = .Birthday.Month
                    ddlDay.SelectedValue = .Birthday.Day
                    If .FacebookUserID = "" Then
                        btnUnlinkFB.Visible = False
                        lblFBLinkStatus.Text = "(沒有連結)"
                    Else
                        btnLinkFB.Visible = False
                        lblFBLinkStatus.Text = "(已連結)"
                    End If
                    hfdFacebookID.Value = .FacebookUserID
                    LoadFacebookInfo()
                End With
            End If

        End If

    End Sub

    Protected Sub SaveData()
        Dim User As MembershipUser = Membership.GetUser()
        Dim Birthday As New Date(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), CInt(ddlDay.SelectedValue))
        Dim daMember As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()
        Dim RowAffected As Integer

        RowAffected = daMember.UpdateQuery(User.UserName, radGender.SelectedValue, txtName.Text, txtEmail.Text, txtContactNo.Text, "", "", Birthday, Date.Now(), User.UserName)

        If RowAffected > 0 Then
            User.Email = txtEmail.Text
            Membership.UpdateUser(User)
        End If

    End Sub


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Page.IsValid Then
            Try
                SaveData()
                lblMessage.Text = "會員資料已成功更新"
            Catch ex As Exception
                lblMessage.Text = "Error: " & ex.Message
            End Try
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
            Dim result = CType(FBClient.Get("me?fields=id,name"), Facebook.JsonObject)
            Dim ID As String = result("id")
            hfdFacebookID.Value = result("id")
            imgFacebook.ImageUrl = String.Format("http://graph.facebook.com/{0}/picture?type=square", result("id"))
            lblFacebook.Text = result("name")
            pnlFacebook.Visible = True
            pnlFacebookLogin.Visible = False
        Else
            pnlFacebook.Visible = False
            pnlFacebookLogin.Visible = True
        End If

    End Sub

    Protected Sub btnLinkFB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLinkFB.Click
        Dim da As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()
        Dim Username As String = Page.User.Identity.Name
        Dim rownum As Integer = da.UpdateFacebookUserID(hfdFacebookID.Value, Username)
        If rownum > 0 Then
            btnLinkFB.Visible = False
            btnUnlinkFB.Visible = True
            lblFBMessage.Text = "成功連結Facebook"
            lblFBLinkStatus.Text = "(已連結)"
        Else
            lblFBMessage.Text = "連結Facebook失敗"
        End If
    End Sub

    Protected Sub btnUnlinkFB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnlinkFB.Click
        Dim da As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()
        Dim Username As String = Page.User.Identity.Name
        Dim rownum As Integer = da.UpdateFacebookUserID("", Username)
        If rownum > 0 Then
            btnLinkFB.Visible = True
            btnUnlinkFB.Visible = False
            lblFBMessage.Text = "成功脫離Facebook"
            lblFBLinkStatus.Text = "(沒有連結)"
        Else
            lblFBMessage.Text = "脫離Facebook失敗"
        End If
    End Sub

End Class
