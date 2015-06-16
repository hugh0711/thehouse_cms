<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="category.aspx.vb" Inherits="backoffice_category" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Reference control="~/control/ProductLangCategory.ascx"  %>

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

        function FCKUpdateLinkedField(id) {
            try {
                if (typeof (FCKeditorAPI) == "object") {
                    FCKeditorAPI.GetInstance(id).UpdateLinkedField();
                }
            }
            catch (err) {
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><asp:Label runat="server" ID="lblTitle">Category</asp:Label></h2>

<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>

        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                </asp:TabContainer>

        <table border="0" width="100%">
            <asp:Label ID="lblCategoryID" Visible="false" runat="server" />
            <colgroup>
                <col width="100" />
                <col />
            </colgroup>
            <%--<tr>
                <td>類別名稱</td>
                <td>
                    <asp:TextBox runat="server" ID="txtCategory" Columns="60"></asp:TextBox>
                    <asp:Button ID="btnLanguage" runat="server" Text="更多語言" Visible="True" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategory"  ValidationGroup="gp1" Display="Dynamic"
                        ErrorMessage="請輸入類別名稱"><br />請輸入類別名稱</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>詳情</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" Columns="60" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqDesc" runat="server" ControlToValidate="txtDesc"  ValidationGroup="gp1" Display="Dynamic" Enabled="false"
                        ErrorMessage="請輸入詳情"><br />請輸入詳情</asp:RequiredFieldValidator>
                    <FCKeditorV2:FCKeditor ID="htmlDescription" runat="server" ToolbarSet="MyToolbar" BasePath="~/fckeditor/" Height="500" Visible="false" >
                    </FCKeditorV2:FCKeditor>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="htmlDescription"  ValidationGroup="gp1" Display="Dynamic" Enabled="false"
                        ErrorMessage="請輸入詳情"><br />請輸入詳情</asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:CheckBox runat="server" ID="chkEnabled" Checked="true" Text="使用" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="PlaceHolderImage" Visible="false">
            <tr>
                <td>圖片</td>
                <td>
                    <asp:Image runat="server" ID="imgCategory" /><br />
                    <asp:Button runat="server" ID="btnUpload" Text="更新圖片" />
                </td>
            </tr>
            </asp:PlaceHolder>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblMessage"></asp:Label>
                </td>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnSave" Text=" 儲存 " ValidationGroup="gp1" />&nbsp;
                    <asp:Button runat="server" ID="btnSaveBack" Text=" 儲存並返回 " ValidationGroup="gp1" Visible="false" />&nbsp;
                    <asp:Button runat="server" ID="btnClose" Text=" 返回 " CausesValidation="false" />&nbsp;
                    <asp:Button runat="server" ID="btnDetele" Text="刪除" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

