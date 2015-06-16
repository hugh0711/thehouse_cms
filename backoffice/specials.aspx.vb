Imports System.IO

Partial Class backoffice_specials
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request("fn") IsNot Nothing AndAlso Request("fn").Trim <> "" Then
                ViewState("fn") = CInt(Request("fn"))
                ViewState("ByCategory") = False
                lblCategoryID.Text = "0"
            End If

            If Request.QueryString("category") IsNot Nothing AndAlso Request.QueryString("category").Trim() <> "" Then
                lblCategoryID.Text = Request.QueryString("category")
                ViewState("fn") = (New CategoryDataSetTableAdapters.CategoryTableAdapter()).GetFunctionID(CInt(lblCategoryID.Text)).GetValueOrDefault(ViewState("fn"))
                ViewState("ByCategory") = True
            Else
                ViewState("ByCategory") = False
                'ViewState("ByCategory") = True
                lblCategoryID.Text = "0"
            End If
            LoadCategory(CInt(lblCategoryID.Text))

            LoadSiteFunction(ViewState("fn"))
            hfdLang.Value = ConfigurationManager.AppSettings("DefaultLanguage")
            BindList()
        End If
    End Sub

    Protected Sub LoadSiteFunction(ByVal FunctionID As Integer)
        Dim Site As New SiteFunctionClass(FunctionID)
        With Site
            lblFunctionName.Text = .FunctionName
            hfdFunctionID.Value = FunctionID
            If Not .HasCategory Then
                CategoryPlaceHolder.Visible = False
            End If
            btnNew.Text = "新增" & .FunctionName
            btnNew2.Text = btnNew.Text
            hfdFunctionName.Value = .FunctionName
        End With
        ViewState("FunctionSettings") = Site
    End Sub

    Protected Sub BindList()
        If ViewState("ByCategory") Then
            ReorderList1.DataSourceID = "SqlDataSourceByCategory"
            SqlDataSourceByCategory.DataBind()
            lblAllCategory.Visible = False
            ReorderList1.AllowReorder = True
        Else
            ReorderList1.DataSourceID = "SqlDataSourceAll"
            SqlDataSourceAll.DataBind()
            lblAllCategory.Visible = True
            If CategoryPlaceHolder.Visible Then
                ReorderList1.AllowReorder = False
            Else
                ReorderList1.AllowReorder = True
            End If
        End If
        ReorderList1.DataBind()
    End Sub

    Protected Sub LoadCategory(ByVal CategoryID As Integer)
        With CategoryPathControl1
            .CategoryID = CategoryID
            .ShowPath()
        End With

        Dim CategoryAdatper As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        'ViewState("fn") = CategoryAdatper.GetFunctionID(CategoryID)

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click, btnNew2.Click
        Dim param As String = ""
        Dim Site As SiteFunctionClass = CType(ViewState("FunctionSettings"), SiteFunctionClass)

        If Site.HasCategory Then
            If lblCategoryID.Text <> "" AndAlso lblCategoryID.Text <> "0" Then
                param = "?category=" & lblCategoryID.Text
            Else
                param = "?fn=" & hfdFunctionID.Value
            End If
        Else
            param = "?fn=" & hfdFunctionID.Value
        End If

        Response.Redirect("~/backoffice/special.aspx" & param)
    End Sub

    'Protected Sub btnCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCategory.Click
    '    Response.Redirect("~/backoffice/categories.aspx?category=" & lblCategoryID.Text)
    'End Sub


    Protected Function GetDate(ByVal value As Date) As String
        Dim ret As String

        If value = #1/1/1900# Or value = #1/1/9900# Then
            ret = "--"
        Else
            ret = value.ToString("d/M/yyyy")
        End If

        Return ret
    End Function

#Region "ReorderList"

    Protected Sub ReorderList1_ItemCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "edit1"
                Response.Redirect(String.Format("~/backoffice/special.aspx?id={0}", e.CommandArgument))
                Exit Sub
            Case "delete1"
                Dim SpecialID As Integer
                SpecialID = Convert.ToInt32(e.CommandArgument)
                'SqlDataSource1.DeleteParameters("ProductID").DefaultValue = ProductID
                'SqlDataSource1.Delete()
                Dim RowAffected As Integer = (New SpecialDataSetTableAdapters.SpecialTableAdapter()).Delete(SpecialID)
                RowAffected = (New SpecialDataSetTableAdapters.SpeicalCategoryTableAdapter()).DeleteBySpecialID(SpecialID)
                ReorderList1.DataBind()
        End Select
        BindList()
    End Sub

    'Protected Sub ReorderList1_ItemReorder(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListItemReorderEventArgs) Handles ReorderList1.ItemReorder
    '    Dim lowerBound As Integer = (IIf(e.OldIndex > e.NewIndex, e.NewIndex, e.OldIndex))
    '    Dim upperBound As Integer = (IIf(e.OldIndex > e.NewIndex, e.OldIndex, e.NewIndex))
    '    Dim incrementer As Integer = (IIf(e.OldIndex > e.NewIndex, 0, 1))

    '    Dim pageContentLogic As New CategoryDataSetTableAdapters.CategoryTableAdapter()

    '    Dim ID As Integer = Convert.ToInt32(ReorderList1.DataKeys(e.OldIndex))
    '    Dim OrderBy As Integer = Convert.ToInt32(e.NewIndex)
    '    Dim updated As Boolean = pageContentLogic.UpdateSortOrder(OrderBy, ID)
    '    For i As Integer = lowerBound + incrementer To upperBound + (incrementer - 1)
    '        ID = Convert.ToInt32(ReorderList1.DataKeys(i))
    '        OrderBy = i + (IIf(incrementer = 0, 1, -1))
    '        updated = pageContentLogic.UpdateSortOrder(OrderBy, ID)
    '    Next
    '    BindList()
    'End Sub
