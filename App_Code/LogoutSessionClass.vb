Imports Microsoft.VisualBasic

Public Class LogoutSessionClass


    'return data for Product Source Url
    Private m_Return_LogoutSuccess As Integer
    Public Property Return_LogoutSuccess() As Integer
        Get
            Return m_Return_LogoutSuccess
        End Get
        Set(value As Integer)
            m_Return_LogoutSuccess = value
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
