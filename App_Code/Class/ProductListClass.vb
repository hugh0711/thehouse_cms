Imports Microsoft.VisualBasic

Public Class ProductListClass
    Inherits List(Of ProductJsonClass)

    Public Sub New(Optional HQ As Boolean = False)

    End Sub

    Public Sub New(ByVal ProductTable As ProductDataSet.view_ProductImageCountDataTable, ByVal MaxCount As Integer, Optional HQ As Boolean = False)
        Convert(ProductTable, Integer.MaxValue, HQ)
    End Sub

    Public Sub New(ByVal ProductTable As ProductDataSet.view_ProductImageCountDataTable, Optional HQ As Boolean = False)
        Convert(ProductTable, Integer.MaxValue, HQ)
    End Sub

    Public Sub LoadLatestFirst(ByVal FunctionID As Integer, Optional ByVal MaxCount As Integer = Integer.MaxValue, Optional HQ As Boolean = False)
        Dim ProductAdapter As New ProductDataSetTableAdapters.view_ProductImageCountTableAdapter
        Dim ProductTable As ProductDataSet.view_ProductImageCountDataTable
        Dim Lang As String = ConfigurationManager.AppSettings("UIDefaultLanguage")
        ProductTable = ProductAdapter.GetDataLatestFirst(FunctionID, Lang)
        Convert(ProductTable, MaxCount, HQ)
    End Sub

    Public Sub LoadTopView(ByVal FunctionID As Integer, Optional ByVal MaxCount As Integer = Integer.MaxValue, Optional HQ As Boolean = False)
        Dim ProductAdapter As New ProductDataSetTableAdapters.view_ProductImageCountTableAdapter
        Dim ProductTable As ProductDataSet.view_ProductImageCountDataTable
        Dim Lang As String = ConfigurationManager.AppSettings("UIDefaultLanguage")
        ProductTable = ProductAdapter.GetDataTopView(FunctionID, Lang)
        Convert(ProductTable, MaxCount, HQ)
    End Sub

    Public Sub Load(ByVal FunctionID As Integer, Optional ByVal MaxCount As Integer = Integer.MaxValue, Optional HQ As Boolean = False)
        Dim ProductAdapter As New ProductDataSetTableAdapters.view_ProductImageCountTableAdapter
        Dim ProductTable As ProductDataSet.view_ProductImageCountDataTable
        Dim Lang As String = ConfigurationManager.AppSettings("UIDefaultLanguage")
        ProductTable = ProductAdapter.GetDataByFunctionID(FunctionID, Lang)
        Convert(ProductTable, MaxCount, HQ)
    End Sub

    Public Sub LoadByCategory(ByVal FunctionID As Integer, ByVal CategoryID As Integer, Optional HQ As Boolean = False)
        Dim ProductAdapter As New ProductDataSetTableAdapters.view_ProductImageCountTableAdapter
        Dim ProductTable As ProductDataSet.view_ProductImageCountDataTable
        Dim Lang As String = ConfigurationManager.AppSettings("UIDefaultLanguage")
        ProductTable = ProductAdapter.GetDataByFunctionIDCategoryID(FunctionID, CategoryID, Lang)
        Convert(ProductTable, Integer.MaxValue, HQ)
    End Sub

    Public Sub SearchByKeyword(ByVal FunctionID As Integer, q As String, Optional MaxCount As Integer = Integer.MaxValue, Optional HQ As Boolean = False)
        Dim ProductAdapter As New ProductDataSetTableAdapters.view_ProductImageTableAdapter
        Dim ProductTable As ProductDataSet.view_ProductImageDataTable
        Dim Lang As String = ConfigurationManager.AppSettings("UIDefaultLanguage")
        q = String.Format("%{0}%", q)
        ProductTable = ProductAdapter.GetDataBySearch(q, Lang, FunctionID)
        Convert(ProductTable, MaxCount, HQ)
    End Sub

    Public Sub SearchByTag(ByVal FunctionID As Integer, Tag As String, Optional MaxCount As Integer = Integer.MaxValue, Optional HQ As Boolean = False)
        Dim TagID As Integer = (New TagDataSetTableAdapters.view_TagTableAdapter()).GetTagID(Tag)
        Me.SearchByTagID(FunctionID, TagID, MaxCount, HQ)
    End Sub

    Public Sub SearchByTagID(ByVal FunctionID As Integer, TagID As Integer, Optional MaxCount As Integer = Integer.MaxValue, Optional HQ As Boolean = False)
        Dim ProductAdapter As New ProductDataSetTableAdapters.view_ProductImageTagTableAdapter
        Dim ProductTable As ProductDataSet.view_ProductImageTagDataTable
        Dim Lang As String = ConfigurationManager.AppSettings("UIDefaultLanguage")
        ProductTable = ProductAdapter.GetDataByTagID(TagID, Lang, FunctionID)
        Convert(ProductTable, MaxCount, HQ)
    End Sub

#Region "Convert"
    Private Sub Convert(ByVal ProductTable As ProductDataSet.view_ProductImageCountDataTable, MaxCount As Integer, HQ As Boolean)
        Me.Clear()
        Dim p As ProductJsonClass
        Dim count As Integer = 0
        For Each row As ProductDataSet.view_ProductImageCountRow In ProductTable
            p = New ProductJsonClass(row, MaxCount, HQ)
            Me.Add(p)
            count += 1
            If count >= MaxCount Then
                Exit For
            End If
        Next
    End Sub

    Private Sub Convert(ByVal ProductTable As ProductDataSet.view_ProductImageDataTable, MaxCount As Integer, HQ As Boolean)
        Me.Clear()
        Dim p As ProductJsonClass
        Dim count As Integer = 0
        For Each row As ProductDataSet.view_ProductImageRow In ProductTable
            p = New ProductJsonClass(row, MaxCount, HQ)
            Me.Add(p)
            count += 1
            If count >= MaxCount Then
                Exit For
            End If
        Next
    End Sub

    Private Sub Convert(ByVal ProductTable As ProductDataSet.view_ProductImageTagDataTable, MaxCount As Integer, HQ As Boolean)
        Me.Clear()
        Dim p As ProductJsonClass
        Dim count As Integer = 0
        For Each row As ProductDataSet.view_ProductImageTagRow In ProductTable
            p = New ProductJsonClass(row, MaxCount, HQ)
            Me.Add(p)
            count += 1
            If count >= MaxCount Then
                Exit For
            End If
        Next
    End Sub
#End Region

End Class
