
Partial Class backoffice_categories
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request("fn") IsNot Nothing AndAlso Request("fn").Trim() <> "" Then
                ViewState("fn") = Request("fn")
                Dim ParentID As Integer = 0
                If Request.QueryString("category") IsNot Nothing AndAlso Request.QueryString("category") <> "" Then
                    ParentID = CInt("0" & Request.QueryString("category"))
                    If ParentID = 0 Then
                        ViewState("fn") = Request("fn")
                    Else
                        ViewState("fn") = (New CategoryDataSetTableAdapters.CategoryTableAdapter()).GetFunctionID(ParentID).GetValueOrDefault(1)
                    End If
                End If
                lblParentID.Text = ParentID
            Else
                lblParentID.Text = "0"
                ViewState("fn") = 1
            End If
            LoadSiteFunction(ViewState("fn"))

            lblLang.Text = ConfigurationManager.AppSettings("DefaultLanguage")
            Bind()
        End If
    End Sub

    Protected Sub LoadSiteFunction(ByVal FunctionID As Integer)
        Dim Site As New SiteFunctionClass(FunctionID)
        With Site
            lblFunctionName.Text = .FunctionName
            If Not .HasCategory Then
                Response.Redirect("~/backoffice/admin.aspx")
            End If
            If FunctionID = CInt(ConfigurationManager.AppSettings("VideoFunctionID")) Then
                phdDefaultTitle.Visible = False
                phdTVTitle.Visible = True
            Else
                phdDefaultTitle.Visible = True
                phdTVTitle.Visible = False
            End If
        End With
        ViewState("FunctionSettings") = Site
    End Sub

    'Protected Sub btnRoot_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRoot.Click
    '    lblParentID.Text = 0
    '    lblLevel.Text = 1
    '    Bind()
    'End Sub

    'Protected Sub btnLevel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLevel1.Click
    '    lblParentID.Text = Convert.ToInt32(btnLevel1.CommandArgument)
    '    lblLevel.Text = 2
    '    Bind()
    'End Sub

    'Protected Sub btnLevel2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLevel2.Click
    '    lblParentID.Text = Convert.ToInt32(btnLevel2.CommandArgument)
    '    lblLevel.Text = 3
    '    Bind()
    'End Sub

    Protected Sub Bind()
        With CategoryPathControl1
            .CategoryID = Convert.ToInt32(lblParentID.Text)
            .ShowPath("categories.aspx?category={0}&fn={1}", "主目錄")
            Dim Site As SiteFunctionClass = CType(ViewState("FunctionSettings"), SiteFunctionClass)
            If .Level <= Site.CategoryCaption.Count Then
                lblTVChannel.Text = Site.CategoryCaption(.Level - 1)
            End If
            lblLevel.Text = .Level
            'Select Case .Level
            '    Case 1
            '        lblTVChannel.Text = "Channel"
            '    Case 2
            '        lblTVChannel.Text = "Program"
            'End Select
        End With

        btnUp.Enabled = (lblParentID.Text <> "0")
        'SqlDataSource1.DataBind()
        ReorderList1.DataBind()
    End Sub


