Imports Microsoft.VisualBasic
Imports Utility

Public Class OrderFormClass

	Public Shared Sub InitRow(ByVal r As OrderFormDataSet.OrderFormRow)
		With r
			.OrderID = 0
			.OrderNumber = ""
			.OrderDate = DateTime.Now
			.CustomerName = ""
			.ContactPhone = ""
			.DeliveryAddress = ""
			.Country = MemberDetailClass.Region_Local
			.Email = ""
			.PaymentMethod = 0
			.DeliveryMethod = 0
			.OrderStatus = OrderClass.OrderStatus_PAYMENT_PENDING
			.TransactionRefNo = ""
			.PaidDate = DateTimeNull
			.PayPalStatus = ""
			.LastUpdateUser = ""
			.LastUpdateTime = DateTime.Now
		End With
	End Sub


End Class
