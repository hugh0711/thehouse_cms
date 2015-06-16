Imports Microsoft.VisualBasic

<Serializable()> _
Public Class PromoClass

#Region "Property"

    Private _PromoSettingID As Integer
    Public Property PromoSettingID() As Integer
        Get
            Return _PromoSettingID
        End Get
        Set(ByVal value As Integer)
            _PromoSettingID = value
        End Set
    End Property

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _TypeID As Integer
    Public Property TypeID() As Integer
        Get
            Return _TypeID
        End Get
        Set(ByVal value As Integer)
            _TypeID = value
        End Set
    End Property

    Private _Width As Integer
    Public Property Width() As Integer
        Get
            Return _Width
        End Get
        Set(ByVal value As Integer)
            _Width = value
        End Set
    End Property

    Private _Height As Integer
    Public Property Height() As Integer
        Get
            Return _Height
        End Get
        Set(ByVal value As Integer)
            _Height = value
        End Set
    End Property

    Private _IsLockAspectRatio As Boolean
    Public Property IsLockAspectRatio() As Boolean
        Get
            Return _IsLockAspectRatio
        End Get
        Set(ByVal value As Boolean)
            _IsLockAspectRatio = value
        End Set
    End Property

    Private _SortOrder As Integer
    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
        End Set
    End Property

    Private _Enabled As Boolean
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
        End Set
    End Property

    Private _ImageShared As Boolean
    Public Property ImageShared() As Boolean
        Get
            Return _ImageShared
        End Get
        Set(ByVal value As Boolean)
            _ImageShared = value
        End Set
    End Property

    Private _AllowRole As String
    Public Property AllowRole() As String
        Get
            Return _AllowRole
        End Get
        Set(ByVal value As String)
            _AllowRole = value
        End Set
    End Property

    Private _AllowSort As Boolean
    Public Property AllowSort() As Boolean
        Get
            Return _AllowSort
        End Get
        Set(ByVal value As Boolean)
            _AllowSort = value
        End Set
    End Property

    Private _UnitFunctionID As Integer
    Public Property UnitFunctionID() As Integer
        Get
            Return _UnitFunctionID
        End Get
        Set(ByVal value As Integer)
            _UnitFunctionID = value
        End Set
    End Property




#End Region

#Region "Constructor"
    Public Sub New()

    End Sub

    Public Sub New(ByVal PromoSettingID As Integer)
        Load(PromoSettingID)
    End Sub
#End Region

#Region "Method"
    Public Sub Load(ByVal PromoSettingID As Integer)
        Dim dt As PromoDataSet.PromoSettingDataTable = (New PromoDataSetTableAdapters.PromoSettingTableAdapter()).GetDataByID(PromoSettingID)
        Dim dr As PromoDataSet.PromoSettingRow

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            With dr
                _PromoSettingID = .PromoSettingID
                _Name = .Name
                _TypeID = .TypeID
                _Width = .Width
                _Height = .Height
                _IsLockAspectRatio = .IsLockAspectRatio
                _SortOrder = .SortOrder
                _Enabled = .Enabled
                _ImageShared = .ImageShared
                _AllowRole = .AllowRole
                _AllowSort = .AllowSort
                _UnitFunctionID = .UnitFunctionID
            End With
        End If
    End Sub

    Public Function AllowUser(ByVal Username As String) As Boolean
        Dim Allow As Boolean = False

        Dim Rs As String() = _AllowRole.Split(",")
        For Each r As String In Rs
            If Roles.IsUserInRole(Username, r) Then
                Allow = True
                Exit For
            End If
        Next

        Return Allow
    End Function
#End Region

End Class
