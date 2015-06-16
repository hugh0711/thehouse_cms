Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Drawing.Imaging

Public Class ImageClass

    Public Enum ImageSize
        Thumbnail = 0
        Normal = 1
    End Enum

    Public Enum CropOption
        FitInArea = 0
        FillTheArea = 1
    End Enum

    Public Shared Function ResizeInRatio(ByVal OriginalSize As Size, ByVal TargetSize As Size) As Size
        Dim ratio, ratio2 As Single
        Dim Size As Size

        Dim AllowBiggerSize As Boolean = True

        ratio = OriginalSize.Width / OriginalSize.Height
        ratio2 = TargetSize.Width / TargetSize.Height

        If (OriginalSize.Width > TargetSize.Width Or OriginalSize.Height > TargetSize.Height) Or AllowBiggerSize Then
            If ratio > ratio2 Then
                ' resize to width
                Size = New Size(TargetSize.Width, Convert.ToInt32(TargetSize.Width / OriginalSize.Width * OriginalSize.Height))
            Else
                ' resize to Height
                Size = New Size(Convert.ToInt32(TargetSize.Height / OriginalSize.Height * OriginalSize.Width), TargetSize.Height)
            End If
        Else
            Size = OriginalSize
        End If

        Return Size
    End Function

    Public Shared Function AddWatermark(ByVal OriginalImage As Image, ByVal WatermarkText As String) As Image
        Dim g As Graphics
        g = Graphics.FromImage(OriginalImage)
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.InterpolationMode = Drawing2D.InterpolationMode.High
        g.DrawString(WatermarkText, New Font("Arial", 20), New SolidBrush(Color.FromArgb(128, 255, 255, 255)), 10, 10)
        Dim img As New Bitmap(OriginalImage)
        g.DrawImage(img, New Point(0, 0))
        Return img
    End Function

    'Public Function ResizeImage(ByVal OriginalImage As Image, ByVal NewSize As Size) As Image
    '    Dim tSize As Size
    '    Dim ResizedImage As Image

    '    tSize = ResizeInRatio(OriginalImage.Size, NewSize)
    '    ResizedImage = OriginalImage.GetThumbnailImage(tSize.Width, tSize.Height, Nothing, IntPtr.Zero)

    '    Return ResizedImage
    'End Function

    Public Shared Function ResizeImage(ByVal OriginalImage As Image, ByVal NewSize As Size, Optional ByVal Watermark As Image = Nothing, Optional ByVal Testmark As Image = Nothing, Optional CropOption As CropOption = CropOption.FitInArea) As Image
        Dim oSize, nSize As Size
        Dim bmp As Bitmap
        Dim g As Graphics
        Dim rect As Rectangle

        oSize = OriginalImage.Size
        Select Case CropOption
            Case ImageClass.CropOption.FitInArea
                nSize = ResizeInRatio(oSize, NewSize)
            Case ImageClass.CropOption.FillTheArea
                nSize = NewSize
        End Select

        bmp = New Bitmap(nSize.Width, nSize.Height)
        g = Graphics.FromImage(bmp)
        g.CompositingQuality = CompositingQuality.HighQuality
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.High

        Select Case CropOption
            Case ImageClass.CropOption.FitInArea

            Case ImageClass.CropOption.FillTheArea
                Dim ratio0 As Single = NewSize.Width / NewSize.Height
                Dim ratio1 As Single = OriginalImage.Width / OriginalImage.Height
                Dim W1, H1 As Integer
                Dim X1, Y1 As Integer
                If ratio0 > ratio1 Then
                    W1 = OriginalImage.Width
                    H1 = NewSize.Height * OriginalImage.Width / NewSize.Width
                    X1 = 0
                    Y1 = 0 ' (OriginalImage.Height - H1) / 2
                Else
                    W1 = NewSize.Width * OriginalImage.Height / NewSize.Height
                    H1 = OriginalImage.Height
                    X1 = (OriginalImage.Width - W1) / 2
                    Y1 = 0
                End If
                OriginalImage = ImageClass.Crop(OriginalImage, W1, H1, X1, Y1)
                oSize = New Size(OriginalImage.Width, OriginalImage.Height)
        End Select

        rect = New Rectangle(0, 0, nSize.Width, nSize.Height)
        g.DrawImage(OriginalImage, rect, 0, 0, oSize.Width, oSize.Height, GraphicsUnit.Pixel)

        If Not Watermark Is Nothing Then
            Dim watermarkRect As Rectangle = New Rectangle(0, nSize.Height - Watermark.Height, Watermark.Width, Watermark.Height)
            g.DrawImage(Watermark, watermarkRect, 0, 0, Watermark.Width, Watermark.Height, GraphicsUnit.Pixel)
        End If

        If Not Testmark Is Nothing Then
            Dim testmarkRect As Rectangle = New Rectangle(nSize.Width - Testmark.Width, 0, Testmark.Width, Testmark.Height)
            g.DrawImage(Testmark, testmarkRect, 0, 0, Testmark.Width, Testmark.Height, GraphicsUnit.Pixel)
        End If

        OriginalImage.Dispose()
        Return bmp
    End Function

    'Public Function ResizeImage(ByVal OriginalImage As Image, ByVal NewSize As Size) As Image
    '    Dim oSize, nSize As Size
    '    Dim bmp As Bitmap
    '    Dim g As Graphics
    '    Dim rect As Rectangle

    '    oSize = OriginalImage.Size
    '    nSize = ResizeInRatio(oSize, NewSize)

    '    bmp = New Bitmap(nSize.Width, nSize.Height)
    '    g = Graphics.FromImage(bmp)
    '    g.CompositingQuality = CompositingQuality.HighQuality
    '    g.SmoothingMode = SmoothingMode.HighQuality
    '    g.InterpolationMode = InterpolationMode.High

    '    rect = New Rectangle(0, 0, nSize.Width, nSize.Height)
    '    g.DrawImage(OriginalImage, rect, 0, 0, oSize.Width, oSize.Height, GraphicsUnit.Pixel)

    '    Return bmp
    'End Function

	'Public Sub ResizeImage(ByVal filename As String)
	'	Dim origImage As Bitmap
	'	Dim sysImage, tnailImage As Image
	'	Dim tSize As Size

	'	origImage = New Bitmap(Server.MapPath(Path.Combine(originalPath, filename)))

	'	tSize = ResizeInRatio(origImage.Size, systemSize)
	'	sysImage = origImage.GetThumbnailImage(tSize.Width, tSize.Height, Nothing, IntPtr.Zero)

	'	sysImage.Save(Server.MapPath(Path.Combine(photoPath, filename)))

	'	tSize = ResizeInRatio(origImage.Size, thumbnailSize)
	'	tnailImage = origImage.GetThumbnailImage(tSize.Width, tSize.Height, Nothing, IntPtr.Zero)
	'	tnailImage.Save(Server.MapPath(Path.Combine(thumbnailPath, filename)))

	'	origImage.Dispose()
	'	sysImage.Dispose()
	'	tnailImage.Dispose()
	'End Sub

    Public Shared Function RotateImage(ByVal OriginalImage As Image, ByVal RotateFilpType As RotateFlipType) As Image
        OriginalImage.RotateFlip(RotateFilpType)
        Return OriginalImage
    End Function

    Public Shared Function Crop(ByVal img As Image, ByVal Width As Integer, ByVal Height As Integer, ByVal X As Integer, ByVal Y As Integer) As Image

        Dim bmp As Bitmap = New Bitmap(Width, Height, img.PixelFormat)
        'bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution)

        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.DrawImage(img, New Rectangle(0, 0, Width, Height), X, Y, Width, Height, GraphicsUnit.Pixel)
        End Using

        Return bmp

    End Function

    Public Shared Function Rotate(ByVal oImg As Image, ByVal Angle As Integer) As Image
        Dim Size As Size
        Dim w1, w2, h1, h2 As Single
        Dim w, h As Integer
        If Angle < 0 Then
            Angle = 360 + Angle
        End If
        Dim degree As Double = DegreeToRadian(Angle)
        Size = oImg.Size
        w1 = Math.Abs(Size.Height * Math.Sin(degree))
        w2 = Math.Abs(Size.Width * Math.Cos(degree))
        h1 = Math.Abs(Size.Width * Math.Sin(degree))
        h2 = Math.Abs(Size.Height * Math.Cos(degree))

        w = w1 + w2
        h = h1 + h2

        Dim img As New Bitmap(w, h)
        Dim g As Graphics = Graphics.FromImage(img)

        g.InterpolationMode = InterpolationMode.Bicubic
        g.SmoothingMode = SmoothingMode.AntiAlias
        'g.RotateTransform(Angle)
        'g.DrawImage(oImg, New Point(w1, 0))
        Dim p() As Point
        Select Case Angle
            Case 0 To 90
                p = New Point() {New Point(w1, 0), New Point(w, h1), New Point(0, h2)}
            Case 90 To 180
                p = New Point() {New Point(w, h2), New Point(w1, h), New Point(w2, 0)}
            Case 180 To 270
                p = New Point() {New Point(w2, h), New Point(0, h2), New Point(w, h1)}
            Case Is > 270
                p = New Point() {New Point(0, h1), New Point(w2, 0), New Point(w1, h)}
        End Select
        g.DrawImage(oImg, p)

        oImg.Dispose()

        Return img
    End Function

    Public Shared Function DegreeToRadian(ByVal Degree As Integer) As Double
        Return Math.PI * Degree / 180.0
    End Function


    Public Shared Function IsCMYK(ByVal MyImage As Image) As Boolean
        Dim ret As Boolean
        If (GetImageFlags(MyImage).IndexOf("Ycck") > -1) Or (GetImageFlags(MyImage).IndexOf("Cmyk") > -1) Or (GetImageFlags(MyImage).IndexOf("Ycbcr") > -1) Then
            ret = True
        Else
            ret = False
        End If
        Return ret
    End Function


    Public Shared Function GetImageFlags(ByVal MyImage As Image) As String
        Dim FlagVals As ImageFlags = CType([Enum].Parse(GetType(ImageFlags), MyImage.Flags.ToString()), ImageFlags)
        Return FlagVals.ToString
    End Function


    Public Shared Function ConvertCMYK2RGB(ByVal MyImage As Image) As Image
        Dim NewBit As Bitmap = New Bitmap(MyImage.Width, MyImage.Height, PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(NewBit)
        g.CompositingQuality = CompositingQuality.HighQuality
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic

        Dim Rect As Rectangle = New Rectangle(0, 0, MyImage.Width, MyImage.Height)
        g.DrawImage(MyImage, Rect)

        Dim retImage As Image = New Bitmap(NewBit)

        g.Dispose()
        NewBit.Dispose()
        MyImage.Dispose()

        Return retImage
    End Function

    Public Shared Function GetSizeFit(ByVal OriginalSize As Size, ByVal BoxSize As Size) As Size
        Dim oRatio As Single = OriginalSize.Width / OriginalSize.Height
        Dim bRatio As Single = BoxSize.Width / BoxSize.Height
        Dim retSize As New Size

        If oRatio < bRatio Then
            With retSize
                .Height = BoxSize.Height
                .Width = Math.Round(.Height * oRatio)
            End With
        Else
            With retSize
                .Width = BoxSize.Width
                .Height = Math.Round(.Width / oRatio)
            End With
        End If

        Return retSize
    End Function

    Public Shared Sub SaveJPGWithCompressionSetting(ByVal image As Image, ByVal szFileName As String, ByVal lCompression As Long)
        Dim ext As String = Path.GetExtension(szFileName).ToLower()
        If ext = ".jpg" Then
            Dim eps As EncoderParameters = New EncoderParameters(1)
            eps.Param(0) = New EncoderParameter(Encoder.Quality, lCompression)
            Dim ici As ImageCodecInfo = GetEncoderInfo("image/jpeg")
            image.Save(szFileName, ici, eps)
        Else
            image.Save(szFileName)
        End If
    End Sub

    Public Shared Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo
        Dim j As Integer
        Dim encoders As ImageCodecInfo()
        encoders = ImageCodecInfo.GetImageEncoders()
        For j = 0 To encoders.Length
            If encoders(j).MimeType = mimeType Then
                Return encoders(j)
            End If
        Next j
        Return Nothing
    End Function
End Class
