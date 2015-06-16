
Partial Class backoffice_tag
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request("fn") IsNot Nothing AndAlso Request("fn") <> "" Then
                hfdFunctionID.Value = Request("fn")
            End If

            If Request("id") IsNot Nothing Then
                ViewState("TagID") = CInt(Request("id"))
                LoadData()
                btnDelete.Visible = True
            Else
                btnDelete.Visible = False
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
            Response.Redirect(String.Format("~/backoffice/tags.aspx?fn={0}", hfdFunctionID.Value))
        Catch ex As Exception
            lblMessage.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect(String.Format("~/backoffice/tags.aspx?fn={0}", hfdFunctionID.Value))
    End Sub

    Protected Sub LoadData()
        Dim TagAdapter As New TagDataSetTableAdapters.TagTableAdapter()
        Dim TagTable As TagDataSet.TagDataTable
        Dim TagRow As TagDataSet.TagRow
        Dim TagID As Integer = ViewState("TagID")

        TagTable = TagAdapter.GetDataByID(TagID)
        If TagTable.Rows.Count > 0 Then
            TagRow = TagTable.Rows(0)
            With TagRow
                txtTag.Text = .Tag
                txtTagGroup.Text = .TagGroup
                chkEnabled.Checked = .Enabled
            End With
            btnLanguage.Visible = True

            hfdFunctionID.Value = (New TagDataSetTableAdapters.TagTableAdapter()).GetFunctionID(TagID).GetValueOrDefault(0)
        End If
    End Sub

    Protected Sub SaveData()
        Dim TagAdapter As New TagDataSetTableAdapters.TagTableAdapter()

        If ViewState("TagID") Is Nothing Then
            Dim FunctionID As Integer = CInt(hfdFunctionID.Value)
            Dim SortOrder As Integer = (New TagDataSetTableAdapters.TagTableAdapter()).GetNextSortOrder(FunctionID).GetValueOrDefault(0)
            ViewState("TagID") = TagAdapter.InsertQuery(txtTag.Text, txtTagGroup.Text, chkEnabled.Checked, Page.User.Identity.Name, FunctionID, SortOrder)
            SaveTagName()
        Else
            TagAdapter.UpdateQuery(txtTag.Text, txtTagGroup.Text, chkEnabled.Checked, Page.User.Identity.Name, ViewState("TagID"))
            SaveTagName()
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim TagAdapter As New TagDataSetTableAdapters.TagTableAdapter()
        Dim TagNameAdapter As New TagDataSetTableAdapters.TagNameTableAdapter()

        TagAdapter.DeleteQuery(ViewState("TagID"))
        TagNameAdapter.DeleteByTagID(ViewState("TagID"))
        Response.Redirect(String.Format("~/backoffice/tags.aspx?fn={0}", hfdFunctionID.Value))
    End Sub

    Protected Sub btnLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLanguage.Click
        Response.Redirect("~/backoffice/tag_name.aspx?id=" & ViewState("TagID"))
    End Sub

    Protected Sub SaveTagName()
        ' Create TagName to other languages
        Dim TagNameAdapter As New TagDataSetTableAdapters.TagNameTableAdapter()
        Dim LanguageSupport() As String = ConfigurationManager.AppSettings("LanguageSupport").ToString().Split(",")
        Dim DefaultLanguage As String = ConfigurationManager.AppSettings("DefaultLanguage")
        Dim TagName As String
        Dim Translate As New GoogleTranslateClass
        Dim TagID As Integer = ViewState("TagID")

        For Each ToLang In LanguageSupport
            TagName = Translate.Translate(txtTag.Text, DefaultLanguage, ToLang)
            If TagNameAdapter.UpdateQuery(TagName, TagID, ToLang) = 0 Then
                TagNameAdapter.Insert(TagID, ToLang, TagName)
            End If
        Next
    End Sub

End Class
