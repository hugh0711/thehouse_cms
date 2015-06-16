Imports Utility

Partial Class backoffice_PaymentMethodList
    Inherits System.Web.UI.Page

	Dim PaymentSearchTable As New PaymentMethodDataSet.view_PaymentMethodDataTable

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Protected Sub Bind()
		PaymentSearchTable = (New PaymentMethodDataSetTableAdapters.view_PaymentMethodTableAdapter).GetDataByLang(Default_Culture)
	End Sub

	Protected Sub ReorderList1_ItemCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.ItemCommand
		If e.CommandName = "btnEdit" Then
			Response.Redirect(String.Format("~/backoffice/PaymentMethod.aspx?PaymentMethodId={0}&FormMode={1}", e.CommandArgument, FORMMODE_EDIT))
		End If

	End Sub

	Protected Function FindPaymentName(ByVal PaymentMethodId As Integer) As String
		Dim s As String = ""
		Dim r As PaymentMethodDataSet.view_PaymentMethodRow
		If PaymentSearchTable.Rows.Count > 0 Then
			For Each r In PaymentSearchTable.Rows
				If r.PaymentMethodId = PaymentMethodId Then
					s = r.LangPaymentMethod
					Exit For
				End If
			Next
		End If
		Return s
	End Function
End Class
