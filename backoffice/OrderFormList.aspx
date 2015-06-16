<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="OrderFormList.aspx.vb" Inherits="backoffice_OrderFormList" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<style type="text/css">
		td, th { text-align:left; vertical-align:top; }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<table>
		<tr>
			<th>Order Number</th>
			<td>
				<asp:TextBox ID="txtOrderNumber" runat="server"></asp:TextBox>
			</td>
			<th>Date</th>
			<td colspan="3" valign="middle">
				<asp:TextBox ID="txtOrderDateFrom" runat="server"></asp:TextBox>
				<asp:CalendarExtender ID="txtOrderDateFrom_CalendarExtender" runat="server" 
					Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtOrderDateFrom" 
					TodaysDateFormat="yyyy-MM-dd">
				</asp:CalendarExtender>
				To
				<asp:TextBox ID="txtOrderDateTo" runat="server"></asp:TextBox>
				<asp:CalendarExtender ID="txtOrderDateTo_CalendarExtender" runat="server" 
					Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtOrderDateTo" 
					TodaysDateFormat="yyyy-MM-dd">
				</asp:CalendarExtender>
			</td>
		</tr>
		<tr>
			<th>Customer</th>
			<td>
				<asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
			</td>
			<th>Phone</th>
			<td>
				<asp:TextBox ID="txtContactPhone" runat="server"></asp:TextBox>
			</td>
			<th>Status</th>
			<td>
				<asp:DropDownList ID="ddlOrderStatus" runat="server" 
					AppendDataBoundItems="True" DataSourceID="dsOrderStatus" 
					DataTextField="Description" DataValueField="OrderStatusCode">
					<asp:ListItem Text="" Value=""></asp:ListItem>
				</asp:DropDownList>
				<asp:SqlDataSource ID="dsOrderStatus" runat="server" 
					ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
					SelectCommand="SELECT * FROM [view_OrderStatus] WHERE ([Lang] = @Lang) ORDER BY [Sort]">
					<SelectParameters>
						<asp:Parameter DefaultValue="zh-hk" Name="Lang" Type="String" />
					</SelectParameters>
				</asp:SqlDataSource>
			</td>
		</tr>
		<tr>
			<td colspan="4"></td>
			<td>
				<asp:Button ID="btnSearch" runat="server" Text="Search" />
			</td>
			<td>
				<asp:Button ID="btnClear" runat="server" Text="Clear" />
			</td>
		</tr>
	</table>
	<br />
    <asp:GridView ID="gvOrderForm" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" 
        DataSourceID="dsOrderForm" BackColor="White" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
        GridLines="Vertical">
        <Columns>
            <asp:BoundField DataField="OrderID" HeaderText="OrderID" InsertVisible="False" 
                ReadOnly="True" SortExpression="OrderID" Visible="False" />
            <asp:BoundField DataField="OrderNumber" HeaderText="訂單編號" 
                SortExpression="OrderNumber" />
            <asp:BoundField DataField="OrderDate" HeaderText="訂單日期" 
                SortExpression="OrderDate" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:BoundField DataField="CustomerName" HeaderText="訂購者" 
                SortExpression="CustomerName" />
            <asp:BoundField DataField="ContactPhone" HeaderText="電話" 
                SortExpression="ContactPhone" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:TemplateField HeaderText="付款方法" SortExpression="PaymentMethod">
				<ItemTemplate>
					<asp:Label ID="Label1" runat="server" Text='<%# DisplayPaymentMethod(Eval("PaymentMethod")) %>'></asp:Label>
				</ItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PaymentMethod") %>'></asp:TextBox>
				</EditItemTemplate>
			</asp:TemplateField>
            <asp:BoundField DataField="TransactionRefNo" HeaderText="付款編號" />
            <asp:BoundField DataField="Description" HeaderText="狀態" 
                SortExpression="Description" />
            <asp:BoundField DataField="TotalAmount" HeaderText="總金額" 
                SortExpression="TotalAmount" DataFormatString="{0:n2}" />
        	<asp:TemplateField>
				<ItemTemplate>
					<asp:Button ID="btnDetail" runat="server" Text="Detail" CommandName="btnDetail" CommandArgument='<%# Eval("OrderNumber") %>' />
				</ItemTemplate>
			</asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" HorizontalAlign="Right" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="#FFFFFF" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <asp:SqlDataSource ID="dsOrderForm" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        SelectCommand=""></asp:SqlDataSource>
</asp:Content>