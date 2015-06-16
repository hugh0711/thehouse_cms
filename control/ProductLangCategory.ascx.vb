Imports System.IO


Partial Class control_ProductLangCategory
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        'Dim _FileBrowser As New CKFinder.FileBrowser()
        '_FileBrowser.BasePath = ResolveClientUrl("~/ckfinder") & "/"
        '_FileBrowser.SetupFCKeditor(htmlDescription)

        Dim Key As String = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
        Page.ClientScript.RegisterOnSubmitStatement(htmlDescription.GetType(), Key, "FCKeditorAPI.GetInstance('" + htmlDescription.ClientID + "').UpdateLinkedField();")
        Key = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
    End Sub

    Public Sub SetLang(ByVal Lang As String)
        hfdLang.Value = Lang
    End Sub

    Public Sub LoadSiteFunction(ByVal FunctionID As Integer)
        Dim Site As New SiteFunctionClass(FunctionID)
        With Site
            txtDesc.Visible = .HasDescription
            htmlDescription.Visible = .HTMLDescription

            If .HasDescription Then
                If .HTMLDescription Then
                    htmlDescription.Visible = True
                    'txtDescription.Visible = False
                    'txtDescription2.Visible = False
                    'RequiredFieldValidator3.Enabled = False
                    'RequiredFieldValidator4.Enabled = False
                Else
                    txtDesc.Visible = True
                    'htmlDescription.Visible = False
                    'htmlDescription2.Visible = False
                    'RequiredFieldValidator1.Enabled = False
                End If
            End If


        End With



        ViewState("FunctionSettings") = Site
        ViewState("fn") = FunctionID


    End Sub

    Public Sub LoadProductCategoryLang(ByVal CategoryID As Integer, ByVal Lang As String)

        Dim cDB As New CategoryDataClassesDataContext

        Dim found_record = (From c In cDB.CategoryNames
                           Where c.CategoryID = CategoryID And c.Lang = Lang
                           Select c).FirstOrDefault

        If found_record IsNot Nothing Then
            txtCategory.Text = found_record.CategoryName

            If txtDesc.Visible Then
                txtDesc.Text = found_record.Description
            Else
                htmlDescription.Value = found_record.Description
            End If
        End If



        'btnLanguage.Visible = (ConfigurationManager.AppSettings("LanguageSupport").LastIndexOf(","c) <> -1)

        hfdLang.Value = Lang
    End Sub


    Protected Sub btnLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLanguage.Click
        Response.Redirect("~/backoffice/category_name.aspx?id=" & ViewState("CategoryID"))
    End Sub


    Public Function SaveProductCategoryLang(ByVal ProductCategoryID As Integer) As Boolean
        Dim Success As Boolean = True


        ' Create CategoryName to other languages
        Dim CategoryNameAdapter As New CategoryDataSetTableAdapters.CategoryNameTableAdapter()
        Dim LanguageSupport() As String = ConfigurationManager.AppSettings("LanguageSupport").ToString().Split(",")
        Dim DefaultLanguage As String = ConfigurationManager.AppSettings("DefaultLanguage")
        Dim CategoryName, Description As String

        CategoryName = txtCategory.Text
        If txtDesc.Visible Then
            Description = txtDesc.Text
        Else
            Description = htmlDescription.Value
        End If
        If CategoryNameAdapter.UpdateQuery(CategoryName, Description, ProductCategoryID, hfdLang.Value) = 0 Then
            CategoryNameAdapter.Insert(ProductCategoryID, hfdLang.Value, CategoryName, Description)
        End If

        Dim cDB As New CategoryDataClassesDataContext

        If hfdLang.Value = DefaultLanguage Then

            Dim cn = From c In cDB.Categories
                   Where c.CategoryID = ProductCategoryID
                   Select c

            If cn IsNot Nothing Then
                cn.FirstOrDefault.Category = CategoryName
                Try
                    cDB.SubmitChanges()
                Catch ex As Exception

                End Try
            End If

        End If


        Return Success
    End Function


End Class
