Imports System.IO
Imports System.Drawing
Imports System.Data
Imports Utility

Partial Class backoffice_special
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim _FileBrowser As New CKFinder.FileBrowser()
        _FileBrowser.BasePath = ResolveClientUrl("~/ckfinder") & "/"
        _FileBrowser.SetupFCKeditor(htmContent)

        Dim Key As String = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
        Page.ClientScript.RegisterOnSubmitStatement(htmContent.GetType(), Key, "FCKeditorAPI.GetInstance('" + htmContent.ClientID + "').UpdateLinkedField();")

        lblUrl.Text = Request.Url.GetLeftPart(UriPartial.Authority)
        If Request.ApplicationPath <> "/" Then
            lblUrl.Text &= Request.ApplicationPath
        End If
        lblUrl.Text &= "/page/m/"
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Bind()
            hfdSpecialID.Value = "0"
            hfdLang.Value = ConfigurationManager.AppSettings("DefaultLanguage")

            'htmlDescription.EditorAreaCSS = "../css/StyleSheet2.css"
            'htmlDescription2.EditorAreaCSS = "../css/StyleSheet2_sc.css"

            If Request.QueryString("id") IsNot Nothing AndAlso Request.QueryString("id") <> "" Then
                hfdSpecialID.Value = Request("id")
                LoadProduct(Convert.ToInt32(Request.QueryString("id")))
            ElseIf Request.QueryString("category") IsNot Nothing AndAlso Request.QueryString("category") <> "" Then
                LoadCategory(Convert.ToInt32(Request.QueryString("category")))
                'If ViewState("FunctionSettings") IsNot Nothing Then
                '    If ViewState("FunctionSettings").hasProductImage Then
                '        btnSave.Text = "Next >"
                '    End If
                'End If
            ElseIf Request("fn") IsNot Nothing AndAlso Request("fn") <> "" Then
                LoadSiteFunction(CInt(Request("fn")))
                ViewState("fn") = CInt(Request("fn"))
                txtCategoryID.Text = "0"
                'If ViewState("FunctionSettings") IsNot Nothing Then
                '    If ViewState("FunctionSettings").hasProductImage Then
                '        btnSave.Text = "Next >"
                '    End If
                'End If
            Else
                'LoadCategory(15)
            End If
            LoadSiteFunction(CInt(ConfigurationManager.AppSettings("SpecialFunctionID")))
        End If

        If hfdSpecialID.Value = "0" Then
            btnDelete.Visible = False
        End If


    End Sub

    

    Protected Sub LoadSiteFunction(ByVal FunctionID As Integer)
        Dim Site As New SiteFunctionClass(FunctionID)
        With Site
            lblFunctionName.Text = .FunctionName
            'btnDelete_ConfirmButtonExtender.ConfirmText = String.Format("Are you sure to delete {0}?", .FunctionName)
            CategoryPlaceHolder.Visible = .HasCategory
            DatePlaceHolder.Visible = .HasDateRange

            'FilePlaceHolder.Visible = .HasFile
            'If .HasFile Then
            '    btnBrowse.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtFileUrl.ClientID))
            '    btnBrowse2.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtFileUrl2.ClientID))
            'End If
            'If .HasTag Then
            '    btnBrowseImageEN.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtImageEN.ClientID))
            '    btnBrowseImageTC.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtImageTC.ClientID))
            'End If
        End With
        ViewState("FunctionSettings") = Site
        ViewState("fn") = FunctionID
        hfdFunctionID.Value = FunctionID



    End Sub

    Protected Sub Bind()


    End Sub

    Protected Sub LoadCategory(ByVal CategoryID As Integer)
        With CategoryPathControl1
            .CategoryID = CategoryID
            .ShowPath()
        End With
        If CategoryID > 0 Then
            txtCategoryID.Text = CategoryID
            Dim CategoryAdapter As New CategoryDataSetTableAdapters.CategoryTableAdapter()
            'ViewState("fn") = CategoryAdapter.GetFunctionID(CategoryID)
            'LoadSiteFunction(ViewState("fn"))
        Else
            txtCategoryID.Text = ""
        End If
    End Sub

    Protected Sub LoadProduct(ByVal ProductID As Integer)
        Dim da As New SpecialDataSetTableAdapters.view_SpecialTableAdapter()
        Dim dt As SpecialDataSet.view_SpecialDataTable
        Dim dr As SpecialDataSet.view_SpecialRow

        dt = da.GetDataBySpecialID(ProductID)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            With dr
                If Not .IsCategoryIDNull Then
                    LoadCategory(.CategoryID)
                    lblSortOrder.Text = .SortOrder
                End If
                txtName.Text = .Name
                htmContent.Value = .Content
                txtUrl.Text = .Url
                imgUrl.Visible = True
                imgUrl.ImageUrl = QRCodeClass.GetQRCodeUrl(lblUrl.Text & txtUrl.Text & lblUrlExt.Text)
                chkEnabled.Checked = .Enabled
                If .StartDate <> Utility.NoStartDate Then txtStartSellDate.Text = .StartDate.ToString("d/M/yyyy")
                If .EndDate <> Utility.NoEndDate Then txtEndSellDate.Text = .EndDate.ToString("d/M/yyyy")

                Dim Site As SiteFunctionClass = ViewState("FunctionSettings")
                ExtendPlaceHolder.Visible = True

            End With
            Dim MultilanguageSupport As Boolean = CBool(ConfigurationManager.AppSettings("MultilanguageSupport"))

        End If
    End Sub



    Protected Sub SaveProduct()
        Dim da As New SpecialDataSetTableAdapters.SpecialTableAdapter()
        Dim CategoryAdatper As New SpecialDataSetTableAdapters.SpeicalCategoryTableAdapter()
        Dim SpecialID As Integer = CInt(hfdSpecialID.Value)
        Dim CategoryID As Integer = CInt(CategoryPathControl1.CategoryID)
        Dim SortOrder As Integer
        Dim DateFrom As Date = Utility.NoStartDate
        Dim DateTo As Date = Utility.NoEndDate
        Dim ContentBGColor As String = ""
        Dim ContentForeColor As String = ""
        Dim ContentHeaderColor As String = ""
        Dim SiderBGColor As String = ""
        Dim SiderForeColor As String = ""
        Dim SiderHeaderColor As String = ""
        Dim Username As String = Page.User.Identity.Name

        If IsDate(txtStartSellDate.Text) Then
            DateFrom = CDate(txtStartSellDate.Text)
        End If
        If IsDate(txtEndSellDate.Text) Then
            DateTo = CDate(txtEndSellDate.Text)
        End If



        ' Save Product
        If SpecialID = 0 Then
            SortOrder = CategoryAdatper.GetNextSortOrder(CategoryID, SpecialID)
            SpecialID = da.InsertQuery(txtName.Text, htmContent.Value, ContentBGColor, ContentForeColor, ContentHeaderColor, SiderBGColor, SiderForeColor, SiderHeaderColor, Now(), Username, chkEnabled.Checked, DateFrom, DateTo, txtUrl.Text)
            hfdSpecialID.Value = SpecialID
        Else
            da.UpdateQuery(txtName.Text, htmContent.Value, ContentBGColor, ContentForeColor, ContentHeaderColor, SiderBGColor, SiderForeColor, SiderHeaderColor, Now(), Username, chkEnabled.Checked, DateFrom, DateTo, txtUrl.Text, SpecialID)
        End If




        ' Save Product Category
        If CategoryPlaceHolder.Visible Then
            CategoryAdatper.DeleteBySpecialID(SpecialID)
            CategoryAdatper.Insert(CategoryID, SpecialID, SortOrder)
        End If



    End Sub




