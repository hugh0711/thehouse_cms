<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CalendarControl.ascx.vb" Inherits="control_CalendarControl"  %>

<asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
<ContentTemplate>
    <asp:Table ID="Table1" runat="server" Width="100%" CssClass="calendar" CellSpacing="0">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell Width="33"><asp:ImageButton runat="server" ID="btnPrev" ImageUrl="~/images/cal_prev.gif" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="CalendarCaption" ColumnSpan="7"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="33"><asp:ImageButton runat="server" ID="btnNext" ImageUrl="~/images/cal_next.gif" /></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <asp:Literal runat="server" ID="ltrEventList"></asp:Literal>
    <div id="event-panel" style="display:none;"></div>
</ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function showPanel(e, sender) {
        if (e != '') {
            $("#event-panel").html($("#" + e).html()).fadeIn();
        }
        else {
            $("#event-panel").fadeOut("fast").html("");
        };
        $(".calendar tr").removeClass("highlight");
        $(sender).addClass("highlight");
    }
</script>