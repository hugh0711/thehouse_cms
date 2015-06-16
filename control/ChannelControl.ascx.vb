
Partial Class control_ChannelControl
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        hfdVideoFunctionID.Value = ConfigurationManager.AppSettings("VideoFunctionID")
    End Sub
End Class
