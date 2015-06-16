
Partial Class UserControl_CategoryPathControl
    Inherits System.Web.UI.UserControl

    Public Property CategoryID() As Integer
        Get
            Return Convert.ToInt32(ViewState("CategoryID"))
        End Get
        Set(ByVal value As Integer)
            ViewState("CategoryID") = value
        End Set
    End Property

    Public Sub ShowPath(Optional ByVal NavigateUrl As String = "", Optional ByVal RootText As String = "", Optional ByVal ShowAllLink As Boolean = False, Optional ByVal FunctionID As Integer = 0)
        Literal1.Text = CategoryClass.GetCategoryPath(Convert.ToInt32(ViewState("CategoryID")), , NavigateUrl, RootText, ConfigurationManager.AppSettings("DefaultLanguage"), ShowAllLink, FunctionID)
    End Sub

    'Public Sub ShowPathWidthID(Optional ByVal NavigateUrl As String = "", Optional ByVal RootText As String = "", Optional ByVal ShowAllLink As Boolean = False)
    '    Literal1.Text = CategoryClass.GetCategoryPathWithID(Convert.ToInt32(ViewState("CategoryID")), , NavigateUrl, RootText, Session("MyCulture"), ShowAllLink)
    'End Sub

    Public ReadOnly Property Level() As Integer
        Get
            Return CategoryClass.GetLevel(Convert.ToInt32(ViewState("CategoryID")))
        End Get
    End Property

End Class