#Region "ReorderList"

    Protected Sub ReorderList1_DeleteCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.DeleteCommand
        Dim CategoryAdpator As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim CategoryNameAdapter As New CategoryDataSetTableAdapters.CategoryNameTableAdapter()
        Dim CategoryID As Integer = CInt(e.CommandArgument)
        CategoryAdpator.Delete(CategoryID)
        CategoryNameAdapter.DeleteByCategoryID(CategoryID)
        Bind()
    End Sub

    Protected Sub ReorderList1_InsertCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.InsertCommand
        'Dim container As Control = CType(e.Source, Button).Parent
        'Dim CategoryAdapter As New CategoryTableAdapters.CategoryTableAdapter()
        'Dim txtCategory As TextBox = CType(container.FindControl("txtCategory"), TextBox)
        'Dim chkDisabled As CheckBox = CType(container.FindControl("chkDisabled"), CheckBox)
        'Dim SortOrder As Integer = ReorderList1.Items.Count - 1
        'CategoryAdapter.Insert(txtCategory.Text, Convert.ToInt32(lblParentID.Text), chkDisabled.Checked, SortOrder)

    End Sub
    'Protected Sub ReorderList1_CancelCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.CancelCommand
    '    ReorderList1.EditItemIndex = -1
    '    Bind()
    'End Sub

    'Protected Sub ReorderList1_EditCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.EditCommand
    '    ReorderList1.EditItemIndex = e.Item.ItemIndex
    '    Bind()
    '    'SqlDataSourceArticle.FilterExpression = "category_id = " & ddlCategory.SelectedValue
    '    'GridView1.DataBind()
    'End Sub

    Protected Sub ReorderList1_ItemCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.ItemCommand

        Select Case e.CommandName.ToLower()
            'Case "showchildren"
            '    'Dim container As Control = CType(e.Source, Button).Parent
            '    'Dim lblCategory As Label = CType(container.FindControl("lblCategory"), Label)
            '    Dim ID As String = e.CommandArgument
            '    Response.Redirect(String.Format("~/backoffice/category.aspx?category={0}", ID))
            '    'Select Case Convert.ToInt32(lblLevel.Text)
            '    '    Case 1
            '    '        btnLevel1.Text = lblCategory.Text
            '    '        btnLevel1.CommandArgument = ID
            '    '    Case 2
            '    '        btnLevel2.Text = lblCategory.Text
            '    '        btnLevel2.CommandArgument = ID
            '    'End Select
            '    'lblParentID.Text = ID
            '    'lblLevel.Text = Convert.ToInt32(lblLevel.Text) + 1
            Case "edit"
                ReorderList1.EditItemIndex = e.Item.ItemIndex
            Case "edit1"
                Response.Redirect(String.Format("~/backoffice/category.aspx?id={0}&fn={1}", e.CommandArgument, ViewState("fn")))
            Case "update"
                'Dim container As Control = CType(e.Source, Button).Parent
                'Dim CategoryAdapter As New CategoryDataSetTableAdapters.CategoryTableAdapter()
                'Dim txtCategory As TextBox = CType(container.FindControl("txtCategory"), TextBox)
                'Dim chkEnabled As CheckBox = CType(container.FindControl("chkEnabled"), CheckBox)
                'CategoryAdapter.UpdateCategory(txtCategory.Text, chkEnabled.Checked, Convert.ToInt32(e.CommandArgument))
                'ReorderList1.EditItemIndex = -1
            Case "cancel"
                ReorderList1.EditItemIndex = -1
            Case "insert"
                'Dim container As Control = CType(e.Source, Button).Parent
                ''Dim lblSortOrder As TextBox = CType(container.FindControl("lblSortOrder"), TextBox)
                ''lblSortOrder.Text = ReorderList1.Items.Count - 1
                'Dim CategoryAdapter As New CategoryDataSetTableAdapters.CategoryTableAdapter()
                'Dim txtCategory As TextBox = CType(container.FindControl("txtCategory"), TextBox)
                'Dim chkEnabled As CheckBox = CType(container.FindControl("chkEnabled"), CheckBox)
                'Dim SortOrder As Integer = ReorderList1.Items.Count - 1
                'CategoryAdapter.Insert(txtCategory.Text, Convert.ToInt32(lblParentID.Text), chkEnabled.Checked, SortOrder, "")

            Case "delete"

        End Select
        Bind()
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
    '    Bind()
    'End Sub

    'Protected Sub btnAddArticle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddArticle.Click
    '    If ddlCategory.SelectedValue <> "" Then
    '        Dim groupAdaptor As New page_publishTableAdapters.category_group_previewTableAdapter()
    '        groupAdaptor.InsertCategoryGroup(Convert.ToInt32(ddlCategory.SelectedValue), 0)

    '        ReorderList1.DataBind()
    '    End If
    'End Sub

    'Protected Sub ReorderList1_UpdateCommand(ByVal sender As Object, ByVal e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles ReorderList1.UpdateCommand
    '    Dim Key As String = ReorderList1.DataKeys(e.Item.ItemIndex).ToString()
    '    Dim groupAdaptor As New page_publishTableAdapters.category_group_previewTableAdapter
    '    groupAdaptor.UpdateCategoryGroup(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(lblID.Text), Convert.ToInt32(lblSortOrder.Text), Convert.ToInt32(Key))

    '    ReorderList1.EditItemIndex = -1
    '    ReorderList1.DataBind()
    'End Sub
#End Region

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click, btnCreate1.click
        Response.Redirect(String.Format("~/backoffice/category.aspx?parentid={0}&fn={1}", lblParentID.Text, ViewState("fn")))
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Bind()
    End Sub

    Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim ParentID As Integer = (New CategoryDataSetTableAdapters.CategoryTableAdapter()).GetParentID(CInt(lblParentID.Text)).GetValueOrDefault(0)
        Response.Redirect(String.Format("~/backoffice/categories.aspx?category={0}&fn={1}", ParentID, ViewState("fn")))
    End Sub

    Protected Function GetThumbnail(ByVal Url As String) As String
        If Url.Trim() = "" Then
            Return CType(ViewState("FunctionSettings"), SiteFunctionClass).CategoryImageNoImage
        Else
            Return IO.Path.Combine(ConfigurationManager.AppSettings("CategoryImagePath"), Url) & "?ticks=" & Now.Ticks
        End If
    End Function

    Protected Function IsShowEpisode() As Boolean
        If ViewState("fn") = ConfigurationManager.AppSettings("VideoFunctionID") AndAlso CInt(lblLevel.Text) = ViewState("FunctionSettings").CategoryMaxLevel Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function GetSubCategory() As String
        Dim Category As String = "子類別"
        Dim Site As SiteFunctionClass = CType(ViewState("FunctionSettings"), SiteFunctionClass)
        Dim Level As Integer = CInt(lblLevel.Text)
        If Level < Site.CategoryCaption.Count Then
            Category = Site.CategoryCaption(Level)
        End If
        Return Category
    End Function

    Protected Function GetProduct() As String
        Return CType(ViewState("FunctionSettings"), SiteFunctionClass).FunctionName
    End Function

End Class
