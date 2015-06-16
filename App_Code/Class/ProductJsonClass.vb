Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

<JsonObject()> _
Public Class ProductJsonClass

    <JsonProperty()> Public ID As Integer
    <JsonProperty()> Public Title As String
    <JsonProperty()> Public Description As String
    <JsonProperty()> Public ImageUrl As String



#Region "New"
    Public Sub New(ByVal ProductRow As ProductDataSet.view_ProductImageCountRow, Optional ByVal MaxChar As Integer = 160, Optional HQ As Boolean = False)
        LoadData(ProductRow, MaxChar, HQ)
    End Sub

    Public Sub New(ByVal ProductRow As ProductDataSet.view_ProductImageRow, Optional ByVal MaxChar As Integer = 160, Optional HQ As Boolean = False)
        LoadData(ProductRow, MaxChar, HQ)
    End Sub

    Public Sub New(ByVal ProductRow As ProductDataSet.view_ProductImageTagRow, Optional ByVal MaxChar As Integer = 160, Optional HQ As Boolean = False)
        LoadData(ProductRow, MaxChar, HQ)
    End Sub

    Public Sub New(ByVal ProductID As Integer, Optional ByVal MaxChar As Integer = 160, Optional HQ As Boolean = False)
        Dim daProduct As New ProductDataSetTableAdapters.view_ProductImageCountTableAdapter()
        Dim dtProduct As ProductDataSet.view_ProductImageCountDataTable
        Dim drProduct As ProductDataSet.view_ProductImageCountRow
        Dim Lang As String = ConfigurationManager.AppSettings("UIDefaultLanguage")

        dtProduct = daProduct.GetDataByProductID(ProductID, Lang)
        If dtProduct.Rows.Count > 0 Then
            drProduct = dtProduct.Rows(0)
            LoadData(drProduct, MaxChar, HQ)
        End If
    End Sub
#End Region

#Region "LoadData"
    Public Sub LoadData(ByVal ProductRow As ProductDataSet.view_ProductImageCountRow, Optional ByVal MaxChar As Integer = 160, Optional HQ As Boolean = False)
        With ProductRow
            Me.ID = .ProductID
            Me.Title = .ProductName
            If MaxChar = Integer.MaxValue Then
                Me.Description = .Description
            Else
                Me.Description = Utility.TrimHtmlText(.Description, MaxChar)
            End If

            If .FunctionID = CInt(ConfigurationManager.AppSettings("VideoFunctionID")) Then
                If .IsMOQUnitNull Then
                    Me.ImageUrl = ""
                Else
                    If HQ Then
                        Me.ImageUrl = VideoClass.GetHQPreview(.MOQUnit)
                    Else
                        Me.ImageUrl = VideoClass.GetPreview(.MOQUnit)
                    End If
                End If
            Else
                If .IsUrlNull OrElse String.IsNullOrWhiteSpace(.Url) Then
                    Me.ImageUrl = ""
                Else
                    If HQ Then
                        Me.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("ProductImagePath"), .Url)
                    Else
                        Me.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), .Url)
                    End If
                End If
            End If
        End With
    End Sub

    Public Sub LoadData(ByVal ProductRow As ProductDataSet.view_ProductImageRow, Optional ByVal MaxChar As Integer = 160, Optional HQ As Boolean = False)
        With ProductRow
            Me.ID = .ProductID
            Me.Title = .ProductName
            If MaxChar = Integer.MaxValue Then
                Me.Description = .Description
            Else
                Me.Description = Utility.TrimHtmlText(.Description, MaxChar)
            End If

            If .FunctionID = CInt(ConfigurationManager.AppSettings("VideoFunctionID")) Then
                If .IsMOQUnitNull Then
                    Me.ImageUrl = ""
                Else
                    If HQ Then
                        Me.ImageUrl = VideoClass.GetHQPreview(.MOQUnit)
                    Else
                        Me.ImageUrl = VideoClass.GetPreview(.MOQUnit)
                    End If
                End If
            Else
                If .IsUrlNull OrElse String.IsNullOrWhiteSpace(.Url) Then
                    Me.ImageUrl = ""
                Else
                    If HQ Then
                        Me.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("ProductImagePath"), .Url)
                    Else
                        Me.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), .Url)
                    End If
                End If
            End If
        End With
    End Sub

    Public Sub LoadData(ByVal ProductRow As ProductDataSet.view_ProductImageTagRow, Optional ByVal MaxChar As Integer = 160, Optional HQ As Boolean = False)
        With ProductRow
            Me.ID = .ProductID
            Me.Title = .ProductName
            If MaxChar = Integer.MaxValue Then
                Me.Description = .Description
            Else
                Me.Description = Utility.TrimHtmlText(.Description, MaxChar)
            End If

            If .FunctionID = CInt(ConfigurationManager.AppSettings("VideoFunctionID")) Then
                If .IsMOQUnitNull Then
                    Me.ImageUrl = ""
                Else
                    If HQ Then
                        Me.ImageUrl = VideoClass.GetHQPreview(.MOQUnit)
                    Else
                        Me.ImageUrl = VideoClass.GetPreview(.MOQUnit)
                    End If
                End If
            Else
                If .IsUrlNull Then
                    Me.ImageUrl = ""
                Else
                    If HQ Then
                        Me.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("ProductImagePath"), .Url)
                    Else
                        Me.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), .Url)
                    End If
                End If
            End If
        End With
    End Sub
#End Region

End Class
