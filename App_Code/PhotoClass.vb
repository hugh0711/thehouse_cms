Imports Microsoft.VisualBasic
'Imports System.Drawing.Imaging
Imports System.Drawing
Imports Utility

Public Class PhotoClass

	Public Shared Function SaveFile(ByVal FileUpload As FileUpload, ByVal txtFileUrl As Label, ByVal Path As String, Optional ByVal returnFileNameOnly As Boolean = True) As String
		Dim s As String = System.Web.HttpContext.Current.Server.MapPath(Path)
		Dim dtFileName As String = ""
		If FileUpload.HasFile Then
			'dtFileName = DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & FileUpload1.FileName
			dtFileName = DateTime.Now.ToString("yyyyMMddHHmmss") & System.IO.Path.GetExtension(FileUpload.FileName).ToLower
			s = s & dtFileName
			FileUpload.SaveAs(s)
			's = Path & dtFileName
			If returnFileNameOnly Then
				s = dtFileName
			Else
				s = System.IO.Path.Combine(Path, dtFileName)
			End If

		Else
			s = txtFileUrl.Text
		End If
		System.Threading.Thread.Sleep(1200)
		Return s
	End Function

	Public Shared Sub ResizeUploadedPhoto(ByVal filename As String, ByVal width As Integer, ByVal height As Integer, ByVal SavePath As String)
		Dim uploadedImage As Bitmap
		uploadedImage = New Bitmap(HttpContext.Current.Server.MapPath(System.IO.Path.Combine(UPLOAD_PATH, filename)))

		Dim uploadedImageSize As Size = uploadedImage.Size
		Dim newSize As New Size
		newSize = ImageClass.ResizeInRatio(uploadedImageSize, New Size(width, height))

		Dim bmp As New Bitmap(newSize.Width, newSize.Height)
		Dim g As Graphics
		g = Graphics.FromImage(bmp)
		With g
			.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
			.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
			.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
		End With

		Dim rect As New Rectangle(0, 0, newSize.Width, newSize.Height)
		g.DrawImage(uploadedImage, rect, 0, 0, uploadedImageSize.Width, uploadedImageSize.Height, GraphicsUnit.Pixel)

		Dim JpegCompression As Integer = CInt(ConfigurationManager.AppSettings("JpegCompression"))
		ImageClass.SaveJPGWithCompressionSetting(bmp, System.IO.Path.Combine(HttpContext.Current.Server.MapPath(SavePath), filename), JpegCompression)
	End Sub
End Class
