<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PageLangControl.ascx.vb" Inherits="control_PageLangControl" %>

<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>

<asp:HiddenField runat="server" ID="hfdLang" />

    <table border="0" width="600">
            <colgroup>
                <col width="100" />
                <col />
            </colgroup>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false">
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkLangEnabled" Checked="true" Text="使用此語言" />
                </td>
            </tr>
            </asp:PlaceHolder>
            <tr>
                <td>Page Title</td>
                <td>
                    <asp:TextBox runat="server" ID="txtPage" Columns="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPage"  ValidationGroup="gp1"
                        ErrorMessage="Please input page title"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="phdTitle" Visible="false">
            <tr>
                <td>Title</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" Columns="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTitle"  ValidationGroup="gp1"
                        ErrorMessage="請輸入標題"></asp:RequiredFieldValidator>
                </td>
            </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible="false">
            
            <tr>
                <td>Keyword</td>
                <td>
                    <asp:TextBox runat="server" ID="txtKeywords" Width="100%" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Description</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDescription" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </td>
            </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" Visible="false">
            <tr>
                <td>Banner</td>
                <td>
                    <asp:TextBox runat="server" ID="txtBannerUrl" Columns="55" ></asp:TextBox>
                    <asp:Button runat="server" ID="btnBannerUrl" Text="Browse Server..." UseSubmitBehavior="false" />
                </td>
            </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="phdTitle2" Visible="false">
            <tr>
                <td>Page Title</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitleUrl" Columns="80" ></asp:TextBox>
                </td>
            </tr>
            </asp:PlaceHolder>
        </table>
        
        

    Content<br />
    <table border="0" width="100%">
        <tr>
            <td>
                <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" ToolbarSet="MyToolbar" BasePath="~/fckeditor/" Width="100%" Height="600"  >
                </FCKeditorV2:FCKeditor>
<%--                <CE:Editor ID="Editor1" runat="server" Height="600">
                </CE:Editor>--%>
            </td>
            <td>

            </td>
        </tr>
    </table>

<%--                <FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server" BasePath="~/fckeditor/"  Height="600" EditorAreaCSS="css/stylesheet2.css,css/stylesheet.css">
                </FCKeditorV2:FCKeditor>--%>
                