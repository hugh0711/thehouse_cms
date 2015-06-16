Imports Microsoft.VisualBasic

Public Class LoginSessionClass

    'return data for Product Source Url
    Private m_Return_LoginSuccess As Integer
    Public Property Return_LoginSuccess() As Integer
        Get
            Return m_Return_LoginSuccess
        End Get
        Set(value As Integer)
            m_Return_LoginSuccess = value
        End Set
    End Property

    'return data for Product Source Url
    Private m_Return_UserRole As String
    Public Property Return_UserRole() As String
        Get
            Return m_Return_UserRole
        End Get
        Set(value As String)
            m_Return_UserRole = value
        End Set
    End Property

    'return data for Product Source Url
    Private m_Return_SessionID As String
    Public Property Return_SessionID() As String
        Get
            Return m_Return_SessionID
        End Get
        Set(value As String)
            m_Return_SessionID = value
        End Set
    End Property

    'return data for Product Source Url
    Private m_Return_ErrorMessage As String
    Public Property Return_ErrorMessage() As String
        Get
            Return m_Return_ErrorMessage
        End Get
        Set(value As String)
            m_Return_ErrorMessage = value
        End Set
    End Property

End Class
