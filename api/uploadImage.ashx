<%@ WebHandler Language="VB" Class="uploadImage" %>

Imports System
Imports System.Web
Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class uploadImage : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        'set result for output
        Dim ResultString As New JObject
        context.Response.ContentType = "application/json"
        Dim typeRequest As String = IIf(context.Request.QueryString("type") Is Nothing, "", context.Request.QueryString("type"))
        'get which product function ID from web config file by QueryString "type".
        '*****Notice: case sensitive******
        Dim FunctionIDRequest As Integer = IIf(typeRequest Is Nothing Or typeRequest.Length = 0, 0, ConfigurationManager.AppSettings(String.Format("FunctionID_{0}", typeRequest)))
        If Not FunctionIDRequest = 0 Then
            Dim gelenResim As Stream = context.Request.InputStream
            If gelenResim.Length = 0 Then
                Dim db As New ProductDbDataContext()
                Dim productImageSize = (From s In db.SiteFunctions
                             Where s.FunctionID = FunctionIDRequest
                             Select s.ProductImageHeight, s.ProductImageWidth, s.ProductThumbnailHeight, s.ProductThumbnailWidth).FirstOrDefault()
                If productImageSize IsNot Nothing Then
                    Dim guidim = Guid.NewGuid().ToString()
                    'set product image path
                    Dim UploadPathToFiles = ConfigurationManager.AppSettings("UploadPath")
                    Dim filename = String.Format("{0}.jpg", guidim)
                    Using fileStream As FileStream = System.IO.File.Create(context.Server.MapPath(String.Format("{0}{1}", UploadPathToFiles, filename)), CInt(gelenResim.Length))
                        Dim bytesInStream(gelenResim.Length) As Byte
                        gelenResim.Read(bytesInStream, 0, CInt(bytesInStream.Length))
                        fileStream.Write(bytesInStream, 0, bytesInStream.Length)
                    End Using
                    ResultString.Add(New JProperty("Result", "Success"))
                    ResultString.Add(New JProperty("FileName", filename))
                Else
                    ResultString.Add(New JProperty("Result", "Error"))
                    ResultString.Add(New JProperty("Error", "Type not found"))
                End If
            Else
                ResultString.Add(New JProperty("Result", "Error"))
                ResultString.Add(New JProperty("Error", "Missing Image"))
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