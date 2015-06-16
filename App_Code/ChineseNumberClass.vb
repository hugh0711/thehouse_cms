Imports Microsoft.VisualBasic

Public Class ChineseNumberClass

    Public Shared Function ToChinese(ByVal Value As Integer) As String

    End Function

    Protected Function GetDigit(ByVal Value As Integer) As String
        Dim ret As String = ""
        Select Case Value
            Case 0

            Case 1
                ret = "一"
            Case 2
                ret = "二"
            Case 3
                ret = "三"
            Case 4
                ret = "四"
            Case 5
                ret = "五"
            Case 6
                ret = "六"
            Case 7
                ret = "七"
            Case 8
                ret = "八"
            Case 9
                ret = "九"
        End Select
        Return ret
    End Function

    Protected Function GetMultiply(ByVal Value As Integer) As String
        Dim ret As String = ""
        Dim v As String = CStr(Value)
        Select Case v.Length
            Case 2
                ret = "十"
            Case 3
                ret = "百"
            Case 4
                ret = "千"
            Case 5
                ret = "萬"
        End Select
        Return ret
    End Function
End Class
