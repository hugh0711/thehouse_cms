

Partial Class control_PageLangControl
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim _FileBrowser As New CKFinder.FileBrowser()
        _FileBrowser.BasePath = ResolveClientUrl("~/ckfinder") & "/"
        _FileBrowser.SetupFCKeditor(FCKeditor1)

        Page.ClientScript.RegisterOnSubmitStatement(FCKeditor1.GetType(), "editor", "FCKeditorAPI.GetInstance('" + FCKeditor1.ClientID + "').UpdateLinkedField();")

        btnBannerUrl.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtBannerUrl.ClientID))

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub SetLang(ByVal Lang As String)
        hfdLang.Value = Lang
    End Sub

    Public Sub LoadLangData(ByVal PageID As Integer)
        Dim PageAdapter As New PageDataSetTableAdapters.PageLangTableAdapter()
        Dim PageTable As PageDataSet.PageLangDataTable
        Dim PageRow As PageDataSet.PageLangRow

        Dim Lang As String = hfdLang.Value

        PageTable = PageAdapter.GetDataByPageIDLang(PageID, Lang)
        If PageTable.Rows.Count > 0 Then
            PageRow = PageTable.Rows(0)
            With PageRow
                txtPage.Text = .Page
                txtTitle.Text = .Title
                txtKeywords.Text = .Keyword
                txtDescription.Text = .Description
                'Editor1.Text = .Content
                FCKeditor1.Value = .Content
                'FCKeditor2.Value = .Content2
                txtBannerUrl.Text = .BannerImageUrl
                txtTitleUrl.Text = .TitleImageUrl
                chkLangEnabled.Checked = .Enabled
            End With
            'btnLanguage.Visible = (ConfigurationManager.AppSettings("LanguageSupport").LastIndexOf(","c) <> -1)
        Else
            txtPage.Text = ""
            txtTitle.Text = ""
            txtKeywords.Text = ""
            txtDescription.Text = ""
            'Editor1.Text = ""
            FCKeditor1.Value = ""
            'FCKeditor2.Value = ""
            txtBannerUrl.Text = ""
            txtTitleUrl.Text = ""
        End If
    End Sub

    Public Sub SaveLangData(ByVal PageID As Integer)
        Dim PageLangAdapter As New PageDataSetTableAdapters.PageLangTableAdapter()
        Dim Lang As String = hfdLang.Value
        Dim RecAffected As Integer
        txtTitle.Text = txtPage.Text

        RecAffected = PageLangAdapter.UpdateQuery(txtTitle.Text, txtKeywords.Text, txtDescription.Text, FCKeditor1.Value, "", txtPage.Text, chkLangEnabled.Checked, txtTitleUrl.Text, txtBannerUrl.Text, PageID, Lang)
        If RecAffected = 0 Then
            PageLangAdapter.Insert(PageID, Lang, txtTitle.Text, txtKeywords.Text, txtDescription.Text, FCKeditor1.Value, txtPage.Text, chkLangEnabled.Checked, "", txtTitleUrl.Text, txtBannerUrl.Text)
        End If
    End Sub


End Class
