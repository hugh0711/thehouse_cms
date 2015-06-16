<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="promos.aspx.vb" Inherits="backoffice_promos" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .banner-image { max-width:100px; max-height:100px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><asp:Label ID="lblPromo" runat="server" Text="Promo List"></asp:Label></h2>
    <br />
    <asp:HiddenField runat="server" ID="hfdTypeID" />

    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>

    <%--類別: --%>
    <asp:DropDownList runat="server" ID="ddlPromo" DataSourceID="dsPromoSetting" AutoPostBack="true"
        DataTextField="Name" DataValueField="PromoSettingID" Visible="false"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="dsPromoSetting" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        
        SelectCommand="SELECT [PromoSettingID], [Name] FROM [PromoSetting] WHERE ([TypeID] = @TypeID) ORDER BY [SortOrder]">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfdTypeID" Name="TypeID" PropertyName="Value" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

	<br />
	<br />
	<asp:Button ID="btnCreateBanner" runat="server" Text="New Banner" />
	<br /><br />
	        
	<br />
	<asp:ReorderList ID="relst" runat="server" DataKeyField="PromoID" SortOrderField="SortOrder" DataSourceID="dsPromo" PostBackOnReorder="False" ItemInsertLocation="End"
                CssClass="reorderList" ShowInsertItem="false" AllowReorder="true" ClientIDMode="AutoID">
		<ItemTemplate>
			<table>
				<tr>
                    <asp:PlaceHolder runat="server" Visible='<%# HasImage  %>'>
					<td>
						<asp:Image ID="Image1" runat="server" CssClass="banner-image" ImageUrl='<%# System.IO.Path.Combine(ConfigurationManager.AppSettings("PromoImagePath"), Eval("PromoImageUrl")) %>' />
					</td>
                    </asp:PlaceHolder>
					<td width="300px">
						<asp:Label ID="lblLink" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
					</td>
					<td width="180px">
						<asp:Button ID="btnEdit" runat="server" Text="Edit" Width="80px" CommandName="btnEdit" CommandArgument='<%# Eval("PromoID") %>' />&nbsp;
						<asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" CommandName="Delete" CommandArgument='<%# Eval("PromoID") %>' />
						<asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnDelete" ConfirmText="Delete ?">
						</asp:ConfirmButtonExtender>
					</td>
				</tr>
			</table>
		</ItemTemplate>
		<DragHandleTemplate>
			<div class="reorderhandle">
            </div>
		</DragHandleTemplate>
		<ReorderTemplate>
			<asp:Panel ID="Panel2" runat="server" CssClass="reorderItem" />
		</ReorderTemplate>
	</asp:ReorderList>
	
	<asp:SqlDataSource ID="dsPromo" runat="server" ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
		SelectCommand="SELECT * FROM [Promo] WHERE PromoSettingID = @PromoSettingID ORDER BY [SortOrder]"
		DeleteCommand="DELETE FROM [Promo] WHERE PromoID = @PromoID"
		UpdateCommand="UPDATE [Promo] SET SortOrder = @SortOrder WHERE (PromoID = @PromoID)">
        <UpdateParameters>
            <asp:Parameter Name="SortOrder" />
            <asp:Parameter Name="PromoID" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter Name="PromoSettingID" Type="Int32" ControlID="ddlPromo" PropertyName="SelectedValue" />            
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="PromoID" />
        </DeleteParameters>
	</asp:SqlDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

