Imports Microsoft.VisualBasic
Imports System.IO

Public Class SaveProductImage
    Public Shared Function SaveProductImage(ByVal source_url As String, ByVal ProductImageWidth As Integer, ByVal ProductImageHeight As Integer, ByVal ProductThumbnailWidth As Integer, ByVal ProductThumbnailHeight As Integer) As String

        'set image source
        Dim Source As String = source_url
        ' Get extension from source
        Dim extension As String = ".jpg"
        Dim IsSupportFormat As Boolean = False
        If Path.GetExtension(source_url).ToString().Contains("jpg") Then
            extension = ".jpg"
        ElseIf Path.GetExtension(source_url).ToString().Contains("png") Then
            extension = ".png"
        ElseIf Path.GetExtension(source_url).ToString().Contains("gif") Then
            extension = ".gif"
        End If

        'set product image path
        Dim OriginalPathToFiles = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings("ProductOriginalImagePath"))
        'set product image path
        Dim UploadPathToFiles = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings("UploadPath"))
        'set new GUIDs 
        Dim sGUID As String
        sGUID = System.Guid.NewGuid.ToString()
        'set file name
        Dim file_name As String = String.Format("{0}{1}", sGUID, extension)
        'set file full path
        Dim file_full_path As String = Path.Combine(String.Format("{0}/{1}", OriginalPathToFiles, file_name))

        Using Client As New System.Net.WebClient

            Dim MyImage As System.Drawing.Image

            MyImage = GetProductImageFromWeb(Source)

            If MyImage Is Nothing Then
                file_name = ""
            Else

                'save image to ProductOriginalImagePath
                MyImage.Save(file_full_path)

                file_full_path = Path.Combine(String.Format("{0}/{1}", UploadPathToFiles, file_name))
                'save image to UploadPath
                MyImage.Save(file_full_path)

                Dim w1, h1, x1, y1 As Integer

                w1 = MyImage.Width
                h1 = MyImage.Height
                x1 = 0
                y1 = 0


                Dim img As System.Drawing.Image
                img = ImageClass.ResizeImage(MyImage, New Drawing.Size(ProductImageWidth, ProductImageHeight))

                Dim Filename = sGUID & ".jpg"
                Dim ProductPath = Path.Combine(ConfigurationManager.AppSettings("ProductImagePath"), Filename)
                ImageClass.SaveJPGWithCompressionSetting(img, HttpContext.Current.Server.MapPath(ProductPath), CInt(ConfigurationManager.AppSettings("JpegCompression")))
                Dim width As Integer = img.Width
                Dim height As Integer = img.Height

                Dim tb As System.Drawing.Image = ImageClass.ResizeImage(img, New Drawing.Size(ProductThumbnailWidth, ProductThumbnailHeight))
                ProductPath = Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), Filename)
                ImageClass.SaveJPGWithCompressionSetting(tb, HttpContext.Current.Server.MapPath(ProductPath), CInt(ConfigurationManager.AppSettings("JpegCompression")))
                Dim tbWidth As Integer = tb.Width
                Dim tbHeight As Integer = tb.Height
                img.Dispose()
                tb.Dispose()
            End If


        End Using

        Return file_name

    End Function

    Public Shared Function GetProductImageFromWeb(ByVal URL As String) As System.Drawing.Image
        Dim Request As System.Net.HttpWebRequest
        Dim Response As System.Net.HttpWebResponse
        Request = System.Net.WebRequest.Create(URL)
        Try
            Response = CType(Request.GetResponse, System.Net.WebResponse)
            If Request.HaveResponse Then
                If Response.StatusCode = Net.HttpStatusCode.OK Then
                    GetProductImageFromWeb = System.Drawing.Image.FromStream(Response.GetResponseStream)
                End If
            End If
            Try
            Catch e As System.Net.WebException
                MsgBox("A web exception has occured [" & URL & "]." & vbCrLf & " System returned: " & e.Message, MsgBoxStyle.Exclamation, "Error!")
                Exit Try
            Catch e As System.Net.ProtocolViolationException
                MsgBox("A protocol violation has occured [" & URL & "]." & vbCrLf & "  System returned: " & e.Message, MsgBoxStyle.Exclamation, "Error!")
                Exit Try
            Catch e As System.Net.Sockets.SocketException
                MsgBox("Socket error [" & URL & "]." & vbCrLf & "  System returned: " & e.Message, MsgBoxStyle.Exclamation, "Error!")
                Exit Try
            Catch e As System.IO.EndOfStreamException
                MsgBox("An IO stream exception has occured. System returned: " & e.Message, MsgBoxStyle.Exclamation, "Error!")
                Exit Try
            Finally
            End Try
        Catch ex As Exception

        End Try
        
    End Function

End Class
