Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

<JsonObject()>
Public Class GroupJsonClass

    <JsonProperty()> Public ID As Integer
    <JsonProperty()> Public Group As String

    Public Sub New(ID As Integer, Group As String)
        Me.ID = ID
        Me.Group = Group
    End Sub

    Public Sub New(dr As UserProductDataSet.UserProductGroupRow)
        Me.ID = dr.GroupID
        Me.Group = dr.GroupName
    End Sub

End Class
