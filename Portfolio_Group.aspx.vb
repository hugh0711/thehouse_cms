Imports System.Linq
Imports Newtonsoft.Json.Linq

Partial Class Portfolio_Group
    Inherits System.Web.UI.Page


    Dim SQLData As JArray

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            hfdAlbumID.Value = Request.QueryString("aID")
            'LoadAllSQLdata()
        End If
    End Sub

    Protected Sub LoadSQLdata(ByVal cID As Integer)
        Dim db As New ProductDbDataContext

        'SELECT [ProductID], [ProductName],[Author],[CameraModel],[Url] FROM [view_ProductImage] WHERE ([Enabled] = @Enabled) and [CategoryID]= @CategoryID ORDER BY [ModifyDate] DESC
        SQLData = New JArray(From u In db.view_ProductImages _
                                          Where u.CategoryID = cID And u.Enabled = True And u.FunctionID = 2 _
                                                           Select New JObject( _
                                                           New JProperty("ProductID", u.ProductID), _
                                                           New JProperty("ProductName", u.ProductName), _
                                                           New JProperty("Author", u.Author), _
                                                           New JProperty("CameraModel", u.CameraModel), _
                                                           New JProperty("CategoryID", u.CategoryID), _
                                                           New JProperty("Url", u.Url)))
    End Sub

    Dim AllSQLData As JArray
    Protected Sub LoadAllSQLdata()
        Dim db As New ProductDbDataContext

        'SELECT [ProductID], [ProductName],[Author],[CameraModel],[Url] FROM [view_ProductImage] WHERE ([Enabled] = @Enabled) and [CategoryID]= @CategoryID ORDER BY [ModifyDate] DESC
        AllSQLData = New JArray(From u In db.view_ProductImages _
                                          Where u.Enabled = True And u.FunctionID = 2 _
                                                           Select New JObject( _
                                                           New JProperty("ProductID", u.ProductID), _
                                                           New JProperty("ProductName", u.ProductName), _
                                                           New JProperty("Author", u.Author), _
                                                           New JProperty("CameraModel", u.CameraModel), _
                                                           New JProperty("CategoryID", u.CategoryID), _
                                                           New JProperty("Url", u.Url)))
    End Sub

    Protected Function ImageItem(ByVal imageUrl As String, ByVal cID As Integer, ByVal category As String) As String


        Dim image As String = ""
        LoadSQLdata(cID)

        If SQLData.Count > 0 Then






            image &= String.Format("<img src='{0}' width='387' height='85' usemap='#image_map' border='0'>", imageUrl)

            image &= " <map name='image_map'>"

            

            For Each row In SQLData

                'If row.Item("CategoryID").ToString = cID Then
                image &= String.Format("<area rel='prettyPhoto[image_map]' shape='rect' coords='6,11,720,730' href='product_image/product/{0}' title='{1}'>", row.Item("Url"), row.Item("ProductName"))
                'End If


            Next

            'AllSQLData.Except(SQLData)
            'For Each other In AllSQLData
            '    Dim exclude_item As String = String.Format("<area rel='prettyPhoto[image_map]' shape='rect' coords='6,11,720,730' href='product_image/product/{0}' title='{1}'>", other.Item("Url"), other.Item("ProductName"))

            '    image.Replace(exclude_item, "")
            'Next

            image &= "</map>"

            Return image
        Else
            Return ""
        End If

    End Function


    
   



End Class
