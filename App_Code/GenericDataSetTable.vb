Imports Microsoft.VisualBasic
Imports System.Data

Public Class GenericDataSetTable
	Inherits DataTable
	Implements System.Collections.IEnumerable

	Protected m_ActualRowType As Type

	Public Sub New()
		MyBase.New()
	End Sub

	Public Sub New(ByVal name As String)
		MyBase.New(name)
	End Sub

	Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
		MyBase.New(info, context)
	End Sub

	Public Property ActualRowType() As Type
		Get
			Return m_ActualRowType
		End Get
		Set(ByVal Value As Type)
			m_ActualRowType = Value
		End Set
	End Property

	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		Return Me.Rows.GetEnumerator
	End Function

	Protected Overrides Function GetRowType() As Type
		Return ActualRowType
	End Function

	Protected Overrides Function NewRowFromBuilder(ByVal builder As DataRowBuilder) As DataRow
		Dim args As Object() = {builder}
		Return Activator.CreateInstance(m_ActualRowType, args)
	End Function

	Protected Overrides Function CreateInstance() As DataTable
		Return New GenericDataSetTable()
	End Function
End Class
