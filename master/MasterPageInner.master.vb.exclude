
Partial Class master_MasterPageInner
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim dr As PromoDataSet.view_PromoRow
        Dim AdPromoID As Integer = CInt(ConfigurationManager.AppSettings("AdPromoID"))

        dr = AdvertisementClass.GetAd(AdPromoID, Session("MyCulture"))
        If IsNothing(dr) Then
            lnkAd.Visible = False
        Else
            lnkAd.Visible = True
            If dr.PromoUrl <> "" Then
                lnkAd.NavigateUrl = String.Format("~/ad.ashx?url={0}", Server.UrlEncode(dr.PromoUrl))
            End If
            lnkAd.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("PromoImagePath"), dr.PromoSingleImageUrl)
        End If
    End Sub
End Class

