Imports Microsoft.VisualBasic

Public Class JsonClass

    Public Shared Function Callback(Context As HttpContext, JSONString As String) As String
        If Context.Request("callback") IsNot Nothing AndAlso Context.Request("callback") <> "" Then
            JSONString = String.Format("{0}({1})", Context.Request("callback"), JSONString)
        End If
        Return JSONString
    End Function

    Public Shared Function GetStatus(Status As Boolean, Message As String) As String
        Dim Param As New List(Of String)
        Param.Add("""status"":" & Status.ToString().ToLower())
        Param.Add("""message"":""" & Message & """")
        Dim Json As String = "{" & Join(Param.ToArray(), ",") & "}"
        Return Json
    End Function

End Class
