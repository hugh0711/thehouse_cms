<%@ WebHandler Language="VB" Class="registerOrEditUser" %>

Imports System
Imports System.Web
Imports System.Net.Mail
Imports System.IO
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Public Class registerOrEditUser : Implements IHttpHandler
    
    Dim returnError As String = ""
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        'set result for output
        Dim ResultString As New JObject
        
        context.Response.ContentType = "application/json"
        
        Dim data = context.Request
        Dim sr As StreamReader = New StreamReader(data.InputStream)
        Dim stream = sr.ReadToEnd()
        Dim javaScriptSerializer = New JavaScriptSerializer()
        Dim postUser = javaScriptSerializer.Deserialize(Of PostUserClass)(stream)
        
        If postUser IsNot Nothing Then
            If postUser.UserName.Length > 0 Then
                returnError = saveUser(postUser)
                'returnError = "" mean success
                If returnError = "" Then
                    Roles.AddUserToRole(postUser.UserName, "Member")
                    ResultString.Add(New JProperty("Result", "Success"))
                Else
                    ResultString.Add(New JProperty("Result", "Error"))
                    ResultString.Add(New JProperty("Error", returnError))
                End If
            Else
                ResultString.Add(New JProperty("Result", "Error"))
                ResultString.Add(New JProperty("Error", "Missing Username"))
            End If
        Else
            ResultString.Add(New JProperty("Result", "Error"))
            ResultString.Add(New JProperty("Error", "Missing Informations"))
        End If
        context.Response.Write(ResultString.ToString())
    End Sub
    
    Protected Sub SendConfirmation(ByVal UserEmail As String,ByVal UserName As String)
        'Dim BaseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority) & Request.ApplicationPath

        Dim sr As New StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/email_template/index.html"))
        Dim message As New MailMessage()

        message.IsBodyHtml = True
        message.From = New MailAddress(ConfigurationManager.AppSettings("EnquiryEmail"))
        message.To.Add(New MailAddress(UserEmail, UserName))
        message.Subject = "Welcome from TheHouse"
        message.Body = sr.ReadToEnd()
        sr.Close()

        message.Body = message.Body.Replace("<%UserName%>", UserName)
        message.Body = message.Body.Replace("<%activationLink%>", String.Format("http://stage.innowil.com/thehouse/newuser.aspx?username={0}", UserName))

        Dim Client As New SmtpClient()
        Client.Send(message)
    End Sub
 
    Protected Function saveUser(ByVal userClass As PostUserClass) As String
        Dim MemberAdaptor As New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter
        Dim roleItem As ListItem
        Dim rolename As String
        Dim Username As String
        Dim StartDate, EndDate As Date
        Dim Interested As String = ""
        Dim User As MembershipUser
        Dim ContactNumber As String = userClass.ContactNumber
        Dim Gender As String = userClass.Gender
        Dim Region As String = ""
        Dim Birthday As Date
        Dim UserPic As String = ""
        Dim displayName As String = userClass.DisplayName
        Dim UserEmail As String = userClass.Email
        Dim DeliveryAddress As String = userClass.DeliveryAddress
        Dim Country As String = userClass.Country
        Dim FacebookID As String = userClass.FacebookID
        Dim Password As String = userClass.Password
        Dim NewPassword As String = userClass.NewPassword
        
        Username = userClass.UserName
        StartDate = #1/1/1900#
        EndDate = #1/1/2999#
        If IsDate(userClass.Birthday) Then
            Birthday = userClass.Birthday
        Else
            Birthday = Utility.NoBirthday
        End If

        'UserPic = saveUserImage()

        If userClass.NewUser Then
            User = Membership.GetUser(Username)
            If User Is Nothing Then
                If Password.Length >= 6 Then
                    User = Membership.CreateUser(Username, Password)
                    MemberAdaptor.Insert(Username, Gender, displayName, UserEmail, ContactNumber, DeliveryAddress, Country, Birthday, DateTime.Now, FacebookID, UserPic)
                    User.Email = UserEmail
                    User.IsApproved = False
                    Membership.UpdateUser(User)
                    Try
                        'Dim newNserID = User.ProviderUserKey.ToString()
                        SendConfirmation(UserEmail, Username)
                    Catch ex As Exception
                        returnError = ex.Message()
                        Return returnError
                        Exit Function
                    End Try
                Else
                    returnError = "Password must contain 6 length or longer."
                    Return returnError
                    Exit Function
                End If
            Else
                returnError = "Duplicate username."
                Return returnError
                Exit Function
            End If
        Else
            User = Membership.GetUser(Username)
            If User IsNot Nothing Then
                If userClass.ChangePassword Then
                    If userClass.Password = User.GetPassword Then
                        User.ChangePassword(User.GetPassword, NewPassword)
                        Membership.UpdateUser(User)
                    Else
                        returnError = "Old password not match."
                        Return returnError
                        Exit Function
                    End If
                End If
                'Dim getSavedfile = Path.GetFileName(imgSaved.ImageUrl)
                'If Not UserPic = getSavedfile Then
                '    UserPic = getSavedfile
                'End If
                MemberAdaptor.Update(Gender, displayName, UserEmail, ContactNumber, DeliveryAddress, Country, Birthday, Utility.StringToDateTime(User.CreationDate), FacebookID, UserPic, Username)
                User.Email = UserEmail
                Membership.UpdateUser(User)
            Else
                returnError = "User not found."
                Return returnError
                Exit Function
            End If
        End If
        
        'errorString = "" mean success
        Return returnError
        
    End Function
        
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class