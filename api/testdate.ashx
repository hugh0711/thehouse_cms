<%@ WebHandler Language="VB" Class="testdate" %>

Imports System
Imports System.Web

Public Class testdate : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        
        'context.Response.Write(ConvertToDatetime("1990/01/01", "12:00am"))
        Dim testDate = DateTime.Parse("1990-01-01 12:00:00.000")
        context.Response.Write(testDate.ToString("hh:mm tt"))
        
    End Sub
 
    Protected Function ConvertToDatetime(ByVal dateString As String, ByVal timeString As String) As DateTime
        Dim returnDatetime As DateTime

        Try
            returnDatetime = DateTime.Parse(String.Format("{0} {1}", dateString, timeString))
            
            'returnDatetime = DateTime.ParseExact(String.Format("{0} {1}", dateString, timeString), "yyyy/MM/dd h:mmtt", Nothing)
        Catch ex As Exception
            'set time string to "0000" if timeString format not correct 
            returnDatetime = DateTime.ParseExact(String.Format("{0}{1}", dateString, "0000"), "yyyyMMddHHmm", Nothing)
        End Try

        Return returnDatetime
    End Function
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class