Imports System.Threading
Imports System.Globalization
Imports System.IO

Partial Class control_LanguageControl
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request("lang") IsNot Nothing Then
                Dim Locale As String = Session("MyCulture")
                Select Case Request("lang").ToLower()
                    Case "en"
                        Session("MyCulture") = "en-us"
                    Case "tc"
                        Session("MyCulture") = "zh-hk"
                    Case "sc"
                        Session("MyCulture") = "zh-cn"
                End Select
                Thread.CurrentThread.CurrentUICulture = New CultureInfo(Session("MyCulture").ToString())
                Response.Redirect(Request.Path)
            End If
        End If

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(Session("MyCulture").ToString())

    End Sub

    Protected Sub LoadData()
        'Dim Language() As String = ConfigurationManager.AppSettings("LanguageSupport").ToString().Split(",")
        'Dim DefaultLanguage As String = ConfigurationManager.AppSettings("DefaultLanguage")
        'Dim item As ListItem
        'Dim items As New ListItemCollection
        'Dim CurrentCulture As String = Session("MyCulture").ToString().ToLower()

        'For Each lang In Language
        '    If lang <> CurrentCulture Then
        '        item = New ListItem(LanguageClass.GetLanguageName(lang), lang)
        '        items.Add(item)
        '    End If
        'Next

        'lvLanguage.DataSource = items
        'lvLanguage.DataBind()
    End Sub


    'Protected Sub bltLanguage_Click(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.BulletedListEventArgs) Handles bltLanguage.Click
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo(bltLanguage.Items(e.Index).Value)
    '    Response.Redirect(Request.Url.OriginalString)
    'End Sub

    Protected Sub btnLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLangEn.Click, btnLangSC.Click, btnLangTC.Click
        Dim button As ImageButton = CType(sender, ImageButton)
        Session("MyCulture") = button.CommandArgument
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(button.CommandArgument)
        Dim Filename As String = Path.GetFileName(Request.Path)
        Dim url As String
        Select Case Filename.ToLower
            Case "page.aspx", "opportunities.aspx", "pressreleases.aspx", "publication.aspx", "feedback.aspx", "event.aspx", "news.aspx"
                url = Request.UrlReferrer.AbsolutePath
            Case Else
                url = Request.Url.AbsolutePath
        End Select
        Response.Redirect(url)
    End Sub



End Class
