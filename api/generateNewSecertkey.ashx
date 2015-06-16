<%@ WebHandler Language="VB" Class="relatedEpisode" %>

Imports System
Imports System.Web
Imports System.Security.Cryptography

Public Class relatedEpisode : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        
        'set new GUIDs 
        Dim sGUID As String
        sGUID = System.Guid.NewGuid.ToString()
        'create new session ID
        Dim new_session_ID As String
        Using md5Hash As MD5 = MD5.Create()
            new_session_ID = GetMd5Hash(md5Hash, sGUID)
        End Using
        
        
        context.Response.ContentType = "text/plain"
        context.Response.Write(new_session_ID)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
   
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
    
    

End Class