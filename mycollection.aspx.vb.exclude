
Partial Class mycollection
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim TypeID As Integer = CInt(ConfigurationManager.AppSettings("CollectionTypeID"))
        ddlGroup.DataSource = (New UserProductDataSetTableAdapters.UserProductGroupTableAdapter()).GetDataByTypeID(TypeID, 0)
        ddlGroup.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrWhiteSpace(Request("group")) Then
                ddlGroup.SelectedValue = Request("group")
            End If

            hfdUsername.Value = Page.User.Identity.Name
            hfdTypeID.Value = CInt(ConfigurationManager.AppSettings("CollectionTypeID"))
        End If
    End Sub


End Class
