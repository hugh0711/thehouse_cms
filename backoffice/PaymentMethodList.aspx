<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="PaymentMethodList.aspx.vb" Inherits="backoffice_PaymentMethodList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center><h3>付款方式</h3>
</center>
<br />
	<asp:HyperLink ID="HyperLink1" runat="server" Text="返 回" 
		NavigateUrl="~/backoffice/SiteInfo.aspx" CssClass="InnerMenu"></asp:HyperLink>
	<br />
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<asp:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" 
				DataSourceID="dsPaymentMethod" PostBackOnReorder="False" CssClass="reorderList" 
                Width="600px" SortOrderField="Sort">
				<ItemTemplate>
				    <table width="500px">
				        <tr>
				            <td width="100px">
								<asp:Label ID="lblMaterialName" runat="server" Text='<%# Eval("PaymentMethodName") %>'></asp:Label>
					        </td>
					        <td width="100px">
								<asp:Label ID="lblName" runat="server" Text='<%# FindPaymentName(Eval("PaymentMethodId")) %>'></asp:Label>
					        </td>
					        <td width="50px">
								<asp:Button ID="btnEdit" runat="server" Text="編輯" CommandName="btnEdit" CommandArgument='<%# Eval("PaymentMethodId") %>' />
					        </td>
				        </tr>
                    </table>
				</ItemTemplate>
				<ReorderTemplate>
					<asp:Panel ID="Panel1" runat="server" CssClass="reorderItem">
					</asp:Panel>
				</ReorderTemplate>
				<DragHandleTemplate>  
					<div class="reorderhandle">
                    </div>
				</DragHandleTemplate>
				<EmptyListTemplate>
		               No Item Data
				</EmptyListTemplate>

			</asp:ReorderList>
		</ContentTemplate>
	</asp:UpdatePanel>

    <asp:SqlDataSource ID="dsPaymentMethod" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        SelectCommand="SELECT * FROM [PaymentMethod] ORDER BY [Sort]" 
		UpdateCommand="UPDATE [PaymentMethod] SET [PaymentMethodName] = @PaymentMethodName, [PaymentPage] = @PaymentPage, [ImageUrl] = @ImageUrl, [Sort] = @Sort WHERE [PaymentMethodId] = @PaymentMethodId" >
		<UpdateParameters>
			<asp:Parameter Name="PaymentMethodName" Type="String" ConvertEmptyStringToNull="False" />
			<asp:Parameter Name="PaymentPage" Type="String" ConvertEmptyStringToNull="False" />
			<asp:Parameter Name="ImageUrl" Type="String" ConvertEmptyStringToNull="False" />
			<asp:Parameter Name="Sort" Type="Int32" />
			<asp:Parameter Name="PaymentMethodId" Type="Int32" />
		</UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>

