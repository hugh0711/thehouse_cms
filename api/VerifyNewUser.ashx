<%@ WebHandler Language="VB" Class="VerifyNewUser" %>

Imports System
Imports System.Web
Imports Newtonsoft.Json.Linq

Public Class VerifyNewUser : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim ResultString As New JObject
        Dim errorMessage As String = ""
        
        If String.IsNullOrEmpty(context.Request.QueryString("id")) Then
            errorMessage= String.Format("Invalid Url or activation code. Please check the email and try again or contact our customer service <a href='mailto:{0}'>{0}</a>", ConfigurationManager.AppSettings("EnquiryEmail"))
        Else
            Dim UserID As Guid
            Try
                UserID = New Guid(context.Request.QueryString("id"))

                Dim User As MembershipUser = Membership.GetUser(UserID)
                If User Is Nothing Then
                    errorMessage = String.Format("Invalid Url or activation code. Please check the email and try again or contact our customer service <a href='mailto:{0}'>{0}</a>", ConfigurationManager.AppSettings("EnquiryEmail"))
                ElseIf User.IsApproved Then
                    errorMessage = String.Format("This user is already activated. Please check the email and try again or contact our customer service <a href='mailto:{0}'>{0}</a>", ConfigurationManager.AppSettings("EnquiryEmail"))
                Else
                    User.IsApproved = True
                    Membership.UpdateUser(User)
                    ResultString.Add(New JProperty("Result", "Success"))
                End If

            Catch ex As Exception
                errorMessage = String.Format("Invalid Url or activation code. Please check the email and try again or contact our customer service <a href='{0}'>{0}</a>", ConfigurationManager.AppSettings("EnquiryEmail"))
            End Try
        End If
        
        If errorMessage.Length > 0 Then
            ResultString.Add(New JProperty("Result", "Success"))
        Else
            ResultString.Add(New JProperty("Result", "Error"))
        End If
        
        ResultString.Add(New JProperty("Message", errorMessage))
        
        context.Response.Write(ResultString.ToString())
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class