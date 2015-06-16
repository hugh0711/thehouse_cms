
Partial Class control_UnitSelectionControl
    Inherits System.Web.UI.UserControl

    Public Event SelectIndexChanged()

    Public Property Culture As String = ConfigurationManager.AppSettings("UIDefaultLanguage")

    Public Function CategoryCaption(Index As Integer) As String
        Dim CategoryName As String = "類別"
        If ViewState("CategoryCaptionList") IsNot Nothing Then
            Dim v As String() = ViewState("CategoryCaptionList").ToString.Split(",")
            If v.Length - 1 >= Index Then
                CategoryName = v(Index)
            End If
        End If
        Return CategoryName
    End Function

    Public Property FunctionID As Integer
        Get
            Return CInt("0" & ViewState("FunctionID"))
        End Get
        Set(value As Integer)
            ViewState("FunctionID") = value
        End Set
    End Property

    Public Sub New()
        ViewState("Controls") = New List(Of String) From {""}
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim Site As New SiteFunctionClass(FunctionID)
            ViewState("CategoryCaptionList") = Site.CategoryCaptionList
            ViewState("UnitCaption") = Site.FunctionName
            lblUnit.Text = Site.FunctionName
            If UnitID = 0 Then
                ViewState("Controls") = New List(Of String) From {""}
                LoadUnit(CategoryID)
            End If
        End If
        ' Re-generate controls
        GenerateControls()
        'lblFunctionID.Text = 
    End Sub

    Protected Sub GenerateControls()
        Dim Values As List(Of String) = CType(ViewState("Controls"), List(Of String))
        Dim ParentID As Integer = 0

        PlaceHolder1.Controls.Clear()
        For Each v As String In Values
            AddLevel(ParentID, v)
            If v = "" Then Exit For
            ParentID = CInt(v)
        Next
    End Sub

    Public Property UnitID As Integer
        Get
            Return CInt("0" & ddlUnit.SelectedValue)
        End Get
        Set(value As Integer)
            Dim CategoryID As Integer = (New ProductDataSetTableAdapters.view_ProductTableAdapter()).GetCategoryID(value).GetValueOrDefault(0)
            If CategoryID <> 0 Then
                Me.CategoryID = CategoryID
                LoadUnit(CategoryID)
                ddlUnit.SelectedValue = value
            End If
        End Set
    End Property

    Public Property CategoryID() As Integer
        Get
            Return GetValue()
        End Get
        Set(ByVal value As Integer)
            SetValue(value)
            GenerateControls()
            AddLevel(value, "")
            SaveValues()
        End Set
    End Property

    Protected Function GetValue() As Integer
        Dim ID As Integer = 0
        Dim Values As List(Of String)
        If ViewState("Controls") IsNot Nothing Then
            Values = CType(ViewState("Controls"), List(Of String))
            If Values(Values.Count - 1) = "" Then
                If Values.Count > 1 Then
                    ID = CInt(Values(Values.Count - 2))
                End If
            Else
                ID = CInt(Values(Values.Count - 1))
            End If
        End If
        Return ID
    End Function

    Protected Sub SetValue(Value As Integer)
        Dim Values As New List(Of String)
        Dim da As New CategoryDataSetTableAdapters.CategoryTableAdapter()

        Do Until Value = 0
            Values.Insert(0, Value)
            Value = da.GetParentID(Value)
        Loop

        ViewState("Controls") = Values
    End Sub

    Protected Sub SaveValues()
        Dim Values As New List(Of String)
        For Each d As DropDownList In PlaceHolder1.Controls
            Values.Add(d.SelectedValue)
            If d.SelectedValue = "" Then Exit For
        Next
        ViewState("Controls") = Values
    End Sub

    Protected Sub AddLevel(ParentID As Integer, SelectedValue As String)
        Dim ddlCategory As New DropDownList
        Dim da As New CategoryDataSetTableAdapters.view_CategoryTableAdapter()
        Dim dt As CategoryDataSet.view_CategoryDataTable

        dt = da.GetDataByFunctionIDParentID(FunctionID, ParentID, Culture)
        With ddlCategory
            Dim Level As Integer = GetMaxLevel()
            .Items.Add(New ListItem(String.Format("-- 請選擇{0} --", CategoryCaption(Level)), ""))

            .AppendDataBoundItems = True
            .AutoPostBack = True
            .CssClass = "textEntry"
            .DataSource = dt
            .DataTextField = "CategoryName"
            .DataValueField = "CategoryID"
            .DataBind()

            .SelectedValue = SelectedValue

            .ID = String.Format("ddlCategory{0}", Level)
            'SetValue(Level, "")
        End With

        If ddlCategory.Items.Count > 1 Then
            AddHandler ddlCategory.SelectedIndexChanged, AddressOf Me.ddlCategory_SelectedIndexChanged
            PlaceHolder1.Controls.Add(ddlCategory)
        End If
    End Sub

    Protected Function GetMaxLevel() As Integer
        Return PlaceHolder1.Controls.Count
    End Function

    Protected Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlCateogry As DropDownList = CType(sender, DropDownList)
        Dim CurrentLevel As Integer = CInt(ddlCateogry.ID.Substring(11))

        ' Remove the sub-ordinate levels
        Do Until CurrentLevel >= GetMaxLevel() - 1
            PlaceHolder1.Controls.RemoveAt(GetMaxLevel() - 1)
        Loop

        If ddlCateogry.SelectedIndex = 0 Then

        ElseIf ddlCateogry.SelectedIndex <> -1 Then
            ' Set the level
            AddLevel(CInt(ddlCateogry.SelectedValue), "")
        End If
        SaveValues()

        LoadUnit(GetValue)
        RaiseEvent SelectIndexChanged()
    End Sub

    Protected Sub LoadUnit(CategoryID As Integer)
        Dim da As New ProductDataSetTableAdapters.view_ProductTableAdapter()
        With ddlUnit
            .DataSource = da.GetDataByCategoryID(CategoryID, Culture)
            .DataTextField = "ProductName"
            .DataValueField = "ProductID"
            .DataBind()

            If .Items.Count > 0 Then
                .Enabled = True
            Else
                .Enabled = False
                .Items.Clear()
                Dim Item As New ListItem(String.Format("-- 沒有{0} --", ViewState("UnitCaption")), "")
                .Items.Add(Item)
            End If
        End With
    End Sub
End Class
