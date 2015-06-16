Imports Microsoft.VisualBasic

Public Class NotesListClass
    Inherits List(Of NotesJsonClass)

    Public Sub New(dt As UserProductDataSet.view_UserProductImageDataTable, Optional HQ As Boolean = False, Optional Media As VideoClass.Media = VideoClass.Media.YouTube)
        Convert(dt, HQ, Media)
    End Sub

    Public Sub Load(dt As UserProductDataSet.view_UserProductImageDataTable, Optional HQ As Boolean = False, Optional Media As VideoClass.Media = VideoClass.Media.YouTube)
        Convert(dt, HQ, Media)
    End Sub

    Protected Sub Convert(dt As UserProductDataSet.view_UserProductImageDataTable, HQ As Boolean, Media As VideoClass.Media)
        Me.Clear()

        Dim u As NotesJsonClass
        For Each dr As UserProductDataSet.view_UserProductImageRow In dt.Rows
            u = New NotesJsonClass(dr, HQ, Media)
            Me.Add(u)
        Next
    End Sub

End Class
