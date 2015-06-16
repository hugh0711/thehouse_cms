
Partial Class UserControl_PagePathControl
    Inherits System.Web.UI.UserControl

    Public Property PageID() As Integer
        Get
            Return Convert.ToInt32(ViewState("PageID"))
        End Get
        Set(ByVal value As Integer)
            ViewState("PageID") = value
        End Set
    End Property

    Public Sub ShowPath(Optional ByVal NavigateUrl As String = "", Optional ByVal RootText As String = "", Optional ByVal ShowAllLink As Boolean = False)
        Literal1.Text = PageClass.GetPagePath(Convert.ToInt32(ViewState("PageID")), , NavigateUrl, RootText, Session("MyCulture"), ShowAllLink)
    End Sub

    Public Sub ShowPathWidthID(Optional ByVal NavigateUrl As String = "", Optional ByVal RootText As String = "", Optional ByVal ShowAllLink As Boolean = False)
        Literal1.Text = PageClass.GetPagePathWithID(Convert.ToInt32(ViewState("PageID")), , NavigateUrl, RootText, Session("MyCulture"), ShowAllLink)
    End Sub

    Public ReadOnly Property Level() As Integer
        Get
            Return PageClass.GetLevel(Convert.ToInt32(ViewState("CategoryID")))
        End Get
    End Property

End Class
