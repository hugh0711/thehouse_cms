Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

<JsonObject()>
Public Class NotesJsonClass

    <JsonProperty()> Public ID As Integer
    <JsonProperty()> Public VideoUrl As String
    <JsonProperty()> Public PreviewUrl As String
    <JsonProperty()> Public EpisodeID As Integer
    <JsonProperty()> Public EpisodeName As String
    <JsonProperty()> Public Notes As String
    <JsonProperty()> Public GroupID As Integer
    <JsonProperty()> Public GroupName As String
    '<JsonProperty()> Public TypeID As Integer
    '<JsonProperty()> Public TypeName As String

    Public Sub New(ID As Integer, Optional HQ As Boolean = False, Optional Media As VideoClass.Media = VideoClass.Media.YouTube)
        Load(ID, HQ, Media)
    End Sub

    Public Sub New(UserProduct As UserProductDataSet.view_UserProductImageRow, Optional HQ As Boolean = False, Optional Media As VideoClass.Media = VideoClass.Media.YouTube)
        Load(UserProduct, HQ, Media)
    End Sub

    Public Sub Load(ID As Integer, Optional Lang As String = "", Optional HQ As Boolean = False, Optional Media As VideoClass.Media = VideoClass.Media.YouTube)
        If Lang = "" Then Lang = ConfigurationManager.AppSettings("UIDefaultLanguage")
        Dim dt As UserProductDataSet.view_UserProductImageDataTable = (New UserProductDataSetTableAdapters.view_UserProductImageTableAdapter()).GetDataByID(ID, Lang)
        If dt.Rows.Count > 0 Then
            Load(dt.Rows(0), HQ, Media)
        End If
    End Sub

    Public Sub Load(UserProduct As UserProductDataSet.view_UserProductImageRow, Optional HQ As Boolean = False, Optional Media As VideoClass.Media = VideoClass.Media.YouTube)
        With UserProduct
            Me.ID = .ID
            Select Case Media
                Case VideoClass.Media.YouTube
                    Me.VideoUrl = VideoClass.GetVideoUrl(.MOQUnit)
                Case Else
                    If .IsVideoUrlNull Then
                        Me.VideoUrl = ""
                    Else
                        Me.VideoUrl = .VideoUrl
                    End If
            End Select
            If HQ Then
                Me.PreviewUrl = VideoClass.GetHQPreview(.MOQUnit)
            Else
                Me.PreviewUrl = VideoClass.GetPreview(.MOQUnit)
            End If
            Me.EpisodeID = .ProductID
            Me.EpisodeName = .ProductName
            Me.Notes = Utility.TrimHtmlText(.LongDescription)
            'Me.Notes = .fileUrl
            'If Not String.IsNullOrWhiteSpace(Me.Notes) Then
            '    If Not Me.Notes.StartsWith("http://") Then
            '        If Me.Notes.StartsWith("/") Then
            '            Me.Notes = "~" & Me.Notes
            '        Else
            '            Me.Notes = "~/" & Me.Notes
            '        End If
            '    End If
            'End If
            Me.GroupID = .GroupID
            Me.GroupName = .GroupName
            'Me.TypeID = .TypeID
            'Me.TypeName = .TypeName
        End With
    End Sub

End Class
