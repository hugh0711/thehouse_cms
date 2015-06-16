Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

<JsonObject()> _
Public Class CommentJsonClass

    <JsonProperty()> Public ID As Long
    <JsonProperty()> Public UserID As String
    <JsonProperty()> Public Name As String
    <JsonProperty()> Public CommentDate As String
    <JsonProperty()> Public Comment As String
    <JsonProperty()> Public LikeCount As Integer
    <JsonProperty()> Public MediaType As String
    <JsonProperty()> Public MediaPreviewUrl As String
    <JsonProperty()> Public MediaUrl As String
    <JsonProperty()> Public MediaTitle As String
    <JsonProperty()> Public MediaDesc As String
    <JsonProperty()> Public UserPicUrl As String
    <JsonProperty()> Public ReplyCount As Integer
    <JsonProperty()> Public Reply As List(Of CommentJsonClass)

    Public Sub New()

    End Sub

    Public Sub New(CommentID As Integer, ByVal UserID As String, ByVal Comment As String, ByVal CommentDate As Date, Optional ByVal ID As String = "")
        Me.ID = CommentID
        Me.UserID = UserID
        Me.Comment = Comment
        Me.CommentDate = GetTime(CommentDate)
        Me.MediaDesc = ID
        Me.Reply = New List(Of CommentJsonClass)
    End Sub

    Public Sub New(CommentID As Integer, Optional CharCount As Integer = Integer.MaxValue)
        Dim da As New CommentDataSetTableAdapters.view_CommentTableAdapter()
        Dim dt As CommentDataSet.view_CommentDataTable
        Dim dr As CommentDataSet.view_CommentRow

        dt = da.GetDataByCommentID(CommentID)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
            Load(dr, CharCount)
        End If

    End Sub

    Public Sub New(ByVal drComment As CommentDataSet.view_CommentRow, Optional CharCount As Integer = Integer.MaxValue)
        Load(drComment, CharCount)
    End Sub

    Protected Sub Load(ByVal drComment As CommentDataSet.view_CommentRow, Optional CharCount As Integer = Integer.MaxValue)
        With drComment
            Me.ID = .CommentID
            Me.UserID = .UserID
            Me.Name = .Name
            Me.CommentDate = GetTime(.CommentDate)
            Me.Comment = .CommentDescription
            Me.LikeCount = .LikeCount
            If Me.Comment.Length > CharCount Then
                Me.Comment = Me.Comment.Substring(0, CharCount)
            End If
            If .MediaUrl.StartsWith("youtube://") Then
                Me.MediaType = "youtube"
                Dim YTID As String = .MediaUrl.Substring(10)
                Me.MediaUrl = VideoClass.GetVideoUrl(YTID)
                Me.MediaPreviewUrl = VideoClass.GetPreview(YTID)
            ElseIf .MediaUrl.StartsWith("image://") Then
                Me.MediaType = "image"
                Dim Context As HttpContext = HttpContext.Current
                Dim Url As String = IO.Path.Combine(ConfigurationManager.AppSettings("CommentImagePath"), .MediaUrl.Substring(8))
                Url = Url.Replace("~/", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & HttpContext.Current.Request.ApplicationPath)
                Me.MediaUrl = Url
                Url = IO.Path.Combine(ConfigurationManager.AppSettings("CommentThumbnailPath"), .MediaUrl.Substring(8))
                Url = Url.Replace("~/", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & HttpContext.Current.Request.ApplicationPath)
                Me.MediaPreviewUrl = Url
            End If
            Me.MediaTitle = .MediaTitle
            Me.MediaDesc = .MediaDesc
            If .IsUserPicUrlNull Then
                Me.UserPicUrl = ConfigurationManager.AppSettings("MaleUserPicUrl")
            Else
                Me.UserPicUrl = .UserPicUrl
            End If
            Me.UserPicUrl = Me.UserPicUrl.Replace("~/", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & HttpContext.Current.Request.ApplicationPath)

            Me.Reply = New List(Of CommentJsonClass)
        End With
    End Sub

    Public Sub LoadReply()
        Dim daComment As New CommentDataSetTableAdapters.view_CommentTableAdapter()
        Dim dtComment As CommentDataSet.view_CommentDataTable
        Dim drComment As CommentDataSet.view_CommentRow
        Dim c As CommentJsonClass

        Me.ReplyCount = 0
        Reply.Clear()
        dtComment = daComment.GetDataByParentID(Me.ID)
        For Each drComment In dtComment.Rows
            With drComment
                c = New CommentJsonClass(drComment)
                Reply.Add(c)
                Me.ReplyCount += 1
            End With
        Next
    End Sub

    Public Sub LoadReplyCount()
        Me.ReplyCount = (New CommentDataSetTableAdapters.view_CommentTableAdapter()).GetReplyCount(Me.ID).GetValueOrDefault(0)
    End Sub

    Protected Function GetTime(ByVal CommentDate As DateTime) As String
        Dim ret As String = ""
        Dim CurrentTime As Date = Now()

        If CommentDate.AddHours(1) > CurrentTime Then
            ret = String.Format("{0}分鐘前發佈", DateDiff(DateInterval.Minute, CommentDate, CurrentTime))
        ElseIf CommentDate.AddDays(1) > CurrentTime Then
            ret = String.Format("{0}小時前發佈", DateDiff(DateInterval.Hour, CommentDate, CurrentTime))
        ElseIf CommentDate.AddDays(6) > CurrentTime Then
            ret = String.Format("{0}日前發佈", DateDiff(DateInterval.Day, CommentDate, CurrentTime))
        Else
            If CommentDate.Year = CurrentTime.Year Then
                ret = CommentDate.ToString("M月d日 h:mm")
            Else
                ret = CommentDate.ToString("yyyy年M月d日 h:mm")
            End If
        End If

        Return ret
    End Function

End Class
