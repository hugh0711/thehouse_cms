Imports Microsoft.VisualBasic

Public Class CategoryListClass
    Inherits List(Of CategoryJsonClass)

    Public Sub New()

    End Sub

    Public Sub New(ByVal CategoryTable As CategoryDataSet.view_CategoryDataTable, ByVal MaxCount As Integer)
        Convert(CategoryTable, Integer.MaxValue)
    End Sub

    Public Sub New(ByVal CategoryTable As CategoryDataSet.view_CategoryDataTable)
        Convert(CategoryTable)
    End Sub



    Public Sub Load(ByVal FunctionID As Integer, ByVal ParentID As Integer, Optional ByVal MaxCount As Integer = Integer.MaxValue, Optional HQ As Boolean = False, Optional Has3D As Boolean = True)
        Dim CategoryAdapter As New CategoryDataSetTableAdapters.view_CategoryTableAdapter()
        Dim CategoryTable As CategoryDataSet.view_CategoryDataTable
        Dim Lang As String = ConfigurationManager.AppSettings("UIDefaultLanguage")
        CategoryTable = CategoryAdapter.GetDataByFunctionIDParentIDEnabled(FunctionID, ParentID, Lang)
        Convert(CategoryTable, MaxCount, HQ, Has3D)
    End Sub


    Private Sub Convert(ByVal CategoryTable As CategoryDataSet.view_CategoryDataTable, Optional ByVal MaxCount As Integer = Integer.MaxValue, Optional HQ As Boolean = False, Optional Has3D As Boolean = True)
        Me.Clear()
        Dim p As CategoryJsonClass
        Dim count As Integer = 0
        Dim Channel3DCategoryID As Integer = CInt(ConfigurationManager.AppSettings("3DChannelCategoryID"))

        For Each row As CategoryDataSet.view_CategoryRow In CategoryTable
            If Has3D OrElse (Not Has3D AndAlso row.CategoryID <> Channel3DCategoryID) Then
                p = New CategoryJsonClass(row, HQ:=HQ)
                Me.Add(p)
                count += 1
                If count >= MaxCount Then
                    Exit For
                End If
            End If
        Next
    End Sub
End Class
