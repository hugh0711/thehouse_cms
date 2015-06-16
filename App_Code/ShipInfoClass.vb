Imports Microsoft.VisualBasic
Imports Utility

Public Class ShipInfoClass

	Private _CustomerName As String
	Private _ContactPhone As String
	Private _DeliveryAddress As String
	Private _Country As String
	Private _Email As String
	Private _Remark As String
	Private _PayemntMethod As Integer
	Private _DeliveryMethod As Integer

	Public Sub New()
		CustomerName = ""
		ContactPhone = ""
		DeliveryAddress = ""
		Country = MemberDetailClass.Region_Local
		Email = ""
		Remark = ""
		PaymentMethod = 0
		DeliveryMethod = 0
	End Sub

	Public Property CustomerName() As String
		Get
			Return _CustomerName
		End Get
		Set(ByVal value As String)
			_CustomerName = value
		End Set
	End Property

	Public Property ContactPhone() As String
		Get
			Return _ContactPhone
		End Get
		Set(ByVal value As String)
			_ContactPhone = value
		End Set
	End Property

	Public Property DeliveryAddress() As String
		Get
			Return _DeliveryAddress
		End Get
		Set(ByVal value As String)
			_DeliveryAddress = value
		End Set
	End Property

	Public Property Country() As String
		Get
			Return _Country
		End Get
		Set(ByVal value As String)
			_Country = value
		End Set
	End Property

	Public Property Email() As String
		Get
			Return _Email
		End Get
		Set(ByVal value As String)
			_Email = value
		End Set
	End Property

	Public Property Remark() As String
		Get
			Return _Remark
		End Get
		Set(ByVal value As String)
			_Remark = value
		End Set
	End Property

	Public Property PaymentMethod() As String
		Get
			Return _PayemntMethod
		End Get
		Set(ByVal value As String)
			_PayemntMethod = value
		End Set
	End Property

	Public Property DeliveryMethod() As String
		Get
			Return _DeliveryMethod
		End Get
		Set(ByVal value As String)
			_DeliveryMethod = value
		End Set
	End Property

End Class
