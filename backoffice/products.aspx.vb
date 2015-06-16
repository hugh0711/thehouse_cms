Imports System.IO


Partial Class backoffice_products
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



            GetAllowUser()
            

        End If
    End Sub


    Protected Sub GetAllowUser()
        Dim tagDB As New ProductDbDataContext()

        Dim Found_tagID As Integer

        Dim sqlWhere As String = ""

        Dim AllowRole As String = "admin,HK_Admin,CN_Admin,MA_Admin"

        Dim CurrentUsername As String = Membership.GetUser().UserName
        Dim Rs As String() = AllowRole.Split(",")


        Dim RoleDictionary As New Dictionary(Of String, String)
        RoleDictionary.Add("HK_Admin", "HK")
        RoleDictionary.Add("CN_Admin", "CN")
        RoleDictionary.Add("MA_Admin", "MA")


        For Each r As String In Rs
            'check current is "Admin" or not
            If Roles.IsUserInRole(CurrentUsername, "Admin") Then

            Else
                'run if user is in role
                If Roles.IsUserInRole(CurrentUsername, r) Then

                    Found_tagID = (From t In tagDB.view_Tags
                           Where t.TagName = RoleDictionary.Item(r)
                           Select t.TagID).FirstOrDefault

                    sqlWhere &= String.Format("AND (TagID={0})", Found_tagID)
                End If
            End If
        Next


        Dim sqlString As String = String.Format("SELECT ProductID, ProductCode, ProductName, CategoryName, Url, Enabled, SortOrder, ThumbnailWidth, ThumbnailHeight, Description FROM view_ProductImageTag WHERE (Lang = @Lang) AND (FunctionID = @FunctionID) {0} ORDER BY CategoryID, SortOrder", sqlWhere)
        SqlDataSourceAll.SelectCommand = sqlString
        SqlDataSourceAll.DataBind()


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
            ReorderList1.DataSourceID = "SqlDataSourceTest"
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

        Response.Redirect("~/backoffice/product.aspx" & param)
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
                Response.Redirect(String.Format("~/backoffice/product.aspx?id={0}", e.CommandArgument))
                Exit Sub
            Case "delete1"
                Dim ProductID As Integer
                ProductID = Convert.ToInt32(e.CommandArgument)
                'SqlDataSource1.DeleteParameters("ProductID").DefaultValue = ProductID
                'SqlDataSource1.Delete()
                ProductClass.Delete(ProductID)
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
            Filename = Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), Filename)
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

    Protected Function GetTags(ByVal ProductID As Integer) As String
        Dim TagAdapter As New ProductDataSetTableAdapters.view_ProductTagTableAdapter()
        Dim TagTable As ProductDataSet.view_ProductTagDataTable
        Dim TagRow As ProductDataSet.view_ProductTagRow
        Dim TagList As New List(Of String)

        TagTable = TagAdapter.GetDataByProductID(ProductID, hfdLang.Value)
        For Each TagRow In TagTable.Rows
            TagList.Add(TagRow.TagName)
        Next

        Return Join(TagList.ToArray(), ", ")
    End Function

    Protected Function getUserRight(ByVal ProductID As Integer) As Boolean
        'check user admin right and set tag
        Dim RoleDictionary = GetAllowUser_Dictionary()

        Dim return_result As Boolean = False


        Dim TagAdapter As New ProductDataSetTableAdapters.view_ProductTagTableAdapter()
        Dim TagTable As ProductDataSet.view_ProductTagDataTable
        Dim TagRow As ProductDataSet.view_ProductTagRow
        Dim TagList As New List(Of String)

        TagTable = TagAdapter.GetDataByProductID(ProductID, hfdLang.Value)
        If RoleDictionary.Count > 0 Then
            For Each TagRow In TagTable.Rows

                If RoleDictionary.ContainsValue(TagRow.TagName) Then
                    return_result = True
                End If

            Next
        End If






        Return return_result
    End Function


    Protected Function GetAllowUser_Dictionary() As Dictionary(Of String, String)



        Dim result As New Dictionary(Of String, String)


        Dim AllowRole As String = "admin,HK_Admin,CN_Admin,MA_Admin"

        Dim CurrentUsername As String = Membership.GetUser().UserName
        Dim Rs As String() = AllowRole.Split(",")


        Dim RoleDictionary As New Dictionary(Of String, String)
        RoleDictionary.Add("HK_Admin", "HK")
        RoleDictionary.Add("CN_Admin", "CN")
        RoleDictionary.Add("MA_Admin", "MA")


        For Each r As String In Rs
            'check current is "Admin" or not
            If Roles.IsUserInRole(CurrentUsername, "Admin") Then
                result = RoleDictionary
            Else
                'run if user is in role
                If Roles.IsUserInRole(CurrentUsername, r) Then
                    result.Add(r, RoleDictionary.Item(r))
                End If
            End If
        Next

        Return result

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
        ' lblCategory.Text = TreeView1.SelectedNode.Text
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

    Protected Sub btnDeleteAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Dim db As New ProductDbDataContext()
        Dim allProductID = From p In db.Products
                           Where p.FunctionID = hfdFunctionID.Value
                           Select p.ProductID

        If allProductID.Count > 0 Then
            For Each foundProductID In allProductID
                ProductClass.Delete(foundProductID)
            Next
        End If
        
        ReorderList1.DataBind()
        BindList()
    End Sub

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
                SqlDataSourceByCategory.SelectCommand = "SELECT ProductID, ProductCode, ProductName, CategoryName, Url, Description, Enabled, SortOrder, ThumbnailWidth, ThumbnailHeight FROM view_ProductImageTag WHERE (CategoryID = @CategoryID) AND (Lang = @Lang) AND (FunctionID = @FunctionID) AND ProductName LIKE '%" & txtProductName.Text & "%' ORDER BY CategoryID, SortOrder"
            Else
                SqlDataSourceAll.SelectCommand = "SELECT ProductID, ProductCode, ProductName, CategoryName, Url, Enabled, SortOrder, ThumbnailWidth, ThumbnailHeight, Description FROM view_ProductImageTag WHERE (Lang = @Lang) AND (FunctionID = @FunctionID) AND ProductName LIKE '%" & txtProductName.Text & "%' ORDER BY CategoryID, SortOrder"
            End If
            ReorderList1.AllowReorder = False
            ReorderList1.DataBind()
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        BindList()
    End Sub

    Protected Sub ReorderList1_UpdateCommand(sender As Object, e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.UpdateCommand
        System.Diagnostics.Debug.Print(String.Format("ID: {0}, Pos: {1}", "a", "b"))
    End Sub

    Protected Sub ReorderList1_DataBinding(sender As Object, e As EventArgs)

    End Sub
End Class
