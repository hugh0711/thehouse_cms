Imports Microsoft.VisualBasic

Public Class TagClass

    Protected Shared EncryptionKey As Integer = 4
    Protected Shared EncryptionLength As Integer = 10

    Public Shared Function EncryptID(ByVal TagID As Integer) As String
        Dim Ret As String = Utility.GenerateRandomString(EncryptionLength)
        Ret = String.Format("{0}{1}{2}", Ret.Substring(0, EncryptionKey), TagID, Ret.Substring(EncryptionKey))
        Return Ret
    End Function

    Public Shared Function DecryptID(ByVal Tag As String) As Integer
        Dim Value As String = Tag
        Value = Value.Substring(EncryptionKey)
        Value = Value.Substring(0, Value.Length - (EncryptionLength - EncryptionKey))
        Return CInt(Value)
    End Function

End Class
