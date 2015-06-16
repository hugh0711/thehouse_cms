<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="top_rank.aspx.vb" Inherits="backoffice_top_rank" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>更新最多瀏覽</h2>
    <table border="0">
        <tr>
            <td>Rank</td>
            <td>Episode</td>
        </tr>
        <tr>
            <td width="100">#1</td>
            <td width="200"><asp:TextBox runat="server" ID="Textbox1"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="100">#2</td>
            <td width="200"><asp:TextBox runat="server" ID="Textbox2"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="100">#3</td>
            <td width="200"><asp:TextBox runat="server" ID="Textbox3"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="100">#4</td>
            <td width="200"><asp:TextBox runat="server" ID="Textbox4"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="100">#5</td>
            <td width="200"><asp:TextBox runat="server" ID="Textbox5"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label runat="server" id="lblMessage" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button runat="server" ID="btnUpdate" Text="Update" />
                <asp:ConfirmButtonExtender runat="server" TargetControlID="btnUpdate" ConfirmText="是否確定更新最多瀏覽?" />
            </td>
        </tr>
    </table>
</asp:Content>

