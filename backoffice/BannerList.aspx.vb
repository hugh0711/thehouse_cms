Imports Utility

Partial Class backoffice_BannerList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ddlPosition.DataBind()

            If Request("pos") IsNot Nothing AndAlso Request("pos") <> "" Then
                ddlPosition.SelectedValue = Request("pos")
            End If

            ddlPosition_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

	'Protected Sub gvBanner_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvBanner.RowCommand
	'	If e.CommandName = "btnDetail" Then
	'		Response.Redirect(String.Format("~/backoffice/Banner.aspx?BannerID={0}&BannerType={1}&FormMode={2}", e.CommandArgument, BannerClass.BannerType_BANNER, FORMMODE_EDIT))
	'	End If
	'End Sub

	Protected Sub btnCreateBanner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateBanner.Click
        Response.Redirect(String.Format("~/backoffice/Banner.aspx?pos={0}&FormMode={1}", ddlPosition.SelectedValue, FORMMODE_INSERT))
	End Sub

    Protected Sub relst_ItemCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles relst.ItemCommand
        If e.CommandName = "btnEdit" Then
            Response.Redirect(String.Format("~/backoffice/Banner.aspx?BannerID={0}&BannerType={1}&FormMode={2}", e.CommandArgument, BannerClass.BannerType_BANNER, FORMMODE_EDIT))
        ElseIf e.CommandName = "btnDelete" Then
            Dim adapter As New BannerDataSetTableAdapters.BannerTableAdapter
            adapter.Delete(e.CommandArgument)
            relst.DataBind()
        End If
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        ddlPosition_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Protected Sub ddlPosition_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPosition.SelectedIndexChanged
        relst.DataBind()
    End Sub

End Class
