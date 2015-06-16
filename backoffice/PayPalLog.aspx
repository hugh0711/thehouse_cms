<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="PayPalLog.aspx.vb" Inherits="backoffice_PayPalLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<style type="text/css">
		td, th { text-align:left; vertical-align:top; }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table border="1">
	<tr>
		<th>PayPalLogId</th>
		<td>
			<asp:Label ID="txtPayPalLogId" runat="server"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>receiver_email</th>
		<td bgcolor="#FFCCFF">
			<asp:Label ID="txtreceiver_email" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>receiver_id</th>
		<td>
			<asp:Label ID="txtreceiver_id" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>residence_country</th>
		<td>
			<asp:Label ID="txtresidence_country" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>transaction_subject</th>
		<td>
			<asp:Label ID="txttransaction_subject" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>txn_id</th>
		<td bgcolor="#FFCCFF">
			<asp:Label ID="txttxn_id" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>txn_type</th>
		<td>
			<asp:Label ID="txttxn_type" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>payer_email</th>
		<td bgcolor="#FFCCFF">
			<asp:Label ID="txtpayer_email" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>payer_id</th>
		<td>
			<asp:Label ID="txtpayer_id" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>payer_status</th>
		<td>
			<asp:Label ID="txtpayer_status" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>first_name</th>
		<td>
			<asp:Label ID="txtfirst_name" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>last_name</th>
		<td>
			<asp:Label ID="txtlast_name" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>address_city</th>
		<td>
			<asp:Label ID="txtaddress_city" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>address_country</th>
		<td>
			<asp:Label ID="txtaddress_country" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>address_country_code</th>
		<td>
			<asp:Label ID="txtaddress_country_code" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>address_name</th>
		<td>
			<asp:Label ID="txtaddress_name" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>address_state</th>
		<td>
			<asp:Label ID="txtaddress_state" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>address_status</th>
		<td>
			<asp:Label ID="txtaddress_status" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>address_street</th>
		<td>
			<asp:Label ID="txtaddress_street" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>address_zip</th>
		<td>
			<asp:Label ID="txtaddress_zip" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>custom</th>
		<td>
			<asp:Label ID="txtcustom" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>handling_amount</th>
		<td>
			<asp:Label ID="txthandling_amount" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>item_name</th>
		<td>
			<asp:Label ID="txtitem_name" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>item_number (Our Order No.)</th>
		<td bgcolor="#FFCCFF">
			<asp:Label ID="txtitem_number" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>mc_currency</th>
		<td bgcolor="#FFCCFF">
			<asp:Label ID="txtmc_currency" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>mc_fee (PayPal Commission)</th>
		<td bgcolor="#FFCCFF">
			<asp:Label ID="txtmc_fee" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th bgcolor="#FFCCFF">mc_gross</th>
		<td bgcolor="#FFCCFF">
			<asp:Label ID="txtmc_gross" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>payment_date</th>
		<td bgcolor="#FFCCFF">
			<asp:Label ID="txtpayment_date" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>payment_fee</th>
		<td>
			<asp:Label ID="txtpayment_fee" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>payment_gross</th>
		<td>
			<asp:Label ID="txtpayment_gross" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>payment_status</th>
		<td bgcolor="#FFCCFF">
			<asp:Label ID="txtpayment_status" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>payment_type</th>
		<td>
			<asp:Label ID="txtpayment_type" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>quantity</th>
		<td>
			<asp:Label ID="txtquantity" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>shipping</th>
		<td>
			<asp:Label ID="txtshipping" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>tax</th>
		<td>
			<asp:Label ID="txttax" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th>verify_sign</th>
		<td>
			<asp:Label ID="txtverify_sign" runat="server" Width="300px"></asp:Label>
		</td>
	</tr>
	<tr>
		<th colspan="3">
			<center>
			<asp:Button ID="btnBack" runat="server" Text="返回" Width="100px" />
			</center>
		</th>
	</tr>
</table>

</asp:Content>

