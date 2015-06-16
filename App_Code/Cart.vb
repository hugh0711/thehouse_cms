Imports Microsoft.VisualBasic
Imports System.Data

Public Class DsCart
	Inherits DataSet

	Public Shared ReadOnly TableName As String = "[Cart]"

	Public CartIdFilter As Integer = 0

	Public Shared ReadOnly ColCartId As String = "CartId"
	Public Shared ReadOnly ColProductId As String = "ProductId"
	Public Shared ReadOnly ColProductCode As String = "ProductCode"
	Public Shared ReadOnly ColProductName As String = "ProductName"
	Public Shared ReadOnly ColQuantity As String = "Quantity"
	Public Shared ReadOnly ColPrice As String = "Price"
	Public Shared ReadOnly ColShippingFee As String = "ShippingFee"

	Protected m_Table As DataTable

	'Public Shared Sub ClassInit()
	'	SyncLock GetType(DsCart)
	'		DrCart.ClassInit()
	'	End SyncLock
	'End Sub

	Protected Sub Init()
		DataSetName = TableName
		Me.Prefix = ""
		Me.Namespace = "http://tempuri.org/" + TableName + ".xsd"
		Me.Locale = New System.Globalization.CultureInfo("en-US")
		Me.CaseSensitive = True
		Me.EnforceConstraints = False

		If Tables.Count <> 0 Then
			m_Table = CType(Me.Tables(TableName), GenericDataSetTable)
		Else
			m_Table = NewTable()
			Tables.Add(m_Table)
		End If
	End Sub

	Public Sub New()
		MyBase.New()
		Init()
	End Sub

	Public Sub New(ByVal name As String)
		MyBase.New(name)
		Init()
	End Sub

	Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
		MyBase.New(info, context)
		Init()
	End Sub

	Public Overrides Function Clone() As DataSet
		Dim cln As DsCart = CType(MyBase.Clone, DsCart)

		cln.Init()
		Return cln
	End Function

	Protected Overrides Function ShouldSerializeTables() As Boolean
		Return False
	End Function

	Protected Overrides Function ShouldSerializeRelations() As Boolean
		Return False
	End Function

	Protected Overridable Function RowType() As Type
		Return GetType(DrCart)

	End Function

	Protected Function NewTable() As GenericDataSetTable
		Dim t As New GenericDataSetTable(TableName)
		t.ActualRowType = RowType()
		t.Columns.Add("CartId", GetType(Integer))
		t.Columns("CartId").AutoIncrement = True
		t.Columns("CartId").AutoIncrementSeed = 1

		t.Columns.Add("ProductId", GetType(Integer))
		t.Columns.Add("ProductCode", GetType(String))
		t.Columns.Add("ProductName", GetType(String))
		t.Columns.Add("Quantity", GetType(Integer))
		t.Columns.Add("Price", GetType(Decimal))
		t.Columns.Add("ShippingFee", GetType(Decimal))
		Return t
	End Function

	Public Sub UpdateShippingFee(ByVal Country As String)
		Dim Carts As New DsCart
		Dim Cart As DrCart

		Dim NewCarts As New DsCart
		Dim NewCart As DrCart

		Dim Product As ProductDataSet.ProductRow
		Dim Products As New ProductDataSet.ProductDataTable
		Dim ShippingFee As Decimal = 0

		Carts = Utility.GetShoppingCart
		For Each Cart In Carts.Table.Rows
			'Get Shipping fee
			ShippingFee = ProductClass.GetShippingFee(Cart.ProductId, Country, Cart.Quantity)

			'Get Default shipping fee if no discount
			If Not ShippingFee > 0 Then
				Products = (New ProductDataSetTableAdapters.ProductTableAdapter).GetDataByProductID(Cart.ProductId, ConfigurationManager.AppSettings("FunctionIDProduct"))
				If Products.Rows.Count > 0 Then
					Product = Products.Rows(0)
					Select Case Country
						Case MemberDetailClass.Region_Local
							ShippingFee = Product.ShippingFee
						Case MemberDetailClass.Region_Overseas
							ShippingFee = Product.Weight
						Case MemberDetailClass.Region_OverseasExpress
							ShippingFee = Product.Height
					End Select
				End If
			End If

			'copy row to new cart
			NewCart = NewCarts.Table.NewRow
			NewCart.Reset()
			NewCart.ProductId = Cart.ProductId
			NewCart.ProductCode = Cart.ProductCode
			NewCart.ProductName = Cart.ProductName
			NewCart.Quantity = Cart.Quantity
			NewCart.Price = Cart.Price
			NewCart.ShippingFee = Cart.Quantity * ShippingFee
			NewCarts.AddRow(NewCart)

		Next
		Utility.SetShoppingCart(NewCarts)
	End Sub

