Imports Microsoft.VisualBasic
Imports System.Data
Imports Utility

Public Class OrderClass

	Public Shared ReadOnly OrderStatus_PAYMENT_PENDING As String = "PAYMENT_PENDING"
	Public Shared ReadOnly OrderStatus_ORDER_CANCEL As String = "ORDER_CANCEL"
	Public Shared ReadOnly OrderStatus_ORDER_COMPLETED As String = "ORDER_COMPLETED"
	Public Shared ReadOnly OrderStatus_PROCESSING As String = "PROCESSING"
	Public Shared ReadOnly OrderStatus_PAYMENT_PAID As String = "PAYMENT_PAID"
	Public Shared ReadOnly OrderStatus_REFUNDED As String = "REFUNDED"


	Public Function InsertOrder(ByVal ShipInfo As ShipInfoClass) As String
		Dim Cart As DsCart = GetShoppingCart()
		Dim nextOrderNo As New SeedClass

		Dim orderID As Integer = 0
		Dim orderItemID As Integer = 0
		Dim orderNo As String = ""

		If Cart.RowCount > 0 And ShipInfo IsNot Nothing Then
			orderNo = nextOrderNo.NextOrderNo
			orderID = SPInsertOrder(CreateNewOrderRow(orderNo, ShipInfo, Cart))
			orderItemID = SPInsertOrderItem(orderNo, Cart)
			'mailClass.SendOrderNotification(orderNo)
		End If
		Return orderNo
	End Function

	Private Function CreateNewOrderRow(ByVal orderNo As String, ByVal ShipInfo As ShipInfoClass, ByVal Cart As DsCart) As OrderFormDataSet.OrderFormRow
		Dim r As OrderFormDataSet.OrderFormRow
		Dim table As New OrderFormDataSet.OrderFormDataTable

		r = table.NewRow
		OrderFormClass.InitRow(r)
		With r
			.OrderNumber = orderNo
			.OrderDate = DateTime.Now
			.CustomerName = ShipInfo.CustomerName
			.ContactPhone = ShipInfo.ContactPhone
			.DeliveryAddress = ShipInfo.DeliveryAddress
			.Country = ShipInfo.Country
			.Remark = ShipInfo.Remark
			.Email = ShipInfo.Email
			.PaymentMethod = ShipInfo.PaymentMethod
			.DeliveryMethod = ShipInfo.DeliveryMethod
			.OrderStatus = OrderStatus_PAYMENT_PENDING
			.TotalAmount = Cart.TotalAmount
			.Lang = MyCulture()
			.LastUpdateUser = ""
			.LastUpdateTime = DateTime.Now
		End With
		Return r
	End Function

	Private Function SPInsertOrder(ByVal r As OrderFormDataSet.OrderFormRow) As Integer
		Dim orderAdapter As New OrderFormDataSetTableAdapters.OrderFormTableAdapter
		Dim orderID As Integer
		orderID = orderAdapter.InsertQuery( _
		r.OrderNumber, _
		r.OrderDate, _
		r.CustomerName, _
		r.ContactPhone, _
		r.DeliveryAddress, _
		r.Country, _
		r.Email, _
		r.Remark, _
		r.PaymentMethod, _
		r.DeliveryMethod, _
		r.OrderStatus, _
		r.TotalAmount, _
		r.TransactionRefNo, _
		r.PaidDate, _
		r.PayPalStatus, _
		r.Lang, _
		r.LastUpdateUser, _
		DateTime.Now _
		)
		Return orderID
	End Function

	Private Function SPInsertOrderItem(ByVal OrderNo As String, ByVal Cart As DsCart) As Integer
		Dim r As DrCart
		Dim orderItemAdapter As New OrderFormDataSetTableAdapters.OrderItemTableAdapter
		Dim OrderItemID As Integer

		For Each r In Cart.Table.Rows
			OrderItemID = orderItemAdapter.InsertQuery( _
			OrderNo, _
			r.ProductId, _
			r.ProductCode, _
			r.ProductName, _
			r.Quantity, _
			r.Price, _
			r.ShippingFee)
		Next
		Return OrderItemID
	End Function


	'Public Sub UpdateOrderStatus(ByVal orderid As Integer, ByVal orderStatus As Integer, ByVal bankComment As String)
	'	Dim adapter As New OrderDataSetTableAdapters.OrderTableAdapter
	'	adapter.UpdateOrderStatus(bankComment, orderStatus, orderid)
	'End Sub

	'Public Shared Function PaymentMethodDescription(ByVal id As Integer, Optional ByVal Culture As String = "") As String
	'	If Culture = "" Then
	'		Culture = HttpContext.Current.Session("MyCulture")
	'	End If
	'	Dim s As String = ""
	'	Dim table As New OrderDataSet.PaymentMethodDataTable
	'	s = (New OrderDataSetTableAdapters.PaymentMethodTableAdapter).GetPaymentMethodDescription(Culture, id)
	'	Return s
	'End Function

	'Public Shared Function DisplayProductName(ByVal ProductCode As String) As String
	'	Dim s As String = ""
	'	Dim r As ProductDataSet.ProductRow
	'	Dim t As New ProductDataSet.ProductDataTable
	'	t = (New ProductDataSetTableAdapters.ProductTableAdapter).GetDataByProductCode(ProductCode, 1)
	'	If t.Rows.Count > 0 Then
	'		r = t.Rows(0)
	'		s = r.Name
	'	End If
	'	Return s
	'End Function

	'Public Shared Function DisplayProductSize(ByVal SizeID As Integer) As String
	'	Return (New ProductDataSetTableAdapters.ProductSizeTableAdapter).GetSize(SizeID)
	'End Function

	'Public Shared Function DisplayProductColor(ByVal ColorID As Integer) As String
	'	Return (New ProductDataSetTableAdapters.ProductColorTableAdapter).GetColorName(ColorID)
	'End Function
End Class

