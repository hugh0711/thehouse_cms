<%@ WebHandler Language="VB" Class="LikeProductOrComment" %>

Imports System
Imports System.Web
Imports Newtonsoft.Json.Linq

Public Class LikeProductOrComment : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        Dim typeRequest As String =IIf(context.Request.QueryString("type") Is Nothing, "", context.Request.QueryString("type"))
        'Pass commentID if type is "comment". Otherwise, Pass productID for RID.
        Dim ReferenceIDRequest As Integer = IIf(context.Request.QueryString("RID") Is Nothing Or Not IsNumeric(context.Request.QueryString("RID")), 0, Convert.ToInt32(context.Request.QueryString("RID")))
        Dim username As String = IIf(context.Request.QueryString("UserName") Is Nothing, "", context.Request.QueryString("UserName"))
        'like=1 mean like, like=0 mean unlike
        Dim LikeRequest As Integer = IIf(context.Request.QueryString("like") Is Nothing Or Not IsNumeric(context.Request.QueryString("like")), 1, Convert.ToInt32(context.Request.QueryString("like")))
        'create variable for get user by user name
        Dim mu As MembershipUser = Membership.GetUser(username)
        'set result for output
        Dim ResultString As New JObject
        Dim db As New ProductDbDataContext()
        
        If mu Is Nothing Then
            ResultString.Add(New JProperty("Result", "Error"))
            ResultString.Add(New JProperty("Error", "User not found"))
            context.Response.Write(ResultString.ToString())
            Exit Sub
        End If
        
        If Not ReferenceIDRequest = 0  And mu IsNot Nothing Then
            'get user ID
            Dim found_userId As String = mu.ProviderUserKey.ToString()
            If LikeRequest = 1 Then
                Dim LikeRecord = From l In db.LikeTables
                              Where l.UserID = username And l.ReferenceID = ReferenceIDRequest And l.LikeType = typeRequest
                              Select l
                If LikeRecord.Count = 0 Then
                    Try
                        Dim newLiketable As New LikeTable
                        newLiketable.LikeType = typeRequest
                        newLiketable.ReferenceID = ReferenceIDRequest
                        newLiketable.UserID = username
                        newLiketable.CreateDate = Date.Now()
                        db.LikeTables.InsertOnSubmit(newLiketable)
                        db.SubmitChanges()
                        ResultString.Add(New JProperty("Result", "Success"))
                        ResultString.Add(New JProperty("Message", String.Format("Like {0}", ReferenceIDRequest)))
                    Catch ex As Exception
                        ResultString.Add(New JProperty("Result", "Error"))
                        ResultString.Add(New JProperty("Error", ex.Message))
                    End Try
                Else
                    ResultString.Add(New JProperty("Result", "Error"))
                    ResultString.Add(New JProperty("Error", String.Format("Already like {0}", ReferenceIDRequest)))
                End If
            ElseIf LikeRequest = 0 Then
                Dim deleteLike = From l In db.LikeTables
                               Where l.UserID = username And l.ReferenceID = ReferenceIDRequest And l.LikeType = typeRequest
                               Select l
                For Each deleteLikeInfo In deleteLike
                    Try
                        db.LikeTables.DeleteOnSubmit(deleteLike.FirstOrDefault())
                        db.SubmitChanges()
                        ResultString.Add(New JProperty("Result", "Success"))
                        ResultString.Add(New JProperty("Message", String.Format("Unlike {0}", ReferenceIDRequest)))
                    Catch ex As Exception
                        ResultString.Add(New JProperty("Result", "Error"))
                        ResultString.Add(New JProperty("Error", ex.Message))
                        context.Response.Write(ResultString.ToString())
                        Exit Sub
                    End Try
                Next
            End If
        Else
            ResultString.Add(New JProperty("Result", "Error"))
            ResultString.Add(New JProperty("Error", "Missing information(s)"))
        End If
        context.Response.Write(ResultString.ToString())
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class