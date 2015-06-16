<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="tag.aspx.vb" Inherits="backoffice_tag" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>
Tag</h2>
<asp:HiddenField runat="server" ID="hfdFunctionID" />

<table border="0" width="400">
    <colgroup>
        <col width="100" />
        <col />
    </colgroup>
    <tr>
        <td>Tag Name</td>
        <td>
            <asp:TextBox runat="server" ID="txtTag"></asp:TextBox>
            <asp:PlaceHolder runat="server" Visible="false">
                <asp:Button ID="btnLanguage" runat="server" Text="More Language" Visible="false" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<br />Please enter Tag Name" ValidationGroup="gp1" ControlToValidate="txtTag" Display="Dynamic" />
            </asp:PlaceHolder>
        </td>
    </tr>
    <asp:PlaceHolder runat="server" Visible="true">
    <tr>
        <td>Tag Group</td>
        <td>
            <asp:TextBox runat="server" ID="txtTagGroup"></asp:TextBox>
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox runat="server" ID="chkEnabled" Checked="true" Text="Enable"/>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button runat="server" ID="btnSave" Text="Save" ValidationGroup="gp1" />&nbsp;
            <asp:Button runat="server" ID="btnClose" Text="Back" />&nbsp;
            <asp:Button runat="server" ID="btnDelete" Text="Delete" />
            <cc1:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure to delete this tag?" 
                TargetControlID="btnDelete">
            </cc1:ConfirmButtonExtender>
        </td>
    </tr>
</table>
<p></p>
<p></p>
</asp:Content>

