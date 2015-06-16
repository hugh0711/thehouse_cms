Imports Microsoft.VisualBasic

Public Class CategoryClass

    Public Shared Function GetCategoryPath(ByVal CategoryID As Integer, Optional ByVal Separator As String = " > ", Optional ByVal NavigateUrl As String = "", Optional ByVal RootText As String = "", Optional ByVal Culture As String = "", Optional ByVal ShowAllLink As Boolean = False, Optional ByVal FunctionID As Integer = 0) As String
        Dim CategoryAdaptor As New CategoryDataSetTableAdapters.view_CategoryTableAdapter()
        Dim NodeText As String
        Dim ParentID As Integer
        Dim CategoryPath As New ArrayList
        FunctionID = CategoryAdaptor.GetFunctionID(CategoryID).GetValueOrDefault(FunctionID)
        Dim _ID As Integer = CategoryID

        Do Until _ID = 0
            NodeText = CategoryAdaptor.GetCategoryName(_ID, Culture)
            ParentID = CategoryAdaptor.GetParentID(_ID)

            If NavigateUrl <> "" And (CategoryID <> _ID Or ShowAllLink) Then
                NodeText = String.Format("<a href='{0}' >{1}</a>", String.Format(NavigateUrl, _ID, FunctionID), NodeText)
            End If

            CategoryPath.Add(NodeText)
            _ID = ParentID
        Loop

        If RootText <> "" Then
            If NavigateUrl <> "" And CategoryID <> 0 Then
                NodeText = String.Format("<a href='{0}'>{1}</a>", String.Format(NavigateUrl, 0, FunctionID), RootText)
            Else
                NodeText = RootText
            End If
            CategoryPath.Add(NodeText)
        End If

        Dim arr As String() = CategoryPath.ToArray(GetType(String))
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

