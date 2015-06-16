Imports Microsoft.VisualBasic
Imports Newtonsoft.Json
Imports System.IO
Imports System.Net.Mail

<JsonObject()>
Public Class MemberDetailJsonClass

    <JsonProperty()> Public UserID As String = ""
    <JsonProperty()> Public Gender As String = ""
    <JsonProperty()> Public Name As String = ""
    <JsonProperty()> Public Email As String = ""
    <JsonProperty()> Public ContactNo As String = ""
    <JsonProperty()> Public Birthday As Date = Utility.NoBirthday
    <JsonProperty()> Public FaceBookID As String = ""
    <JsonProperty()> Public Password As String = ""
    <JsonProperty()> Public UserPicUrl As String = ""

    Public Sub New()

    End Sub

    Public Function Load(UserID As String) As MemberDetailJsonClass
        Dim Member As New MemberDetailJsonClass()
        Dim da As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()
        Dim dt As MemberDetailDataSet.MemberDetailDataTable

        dt = da.GetDataByUserID(UserID)
        If dt.Rows.Count > 0 Then
            Dim dr As MemberDetailDataSet.MemberDetailRow = dt.Rows(0)
            With dr
                Me.UserID = .UserID
                Me.Gender = .Gender
                Me.Name = .Name
                Me.Email = .Email
                Me.ContactNo = .ContactNo
                Me.Birthday = .Birthday
                Me.FaceBookID = .FacebookUserID
            End With
        End If

        Return Member
    End Function

    Public Function Save() As Boolean
        Dim da As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()
        Dim User As MembershipUser = Membership.GetUser(UserID)
        Dim Ret As Boolean = False

        If User IsNot Nothing Then
            User.Email = Email
            Membership.UpdateUser(User)
            da.Update(UserID, Gender, Name, Email, ContactNo, "", "", Birthday, Now(), FaceBookID, UserID)

            Ret = True
        End If

        Return Ret
    End Function

    Public Shared Function ChangePassword(UserID As String, OldPassword As String, NewPassword As String) As Boolean
        Dim User As MembershipUser = Membership.GetUser(UserID)
        Dim Ret As Boolean = False
        If User IsNot Nothing Then
            Ret = User.ChangePassword(OldPassword, NewPassword)
        End If
        Return Ret
    End Function

    'Public Function Create(ByRef Result As String) As Boolean
    '    Dim da As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()
    '    Dim User As MembershipUser = Membership.GetUser(UserID)
    '    Dim Status As Boolean = False
    '    Dim Message As String = ""

    '    If User Is Nothing Then
    '        Try
    '            User = Membership.CreateUser(UserID, Password, Email)
    '            User.IsApproved = False
    '            Membership.UpdateUser(User)
    '            da.Insert(UserID, Gender, Name, Email, ContactNo, "", "", Birthday, Now(), FaceBookID)

    '            Roles.AddUserToRole(UserID, "Member")

    '            SendVerification(User)
    '            Status = True
    '        Catch ex As Exception
    '            Message = "Error: " & ex.Message
    '        End Try
    '    Else
    '        Message = "User already existed"
    '    End If

    '    Dim Param As New List(Of String)
    '    Param.Add("""status"":" & Status.ToString().ToLower())
    '    Param.Add("""message"":""" & Message & """")
    '    Result = "{" & Join(Param.ToArray(), ",") & "}"

    '    Return Status
    'End Function

    Public Function Create() As Boolean
        Dim da As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter()
        Dim User As MembershipUser = Membership.GetUser(UserID)
        Dim Status As Boolean = False

        User = Membership.CreateUser(UserID, Password, Email)
        User.IsApproved = False
        Membership.UpdateUser(User)
        da.Insert(UserID, Gender, Name, Email, ContactNo, "", "", Birthday, Now(), FaceBookID, UserPicUrl)

        Roles.AddUserToRole(UserID, "Member")

        SendVerification(User)
        Status = True

        Return Status
    End Function
    Protected Sub SendVerification(ByVal User As MembershipUser)
        Dim newUser As Guid = CType(User.ProviderUserKey, Guid)
        Dim BaseUrl As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & HttpContext.Current.Request.ApplicationPath
        Dim FullPath As String = BaseUrl & "VerifyNewUser.aspx?id=" & newUser.ToString()

        Dim sr As New StreamReader(HttpContext.Current.Server.MapPath("~/email_template/VerifyNewUser.txt"))
        Dim message As New MailMessage()

        message.IsBodyHtml = True
        message.From = New MailAddress(ConfigurationManager.AppSettings("EnquiryEmail"))
        message.To.Add(New MailAddress(Email, Name))
        message.Subject = "Email Verification from MajiTV"
        message.Body = sr.ReadToEnd()
        sr.Close()

        message.Body = message.Body.Replace("<%UserName%>", Name)
        message.Body = message.Body.Replace("<%VerificationUrl%>", FullPath)

        Dim Client As New SmtpClient()
        'Client.Send(message)
    End Sub

End Class
