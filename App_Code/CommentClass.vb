Imports Microsoft.VisualBasic
Imports Utility

Public Class CommentClass

	Public Shared ReadOnly CommentType_Product As String = "PRODUCT"
	Public Shared ReadOnly CommentType_Articles As String = "ARTICLES"

	Public Shared Sub InitRow(ByVal r As CommentDataSet.CommentRow, ByVal CommentType As String)
		With r
			.CommentID = 0
			.CommentType = CommentType
			.ReferenceID = 0
			.UserID = ""
			.CommentDate = DateTime.Now
			.CommentDescription = ""
			.IsInspected = False
			.IsDisable = False
		End With
	End Sub

	Public Shared Sub PostComment(ByVal ReferenceID As Integer, ByVal CommentType As String, ByVal CommentMsg As String, ByVal UserID As String)
		Dim adapter As New CommentDataSetTableAdapters.CommentTableAdapter
        adapter.Insert(CommentType, ReferenceID, UserID, DateTime.Now, CommentMsg, False, False, 0, "", "", "")
	End Sub

End Class
