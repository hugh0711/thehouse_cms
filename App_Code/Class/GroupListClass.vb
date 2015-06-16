Imports Microsoft.VisualBasic

Public Class GroupListClass
    Inherits List(Of GroupJsonClass)

    Public Sub New(TypeID As Integer)
        Load(TypeID)
    End Sub

    Public Sub Load(TypeID As Integer)
        Dim dt As UserProductDataSet.UserProductGroupDataTable = (New UserProductDataSetTableAdapters.UserProductGroupTableAdapter()).GetDataByTypeID(TypeID, 0)

        Me.Clear()
        Dim g As GroupJsonClass
        For Each dr As UserProductDataSet.UserProductGroupRow In dt.Rows
            g = New GroupJsonClass(dr)
            Me.Add(g)
        Next
    End Sub
End Class
