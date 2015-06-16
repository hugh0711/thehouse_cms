Imports System.Data
Imports Utility

Partial Class Articles
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        hfdFunctionID.Value = ConfigurationManager.AppSettings("FunctionIDArticles")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Bind()
        End If
    End Sub

    Protected Sub Bind()
        hfdProductID.Value = 0
        hfdCategoryID.Value = 0

        If (Request.QueryString("ProductID") & "").Length > 0 Then
            hfdProductID.Value = Request.QueryString("ProductID")
        End If
        If (Request.QueryString("CategoryID") & "").Length > 0 Then
            hfdCategoryID.Value = Request.QueryString("CategoryID")
        Else
            hfdCategoryID.Value = (New CategoryDataSetTableAdapters.CategoryTableAdapter()).GetFirstCategoryIDByFunctionID(ConfigurationManager.AppSettings("FunctionIDArticles")).GetValueOrDefault(0)
        End If

        If hfdProductID.Value = 0 Then
            hfdProductID.Value = (New ProductDataSetTableAdapters.view_ProductTableAdapter()).GetFirstProductIDByCategoryID(hfdCategoryID.Value).GetValueOrDefault(0)
        End If


        Dim r As ProductDataSet.view_ProductRow = Nothing
        Dim t As New ProductDataSet.view_ProductDataTable

        If hfdProductID.Value = 0 And hfdCategoryID.Value = 0 Then
            r = GetFirstProductByCategoryID(GetFirstCategory)
        ElseIf hfdCategoryID.Value > 0 And hfdProductID.Value = 0 Then
            r = GetFirstProductByCategoryID(hfdCategoryID.Value)
        ElseIf hfdProductID.Value > 0 Then
            t = (New ProductDataSetTableAdapters.view_ProductTableAdapter).GetDataByProductID(hfdProductID.Value, MyCulture)
            If t.Rows.Count > 0 Then
                r = t.Rows(0)
            End If
        End If

        If r IsNot Nothing Then
            With r
                hfdProductID.Value = r.ProductID
                hfdCategoryID.Value = r.CategoryID

                lblDate.Text = DateTimeToString(.ModifyDate, False)
                lblTitle.Text = .ProductName
                If (.fileUrl & "").Trim() <> "" Then
                    ltrContent.Text = String.Format("<object data='{0}' type='application/pdf' width='100%' height='900'>" _
                        & "<p>It appears you don't have a PDF plugin for this browser. " _
                        & "Please <a href='http://www.adobe.com'>download</a> " _
                        & "Adobe Reader</p> " _
                        & "</object> ", (.fileUrl & "").Trim())
                Else
                    ltrContent.Text = Replace(.Description, vbCr, "<br />")
                End If
            End With
        End If

        If Not Membership.GetUser Is Nothing Then
            pnlWriteComment.Visible = True
        Else
            pnlWriteComment.Visible = False
        End If

    End Sub

    Protected Function GetFirstProductByCategoryID(ByVal CategoryID As Integer) As ProductDataSet.view_ProductRow
        Dim r As ProductDataSet.view_ProductRow = Nothing
        Dim t As New ProductDataSet.view_ProductDataTable
        t = (New ProductDataSetTableAdapters.view_ProductTableAdapter).GetLastestProduct(CategoryID, 2, MyCulture)
        If t.Rows.Count > 0 Then
            r = t.Rows(0)
        End If
        Return r
    End Function

    Protected Function GetFirstCategory() As Integer
        Dim id As Integer = 0
        Dim r As CategoryDataSet.CategoryRow
        Dim t As New CategoryDataSet.CategoryDataTable
        t = (New CategoryDataSetTableAdapters.CategoryTableAdapter).GetDataByParentIDEnabled(0, 2)
        If t.Rows.Count > 0 Then
            r = t.Rows(0)
            id = r.CategoryID
        End If
        Return id
    End Function

    Protected Function GetImage(ByVal ProductID As Integer) As String
        Dim s As String = ""
        Dim imageRow As ProductDataSet.ProductImageRow
        Dim imageTable As New ProductDataSet.ProductImageDataTable
        imageTable = (New ProductDataSetTableAdapters.ProductImageTableAdapter).GetDataByProductID(ProductID)
        If imageTable.Rows.Count > 0 Then
            imageRow = imageTable.Rows(0)
            s = System.IO.Path.Combine(ConfigurationManager.AppSettings("ProductImagePath"), imageRow.Url)
        End If
        Return s
    End Function

    Protected Function IsCategorySelect(ByVal CategoryID As Integer) As String
        Dim s As String = ""
        If CategoryID = hfdCategoryID.Value Then
            s = "categoryselect"
        End If
        Return s
    End Function

    Protected Function IsDisplayProduct(ByVal CategoryID As Integer) As String
        Dim s As String = "false"
        If CategoryID = hfdCategoryID.Value Then
            s = "true"
        End If
        Return s
    End Function

    Protected Function DisplayUserName(ByVal UserID As String) As String
        Return UserID
    End Function

    Protected Sub btnPostComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPostComment.Click
        If txtWriteComment.Text.Trim.Length > 0 Then
            CommentClass.PostComment(hfdProductID.Value, CommentClass.CommentType_Product, txtWriteComment.Text, Membership.GetUser.UserName)
            txtWriteComment.Text = ""
            lstComment.DataBind()
        End If
    End Sub

End Class
