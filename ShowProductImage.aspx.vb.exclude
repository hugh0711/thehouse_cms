Imports System.Linq
Imports Newtonsoft.Json.Linq

Partial Class ShowProductImage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            hfdAlbumID.Value = Request.QueryString("aID")
            'MsgBox(Request.QueryString("cID"))
            If Not hfdAlbumID.Value = "" Then
                '<li data-preview="images/show_image/large/1.jpg"><a href="#"><img src="images/show_image/small/1.jpg" alt="image01" /></a></li>
                LoadSQLdata(New Guid(hfdAlbumID.Value))
                Dim imageItem As String = ""
                For Each row In SQLData
                    Dim url As String = row.Item("Url")
                    Dim big_image As String = String.Format("product_image/album/{0}/{1}", hfdAlbumID.Value, url)
                    Dim small_image As String = String.Format("product_image/album/{0}/tb/{1}", hfdAlbumID.Value, url)

                    imageItem &= String.Format("<li data-preview='{0}'><a href='#'><img src='{1}' alt='{2}'style='max-height:100px;max-width:80px;padding:5px;'/></a></li>", big_image, small_image, row.Item("Author"))
                Next
                lit_imageItem.Text = imageItem

                'MsgBox(imageItem)
            End If
        End If


    End Sub

    Dim SQLData As JArray
    Protected Sub LoadSQLdata(ByVal aID As Guid)
        Dim db As New CMSDataContext

        'SELECT [ProductID], [ProductName],[Author],[CameraModel],[Url] FROM [view_ProductImage] WHERE ([Enabled] = @Enabled) and [CategoryID]= @CategoryID ORDER BY [ModifyDate] DESC
        SQLData = New JArray(From u In db.view_AlbumPhotoInfos _
                                          Where u.AlbumID = aID And u.Enabled = True _
                                                           Select New JObject( _
                                                           New JProperty("ProductID", u.PhotoID), _
                                                           New JProperty("Author", u.Author), _
                                                           New JProperty("CameraModel", u.camera_model), _
                                                           New JProperty("Url", u.PhotoName)))
    End Sub

End Class
