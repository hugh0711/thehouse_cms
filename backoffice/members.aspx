<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="members.aspx.vb" Inherits="backoffice_members" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>
        會員管理</h1>
    <table >
        <tr style="display:none;">
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
                搜尋: <asp:TextBox ID="txtFilter" runat="server" Columns="50"></asp:TextBox>
                <asp:Label ID="lblAction" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnFindByUsername" runat="server" Text="搜尋會員名稱" />
            </td>
            <td>
                <asp:Button ID="btnFindByEmail" runat="server" Text="搜尋Email" />
            </td>
            <td>
                <asp:Button ID="btnShowAll" runat="server" Text="顯示所有會員" />
            </td>
        </tr>
    </table>
    <%--<p>
    <asp:Button ID="btnCreateUser" runat="server" Text="新增用戶"/>
    </p>--%>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
        PageSize="15" AllowSorting="True" AutoGenerateColumns="False" Width="100%" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="Username" DataNavigateUrlFormatString="~/backoffice/member.aspx?member={0}"
                DataTextField="Username" HeaderText="會員名稱">
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
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <%--<p>
    <asp:Button ID="btnCreateUser2" runat="server" Text="新增用戶" />
    </p>--%>
    <br />
    <br />

</asp:Content>

