<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/admin.master" AutoEventWireup="false" CodeFile="CommentList.aspx.vb" Inherits="backoffice_CommentList" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
	th { background:#efefef; vertical-align:middle; text-align:left; padding:0 5px; }
	td { vertical-align:top; text-align:left; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>評論管理</h1>

<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>

    <table>
	    <tr>
		    <th>開始日期</th>
		    <td>
			    <asp:TextBox ID="txtDateFrom" runat="server" Columns="10"></asp:TextBox>
			    <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDateFrom" ClearTime="True" Format="yyyy-MM-dd" 
				    TodaysDateFormat="yyyy-MM-dd">
			    </asp:CalendarExtender>
			    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Date Incorrect" ControlToValidate="txtDateFrom" Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator>
		    </td>
		    <th>最後日期</th>
		    <td>
			    <asp:TextBox ID="txtDateTo" runat="server" Columns="10"></asp:TextBox>
			    <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDateTo" ClearTime="True" Format="yyyy-MM-dd" 
				    TodaysDateFormat="yyyy-MM-dd">
			    </asp:CalendarExtender>
			    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Date Incorrect" ControlToValidate="txtDateTo" Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator>
		    </td>
		    <th>檢舉</th>
		    <td>
			    <asp:DropdownList ID="radIsInspected" runat="server" >
				    <asp:ListItem Text="不苟" Value="" Selected="True"></asp:ListItem>
				    <asp:ListItem Text="已檢舉" Value="1"></asp:ListItem>
				    <asp:ListItem Text="沒有檢舉" Value="0"></asp:ListItem>
			    </asp:DropdownList>
		    </td>
		    <th>隠藏</th>
		    <td>
			    <asp:DropdownList ID="radIsDisable" runat="server" >
				    <asp:ListItem Text="不苟" Value="" Selected="True"></asp:ListItem>
				    <asp:ListItem Text="已隱藏" Value="1"></asp:ListItem>
				    <asp:ListItem Text="沒有隱藏" Value="0"></asp:ListItem>
			    </asp:DropdownList>
		    </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlSort">
                    <asp:ListItem Value="ASC">評論按時間順序</asp:ListItem>
                    <asp:ListItem Value="DESC">評論按時間倒序</asp:ListItem>
                </asp:DropDownList>
            </td>
		    <td>
			    <asp:Button ID="btnSearch" runat="server" Text="顯示" />
		    </td>
	    </tr>
    </table>



    	<asp:SqlDataSource ID="dsComment" runat="server" ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
		SelectCommand=""></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
            SelectCommand="SELECT * FROM [Comment] WHERE (([IsInspected] = @IsInspected) AND ([IsDisable] = @IsDisable)) ORDER BY [CommentDate] ">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="IsInspected" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="IsDisable" Type="Boolean" />
        </SelectParameters>
        </asp:SqlDataSource>
	<asp:GridView ID="gvComment" runat="server" AllowPaging="True" Width="100%" PageSize="25"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
		BorderWidth="1px" CellPadding="4" DataKeyNames="CommentID" 
            DataSourceID="dsComment" ForeColor="Black" GridLines="Vertical">
		<RowStyle BackColor="#F7F7DE" />
		<EmptyDataTemplate>
		    沒有評論
		</EmptyDataTemplate>
		<Columns>
			<asp:BoundField DataField="CommentID" HeaderText="CommentID" InsertVisible="False" ReadOnly="True" SortExpression="CommentID" Visible="False" />
			<asp:BoundField DataField="CommentDate" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="評論日期" SortExpression="CommentDate" />
            <asp:TemplateField HeaderText="評論用戶" SortExpression="UserID" >
                <ItemTemplate>
                    <asp:HyperLink runat="server" Text='<%# Eval("UserID") %>' NavigateUrl='<%# String.Format("~/backoffice/user.aspx?user={0}", Eval("UserID")) %>' Target="_blank"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
			<asp:BoundField DataField="CommentType" HeaderText="Show in" SortExpression="CommentType" Visible="false" />
			<asp:TemplateField HeaderText="內容" SortExpression="CommentDescription">
				<ItemTemplate>
					<asp:Label ID="Label1" runat="server" Text='<%# Eval("CommentDescription") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<%--<asp:CheckBoxField DataField="IsInspected" HeaderText="IsInspected" SortExpression="IsInspected" />--%>
			<%--<asp:CheckBoxField DataField="IsDisable" HeaderText="Disabled" SortExpression="IsDisable" ItemStyle-HorizontalAlign="Center"  />--%>
			<asp:TemplateField HeaderText="" SortExpression="CommentType">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# GetUrl(Eval("CommentType"), Eval("ReferenceID")) %>' Target="_blank">顯示評論網頁</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
			<asp:TemplateField>
				<ItemTemplate>
                    <asp:PlaceHolder runat="server" Visible='<%# Not Eval("IsInspected") AND NOT Eval("IsDisable") %>'>
					<asp:Button ID="btnInspect" runat="server" Text="檢舉" CommandName="inspect" CommandArgument='<%# Eval("CommentID") %>' UseSubmitBehavior="false" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnInspect" ConfirmText="確定檢舉?" />
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" Visible='<%# Eval("IsInspected") AND NOT Eval("IsDisable") %>'>
					<asp:Button ID="btnRelease" runat="server" Text="解封" CommandName="release" CommandArgument='<%# Eval("CommentID") %>' UseSubmitBehavior="false" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnRelease" ConfirmText="確定解除檢舉?" />
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" Visible='<%# Not Eval("IsDisable") %>'>
					<asp:Button ID="btnDisable" runat="server" Text="隱藏" CommandName="disable" CommandArgument='<%# Eval("CommentID") %>' UseSubmitBehavior="false" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnDisable" ConfirmText="確定隱藏?" />
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" Visible='<%# Eval("IsDisable") %>'>
					<asp:Button ID="btnEnable" runat="server" Text="顯示" CommandName="enable" CommandArgument='<%# Eval("CommentID") %>' UseSubmitBehavior="false" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" TargetControlID="btnEnable" ConfirmText="確定顯示?" />
                    </asp:PlaceHolder>
					<asp:Button ID="btnDelete" runat="server" Text="刪除" CommandName="delete" CommandArgument='<%# Eval("CommentID") %>' Visible="false"/>
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" TargetControlID="btnDelete" ConfirmText="確定刪除?" />
					<%--<asp:Button ID="btnDetail" runat="server" Text="Detail" CommandName="detail" CommandArgument='<%# Eval("CommentID") %>' />--%>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
		<FooterStyle BackColor="#CCCC99" />
		<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
		<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="Black" />
		<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" CssClass="updateProgress" ControlToOverlayID="UpdatePanel1" TargetControlID="UpdateProgress1" runat="server" />
</asp:Content>

