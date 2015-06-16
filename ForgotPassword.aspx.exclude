<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="false" CodeFile="ForgotPassword.aspx.vb" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .form { padding-top:60px;  }
        .control { margin:0 auto; width:50%; line-height:1.5em; }
        .control input[button] { float:right; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>忘記密碼</h1>
    
    <div class="form">
        <asp:Panel runat="server" CssClass="control" DefaultButton="btnSubmit">
        <center>
        <p>請輸入登入ID，系統會將密碼發送給您。</p>
        <br />
        登入ID:
        <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
        <asp:Button runat="server" ID="btnSubmit" Text="提交" /><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="請入您的登入ID" ControlToValidate="txtUserName" 
                Display="Dynamic" SetFocusOnError="True" />
            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                ErrorMessage="登入ID不正確，請重試" 
                ControlToValidate="txtUserName" Display="Dynamic" SetFocusOnError="True" />
        <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label><br />
        </center>
        
        </asp:Panel>
    </div>
    
</asp:Content>


