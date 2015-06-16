<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProductLangCategory.ascx.vb" Inherits="control_ProductLangCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>


<asp:HiddenField runat="server" ID="hfdLang" />

        <table border="0" width="100%">
            <colgroup>
                <col width="100" />
                <col />
            </colgroup>
            <tr>
                <td>類別名稱</td>
                <td>
                    <asp:TextBox runat="server" ID="txtCategory" Columns="60"></asp:TextBox>
                    <asp:Button ID="btnLanguage" runat="server" Text="更多語言" Visible="False" />
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
            </tr>
 
            
        </table>
   