#Region "COM-based client access methods"
	Public ReadOnly Property Table() As GenericDataSetTable
		Get
			Return m_Table
		End Get
	End Property

	Public Function RowAt(ByVal index As Integer) As Object
		Return m_Table.Rows(index)
	End Function

	Public ReadOnly Property RowCount() As Integer
		Get
			Return m_Table.Rows.Count
		End Get
	End Property

	Public Sub AddRow(ByVal row As DrCart)
		Dim ProductExisted As Boolean = False
		Dim r As DrCart
		For Each r In Table.Rows
			If r.ProductId = row.ProductId Then
				r.Quantity += row.Quantity
				r.Price += row.Price
				r.ShippingFee += row.ShippingFee
				ProductExisted = True
				Exit For
			End If
		Next

		If Not ProductExisted Then
			m_Table.Rows.Add(row)
		End If
	End Sub

	Public Sub InsertAt(ByVal row As DataRow, ByVal pos As Integer)
		m_Table.Rows.InsertAt(row, pos)
	End Sub

	Public Sub RemoveRow(ByVal row As DataRow)
		m_Table.Rows.Remove(row)
	End Sub

	Public Sub RemoveAt(ByVal index As Integer)
		m_Table.Rows.RemoveAt(index)
	End Sub

	Public Function TotalAmount(Optional ByVal withShippingFee As Boolean = True) As Decimal
		Dim r As DrCart
		Dim Total As Decimal = 0
		For Each r In Table.Rows
			If withShippingFee Then
				Total = Total + r.Price + r.ShippingFee
			Else
				Total = Total + r.Price
			End If
		Next
		Return Total
	End Function

	Public Sub RemoveProductByCartId(ByVal CartId As Integer)
		Dim r As DrCart
		For Each r In Table.Rows
			If r.CartId = CartId Then
				Table.Rows.Remove(r)
				Exit For
			End If
		Next
	End Sub
#End Region

End Class

Public Class DrCart
	Inherits DataRow

	Protected Shared ReadOnly m_CartIdIndex As Integer = 0

	Protected Shared ReadOnly m_ProductIdIndex As Integer = 1
	Protected Shared ReadOnly m_ProductCodeIndex As Integer = 2
	Protected Shared ReadOnly m_ProductNameIndex As Integer = 3
	Protected Shared ReadOnly m_QuantityIndex As Integer = 4
	Protected Shared ReadOnly m_PriceIndex As Integer = 5
	Protected Shared ReadOnly m_ShippingFeeIndex As Integer = 6

	'Public Shared Sub ClassInit()
	'	Dim i As Integer

	'	i = 0

	'	m_CartIdIndex = i
	'	i = i + 1

	'	m_ProductIdIndex = i
	'	i = i + 1
	'	m_ProductCodeIndex = i
	'	i = i + 1


	'End Sub

	Protected m_Table As GenericDataSetTable

	Public Sub New()
		' This method should never be called
		MyBase.New(Nothing)
	End Sub

	Public Sub New(ByVal rb As DataRowBuilder)
		MyBase.New(rb)
		Me.m_Table = CType(Me.Table, GenericDataSetTable)
	End Sub

	Public Property CartId() As Integer
		Get
			Return CType(Item(m_CartIdIndex), Integer)
		End Get
		Set(ByVal Value As Integer)
			Item(m_CartIdIndex) = Value
		End Set
	End Property

	Public Property ProductId() As Integer
		Get
			Return CType(Item(m_ProductIdIndex), Integer)
		End Get
		Set(ByVal Value As Integer)
			Item(m_ProductIdIndex) = Value
		End Set
	End Property

	Public Property ProductCode() As String
		Get
			Return CType(Item(m_ProductCodeIndex), String)
		End Get
		Set(ByVal Value As String)
			Item(m_ProductCodeIndex) = Value
		End Set
	End Property

	Public Property ProductName() As String
		Get
			Return CType(Item(m_ProductNameIndex), String)
		End Get
		Set(ByVal Value As String)
			Item(m_ProductNameIndex) = Value
		End Set
	End Property

	Public Property Quantity() As Integer
		Get
			Return CType(Item(m_QuantityIndex), Integer)
		End Get
		Set(ByVal Value As Integer)
			Item(m_QuantityIndex) = Value
		End Set
	End Property

	Public Property Price() As Decimal
		Get
			Return CType(Item(m_PriceIndex), Decimal)
		End Get
		Set(ByVal Value As Decimal)
			Item(m_PriceIndex) = Value
		End Set
	End Property

	Public Property ShippingFee() As Decimal
		Get
			Return CType(Item(m_ShippingFeeIndex), Decimal)
		End Get
		Set(ByVal Value As Decimal)
			Item(m_ShippingFeeIndex) = Value
		End Set
	End Property

	'Public Function ReadRow(ByVal rs As SqlDataReader, ByVal i As Integer) As Integer
	'Public Function ReadRow(ByVal rs As OleDbDataReader, ByVal i As Integer) As Integer
	'	Column1 = rs.GetString(i)
	'	i = i + 1

	'	Column2 = rs.GetDouble(i)
	'	i = i + 1

	'	Return i
	'End Function

	Public Sub Reset()
		'CartId = 0

		ProductId = 0
		ProductCode = ""
		ProductName = ""
		Quantity = 0
		Price = 0
		ShippingFee = 0

	End Sub
End Class
