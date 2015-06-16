Imports System.IO
Imports System.Drawing

Partial Class backoffice_category_upload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("id") IsNot Nothing AndAlso Request.QueryString("id") <> "" Then
                hfdCategoryID.Value = Request.QueryString("id")
                LoadData()
            Else
                ShowPanel(4)
            End If
        End If
    End Sub

    Protected Sub LoadData()
        Dim CategoryAdaptor As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim CategoryID As Integer = CInt(hfdCategoryID.Value)
        Dim Filename As String = CategoryAdaptor.GetImageUrl(CategoryID)
        Dim FunctionID As Integer = CategoryAdaptor.GetFunctionID(CategoryID)
        Dim Site As New SiteFunctionClass(FunctionID)
        ViewState("FunctionSettings") = Site

        If Filename <> "" Then
            ShowPanel(3)
            imgSaved.ImageUrl = Path.Combine(ConfigurationManager.AppSettings("CategoryImagePath"), Filename) & "?" & Now().Ticks
        Else
            ShowPanel(1)
        End If

        If Not Site.IsCategoryImageOptional Then
            btnNoImage.Visible = False
            btnNoImage1.Visible = False
            btnNoImage2.Visible = False
        End If
    End Sub

    Protected Sub SaveData()
        Dim CategoryAdaptor As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim ImageName As String = Path.Combine(ConfigurationManager.AppSettings("UploadPath"), Session("WorkingImage"))
        Dim CategoryPath, Filename As String
        Dim w1, h1, x1, y1 As Integer

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

        Dim Site As SiteFunctionClass = ViewState("FunctionSettings")
        Dim JpegCompression As Integer = CInt(ConfigurationManager.AppSettings("JpegCompression"))

        oImg.Save(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("CategoryOriginalImagePath"), hfdCategoryID.Value & ".png")))
        Dim img As Image = ImageClass.Crop(oImg, w1, h1, x1, y1)
        img = ImageClass.ResizeImage(img, New Size(Site.CategoryImageWidth, Site.CategoryImageHeight))

        Filename = hfdCategoryID.Value & ".jpg"
        CategoryPath = Path.Combine(ConfigurationManager.AppSettings("CategoryImagePath"), Filename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(CategoryPath), JpegCompression)
        'img.Save(Server.MapPath(CategoryPath), Imaging.ImageFormat.Jpeg)
        imgSaved.ImageUrl = CategoryPath & "?" & Now().Ticks

        Dim Size As New Size(Site.CategoryThumbnailWidth, Site.CategoryThumbnailHeight)
        img = ImageClass.ResizeImage(img, Size)
        CategoryPath = Path.Combine(ConfigurationManager.AppSettings("CategoryThumbnailPath"), Filename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(CategoryPath), JpegCompression)
        img.Dispose()

        CategoryAdaptor.UpdateImageUrl(Filename, Convert.ToInt32(hfdCategoryID.Value))
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
            If chkcrop.Checked Then
                ShowPanel(2)
                imgCrop.ImageUrl = FilePath & "?" & Now().Ticks
                imgPreview.ImageUrl = FilePath & "?" & Now().Ticks
            Else
                resizeimage()
            End If
        End If
    End Sub

    Protected Sub ResizeImage()
        Dim Filename As String = Session("WorkingImage")
        Dim NewFilename As String = hfdCategoryID.Value & ".jpg"
        Dim JpegCompression As Integer = CInt(ConfigurationManager.AppSettings("JpegCompression"))
        Dim img As Image = Image.FromFile(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("UploadPath"), Filename)))
        'Save Original Image
        img.Save(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("CategoryOriginalImagePath"), hfdCategoryID.Value & ".png")))

        Dim Size As New Size(ViewState("FunctionSettings").CategoryImageWidth, ViewState("FunctionSettings").CategoryImageHeight)
        img = ImageClass.ResizeImage(img, Size)
        Dim SavePath As String = Path.Combine(ConfigurationManager.AppSettings("CategoryImagePath"), NewFilename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(SavePath), JpegCompression)
        imgSaved.ImageUrl = SavePath & "?" & Now().Ticks

        Size = New Size(ViewState("FunctionSettings").CategoryThumbnailWidth, ViewState("FunctionSettings").CategoryThumbnailHeight)
        img = ImageClass.ResizeImage(img, Size)
        SavePath = Path.Combine(ConfigurationManager.AppSettings("CategoryThumbnailPath"), NewFilename)
        ImageClass.SaveJPGWithCompressionSetting(img, Server.MapPath(SavePath), JpegCompression)

        img.Dispose()

        Dim CategoryAdaptor As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        CategoryAdaptor.UpdateImageUrl(NewFilename, CInt(hfdCategoryID.Value))

        ShowPanel(3)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
            Response.Redirect(String.Format("~/backoffice/category.aspx?id={0}", hfdCategoryID.Value))
        Catch ex As Exception
            lblMessage2.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        ShowPanel(1)
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(String.Format("~/backoffice/category.aspx?id={0}", hfdCategoryID.Value))
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(String.Format("~/backoffice/category.aspx?id={0}", hfdCategoryID.Value))
    End Sub

    Protected Sub btnCancel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel1.Click
        If imgSaved.ImageUrl = "" Then
            ShowPanel(1)
        Else
            ShowPanel(3)
        End If
    End Sub

    Protected Sub btnCrop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrop.Click
        Dim ImageAdapter As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim url As String = Path.GetFileNameWithoutExtension(ImageAdapter.GetImageUrl(CInt(hfdCategoryID.Value))) & ".png"
        Session("WorkingImage") = url
        Dim UploadPath As String = Path.Combine(ConfigurationManager.AppSettings("UploadPath"), url)
        Dim OriginalPath As String = Path.Combine(ConfigurationManager.AppSettings("CategoryOriginalImagePath"), url)
        File.Copy(Server.MapPath(OriginalPath), Server.MapPath(UploadPath), True)
        imgCrop.ImageUrl = UploadPath & "?" & Now().Ticks
        imgPreview.ImageUrl = UploadPath & "?" & Now().Ticks
        ShowPanel(2)
    End Sub

    Protected Sub btnAuto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAuto.Click
        ResizeImage()
    End Sub


    Protected Sub btnNoImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoImage.Click, btnNoImage1.Click, btnNoImage2.Click
        Dim CategoryAdapter As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim RowAffected As Integer = 0

        RowAffected = CategoryAdapter.UpdateImageUrl("", CInt(hfdCategoryID.Value))
        Response.Redirect(String.Format("~/backoffice/category.aspx?id={0}", hfdCategoryID.Value))
    End Sub
End Class
