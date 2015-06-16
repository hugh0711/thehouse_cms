Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

<JsonObject()> _
Public Class HtmlJsonClass

    <JsonProperty()> Public Html As String

    Public Sub New(ByVal Html As String)
        Me.Html = Html
    End Sub

End Class
