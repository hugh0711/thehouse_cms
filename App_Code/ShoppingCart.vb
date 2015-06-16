Imports System.Data

Public Class ShoppingCart

	Public Shared Function GetCart() As DataTable
		Dim t As New DataTable
		If HttpContext.Current.Session("Cart") Is Nothing Then
			t = makeCart()
			HttpContext.Current.Session("Cart") = t
		Else
			t = HttpContext.Current.Session("Cart")
		End If

		Return t
	End Function

	Public Shared Function makeCart() As DataTable
		Dim Cart As New DataTable("Cart")
		With Cart
			.Columns.Add("ID", GetType(Integer))
			.Columns("ID").AutoIncrement = True
			.Columns("ID").AutoIncrementSeed = 1

			.Columns.Add("ProductID", GetType(Integer))
			.Columns.Add("ProductCode", GetType(String))
			.Columns.Add("ProductName", GetType(String))
			.Columns.Add("Quantity", GetType(Integer))
			.Columns.Add("Price", GetType(Decimal))
			.Columns.Add("ShippingFee", GetType(Decimal))
		End With
		Return Cart
	End Function

	Public Shared Sub AddToCart(ByRef Cart As DataTable, ByVal ProductID As Integer, ByVal ProductCode As String, ByVal ProductName As String, ByVal Quantity As Integer, ByVal Price As Decimal, ByVal ShippingFee As Decimal)
		Dim ProductExisted As Boolean = False
		Dim dr As DataRow

		For Each dr In Cart.Rows
			If dr("ProductID") = ProductID Then
				dr("Quantity") += Quantity
				ProductExisted = True
				Exit For
			End If
		Next

		If Not ProductExisted Then
			dr = Cart.NewRow()
			dr("ProductID") = ProductID
			dr("ProductCode") = ProductCode
			dr("ProductName") = ProductName
			dr("Quantity") = Quantity
			dr("Price") = Price
			dr("ShippingFee") = ShippingFee
			Cart.Rows.Add(dr)
		End If
	End Sub

	Public Shared Sub RemoveFromCart(ByRef Cart As DataTable, ByVal ProductID As Integer)
		Dim dr As DataRow

		For Each dr In Cart.Rows
			If dr("ProductID") = ProductID Then
				Cart.Rows.Remove(dr)
				Exit For
			End If
		Next
	End Sub

	Public Shared Sub SetQuantity(ByRef Cart As DataTable, ByVal ProductID As Integer, ByVal Quantity As Integer)
		Dim dr As DataRow

		For Each dr In Cart.Rows
			If dr("ProductID") = ProductID Then
				dr("Quantity") = Quantity
				Exit For
			End If
		Next
	End Sub

	Public Shared Function GetTotalAmount(ByVal Cart As DataTable) As Decimal
		Dim dr As DataRow
		Dim Amount As Decimal = 0
		'ShippingFee = 0

		For Each dr In Cart.Rows
			Amount += (dr("Price") * dr("Quantity"))
			'ShippingFee += (dr("ShippingFee") * dr("Quantity"))
		Next

		'If Not Amount < 100.0 Then
		'	ShippingFee = 0
		'End If
		Return Amount
	End Function

	Public Shared Function Existed(ByVal Cart As DataTable, ByVal ProductID As Integer) As Boolean
		Try
			Dim q = From item In Cart.Rows Where item("ProductID") = ProductID
			Return (q.ToArray().Count > 0)
		Catch ex As Exception
			Return False
		End Try
	End Function

	Public Shared Sub CheckOut(ByVal Cart As DataTable)

	End Sub
End Class
