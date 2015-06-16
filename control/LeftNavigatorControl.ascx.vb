
Partial Class control_LeftNavigatorControl
    Inherits System.Web.UI.UserControl

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
        Dim PageID3 As Integer = 0

        Dim Level As Integer = 1
        Dim tParentID As Integer = PageID
        Do Until tParentID = 0
            tParentID = PageAdapter.GetParentID(tParentID).GetValueOrDefault(0)
            Level += 1
        Loop

        Select Case Level
            Case Is = 3
                ParentID = PageAdapter.GetParentID(PageID)
            Case Is >= 4
                For i As Integer = 4 To Level
                    PageID3 = PageID
                    ParentID = PageAdapter.GetParentID(PageID)
                    PageID = ParentID
                    ParentID = PageAdapter.GetParentID(PageID)
                Next
                'Case Is = 2
                '    PageID = PageAdapter.GetDefaultPageID(PageID)
        End Select

        'lblLevel.Text = Level
        hfdParentID.Value = ParentID
        hfdPageID.Value = PageID
        hfdPageID3.Value = PageID3
        'lblTitle.Text = (New PageDataSetTableAdapters.view_PageTableAdapter()).GetPageTitleByPageID(ParentID, hfdLang.Value)

        ListView1.DataBind()
    End Sub


End Class
