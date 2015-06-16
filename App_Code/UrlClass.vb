Imports Microsoft.VisualBasic

Public Class UrlClass

    Public Shared Function Reformat(ByVal Url As String) As String
        Url = Url.Replace("/", "-").Replace("\", "-").Replace(":", "-").Replace("*", "-").Replace("?", "-").Replace("""", "-").Replace("<", "-").Replace(">", "-").Replace("|", "-")
        Url = Url.Replace(" ", "-")
        Do Until Url.IndexOf("--") = -1
            Url = Url.Replace("--", "-")
        Loop
        Return Url
    End Function

End Class
