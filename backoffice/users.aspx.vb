
Partial Class backoffice_users
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblAction.Text = "1"
            RefreshMembers()
        End If
    End Sub

    Protected Sub RefreshMembers(Optional ByVal PageIndex As Integer = 0)
        With GridView1
            Select Case lblAction.Text
                Case "1" ' All Users
                    .DataSource = Membership.GetAllUsers()
                Case "2" ' Find by Username
                    .DataSource = Membership.FindUsersByName(txtFilter.Text & rblMatch.SelectedValue)
                Case "3" ' Find by Email
                    .DataSource = Membership.FindUsersByEmail(txtFilter.Text & rblMatch.SelectedValue)
            End Select
            .PageIndex = PageIndex
            .DataBind()
        End With
    End Sub

 
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        RefreshMembers(e.NewPageIndex)
    End Sub


    Protected Sub btnFindByUsername_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindByUsername.Click
        lblAction.Text = "2"
        RefreshMembers()
    End Sub

    Protected Sub btnFindByEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindByEmail.Click
        lblAction.Text = "3"
        RefreshMembers()
    End Sub

    Protected Sub btnShowAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowAll.Click
        lblAction.Text = "1"
        RefreshMembers()
    End Sub

    Protected Sub btnCreateUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateUser.Click, btnCreateUser2.Click
        Response.Redirect("~/backoffice/user.aspx")
    End Sub
End Class
