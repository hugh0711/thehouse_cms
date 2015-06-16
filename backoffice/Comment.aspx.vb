Imports Utility

Partial Class backoffice_Comment
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Public Sub Bind()
		Dim CommentID As Integer = 0
		If (Request("CommentID") & "").Length > 0 Then
			CommentID = Convert.ToInt32(Request("CommentID"))
		End If

		Dim r As CommentDataSet.CommentRow
		Dim t As New CommentDataSet.CommentDataTable
		t = (New CommentDataSetTableAdapters.CommentTableAdapter).GetDataByCommentID(CommentID)
		If t.Rows.Count > 0 Then
			r = t.Rows(0)
			With r
				hfdCommentID.Value = .CommentID
				txtCommentType.Text = .CommentType
				txtReferenceID.Text = .ReferenceID
				txtUserID.Text = .UserID
				txtCommentDate.Text = DateTimeToString(.CommentDate)
				txtCommentDescription.Text = .CommentDescription
				chkIsInspected.Checked = .IsInspected
				chkIsDisable.Checked = .IsDisable
			End With
		End If
	End Sub

	'-- Code Behind Update --
	Public Sub Update()
		Dim adapter As New CommentDataSetTableAdapters.CommentTableAdapter
        adapter.Update( _
         txtCommentType.Text _
         , Convert.ToInt32(txtReferenceID.Text) _
         , txtUserID.Text _
         , StringToDateTime(txtCommentDate.Text) _
         , txtCommentDescription.Text _
         , chkIsInspected.Checked _
         , chkIsDisable.Checked _
         , 0 _
         , "" _
         , "" _
         , "" _
         , Convert.ToInt32(hfdCommentID.Value) _
         )
	End Sub

	Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
		Dim adapter As New CommentDataSetTableAdapters.CommentTableAdapter
		adapter.Delete(hfdCommentID.Value)
		BackPageAction()
	End Sub

	Protected Sub BackPageAction()
		Response.Redirect("~/backoffice/CommentList.aspx")
	End Sub

	Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
		BackPageAction()
	End Sub

	Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
		Update()
		BackPageAction()
	End Sub
End Class