#End Region

    Protected Function GetThumbnail(ByVal ProductID As Integer) As String
        Dim Site As SiteFunctionClass = ViewState("FunctionSettings")
        Dim ImageAdapter As New ProductDataSetTableAdapters.ProductImageTableAdapter()
        Dim Filename As String = ImageAdapter.GetImageUrl(ProductID, 1)
        If Filename Is Nothing Then
            Filename = Site.ProductImageNoImage
        Else
            Filename = Path.Combine(ConfigurationManager.AppSettings("SpecialThumbnailPath"), Filename)
        End If
        Dim ImageUrl As String = Filename
        Dim ImagePath As String = Server.MapPath(ImageUrl)

        If File.Exists(ImagePath) Then
            Return ImageUrl & "?" & Now.Ticks
        Else
            Return Site.ProductImageNoImage & "?" & Now.Ticks
        End If
    End Function

    Protected Function GetThumbnailWidth(ByVal Height As Integer, ByVal ThumbnailWidth As Integer, ByVal ThumbnailHeight As Integer) As Integer
        If ThumbnailHeight = 0 Then
            Return Height
        Else
            Return CInt(Height * ThumbnailWidth / ThumbnailHeight)
        End If
    End Function




#Region "Tree View"

    Protected Sub TreeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.SelectedNodeChanged
        If TreeView1.SelectedNode Is Nothing Then
            lblParentID.Text = "0"
        Else
            Dim CategoryID As Integer = Convert.ToInt32(TreeView1.SelectedNode.Value)
            LoadCategory(CategoryID)
            ViewState("ByCategory") = True
            lblCategoryID.Text = CategoryID
            BindList()
            'ReorderList1.DataBind()
            btnSelectCategory_ModalPopupExtender.Hide()
        End If
        'lblCategory.Text = TreeView1.SelectedNode.Text
        'ReorderList1.DataBind()
    End Sub

    Protected Sub TreeView1_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodePopulate
        'lblCategory.Text = e.Node.Text
        LoadCategoryTree(e.Node, Convert.ToInt32(e.Node.Value))
    End Sub

    Protected Sub LoadCategoryTree(ByVal CurrentNode As TreeNode, ByVal ID As Integer)
        Dim CategoryAdaptor As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim CategoryTable As CategoryDataSet.CategoryDataTable
        Dim CategoryRow As CategoryDataSet.CategoryRow
        Dim node As TreeNode

        CurrentNode.ChildNodes.Clear()
        CategoryTable = CategoryAdaptor.GetDataByParentID(CInt(ID), CInt(ViewState("fn")))
        For Each CategoryRow In CategoryTable.Rows
            With CategoryRow
                node = New TreeNode(.Category, .CategoryID)
                node.PopulateOnDemand = True
                node.SelectAction = TreeNodeSelectAction.SelectExpand
            End With
            CurrentNode.ChildNodes.Add(node)
        Next
        lblParentID.Text = ID
    End Sub

#End Region

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        ReorderList1.DataBind()
    End Sub

    Protected Sub btnAllCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllCategory.Click
        ViewState("ByCategory") = False
        lblCategoryID.Text = "0"
        LoadCategory(0)
        BindList()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtProductName.Text.Length > 0 Then
            If ReorderList1.DataSourceID = "SqlDataSourceByCategory" Then
                SqlDataSourceByCategory.SelectCommand = "SELECT SpecialID, Name, Enabled, Category FROM view_Special WHERE (CategoryID = @CategoryID) AND Name LIKE '%" & txtProductName.Text & "%' ORDER BY CategoryID, SortOrder"
            Else
                SqlDataSourceAll.SelectCommand = "SELECT SpecialID, Name, Enabled, Category FROM view_Special WHERE Name LIKE '%" & txtProductName.Text & "%' ORDER BY CategoryID, SortOrder"
            End If
            ReorderList1.AllowReorder = False
            ReorderList1.DataBind()
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        BindList()
    End Sub

End Class
