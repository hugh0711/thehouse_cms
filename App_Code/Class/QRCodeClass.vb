Imports Microsoft.VisualBasic

Public Class QRCodeClass

    Public Shared Function GetQRCodeUrl(Url As String) As String
        Dim ReturnUrl As String = ""

        ReturnUrl = String.Format("https://chart.googleapis.com/chart?chs=150x150&cht=qr&chl={0}", HttpContext.Current.Server.UrlEncode(Url))

        Return ReturnUrl
    End Function

End Class
