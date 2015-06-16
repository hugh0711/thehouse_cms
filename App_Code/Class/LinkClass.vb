Imports Microsoft.VisualBasic

Public Class LinkClass

    Public Enum Options
        Unit = 0
        Category = 1
        Tag = 2
        Url = 3
        None = 4
    End Enum

    Protected Shared CategoryPrefix As String = "category://"
    Protected Shared TagPrefix As String = "tag://"
    Protected Shared UnitPrefix As String = "unit://"
    Protected Shared UrlPrefix As String = ""


    Public Property LinkOption As Options
    Public Property LinkValue As String

    Public Sub New()
        LinkOption = Options.None
        LinkValue = ""
    End Sub

    Public Shared Function GetUrl(obj As LinkClass) As String
        Dim Url As String = ""

        Select Case obj.LinkOption
            Case Options.Category
                Url = String.Format("~/category.aspx?id={0}", obj.LinkValue)
            Case Options.Tag
                Url = String.Format("~/tag.aspx?id={0}", obj.LinkValue)
            Case Options.Unit
                Url = String.Format("~/product.aspx?id={0}", obj.LinkValue)
            Case Options.Url
                Url = obj.LinkValue
        End Select

        Return Url
    End Function

    Public Shared Function GetFromDBValue(Value As String) As LinkClass
        Dim obj As New LinkClass

        If Value.StartsWith(CategoryPrefix) Then
            If Value.Length > CategoryPrefix.Length Then
                obj.LinkOption = Options.Category
                obj.LinkValue = Value.Substring(CategoryPrefix.Length)
            End If

        ElseIf Value.StartsWith(TagPrefix) Then
            If Value.Length > TagPrefix.Length Then
                obj.LinkOption = Options.Tag
                obj.LinkValue = Value.Substring(TagPrefix.Length)
            End If

        ElseIf Value.StartsWith(UnitPrefix) Then
            If Value.Length > UnitPrefix.Length Then
                obj.LinkOption = Options.Unit
                obj.LinkValue = Value.Substring(UnitPrefix.Length)
            End If

        ElseIf Value <> "" Then
            obj.LinkOption = Options.Url
            obj.LinkValue = Value

        End If


        Return obj
    End Function

    Public Shared Function GetDBValue(obj As LinkClass) As String
        Dim Value As String = ""

        Select Case obj.LinkOption
            Case Options.Category
                Value = CategoryPrefix & obj.LinkValue
            Case Options.Tag
                Value = TagPrefix & obj.LinkValue
            Case Options.Unit
                Value = UnitPrefix & obj.LinkValue
            Case Options.Url
                Value = obj.LinkValue
        End Select

        Return Value
    End Function


    Public Shared Function GetDBValue(LinkOption As Options, Optional Value As String = "") As String
        Dim obj As New LinkClass With {.LinkOption = LinkOption, .LinkValue = Value}
        Return GetDBValue(obj)
    End Function



End Class
