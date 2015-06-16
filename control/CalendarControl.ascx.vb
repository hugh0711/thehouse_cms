
Partial Class control_CalendarControl
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            RenderTable(Today)
        End If
    End Sub

    Protected Sub RenderTable(CurrentMonth As Date)
        Dim tr As TableRow
        Dim td As TableCell

        btnPrev.CommandArgument = CurrentMonth.AddMonths(-1).Ticks

        Dim Month As String() = New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
        CalendarCaption.Text = String.Format("{1} {0}", Now.Year, Month(CurrentMonth.Month - 1))

        btnNext.CommandArgument = CurrentMonth.AddMonths(1).Ticks


        ' Add Calendar Day Of Week
        Dim DayOfWeek As String() = New String() {"S", "M", "T", "W", "T", "F", "S"}
        tr = New TableRow()
        td = New TableCell()
        tr.Cells.Add(td)
        For i As Integer = 0 To DayOfWeek.Length - 1
            td = New TableCell()
            td.Text = DayOfWeek(i)
            td.CssClass = "dayofweek"
            If i = 0 Then
                td.CssClass &= " weekend"
            Else
                td.CssClass &= " normal"
            End If
            tr.Cells.Add(td)
        Next
        td = New TableCell()
        tr.Cells.Add(td)
        Table1.Rows.Add(tr)

        ' Add Calendar Day
        Dim DayDiff As DayOfWeek
        Dim CurrentDate As Date

        CurrentDate = New Date(CurrentMonth.Year, CurrentMonth.Month, 1)
        DayDiff = CurrentDate.DayOfWeek * -1
        CurrentDate = DateAdd(DateInterval.Day, DayDiff, CurrentDate)
        Dim Column As Integer = 0

        Dim da As New PromoDataSetTableAdapters.view_PromoTableAdapter()
        Dim PromoSettingID As Integer = CInt(ConfigurationManager.AppSettings("CalendarPromoID"))
        Dim dt As PromoDataSet.view_PromoDataTable = da.GetDataByYearMonth(PromoSettingID, Session("MyCulture"), CurrentMonth.Year, CurrentMonth.Month)
        'Dim dr As PromoDataSet.view_PromoRow

        ltrEventList.Text = ""
        Dim Events As List(Of String)
        Dim PanelID As String = ""

        Do
            If Column = 0 Then
                tr = New TableRow
                td = New TableCell()
                tr.Cells.Add(td)

                Dim elist = From dr In dt Where CurrentDate <= dr.StartDate And dr.StartDate < CurrentDate.AddDays(7) Select dr.StartDate, dr.UnitFunctionID, dr.PromoUrl, dr.PromoName

                Events = New List(Of String)
                For Each e In elist
                    Events.Add(String.Format("<tr><td>{2:M月d日}</td><td><a href='{0}'>{1}</a></td></tr>", ResolveUrl(Utility.GetNavigateUrl(e.UnitFunctionID, e.PromoUrl)), e.PromoName, e.StartDate))
                Next

                If Events.Count > 0 Then
                    PanelID = String.Format("day-{0}", CurrentDate.Ticks)
                    ltrEventList.Text &= String.Format("<div id='{0}' style='display:none;'><table border='0'>{1}</table></div>", PanelID, Join(Events.ToArray(), ""))
                Else
                    PanelID = ""
                End If
                tr.Attributes("onclick") = String.Format("showPanel('{0}', this)", PanelID)
            End If

            tr.Cells.Add(DayCell(CurrentDate, CurrentMonth, dt))

            Column += 1
            CurrentDate = DateAdd(DateInterval.Day, 1, CurrentDate)
            If Column = 7 Then
                td = New TableCell()
                tr.Cells.Add(td)
                Table1.Rows.Add(tr)
                Column = 0
            End If
        Loop While (CurrentMonth.Year = CurrentDate.Year And CurrentMonth.Month = CurrentDate.Month) Or (Column > 0)

    End Sub

    Protected Function DayCell(CurrentDate As Date, CurrentMonth As Date, dt As PromoDataSet.view_PromoDataTable) As TableCell
        Dim td As New TableCell
        With td
            .Text = CurrentDate.Day
            If CurrentDate.DayOfWeek <> DayOfWeek.Sunday Then
                .CssClass &= " normal"
            End If

            Dim Events As New List(Of String)

            For Each dr As PromoDataSet.view_PromoRow In dt.Rows

                If dr.StartDate = CurrentDate.Date Then
                    .CssClass &= " highlight"
                    'Events.Add(String.Format("<a href='{0}'>{1}</a>", ResolveUrl(Utility.GetNavigateUrl(dr.UnitFunctionID, dr.PromoUrl)), dr.PromoName))
                End If
            Next

            'If Events.Count > 0 Then
            '    Dim Literal1 As New Literal
            '    Dim PanelID As String = String.Format("day-{0}", CurrentDate.Ticks)
            '    Literal1.Text = String.Format("<div id='{0}' style='display:none;'><ul><li>{1}</li></ul></div>", PanelID, Join(Events.ToArray(), "</li><li>"))
            '    td.Controls.Add(Literal1)

            '    Dim Link = New HyperLink
            '    'Link.NavigateUrl = "#"
            '    Link.Attributes("onclick") = String.Format("showPanel('{0}')", PanelID)
            '    Link.Text = CurrentDate.Day
            '    td.Controls.Add(Link)
            'End If

            If CurrentDate.Date = Today.Date Then
                .CssClass &= " today"
            End If

            If CurrentDate.Month <> CurrentMonth.Month Then
                .CssClass &= " inactive"
            End If
        End With

        Return td
    End Function

    Protected Sub RefreshMonth_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNext.Click, btnPrev.Click
        Do While Table1.Rows.Count > 2
            Table1.Rows.RemoveAt(1)
        Loop
        Dim b As ImageButton = CType(sender, ImageButton)
        RenderTable(New Date(CLng(b.CommandArgument)))
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

    End Sub
End Class
