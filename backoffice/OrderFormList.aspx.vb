Imports Utility

Partial Class backoffice_OrderFormList
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Protected Sub Bind()
		ViewState("PaymentMethod") = (New PaymentMethodDataSetTableAdapters.PaymentMethodLangTableAdapter).GetData
		Clear()
		Search()
	End Sub

	Protected Sub gvOrderForm_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvOrderForm.PageIndexChanged
		Search()
	End Sub

	Protected Sub gvOrderForm_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvOrderForm.RowCommand
		If e.CommandName = "btnDetail" Then
			Response.Redirect(String.Format("~/backoffice/OrderForm.aspx?OrderNumber={0}", e.CommandArgument))
		End If
	End Sub

	Protected Sub Clear()
		txtOrderNumber.Text = ""
		txtOrderDateFrom.Text = ""
		txtOrderDateTo.Text = ""
		txtCustomerName.Text = ""
		txtContactPhone.Text = ""
		ddlOrderStatus.SelectedValue = ""
	End Sub

	Protected Sub Search()
		Dim SelectSql As String = "SELECT * FROM [view_OrderFormOrderStatusLang]"
		Dim WhereSql As String = ""
		Dim OrderSql As String = " ORDER BY [OrderDate] DESC, [OrderNumber]"
		Dim a As String = ""
		If txtOrderNumber.Text.Length > 0 Then
			WhereSql = WhereSql & a & "[OrderNumber] LIKE '%" & txtOrderNumber.Text & "%'"
			a = " AND "
		End If
		If txtOrderDateFrom.Text.Length > 0 Then
			WhereSql = WhereSql & a & "[OrderDate] >= '" & txtOrderDateFrom.Text & "'"
			a = " AND "
		End If
		If txtOrderDateTo.Text.Length > 0 Then
			WhereSql = WhereSql & a & "[OrderDate] < DATEADD(DAY,1,'" & txtOrderDateTo.Text & "')"
			a = " AND "
		End If
		If txtCustomerName.Text.Length > 0 Then
			WhereSql = WhereSql & a & "[CustomerName] LIKE '%" & txtCustomerName.Text & "%'"
			a = " AND "
		End If
		If txtContactPhone.Text.Length > 0 Then
			WhereSql = WhereSql & a & "[ContactPhone] LIKE '%" & txtContactPhone.Text & "%'"
			a = " AND "
		End If
		If ddlOrderStatus.SelectedValue.Length > 0 Then
			WhereSql = WhereSql & a & "[OrderStatus] = '" & ddlOrderStatus.SelectedValue & "'"
			a = " AND "
		End If

		WhereSql = WhereSql & a & "[Lang] = '" & MyCulture_HK & "'"

		If WhereSql.Length > 0 Then
			WhereSql = " WHERE " & WhereSql
		End If
		dsOrderForm.SelectCommand = SelectSql & WhereSql & OrderSql
		gvOrderForm.DataBind()
	End Sub

	Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
		Search()
	End Sub

	Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
		Clear()
	End Sub

	Protected Function DisplayPaymentMethod(ByVal PaymentMethodId As Integer) As String
		Dim s As String = ""
		Dim r As PaymentMethodDataSet.PaymentMethodLangRow
		Dim t As New PaymentMethodDataSet.PaymentMethodLangDataTable
		If ViewState("PaymentMethod") IsNot Nothing Then
			t = ViewState("PaymentMethod")
		End If

		If t.Rows.Count > 0 Then
			For Each r In t.Rows
				If r.PaymentMethodId = PaymentMethodId And r.Lang = MyCulture_HK Then
					s = r.LangPaymentMethod
					Exit For
				End If
			Next
		End If
		Return s
	End Function
End Class
