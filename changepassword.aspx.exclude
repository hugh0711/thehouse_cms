<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPageInner.master" AutoEventWireup="false" CodeFile="changepassword.aspx.vb" Inherits="changepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="section-title">個人密碼</div>

<asp:Panel runat="server" ID="Panel1" CssClass="form" DefaultButton="btnSave">
    <div class="caption">舊密碼</div>
    <div class="content">
        <asp:TextBox runat="server" ID="txtOldPassword" TextMode="Password"></asp:TextBox>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword" Display="Dynamic"><br />必須填寫舊密碼</asp:RequiredFieldValidator>
        <asp:CustomValidator ID="valOldPassword" runat="server" ControlToValidate="txtOldPassword" Display="Dynamic"><br />密碼不正確</asp:CustomValidator>
    </div>
    <div class="clear"></div>
    
    <div class="caption">密碼</div>
    <div class="content">
        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
		<asp:Label ID="Label1" runat="server" Text="(6 - 20 個字)" CssClass="notes"></asp:Label>
		<asp:RequiredFieldValidator ID="reqvalPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic"><br />必須填寫密碼</asp:RequiredFieldValidator>

    </div>
    <div class="clear"></div>
    
    <div class="caption">重復輸入密碼</div>
    <div class="content">
        <asp:TextBox runat="server" ID="txtPassword2" TextMode="Password"></asp:TextBox>
		<asp:RequiredFieldValidator ID="reqvalPassword2" runat="server" ControlToValidate="txtPassword2" Display="Dynamic"><br />必須重復輸入密碼</asp:RequiredFieldValidator>
		<asp:CompareValidator ID="valPassword2" runat="server" ControlToValidate="txtPassword2" ControlToCompare="txtPassword" Operator="Equal" Type="String"><br />重復輸入密碼不同乎</asp:CompareValidator>
    </div>
    <div class="clear"></div>

    <div class="caption"></div>
    <div class="content">
        <asp:Button runat="server" ID="btnSave" Text="儲存" CssClass="submit-button" />
    </div>
</asp:Panel>

<asp:Panel runat="server" ID="Panel2" CssClass="form">
    <div class="caption"></div>
    <div class="content">
        密碼已成功更新，<asp:HyperLink runat="server" NavigateUrl="~/member.aspx" Text="按此"></asp:HyperLink>返回會員專頁。
        
    
    </div>
</asp:Panel>

</asp:Content>

