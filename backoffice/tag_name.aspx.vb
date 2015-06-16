
Partial Class backoffice_tag_name
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request("id") IsNot Nothing Then
                hfdTagID.Value = Request("id")
                Bind()
            End If
        End If
    End Sub

    Protected Sub Bind()
        GridView1.DataBind()
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("~/backoffice/tag.aspx?id=" & hfdTagID.Value)
    End Sub

    Protected Sub btnTranslate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim TagAdapter As New TagDataSetTableAdapters.TagTableAdapter()
            Dim TagNameAdapter As New TagDataSetTableAdapters.TagNameTableAdapter()
            Dim LanguageSupport() As String = ConfigurationManager.AppSettings("LanguageSupport").ToString().Split(",")
            Dim TagName As String
            Dim Translate As New GoogleTranslateClass

            Dim container As Control = CType(sender, Button).Parent()
            Dim hfdTagNameID As HiddenField = CType(container.FindControl("hfdTagNameID"), HiddenField)
            Dim hfdTagName As HiddenField = CType(container.FindControl("hfdTagName"), HiddenField)
            Dim hfdLang As HiddenField = CType(container.FindControl("hfdLang"), HiddenField)

            Dim Tag As String = hfdTagName.Value
            Dim Language As String = hfdLang.Value
            Dim TagID As Integer = CInt(hfdTagID.Value)

            For Each ToLang In LanguageSupport
                TagName = Translate.Translate(Tag, Language, ToLang)
                If TagNameAdapter.UpdateQuery(TagName, TagID, ToLang) = 0 Then
                    TagNameAdapter.Insert(TagID, ToLang, TagName)
                End If

                If ToLang = ConfigurationManager.AppSettings("DefaultLanguage") Then
                    TagAdapter.UpdateTag(TagName, TagID)
                End If
            Next
            Bind()
            lblMessage.Text = "類別名稱已翻譯完成"
            lblMessage.ForeColor = Drawing.Color.Black
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Drawing.Color.Red
        End Try
    End Sub

End Class
