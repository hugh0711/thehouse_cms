Imports System.IO
Imports System.Drawing

Imports System.Collections.Generic
Imports System.Linq
Imports System.Net

Imports Facebook
Imports Facebook.Web

Partial Class backoffice_albumphotos
    Inherits System.Web.UI.Page

    Protected Shared AlbumPath As String
    Protected Shared AlbumTBPath As String


    'Public tokens As Dictionary(Of String, String)
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Not String.IsNullOrWhiteSpace(Request.QueryString("album")) Then
                hfdAlbumID.Value = Request.QueryString("album")

                Session("UserID") = Request.QueryString("album")

                AlbumPath = Utility.GetAlbumPath(hfdAlbumID.Value, ImageClass.ImageSize.Normal)
                AlbumTBPath = Utility.GetAlbumPath(hfdAlbumID.Value, ImageClass.ImageSize.Thumbnail)

                'facebook Authorization()
                'tokens = CheckAuthorization()
                Session("tokens") = CheckAuthorization()

            End If
        End If
    End Sub


    Protected Function CheckAuthorization() As Dictionary(Of String, String)
        Dim app_id = ConfigurationManager.AppSettings("FacebookAppID")
        Dim app_secret = ConfigurationManager.AppSettings("FacebookAppSecret")
        Dim scope As String = "publish_stream,manage_pages,user_photos,photo_upload,friends_photos"


        Dim strURL As String = HttpContext.Current.Request.Url.AbsoluteUri

        Dim tokens As Dictionary(Of String, String)

        Dim code As String = Request.QueryString("code")

        If (Request.QueryString("code") = "") Then

            Response.Redirect(String.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}", app_id, strURL, scope))

        Else

            tokens = New Dictionary(Of String, String)()

            Dim url As String = String.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                                              app_id, strURL, scope, code, app_secret)


            Dim request As HttpWebRequest = WebRequest.Create(url)


            Using response As HttpWebResponse = request.GetResponse()

                Try

                Catch ex As Exception

                End Try
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream())

                Dim vals As String = reader.ReadToEnd()

                For Each token In vals.Split("&")

                    'meh.aspx?token1=steve&token2=jake&...
                    tokens.Add(token.Substring(0, token.IndexOf("=")),
                        token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1))
                Next

            End Using
        End If


        Return tokens

    End Function

    Protected Sub AjaxFileUpload1_UploadComplete(sender As Object, e As AjaxControlToolkit.AjaxFileUploadEventArgs) 'Handles AjaxFileUpload1.UploadComplete

        ' Generate file path
        Dim PhotoID As Guid = Guid.NewGuid()
        'Dim AlbumID As New Guid(hfdAlbumID.Value)
        Dim AlbumID As New Guid(Session.Item("UserID").ToString)

        Dim Filename As String = PhotoID.ToString() & ".jpg"
        Dim JpegCompression As Long = CLng(ConfigurationManager.AppSettings("JpegCompression"))
        Dim FilePath As String = Path.Combine(AlbumPath, Filename)
        Dim TempFile As String = Path.Combine(ConfigurationManager.AppSettings("UploadPath"), Path.GetFileNameWithoutExtension(IO.Path.GetRandomFileName) & Path.GetExtension(e.FileName))

        ' Save upload file to the file system
        AjaxFileUpload1.SaveAs(MapPath(TempFile))
        Dim Image As Image = Image.FromFile(MapPath(TempFile))
        Dim NormalSize As New Size(CInt(ConfigurationManager.AppSettings("AlbumPhotoWidth")), CInt(ConfigurationManager.AppSettings("AlbumPhotoHeight")))
        Dim Image1 As Image = ImageClass.ResizeImage(Image, NormalSize)
        ImageClass.SaveJPGWithCompressionSetting(Image1, MapPath(FilePath), JpegCompression)
        Image1.Dispose()

        Image = Image.FromFile(MapPath(TempFile))
        Dim ThumbnailSize As New Size(CInt(ConfigurationManager.AppSettings("AlbumPhotoThumbnailWidth")), CInt(ConfigurationManager.AppSettings("AlbumPhotoThumbnailHeight")))
        Dim Image2 As Image = ImageClass.ResizeImage(Image, ThumbnailSize, , , ImageClass.CropOption.FillTheArea)
        FilePath = Path.Combine(AlbumPath, "tb\" & Filename)
        ImageClass.SaveJPGWithCompressionSetting(Image2, MapPath(FilePath), JpegCompression)
        Image2.Dispose()
        Image.Dispose()


        'find original file name and author name and split
        Dim o_filename = e.FileName.Split("_")

        Dim sortOrder As Integer = Convert.ToInt32(o_filename(0))
        Dim authorName As String = o_filename(1)



        Dim image_info As Goheer.EXIF.EXIFextractor

        'save image EXIF information
        image_info = CheckEXIF(MapPath(TempFile))








       

        Dim db As New CMSDataContext
        Dim p As New AlbumPhoto With {
            .PhotoID = PhotoID,
            .AlbumID = AlbumID,
            .PhotoName = Filename,
            .Description = "",
            .SortOrder = sortOrder,
            .CreateDate = Now(),
            .CreatedBy = User.Identity.Name,
            .UpdateDate = Now(),
            .UpdatedBy = User.Identity.Name
        }
        db.AlbumPhotos.InsertOnSubmit(p)
        db.SubmitChanges()

        ' If Album does not have cover image, add to it
        Try
            Dim q = (From a In db.Albums Where a.AlbumID = AlbumID And a.PreviewUrl = "" Select a).First()
            If q IsNot Nothing Then
                q.PreviewUrl = Filename
                db.SubmitChanges()
            End If
        Catch ex As Exception
        End Try


        'Insert photo EXIF information
        InsertPhotoInfo(PhotoID, image_info, authorName)



        'Upload photo to FB Album or create new album
        UploadPhotoToFB(PhotoID, Session("tokens"), MapPath(TempFile))


        'Delete Original image file
        Try
            File.Delete(MapPath(TempFile))
        Catch
        End Try

        'LoadData()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(String.Format("~/backoffice/album.aspx?album={0}", hfdAlbumID.Value))
    End Sub

    Protected Sub UploadPhotoToFB(PhotoID As Guid, tokens As Dictionary(Of String, String), pathToFiles As String)

        Dim db As New CMSDataContext

        'Get photo information
        Dim photo_info = (From a In db.view_AlbumPhotoInfos Where a.PhotoID = PhotoID Select a).FirstOrDefault


        Dim access_token As String = tokens("access_token")

        access_token = tokens("access_token")


        Dim client = New FacebookClient(access_token)

        '/**************Check albums exist*****************/

        Dim albums As JsonObject = client.Get("/me/albums")


        'Album ID
        Dim FB_result As New JsonObject

        Dim exist As Boolean = False
        'Check album exist or not
        For Each album In albums.Item("data")
            'MsgBox(album("name").ToString)
            If album("name").ToString = photo_info.AlbumName Then
                FB_result = album
                exist = True

            End If

        Next



        'Dim image_name As String = "854.jpg"
        'Dim file = String.Format("~/product_image/product/{0}", image_name)
        'Dim pathToFiles = HttpContext.Current.Server.MapPath(file)

        'MsgBox(result.Item("id").ToString)
        If exist = True Then
            UploadPhotoToFB(client, FB_result.Item("id").ToString, pathToFiles, pathToFiles, photo_info.Author)
        Else
            FB_result = CreateAlbum(client, photo_info.AlbumName)

            UploadPhotoToFB(client, FB_result.Item("id").ToString, pathToFiles, pathToFiles, photo_info.Author)
        End If


    End Sub


    Protected Function CreateAlbum(client As FacebookClient, AlbumName As String) As JsonObject


        'Create new album
        Dim album_info As New Dictionary(Of String, String)
        album_info.Add("name", AlbumName)
        'album_info.Add("picture", "http://www.shop.hidra.com.tr/wp-content/uploads/high-resolution-photo_7292_1.jpg")
        Return client.Post("/me/albums", album_info)


    End Function

    Protected Sub UploadPhotoToFB(client As FacebookClient, AlbumID As String, image_name As String, image_path As String, Author As String)

        Dim PhotoName As String = String.Format("Photo by {0}", Author)

        'Upload new photo
        Dim media As New Facebook.FacebookMediaObject
        media.ContentType = "image/jpeg"
        media.FileName = image_name

        Dim filebytes As Byte() = System.IO.File.ReadAllBytes(image_path)
        media.SetValue(filebytes)
        Dim upload As New Dictionary(Of String, Object)
        upload.Add("name", PhotoName)
        'upload.Add("message", "This is my first uploaded image")
        upload.Add("@file.jpg", media)

        client.Post(String.Format("/{0}/photos", AlbumID), upload)


    End Sub


    Protected Sub InsertPhotoInfo(PhotoID As Guid, image_info As Goheer.EXIF.EXIFextractor, authorName As String)
        Dim db As New CMSDataContext


        Dim camera_model As String
        Dim f_stop As String
        Dim exposure_time As String
        Dim iso_speed As String
        Dim exposure_bias As String
        Dim focal_length As String
        Dim max_aperture As String
        Dim meeting_mode As String
        Dim subject_distance As String
        Dim flash_mode As String


        Try
            camera_model = image_info.Item("Equip Model").ToString
        Catch ex As Exception
            camera_model = ""
        End Try

        Try
            f_stop = image_info.Item("F-Number").ToString
        Catch ex As Exception
            f_stop = ""
        End Try

        Try
            exposure_time = image_info.Item("Exposure Time").ToString
        Catch ex As Exception
            exposure_time = ""
        End Try

        Try
            iso_speed = image_info.Item("ISO Speed").ToString
        Catch ex As Exception
            iso_speed = ""
        End Try

        Try
            exposure_bias = image_info.Item("Exposure Bias").ToString
        Catch ex As Exception
            exposure_bias = ""
        End Try

        Try
            focal_length = image_info.Item("FocalLength").ToString
        Catch ex As Exception
            focal_length = ""
        End Try

        Try
            max_aperture = image_info.Item("MaxAperture").ToString
        Catch ex As Exception
            max_aperture = ""
        End Try

        Try
            meeting_mode = image_info.Item("Metering Mode").ToString
        Catch ex As Exception
            meeting_mode = ""
        End Try

        Try
            subject_distance = image_info.Item("Subject Loc").ToString
        Catch ex As Exception
            subject_distance = ""
        End Try

        Try
            flash_mode = image_info.Item("Flash").ToString
        Catch ex As Exception
            flash_mode = ""
        End Try


        Dim ap_info As New AlbumPhoto_info With {
            .PhotoID = PhotoID,
            .camera_model = If(camera_model.Length > 0, camera_model, ""),
            .f_stop = If(camera_model.Length > 0, camera_model, ""),
            .exposure_time = If(exposure_time.Length > 0, exposure_time, ""),
            .iso_speed = If(iso_speed.Length > 0, iso_speed, ""),
            .exposure_bias = If(exposure_bias.Length > 0, exposure_bias, ""),
            .focal_length = If(focal_length.Length > 0, focal_length, ""),
            .max_aperture = If(max_aperture.Length > 0, max_aperture, ""),
            .meeting_mode = If(meeting_mode.Length > 0, meeting_mode, ""),
            .subject_distance = If(subject_distance.Length > 0, subject_distance, ""),
        .flash_mode = If(flash_mode.Length > 0, flash_mode, ""),
            .Author = authorName
        }

        db.AlbumPhoto_infos.InsertOnSubmit(ap_info)
        db.SubmitChanges()


    End Sub


    Protected Function CheckEXIF(file_path As String) As Goheer.EXIF.EXIFextractor


        '/// "Exif IFD"
        '/// "Gps IFD"
        '/// "New Subfile Type"
        '/// "Subfile Type"
        '/// "Image Width"
        '/// "Image Height"
        '/// "Bits Per Sample"
        '/// "Compression"
        '/// "Photometric Interp"
        '/// "Thresh Holding"
        '/// "Cell Width"
        '/// "Cell Height"
        '/// "Fill Order"
        '/// "Document Name"
        '/// "Image Description"
        '/// "Equip Make"
        '/// "Equip Model"
        '/// "Strip Offsets"
        '/// "Orientation"
        '/// "Samples PerPixel"
        '/// "Rows Per Strip"
        '/// "Strip Bytes Count"
        '/// "Min Sample Value"
        '/// "Max Sample Value"
        '/// "X Resolution"
        '/// "Y Resolution"
        '/// "Planar Config"
        '/// "Page Name"
        '/// "X Position"
        '/// "Y Position"
        '/// "Free Offset"
        '/// "Free Byte Counts"
        '/// "Gray Response Unit"
        '/// "Gray Response Curve"
        '/// "T4 Option"
        '/// "T6 Option"
        '/// "Resolution Unit"
        '/// "Page Number"
        '/// "Transfer Funcition"
        '/// "Software Used"
        '/// "Date Time"
        '/// "Artist"
        '/// "Host Computer"
        '/// "Predictor"
        '/// "White Point"
        '/// "Primary Chromaticities"
        '/// "ColorMap"
        '/// "Halftone Hints"
        '/// "Tile Width"
        '/// "Tile Length"
        '/// "Tile Offset"
        '/// "Tile ByteCounts"
        '/// "InkSet"
        '/// "Ink Names"
        '/// "Number Of Inks"
        '/// "Dot Range"
        '/// "Target Printer"
        '/// "Extra Samples"
        '/// "Sample Format"
        '/// "S Min Sample Value"
        '/// "S Max Sample Value"
        '/// "Transfer Range"
        '/// "JPEG Proc"
        '/// "JPEG InterFormat"
        '/// "JPEG InterLength"
        '/// "JPEG RestartInterval"
        '/// "JPEG LosslessPredictors"
        '/// "JPEG PointTransforms"
        '/// "JPEG QTables"
        '/// "JPEG DCTables"
        '/// "JPEG ACTables"
        '/// "YCbCr Coefficients"
        '/// "YCbCr Subsampling"
        '/// "YCbCr Positioning"
        '/// "REF Black White"
        '/// "ICC Profile"
        '/// "Gamma"
        '/// "ICC Profile Descriptor"
        '/// "SRGB RenderingIntent"
        '/// "Image Title"
        '/// "Copyright"
        '/// "Resolution X Unit"
        '/// "Resolution Y Unit"
        '/// "Resolution X LengthUnit"
        '/// "Resolution Y LengthUnit"
        '/// "Print Flags"
        '/// "Print Flags Version"
        '/// "Print Flags Crop"
        '/// "Print Flags Bleed Width"
        '/// "Print Flags Bleed Width Scale"
        '/// "Halftone LPI"
        '/// "Halftone LPIUnit"
        '/// "Halftone Degree"
        '/// "Halftone Shape"
        '/// "Halftone Misc"
        '/// "Halftone Screen"
        '/// "JPEG Quality"
        '/// "Grid Size"
        '/// "Thumbnail Format"
        '/// "Thumbnail Width"
        '/// "Thumbnail Height"
        '/// "Thumbnail ColorDepth"
        '/// "Thumbnail Planes"
        '/// "Thumbnail RawBytes"
        '/// "Thumbnail Size"
        '/// "Thumbnail CompressedSize"
        '/// "Color Transfer Function"
        '/// "Thumbnail Data"
        '/// "Thumbnail ImageWidth"
        '/// "Thumbnail ImageHeight"
        '/// "Thumbnail BitsPerSample"
        '/// "Thumbnail Compression"
        '/// "Thumbnail PhotometricInterp"
        '/// "Thumbnail ImageDescription"
        '/// "Thumbnail EquipMake"
        '/// "Thumbnail EquipModel"
        '/// "Thumbnail StripOffsets"
        '/// "Thumbnail Orientation"
        '/// "Thumbnail SamplesPerPixel"
        '/// "Thumbnail RowsPerStrip"
        '/// "Thumbnail StripBytesCount"
        '/// "Thumbnail ResolutionX"
        '/// "Thumbnail ResolutionY"
        '/// "Thumbnail PlanarConfig"
        '/// "Thumbnail ResolutionUnit"
        '/// "Thumbnail TransferFunction"
        '/// "Thumbnail SoftwareUsed"
        '/// "Thumbnail DateTime"
        '/// "Thumbnail Artist"
        '/// "Thumbnail WhitePoint"
        '/// "Thumbnail PrimaryChromaticities"
        '/// "Thumbnail YCbCrCoefficients"
        '/// "Thumbnail YCbCrSubsampling"
        '/// "Thumbnail YCbCrPositioning"
        '/// "Thumbnail RefBlackWhite"
        '/// "Thumbnail CopyRight"
        '/// "Luminance Table"
        '/// "Chrominance Table"
        '/// "Frame Delay"
        '/// "Loop Count"
        '/// "Pixel Unit"
        '/// "Pixel PerUnit X"
        '/// "Pixel PerUnit Y"
        '/// "Palette Histogram"
        '/// "Exposure Time"
        '/// "F-Number"
        '/// "Exposure Prog"
        '/// "Spectral Sense"
        '/// "ISO Speed"
        '/// "OECF"
        '/// "Ver"
        '/// "DTOrig"
        '/// "DTDigitized"
        '/// "CompConfig"
        '/// "CompBPP"
        '/// "Shutter Speed"
        '/// "Aperture"
        '/// "Brightness"
        '/// "Exposure Bias"
        '/// "MaxAperture"
        '/// "SubjectDist"
        '/// "Metering Mode"
        '/// "LightSource"
        '/// "Flash"
        '/// "FocalLength"
        '/// "Maker Note"
        '/// "User Comment"
        '/// "DTSubsec"
        '/// "DTOrigSS"
        '/// "DTDigSS"
        '/// "FPXVer"
        '/// "ColorSpace"
        '/// "PixXDim"
        '/// "PixYDim"
        '/// "RelatedWav"
        '/// "Interop"
        '/// "FlashEnergy"
        '/// "SpatialFR"
        '/// "FocalXRes"
        '/// "FocalYRes"
        '/// "FocalResUnit"
        '/// "Subject Loc"
        '/// "Exposure Index"
        '/// "Sensing Method"
        '/// "FileSource"
        '/// "SceneType"

        Dim bmp As System.Drawing.Bitmap = New System.Drawing.Bitmap(file_path)
        Dim er As Goheer.EXIF.EXIFextractor = New Goheer.EXIF.EXIFextractor(bmp, "\n")


        'MsgBox(er.Item("ISO Speed"))


        Return er
    End Function


End Class
