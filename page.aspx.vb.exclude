Imports System.Threading
Imports System.Globalization

Partial Class page
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Literal1.Text = ViewState("Content")
			'Literal2.Text = ViewState("Content2")
            'ltrPrintContent.Text = ViewState("Content")
			'imgBanner.ImageUrl = ViewState("BannerUrl")
            'imgTitle.ImageUrl = ViewState("TitleUrl")
        End If
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Request("id") IsNot Nothing Then
            ViewState("PageID") = CInt(Request("id"))
            LoadData()
		ElseIf Request("url") IsNot Nothing Then
			ViewState("PageID") = (New PageDataSetTableAdapters.PageTableAdapter()).GetPageIDByUrl(Request("url"))
			LoadData()
        End If


	End Sub

    Protected Overrides Sub InitializeCulture()
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(Session("MyCulture").ToString())
        MyBase.InitializeCulture()
    End Sub

    Protected Sub LoadData()
        Dim PageID As Integer = ViewState("PageID")
        If PageID = 0 Then
            Response.StatusCode = 404
            Response.End()
        End If

        Dim PageAdapter As New PageDataSetTableAdapters.view_PageTableAdapter()
        Dim PageTable As PageDataSet.view_PageDataTable
        Dim PageRow As PageDataSet.view_PageRow
        Dim Lang As String = Session("MyCulture")
        Dim RedirectUrl As String = (New PageDataSetTableAdapters.PageTableAdapter()).GetRedirectUrl(PageID).ToString().Trim()

        If RedirectUrl <> "" Then
            Response.Redirect(String.Format("~/{0}.htm", RedirectUrl))
        End If

        PageTable = PageAdapter.GetDataByIDLang(PageID, Lang)
        If PageTable.Rows.Count > 0 Then
            PageRow = PageTable.Rows(0)
            With PageRow
                MasterPageFile = String.Format("~/master/{0}.master", .MasterPage)
                Page.Title = String.Format("{0} - {1}", .Title, ConfigurationManager.AppSettings("CompanyName"))
                If Not .IsKeywordNull AndAlso .Keyword <> "" Then
                    Dim MetaKeyword As New HtmlMeta()
                    MetaKeyword.Name = "keywords"
                    MetaKeyword.Content = .Keyword
                    Page.Header.Controls.Add(MetaKeyword)
                End If
                If Not .IsDescriptionNull AndAlso .Description <> "" Then
                    Dim MetaDesc As New HtmlMeta()
                    MetaDesc.Name = "description"
                    MetaDesc.Content = .Description
                    Page.Header.Controls.Add(MetaDesc)
                End If
                ViewState("Content") = .Content
                ViewState("Content2") = .Content2

                ViewState("BannerUrl") = .BannerImageUrl
                ViewState("TitleUrl") = .TitleImageUrl
            End With
        Else
            Response.Redirect(ConfigurationManager.AppSettings("HomePage"))
        End If

    End Sub


End Class
