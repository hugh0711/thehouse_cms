Imports Microsoft.VisualBasic
Imports System.IO

Public Class ProductClass

    Public Sub New()

    End Sub

    Public Shared Sub Delete(ByVal ProductID As Integer)
        Dim ProductAdapter As New ProductDataSetTableAdapters.ProductTableAdapter()
        ProductAdapter.Delete(ProductID)
        Dim CategoryAdapter As New ProductDataSetTableAdapters.CategoryProductTableAdapter()
        CategoryAdapter.DeleteByProductID(ProductID)
        Dim SizeAdapter As New ProductDataSetTableAdapters.ProductSizeTableAdapter()
        SizeAdapter.DeleteByProductID(ProductID)
        Dim TagAdapter As New TagDataSetTableAdapters.ProductTagTableAdapter()
        TagAdapter.DeleteByProductID(ProductID)

        Dim db As New ProductDbDataContext()

        'Delete EventDetails
        Dim foundEventDetail = From p In db.EventDetails
                                Where p.ProductID = ProductID
                                Select p
        If foundEventDetail.Count > 0 Then
            For Each imageUrl In foundEventDetail
                DeleteProductImage(imageUrl.MenuUrl)
            Next
            db.EventDetails.DeleteAllOnSubmit(foundEventDetail)
        End If
        'End delete EventDetails

        'Delete EventUsers
        Dim foundEventUsers = From p In db.EventUsers
                                Where p.EventID = ProductID
                                Select p
        If foundEventUsers.Count > 0 Then
            db.EventUsers.DeleteAllOnSubmit(foundEventUsers)
        End If
        'End delete EventUsers

        'Delete ProductImages
        Dim foundProductImage = From p In db.ProductImages
                                Where p.ProductID = ProductID
                                Select p
        If foundProductImage.Count > 0 Then
            For Each imageUrl In foundProductImage
                DeleteProductImage(imageUrl.Url)
            Next
            db.ProductImages.DeleteAllOnSubmit(foundProductImage)
        End If
        'End delete ProductImages

        'Delete ProductDetails
        Dim foundProductDetail = From p In db.ProductDetails
                                Where p.ProductID = ProductID
                                Select p
        If foundProductDetail.Count > 0 Then
            For Each imageUrl In foundEventDetail
                DeleteProductImage(imageUrl.MenuUrl)
            Next
            db.ProductDetails.DeleteAllOnSubmit(foundProductDetail)
        End If
        'End delete ProductDetails

        'Delete ProductNames
        Dim foundProductName = From p In db.ProductNames
                                Where p.ProductID = ProductID
                                Select p
        If foundProductName.Count > 0 Then
            db.ProductNames.DeleteAllOnSubmit(foundProductName)
        End If
        'End delete ProductNames

        'Delete ProductNames
        Dim foundProductRelated = From p In db.ProductRelateds
                                Where p.ProductID = ProductID
                                Select p
        If foundProductRelated.Count > 0 Then
            db.ProductRelateds.DeleteAllOnSubmit(foundProductRelated)
        End If
        'End delete ProductNames

        'Delete RestaurantDetails
        Dim foundRestaurantDetail = From p In db.RestaurantDetails
                                Where p.ProductID = ProductID
                                Select p
        If foundRestaurantDetail.Count > 0 Then
            db.RestaurantDetails.DeleteAllOnSubmit(foundRestaurantDetail)
        End If
        'End delete RestaurantDetails

        'Delete RestaurantEventDateTimes
        Dim foundRestaurantEventDateTime = From p In db.RestaurantEventDateTimes
                                Where p.ProductID = ProductID
                                Select p
        If foundRestaurantEventDateTime.Count > 0 Then
            db.RestaurantEventDateTimes.DeleteAllOnSubmit(foundRestaurantEventDateTime)
        End If
        'End delete RestaurantEventDateTimes

        Try
            db.SubmitChanges()
        Catch ex As Exception
        End Try

    End Sub

    Public Shared Sub DeleteProductImage(ByVal inputFileName As String)
        Dim deleteImagePathDict As New Dictionary(Of Integer, String)
        deleteImagePathDict.Add(1, ConfigurationManager.AppSettings("ProductOriginalImagePath"))
        deleteImagePathDict.Add(2, ConfigurationManager.AppSettings("ProductThumbnailPath"))
        deleteImagePathDict.Add(3, ConfigurationManager.AppSettings("ProductImagePath"))
        Dim Filename As String = ""
        Dim ImagePath As String = ""
        For Each deletePath In deleteImagePathDict
            Filename = Path.Combine(deletePath.Value, inputFileName)
            ImagePath = HttpContext.Current.Server.MapPath(Filename)
            If File.Exists(ImagePath) Then
                File.Delete(ImagePath)
            End If
        Next
    End Sub

    Public Shared Function GetImageThumbnail(ByVal ProductID As Integer) As String
        Dim s As String = ConfigurationManager.AppSettings("ProductEmptyThumbnail")
        Dim r As ProductDataSet.ProductImageRow
        Dim t As New ProductDataSet.ProductImageDataTable
        t = (New ProductDataSetTableAdapters.ProductImageTableAdapter).GetDataByProductID(ProductID)
        If t.Rows.Count > 0 Then
            r = t.Rows(0)
            s = System.IO.Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), r.Url)
        End If
        Return s
    End Function

    Public Shared Function GetShippingFee(ByVal ProductID As Integer, ByVal Country As String, ByVal Quantity As Integer) As Decimal
        Dim ShippingFee As Decimal = 0
        Dim t As New ProductDataSet.ShippingFeeDataTable
        Dim r As ProductDataSet.ShippingFeeRow
        t = (New ProductDataSetTableAdapters.ShippingFeeTableAdapter).GetDataByQty(ProductID, Quantity)
        If t.Rows.Count > 0 Then
            r = t.Rows(0)
            Select Case Country
                Case MemberDetailClass.Region_Local
                    ShippingFee = r.Local
                Case MemberDetailClass.Region_Overseas
                    ShippingFee = r.OverSeas
                Case MemberDetailClass.Region_OverseasExpress
                    ShippingFee = r.OverSeasExpress
            End Select
        End If
        Return ShippingFee
    End Function
End Class
