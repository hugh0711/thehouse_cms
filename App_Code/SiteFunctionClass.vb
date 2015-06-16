Imports Microsoft.VisualBasic

<Serializable()> _
Public Class SiteFunctionClass

#Region "Property"

    Public Property FunctionID As Integer
    Public Property FunctionName As String
    Public Property Enabled As Boolean
    Public Property HasCategory As Boolean
    Public Property DefaultCategoryID As Integer
    Public Property CategoryThumbnailWidth As Integer
    Public Property CategoryThumbnailHeight As Integer
    Public Property CategoryThumbnailNoImage As String
    Public Property CategoryMaxLevel As Integer
    Public Property HasCategoryImage As Boolean
    Public Property CategoryImageWidth As Integer
    Public Property CategoryImageHeight As Integer
    Public Property CategoryImageNoImage As String
    Public Property ProductThumbnailWidth As Integer
    Public Property ProductThumbnailHeight As Integer
    Public Property ProductThumbnailNoImage As String
    Public Property HasProductImage As Boolean
    Public Property ProductImageWidth As Integer
    Public Property ProductImageHeight As Integer
    Public Property ProductImageCount As Integer
    Public Property ProductImageNoImage As String
    Public Property AllowUser As String
    Public Property SortOrder As Integer
    Public Property HasProductCode As String
    Public Property HasDetails As Boolean
    Public Property HasPrice As Boolean
    Public Property HasDateRange As Boolean
    Public Property HasTag As Boolean
    Public Property HasFile As Boolean
    Public Property HasDescription As Boolean
    Public Property HTMLDescription As Boolean
    Public Property IsCategoryImageOptional As Boolean
    Public Property IsProductImageOptional As Boolean
    Public Property CategoryIOSImageWidth As Integer
    Public Property CategoryIOSImageHeight As Integer
    Public Property CategoryAndroidImageWidth As Integer
    Public Property CategoryAndroidImageHeight As Integer
    Public Property ProductIOSThumbnailWidth As Integer
    Public Property ProductIOSThumbnailHeight As Integer
    Public Property ProductIOSImageWidth As Integer
    Public Property ProductIOSImageHeight As Integer
    Public Property ProductAndroidThumbnailWidth As Integer
    Public Property ProductAndroidThumbnailHeight As Integer
    Public Property ProductAndroidImageWidth As Integer
    Public Property ProductAndroidImageHeight As Integer
    Public Property CategoryImageRatio As Single
    Public Property ProductImageRatio As Single
    Public Property CategoryCaptionList As String
    Public Property CategoryCaption As String()
    Public Property hasUrl As Boolean
    Public Property hasVideo As Boolean

#End Region

#Region "Constructor"

    Public Sub New()

    End Sub

    Public Sub New(ByVal FunctionID As Integer)
        Me.Load(FunctionID)
    End Sub
#End Region

#Region "DB Related"

    Public Sub Load(ByVal FunctionID As Integer)
        Dim FunctionAdapter As New SiteDataSetTableAdapters.SiteFunctionTableAdapter()
        Dim FunctionTable As SiteDataSet.SiteFunctionDataTable
        Dim FunctionRow As SiteDataSet.SiteFunctionRow

        FunctionTable = FunctionAdapter.GetDataByID(FunctionID)
        If FunctionTable.Rows.Count Then
            FunctionRow = FunctionTable.Rows(0)
            With FunctionRow
                Me.FunctionID = .FunctionID
                Me.FunctionName = .FunctionName
                Me.Enabled = .Enabled
                Me.HasCategory = .hasCategory
                Me.DefaultCategoryID = .DefaultCategoryID
                Me.CategoryThumbnailWidth = .CategoryThumbnailWidth
                Me.CategoryThumbnailHeight = .CategoryThumbnailHeight
                Me.CategoryThumbnailNoImage = .CategoryThumbnailNoImage
                Me.CategoryMaxLevel = .CategoryMaxLevel
                Me.HasCategoryImage = .hasCategoryImage
                Me.CategoryImageWidth = .CategoryImageWidth
                Me.CategoryImageHeight = .CategoryImageHeight
                Me.CategoryImageNoImage = .CategoryImageNoImage
                Me.ProductThumbnailWidth = .ProductThumbnailWidth
                Me.ProductThumbnailHeight = .ProductThumbnailHeight
                Me.ProductThumbnailNoImage = .ProductThumbnailNoImage
                Me.HasProductImage = .hasProductImage
                Me.ProductImageWidth = .ProductImageWidth
                Me.ProductImageHeight = .ProductImageHeight
                Me.ProductImageCount = .ProductImageCount
                Me.ProductImageNoImage = .ProductImageNoImage
                Me.SortOrder = .SortOrder
                Me.AllowUser = .AllowUser
                Me.HasProductCode = .hasProductCode
                Me.HasDetails = .hasDetails
                Me.HasPrice = .hasPrice
                Me.HasDateRange = .hasDateRange
                Me.HasTag = .hasTag
                Me.HasFile = .hasFile
                Me.HasDescription = .hasDescription
                Me.HTMLDescription = .HTMLDescription
                Me.IsCategoryImageOptional = .CategoryImageOptional
                Me.IsProductImageOptional = .ProductImageOptional
                Me.CategoryIOSImageWidth = .CategoryIOSImageWidth
                Me.CategoryIOSImageHeight = .CategoryIOSImageHeight
                Me.CategoryAndroidImageWidth = .CategoryAndroidImageWidth
                Me.CategoryAndroidImageHeight = .CategoryAndroidImageHeight
                Me.ProductIOSThumbnailWidth = .ProductIOSThumbnailWidth
                Me.ProductIOSThumbnailHeight = .ProductIOSThumbnailHeight
                Me.ProductIOSImageWidth = .ProductIOSImageWidth
                Me.ProductIOSImageHeight = .ProductIOSImageHeight
                Me.ProductAndroidThumbnailWidth = .ProductAndroidThumbnailWidth
                Me.ProductAndroidThumbnailHeight = .ProductAndroidThumbnailHeight
                Me.ProductAndroidImageWidth = .ProductAndroidImageWidth
                Me.ProductAndroidImageHeight = .ProductAndroidImageHeight
                Me.CategoryImageRatio = CategoryImageWidth / CategoryImageHeight
                Me.ProductImageRatio = ProductImageWidth / ProductImageHeight
                If Not .IsCategoryCaptionListNull Then
                    Me.CategoryCaptionList = .CategoryCaptionList
                    Me.CategoryCaption = .CategoryCaptionList.Split(",")
                End If
                Me.hasUrl = .hasUrl
                Me.hasVideo = .hasVideo
            End With
        End If
    End Sub

#End Region

End Class


