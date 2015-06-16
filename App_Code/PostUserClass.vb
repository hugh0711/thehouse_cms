Imports Microsoft.VisualBasic

Public Class PostUserClass

    Private UserNameValue As String
    Public Property UserName() As String
        Get
            ' Gets the property value. 
            Return UserNameValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            UserNameValue = Value
        End Set
    End Property

    Private PasswordValue As String
    Public Property Password() As String
        Get
            ' Gets the property value. 
            Return PasswordValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            PasswordValue = Value
        End Set
    End Property

    Private DisplayNameValue As String
    Public Property DisplayName() As String
        Get
            ' Gets the property value. 
            Return DisplayNameValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            DisplayNameValue = Value
        End Set
    End Property

    Private NewUserValue As Boolean
    Public Property NewUser() As Boolean
        Get
            ' Gets the property value. 
            Return NewUserValue
        End Get
        Set(ByVal Value As Boolean)
            ' Sets the property value.
            NewUserValue = Value
        End Set
    End Property

    Private ChangePasswordValue As Boolean
    Public Property ChangePassword() As Boolean
        Get
            ' Gets the property value. 
            Return ChangePasswordValue
        End Get
        Set(ByVal Value As Boolean)
            ' Sets the property value.
            ChangePasswordValue = Value
        End Set
    End Property

    Private NewPasswordValue As String
    Public Property NewPassword() As String
        Get
            ' Gets the property value. 
            Return NewPasswordValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            NewPasswordValue = Value
        End Set
    End Property

    Private DeliveryAddressValue As String
    Public Property DeliveryAddress() As String
        Get
            ' Gets the property value. 
            Return DeliveryAddressValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            DeliveryAddressValue = Value
        End Set
    End Property

    Private EmailValue As String
    Public Property Email() As String
        Get
            ' Gets the property value. 
            Return EmailValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            EmailValue = Value
        End Set
    End Property

    Private ContactNumberValue As String
    Public Property ContactNumber() As String
        Get
            ' Gets the property value. 
            Return ContactNumberValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            ContactNumberValue = Value
        End Set
    End Property

    'Male/Female
    Private GenderValue As String
    Public Property Gender() As String
        Get
            ' Gets the property value. 
            Return GenderValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            GenderValue = Value
        End Set
    End Property

    Private CountryValue As String
    Public Property Country() As String
        Get
            ' Gets the property value. 
            Return CountryValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            CountryValue = Value
        End Set
    End Property

    Private BirthdayValue As Date
    Public Property Birthday() As Date
        Get
            ' Gets the property value. 
            Return BirthdayValue
        End Get
        Set(ByVal Value As Date)
            ' Sets the property value.
            BirthdayValue = Value
        End Set
    End Property

    Private FacebookIDValue As String
    Public Property FacebookID() As String
        Get
            ' Gets the property value. 
            Return FacebookIDValue
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            FacebookIDValue = Value
        End Set
    End Property

End Class
