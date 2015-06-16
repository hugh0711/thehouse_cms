<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="Banner.aspx.vb" Inherits="backoffice_Banner" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<style type="text/css">
	 .watertext {color:#CCCCCC;}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>Banner</h2>
<asp:HiddenField ID="hfdBannerID" runat="server" />
<asp:HiddenField ID="hfdPositionID" runat="server" />
<asp:HiddenField ID="hfdBannerType" runat="server" />
<asp:HiddenField ID="hfdFormMode" runat="server" />
<asp:HiddenField ID="hfdBackPanel" runat="server" />
<asp:HiddenField ID="hfdTagFunctionID" runat="server" />

<table class="tableDetail">
    <asp:PlaceHolder runat="server" Visible="false">
	<tr>
		<th>CreateDate</th>
		<td width="600px">
			<asp:TextBox ID="txtBannerCreateDate" runat="server" Width="300px"></asp:TextBox>
			<asp:CalendarExtender ID="txtBannerCreateDate_CalendarExtender" runat="server" ClearTime="True" Enabled="True" Format="yyyy-MM-dd" 
				TargetControlID="txtBannerCreateDate">
			</asp:CalendarExtender>
			<asp:CustomValidator ID="custvaltxtBannerCreateDate" runat="server" ErrorMessage="Date incorrect" ControlToValidate="txtBannerCreateDate" Display="Dynamic"></asp:CustomValidator>
		</td>
	</tr>
	</asp:PlaceHolder>
	<tr>
		<th valign="top">Position</th>
		<td>
			<asp:DropDownList ID="ddlBannerPosition" runat="server" DataSourceID="dsBannerPosition" DataTextField="Description" 
				DataValueField="ID">
				<%--<asp:ListItem Value="" Text=""></asp:ListItem>--%>
			</asp:DropDownList>
			<asp:SqlDataSource ID="dsBannerPosition" runat="server" ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
				SelectCommand="SELECT ID, Description FROM [BannerPosition] WHERE Enabled = 1 ORDER BY [SortOrder]">
				<SelectParameters>
				</SelectParameters>
			</asp:SqlDataSource>
		</td>
	</tr>
	<tr>
		<th></th>
		<td>
			<asp:TabContainer ID="TabContainer1" runat="server">
				<asp:TabPanel runat="server" ID="tabEN">
					<HeaderTemplate>Eng</HeaderTemplate>
					<ContentTemplate>
						Name :
						<asp:TextBox ID="txtLangNameEN" runat="server" Width="200px"></asp:TextBox>
						<br />
						<asp:Label ID="txtUrlEN" runat="server" Width="500px" Visible="false"></asp:Label>
						<br />
						<asp:FileUpload ID="FileUploadEN" runat="server" />
						<br />
						<asp:Image ID="ImgEN" runat="server" Height="100px" />
					</ContentTemplate>
				</asp:TabPanel>
				<asp:TabPanel runat="server" ID="tabHK">
					<HeaderTemplate>Chinese</HeaderTemplate>
					<ContentTemplate>
						Name :
						<asp:TextBox ID="txtLangNameHK" runat="server" Width="200px"></asp:TextBox>
						<br />
						<asp:Label ID="txtUrlHK" runat="server" Width="500px" Visible="false"></asp:Label>
						<br />
						<asp:FileUpload ID="FileUploadHK" runat="server" />
						<br />
						<asp:Image ID="ImgHK" runat="server" Height="100px" />
					</ContentTemplate>
				</asp:TabPanel>
			</asp:TabContainer>
		</td>
	</tr>
    <%--IsDisable--%>
	<tr>
		<th valign="top">Tag</th>
		<td>
		    <asp:SqlDataSource runat="server" ID="dsTag" 
                ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                
                SelectCommand="SELECT [TagID], [Tag] FROM [Tag] WHERE (([Enabled] = @Enabled) AND ([FunctionID] = @FunctionID)) ORDER BY [SortOrder]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
                    <asp:ControlParameter ControlID="hfdTagFunctionID" Name="FunctionID" 
                        PropertyName="Value" Type="Int32" />
                </SelectParameters>
		    </asp:SqlDataSource>
		    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
		        <ContentTemplate>
		            <asp:RadioButton runat="server" ID="rbLinkTag" Text="Use Tag" AutoPostBack="true" GroupName="link" />
        		    <asp:Panel runat="server" ID="pnlLinkTag" GroupingText="Select Tag" style="padding-left:20px;">
		            <asp:RadioButtonList runat="server" ID="rblTag" DataSourceID="dsTag" 
                                DataTextField="Tag" DataValueField="TagID" AutoPostBack="true" 
                                RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:RadioButtonList>
        		    </asp:Panel>
		            <asp:RadioButton runat="server" ID="rbLinkUrl" Text="Other Url" AutoPostBack="true" GroupName="link" />
        		    <asp:Panel runat="server" ID="pnlLinkUrl" Enabled="false" GroupingText="Other Url" style="padding-left:20px;">
			            <asp:TextBox ID="txtBannerUrl" runat="server" Width="500px" Visible="true"></asp:TextBox><br />
			            <asp:Label runat="server" Text="use ~ to replace http://www.winexpert.com.hk. For example, ~/promote.aspx?id=1"></asp:Label>
			        </asp:Panel>
		        </ContentTemplate>
		    </asp:UpdatePanel>
		    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1">
		        <ProgressTemplate>
		            <div></div>
		        </ProgressTemplate>
		    </asp:UpdateProgress>
            <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" CssClass="updateProgress" TargetControlID="UpdateProgress1" ControlToOverlayID="UpdatePanel1" />

		</td>
	</tr>
	<tr>
		<th><%--Order--%></th>
		<td>
			<asp:TextBox ID="txtBannerOrder" runat="server" Width="300px" Visible="false"></asp:TextBox>
			<%--<asp:CompareValidator ID="comvaltxtBannerOrder" runat="server" ErrorMessage="BannerOrder > 0" Display="Dynamic" ControlToValidate="txtBannerOrder" Type="Integer"  Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>--%>
		</td>
	</tr>
	<tr>
		<th><%--IsDisable--%></th>
		<td>
			<asp:CheckBox ID="chkEnabled" runat="server" Text="Enabled" Visible="true"/>
		</td>
	</tr>
</table>
<table>
	<tr>
		<th width="700px">
			<asp:Button ID="btnDelete" runat="server" Text="Delete" BackColor="Red" 
				BorderColor="#CC0000" BorderStyle="Outset" BorderWidth="2px" 
				ForeColor="White" Width="80px" CausesValidation="False" />
			<asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" 
				ConfirmText="Confirm Delete ?" Enabled="True" TargetControlID="btnDelete">
			</asp:ConfirmButtonExtender>
			<asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#FF6600" 
				BorderColor="#FF9933" BorderStyle="Outset" BorderWidth="2px" 
				ForeColor="White" Width="80px" />
			<asp:Button ID="btnBack" runat="server" Text="Back" BackColor="#CCCCCC" 
				BorderColor="Gray" BorderStyle="Outset" BorderWidth="2px" 
				ForeColor="#333333" Width="80px" CausesValidation="False" />
		</th>
	</tr>
</table>
</asp:Content>

