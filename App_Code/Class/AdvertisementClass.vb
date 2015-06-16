Imports Microsoft.VisualBasic

Public Class AdvertisementClass

    Public Shared Function GetAd(PromoSettingID As Integer, Culture As String) As PromoDataSet.view_PromoRow
        Dim da As New PromoDataSetTableAdapters.view_PromoTableAdapter()
        Dim dt As PromoDataSet.view_PromoDataTable
        Dim dr As PromoDataSet.view_PromoRow
        Dim AdPromoID As Integer = CInt(ConfigurationManager.AppSettings("AdPromoID"))

        dt = da.GetDataRandom(AdPromoID, Culture)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
        End If

        Return dr
    End Function

End Class
