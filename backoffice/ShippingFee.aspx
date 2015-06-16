<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="ShippingFee.aspx.vb" Inherits="backoffice_ShippingFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        td
        {
            vertical-align: top;
        }
        
        .popTable th { background-color:#CCCCCC;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:HiddenField ID="hfdProductID" runat="server" />
	<asp:HiddenField ID="hfdShippingID" runat="server" />
	Advance Shipping Fee
	<table class="popTable">
		<tr>
			<th></th>
			<th>Min.</th>
			<th>Max.</th>
			<th></th>
		</tr>
		<tr>
			<td>Quantity</td>
			<td>
				<asp:TextBox ID="txtMinQty" runat="server" ValidationGroup="popup"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvaltxtMinQty" runat="server" 
					ErrorMessage="Information required" Display="Dynamic" 
					ControlToValidate="txtMinQty" ValidationGroup="popup"></asp:RequiredFieldValidator>
				<asp:CompareValidator ID="compvaltxtMinQty" runat="server" 
					ErrorMessage="<br />Information incorrect" ControlToValidate="txtMinQty" 
					Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ValidationGroup="popup"></asp:CompareValidator>
			</td>
			<td>
				<asp:TextBox ID="txtMaxQty" runat="server" ValidationGroup="popup"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvaltxtMaxQty" runat="server" ErrorMessage="Information required" Display="Dynamic" ControlToValidate="txtMaxQty" ValidationGroup="popup"></asp:RequiredFieldValidator>
				<asp:CompareValidator ID="compvaltxtMaxQty" runat="server" 
					ErrorMessage="<br />Information incorrect" ControlToValidate="txtMaxQty" 
					Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ValidationGroup="popup"></asp:CompareValidator>
			</td>
			<td></td>
		</tr>
		<tr>
			<td colspan="4"></td>
		</tr>
		<tr>
			<th></th>
			<th>Local</th>
			<th>OverSeas</th>
			<th>OverSeas (Express)</th>
		</tr>
		<tr>
			<td>Shipping Fee</td>
			<td>
				<asp:TextBox ID="txtLocal" runat="server" ValidationGroup="popup"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvaltxtLocal" runat="server" ErrorMessage="Information required" Display="Dynamic" ControlToValidate="txtLocal" ValidationGroup="popup"></asp:RequiredFieldValidator>
				<asp:CompareValidator ID="compvaltxtLocal" runat="server" 
					ErrorMessage="<br />Information incorrect" ControlToValidate="txtLocal" 
					Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="popup"></asp:CompareValidator>
			</td>
			<td>
				<asp:TextBox ID="txtOverseas" runat="server" ValidationGroup="popup"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvaltxtOverseas" runat="server" ErrorMessage="Information required" Display="Dynamic" ControlToValidate="txtOverseas" ValidationGroup="popup"></asp:RequiredFieldValidator>
				<asp:CompareValidator ID="compvaltxtOverseas" runat="server" 
					ErrorMessage="<br />Information incorrect" ControlToValidate="txtOverseas" 
					Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="popup"></asp:CompareValidator>
			</td>
			<td>
				<asp:TextBox ID="txtOverseasExpress" runat="server" ValidationGroup="popup"></asp:TextBox>
				<asp:RequiredFieldValidator ID="reqvaltxtOverseasExpress" runat="server" ErrorMessage="Information required" Display="Dynamic" ControlToValidate="txtOverseasExpress" ValidationGroup="popup"></asp:RequiredFieldValidator>
				<asp:CompareValidator ID="compvaltxtOverseasExpress" runat="server" 
					ErrorMessage="<br />Information incorrect" ControlToValidate="txtOverseasExpress" 
					Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="popup"></asp:CompareValidator>
			</td>
		</tr>
		<tr>
			<td colspan="4"></td>
		</tr>
		<tr>
			<td></td>
			<td>
				<asp:Button ID="btnShippingSave" runat="server" Text=" Save " ValidationGroup="popup" />
			</td>
			<td>
				<asp:Button ID="btnShippingCancel" runat="server" Text="Close" CausesValidation="False" />
			</td>
			<td></td>
		</tr>
	</table>


</asp:Content>

