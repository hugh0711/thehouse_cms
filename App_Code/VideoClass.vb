Imports Microsoft.VisualBasic

Public Class VideoClass

    Public Enum Media
        Standard = 1
        HD = 2
        Mobile = 3
        YouTube = 4
    End Enum

    Public Shared YouTubeTag As String = "youtube://"
    
    Public Shared Function GetVideoID(ByVal url As String) As String
        Dim Regex As New Regex("^.*((youtu.be\/)|(v\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*")
        Dim t As String() = Regex.Split(url)
        If t.Length > 3 Then
            Return t(3)
        Else
            Return ""
        End If
    End Function


    Public Shared Function GetVideoUrl(ByVal VideoID As String) As String
        Return String.Format("http://youtu.be/{0}", VideoID)
    End Function

    Public Shared Function GetVideoIDFromMediaUrl(Url As String) As String
        Dim VideoID As String = ""

        If Url.StartsWith(YouTubeTag) AndAlso Url.Length > YouTubeTag.Length Then
            VideoID = Url.Substring(YouTubeTag.Length)

        End If

        Return VideoID
    End Function

    Public Shared Function GetMediaUrl(Url As String) As String
        Dim ReturnUrl As String = ""
        If Url.StartsWith(YouTubeTag) AndAlso Url.Length > YouTubeTag.Length Then
            Dim VideoID As String = Url.Substring(YouTubeTag.Length)
            ReturnUrl = GetVideoUrl(VideoID)
        ElseIf Url.StartsWith("http://") OrElse Url.StartsWith("https://") Then
            ReturnUrl = Url
        End If

        Return ReturnUrl
    End Function

    Public Shared Function GetFacebookID(ByVal url As String) As String
        Dim s As String
        Dim p As Integer
        p = url.LastIndexOf("/")
        If p = url.Length Then
            url = url.Substring(0, url.Length - 1)
            p = url.LastIndexOf("/")
        End If
        If p < 0 Then
            s = ""
        Else
            s = url.Substring(p + 1)
        End If

        Return s
    End Function

    Public Shared Function GetFacebookID1(ByVal url As String) As String
        Dim Regex As New Regex("/(?:http:\/\/)?(?:www.)?facebook.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[\w\-]*\/)*([\w\-]*)/")
        Dim t As String() = Regex.Split(url)
        Return t(1)
    End Function

    Public Shared Function GetFacebookLink(ByVal FacebookID As String) As String
        Return String.Format("http://www.facebook.com/{0}", FacebookID)
    End Function

    Public Shared Function ValidateVideoLink(ByVal url As String) As Boolean
        Dim flag As Boolean = True
        Try
            Dim s As String
            Dim p() As String
            p = Split(url, "/")
            s = p(3)
            If Not s.Length > 0 Then
                flag = False
            End If
        Catch ex As Exception
            flag = False
        Finally
        End Try
        Return flag
    End Function

    Public Shared Function GetPreview(ByVal VideoID As String, Optional HQ As Boolean = False) As String
        Dim s As String = ""
        If VideoID.Length > 0 Then
            Dim domain As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
            If HttpContext.Current.Request.ApplicationPath <> "/" Then
                domain &= HttpContext.Current.Request.ApplicationPath
            End If
            s = domain & "/api/previewimage.ashx?video=" & VideoID
            If HQ Then
                s &= "&hq=true"
            End If
        End If
        Return s
    End Function

    Public Shared Function GetHQPreview(ByVal VideoID As String) As String
        Dim s As String = ""
        If VideoID.Length > 0 Then
            Dim domain As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
            If HttpContext.Current.Request.ApplicationPath <> "/" Then
                domain &= HttpContext.Current.Request.ApplicationPath
            End If
            s = domain & "/api/previewimage.ashx?video=" & VideoID & "&hq=true"
        End If
        Return s
    End Function

    Public Shared Function GetYouTubePreview(ByVal VideoID As String, Optional HQ As Boolean = False) As String
        Dim s As String = ""
        If VideoID.Length > 0 Then
            If HQ Then
                s = "http://i3.ytimg.com/vi/" & VideoID & "/hqdefault.jpg"
            Else
                s = "http://i3.ytimg.com/vi/" & VideoID & "/default.jpg"
            End If
        End If
        Return s
    End Function

    Public Shared Function GetYouTubeHQPreview(ByVal VideoID As String) As String
        Dim s As String = ""
        If VideoID.Length > 0 Then
            s = "http://i3.ytimg.com/vi/" & VideoID & "/hqdefault.jpg"
        End If
        Return s
    End Function

End Class
