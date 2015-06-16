Imports Microsoft.VisualBasic
Imports Utility

Public Class PayPalLogClass
	Public Shared Sub InitRow(ByVal r As PayPalLogDataSet.PayPalLogRow)
		With r
			.PayPalLogId = 0
			.receiver_email = ""
			.receiver_id = ""
			.residence_country = ""
			.transaction_subject = ""
			.txn_id = ""
			.txn_type = ""
			.payer_email = ""
			.payer_id = ""
			.payer_status = ""
			.first_name = ""
			.last_name = ""
			.address_city = ""
			.address_country = ""
			.address_country_code = ""
			.address_name = ""
			.address_state = ""
			.address_status = ""
			.address_street = ""
			.address_zip = ""
			.custom = ""
			.handling_amount = 0
			.item_name = ""
			.item_number = ""
			.mc_currency = ""
			.mc_fee = 0
			.mc_gross = 0
			.payment_date = DateTimeNull
			.payment_fee = 0
			.payment_gross = 0
			.payment_status = ""
			.payment_type = ""
			.quantity = 0
			.shipping = 0
			.tax = 0
			.verify_sign = ""
		End With
	End Sub

End Class
