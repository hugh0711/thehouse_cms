Imports Utility

Partial Class master_MasterPageService
    Inherits System.Web.UI.MasterPage

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Protected Sub Bind()
		Dim ID As Integer

		If Request("id") IsNot Nothing AndAlso Request("id") <> "" Then
			ID = CInt(Request("id"))
		ElseIf Request.QueryString("url") IsNot Nothing AndAlso Request.QueryString("url") <> "" Then
			ID = (New PageDataSetTableAdapters.PageTableAdapter()).GetPageIDByUrl(Request.QueryString("url"))
		Else
			Response.StatusCode = 404
			Response.End()
		End If
		Dim parentID As Integer = 0
		parentID = (New PageDataSetTableAdapters.view_PageTableAdapter).GetParentID(ID)
		If Not parentID > 0 Then
			Dim PageID As Integer = 0
			PageID = (New PageDataSetTableAdapters.PageTableAdapter).GetDefaultPageID(ID)
			If PageID > 0 Then
				Response.Redirect(String.Format("~/page.aspx?id={0}", PageID))
			End If
		End If

		If MyCulture() = MyCulture_EN Then
			locMenuTitle.Text = "Our Services"
		End If
	End Sub
End Class

