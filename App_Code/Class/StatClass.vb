Imports Microsoft.VisualBasic

Public Class StatClass

    Public Shared Sub AddViewCount(StatType As String, ID As Integer, Username As String)
        Dim RowAffected As Integer = (New StatDataSetTableAdapters.ViewCountTableAdapter()).AddViewCount(Now(), Username, "episode", ID)
        If RowAffected = 0 Then
            RowAffected = (New StatDataSetTableAdapters.ViewCountTableAdapter()).Insert("episode", ID, 1, Username, Now())
        End If
    End Sub

End Class
