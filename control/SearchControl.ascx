<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SearchControl.ascx.vb" Inherits="control_SearchControl" %>
<asp:Panel runat="server" ID="SearchPanel" DefaultButton="btnSearch">
    <asp:TextBox runat="server" ID="txtSearch"></asp:TextBox>
    <asp:Button runat="server" ID="btnSearch" Text="Search" />
</asp:Panel>