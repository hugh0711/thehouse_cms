Imports System.IO

Partial Class backoffice_promos
    Inherits System.Web.UI.Page

    Protected HasImage As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrWhiteSpace(Request("type")) Then
                hfdTypeID.Value = Request("type")
            End If
            If Not String.IsNullOrWhiteSpace(Request("pm")) Then
                Dim PromoSettingID As Integer = CInt(Request("pm"))
                hfdTypeID.Value = (New PromoDataSetTableAdapters.PromoSettingTableAdapter()).GetTypeID(PromoSettingID)
            End If
            ddlPromo.DataBind()
            LoadSettingData()
            LoadData()
        End If
    End Sub

    Protected Sub LoadSettingData()
        Dim PromoSettingID As Integer = CInt(hfdTypeID.Value)

        Dim dt As PromoDataSet.PromoSettingDataTable = (New PromoDataSetTableAdapters.PromoSettingTableAdapter()).GetDataByID(PromoSettingID)
        If dt.Rows.Count > 0 Then
            Dim dr As PromoDataSet.PromoSettingRow = dt.Rows(0)
            With dr
                HasImage = .hasImage
            End With
        End If

    End Sub

    Protected Sub LoadData()
        Dim PromoSetting As New PromoClass(CInt(ddlPromo.SelectedValue))
        With PromoSetting
            If Not .AllowUser(Membership.GetUser().UserName) Then
                Response.Redirect("~/backoffice/admin.aspx")
            End If
            relst.AllowReorder = .AllowSort
            lblPromo.Text = .Name
            btnCreateBanner.Text = String.Format("新增{0}", .Name)
        End With
        relst.DataBind()

    End Sub
    'Protected Sub gvBanner_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvBanner.RowCommand
    '	If e.CommandName = "btnDetail" Then
    '		Response.Redirect(String.Format("~/backoffice/Banner.aspx?BannerID={0}&BannerType={1}&FormMode={2}", e.CommandArgument, BannerClass.BannerType_BANNER, FORMMODE_EDIT))
    '	End If
    'End Sub

    Protected Sub btnCreateBanner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateBanner.Click
        'Response.Redirect(String.Format("~/backoffice/promo.aspx?pm={0}", ddlPromo.SelectedValue))
        Response.Redirect(String.Format("~/backoffice/BannerUpload.aspx"))
    End Sub

    Protected Sub relst_ItemCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles relst.ItemCommand
        If e.CommandName = "btnEdit" Then
            Response.Redirect(String.Format("~/backoffice/promo.aspx?id={0}", e.CommandArgument))
        ElseIf e.CommandName = "Delete" Then
            Dim db As New CMSDataContext
            Dim Filename = (From p In db.Promos Where p.PromoID = e.CommandArgument.ToString Select p.PromoImageUrl).FirstOrDefault
            Dim bannerPath As String = "~/product_image/banner/"
            Dim FilePath As String = Path.Combine(bannerPath, Filename)
            'Delete Original image file
            Try
                File.Delete(MapPath(FilePath))
            Catch
            End Try

            'ElseIf e.CommandName = "btnDelete" Then
            '    Dim adapter As New BannerDataSetTableAdapters.BannerTableAdapter
            '    adapter.Delete(e.CommandArgument)
            '    relst.DataBind()
        End If
    End Sub
End Class
