Imports Microsoft.VisualBasic
Imports Utility

Public Class MemberDetailClass

	Public Shared UserRole_Member As String = "Member"
	Public Shared UserRole_Admin As String = "Admin"

	Public Shared Region_Local As String = "Local"
	Public Shared Region_Overseas As String = "Overseas"
	Public Shared Region_OverseasExpress As String = "OverseasExpress"

	Public Shared Sub InitRow(ByVal r As MemberDetailDataSet.MemberDetailRow)
		With r
			.UserID = ""
			.Gender = ""
			.Name = ""
			.Email = ""
			.ContactNo = ""
			.DeliveryAddress = ""
			.Country = Region_Local
			.Birthday = DateTimeNull
			.CreateDate = DateTime.Now
		End With
	End Sub

	Public Shared Function GetUserName(ByVal UserID As String) As String
		Dim s As String = ""
		Dim r As MemberDetailDataSet.MemberDetailRow
		Dim t As New MemberDetailDataSet.MemberDetailDataTable
		t = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter).GetDataByUserID(UserID)
		If t.Rows.Count > 0 Then
			r = t.Rows(0)
			s = r.Name
		End If
		Return s
	End Function
End Class
