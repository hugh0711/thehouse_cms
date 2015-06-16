Imports Utility

Partial Class backoffice_OrderForm
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Protected Sub Bind()
		Dim OrderNumber As String = ""
		If (Request("OrderNumber") & "").Length > 0 Then
			OrderNumber = Request("OrderNumber")
		End If

		'Init Control

		Dim r As OrderFormDataSet.OrderFormRow
		Dim t As New OrderFormDataSet.OrderFormDataTable
		t = (New OrderFormDataSetTableAdapters.OrderFormTableAdapter).GetDataByOrderNumber(OrderNumber)
		If t.Rows.Count > 0 Then
			r = t.Rows(0)
			With r
				hfdOrderId.Value = .OrderID
				txtOrderNumber.Text = .OrderNumber
				txtOrderDate.Text = DateTimeToString(.OrderDate, False)
				txtCustomerName.Text = .CustomerName
				txtContactPhone.Text = .ContactPhone
				txtDeliveryAddress.Text = Replace(.DeliveryAddress, vbCrLf, "<br />")
				txtCountry.Text = (.Country)
				txtEmail.Text = .Email
				txtRemark.Text = Replace(.Remark, vbCrLf, "<br />")
				txtTotalAmount.Text = String.Format("{0:n2}", .TotalAmount)
				ddlOrderStatus.SelectedValue = .OrderStatus
				hfdPaymentMethodId.Value = .PaymentMethod
				txtPaymentMthod.Text = (New PaymentMethodDataSetTableAdapters.PaymentMethodLangTableAdapter).GetPaymentMethod(hfdPaymentMethodId.Value, Default_Culture)
				txtTransactionRefNo.Text = .TransactionRefNo
				txtPaidDate.Text = IIf(.PaidDate = DateTimeNull, "", DateTimeToString(.PaidDate))
				txtPayPalStatus.Text = .PayPalStatus
				txtLastUpdateUser.Text = .LastUpdateUser
				txtLastUpdateTime.Text = DateTimeToString(.LastUpdateTime)
				If r.PaymentMethod = PaymentMethodClass.PaymentMethodID_PayPal And r.TransactionRefNo.Length > 0 Then
					btnTransactionDetail.Visible = True
				Else
					btnTransactionDetail.Visible = False
				End If
			End With
		Else

		End If

	End Sub

	Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
		BackPageAction()
	End Sub

	Protected Function DisplayProductImage(ByVal ProductId As Integer) As String
		Dim s As String = ""
		Dim r As ProductDataSet.ProductImageRow
		Dim t As New ProductDataSet.ProductImageDataTable
		t = (New ProductDataSetTableAdapters.ProductImageTableAdapter).GetDataByProductID(ProductId)
		If t.Rows.Count > 0 Then
			r = t.Rows(0)
			s = System.IO.Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), r.Url)
		End If
		Return s
	End Function

	Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
		Dim adapter As New OrderFormDataSetTableAdapters.OrderFormTableAdapter
		Dim PaidDate As DateTime
		If txtPaidDate.Text.Length > 0 Then
			PaidDate = StringToDateTime(txtPaidDate.Text)
		Else
			PaidDate = DateTimeNull
		End If
		'adapter.UpdateOrderStatus(ddlOrderStatus.SelectedValue, Membership.GetUser.UserName, DateTime.Now, txtOrderNumber.Text)
		adapter.UpdateTransaction(ddlOrderStatus.SelectedValue, txtTransactionRefNo.Text, PaidDate, txtPayPalStatus.Text, Membership.GetUser.UserName, DateTime.Now, txtOrderNumber.Text)
		BackPageAction()
	End Sub

	Protected Sub BackPageAction()
		Response.Redirect("~/backoffice/OrderFormList.aspx")
	End Sub

	Protected Sub btnTransactionDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransactionDetail.Click
		Response.Redirect(String.Format("~/backoffice/PayPalLog.aspx?TransactionRef={0}", txtTransactionRefNo.Text))
	End Sub
End Class
