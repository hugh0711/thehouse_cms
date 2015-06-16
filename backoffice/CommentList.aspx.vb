Imports Utility

Partial Class backoffice_CommentList
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
            Bind()
        Else
            Search()
        End If
	End Sub

	Protected Sub Bind()
		'Init Controls

	End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Search()
    End Sub

    Protected Sub Search()
        Dim SelectSql As String = "SELECT * FROM [Comment] "
        Dim WhereSql As String = ""
        Dim OrderSql As String = " ORDER BY [CommentDate] " & ddlSort.SelectedValue
        Dim a As String = ""

        'If radCommentType.SelectedValue.Length > 0 Then
        '    WhereSql = WhereSql & a & "[CommentType] = '" & radCommentType.SelectedValue & "'"
        '    a = " AND "
        'End If
        'If ddlUserID.SelectedValue.Length > 0 Then
        '    WhereSql = WhereSql & a & "[UserID] = '" & ddlUserID.SelectedValue & "'"
        '    a = " AND "
        'End If

        If txtDateFrom.Text.Length > 0 Then
            WhereSql = WhereSql & a & "[CommentDate] >= '" & txtDateFrom.Text & "'"
            a = " AND "
        End If
        If txtDateTo.Text.Length > 0 Then
            WhereSql = WhereSql & a & "[CommentDate] < DATEADD(Day, 1, '" & txtDateTo.Text & "')"
            a = " AND "
        End If

        If radIsInspected.SelectedValue.Length > 0 Then
            WhereSql = WhereSql & a & "[IsInspected] = " & radIsInspected.SelectedValue
            a = " AND "
        End If

        If radIsDisable.SelectedValue.Length > 0 Then
            WhereSql = WhereSql & a & "[IsDisable] = " & radIsDisable.SelectedValue
            a = " AND "
        End If

        If WhereSql.Length > 0 Then
            WhereSql = " WHERE " & WhereSql
        End If

        dsComment.SelectCommand = SelectSql & WhereSql & OrderSql
        gvComment.DataBind()
    End Sub

    Protected Sub gvComment_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvComment.RowCommand
        Dim CommentAdapter As New CommentDataSetTableAdapters.CommentTableAdapter()
        Dim CommentID As Integer = CInt(e.CommandArgument)

        Select Case e.CommandName
            Case "inspect"
                CommentAdapter.UpdateInspected(True, CommentID)
            Case "release"
                CommentAdapter.UpdateInspected(False, CommentID)
            Case "disable"
                CommentAdapter.UpdateDisabled(True, CommentID)
            Case "enable"
                CommentAdapter.UpdateDisabled(False, CommentID)
            Case "delete"
                CommentAdapter.Delete(CommentID)
        End Select
        gvComment.DataBind()
    End Sub

    Protected Function GetUrl(ByVal RefType As String, ByVal RefID As Integer) As String
        Dim fString As String = "{0}"

        Select Case RefType.ToLower()
            Case "episode", "product"
                fString = "~/episode.aspx?id={0}"
            Case "program"

            Case "channel"

        End Select

        Return String.Format(fString, RefID)
    End Function

    Protected Function DisplayCommentDescription(ByVal Description As String) As String
        Dim s As String = ""
        If Description.Length > 20 Then
            s = Description.Substring(0, 20) & "..."
        Else
            s = Description
        End If
        Return s
    End Function
End Class
