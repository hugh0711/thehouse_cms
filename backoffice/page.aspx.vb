
Partial Class backoffice_page
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        CreateTab()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            'Dim _FileBrowser As CKFinder.FileBrowser = New CKFinder.FileBrowser()
            '_FileBrowser.BasePath = ResolveClientUrl("~/ckfinder") & "/" '"/ckfinder/"
            '_FileBrowser.SetupFCKeditor(FCKeditor1)
            'Dim _FileBrowser2 As CKFinder.FileBrowser = New CKFinder.FileBrowser()
            '_FileBrowser2.BasePath = ResolveClientUrl("~/ckfinder") & "/" '"/ckfinder/"
            '_FileBrowser2.SetupFCKeditor(FCKeditor2)

            'Dim ArrStyle() As String = New String() {"css/StyleSheet2.css", "css/StyleSheet.css"}
            'FCKeditor1.EditorAreaCSS = "../css/StyleSheet2.css"
            'FCKeditor2.EditorAreaCSS = "../css/StyleSheet2.css"

            'If Request("fn") IsNot Nothing OrElse Request("id").Trim <> "" Then
            '    ViewState("fn") = CInt(Request("fn"))
            'Else
            '    ViewState("fn") = 1
            'End If
            'ViewState("FunctionSettings") = New SiteFunctionClass(ViewState("fn"))
            If Request("parentid") IsNot Nothing Then
                ViewState("ParentID") = CInt(Request("parentid"))
                btnDelete.Visible = False
                'If ViewState("FunctionSettings").hasCategoryImage Then
                '    btnSave.Text = "下一步"
                'End If
            ElseIf Request("id") IsNot Nothing Then
                ViewState("PageID") = CInt(Request("id"))
                btnDelete.Visible = True
                LoadData()
            Else
                Response.Redirect("~/backoffice/pages.aspx")
            End If
            ViewState("Page") = ""
            'EditMode(1)
        End If
    End Sub

    'Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
    '    Response.Redirect(String.Format("~/backoffice/category_upload.aspx?id={0}", ViewState("CategoryID"), ViewState("fn")))
    'End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim IsNew As Boolean = ViewState("PageID") Is Nothing
            SaveData()
            'If IsNew And ViewState("FunctionSettings").HasCategoryImage Then
            '    Response.Redirect(String.Format("~/backoffice/category_upload.aspx?id={0}", ViewState("CategoryID"), ViewState("fn")))
            'Else
            '    Response.Redirect(String.Format("~/backoffice/categories.aspx?category={0}&fn={1}", ViewState("ParentID"), ViewState("fn")))
            'End If
            lblMessage.Text = "Web page is saved"
        Catch ex As Exception
            lblMessage.Text = "Web page save failed: " & ex.Message
        End Try
    End Sub

    Protected Sub btnSaveClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveClose.Click
        Try
            Dim IsNew As Boolean = ViewState("PageID") Is Nothing
            SaveData()
            Response.Redirect(String.Format("~/backoffice/pages.aspx?page={0}", ViewState("ParentID")))
        Catch ex As Exception
            lblMessage.Text = "Web page save failed: " & ex.Message
        End Try
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect(String.Format("~/backoffice/pages.aspx?page={0}", ViewState("ParentID")))
    End Sub

    Protected Sub LoadData()
        Dim PageAdapter As New PageDataSetTableAdapters.PageTableAdapter()
        Dim PageTable As PageDataSet.PageDataTable
        Dim PageRow As PageDataSet.PageRow

        PageTable = PageAdapter.GetDataByID(ViewState("PageID"))
        If PageTable.Rows.Count > 0 Then
            PageRow = PageTable.Rows(0)
            With PageRow
                'txtPage.Text = .Page
                txtUrl.Text = .url
                txtRedirect.Text = .Page
                chkVisible.Checked = .Visible
                'txtTitle.Text = .Title
                'txtKeywords.Text = .Keyword
                'txtDescription.Text = .Description
                ddlMasterPage.SelectedValue = .MasterPage
                'FCKeditor1.Value = .Content
                ViewState("Page") = .Page
                chkEnabled.Checked = .Enabled
                ViewState("ParentID") = .ParentPageID
            End With
            'btnLanguage.Visible = (ConfigurationManager.AppSettings("LanguageSupport").LastIndexOf(","c) <> -1)
            'btnEditLang.Enabled = True
            LoadTab()
        Else
            'btnEditLang.Enabled = False
        End If
    End Sub


    Protected Sub SaveData()
        Dim PageAdapter As New PageDataSetTableAdapters.PageTableAdapter()
        'Dim PageLangAdapter As New PageDataSetTableAdapters.PageLangTableAdapter()
        Dim SortOrder As Integer = PageAdapter.GetNextSortOrder(CInt(ViewState("ParentID"))).GetValueOrDefault(1)
        'Dim ImageFilename As String = IO.Path.GetFileName(imgPage.ImageUrl)

        If ViewState("PageID") Is Nothing Then
            ViewState("PageID") = PageAdapter.InsertQuery(0, txtRedirect.Text, txtUrl.Text, "", "", "", ViewState("ParentID"), ddlMasterPage.SelectedValue, "", chkEnabled.Checked, Page.User.Identity.Name, SortOrder, chkVisible.Checked)
            ' Create empty record for PageName 
            'SavePageName()
        Else
            PageAdapter.UpdateQuery(0, txtUrl.Text, "", "", "", ViewState("ParentID"), ddlMasterPage.SelectedValue, "", chkEnabled.Checked, Page.User.Identity.Name, txtRedirect.Text, chkVisible.Checked, ViewState("PageID"))
            '    If ViewState("Page") <> txtPage.Text Then
            '        SavePageName()
            '    End If
        End If
        SaveTab()
        'btnEditLang.Enabled = True
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim PageAdapter As New PageDataSetTableAdapters.PageTableAdapter()
        Dim PageLangAdapter As New PageDataSetTableAdapters.PageLangTableAdapter()

        PageAdapter.Delete(ViewState("PageID"))
        PageLangAdapter.DeleteByPageID(ViewState("PageID"))
        Response.Redirect(String.Format("~/backoffice/pages.aspx?id={0}", ViewState("ParentID")))
    End Sub

    'Protected Sub btnLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLanguage.Click
    '    Response.Redirect("~/backoffice/category_name.aspx?id=" & ViewState("CategoryID"))
    'End Sub

    'Protected Sub SavePageName()
    '    ' Create CategoryName to other languages
    '    Dim PageLangAdapter As New PageDataSetTableAdapters.PageLangTableAdapter
    '    Dim LanguageSupport() As String = ConfigurationManager.AppSettings("LanguageSupport").ToString().Split(",")
    '    Dim DefaultLanguage As String = ConfigurationManager.AppSettings("DefaultLanguage")
    '    Dim CategoryName As String
    '    Dim Translate As New GoogleTranslateClass
    '    For Each ToLang In LanguageSupport
    '        PageLangAdapter.Insert(ViewState("PageID"), ToLang, "", "", "", "", "", True, "", "", "")
    '    Next
    'End Sub




    'Protected Sub EditMode(ByVal index As Integer)
    '    Select Case index
    '        Case 1
    '            pnlUrl.Enabled = True
    '            pnlButton.Enabled = True
    '            pnlDetails.Visible = False
    '        Case 2
    '            pnlUrl.Enabled = False
    '            pnlButton.Enabled = False
    '            pnlDetails.Visible = True
    '    End Select

    '    lblMessage.Text = ""
    'End Sub


    'Protected Sub btnEditLang_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditLang.Click
    '    LoadLangData()
    '    EditMode(2)
    '    'Select Case ddlLang.SelectedValue.ToLower()
    '    '    Case "zh-cn"
    '    '        Editor1.EditorWysiwygModeCss = "~/css/StyleSheet.css,~/css/StyleSheet2_sc.css"
    '    '    Case Else
    '    '        Editor1.EditorWysiwygModeCss = "~/css/StyleSheet.css,~/css/StyleSheet2.css"
    '    'End Select
    'End Sub

    'Protected Sub btnSaveLang_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveLang.Click
    '    Try
    '        SaveLangData()
    '        EditMode(1)
    '        lblMessage.Text = "網頁內容已儲存"
    '    Catch ex As Exception
    '        lblMessage.Text = "網頁內容儲存時發生問題: " & ex.Message
    '    End Try
    'End Sub

    'Protected Sub btnCancelLang_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelLang.Click
    '    EditMode(1)
    'End Sub

    Protected Sub CreateTab()
        Dim Desc() = ConfigurationManager.AppSettings("LanguageSupportDescription").Split(",")
        Dim Lang() = ConfigurationManager.AppSettings("LanguageSupport").Split(",")

        For i As Integer = 0 To Desc.Count - 1
            Dim Tab As New AjaxControlToolkit.TabPanel
            Tab.HeaderText = Desc(i)

            Dim PageDetail As UserControl = LoadControl("~/control/PageLangControl.ascx")
            With CType(PageDetail, control_PageLangControl)
                .ID = String.Format("PageLangControl{0}", i + 1)
                .setLang(lang(i))
            End With
            Tab.Controls.Add(PageDetail)

            TabContainer1.Tabs.Add(Tab)
        Next

    End Sub

    'Protected Sub SetupTab()
    '    Dim Lang() = ConfigurationManager.AppSettings("LanguageSupport").Split(",")
    '    Dim PageID As Integer = CInt(ViewState("PageID"))

    '    For i As Integer = 0 To TabContainer1.Tabs.Count - 1
    '        Dim Tab As AjaxControlToolkit.TabPanel = TabContainer1.Tabs(i)
    '        Dim c As UserControl = Tab.FindControl(String.Format("PageLangControl{0}", i + 1))
    '        With CType(c, control_PageLangControl)
    '            '.LoadSiteFunction(ViewState("fn"))
    '            '.LoadProductLang(ProductID, Lang(i))
    '        End With
    '    Next

    'End Sub

    Protected Sub LoadTab()
        Dim Lang() = ConfigurationManager.AppSettings("LanguageSupport").Split(",")
        Dim PageID As Integer = CInt(ViewState("PageID"))

        For i As Integer = 0 To TabContainer1.Tabs.Count - 1
            Dim Tab As AjaxControlToolkit.TabPanel = TabContainer1.Tabs(i)
            Dim c As UserControl = Tab.FindControl(String.Format("PageLangControl{0}", i + 1))
            With CType(c, control_PageLangControl)
                '.LoadSiteFunction(ViewState("fn"))
                .LoadLangData(PageID)
            End With
        Next

    End Sub

    Protected Sub SaveTab()
        Dim PageID As Integer = CInt(ViewState("PageID"))

        For i As Integer = 0 To TabContainer1.Tabs.Count - 1
            Dim Tab As AjaxControlToolkit.TabPanel = TabContainer1.Tabs(i)
            Dim c As UserControl = Tab.FindControl(String.Format("PageLangControl{0}", i + 1))
            CType(c, control_PageLangControl).SaveLangData(PageID)
        Next
    End Sub


End Class
