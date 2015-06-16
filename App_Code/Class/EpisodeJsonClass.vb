Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

<JsonObject()> _
Public Class EpisodeJsonClass
    <JsonProperty()> Public ChannelTitle As String
    <JsonProperty()> Public ChannelID As Integer
    <JsonProperty()> Public ProgramTitle As String
    <JsonProperty()> Public ProgramID As Integer
    <JsonProperty()> Public EpisodeTitle As String
    <JsonProperty()> Public EpisodeID As Integer
    <JsonProperty()> Public Description As String
    <JsonProperty()> Public YouTubeID As String
    <JsonProperty()> Public FacebookID As String
    <JsonProperty()> Public VideoUrl As String
    <JsonProperty()> Public is3D As Boolean
    <JsonProperty()> Public PreviewUrl As String
    <JsonProperty()> Public Notes As String
    <JsonProperty()> Public IsMyVideo As Boolean = False
    <JsonProperty()> Public IsMyCollection As Boolean = False


    Public Sub New(ByVal EpisodeID As Integer, Optional Lang As String = "", Optional HQ As Boolean = False, Optional Media As VideoClass.Media = VideoClass.Media.YouTube)
        Load(EpisodeID, Lang, HQ, Media)
    End Sub

    Public Sub Load(ByVal EpisodeID As Integer, Optional Lang As String = "", Optional HQ As Boolean = False, Optional Media As VideoClass.Media = VideoClass.Media.YouTube)
        Dim VideoAdapter As New ProductDataSetTableAdapters.view_ProductTableAdapter()
        Dim VideoTable As ProductDataSet.view_ProductDataTable
        Dim VideoRow As ProductDataSet.view_ProductRow
        If Lang = "" Then Lang = ConfigurationManager.AppSettings("UIDefaultLanguage")

        VideoTable = VideoAdapter.GetDataByProductID(EpisodeID, Lang)
        If VideoTable.Rows.Count > 0 Then
            VideoRow = VideoTable.Rows(0)

            With VideoRow
                Me.EpisodeID = EpisodeID
                Me.EpisodeTitle = .ProductName
                Me.Description = .Description
                Me.YouTubeID = .MOQUnit
                Me.FacebookID = .ProductionLeadTime
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
                Me.is3D = .Video3D
                Me.Notes = .LongDescription
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
                If HQ Then
                    Me.PreviewUrl = VideoClass.GetHQPreview(.MOQUnit)
                Else
                    Me.PreviewUrl = VideoClass.GetPreview(.MOQUnit)
                End If

                Dim CategoryAdapter As New CategoryDataSetTableAdapters.view_CategoryTableAdapter()
                Dim CategoryTable As CategoryDataSet.view_CategoryDataTable
                Dim CategoryRow As CategoryDataSet.view_CategoryRow

                CategoryTable = CategoryAdapter.GetDataByCategoryID(.CategoryID, Lang)
                If CategoryTable.Rows.Count > 0 Then
                    CategoryRow = CategoryTable.Rows(0)

                    'txtProgram.Text = .CategoryName
                    Me.ProgramID = .CategoryID
                    Me.ProgramTitle = .CategoryName

                    Me.ChannelID = CategoryAdapter.GetParentID(Me.ProgramID)
                    Me.ChannelTitle = CategoryAdapter.GetCategoryName(Me.ChannelID, Lang)
                End If
            End With

            ' Check if it is in the Favorite Video
            If HttpContext.Current.User.Identity.IsAuthenticated Then
                Dim Username As String = HttpContext.Current.User.Identity.Name
                Dim TypeID As Integer = CInt(ConfigurationManager.AppSettings("FavVideoTypeID"))
                Dim VideoCount As Integer = (New UserProductDataSetTableAdapters.UserProductTableAdapter()).GetProductCount(Username, EpisodeID, TypeID).GetValueOrDefault(0)
                If VideoCount = 0 Then
                    IsMyVideo = False
                Else
                    IsMyVideo = True
                End If

                TypeID = CInt(ConfigurationManager.AppSettings("CollectionTypeID"))
                Dim GroupID As Integer = (New UserProductDataSetTableAdapters.UserProductTableAdapter()).GetGroupID(Username, EpisodeID, TypeID).GetValueOrDefault(-1)
                If GroupID = -1 Then
                    IsMyCollection = False
                Else
                    IsMyCollection = True
                End If
            End If
        End If
    End Sub

End Class
