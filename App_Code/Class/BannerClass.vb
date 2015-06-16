Imports Microsoft.VisualBasic
Imports Utility

Public Class BannerClass
	Public Shared ReadOnly BannerNoImage As String = "noimage.gif"
	Public Shared ReadOnly BannerImagePath As String = ConfigurationManager.AppSettings("BannerImagePath")
	'Public Shared ReadOnly AlbumBannerImagePath As String = ConfigurationManager.AppSettings("ProductImagePath")

	'Public Shared ReadOnly BannerPosition_HOME As String = "HOME"
	'Public Shared ReadOnly BannerPosition_EXPERT As String = "EXPERT"

	Public Shared ReadOnly BannerType_BANNER As String = "BANNER"
	'Public Shared ReadOnly BannerType_ADVERTISING As String = "ADVERTISING"

	Public Shared Sub InitRow(ByVal r As BannerDataSet.BannerRow)
		With r
			.BannerID = 0
			.BannerCreateDate = DateTime.Now
			.BannerName = ""
            .PositionID = 0
			.BannerImagePath = ""
			.BannerWidth = 0
			.BannerHeight = 0
			.BannerUrl = ""
            .SortOrder = 0
            .Enabled = True
		End With
	End Sub

	Public Shared Sub DeleteByBannerID(ByVal BannerID As Integer)
		Dim adapter As New BannerDataSetTableAdapters.BannerTableAdapter
		adapter.Delete(BannerID)
		Dim adapterLang As New BannerDataSetTableAdapters.BannerLangTableAdapter
		adapterLang.DeleteByBannerID(BannerID)
	End Sub

	Public Shared Sub InitRow(ByVal r As BannerDataSet.BannerLangRow, ByVal Culture As String)
		With r
            .id = 0
			.BannerID = 0
			.Lang = Culture
			.LangUrl = ""
			.LangName = ""
		End With
	End Sub

	'Public Shared Function AdvertisingTable() As BannerDataSet.BannerDataTable
	'	Dim t As New BannerDataSet.BannerDataTable
	'	t = (New BannerDataSetTableAdapters.BannerTableAdapter).GetDataByType(BannerType_ADVERTISING)
	'	Return t
	'End Function

    Public Shared Function AdvertisingRowByPosition(ByVal PositionID As Integer, ByVal t As BannerDataSet.BannerDataTable) As BannerDataSet.BannerRow
        Dim r As BannerDataSet.BannerRow
        r = t.NewRow
        InitRow(r)
        For Each r In t.Rows
            If r.PositionID = PositionID Then
                Exit For
            End If
        Next
        If Not r.BannerImagePath.Length > 0 Then
            r.BannerImagePath = BannerNoImage
        End If
        Return r
    End Function
End Class
