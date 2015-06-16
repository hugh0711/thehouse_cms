Imports Microsoft.VisualBasic

Public Class ResultErrorClass

    Private userNameValue As String
    Public Property UserName() As String
        Get
            ' Gets the property value. 
            Return userNameValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            userNameValue = Value
        End Set
    End Property

    Private ErrorMessageValue As String
    Public Property ErrorMessage() As String
        Get
            ' Gets the property value. 
            Return ErrorMessageValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            ErrorMessageValue = Value
        End Set
    End Property

End Class
