Imports Utility

Partial Class productlist
    Inherits System.Web.UI.Page


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Protected Sub Bind()
		hfdCategoryID.Value = 0
		If (Request.QueryString("CategoryID") & "").Length > 0 Then
			hfdCategoryID.Value = Convert.ToInt32(Request.QueryString("CategoryID"))
		End If

		'Get First Category
		If Not hfdCategoryID.Value > 0 Then
			Dim r As CategoryDataSet.CategoryRow
			Dim t As New CategoryDataSet.CategoryDataTable
			t = (New CategoryDataSetTableAdapters.CategoryTableAdapter).GetDataByParentID(0, 2)
			If t.Rows.Count > 0 Then
				r = t.Rows(0)
				hfdCategoryID.Value = r.CategoryID
			End If

		End If

		If MyCulture() = MyCulture_EN Then
			locMenuTitle.Text = "Category"
		End If
	End Sub
End Class
