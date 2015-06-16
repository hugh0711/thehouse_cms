<%@ WebHandler Language="VB" Class="getUserInfo" %>

Imports System
Imports System.Web
Imports Newtonsoft.Json.Linq

Public Class getUserInfo : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        Dim ResultString As New JObject
        
        Dim username As String = IIf(context.Request.QueryString("UserName") Is Nothing, "", context.Request.QueryString("UserName"))

        Dim mu As MembershipUser = Membership.GetUser(username)
        
        If mu Is Nothing Or username.Length = 0 Then
            ResultString.Add(New JProperty("Result", "Error"))
            ResultString.Add(New JProperty("Error", "Missing Information or user not found"))
        Else
            Dim r As MemberDetailDataSet.MemberDetailRow
            Dim t As New MemberDetailDataSet.MemberDetailDataTable
            t = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter).GetDataByUserID(username)
            If t.Rows.Count > 0 Then
                r = t.Rows(0)
                With r
                    ResultString.Add(New JProperty("DisplayName", .Name))
                    ResultString.Add(New JProperty("DeliveryAddress", .DeliveryAddress))
                    ResultString.Add(New JProperty("Email", .Email))
                    ResultString.Add(New JProperty("ContactNumber", .ContactNo))
                    ResultString.Add(New JProperty("Gender", .Gender))
                    ResultString.Add(New JProperty("Country", .Country))
                    ResultString.Add(New JProperty("Birthday", .Birthday))
                    ResultString.Add(New JProperty("FacebookID", .FacebookUserID))
                    'If Not .IsUserPicUrlNull Then
                    '    imgSaved.ImageUrl = Path.Combine(ConfigurationManager.AppSettings("UserImagePath"), .UserPicUrl)
                    'End If
                End With
            Else
                ResultString.Add(New JProperty("Result", "Error"))
                ResultString.Add(New JProperty("Error", "User not found"))
            End If
        End If
        context.Response.Write(ResultString.ToString())
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class