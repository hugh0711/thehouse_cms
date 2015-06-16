
Partial Class control_SearchControl
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request("q") IsNot Nothing Then
                txtSearch.Text = Request("q")
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Response.Redirect("~/search.aspx?q=" & Server.UrlEncode(txtSearch.Text))
    End Sub

End Class
