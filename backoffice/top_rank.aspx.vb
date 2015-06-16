
Partial Class backoffice_top_rank
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
        Dim FunctionID As Integer = CInt(ConfigurationManager.AppSettings("VideoFunctionID"))
        Dim Lang As String = Session("MyCulture")
        Dim MaxViewCount As Integer = (New ProductDataSetTableAdapters.view_ProductImageCountTableAdapter()).GetMaxViewCount(FunctionID, Lang)

        Dim ds As New StatDataSetTableAdapters.ViewCountTableAdapter()

        Try
            ds.UpdateViewCount(MaxViewCount + 500, CInt(Textbox5.Text))
            ds.UpdateViewCount(MaxViewCount + 1000, CInt(Textbox4.Text))
            ds.UpdateViewCount(MaxViewCount + 1500, CInt(Textbox3.Text))
            ds.UpdateViewCount(MaxViewCount + 2000, CInt(Textbox2.Text))
            ds.UpdateViewCount(MaxViewCount + 2500, CInt(Textbox1.Text))
            lblMessage.Text = "更新成功"
        Catch ex As Exception
            lblMessage.Text = "更新錯誤: " & ex.Message
        End Try

    End Sub

End Class
