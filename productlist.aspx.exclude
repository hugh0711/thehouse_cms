<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="false" CodeFile="productlist.aspx.vb" Inherits="productlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:HiddenField ID="hfdCategoryID" runat="server" />
	
	<div style="float:left; padding-top:10px;">
	<div class="service">
		<div class="netvigator">
			<asp:Image ID="Image1" runat="server" ImageUrl="~/images/servicemenutopbg.png" />
			<h2>
				<asp:Localize ID="locMenuTitle" runat="server" Text=" 產品分類"></asp:Localize>
			</h2>
			<br /><br />

			<asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
				<ItemTemplate>
					<li>
						<asp:HiddenField ID="hfdCategoryID" runat="server" Value='<%# Eval("CategoryID") %>' />
						<asp:HyperLink ID="lnkCategory" runat="server" Text='<%# Eval("CategoryName") %>' NavigateUrl='<%# String.Format("~/productlist.aspx?CategoryID={0}", Eval("CategoryID")) %>'></asp:HyperLink>
					</li>
				</ItemTemplate>
				<EmptyDataTemplate>
				</EmptyDataTemplate>
				<LayoutTemplate>
					<ul ID="itemPlaceholderContainer" runat="server" style="">
						<li ID="itemPlaceholder" runat="server" />
					</ul>
					</LayoutTemplate>
					<ItemSeparatorTemplate>
					</ItemSeparatorTemplate>
			</asp:ListView>
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
					ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
					SelectCommand="SELECT [CategoryName], [CategoryID] FROM [view_Category] WHERE (([Lang] = @Lang) AND ([FunctionID] = @FunctionID) AND ([Enabled] = 1)) ORDER BY [SortOrder]">
				<SelectParameters>
					<asp:SessionParameter Name="Lang" SessionField="MyCulture" Type="String" />
					<asp:Parameter DefaultValue="2" Name="FunctionID" Type="Int32" />
				</SelectParameters>
				</asp:SqlDataSource>

			<asp:Image ID="Image2" runat="server" ImageUrl="~/images/servicemenubottombg.png" />
		</div>
		
		<div class="productwrapper">
			<asp:ListView ID="lstProductList" runat="server" DataKeyNames="ProductID" 
				DataSourceID="dsProductList">
				<ItemTemplate>
					<li>
						<asp:HyperLink ID="HyperLink1" runat="server" 
							 NavigateUrl='<%# String.Format("~/products.aspx?CategoryID={0}&ProductID={1}", Eval("CategoryID"), Eval("ProductID")) %>'>
							<asp:Image ID="Image3" runat="server" ImageUrl='<%# ProductClass.GetImageThumbnail(Eval("ProductID")) %>' CssClass="pic" Height="100px" />
							<br /><br />
							<asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' CssClass="price"></asp:Label>
							<br />
							<asp:Label ID="lblPrice" runat="server" Text='<%# String.Format("Price : {0}", Utility.AmountToString(Eval("SellingPrice"))) %>' CssClass="price" Font-Strikeout='<%# (Eval("DiscountPrice") > 0) %>'></asp:Label>
							<br />
							<asp:Label ID="lblDiscount" runat="server" Text='<%# String.Format("Discount : {0}", Utility.AmountToString(Eval("DiscountPrice"))) %>' CssClass="price" Visible='<%# (Eval("DiscountPrice") > 0) %>'></asp:Label>
						</asp:HyperLink>
					</li>
				</ItemTemplate>
				<EmptyDataTemplate>
				</EmptyDataTemplate>
				<LayoutTemplate>
					<ul ID="itemPlaceholderContainer" runat="server" class="productlist">
						<li ID="itemPlaceholder" runat="server" />
					</ul>
					<div style=" clear:both; text-align:center; padding-top:40px;">
						<asp:DataPager ID="DataPager1" runat="server" PageSize="9">
							<Fields>
								<asp:NextPreviousPagerField ButtonType="Image" ShowFirstPageButton="True" 
									ShowNextPageButton="False" ShowPreviousPageButton="False" FirstPageImageUrl="~/images/pagefirst.png" />
								<asp:NumericPagerField />
								<asp:NextPreviousPagerField ButtonType="Image" ShowLastPageButton="True" 
									ShowNextPageButton="False" ShowPreviousPageButton="False" LastPageImageUrl="~/images/pagelast.png" />
							</Fields>
						</asp:DataPager>
					</div>
				</LayoutTemplate>
				<ItemSeparatorTemplate>
				</ItemSeparatorTemplate>
			</asp:ListView>
			<asp:SqlDataSource ID="dsProductList" runat="server" 
					ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
					SelectCommand="SELECT [CategoryID], [ProductID], [CreateDate], [ProductName], [SellingPrice], [DiscountPrice] FROM [view_Product] WHERE (([Lang] = @Lang) AND ([Enabled] = @Enabled) AND ([CategoryID]=@CategoryID) AND [FunctionID]=2) ORDER BY [SortOrder]">
				<SelectParameters>
					<asp:SessionParameter Name="Lang" SessionField="MyCulture" Type="String" />
					<asp:Parameter DefaultValue="True" Name="Enabled" Type="Boolean" />
					<asp:ControlParameter ControlID="hfdCategoryID" PropertyName="Value" Name="CategoryID" DbType="Int32" />
				</SelectParameters>
				</asp:SqlDataSource>
		</div>
	</div>
</div>
</asp:Content>