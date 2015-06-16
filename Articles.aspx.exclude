<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="false" CodeFile="Articles.aspx.vb" Inherits="Articles" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:HiddenField ID="hfdProductID" runat="server" />
	<asp:HiddenField ID="hfdCategoryID" runat="server" />
	<asp:HiddenField ID="hfdFunctionID" runat="server" />
	
	<div style="float:left; padding-top:10px;">
	<div class="service">
		<div class="netvigator">
			<asp:Image ID="Image1" runat="server" ImageUrl="~/images/servicemenutopbg.png" />
			<h2>
				<asp:Localize ID="locMenuTitle" runat="server" Text="知識庫"></asp:Localize>
			</h2>
			<br /><br />

			<asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
				<ItemTemplate>
					<li class='<%# IsCategorySelect(Eval("CategoryID")) %>'>
						<asp:HiddenField ID="hfdCategoryID" runat="server" Value='<%# Eval("CategoryID") %>' />
						<asp:HyperLink ID="lnkCategory" runat="server" Text='<%# Eval("CategoryName") %>' NavigateUrl='<%# String.Format("~/articles.aspx?CategoryID={0}", Eval("CategoryID")) %>'></asp:HyperLink>

						<asp:ListView ID="lstProduct" runat="server" DataSourceID="dsProduct" Visible='<%# IsDisplayProduct(Eval("CategoryID")) %>'>
							<ItemTemplate>
								<li style="">
									<asp:HyperLink ID="lnkProduct" runat="server" Text='<%# Eval("ProductName") %>' NavigateUrl='<%# String.Format("~/articles.aspx?CategoryID={0}&ProductID={1}", Eval("CategoryID"), Eval("ProductID")) %>'></asp:HyperLink>
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
						<asp:SqlDataSource ID="dsProduct" runat="server" 
								ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
								SelectCommand="SELECT * FROM [view_Product] WHERE (([Lang] = @Lang) AND ([FunctionID] = @FunctionID) AND ([Enabled] = 1) AND ([CategoryID] = @CategoryID)) AND Enabled = 1 ORDER BY [SortOrder]">
							<SelectParameters>
								<asp:SessionParameter Name="Lang" SessionField="MyCulture" Type="String" />
								<asp:ControlParameter ControlID="hfdFunctionID" PropertyName="Value" Name="FunctionID" Type="Int32" />
								<asp:ControlParameter ControlID="hfdCategoryID" PropertyName="Value" Name="CategoryID" Type="Int32" />
							</SelectParameters>
							</asp:SqlDataSource>

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
					<asp:ControlParameter ControlID="hfdFunctionID" PropertyName="Value" Name="FunctionID" Type="Int32" />
				</SelectParameters>
				</asp:SqlDataSource>

			<asp:Image ID="Image2" runat="server" ImageUrl="~/images/servicemenubottombg.png" />
		</div>
		
		<div class="productwrapper article">
		    <h1><asp:Literal runat="server" ID="lblTitle"></asp:Literal></h1>
		    <asp:Label ID="lblDate" runat="server" CssClass="date"></asp:Label>
		    <br />
		    <br />
		    <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
				
				<asp:UpdatePanel runat="server" ID="UpdatePanel1">
				    <ContentTemplate>
				<div class="comment">
					<h2 style="padding-bottom:10px;">
						<asp:Localize ID="locComment" runat="server" Text="評論"></asp:Localize>
					</h2>
					<br />
					<asp:Panel ID="pnlWriteComment" runat="server" Visible="true">
						<div style="width: 100%; padding: 0px 0px 0px 0px; margin: 0px; float: left;">
							<asp:TextBox ID="txtWriteComment" runat="server" Width="360px" TextMode="MultiLine" Rows="6"></asp:TextBox>
							<div style="padding-top: 10px; padding-right: 10px; margin: 0px">
								<asp:Button ID="btnPostComment" runat="server" Text="發表" BorderStyle="Solid" 
								BorderColor="#E0E0E0" BorderWidth="1px" BackColor="#CCCCCC" ForeColor="White" Width="80px" />
							</div>
						</div>
					</asp:Panel>
					<br />
					<asp:ListView ID="lstComment" runat="server" DataKeyNames="CommentID" DataSourceID="dsComment">
						<ItemTemplate>
							<li>
								<asp:Label ID="lblUser" runat="server" Text='<%# DisplayUserName(Eval("UserID")) %>' CssClass="user" ></asp:Label>
								<asp:Label ID="lblAgo" runat="server" Text='<%# Utility.DateTimeToString(Eval("CommentDate"), True) %>' CssClass="date"></asp:Label>
								
								<div class="description">
									<asp:Literal ID="txtCommentDescription" runat="server" Text='<%# Replace(Eval("CommentDescription"), VbCrLf, "<br />") %>'> </asp:Literal>
								</div>
							</li>
						</ItemTemplate>
						<EmptyDataTemplate>
						</EmptyDataTemplate>
						<LayoutTemplate>
							<ul ID="itemPlaceholderContainer" runat="server">
								<li ID="itemPlaceholder" runat="server" />
							</ul>
						</LayoutTemplate>
						<ItemSeparatorTemplate>
						</ItemSeparatorTemplate>
					</asp:ListView>
					<asp:SqlDataSource ID="dsComment" runat="server" ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
						SelectCommand="SELECT * FROM [Comment] WHERE (([ReferenceID] = @ReferenceID) AND ([CommentType] = @CommentType) AND [IsDisable] = 0) ORDER BY [CommentDate] DESC, [CommentID] DESC">
						<SelectParameters>
							<asp:ControlParameter ControlID="hfdProductID" PropertyName="Value" Name="ReferenceID" Type="Int32" />
							<asp:Parameter DefaultValue="PRODUCT" Name="CommentType" Type="String" />
						</SelectParameters>
					</asp:SqlDataSource>
				</div>
					</ContentTemplate>
				</asp:UpdatePanel>
                <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" ControlToOverlayID="UpdatePanel1" CssClass="updateProgress" TargetControlID="UpdateProgress1" />
                
			</div>
				
				
				
				
				
			<%--<div class="productinfo">
				<asp:Image ID="Image5" runat="server" ImageUrl="~/images/productinfotop.png" />
				<div class="descwrapper">
					<h2 style="padding-bottom:10px;">
						<asp:Localize ID="locProductInformation" runat="server" Text="產品資料"></asp:Localize>
					</h2>
					<asp:Literal ID="ltrProductInformation" runat="server"></asp:Literal>
				</div>
				<asp:Image ID="Image6" runat="server" ImageUrl="~/images/productinfobottom.png" />
				<br /><br />
				<table border="0" cellpadding="0" cellspacing="0" >
					<tr>
						<th width="170px" style="padding-bottom:10px; float:left;">
							<asp:Localize ID="locPrice" runat="server" Text="價格"></asp:Localize>
						</th>
						<td width="120px" style="padding-bottom:10px;  text-align:right; padding-right:10px;">
							<asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
						</td>
					</tr>
					<tr>
						<th style="padding-bottom:10px; float:left;">
							<asp:Localize ID="locDiscountPrice" runat="server" Text="折扣優惠"></asp:Localize>
						</th>
						<td style="padding-bottom:10px;  text-align:right; padding-right:10px;">
							<asp:Label ID="lblDiscountedPrice" runat="server" Text=""></asp:Label>
						</td>
					</tr>
					<tr>
						<th style="padding-bottom:10px; float:left;" colspan="2">
							<asp:Localize ID="locShippingFee" runat="server" Text="* 不包括送貨費和手續費"></asp:Localize>
						</th>
					</tr>
					<%--<tr>
						<th style="padding-bottom:10px; float:left;">
							<asp:Localize ID="locShippingLocal" runat="server" Text="香港"></asp:Localize>
						</th>
						<td style="padding-bottom:10px;  text-align:right; padding-right:10px;">
							<asp:Label ID="lblShippingLocal" runat="server" Text=""></asp:Label>
						</td>
					</tr>
					<tr>
						<th style="padding-bottom:10px; float:left;">
							<asp:Localize ID="locShippingOverseas" runat="server" Text="海外"></asp:Localize>
						</th>
						<td style="padding-bottom:10px;  text-align:right; padding-right:10px;">
							<asp:Label ID="lblShippingOverseas" runat="server" Text=""></asp:Label>
						</td>
					</tr>
					<tr>
						<th style="padding-bottom:10px; float:left;">
							<asp:Localize ID="locShippingExpress" runat="server" Text="海外速遞"></asp:Localize>
						</th>
						<td style="padding-bottom:10px;  text-align:right; padding-right:10px;">
							<asp:Label ID="lblShippingExpress" runat="server" Text=""></asp:Label>
						</td>
					</tr>
					<tr>
						<th style="padding-bottom:10px; float:left;">
							<asp:Localize ID="locQuantity" runat="server" Text="數量"></asp:Localize>
						</th>
						<td style="padding-bottom:10px;  text-align:right; padding-right:10px;">
							<asp:TextBox ID="txtQuantity" runat="server" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" Width="100px" style="text-align:right;"></asp:TextBox>
							<asp:CompareValidator ID="compvaltxtQuantity" runat="server" ErrorMessage="<br />數量不正確" Display="Dynamic" ControlToValidate="txtQuantity" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
							<asp:RequiredFieldValidator ID="reqvaltxtQuantity" runat="server" ErrorMessage="<br />數量必須填寫" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<th style="padding-bottom:10px; float:left;">
						</th>
						<td style="padding-bottom:10px;  text-align:right; padding-right:10px;">
							<asp:Button ID="btnAddCart" runat="server" Text="加入購物車" BorderStyle="Solid" 
								BorderColor="#D5D5D5" BorderWidth="2px" BackColor="#BFBFBF" ForeColor="White" Width="100px" style="padding:5px" />
						</td>
					</tr>
				</table>
			</div>--%>
		</div>
	</div>

</asp:Content>

