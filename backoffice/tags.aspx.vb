
Partial Class backoffice_tags
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        hfdLang.Value = ConfigurationManager.AppSettings("DefaultLanguage")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request("fn") IsNot Nothing AndAlso Request("fn") <> "" Then
                Dim FunctionID As Integer = CInt(Request("fn"))
                hfdFunctionID.Value = FunctionID
                LoadSiteFunction(FunctionID)
                Bind()
            Else
                Response.Redirect("~/backoffice/admin.aspx")
            End If
        End If
    End Sub

    Protected Sub Bind()

    End Sub

    Protected Sub LoadSiteFunction(ByVal FunctionID As Integer)
        Dim Site As New SiteFunctionClass(FunctionID)
        With Site
            lblFunctionName.Text = .FunctionName
            If Not .HasTag Then
                Response.Redirect("~/backoffice/admin.aspx")
            End If
        End With
        ViewState("FunctionSettings") = Site
    End Sub

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click, btnCreate1.Click
        Response.Redirect(String.Format("~/backoffice/tag.aspx?fn={0}", hfdFunctionID.Value))
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Bind()
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TagAdapter As New TagDataSetTableAdapters.TagTableAdapter()
        Dim container As Control = CType(sender, Button).Parent()
        Dim hfdTagID As HiddenField = CType(container.FindControl("hfdTagID"), HiddenField)
        Response.Redirect(String.Format("~/backoffice/tag.aspx?id={0}", hfdTagID.Value))
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TagAdapter As New TagDataSetTableAdapters.TagTableAdapter()
        Dim container As Control = CType(sender, Button).Parent()
        Dim hfdTagID As HiddenField = CType(container.FindControl("hfdTagID"), HiddenField)
        TagAdapter.DeleteQuery(CInt(hfdTagID.Value))
        Bind()
    End Sub

    Protected Function ShowTag(ByVal TagName As String, ByVal TagGroup As String) As String
        Dim ret As String = TagName

        If TagGroup.Trim <> "" Then
            ret &= String.Format(" ({0})", TagGroup)
        End If

        Return ret
    End Function

    Protected Sub ReorderList1_ItemCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.ItemCommand
        Select Case e.CommandName
            Case "edit1"
                Response.Redirect(String.Format("~/backoffice/tag.aspx?id={0}", e.CommandArgument))

        End Select
    End Sub
End Class
