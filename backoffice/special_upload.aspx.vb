Imports System.IO
Imports System.Drawing

Partial Class backoffice_special_upload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrWhiteSpace(Request.QueryString("id")) Then
                hfdProductID.Value = Request.QueryString("id")
                LoadData()
            ElseIf Not String.IsNullOrWhiteSpace(Request.QueryString("imgid")) Then
                hfdImageID.Value = Request("imgid")
                LoadData()
            Else
                ShowPanel(4)
            End If
        End If
    End Sub

    Protected Sub LoadData()
        Dim ImageAdapter As New SpecialDataSetTableAdapters.SpecialImageTableAdapter()
        Dim Filename As String = ""
        If Not String.IsNullOrWhiteSpace(hfdImageID.Value) Then
            Dim ImageID As Integer = CInt(hfdImageID.Value)
            Filename = ImageAdapter.GetImageUrl(ImageID)
            hfdProductID.Value = ImageAdapter.GetSpecialID(ImageID)
        End If

        Dim FunctionID As Integer = CInt(ConfigurationManager.AppSettings("SpecialFunctionID"))
        ViewState("FunctionSettings") = New SiteFunctionClass(FunctionID)

        If Filename <> "" Then
            ShowPanel(3)
            imgSaved.ImageUrl = Path.Combine(ConfigurationManager.AppSettings("SpecialMobileImagePath"), Filename) & "?" & Now().Ticks
        Else
            ShowPanel(1)
        End If
    End Sub

    Protected Sub SaveData()
        Dim ImageAdapter As New SpecialDataSetTableAdapters.SpecialImageTableAdapter()
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

        Filename = Guid.NewGuid.ToString()

        oImg.Save(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("SpecialOriginalImagePath"), Filename & ".png")))
        Dim img As Image = ImageClass.Crop(oImg, w1, h1, x1, y1)
        img = ImageClass.ResizeImage(img, New Size(Site.ProductImageWidth, Site.ProductImageHeight))

        Filename &= ".jpg"
        ProductPath = Path.Combine(ConfigurationManager.AppSettings("SpecialImagePath"), Filename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(ProductPath), CInt(ConfigurationManager.AppSettings("JpegCompression")))
        Dim width As Integer = img.Width
        Dim height As Integer = img.Height

        Dim tb As Image = ImageClass.ResizeImage(img, New Size(CInt(ConfigurationManager.AppSettings("SpecialMobileImageWidth")), CInt(ConfigurationManager.AppSettings("SpecialMobileImageHeight"))))
        ProductPath = Path.Combine(ConfigurationManager.AppSettings("SpecialMobileImagePath"), Filename)
        ImageClass.SaveJPGWithCompressionSetting(tb, Server.MapPath(ProductPath), CInt(ConfigurationManager.AppSettings("JpegCompression")))
        imgSaved.ImageUrl = ProductPath & "?" & Now().Ticks

        tb = ImageClass.ResizeImage(img, New Size(Site.ProductThumbnailWidth, Site.ProductThumbnailHeight))
        ProductPath = Path.Combine(ConfigurationManager.AppSettings("SpecialThumbnailPath"), Filename)
        ImageClass.SaveJPGWithCompressionSetting(tb, Server.MapPath(ProductPath), CInt(ConfigurationManager.AppSettings("JpegCompression")))
        Dim tbWidth As Integer = tb.Width
        Dim tbHeight As Integer = tb.Height
        img.Dispose()
        tb.Dispose()

        If String.IsNullOrWhiteSpace(hfdImageID.Value) Then
            hfdImageID.Value = ImageAdapter.InsertQuery(CInt(hfdProductID.Value), "", Filename)
        Else
            ImageAdapter.UpdateImageUrl(Filename, CInt(hfdImageID.Value))
        End If
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
        Dim NewFilename As String = Guid.NewGuid.ToString()
        Dim JpegCompression As Integer = CInt(ConfigurationManager.AppSettings("JpegCompression"))
        Dim img As Image = Image.FromFile(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("UploadPath"), Filename)))
        Dim Site As SiteFunctionClass = ViewState("FunctionSettings")

        ' Save Original Image
        img.Save(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("SpecialOriginalImagePath"), NewFilename & ".png")))
        Filename &= ".jpg"
        NewFilename &= ".jpg"

        Dim Size As New Size(Site.ProductImageWidth, Site.ProductImageHeight)
        img = ImageClass.ResizeImage(img, Size)
        Dim SavePath As String = Path.Combine(ConfigurationManager.AppSettings("SpecialImagePath"), NewFilename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(SavePath), JpegCompression)
        Dim width As Integer = img.Width
        Dim height As Integer = img.Height

        Size = New Size(CInt(ConfigurationManager.AppSettings("SpecialMobileImageWidth")), CInt(ConfigurationManager.AppSettings("SpecialMobileImageHeight")))
        img = ImageClass.ResizeImage(img, Size)
        SavePath = Path.Combine(ConfigurationManager.AppSettings("SpecialMobileImagePath"), NewFilename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(SavePath), JpegCompression)
        imgSaved.ImageUrl = SavePath & "?" & Now().Ticks

        Size = New Size(Site.ProductThumbnailWidth, Site.ProductThumbnailHeight)
        img = ImageClass.ResizeImage(img, Size)
        SavePath = Path.Combine(ConfigurationManager.AppSettings("SpecialThumbnailPath"), NewFilename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(SavePath), JpegCompression)
        Dim tbWidth As Integer = img.Width
        Dim tbHeight As Integer = img.Height
        img.Dispose()

        Dim ImageAdapter As New SpecialDataSetTableAdapters.SpecialImageTableAdapter()
        If String.IsNullOrWhiteSpace(hfdImageID.Value) Then
            hfdImageID.Value = ImageAdapter.InsertQuery(CInt(hfdProductID.Value), "", NewFilename)
        Else
            ImageAdapter.UpdateImageUrl(NewFilename, CInt(hfdImageID.Value))
        End If
        ShowPanel(3)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
            Response.Redirect(String.Format("~/backoffice/special.aspx?id={0}", hfdProductID.Value))
        Catch ex As Exception
            lblMessage2.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        ShowPanel(1)
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(String.Format("~/backoffice/special.aspx?id={0}", hfdProductID.Value))
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btnCancel1.Click
        If imgSaved.ImageUrl = "" Then
            ShowPanel(1)
        Else
            ShowPanel(3)
        End If
    End Sub

    Protected Sub btnCrop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrop.Click
        Dim ImageAdapter As New SpecialDataSetTableAdapters.SpecialImageTableAdapter()
        Dim url As String = Path.GetFileNameWithoutExtension(ImageAdapter.GetImageUrl(CInt(hfdImageID.Value))) & ".png"
        Session("WorkingImage") = url
        Dim UploadPath As String = Path.Combine(ConfigurationManager.AppSettings("UploadPath"), url)
        Dim OriginalPath As String = Path.Combine(ConfigurationManager.AppSettings("SpecialOriginalImagePath"), url)
        File.Copy(Server.MapPath(OriginalPath), Server.MapPath(UploadPath), True)
        imgCrop.ImageUrl = UploadPath & "?" & Now().Ticks
        imgPreview.ImageUrl = UploadPath & "?" & Now().Ticks
        ShowPanel(2)
    End Sub

    Protected Sub btnAuto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAuto.Click
        ResizeImage()
    End Sub

    Protected Sub btnNoImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoImage.Click, btnNoImage1.Click, btnNoImage2.Click
        Dim ImageAdapter As New SpecialDataSetTableAdapters.SpecialImageTableAdapter()
        Dim RowAffected As Integer = 0

        RowAffected = ImageAdapter.DeleteImage(CInt(hfdImageID.Value))
        Response.Redirect(String.Format("~/backoffice/special.aspx?id={0}", hfdProductID.Value))
    End Sub
End Class

