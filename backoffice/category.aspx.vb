Imports System.IO
Imports System.Drawing
Imports System.Data
Imports Utility

Partial Class backoffice_category
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

        CreateTab()
    End Sub

    Protected Sub CreateTab()
        Dim Desc() = ConfigurationManager.AppSettings("LanguageSupportDescription").Split(",")
        Dim Lang() = ConfigurationManager.AppSettings("LanguageSupport").Split(",")

        For i As Integer = 0 To Desc.Count - 1
            Dim Tab As New AjaxControlToolkit.TabPanel
            Tab.HeaderText = Desc(i)

            Dim ProductCategoryDetail As UserControl = LoadControl("~/control/ProductLangCategory.ascx")
            With CType(ProductCategoryDetail, control_ProductLangCategory)
                .ID = String.Format("ProductLangCategory{0}", i + 1)
                .SetLang(Lang(i))
            End With
            Tab.Controls.Add(ProductCategoryDetail)

            TabContainer1.Tabs.Add(Tab)
        Next

    End Sub

    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim Site As SiteFunctionClass

            If Request("fn") IsNot Nothing AndAlso Request("fn").Trim <> "" Then
                ViewState("fn") = CInt(Request("fn"))
            Else
                If Request("parentid") IsNot Nothing AndAlso Request("parentid").Trim <> "" Then
                    ViewState("fn") = (New CategoryDataSetTableAdapters.CategoryTableAdapter()).GetFunctionID(CInt(Request("parentid")))
                End If
                If Request("id") IsNot Nothing AndAlso Request("id").Trim <> "" Then
                    ViewState("fn") = (New CategoryDataSetTableAdapters.CategoryTableAdapter()).GetFunctionID(CInt(Request("id")))
                End If
            End If
            Site = New SiteFunctionClass(ViewState("fn"))
            ViewState("FunctionSettings") = Site

            If Request("parentid") IsNot Nothing Then
                ViewState("ParentID") = CInt(Request("parentid"))
                btnDetele.Visible = False
                If Site.HasCategoryImage Then
                    btnSave.Text = "下一步"
                End If
                LoadSetting()

                SetupTab()
            ElseIf Request("id") IsNot Nothing Then
                ViewState("CategoryID") = CInt(Request("id"))
                btnDetele.Visible = True
                LoadSetting()
                LoadData()

                LoadTab()
            Else
                Response.Redirect("~/backoffice/categoies.aspx")
            End If
            ViewState("Category") = ""
        End If
    End Sub



    Protected Sub LoadTab()
        Dim Lang() = ConfigurationManager.AppSettings("LanguageSupport").Split(",")
        Dim CategoryID As Integer = CInt(Request("id"))

        For i As Integer = 0 To TabContainer1.Tabs.Count - 1
            Dim Tab As AjaxControlToolkit.TabPanel = TabContainer1.Tabs(i)
            Dim c As UserControl = Tab.FindControl(String.Format("ProductLangCategory{0}", i + 1))
            With CType(c, control_ProductLangCategory)
                .LoadSiteFunction(ViewState("fn"))
                .LoadProductCategoryLang(CategoryID, Lang(i))
            End With
        Next

    End Sub

    Protected Sub SetupTab()
        Dim Lang() = ConfigurationManager.AppSettings("LanguageSupport").Split(",")
        'Dim CategoryID As Integer = CInt(Request("id"))

        For i As Integer = 0 To TabContainer1.Tabs.Count - 1
            Dim Tab As AjaxControlToolkit.TabPanel = TabContainer1.Tabs(i)
            Dim c As UserControl = Tab.FindControl(String.Format("ProductLangCategory{0}", i + 1))
            With CType(c, control_ProductLangCategory)
                .LoadSiteFunction(ViewState("fn"))
                '.LoadProductCategoryLang(CategoryID, Lang(i))
            End With
        Next

    End Sub



    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Response.Redirect(String.Format("~/backoffice/category_upload.aspx?id={0}", ViewState("CategoryID"), ViewState("fn")))
    End Sub

    Protected Sub LoadSetting()
        Dim Site As SiteFunctionClass = CType(ViewState("FunctionSettings"), SiteFunctionClass)
        Dim Level As Integer = 0
        If Not String.IsNullOrWhiteSpace(ViewState("CategoryID")) Then
            Level = CategoryClass.GetLevel(CInt(ViewState("CategoryID")))
            Level = Level - 1
        ElseIf Not String.IsNullOrWhiteSpace(ViewState("ParentID")) Then
            Dim ParentID As Integer = CInt(ViewState("ParentID"))
            If ParentID = 0 Then
                Level = 1
            Else
                Level = CategoryClass.GetLevel(ParentID)
            End If
        End If
        If Level <= Site.CategoryCaption.Count Then
            lblTitle.Text = Site.CategoryCaption(Level - 1)
        End If


        'If Site.FunctionID = CInt(ConfigurationManager.AppSettings("VideoFunctionID")) Then
        '    htmlDescription.Visible = True
        '    txtDesc.Visible = False
        'End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Page.IsValid Then
            Try
                Dim IsNew As Boolean = ViewState("CategoryID") Is Nothing
                SaveData()
                If IsNew And ViewState("FunctionSettings").HasCategoryImage Then
                    Response.Redirect(String.Format("~/backoffice/category_upload.aspx?id={0}", ViewState("CategoryID"), ViewState("fn")))
                Else
                    lblMessage.Text = "Save succesful"
                End If
            Catch ex As Exception
                lblMessage.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub btnSaveBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveBack.Click
        If Page.IsValid Then
            Try
                Dim IsNew As Boolean = ViewState("CategoryID") Is Nothing
                SaveData()

                Response.Redirect(String.Format("~/backoffice/categories.aspx?category={0}&fn={1}", ViewState("ParentID"), ViewState("fn")))
            Catch ex As Exception
                lblMessage.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect(String.Format("~/backoffice/categories.aspx?category={0}&fn={1}", ViewState("ParentID"), ViewState("fn")))
    End Sub

    Protected Sub LoadData()
        Dim CategoryAdapter As New CategoryDataSetTableAdapters.view_CategoryTableAdapter()
        Dim CategoryTable As CategoryDataSet.view_CategoryDataTable
        Dim CategoryRow As CategoryDataSet.view_CategoryRow

        CategoryTable = CategoryAdapter.GetDataByCategoryID(ViewState("CategoryID"), ConfigurationManager.AppSettings("DefaultLanguage"))
        If CategoryTable.Rows.Count > 0 Then
            CategoryRow = CategoryTable.Rows(0)
            With CategoryRow
                'txtCategory.Text = .Category
                'If txtDesc.Visible Then
                '    txtDesc.Text = .Description
                'Else
                '    htmlDescription.Value = .Description
                'End If

                'SetupTab(.Category, .Description)

                If Not .IsCategoryNull Then
                    ViewState("Category") = .CategoryName
                End If


                chkEnabled.Checked = .Enabled
                ViewState("ParentID") = .ParentID
                ViewState("fn") = .FunctionID
                Dim Site As SiteFunctionClass = ViewState("FunctionSettings")
                Dim url As String = .Url
                If url = "" Then
                    imgCategory.ImageUrl = Site.CategoryImageNoImage
                Else
                    imgCategory.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("CategoryThumbnailPath"), url) & "?" & Now().Ticks
                End If
                PlaceHolderImage.Visible = Site.HasCategoryImage

                btnSaveBack.Visible = True
            End With
            'btnLanguage.Visible = (ConfigurationManager.AppSettings("LanguageSupport").LastIndexOf(","c) <> -1)
        End If
    End Sub

    Protected Sub SaveData()
        Dim CategoryAdapter As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim CategoryNameAdapter As New CategoryDataSetTableAdapters.CategoryNameTableAdapter()
        Dim SortOrder As Integer = CategoryAdapter.GetNextSortOrder(CInt(ViewState("ParentID")), CInt(ViewState("fn")))
        Dim ImageFilename As String = IO.Path.GetFileName(imgCategory.ImageUrl)

        If ViewState("CategoryID") Is Nothing Then
            ViewState("CategoryID") = CategoryAdapter.InsertQuery(CInt(ViewState("fn")), "", ViewState("ParentID"), SortOrder, chkEnabled.Checked, "", Page.User.Identity.Name)
            ' Create CategoryName to other languages
            'SaveCategoryName()

            SaveTab(ViewState("CategoryID"))
        Else
            CategoryAdapter.UpdateQuery("", chkEnabled.Checked, Page.User.Identity.Name, ViewState("CategoryID"))

            'SaveCategoryName()
            SaveTab(ViewState("CategoryID"))

        End If

        PlaceHolderImage.Visible = True
    End Sub

    Protected Sub btnDetele_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetele.Click
        Dim CategoryAdapter As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim CategoryNameAdapter As New CategoryDataSetTableAdapters.CategoryNameTableAdapter()

        CategoryAdapter.Delete(ViewState("CategoryID"))
        CategoryNameAdapter.DeleteByCategoryID(ViewState("CategoryID"))
        Response.Redirect(String.Format("~/backoffice/categories.aspx?id={0}&fn={1}", ViewState("ParentID"), ViewState("fn")))
    End Sub


    Protected Sub SaveTab(CategoryID As Integer)
        Dim ProductID As Integer = CategoryID

        For i As Integer = 0 To TabContainer1.Tabs.Count - 1
            Dim Tab As AjaxControlToolkit.TabPanel = TabContainer1.Tabs(i)
            Dim c As UserControl = Tab.FindControl(String.Format("ProductLangCategory{0}", i + 1))
            CType(c, control_ProductLangCategory).SaveProductCategoryLang(ProductID)
        Next
    End Sub


    'Protected Sub SaveCategoryName()
    '    ' Create CategoryName to other languages
    '    Dim CategoryNameAdapter As New CategoryDataSetTableAdapters.CategoryNameTableAdapter()
    '    Dim LanguageSupport() As String = ConfigurationManager.AppSettings("LanguageSupport").ToString().Split(",")
    '    Dim DefaultLanguage As String = ConfigurationManager.AppSettings("DefaultLanguage")
    '    Dim CategoryName, Description As String
    '    Dim Translate As New GoogleTranslateClass
    '    For Each ToLang In LanguageSupport
    '        CategoryName = Translate.Translate(txtCategory.Text, DefaultLanguage, ToLang)
    '        If txtDesc.Visible Then
    '            Description = txtDesc.Text
    '        Else
    '            Description = htmlDescription.Value
    '        End If
    '        If CategoryNameAdapter.UpdateQuery(CategoryName, Description, ViewState("CategoryID"), ToLang) = 0 Then
    '            CategoryNameAdapter.Insert(ViewState("CategoryID"), ToLang, CategoryName, Description)
    '        End If
    '    Next
    'End Sub

End Class
