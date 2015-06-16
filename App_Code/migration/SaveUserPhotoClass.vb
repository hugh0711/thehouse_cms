Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Net.Http


Public Class SaveUserPhotoClass

    Public Shared Function saveUserPhoto(ByVal source_url As String) As String

        'set image source
        Dim Source As String = source_url
        ' Get extension from source
        Dim extension As String = ".jpg"
        'set product image path
        Dim OriginalPathToFiles = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings("UserImagePath"))
        'set product image path
        Dim UploadPathToFiles = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings("UploadPath"))
        'set new GUIDs 
        Dim sGUID As String
        sGUID = System.Guid.NewGuid.ToString()
        'set file name
        Dim file_name As String = String.Format("{0}{1}", sGUID, extension)
        'set file full path
        Dim file_full_path As String = Path.Combine(String.Format("{0}/{1}", OriginalPathToFiles, file_name))

        Using Client As New System.Net.WebClient

            Dim MyImage As System.Drawing.Image

            MyImage = GetUserPhotoImageFromWeb(Source)
            'save image to ProductOriginalImagePath
            MyImage.Save(file_full_path)

            file_full_path = Path.Combine(String.Format("{0}/{1}", UploadPathToFiles, file_name))
            'save image to UploadPath
            MyImage.Save(file_full_path)

        End Using

        Return file_name

    End Function

    Public Shared Function GetUserPhotoImageFromWeb(ByVal URL As String) As System.Drawing.Image
        Dim Request As System.Net.HttpWebRequest
        Dim Response As System.Net.HttpWebResponse
        Request = System.Net.WebRequest.Create(URL)
        Response = CType(Request.GetResponse, System.Net.WebResponse)
        If Request.HaveResponse Then
            If Response.StatusCode = Net.HttpStatusCode.OK Then
                GetUserPhotoImageFromWeb = System.Drawing.Image.FromStream(Response.GetResponseStream)
            End If
        End If
        Try
        Catch e As System.Net.WebException
            MsgBox("A web exception has occured [" & URL & "]." & vbCrLf & " System returned: " & e.Message, MsgBoxStyle.Exclamation, "Error!")
            Exit Try
        Catch e As System.Net.ProtocolViolationException
            MsgBox("A protocol violation has occured [" & URL & "]." & vbCrLf & "  System returned: " & e.Message, MsgBoxStyle.Exclamation, "Error!")
            Exit Try
        Catch e As System.Net.Sockets.SocketException
            MsgBox("Socket error [" & URL & "]." & vbCrLf & "  System returned: " & e.Message, MsgBoxStyle.Exclamation, "Error!")
            Exit Try
        Catch e As System.IO.EndOfStreamException
            MsgBox("An IO stream exception has occured. System returned: " & e.Message, MsgBoxStyle.Exclamation, "Error!")
            Exit Try
        Finally
        End Try
    End Function

End Class
