<%@ Page Language="VB" AutoEventWireup="false" CodeFile="forgotpassword.aspx.vb" Inherits="template_forgotpassword" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Thank you for your request of password recovery. Here is your password:</p>
        <p></p>
        <p>Username: <asp:Label runat="server" ID="lblUsername"></asp:Label></p>
        <p>Password: <asp:Label runat="server" ID="lblPassword"></asp:Label></p>
        <p></p>
        <p>Please login and enjoy the experience in <asp:HyperLink runat="server" ID="lnkLogin"></asp:HyperLink></p>  
        <p></p>
        <p>Winexpert.hk</p>
    </div>
    </form>
</body>
</html>
