Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

<JsonObject()> _
Public Class CategoryJsonClass

    <JsonProperty()> Public ID As Integer
    <JsonProperty()> Public Title As String
    <JsonProperty()> Public Description As String
    <JsonProperty()> Public ImageUrl As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal CategoryRow As CategoryDataSet.view_CategoryRow, Optional ByVal MaxChar As Integer = 160, Optional HQ As Boolean = False)
        With CategoryRow
            Me.ID = .CategoryID
            Me.Title = .CategoryName
            Me.Description = Utility.TrimHtmlText(.Description, MaxChar)
            If .IsUrlNull Then
                Me.ImageUrl = ""
            Else
                If HQ Then
                    Me.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("CategoryImagePath"), .Url)
                Else
                    Me.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("CategoryThumbnailPath"), .Url)
                End If
            End If
        End With
    End Sub
End Class
