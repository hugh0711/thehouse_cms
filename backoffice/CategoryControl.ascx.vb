
Partial Class backoffice_CategoryControl
    Inherits System.Web.UI.UserControl

    Public Event SelectedIndexChanged(ByVal CategoyID As Integer)

    Protected Sub ddlLevel1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLevel1.SelectedIndexChanged
        RaiseEvent SelectedIndexChanged(Convert.ToInt32(ddlLevel1.SelectedValue))
        ShowCategory(2)
    End Sub

    Protected Sub ddlLevel2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLevel2.SelectedIndexChanged
        RaiseEvent SelectedIndexChanged(Convert.ToInt32(ddlLevel2.SelectedValue))
        ShowCategory(3)
    End Sub

    Protected Sub ddlLevel3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLevel3.SelectedIndexChanged
        RaiseEvent SelectedIndexChanged(Convert.ToInt32(ddlLevel3.SelectedValue))
    End Sub

    Public Property SelectedValue() As String
        Get
            If PanelLevel3.Visible = True Then
                Return ddlLevel3.SelectedValue
            ElseIf PanelLevel2.Visible = True Then
                Return ddlLevel2.SelectedValue
            Else
                Return ddlLevel1.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            Dim CategoryAdaptor As New CategoryDataSetTableAdapters.CategoryTableAdapter()
            Dim CategoryLevel2, CategoryLevel1 As Integer
            CategoryLevel2 = CategoryAdaptor.GetParentID(Convert.ToInt32(value)).GetValueOrDefault(-1)
            If CategoryLevel2 = 0 Then
                Bind(1)
                ddlLevel1.SelectedValue = value
                ShowCategory(2)
                Exit Property
            End If
            If CategoryLevel2 = -1 Then
                ShowCategory(1)
                Exit Property
            End If
            CategoryLevel1 = CategoryAdaptor.GetParentID(CategoryLevel2).GetValueOrDefault(-1)
            If CategoryLevel1 = 0 Then
                Bind(1)
                ddlLevel1.SelectedValue = CategoryLevel2
                Bind(2)
                ddlLevel2.SelectedValue = value
                ShowCategory(3)
                Exit Property
            End If

            Bind(1)
            ddlLevel1.SelectedValue = CategoryLevel1

            Bind(2)
            ddlLevel2.SelectedValue = CategoryLevel2

            Bind(3)
            ddlLevel3.SelectedValue = value
        End Set
    End Property

    Public Sub ShowCategory(ByVal Level As Integer)
        Select Case Level
            Case 1
                PanelLevel2.Visible = False
                PanelLevel3.Visible = False
                Bind(1)
            Case 2
                PanelLevel2.Visible = True
                PanelLevel3.Visible = False
                Bind(2)
            Case 3
                PanelLevel2.Visible = True
                PanelLevel3.Visible = True
                Bind(3)
        End Select
    End Sub

    Protected Sub Bind(ByVal Level As Integer)
        Select Case Level
            Case 1
                SqlDataSourceLevel1.DataBind()
                With ddlLevel1
                    .Items.Clear()
                    .Items.Add(New ListItem("-- 請選擇類別 --", ""))
                    .DataBind()
                End With
            Case 2
                SqlDataSourceLevel2.DataBind()
                With ddlLevel2
                    .Items.Clear()
                    .Items.Add(New ListItem("-- 請選擇類別 --", ""))
                    .DataBind()
                End With
            Case 3
                SqlDataSourceLevel3.DataBind()
                With ddlLevel3
                    .Items.Clear()
                    .Items.Add(New ListItem("-- 請選擇類別 --", ""))
                    .DataBind()
                End With
        End Select
    End Sub
End Class
