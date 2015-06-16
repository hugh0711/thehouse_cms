<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="false" CodeFile="admin.aspx.vb" Inherits="admin" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="section-title">登入</div>

    
    <br />
    <br />
    <table border="0" align="center" style="width:350px; height:350px;">
        <tr>
            <td align="left" valign="middle">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="Login1$LoginButton">
                    <asp:Login ID="Login1" runat="server">
                        <LayoutTemplate>
                            <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                                <tr>
                                    <td>
                                        <table cellpadding="0">
                                            <tr>
                                                <td align="center" colspan="2">
                                                    登入</td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">帳戶:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                        ControlToValidate="UserName" ErrorMessage="請輸入帳戶" 
                                                        ToolTip="必須輸入帳戶" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密碼:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                        ControlToValidate="Password" ErrorMessage="請輸入密碼" 
                                                        ToolTip="必須輸入密碼." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
<%--                                            <tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td align="center" colspan="2" style="color:Red;">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" 
                                                        onclick="LoginButton_Click" Text="登入" ValidationGroup="Login1" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:Login>
                    <br />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/forgotpassword.aspx" Text="忘記密碼"></asp:HyperLink>
                    <cc1:NoBot ID="NoBot1" runat="server" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
</asp:Content>

