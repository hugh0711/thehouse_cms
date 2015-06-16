Imports LitJson
Imports System.Net
Imports System.Web
Imports System.IO

Public Class GoogleTranslateClass


    Public Function Translate(ByVal Text As String, ByVal LanguageFrom As String, ByVal LanguageTo As String) As String
        Return Text

        If Text.Length > 500 Then
            Throw New Exception("Google Translate does not allow 500 characters translation")
        End If

        LanguageFrom = LanguageCorrection(LanguageFrom)
        LanguageTo = LanguageCorrection(LanguageTo)

        Dim RequestString As String = String.Format("http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q={0}&langpair={1}%7C{2}", HttpUtility.UrlEncode(Text), LanguageFrom, LanguageTo)
        Dim oRequest As WebRequest = WebRequest.Create(RequestString)
        Dim oResponse As WebResponse = oRequest.GetResponse()

        Dim oReader As New StreamReader(oResponse.GetResponseStream())
        Dim sResult As String = oReader.ReadToEnd()

        Dim oData As JsonData = JsonMapper.ToObject(sResult)
        If oData("responseStatus").ToString() <> "200" Then
            Throw New Exception("Translation error.")
        End If

        Return oData("responseData")("translatedText").ToString()
    End Function

    Public Function Translate(ByVal Text As String, ByVal LanguageTo As String) As String
        LanguageTo = LanguageCorrection(LanguageTo)
        Return Me.Translate(Text, "", LanguageTo)
    End Function

    Public Function LanguageCorrection(ByVal Language As String) As String
        Select Case Language.ToLower()
            Case "zh-hk"
                Language = "zh-TW"
            Case "en-us"
                Language = "en"
        End Select

        Return Language
    End Function
End Class
