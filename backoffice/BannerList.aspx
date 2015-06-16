<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="BannerList.aspx.vb" Inherits="backoffice_BannerList" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h2><asp:Label ID="lblBannerList" runat="server" Text="Banner List"></asp:Label></h2>
    <br />
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>

    <asp:DropDownList runat="server" ID="ddlPosition" DataSourceID="dsPosition" DataTextField="Description" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="dsPosition" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        
        SelectCommand="SELECT [ID], [Description] FROM [BannerPosition] WHERE Enabled = 1">
        <SelectParameters>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Button runat="server" ID="btnShow" Text="Show" />
	<br />
	<asp:Button ID="btnCreateBanner" runat="server" Text="New Banner" />
	<br /><br />
	<br />
	<asp:ReorderList ID="relst" runat="server" DataKeyField="BannerID" SortOrderField="SortOrder" DataSourceID="dsBanner" PostBackOnReorder="False" ItemInsertLocation="End"
                CssClass="reorderList" ShowInsertItem="false" AllowReorder="true" ClientIDMode="AutoID">
		<ItemTemplate>
			<table>
				<tr>
					<td>
						<asp:Image ID="Image1" runat="server" Width="100px" ImageUrl='<%# System.IO.Path.Combine(BannerClass.BannerImagePath, Eval("BannerImagePath")) %>' />
					</td>
					<td width="300px">
						<asp:Label ID="lblLink" runat="server" Text='<%# Eval("BannerName") %>'></asp:Label>
					</td>
					<td width="180px">
						<asp:Button ID="btnEdit" runat="server" Text="Edit" Width="80px" CommandName="btnEdit" CommandArgument='<%# Eval("BannerID") %>' />&nbsp;
						<asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" CommandName="btnDelete" CommandArgument='<%# Eval("BannerID") %>' />
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
	
	<asp:SqlDataSource ID="dsBanner" runat="server" ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
		SelectCommand="SELECT * FROM [Banner] WHERE PositionID = @PositionID ORDER BY [SortOrder]"
		DeleteCommand="DELETE FROM [Banner] WHERE BannerID = @BannerID"
		UpdateCommand="UPDATE [Banner] SET SortOrder = @SortOrder WHERE (BannerID = @BannerID)">
        <UpdateParameters>
            <asp:Parameter Name="SortOrder" />
            <asp:Parameter Name="BannerID" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter Name="PositionID" Type="Int32" ControlID="ddlPosition" PropertyName="SelectedValue" />            
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="BannerID" />
        </DeleteParameters>
	</asp:SqlDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" TargetControlID="UpdateProgress1" CssClass="updateProgress" ControlToOverlayID="UpdatePanel1" />
    
</asp:Content>

