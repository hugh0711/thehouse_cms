
Partial Class control_TopMenuControl
    Inherits System.Web.UI.UserControl


    Const TotalWidth As Integer = 722

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadData()
        End If
    End Sub

    Protected Sub LoadData()
        hfdLang.Value = Session("MyCulture")
        Dim PageAdapter As New PageDataSetTableAdapters.PageTableAdapter()
        Dim PageID As Integer = PageAdapter.GetPageIDByUrl(Request("url")).GetValueOrDefault(0)
        Dim ParentID As Integer = PageID

        Dim Level As Integer = 1
        Dim tParentID As Integer = PageID
        Do Until tParentID = 0
            tParentID = PageAdapter.GetParentID(tParentID).GetValueOrDefault(0)
            Level += 1
        Loop

        If Level >= 3 Then
            ParentID = PageAdapter.GetParentID(PageID)
        End If
        If Level >= 4 Then
            PageID = ParentID
            ParentID = PageAdapter.GetParentID(PageID)
        End If
        'lblLevel.Text = Level
        hfdParentID.Value = 0
        hfdPageID.Value = PageID

        ListView1.DataBind()
    End Sub

    Protected Function GetWidth(ByVal PageID As Integer) As Integer
        Dim Count As Integer = (New PageDataSetTableAdapters.view_PageTableAdapter()).CountPageByParentID(PageID, hfdLang.Value)
        If Count = 0 Then
            Return TotalWidth
        Else
            Return Math.Floor(TotalWidth / Count) - 1
        End If
    End Function


End Class
