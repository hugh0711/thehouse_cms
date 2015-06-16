Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Net
Imports System.IO
Imports System.Data


Public Class EmailClass
    Public Shared Sub SendMail(ByVal body As String, ByVal EMaillAddress As String, ByVal Name As String)
        Dim mailMessage As New MailMessage
        Dim FromAddress As MailAddress
        Dim ToAddress As MailAddress

        FromAddress = New MailAddress(EMaillAddress, Name)
        ToAddress = New MailAddress(ConfigurationManager.AppSettings("SalesEmail"))

        With mailMessage
            .From = FromAddress ' "no-reply@otto.hk"
            .To.Add(ToAddress)
            .SubjectEncoding = Text.Encoding.UTF8
            .Subject = "Feedback"
            .IsBodyHtml = False
            .BodyEncoding = Text.Encoding.UTF8
            .Body = body

        End With
        SendMessage(mailMessage)
    End Sub

	Public Shared Sub SendToFriend(ByVal from_Address As String, ByVal DisplayFromName As String, ByVal to_Address As String, ByVal body As String)
		Dim mailMessage As New MailMessage
		Dim FromAddress As MailAddress
		Dim ToAddress() As String = Split(to_Address, ";")

		FromAddress = New MailAddress(from_Address, DisplayFromName)

		With mailMessage
			.From = FromAddress
			For Each add As String In ToAddress
				.To.Add(add)
			Next
			.SubjectEncoding = Text.Encoding.UTF8
			.Subject = "Message from Sino French Water website"
			.IsBodyHtml = False
			.BodyEncoding = Text.Encoding.UTF8
			.Body = body

		End With
		SendMessage(mailMessage)
	End Sub

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
	Public Shared Function GetMessageBody(ByVal URL As String) As String
		Dim ret As String
		'Return ""

		Dim WebClient As New WebClient()
		Dim myStream As Stream
		'URL = URL.Replace("~", "http://test.time2date.com.hk")
		'URL = URL.Replace("~", "http://localhost:2858/Time2Date3")
		'URL = _WebControl.ResolveClientUrl(URL)
		If HttpContext.Current.Request.IsLocal Then
			URL = URL.Replace("~", "http://localhost:51870" & HttpContext.Current.Request.ApplicationPath)
		Else
			URL = URL.Replace("~", "http://" & HttpContext.Current.Request.Url.Host & HttpContext.Current.Request.ApplicationPath)
		End If

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

	Public Overloads Shared Sub SendEmail(ByVal FromAddress As MailAddress, ByVal ToAddress As MailAddress, ByVal Subject As String, ByVal Body As String)
		Dim MyMessage As New MailMessage

		With MyMessage
			.From = FromAddress
			.To.Add(ToAddress)

			.SubjectEncoding = Text.Encoding.UTF8
			.Subject = Subject

			.IsBodyHtml = True
			.BodyEncoding = Text.Encoding.UTF8
			.Body = Body
		End With

		SendMessage(MyMessage)
	End Sub

	Public Overloads Shared Sub SendEmail(ByVal FromAddress As MailAddress, ByVal ToAddress As MailAddress, ByVal BCCAddress As MailAddress, ByVal Subject As String, ByVal Body As String)
		Dim MyMessage As New MailMessage

		With MyMessage
			.From = FromAddress
			.To.Add(ToAddress)
			.Bcc.Add(BCCAddress)

			.SubjectEncoding = Text.Encoding.UTF8
			.Subject = Subject

			.IsBodyHtml = True
			.BodyEncoding = Text.Encoding.UTF8
			.Body = Body
		End With

		SendMessage(MyMessage)
	End Sub

	Public Overloads Shared Sub SendEmail(ByVal FromAddress As MailAddress, ByVal ToAddress As MailAddress, ByVal BCCAddress As MailAddressCollection, ByVal Subject As String, ByVal Body As String)
		Dim MyMessage As New MailMessage

		With MyMessage
			.From = FromAddress
			.To.Add(ToAddress)
			For Each MyAddress As MailAddress In BCCAddress
				.Bcc.Add(MyAddress)
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
