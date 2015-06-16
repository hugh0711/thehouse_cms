<%@ Page Language="VB" AutoEventWireup="false" CodeFile="test_md5.aspx.vb" Inherits="test_md5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="js/jquery.md5.js"></script>
    <script type="text/javascript" src="js/jquery.1.6.4.min.js"></script>
    <script type="text/javascript">


        function getMD5() {
            var password = document.getElementById('<%=txt_password.ClientID%>').value;
            var secert_key = document.getElementById('<%=txt_secert_key.ClientID%>').value;
            document.getElementById("lbl_test").innerText = md5(password, secert_key);
            //var md5 = $.md5('admin2011', '123', true);
            //$('#lbl_test').val(md5('admin2011', secert_key));
            //$('#lbl_test').val('asdasd');
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Password:<asp:TextBox runat="server" Text="admin2011" ID="txt_password" Width="20%" />
        <br />
         Secert Key:<asp:TextBox runat="server" Width="50%"  ID="txt_secert_key"></asp:TextBox>
        <br />
        <button onclick="getMD5();return false;">Get Secert Key</button>
        <br />
    <asp:label runat="server" ID="lbl_test" Width="100%"/>
    </div>
    </form>
</body>
</html>
