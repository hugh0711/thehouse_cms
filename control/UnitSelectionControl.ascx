<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UnitSelectionControl.ascx.vb" Inherits="control_UnitSelectionControl" %>
<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
        <%--類別: --%>
        <asp:PlaceHolder runat="server" ID="PlaceHolder1"></asp:PlaceHolder>    
        <br />
        <asp:Label runat="server" ID="lblUnit">單元</asp:Label>:
        <asp:DropDownList runat="server" ID="ddlUnit" CssClass="textEntry"></asp:DropDownList>
    </ContentTemplate>
</asp:UpdatePanel>