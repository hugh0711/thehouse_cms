<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/admin.master" AutoEventWireup="false" CodeFile="Comment.aspx.vb" Inherits="backoffice_Comment" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
	th { background:#efefef; }
	td, th { vertical-align:top; text-align:left; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center><h3>Comment</h3></center>
<asp:HiddenField ID="hfdCommentID" runat="server" />
<asp:HiddenField ID="FormMode" runat="server" />

<table class="tableDetail">
	<tr>
		<th>CommentType</th>
		<td>
			<asp:Label ID="txtCommentType" runat="server" Text=""></asp:Label>
		</td>
		<td>
		</td>
	</tr>
	<tr>
		<th>ReferenceID</th>
		<td>
			<asp:Label ID="txtReferenceID" runat="server" Width="300px"></asp:Label>
		</td>
		<td>
		</td>
	</tr>
	<tr>
		<th>UserID</th>
		<td>
			<asp:Label ID="txtUserID" runat="server" Width="300px"></asp:Label>
		</td>
		<td>
		</td>
	</tr>
	<tr>
		<th>CommentDate</th>
		<td>
			<asp:Label ID="txtCommentDate" runat="server" Width="300px"></asp:Label>
		</td>
		<td>
		</td>
	</tr>
	<tr>
		<th>CommentDescription</th>
		<td>
			<asp:TextBox ID="txtCommentDescription" runat="server" Width="300px" TextMode="MultiLine" Rows="5"></asp:TextBox>
		</td>
		<td>
			<asp:RequiredFieldValidator ID="reqvaltxtCommentDescription" runat="server" ControlToValidate="txtCommentDescription" Display="Dynamic" ErrorMessage="CommentDescription required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<th><%--IsInspected--%></th>
		<td>
			<asp:CheckBox ID="chkIsInspected" runat="server" Visible="false"/>
		</td>
		<td>
		</td>
	</tr>
	<tr>
		<th>IsDisable</th>
		<td>
			<asp:CheckBox ID="chkIsDisable" runat="server"/>
		</td>
		<td>
		</td>
	</tr>
</table>
<table>
	<tr>
		<th>
			<asp:Button ID="btnDelete" runat="server" Text="Delete" BackColor="Red" 
				BorderColor="#CC0000" BorderStyle="Outset" BorderWidth="2px" 
				ForeColor="White" Width="80px" CausesValidation="False" />
			<asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" 
				ConfirmText="Confirm Delete ?" Enabled="True" TargetControlID="btnDelete">
			</asp:ConfirmButtonExtender>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#FF6600" 
				BorderColor="#FF9933" BorderStyle="Outset" BorderWidth="2px" 
				ForeColor="White" Width="80px" />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID="btnBack" runat="server" Text="Back" BackColor="#CCCCCC" 
				BorderColor="Gray" BorderStyle="Outset" BorderWidth="2px" 
				ForeColor="#333333" Width="80px" CausesValidation="False" />
		</th>
	</tr>
</table>

</asp:Content>

