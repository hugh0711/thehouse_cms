<%@ WebHandler Language="VB" Class="wineGroupList" %>

Imports System
Imports System.Web
Imports Newtonsoft.Json.Linq

Public Class wineGroupList : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim wineFunctionID As Integer = ConfigurationManager.AppSettings("FunctionID_Wine")
        'set lang found in query string requested and check if reuest is null or not
        Dim langRequest As String = ""
        
        'check query string "lang" have value or null
        Try
            langRequest = IIf(context.Request.QueryString("lang") Is Nothing Or context.Request.QueryString("lang").Length = 0 Or context.Request.QueryString("lang") = "", "en-us", context.Request.QueryString("lang").ToLower())
        Catch ex As Exception
            langRequest = "en-us"
        End Try
        Dim ResultString As New JObject
        
        Dim db As New ProductDbDataContext()
        Dim WineTagGroup = From t In db.view_Tags
                           Where t.FunctionID = wineFunctionID
                           Group t By t.TagGroup Into  Group
                           Select TagGroup
                           
        For Each tagGroup In WineTagGroup
            ResultString.Add(New JProperty(tagGroup.ToString().ToLower(), SearchByTagGroup(wineFunctionID, langRequest, tagGroup.ToLower())))
        Next
        
        context.Response.Write(ResultString.ToString())
        
    End Sub
    
#Region "SearchByTagGroup"
    Protected Function SearchByTagGroup(ByVal wineFunctionID As Integer, ByVal langRequest As String, ByVal tagGroup As String) As JArray
        Dim db As New ProductDbDataContext()
        Dim theProducts = New JArray(From p In db.view_Tags
              Where p.FunctionID = wineFunctionID And p.Lang.ToLower = langRequest And p.Enabled = True And p.TagGroup.ToLower()=tagGroup
              Order By p.SortOrder Ascending
              Select New JObject( _
                           New JProperty("TagID", p.TagID), _
                           New JProperty("Tag", p.Tag), _
                           New JProperty("TagGroup", p.TagGroup), _
                           New JProperty("Lang", p.Lang), _
                           New JProperty("SortOrder", p.SortOrder) _
                           ))
        Return theProducts
    End Function
#End Region
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class