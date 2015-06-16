Imports System.Linq
Imports Newtonsoft.Json.Linq


Partial Class Portfolio_Sub
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            hfdCatID.Value = Request.QueryString("cID")
            If Not hfdCatID.Value = "" Then

            End If


        End If
    End Sub

 


    Protected Function ImageItem(CatID As Guid) As String

        Dim result As String = ""


        LoadSQLdata(CatID)

        If SQLData.Count > 0 Then

            For Each row In SQLData
                Dim url As String = row.Item("Url")
                Dim big_image As String = String.Format("product_image/album/{0}/{1}", CatID, url)
                Dim small_image As String = String.Format("product_image/album/{0}/tb/{1}", CatID, url)
                Dim author As String = ""
                If row.Item("Author") IsNot Nothing And row.Item("Author").ToString.Length > 0 Then
                    author = row.Item("Author")
                End If

                result &= String.Format("<li><a href='{0}'><img src='{1}' alt='Photo By {2}'/></a></li>", big_image, small_image, author)
            Next

        End If




        Return result

    End Function



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
