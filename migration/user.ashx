<%@ WebHandler Language="VB" Class="user" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports MySql.Data
Imports MySql.Data.MySqlClient


Public Class user : Implements IHttpHandler
    
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
       
        Dim defaultPassword = "thehouse2015"
        Dim User As MembershipUser
        Dim db As New ProductDbDataContext()
        
        Dim allUserDetails = From d In db.MemberDetails
                           Select d
        
        db.MemberDetails.DeleteAllOnSubmit(allUserDetails)
        
        Try
            db.SubmitChanges()
        Catch ex As Exception
            context.Response.Write(String.Format("Delete all user member details error:{0} <br/>", ex.Message()))
        End Try
        
        Dim allusers = Membership.GetAllUsers()
        For Each founduser As MembershipUser In allusers
            Dim deleteUser = founduser.UserName.ToString()
            Membership.DeleteUser(deleteUser)
            context.Response.Write(String.Format("Deleted user:{0} <br/>", deleteUser))
        Next
        
        
        Dim constr As String = ConfigurationManager.ConnectionStrings("TheHouseMySql").ConnectionString
        Using con As New MySqlConnection(constr)
            con.Open()
            Using Com As New MySqlCommand("select a.*,p.* FROM thehouse.th_user_profile p , thehouse.th_user_acct a where p.user_id = a.user_id and a.status='Active'", con)
                'Using Com As New MySqlCommand("select * from userinfo WHERE emailAddress=?emailAddress", con)
                'Com.CommandType = Data.CommandType.Text
                'Com.Parameters.AddWithValue("?emailAddress", theUN)
                Using RDR = Com.ExecuteReader()
                    If RDR.HasRows Then
                        
                        
                        Do While RDR.Read
                            
                            Dim username As String = RDR.Item("user_login").ToString()
                            Dim password As String = RDR.Item("user_pwd").ToString()
                            If password.Length = 0 Then
                                password = defaultPassword
                            End If
                            Dim userRole As String = RDR.Item("member_type").ToString()
                            If userRole = "User" Then
                                userRole = "Member"
                            ElseIf userRole = "Opinion Leader" Then
                                userRole = "OpinionLeaders"
                            End If
                            
                            Dim Gender As String = RDR.Item("gender").ToString()
                            If Gender = "Male" Then
                                Gender = "M"
                            ElseIf Gender = "Female" Then
                                Gender = "F"
                            End If
                            
                            Dim displayName As String = RDR.Item("nickname").ToString()
                            Dim UserEmail As String = RDR.Item("email").ToString()
                            Dim ContactNumber As String = RDR.Item("phone_num").ToString()
                            Dim DeliveryAddress As String = RDR.Item("addr").ToString()
                            Dim Country As String = ""
                            Dim Birthday As String = RDR.Item("dob").ToString()
                            If Birthday.Length > 0 Then
                                If IsNumeric(Birthday) Then
                                    Dim edate = String.Format("10/12/{0}", Birthday)
                                    Birthday = Date.ParseExact(edate, "dd/MM/yyyy",
                                           System.Globalization.DateTimeFormatInfo.InvariantInfo)
                                End If
                            Else
                                Birthday = Utility.NoBirthday
                                
                            End If
                            
                            Dim loginMethod As String = RDR.Item("login_method").ToString()
                            Dim facebookID As String = ""
                            Dim googleplusID As String = ""
                            If loginMethod = "Facebook" Then
                                facebookID = RDR.Item("login_method_id").ToString()
                            ElseIf loginMethod = "Google Plus" Then
                                googleplusID = RDR.Item("login_method_id").ToString()
                            End If
                            
                            Dim userDesc As String = RDR.Item("user_desc").ToString()
                            Dim userWeb As String = RDR.Item("website").ToString()
                            Dim userWeibo As String = RDR.Item("weibo").ToString()
                            Dim userTwitter As String = RDR.Item("twitter").ToString()
                            
                            Dim UserPic As String = RDR.Item("photo").ToString()
                            'Dim getSavedfile = Path.GetFileName(imgSaved.ImageUrl)
                            If UserPic = "images/default_photo.jpg" Then
                                UserPic = "default.jpg"
                            Else
                                UserPic = SaveUserPhotoClass.saveUserPhoto(String.Format("http://thehouse.com.hk/{0}", UserPic))
                            End If
                            
                            User = Membership.GetUser(username)
                            If User Is Nothing Then
                                User = Membership.CreateUser(username, password)
                                Dim newMemberDetail As New MemberDetail With {
                                    .UserID = username, _
                                    .Gender = Gender, _
                                    .Name = displayName, _
                                    .Email = UserEmail, _
                                    .ContactNo = ContactNumber, _
                                    .DeliveryAddress = DeliveryAddress, _
                                    .Country = Country, _
                                    .Birthday = Birthday, _
                                    .CreateDate = Date.Now(), _
                                    .FacebookUserID = facebookID, _
                                    .UserPicUrl = UserPic, _
                                    .UserDesc = userDesc, _
                                    .UserWebsite = userWeb, _
                                    .GooglePlusUserID = googleplusID, _
                                    .WeiboUserID = userWeibo, _
                                    .TwitterUserID = userTwitter}
                                db.MemberDetails.InsertOnSubmit(newMemberDetail)
                                
                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception
                                    context.Response.Write(String.Format("Error:{0} <br/>", username))
                                End Try
                                
                                User.Email = UserEmail
                                User.IsApproved = True
                                Membership.UpdateUser(User)
                                
            
                                If Not Roles.RoleExists(userRole) Then
                                    Roles.CreateRole(userRole)
                                End If
                                
                                If Not Roles.IsUserInRole(username, userRole) Then
                                    Roles.AddUserToRole(username, userRole)
                                End If
                                
                                context.Response.Write(String.Format("Success:{0} <br/>", username))
                                
                                
                            End If
                            
                        Loop
                    End If
                End Using
            End Using
            con.Close()
        End Using
        
        Membership.CreateUser("admin", "admin2011", "wilton@innowil.com")

        Dim _Roles() As String = {"Admin", "Member"}
        Dim _role As String

        For Each _role In _Roles
            
            Roles.AddUserToRole("admin", _role)
        Next
        
        context.Response.Write("Admin user creaded")
        
    End Sub
 
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class