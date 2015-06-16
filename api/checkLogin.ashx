<%@ WebHandler Language="VB" Class="checkLogin" %>

Imports System
Imports System.Web
Imports System.Security.Cryptography
Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class checkLogin : Implements IHttpHandler
    
    Protected loginSession As New LoginSessionClass
    Protected logoutSession As New LogoutSessionClass
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        'set result for output
        Dim ResultString As New JObject
        'create variable for result
        Dim result As New JArray
        'create variable for get username in QueryString
        Dim username As String = IIf(context.Request.QueryString("UserName") Is Nothing, "", context.Request.QueryString("UserName"))
        'create variable for get encoded password in QueryString
        Dim password As String = IIf(context.Request.QueryString("password") Is Nothing, "", context.Request.QueryString("password"))
        'create variable for get session ID in QueryString
        Dim session_id As String = IIf(context.Request.QueryString("SID") Is Nothing, "", context.Request.QueryString("SID"))
        'create variable for get key(timestamp) in QueryString
        'Dim encode_key As String = IIf(context.Request.QueryString("key") Is Nothing, "", context.Request.QueryString("key"))
        'get encode key(SecertKey) from confid file "SecertKey" variable
        Dim encode_key As String = ConfigurationManager.AppSettings("SecertKey")
        'create variable for get session ID in QueryString
        Dim logout As Integer = IIf(context.Request.QueryString("logout") Is Nothing, 0, context.Request.QueryString("logout"))
        'create variable to save encoded password
        Dim encoded_password As String = ""
        'set database for search
        Dim loginDatabase As New LoginAPIDataContext
        
        If logout = 1 Then
            'search session_id if it is not null. Otherwise, search by username and password.
            If Not session_id = "" And session_id.Length > 0 Then
                'search login details by session_id
                Dim session_found = (From p In loginDatabase.Login_Sessions
                                  Where p.sid = session_id
                                  Select p).FirstOrDefault
                'run if found session_id from database
                If session_found IsNot Nothing Then
                    'check is timeout or not by check_SessionTimeout with last modified timestamp
                    If check_SessionTimeout(session_found.timemodified) Then
                        'Session timeout
                        'set login_success to 1 (login not success)
                        logoutSession.Return_LogoutSuccess = 0
                    Else
                        'modify sessionID for logout
                        ModifySessionIDToLogout(session_id)
                    End If
                Else
                    'set login_success to 1 (login not success)
                    logoutSession.Return_LogoutSuccess = 1
                    'set error message
                    logoutSession.Return_ErrorMessage = "Session ID not found"
                End If
            End If
            result.Add(New JObject(New JProperty("LogoutResult", logoutSession.Return_LogoutSuccess), _
                                   New JProperty("ErrorMessage", logoutSession.Return_ErrorMessage) _
                                   ))
            ResultString.Add(New JProperty("LogoutRequest", result))
            context.Response.Write(ResultString.ToString())
        Else
            'search session_id if it is not null. Otherwise, search by username and password.
            If Not session_id = "" And session_id.Length > 0 Then
                'search login details by session_id
                Dim session_found = (From p In loginDatabase.Login_Sessions
                                  Where p.sid = session_id
                                  Select p).FirstOrDefault
                'run if found session_id from database
                If session_found IsNot Nothing Then
                    'check is timeout or not by check_SessionTimeout with last modified timestamp
                    If check_SessionTimeout(session_found.timemodified) Then
                        'set login_success to 1 (login not success)
                        loginSession.Return_LoginSuccess = 1
                        'set error message
                        loginSession.Return_ErrorMessage = "Session timeout"
                    Else
                        'update login session record by session ID if it is not timeout
                        UpdateSessionBySID(session_id)
                    End If
                Else
                    'set login_success to 1 (login not success)
                    loginSession.Return_LoginSuccess = 1
                    'set error message
                    loginSession.Return_ErrorMessage = "Session ID not found"
                End If
            Else
                'get encoded password and check password if username and password are not null
                If Not password = "" And password.Length > 0 And Not username = "" And username.Length > 0 And Not password = "" And password.Length > 0 And Not encode_key = "" And encode_key.Length > 0 Then
                    'get encoded password from GetEncodedPasswordByUserName
                    encoded_password = GetEncodedPasswordByUserName(username, encode_key)
                    'run if password is correct and input password is not null
                    If encoded_password.Length > 0 And encoded_password = password Then
                        'set login_success to 0 (login success)
                        loginSession.Return_LoginSuccess = 0
                        'create variable for get user by user name
                        Dim mu As MembershipUser = Membership.GetUser(username)
                        'get user ID
                        Dim found_userId As String = mu.ProviderUserKey.ToString()
                        'insert to database and return Session ID
                        InsertNewSessionRecord(username)
                    Else
                        'set login_success to 1 (login not success)
                        loginSession.Return_LoginSuccess = 1
                        'set error message
                        loginSession.Return_ErrorMessage = "Incorrect Password"
                    End If
                Else
                    'set login_success to 1 (login not success)
                    loginSession.Return_LoginSuccess = 1
                    'set error message
                    loginSession.Return_ErrorMessage = "Username/ Password/ Key(timestamp) is null"
                End If
            End If
            result.Add(New JObject(New JProperty("LoginResult", loginSession.Return_LoginSuccess), _
                                   New JProperty("UserRole", loginSession.Return_UserRole), _
                                   New JProperty("Session", loginSession.Return_SessionID), _
                                   New JProperty("ErrorMessage", loginSession.Return_ErrorMessage) _
                                   ))
            ResultString.Add(New JProperty("LoginRequest", result))
            context.Response.Write(ResultString.ToString())
        End If
        
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    
    Protected Sub InsertNewSessionRecord(ByVal user_name As String)
        'set database
        Dim loginDatabase As New LoginAPIDataContext()
        
        'search login session record by user name
        Dim find_lastLogin_record = (From p In loginDatabase.Login_Sessions
                                     Order By p.timemodified Descending
                                  Where p.UserName = user_name
                                  Select p).FirstOrDefault()
                                  
        'create variable for saving last modified login timestamp
        Dim last_Login_timestamp As Long = 0
        
        Try
            'set last login timestamp if exist. Otherwise, set it to 0
            last_Login_timestamp = find_lastLogin_record.timemodified
        Catch ex As Exception
            last_Login_timestamp = 0
        End Try
        
        
        'run if last login session timeout. Check last login session timeout or not by check_SessionTimeout with last last Login record 
        If check_SessionTimeout(last_Login_timestamp) Then
            
        
            'set current timestamp
            Dim now_timestamp = DateTimeToUnixTimestamp(DateTime.Now)
            'set new GUIDs 
            Dim sGUID As String
            sGUID = System.Guid.NewGuid.ToString()
            'create new session ID
            Dim new_session_ID As String
            Using md5Hash As MD5 = MD5.Create()
                new_session_ID = GetMd5Hash(md5Hash, sGUID)
            End Using

            Dim session_record As New Login_Session
            session_record.UserName = user_name
            session_record.sid = new_session_ID
            session_record.timecreated = now_timestamp
            session_record.timemodified = now_timestamp
        
            loginDatabase.Login_Sessions.InsertOnSubmit(session_record)
        
            Try
                'update database record
                loginDatabase.SubmitChanges()
                'set login_success to 0 (login success)
                loginSession.Return_LoginSuccess = 0
                'set role role(s) by setUserRoles function
                loginSession.Return_UserRole = setUserRoles(Roles.GetRolesForUser(user_name))
                
                
                
            Catch
                'set login_success to 0 (login not success)
                loginSession.Return_LoginSuccess = 1
                'set error message
                loginSession.Return_ErrorMessage = "New Session ID error"
            End Try

       
            loginSession.Return_SessionID = new_session_ID
        Else
            'get preious session ID
            Dim previous_SessionID = find_lastLogin_record.sid
            'set session ID for return
            loginSession.Return_SessionID = previous_SessionID
            'update login session record by previous_SessionID
            UpdateSessionBySID(previous_SessionID)
        End If
        
        
    End Sub
    
    Protected Sub UpdateSessionBySID(ByVal session_ID As String)
        'set database
        Dim loginDatabase As New LoginAPIDataContext
        'find session record by session id
        Dim session_record = (From s In loginDatabase.Login_Sessions
                           Where s.sid = session_ID
                           Select s).FirstOrDefault
        
        'run if session_record found
        If session_record IsNot Nothing Then
            
            'update last modify timestamp by now timestamp
            session_record.timemodified = DateTimeToUnixTimestamp(DateTime.Now)
            
            Try
                'update database record
                loginDatabase.SubmitChanges()
                'set login_success to 0 (login success)
                loginSession.Return_LoginSuccess = 0
                'set role role(s) by setUserRoles function
                loginSession.Return_UserRole = setUserRoles(Roles.GetRolesForUser(session_record.UserName))
                'set session ID
                loginSession.Return_SessionID = session_ID
            Catch
                'set login_success to 0 (login not success)
                loginSession.Return_LoginSuccess = 1
                'set error message
                loginSession.Return_ErrorMessage = "Session ID not found"
            End Try
        Else
            'set login_success to 0 (login not success)
            loginSession.Return_LoginSuccess = 1
            'set error message
            loginSession.Return_ErrorMessage = "Session ID not found"
        End If
        
    End Sub
    
    
    Protected Function check_SessionTimeout(ByVal input_timestamp As Long) As Boolean
        
        'get Session Timeout by minutes from web config file
        Dim SessionTimeout = (ConfigurationManager.AppSettings("SessionTimeout"))
        
        'get last modified datetime by timemodified timestamp
        Dim found_datetime = ToDateTime(input_timestamp)
        'get session timeout datetime by found_datetime add session timeout minutes
        Dim session_timeout_datetime = found_datetime.AddMinutes(SessionTimeout)
        
        'set current datetime for checking
        Dim now_datetime As DateTime = DateTime.Now
        
        'return true if it is timeout.
        If now_datetime > session_timeout_datetime Then
            Return True
        Else
            Return False
        End If
        
    End Function
        
    
    Public Shared Function GetEncodedPasswordByUserName(ByVal username As String, ByVal encode_key As String) As String
        'create variable for result
        Dim result As String = ""
        'create variable for search user password by user name
        Dim found_password As String = ""
        
        Try
            'found password by user name
            found_password = Membership.Provider.GetPassword(username, "1")
        Catch ex As Exception
            'set to null if user name not found 
            found_password = ""
        End Try
        'run encode password if it can found password by user name
        If found_password.Length > 0 Then
            'create variable for ASCIIEncoding
            Dim encoding As New System.Text.ASCIIEncoding()
            'create variable for get encode_key by byte
            Dim keyByte As Byte() = encoding.GetBytes(encode_key)
            'create variable for encode_key HMACMD5
            Dim hmacmd5 As HMACMD5 = New HMACMD5(keyByte)
            'create variable for get found_password by byte
            Dim messageBytes As Byte() = encoding.GetBytes(found_password)
            'create variable for ComputeHash of found_password by encode_key
            Dim hashmessage As Byte() = hmacmd5.ComputeHash(messageBytes)
            'set result by ByteToString
            result = ByteToString(hashmessage).ToLower()
        End If
        
        
        Return result
    End Function
    
    Protected Function setUserRoles(ByVal userRoles As String()) As String
        'set result for return
        Dim result As String = ""
        
        For i As Integer = 0 To userRoles.Length - 1
            If i = userRoles.Length - 1 Then
                result &= String.Format("{0}", userRoles(i))
            Else
                result &= String.Format("{0}, ", userRoles(i))
            End If
            
        Next
        
        'return result
        Return result
        
    End Function
    
    Public Shared Function DateTimeToUnixTimestamp(ByVal _DateTime As DateTime) As Long

        Dim _UnixTimeSpan As TimeSpan = (_DateTime.Subtract(New DateTime(1970, 1, 1, 0, 0, 0, Nothing, DateTimeKind.Local)))
        Return CLng(Fix(_UnixTimeSpan.TotalSeconds))

    End Function
    
    Public Shared Function ToDateTime(timestamp As Long) As DateTime
        'set up unix time start datetime
        Dim dateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0)

        'get now datetime by adding input timestamp with AddMilliseconds function
        dateTime = dateTime.AddSeconds(timestamp)

        'return result
        Return FormatDateTime(dateTime)
    End Function
    
    
    Public Shared Function ByteToString(buff As Byte()) As String
        Dim sbinary As String = ""

        For i As Integer = 0 To buff.Length - 1
            ' hex format
            sbinary += buff(i).ToString("X2")
        Next
        Return (sbinary)
    End Function
    
    
    Shared Function GetMd5Hash(ByVal md5Hash As MD5, ByVal input As String) As String

        ' Convert the input string to a byte array and compute the hash. 
        Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input))

        ' Create a new Stringbuilder to collect the bytes 
        ' and create a string. 
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data  
        ' and format each one as a hexadecimal string. 
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string. 
        Return sBuilder.ToString()

    End Function 'GetMd5Hash
    
    Protected Sub ModifySessionIDToLogout(ByVal input_SessionID As String)
        'set database
        Dim logoutDatabase As New LoginAPIDataContext
        'find session record by session id
        Dim session_record = (From s In logoutDatabase.Login_Sessions
                           Where s.sid = input_SessionID
                           Select s).FirstOrDefault
        
        'run if session_record found
        If session_record IsNot Nothing Then
            
            'get Session Timeout by minutes from web config file
            Dim SessionTimeout = (ConfigurationManager.AppSettings("SessionTimeout"))
            
            'update last modify timestamp by now timestamp
            session_record.timemodified = DateTimeToUnixTimestamp(DateTime.Now.AddMinutes(-SessionTimeout))
            
            Try
                'update database record
                logoutDatabase.SubmitChanges()
                'set logout_success to 0 (login success)
                logoutSession.Return_LogoutSuccess = 0
            Catch
                'set logout_success to 1 (login not success)
                logoutSession.Return_LogoutSuccess = 1
                'set error message
                loginSession.Return_ErrorMessage = "Session ID not found"
            End Try
            
        Else
            'set logout_success to 1 (login not success)
            logoutSession.Return_LogoutSuccess = 1
            'set error message
            loginSession.Return_ErrorMessage = "Session ID not found"
        End If
    End Sub
    
End Class