Imports Utility

Partial Class backoffice_PayPalLog
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Public Sub Bind()
		Dim TransactionRef As String = ""
		If (Request("TransactionRef") & "").Length > 0 Then
			TransactionRef = Request("TransactionRef")
		End If

		Dim r As PayPalLogDataSet.PayPalLogRow
		Dim t As New PayPalLogDataSet.PayPalLogDataTable
		t = (New PayPalLogDataSetTableAdapters.PayPalLogTableAdapter).GetDataBytxn_id(TransactionRef)
		If t.Rows.Count > 0 Then
			r = t.Rows(0)
			With r
				txtPayPalLogId.Text = .PayPalLogId
				txtreceiver_email.Text = .receiver_email
				txtreceiver_id.Text = .receiver_id
				txtresidence_country.Text = .residence_country
				txttransaction_subject.Text = .transaction_subject
				txttxn_id.Text = .txn_id
				txttxn_type.Text = .txn_type
				txtpayer_email.Text = .payer_email
				txtpayer_id.Text = .payer_id
				txtpayer_status.Text = .payer_status
				txtfirst_name.Text = .first_name
				txtlast_name.Text = .last_name
				txtaddress_city.Text = .address_city
				txtaddress_country.Text = .address_country
				txtaddress_country_code.Text = .address_country_code
				txtaddress_name.Text = .address_name
				txtaddress_state.Text = .address_state
				txtaddress_status.Text = .address_status
				txtaddress_street.Text = .address_street
				txtaddress_zip.Text = .address_zip
				txtcustom.Text = .custom
				txthandling_amount.Text = .handling_amount
				txtitem_name.Text = .item_name
				txtitem_number.Text = .item_number
				txtmc_currency.Text = .mc_currency
				txtmc_fee.Text = .mc_fee
				txtmc_gross.Text = .mc_gross
				txtpayment_date.Text = DateTimeToString(.payment_date)
				txtpayment_fee.Text = .payment_fee
				txtpayment_gross.Text = .payment_gross
				txtpayment_status.Text = .payment_status
				txtpayment_type.Text = .payment_type
				txtquantity.Text = .quantity
				txtshipping.Text = .shipping
				txttax.Text = .tax
				txtverify_sign.Text = .verify_sign
			End With
		End If
	End Sub

	Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
		Response.Redirect(String.Format("~/backoffice/OrderForm.aspx?OrderNumber={0}", txtitem_number.Text))
	End Sub
End Class
