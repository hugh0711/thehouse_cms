Imports System.IO
Imports System.Drawing

Partial Class backoffice_product_upload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("id") IsNot Nothing AndAlso Request.QueryString("id") <> "" Then
                hfdProductID.Value = Request.QueryString("id")
                LoadData()
            Else
                ShowPanel(4)
            End If
        End If
    End Sub

    Protected Sub LoadData()
        Dim ImageAdapter As New ProductDataSetTableAdapters.ProductImageTableAdapter()
        Dim ProductID As Integer = Convert.ToInt32(hfdProductID.Value)
        Dim Filename As String = ImageAdapter.GetImageUrl(ProductID, 1)

        Dim FunctionID As Integer = (New ProductDataSetTableAdapters.ProductTableAdapter()).GetFunctionID(ProductID)
        ViewState("FunctionSettings") = New SiteFunctionClass(FunctionID)

        If Filename <> "" Then
            ShowPanel(3)
            imgSaved.ImageUrl = Path.Combine(ConfigurationManager.AppSettings("ProductImagePath"), Filename) & "?" & Now().Ticks
        Else
            ShowPanel(1)
        End If
    End Sub

    Protected Sub SaveData()
        Dim ImageAdapter As New ProductDataSetTableAdapters.ProductImageTableAdapter()
        Dim ImageName As String = Path.Combine(ConfigurationManager.AppSettings("UploadPath"), Session("WorkingImage"))
        Dim ProductPath, Filename As String
        Dim w1, h1, x1, y1 As Integer
        Dim Site As SiteFunctionClass = ViewState("FunctionSettings")

        Dim oImg As Bitmap = Bitmap.FromFile(Server.MapPath(ImageName))

        If W.Value = "" Then
            w1 = oImg.Width
            h1 = oImg.Height
            x1 = 0
            y1 = 0
        Else
            w1 = CInt(W.Value)
            h1 = CInt(H.Value)
            x1 = CInt(X.Value)
            y1 = CInt(Y.Value)
        End If

        'oImg.Save(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("ProductOriginalImagePath"), hfdProductID.Value & ".png")))
        oImg.Save(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("ProductOriginalImagePath"), hfdProductID.Value & ".jpg")))

        Dim img As Image = ImageClass.Crop(oImg, w1, h1, x1, y1)
        img = ImageClass.ResizeImage(img, New Size(Site.ProductImageWidth, Site.ProductImageHeight))

        Filename = hfdProductID.Value & ".jpg"
        ProductPath = Path.Combine(ConfigurationManager.AppSettings("ProductImagePath"), Filename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(ProductPath), CInt(ConfigurationManager.AppSettings("JpegCompression")))
        imgSaved.ImageUrl = ProductPath & "?" & Now().Ticks
        Dim width As Integer = img.Width
        Dim height As Integer = img.Height

        Dim tb As Image = ImageClass.ResizeImage(img, New Size(Site.ProductThumbnailWidth, Site.ProductThumbnailHeight))
        ProductPath = Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), Filename)
        ImageClass.SaveJPGWithCompressionSetting(tb, Server.MapPath(ProductPath), CInt(ConfigurationManager.AppSettings("JpegCompression")))
        Dim tbWidth As Integer = tb.Width
        Dim tbHeight As Integer = tb.Height
        img.Dispose()
        tb.Dispose()

        ImageAdapter.DeleteImage(CInt(hfdProductID.Value), 1)
        ImageAdapter.Insert(CInt(hfdProductID.Value), Filename, width, height, 1, tbWidth, tbHeight)
        ShowPanel(3)
    End Sub

    Protected Sub ShowPanel(ByVal index As Integer)
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False

        Select Case index
            Case 1
                Panel1.Visible = True
            Case 2
                Panel2.Visible = True
            Case 3
                Panel3.Visible = True
            Case 4
                Panel4.Visible = True
        End Select
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim UploadPath As String = ConfigurationManager.AppSettings("UploadPath")
        Dim FilePath As String = ""
        Dim FileOK As Boolean = False
        Dim FileSaved As Boolean = False

        If FileUpload1.HasFile Then
            Session("WorkingImage") = FileUpload1.FileName
            Dim FileExtension As String = Path.GetExtension(Session("WorkingImage")).ToLower()
            Select Case FileExtension
                Case ".jpg", ".jpeg", ".png", ".gif", ".bmp"
                    FileOK = True
            End Select
        End If

        If FileOK Then
            Try
                FilePath = Path.Combine(UploadPath, Session("WorkingImage"))
                FileUpload1.SaveAs(Server.MapPath(FilePath))
                FileSaved = True
            Catch ex As Exception
                lblMessage.Text = "檔案上傳失敗. " & ex.Message
                FileSaved = False
            End Try
        Else
            lblMessage.Text = "上傳檔案必須為影像檔案"
        End If

        If FileSaved Then
            If chkCrop.Checked Then
                ShowPanel(2)
                imgCrop.ImageUrl = FilePath & "?" & Now().Ticks
                imgPreview.ImageUrl = FilePath & "?" & Now().Ticks
            Else
                ResizeImage()
            End If
        End If
    End Sub

    Protected Sub ResizeImage()
        Dim Filename As String = Session("WorkingImage")
        Dim NewFilename As String = hfdProductID.Value & ".jpg"
        Dim JpegCompression As Integer = CInt(ConfigurationManager.AppSettings("JpegCompression"))
        Dim img As Image = Image.FromFile(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("UploadPath"), Filename)))
        Dim Site As SiteFunctionClass = ViewState("FunctionSettings")

        ' Save Original Image
        'img.Save(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("ProductOriginalImagePath"), hfdProductID.Value & ".png")))
        img.Save(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("ProductOriginalImagePath"), hfdProductID.Value & ".jpg")))

        Dim Size As New Size(Site.ProductImageWidth, Site.ProductImageHeight)
        img = ImageClass.ResizeImage(img, Size)
        Dim SavePath As String = Path.Combine(ConfigurationManager.AppSettings("ProductImagePath"), NewFilename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(SavePath), JpegCompression)
        imgSaved.ImageUrl = SavePath & "?" & Now().Ticks
        Dim width As Integer = img.Width
        Dim height As Integer = img.Height

        Size = New Size(Site.ProductThumbnailWidth, Site.ProductThumbnailHeight)
        img = ImageClass.ResizeImage(img, Size)
        SavePath = Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), NewFilename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(SavePath), JpegCompression)
        Dim tbWidth As Integer = img.Width
        Dim tbHeight As Integer = img.Height
        img.Dispose()

        Dim ImageAdapter As New ProductDataSetTableAdapters.ProductImageTableAdapter()
        ImageAdapter.DeleteImage(CInt(hfdProductID.Value), 1)
        ImageAdapter.Insert(CInt(hfdProductID.Value), NewFilename, width, height, 1, tbWidth, tbHeight)
        ShowPanel(3)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
            Response.Redirect(String.Format("~/backoffice/product.aspx?id={0}", hfdProductID.Value))
        Catch ex As Exception
            lblMessage2.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        ShowPanel(1)
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(String.Format("~/backoffice/product.aspx?id={0}", hfdProductID.Value))
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btnCancel1.Click
        If imgSaved.ImageUrl = "" Then
            ShowPanel(1)
        Else
            ShowPanel(3)
        End If
    End Sub

    Protected Sub btnCrop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrop.Click
        Dim ImageAdapter As New ProductDataSetTableAdapters.ProductImageTableAdapter()
        'Dim png_url As String = Path.GetFileNameWithoutExtension(ImageAdapter.GetImageUrl(CInt(hfdProductID.Value), 1)) & ".png"
        'Session("WorkingImage") = png_url
        'Dim UploadPath As String = Path.Combine(ConfigurationManager.AppSettings("UploadPath"), png_url)
        'Dim OriginalPath As String = Path.Combine(ConfigurationManager.AppSettings("ProductOriginalImagePath"), png_url)
        'File.Copy(Server.MapPath(OriginalPath), Server.MapPath(UploadPath), True)

        Dim png_url As String = Path.GetFileNameWithoutExtension(ImageAdapter.GetImageUrl(CInt(hfdProductID.Value), 1)) & ".png"
        Session("WorkingImage") = png_url
        Dim UploadPath As String = Path.Combine(ConfigurationManager.AppSettings("UploadPath"), png_url)
        Dim OriginalPath As String = Path.Combine(ConfigurationManager.AppSettings("ProductOriginalImagePath"), png_url)


        If My.Computer.FileSystem.FileExists(OriginalPath) Then
            File.Copy(Server.MapPath(OriginalPath), Server.MapPath(UploadPath), True)
        Else
            Dim jpg_url As String = Path.GetFileNameWithoutExtension(ImageAdapter.GetImageUrl(CInt(hfdProductID.Value), 1)) & ".jpg"
            Session("WorkingImage") = jpg_url
            UploadPath = Path.Combine(ConfigurationManager.AppSettings("UploadPath"), jpg_url)
            OriginalPath = Path.Combine(ConfigurationManager.AppSettings("ProductOriginalImagePath"), jpg_url)
            File.Copy(Server.MapPath(OriginalPath), Server.MapPath(UploadPath), True)

        End If



        imgCrop.ImageUrl = UploadPath & "?" & Now().Ticks
        imgPreview.ImageUrl = UploadPath & "?" & Now().Ticks
        ShowPanel(2)
    End Sub

    Protected Sub btnAuto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAuto.Click
        ResizeImage()
    End Sub

    Protected Sub btnNoImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoImage.Click, btnNoImage1.Click, btnNoImage2.Click
        Dim ImageAdapter As New ProductDataSetTableAdapters.ProductImageTableAdapter()
        Dim RowAffected As Integer = 0

        RowAffected = ImageAdapter.DeleteImage(CInt(hfdProductID.Value), 1)
        Response.Redirect(String.Format("~/backoffice/product.aspx?id={0}", hfdProductID.Value))
    End Sub
End Class
