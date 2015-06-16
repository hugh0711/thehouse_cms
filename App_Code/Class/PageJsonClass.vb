Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

Public Class PageJsonClass

    <JsonProperty()> Public Content As String

    Public Sub New()

    End Sub

    Public Sub New(PageID As Integer)
        Load(PageID)
    End Sub

    Public Sub Load(PageID As Integer)
        Dim da As New PageDataSetTableAdapters.view_PageTableAdapter()
        Dim dt As PageDataSet.view_PageDataTable
        Dim dr As PageDataSet.view_PageRow
        Dim Lang As String = ConfigurationManager.AppSettings("UIDefaultLanguage")

        dt = da.GetDataByIDLang(PageID, Lang)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
            Me.Content = dr.Content
        End If
    End Sub

End Class
