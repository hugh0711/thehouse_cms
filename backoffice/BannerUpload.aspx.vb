Imports System.IO
Imports System.Drawing

Imports System.Collections.Generic
Imports System.Linq
Imports System.Net

Partial Class backoffice_BannerUpload
    Inherits System.Web.UI.Page

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/backoffice/promos.aspx?type=1")
    End Sub

    Protected Sub AjaxFileUpload1_UploadComplete(sender As Object, e As AjaxControlToolkit.AjaxFileUploadEventArgs) 'Handles AjaxFileUpload1.UploadComplete

        'set file info
        Dim PhotoID As Guid = Guid.NewGuid()
        Dim Filename As String = PhotoID.ToString() & ".jpg"
        Dim bannerPath As String = "~/product_image/banner/"
        Dim FilePath As String = Path.Combine(bannerPath, Filename)
        Dim TempFile As String = Path.Combine(ConfigurationManager.AppSettings("UploadPath"), Path.GetFileNameWithoutExtension(IO.Path.GetRandomFileName) & Path.GetExtension(e.FileName))
        Dim JpegCompression As Long = CLng(ConfigurationManager.AppSettings("BannerJpegCompression"))

        ' Save upload file to the file system
        AjaxFileUpload1.SaveAs(MapPath(TempFile))
        Dim Image As Image = Image.FromFile(MapPath(TempFile))
        Dim NormalSize As New Size(CInt(ConfigurationManager.AppSettings("BannerPhotoWidth")), CInt(ConfigurationManager.AppSettings("BannerPhotoHeight")))
        Dim Image1 As Image = ImageClass.ResizeImage(Image, NormalSize)
        ImageClass.SaveJPGWithCompressionSetting(Image1, MapPath(FilePath), JpegCompression)
        Image1.Dispose()


        'save dtata to DB
        SaveData(Filename)

        'Delete Original image file
        Try
            File.Delete(MapPath(Filename))
        Catch
        End Try


    End Sub

    Protected Sub SaveData(Filename As String)



        Dim da As New PromoDataSetTableAdapters.PromoTableAdapter()
        Dim PromoSettingID As Integer = 1
        Dim UnitFunctionID As Integer = 2
        Dim Enabled As Boolean = True
        Dim StartDate As Date = Utility.NoStartDate
        Dim EndDate As Date = Utility.NoEndDate
        'Dim PromoUrl As String = GetOption()
        Dim SortOrder As Integer
        SortOrder = da.GetNextSortOrder(PromoSettingID)
        Dim CurrentUser As String = Page.User.Identity.Name


        'If IsDate(txtStartDate.Text) Then
        '    StartDate = Utility.GetStartDate(txtStartDate.Text)
        'End If
        'If IsDate(txtEndDate.Text) Then
        '    EndDate = Utility.GetEndDate(txtEndDate.Text)
        'End If


        Dim ImageUrl As String = Filename

        Dim banner_name As String = String.Format("Background {0}", SortOrder)

        Dim db As New CMSDataContext

        Dim banner As New Promo() With {
            .Name = banner_name,
            .PromoSettingID = PromoSettingID,
            .PromoImageUrl = Filename,
            .PromoUrl = "",
            .UnitFunctionID = UnitFunctionID,
            .StartDate = StartDate,
            .IsSingleDay = 0,
            .SortOrder = SortOrder,
            .Enabled = Enabled,
            .EndDate = EndDate,
            .CreateDate = Now(),
            .CreatedBy = User.Identity.Name,
            .UpdateDate = Now(),
        .UpdatedBy = User.Identity.Name
        }
        db.Promos.InsertOnSubmit(banner)
        db.SubmitChanges()


    End Sub


End Class
