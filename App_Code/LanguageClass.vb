Imports Microsoft.VisualBasic

Public Class LanguageClass

    Public Shared Function GetLanguageName(ByVal LanguageCode As String) As String
        Dim Lang As String = ""

        Select Case LanguageCode
            Case "en", "en-us"
                Lang = "English"
            Case "zh-hk"
                Lang = "繁體中文"
            Case "zh-cn"
                Lang = "简体中文"
        End Select

        Return Lang
    End Function

End Class
