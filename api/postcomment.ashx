<%@ WebHandler Language="VB" Class="postcomment" %>

Imports System
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class postcomment : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        'set result for output
        Dim ResultString As New JObject
        
        context.Response.ContentType = "application/json"
        
        Dim data = context.Request
        Dim sr As StreamReader = New StreamReader(data.InputStream)
        Dim stream = sr.ReadToEnd()
        Dim javaScriptSerializer = New JavaScriptSerializer()
        Dim postComment = javaScriptSerializer.Deserialize(Of PostCommentClass)(stream)
        
        If postComment IsNot Nothing Then
            
            'create variable for get user by user name
            Dim mu As MembershipUser = Membership.GetUser(postComment.UserID)
            
            If mu IsNot Nothing Then
                Dim db As New ProductDbDataContext()
                Dim newComment As New Comment
                newComment.CommentType = postComment.CommentType
                newComment.ReferenceID = postComment.ReferenceID
                newComment.UserID = postComment.UserID
                newComment.CommentDate = Date.Now()
                newComment.CommentDescription = postComment.CommentDescription
                newComment.IsInspected = postComment.IsInspected
                newComment.IsDisable = postComment.IsDisable
                newComment.ParentID = postComment.ParentID
                newComment.MediaUrl = ""
                newComment.MediaTitle = ""
                newComment.MediaDesc = ""
                Try
                    db.Comments.InsertOnSubmit(newComment)
                    db.SubmitChanges()
                    ResultString.Add(New JProperty("Result", "Success"))
                Catch ex As Exception
                    ResultString.Add(New JProperty("Result", "Error"))
                    ResultString.Add(New JProperty("Error", ex.Message))
                End Try
            Else
                ResultString.Add(New JProperty("Result", "Error"))
                ResultString.Add(New JProperty("Error", "User not found"))
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