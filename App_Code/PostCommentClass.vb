Imports Microsoft.VisualBasic

Public Class PostCommentClass

    Private CommentTypeValue As String
    Public Property CommentType() As String
        Get
            ' Gets the property value. 
            Return CommentTypeValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            CommentTypeValue = Value
        End Set
    End Property

    Private ReferenceIDValue As Integer
    Public Property ReferenceID() As Integer
        Get
            ' Gets the property value. 
            Return ReferenceIDValue
        End Get
        Set(ByVal Value As Integer)
            ' Sets the property value.
            ReferenceIDValue = Value
        End Set
    End Property

    Private UserIDValue As String
    Public Property UserID() As String
        Get
            ' Gets the property value. 
            Return UserIDValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            UserIDValue = Value
        End Set
    End Property

    Private CommentDescriptionValue As String
    Public Property CommentDescription() As String
        Get
            ' Gets the property value. 
            Return CommentDescriptionValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            CommentDescriptionValue = Value
        End Set
    End Property

    Private IsInspectedValue As Boolean
    Public Property IsInspected() As Boolean
        Get
            ' Gets the property value. 
            Return IsInspectedValue
        End Get
        Set(ByVal Value As Boolean)
            ' Sets the property value.
            IsInspectedValue = Value
        End Set
    End Property

    Private IsDisableValue As Boolean
    Public Property IsDisable() As Boolean
        Get
            ' Gets the property value. 
            Return IsDisableValue
        End Get
        Set(ByVal Value As Boolean)
            ' Sets the property value.
            IsDisableValue = Value
        End Set
    End Property

    Private ParentIDValue As Integer
    Public Property ParentID() As Integer
        Get
            ' Gets the property value. 
            Return ParentIDValue
        End Get
        Set(ByVal Value As Integer)
            ' Sets the property value.
            ParentIDValue = Value
        End Set
    End Property

End Class
