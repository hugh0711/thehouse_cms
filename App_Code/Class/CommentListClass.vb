Imports Microsoft.VisualBasic

Public Class CommentListClass
    Inherits List(Of CommentJsonClass)

    Public Sub New(ByVal ReferenceID As Integer, ByVal CommentType As String, Optional ParentID As Integer = 0, Optional LoadReply As Boolean = False, Optional CharCount As Integer = Integer.MaxValue)
        LoadData(ReferenceID, CommentType, ParentID, LoadReply, CharCount)
    End Sub

    Public Sub New(ID As Integer, Optional LoadReply As Boolean = False, Optional CharCount As Integer = Integer.MaxValue)
        LoadData(ID, LoadReply, CharCount)
    End Sub

    Public Sub LoadData(ByVal ReferenceID As Integer, ByVal CommentType As String, Optional ParentID As Integer = 0, Optional LoadReply As Boolean = False, Optional CharCount As Integer = Integer.MaxValue)
        Dim daComment As New CommentDataSetTableAdapters.view_CommentTableAdapter()
        Dim dtComment As CommentDataSet.view_CommentDataTable
        Dim drComment As CommentDataSet.view_CommentRow
        Dim c As CommentJsonClass

        Me.Clear()
        dtComment = daComment.GetDataByReferenceIDParentID(ReferenceID, CommentType, ParentID)
        For Each drComment In dtComment.Rows
            With drComment
                c = New CommentJsonClass(drComment, CharCount)
                If LoadReply Then
                    c.LoadReply()
                Else
                    c.LoadReplyCount()
                End If
                Me.Add(c)
            End With
        Next
    End Sub

    Public Sub LoadData(ID As Integer, Optional LoadReply As Boolean = False, Optional CharCount As Integer = Integer.MaxValue)
        Dim daComment As New CommentDataSetTableAdapters.view_CommentTableAdapter()
        Dim dtComment As CommentDataSet.view_CommentDataTable
        Dim drComment As CommentDataSet.view_CommentRow
        Dim c As CommentJsonClass

        Me.Clear()
        dtComment = daComment.GetDataByCommentID(ID)
        For Each drComment In dtComment.Rows
            With drComment
                c = New CommentJsonClass(drComment, CharCount)
                If LoadReply Then
                    c.LoadReply()
                Else
                    c.LoadReplyCount()
                End If
                Me.Add(c)
            End With
        Next
    End Sub

End Class