#Region "Page Event"


    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Dim param As String = ""
        If ViewState("FunctionSettings").hasCategory Then
            If CategoryPathControl1.CategoryID <> 0 Then
                param = "?category=" & CategoryPathControl1.CategoryID
            Else
                param = "?fn=" & ViewState("fn")
            End If
        Else
            param = "?fn=" & ViewState("fn")
        End If

        Response.Redirect("~/backoffice/specials.aspx" & param)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Page.IsValid Then
            Try
                Dim IsNew As Boolean = (hfdSpecialID.Value = "0")
                SaveProduct()
                'If IsNew And ViewState("FunctionSettings").hasProductImage Then
                '    Response.Redirect(String.Format("~/backoffice/special_upload.aspx?id={0}", hfdSpecialID.Value))
                'End If
                If IsNew Then
                    ExtendPlaceHolder.Visible = True
                    imgUrl.Visible = True
                    imgUrl.ImageUrl = QRCodeClass.GetQRCodeUrl(lblUrl.Text & txtUrl.Text & lblUrlExt.Text)
                End If

                lblMessage.Text = String.Format("儲存成功")
                lblMessage.ForeColor = Color.Black
            Catch ex As Exception
                lblMessage.Text = ex.Message
                lblMessage.ForeColor = Color.Red
            End Try
        End If
    End Sub


#End Region

#Region "Tree View"

    Protected Sub TreeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.SelectedNodeChanged
        If TreeView1.SelectedNode Is Nothing Then
            lblParentID.Text = "0"
        Else
            Dim CategoryID As Integer = Convert.ToInt32(TreeView1.SelectedNode.Value)
            LoadCategory(CategoryID)
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

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ProductClass.Delete(CInt(hfdSpecialID.Value))
        Response.Redirect(String.Format("~/backoffice/specials.aspx?category={0}&fn={1}", CategoryPathControl1.CategoryID, ViewState("fn")))
    End Sub


  

    Protected Sub btnSaveBack_Click(sender As Object, e As System.EventArgs) Handles btnSaveBack.Click
        If Page.IsValid Then
            Try
                Dim IsNew As Boolean = (hfdSpecialID.Value = "0")
                SaveProduct()
                'If IsNew And ViewState("FunctionSettings").hasProductImage Then
                '    Response.Redirect(String.Format("~/backoffice/special_upload.aspx?id={0}", hfdSpecialID.Value))
                'End If
                btnClose_Click(Nothing, Nothing)
            Catch ex As Exception
                lblMessage.Text = ex.Message
                lblMessage.ForeColor = Color.Red
            End Try
        End If
    End Sub

    Protected Sub rlImage_ItemCommand(sender As Object, e As AjaxControlToolkit.ReorderListCommandEventArgs) Handles rlImage.ItemCommand
        Select Case e.CommandName
            Case "insert1"
                Response.Redirect("~/backoffice/special_upload.aspx?id=" & hfdSpecialID.Value)
            Case "edit1"
                Response.Redirect("~/backoffice/special_upload.aspx?imgid=" & e.CommandArgument)
        End Select
    End Sub

    Protected Sub valUrl_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valUrl.ServerValidate
        Dim Count As Integer = (New SpecialDataSetTableAdapters.SpecialTableAdapter()).UrlCount(txtUrl.Text).GetValueOrDefault(0)
        args.IsValid = (Count = 0)
    End Sub
End Class

