Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Net
Imports System.IO
Imports System.Data


Public Class Email

    Public Shared Function GetMailAddressCollection(ByVal EmailString As String) As MailAddressCollection
        Dim email() As String = EmailString.Split(New Char() {","c, ";"c})
        Dim col As New MailAddressCollection()
        For Each e As String In email
            col.Add(e)
        Next
        Return col
    End Function

    'Public Sub SendOrder(ByVal Cart As datatable)
    '    Dim FromAddress As MailAddress
    '    Dim ToAddress As New MailAddressCollection
    '    Dim URL, Body As String

    '    FromAddress = New MailAddress(User.Email)
    '    Friends = FriendAddress.Split(";"c)
    '    For Each f As String In Friends
    '        ToAddress.Add(New MailAddress(f))
    '    Next

    '    URL = String.Format(ConfigurationManager.AppSettings("TellAFriendBody"), InvitorUserID)
    '    Body = GetMessageBody(URL)

    '    SendEmail(FromAddress, ToAddress, "誠意邀請", Body)
    'End Sub

#Region "Message Seding Functions"
    Protected Function GetMessageBody(ByVal URL As String) As String
        Dim ret As String
        'Return ""

        Dim WebClient As New WebClient()
        Dim myStream As Stream
        URL = URL.Replace("~", "http://test.time2date.com.hk")
        'URL = URL.Replace("~", "http://localhost:2858/Time2Date3")
        'URL = _WebControl.ResolveClientUrl(URL)

        myStream = WebClient.OpenRead(URL)
        Dim sr As New StreamReader(myStream, System.Text.Encoding.UTF8)
        ret = sr.ReadToEnd()

        myStream.Close()

        Return ret
    End Function

    Public Overloads Shared Sub SendEmail(ByVal FromAddress As MailAddress, ByVal ToAddress As MailAddressCollection, ByVal Subject As String, ByVal Body As String)
        Dim MyMessage As New MailMessage
        Dim MyAddress As MailAddress

        With MyMessage
            .From = FromAddress
            For Each MyAddress In ToAddress
                .To.Add(MyAddress)
            Next

            .SubjectEncoding = Text.Encoding.UTF8
            .Subject = Subject

            .IsBodyHtml = True
            .BodyEncoding = Text.Encoding.UTF8
            .Body = Body
        End With

        SendMessage(MyMessage)
    End Sub

    Public Overloads Shared Sub SendEmail(ByVal FromAddress As MailAddress, ByVal ToAddress As MailAddress, ByVal CCAddress As MailAddressCollection, ByVal Subject As String, ByVal Body As String)
        Dim MyMessage As New MailMessage

        With MyMessage
            .From = FromAddress
            .To.Add(ToAddress)
            For Each c As MailAddress In CCAddress
                .CC.Add(c)
            Next

            .SubjectEncoding = Text.Encoding.UTF8
            .Subject = Subject

            .IsBodyHtml = True
            .BodyEncoding = Text.Encoding.UTF8
            .Body = Body
        End With

        SendMessage(MyMessage)
    End Sub


    Public Shared Sub SendMessage(ByVal Message As MailMessage)
        Dim SmtpClient As New SmtpClient()
        'Return

        SmtpClient = New SmtpClient()
        SmtpClient.Send(Message)
    End Sub
#End Region
End Class
