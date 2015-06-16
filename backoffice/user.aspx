<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false"
    CodeFile="user.aspx.vb" Inherits="backoffice_user" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            height: 31px;
        }
        td { vertical-align:top; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        用戶資料</h1>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
    <blockquote>
        <table width="100%" border="0" cellpadding="2" cellspacing="1">
            <colgroup>
                <col width="100" />
                <col />
            </colgroup>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="gp1" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    用戶名稱<span style="color: Red">*</span>
                </td>
                <td valign="top">
                    <asp:TextBox runat="server" ID="txtUsername" Columns="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername"
                        ErrorMessage="請輸入用戶名稱" ValidationGroup="gp1">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtUsername"
                        ErrorMessage="用戶名稱已存在, 請選用另一個用戶名稱" ValidationGroup="gp1">*</asp:CustomValidator>
                </td>
            </tr>
            <%--<tr>
                <td valign="top">
                    Last Name<span style="color: Red">*</span>
                </td>
                <td valign="top">
                    <asp:TextBox runat="server" ID="txtLastName" Columns="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLastName"
                        ErrorMessage="Please fill last name" ValidationGroup="gp1">*</asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <tr>
                <td valign="top">
                    顯示名稱<span style="color: Red">*</span>
                </td>
                <td valign="top">
                    <asp:TextBox runat="server" ID="txtDisplayName" Columns="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDisplayName"
                        ErrorMessage="Please fill first name" ValidationGroup="gp1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <asp:Panel runat="server" ID="pnlPassword">
            <tr>
                <td valign="top">
                    密碼<span style="color: Red">*</span>
                </td>
                <td valign="top">
                    <asp:TextBox runat="server" ID="txtPassword" Columns="40" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="請輸入登入密碼" ValidationGroup="gp1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    重入密碼<span style="color: Red">*</span>
                </td>
                <td valign="top">
                    <asp:TextBox runat="server" ID="txtPassword2" Columns="40" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassword2"
                        ErrorMessage="請輸入重入密碼" ValidationGroup="gp1">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtPassword" ControlToValidate="txtPassword2" 
                        ErrorMessage="重入密碼不相同" ValidationGroup="gp1">*</asp:CompareValidator>
                </td>
            </tr>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlSetPassword" Visible="false">
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button runat="server" ID="btnSetPassword" Text="設定密碼" />
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td valign="top">
                    電子郵件<span style="color: Red">*</span>
                </td>
                <td valign="top">
                    <asp:TextBox runat="server" ID="txtEmail" Columns="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="請輸入Email" ValidationGroup="gp1">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="不合法Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="gp1">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    性別<span style="color: Red">*</span>
                </td>
                <td valign="top">
                    <asp:RadioButtonList runat="server" ID="rblGender" RepeatLayout="Table" RepeatDirection="Horizontal"
                        Style="float: left">
                        <asp:ListItem Value="M">Male</asp:ListItem>
                        <asp:ListItem Value="F">Female</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorGender" runat="server" ControlToValidate="rblGender"
                        ErrorMessage="Please select gender" ValidationGroup="gp1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    聯絡電話
                </td>
                <td valign="top">
                    <asp:TextBox runat="server" ID="txtContact" Columns="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtContact" Enabled="false"
                        ErrorMessage="Please fill contact number" ValidationGroup="gp1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
    
            <asp:PlaceHolder runat="server" Visible="false">
            <tr>
    				<td valign="top">
					送遞地址
				</td>
				<td>
					<asp:TextBox ID="txtDeliveryAddress" runat="server" TextMode="MultiLine" Rows="5" Width="300px"></asp:TextBox>
				</td>
            </tr>
            <%--<tr>
                <td valign="top">
                    Region<span style="color: Red">*</span>
                </td>
                <td valign="top">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnection %>"
                        SelectCommand="SELECT [Code3], [Name] FROM [aspnet_Countries] WHERE ([Disabled] = @Disabled) ORDER BY [Name]">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="false" Name="Disabled" Type="Boolean" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:DropDownList runat="server" ID="ddlRegion" DataSourceID="SqlDataSource1" DataTextField="Name"
                        DataValueField="Code3">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    What is your specialized area of sourcing?
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnection %>"
                        SelectCommand="SELECT [Category_id], [Category] FROM [Category] WHERE (([IsDisabled] = @IsDisabled) AND ([Parent_id] = @Parent_id)) ORDER BY [sort_order], [Category]">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="false" Name="IsDisabled" Type="Boolean" />
                            <asp:Parameter DefaultValue="0" Name="Parent_id" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:CheckBoxList ID="cblInterested" runat="server" DataSourceID="SqlDataSource2"
                        DataTextField="Category" DataValueField="Category_id">
                    </asp:CheckBoxList>
                </td>
            </tr>
            --%>
            <tr>
                <td>
                    Country
                </td>
                <td>
					<asp:RadioButtonList ID="radCountry" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="Local" Text="Local"></asp:ListItem>
						<asp:ListItem Value="Overseas" Text="Overseas"></asp:ListItem>
						<asp:ListItem Value="OverseasExpress" Text="Overseas Express"></asp:ListItem>
					</asp:RadioButtonList>
                </td>
            </tr>
            <tr>
    				<td valign="top">
					Birtday
				</td>
				<td>
					<asp:TextBox ID="txtBirthday" runat="server" Width="300px"></asp:TextBox>
				</td>
            </tr>
            </asp:PlaceHolder>

            <tr>
    				<td valign="top">
					Facebook ID
				</td>
				<td>
					<asp:TextBox ID="txtFacebookID" runat="server" Width="300px"></asp:TextBox>
				</td>
            </tr>
            <tr>
                <td>
                    建立日期
                </td>
                <td>
                    <asp:Label ID="lblCreationDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    最近登入日期
                </td>
                <td>
                    <asp:Label ID="lblLastLoginDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="chkApproved" runat="server" Text="核准" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style2">
                    <asp:Panel runat="server" ID="pnlLockedOut">
                        <asp:Label runat="server" ID="lblLockedOut" Text="這用已戶被封鎖" ForeColor="Red"></asp:Label>
                        <asp:Button runat="server" ID="btnUnlock" Text="解鎖" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    角色
                </td>
                <td class="style2">
                    <asp:CheckBoxList ID="cblRole" runat="server" ValidationGroup="user" Height="26px">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                </td>
            </tr>
<%--            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSend" runat="server" Text="Send Password to User" />
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkNew" runat="server" Visible="False" />
                    <asp:Button ID="btnSave" runat="server" Text=" 儲存 " ValidationGroup="gp1" />
                    <asp:Button ID="btnSaveClose" runat="server" Text="儲存及返回" ValidationGroup="gp1" />
                    <asp:Button ID="btnClose" runat="server" Text=" 返回 " />
                    <asp:Button ID="btnDelete" runat="server" Text=" 刪除 " />
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <p>
                        &nbsp;</p>
                </td>
            </tr>
        </table>
    </blockquote>
            </ContentTemplate>
        </asp:UpdatePanel>

    <p>
    </p>
    <p>
    </p>
</asp:Content>
