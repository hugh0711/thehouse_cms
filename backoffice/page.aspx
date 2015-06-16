<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="page.aspx.vb" Inherits="backoffice_page" ValidateRequest="false" %>

<%--<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>--%>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<%@ Register src="~/control/PageLangControl.ascx" tagname="PageLangControl" tagprefix="uc4" %>

<%@ Reference Control="~/control/PageLangControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../ckfinder/ckfinder.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function BrowseServer(inputId) {
            //CKFinder.Popup('../ckfinder/', null, null, SetFileField);
            var finder = new CKFinder();
            finder.BasePath = '../ckfinder/';
            finder.SelectFunction = SetFileField;
            finder.SelectFunctionData = inputId;
            finder.Popup();

        }

        function SetFileField(fileUrl, data) {
            $("#" + data["selectFunctionData"]).val(fileUrl);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>
        Web Page</h2>



<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
    
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="gp1" ShowSummary="false" ShowMessageBox="true" />
<asp:Panel runat="server" ID="pnlUrl">
        <table border="0" width="600">
            <colgroup>
                <col width="100" />
                <col />
            </colgroup>
            <tr>
                    <td>URL</td>
                <td>
                    <asp:TextBox runat="server" ID="txtUrl" Columns="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUrl"  ValidationGroup="gp1"
                        ErrorMessage="請輸入URL"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Template</td>
                <td>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" 
                        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                        SelectCommand="SELECT [MasterPage] FROM [MasterPage] ORDER BY [SortOrder]"></asp:SqlDataSource>
                    <asp:DropDownList runat="server" ID="ddlMasterPage" 
                        DataSourceID="SqlDataSource1" DataTextField="MasterPage" 
                        DataValueField="MasterPage"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Redirect (if any)</td>
                <td><asp:TextBox runat="server" ID="txtRedirect" ></asp:TextBox>.htm</td>
            </tr>
            <tr>
                <td>Visible</td>
                <td><asp:CheckBox runat="server" ID="chkVisible" Checked="true" /></td>
            </tr>
            <tr>
                <td>Enabled</td>
                <td>
                    <asp:CheckBox runat="server" ID="chkEnabled" Checked="true" />
                </td>
            </tr>
        </table>

</asp:Panel>
    


                 <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                </cc1:TabContainer>

    
<asp:Panel runat="server" ID="pnlButton">
    <table border="0" width="600">

            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblMessage"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnLanguage" runat="server" Text="更多語言" Visible="False" />
                    <asp:Button runat="server" ID="btnSave" Text="Update" ValidationGroup="gp1" />&nbsp;
                    <asp:Button runat="server" ID="btnSaveClose" Text="Update &amp; Back" ValidationGroup="gp1" />&nbsp;
                    <asp:Button runat="server" ID="btnClose" Text="Back" />&nbsp;
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" />
                </td>
            </tr>
        </table>
</asp:Panel>


        </ContentTemplate>
</asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc2:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" ControlToOverlayID="UpdatePanel1" TargetControlID="UpdateProgress1" OverlayType="Control" CssClass="updateProgress" />

</asp:Content>

