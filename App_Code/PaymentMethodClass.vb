Imports Microsoft.VisualBasic
Imports Utility

Public Class PaymentMethodClass

	Public Shared ReadOnly PaymentMethodID_Bank As Integer = 1
	Public Shared ReadOnly PaymentMethodID_PayPal As Integer = 2

	Public Shared Sub InitRow(ByVal r As PaymentMethodDataSet.PaymentMethodRow)
		With r
			.PaymentMethodId = 0
			.PaymentMethodName = ""
			.PaymentPage = ""
			.ImageUrl = ""
			.Sort = 0
		End With
	End Sub

	Public Shared Sub InitRow(ByVal r As PaymentMethodDataSet.PaymentMethodLangRow, ByVal Culture As String)
		With r
			.LangPaymentMethodId = 0
			.PaymentMethodId = 0
			.Lang = Culture
			.LangPaymentMethod = ""
			.LangDescription = ""
		End With
	End Sub
End Class
