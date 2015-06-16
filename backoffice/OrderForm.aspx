<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="OrderForm.aspx.vb" Inherits="backoffice_OrderForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:HiddenField ID="hfdOrderId" runat="server" />
    <table cellpadding="5" cellspacing="0" border="1">
		<tr>
			<th align="left">訂單編號
			</th>
			<td align="left" width="300px">
				<asp:Label ID="txtOrderNumber" runat="server" Text=""></asp:Label>
			</td>
            <th align="left">訂單日期
            </th>
            <td align="left">
                <asp:Label ID="txtOrderDate" runat="server" Text=""></asp:Label>
            </td>
		</tr>
        <tr>
            <th align="left">客戶名稱
            </th>
            <td align="left" colspan="3">
                <asp:Label ID="txtCustomerName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="left">聯絡電話
            </th>
            <td align="left">
                <asp:Label ID="txtContactPhone" runat="server" Text=""></asp:Label>
            </td>
            <th align="left">Email
            </th>
            <td align="left">
                <asp:Label ID="txtEmail" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="left">送貨地址
            </th>
            <td align="left" colspan="3">
                <asp:Label ID="txtDeliveryAddress" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="left">送貨國家
            </th>
            <td align="left" colspan="3">
                <asp:Label ID="txtCountry" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="left">備註
            </th>
            <td align="left" colspan="3">
                <asp:Label ID="txtRemark" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="left">總額
            </th>
            <td align="left">
                <asp:Label ID="txtTotalAmount" runat="server" Text=""></asp:Label>
            </td>
            <th align="left">訂單狀態
            </th>
            <td align="left">
				<asp:DropDownList ID="ddlOrderStatus" runat="server" 
					DataSourceID="dsOrderStatus" 
					DataTextField="Description" DataValueField="OrderStatusCode">
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
			<th align="left" valign="top">付款方法 <br /><br />付款編號
			</th>
			<td align="left" valign="top">
				<asp:HiddenField ID="hfdPaymentMethodId" runat="server" />
				<asp:Label ID="txtPaymentMthod" runat="server" Text=""></asp:Label>
				<br /><br />
				<asp:TextBox ID="txtTransactionRefNo" runat="server" Width="200px"></asp:TextBox>
				<asp:Button ID="btnTransactionDetail" runat="server" Text="詳情" />
			</td>
			<th align="left" valign="top">付款日期<br />(YYYY-MM-DD hh:mm)
			</th>
			<td align="left" valign="top">
				<asp:TextBox ID="txtPaidDate" runat="server" Width="120px"></asp:TextBox>
				&nbsp;&nbsp;
				<asp:Label ID="txtPayPalStatus" runat="server" Text=""></asp:Label>
			</td>
        </tr>
        <tr>
			<th>最近更新用戶</th>
			<td>
				<asp:Label ID="txtLastUpdateUser" runat="server" Text=""></asp:Label>
			</td>
			<th>最近更新日期</th>
			<td>
				<asp:Label ID="txtLastUpdateTime" runat="server" Text=""></asp:Label>
			</td>
        </tr>
        <tr>
            <td colspan="4">
				<div style="float: left">
					<asp:Button ID="btnSave" runat="server" Text="儲存" BackColor="#66CCFF" Width="100px" />
				</div>
				<div style="float: right">
					<asp:Button ID="btnBack" runat="server" Text="返回" Width="100px" />
				</div>
            </td>
        </tr>
    </table>
	<br />
	<asp:GridView ID="gvOrderItem" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="OrderItemId" DataSourceID="dsOrderItem" Width="100%">
		<Columns>
			<asp:BoundField DataField="OrderItemId" HeaderText="OrderItemId" 
				InsertVisible="False" ReadOnly="True" SortExpression="OrderItemId" 
				Visible="False" />
			<asp:BoundField DataField="OrderNumber" HeaderText="OrderNumber" 
				SortExpression="OrderNumber" Visible="False" />
			<asp:BoundField DataField="OrderProductId" HeaderText="OrderProductId" 
				SortExpression="OrderProductId" Visible="False" />
			<asp:BoundField DataField="OrderProductCode" HeaderText="產品編號" 
				SortExpression="OrderProductCode" Visible="false" >
			<ItemStyle VerticalAlign="Top" />
			</asp:BoundField>
			<asp:BoundField DataField="OrderProductName" HeaderText="名稱" 
				SortExpression="OrderProductName" >
			<ItemStyle VerticalAlign="Top" />
			</asp:BoundField>
			<asp:BoundField DataField="OrderQuantity" HeaderText="數量" 
				SortExpression="OrderQuantity" >
			<ItemStyle VerticalAlign="Top" />
			</asp:BoundField>
			<asp:BoundField DataField="OrderPrice" DataFormatString="{0:n2}" 
				HeaderText="金額" SortExpression="OrderPrice" >
			<ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:BoundField DataField="OrderShippingFee" DataFormatString="{0:n2}" 
				HeaderText="運送費用" SortExpression="OrderShippingFee" >
			<ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:TemplateField>
				<ItemTemplate>
					<asp:Label ID="Label1" runat="server" Text='<%# Eval("OrderProductName") %>' style="clear:both; float:left;"></asp:Label>
					<asp:Image ID="Image1" runat="server" ImageUrl='<%# DisplayProductImage(Eval("OrderProductId")) %>' Width="50px" style="clear:both; float:left;" />
				</ItemTemplate>
				<ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
			</asp:TemplateField>
		</Columns>
		<FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="#FFFFFF" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
	</asp:GridView>
	<asp:SqlDataSource ID="dsOrderItem" runat="server" 
		ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
		SelectCommand="SELECT * FROM [OrderItem] WHERE ([OrderNumber] = @OrderNumber)">
		<SelectParameters>
			<asp:ControlParameter ControlID="txtOrderNumber" Name="OrderNumber" 
				PropertyName="Text" Type="String" />
		</SelectParameters>
	</asp:SqlDataSource>

</asp:Content>

