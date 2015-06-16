<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false"
    CodeFile="users.aspx.vb" Inherits="backoffice_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <h2>
        用戶管理</h2>
    <table style="display:none;">
        <tr>
            <td>
                <asp:RadioButtonList ID="rblMatch" runat="server" RepeatDirection="Horizontal" 
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True">Exact Match</asp:ListItem>
                    <asp:ListItem Value="%">Start With</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtFilter" runat="server" Columns="50"></asp:TextBox>
                <asp:Label ID="lblAction" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnFindByUsername" runat="server" Text="Find Username" />
            </td>
            <td>
                <asp:Button ID="btnFindByEmail" runat="server" Text="Find Email" />
            </td>
            <td>
                <asp:Button ID="btnShowAll" runat="server" Text="Show All" />
            </td>
        </tr>
    </table>
    <p>
    <asp:Button ID="btnCreateUser" runat="server" Text="新增用戶"/>
    </p>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="gridview"
        PageSize="15" AllowSorting="True" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="Username" DataNavigateUrlFormatString="~/backoffice/user.aspx?user={0}"
                DataTextField="Username" HeaderText="用戶名稱">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="email" HeaderText="Email">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="IsOnline" HeaderText="在線">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:CheckBoxField DataField="IsApproved" HeaderText="核准">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:BoundField DataField="CreationDate" HeaderText="建立日期" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="LastLoginDate" HeaderText="最近登入日期" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <%--<asp:HyperLinkField HeaderText="" Text="產品" DataNavigateUrlFields="Username" DataNavigateUrlFormatString="~/backoffice/userproducts.aspx?{0}" />--%>
        </Columns>
    </asp:GridView>
    <p>
    <asp:Button ID="btnCreateUser2" runat="server" Text="新增用戶" />
    </p>
    <br />
    <br />
</asp:Content>
