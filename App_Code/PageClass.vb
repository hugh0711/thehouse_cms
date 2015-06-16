Imports Microsoft.VisualBasic

Public Class PageClass

    Public Shared Function GetPagePath(ByVal PageID As Integer, Optional ByVal Separator As String = " > ", Optional ByVal NavigateUrl As String = "", Optional ByVal RootText As String = "", Optional ByVal Culture As String = "", Optional ByVal ShowAllLink As Boolean = False) As String
        Dim PageAdaptor As New PageDataSetTableAdapters.view_PageTableAdapter()
        Dim PageTable As PageDataSet.view_PageDataTable
        Dim PageRow As PageDataSet.view_PageRow
        Dim NodeText As String
        Dim ParentID As Integer
        Dim Url As String
        Dim PagePath As New ArrayList

        Dim _ID As Integer = PageID

        Do Until _ID = 0
            PageTable = PageAdaptor.GetDataByIDLang(_ID, Culture)
            If PageTable.Rows.Count > 0 Then
                PageRow = PageTable.Rows(0)
                NodeText = PageRow.Title
                Url = PageRow.url
                ParentID = PageRow.ParentPageID

                If NavigateUrl <> "" Or (PageID <> _ID Or ShowAllLink) Then
                    NodeText = String.Format("<a href='{0}.htm' >{1}</a>", Url, NodeText)
                End If

                PagePath.Add(NodeText)
                _ID = ParentID
            End If
        Loop

        If RootText <> "" Then
            If NavigateUrl <> "" And PageID <> 0 Then
                NodeText = String.Format("<a href='{0}{2}'>{1}</a>", NavigateUrl, RootText, 0)
            Else
                NodeText = String.Format("<a href='default.htm'>{0}</a>", RootText)
            End If
            PagePath.Add(NodeText)
        End If

        Dim arr As String() = PagePath.ToArray(GetType(String))
        Array.Reverse(arr)

        Return Join(arr, Separator)
    End Function


    Public Shared Function GetPagePathWithID(ByVal PageID As Integer, Optional ByVal Separator As String = " > ", Optional ByVal NavigateUrl As String = "", Optional ByVal RootText As String = "", Optional ByVal Culture As String = "", Optional ByVal ShowAllLink As Boolean = False) As String
        Dim PageAdaptor As New PageDataSetTableAdapters.view_PageTableAdapter()
        Dim PageTable As PageDataSet.view_PageDataTable
        Dim PageRow As PageDataSet.view_PageRow
        Dim NodeText As String
        Dim ParentID As Integer
        Dim Url As String
        Dim PagePath As New ArrayList

        Dim _ID As Integer = PageID

        Do Until _ID = 0
            PageTable = PageAdaptor.GetDataByIDLang(_ID, Culture)
            If PageTable.Rows.Count > 0 Then
                PageRow = PageTable.Rows(0)
                NodeText = PageRow.Title
                Url = PageRow.url
                ParentID = PageRow.ParentPageID

                If NavigateUrl <> "" And (PageID <> _ID Or ShowAllLink) Then
                    NodeText = String.Format("<a href='{0}{2}' >{1}</a>", NavigateUrl, NodeText, _ID)
                End If

                PagePath.Add(NodeText)
                _ID = ParentID
            End If
        Loop

        If RootText <> "" Then
            If NavigateUrl <> "" And PageID <> 0 Then
                NodeText = String.Format("<a href='{0}{2}'>{1}</a>", NavigateUrl, RootText, 0)
            Else
                NodeText = RootText
            End If
            PagePath.Add(NodeText)
        End If

        Dim arr As String() = PagePath.ToArray(GetType(String))
        Array.Reverse(arr)

        Return Join(arr, Separator)
    End Function

    Public Shared Function GetLevel(ByVal CategoryID As Integer) As Integer
        Dim CategoryAdaptor As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim Level As Integer = 1

        Do Until CategoryID = 0
            Level += 1
            CategoryID = CategoryAdaptor.GetParentID(CategoryID)
        Loop

        Return Level
    End Function
End Class

