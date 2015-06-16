<%@ WebHandler Language="VB" Class="olFollow" %>

Imports System
Imports System.Web
Imports Newtonsoft.Json.Linq

Public Class olFollow : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        Dim ResultString As New JObject
        Dim ol As String = IIf(context.Request.QueryString("ol") Is Nothing, "", context.Request.QueryString("ol"))
        Dim follower As String = IIf(context.Request.QueryString("follower") Is Nothing, "", context.Request.QueryString("follower"))
        Dim OpinionLeaderRoleName As String = ConfigurationManager.AppSettings("OpinionLeaderRoleName")
        
        If ol.Length > 0 And follower.Length > 0 Then
            
            If Roles.IsUserInRole(ol, OpinionLeaderRoleName) Then
                
                Dim mu As MembershipUser = Membership.GetUser(follower)
                
                If mu IsNot Nothing Then
                    
                    Dim db As New ProductDbDataContext
                    
                    Dim followerFound = From o In db.OpinionLeaders
                                       Where o.OLUserName = ol And o.OLFollower = follower
                                       Select o
                    If followerFound.Count = 0 Then
                        
                        Dim newFollower As New OpinionLeader
                        newFollower.OLUserName = ol
                        newFollower.OLFollower = follower
                        
                        Try
                            db.OpinionLeaders.InsertOnSubmit(newFollower)
                            db.SubmitChanges()
                            
                            ResultString.Add(New JProperty("Result", "Success"))
                        Catch ex As Exception
                            ResultString.Add(New JProperty("Result", "Error"))
                            ResultString.Add(New JProperty("Error", ex.Message))
                        End Try
                    Else
                        ResultString.Add(New JProperty("Result", "Error"))
                        ResultString.Add(New JProperty("Error", "Already followed"))
                    End If
                    
                Else
                    ResultString.Add(New JProperty("Result", "Error"))
                    ResultString.Add(New JProperty("Error", "Follower username not found"))
                End If
                
            Else
                ResultString.Add(New JProperty("Result", "Error"))
                ResultString.Add(New JProperty("Error", "User not in OpinionLeader Role"))
            End If
            
        Else
            ResultString.Add(New JProperty("Result", "Error"))
            ResultString.Add(New JProperty("Error", "Missing Data"))
        End If

        context.Response.Write(ResultString.ToString())
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class