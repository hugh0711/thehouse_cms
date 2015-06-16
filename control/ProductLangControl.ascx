<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProductLangControl.ascx.vb" Inherits="control_ProductLangControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>


<asp:HiddenField runat="server" ID="hfdLang" />


        <table border="0" width="100%">

                                
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblName">Name</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Columns="80" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        <asp:PlaceHolder runat="server" ID="phdLangButton" Visible="false">
                        <asp:Button ID="btnNameLanguage" runat="server" Text="More Language" Visible="False" />
                        </asp:PlaceHolder>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please input name"
                            ControlToValidate="txtName" ValidationGroup="gp1" CssClass="errorText" 
                            ForeColor=""></asp:RequiredFieldValidator>
                    </td>
                </tr>


                <asp:PlaceHolder runat="server" ID="DescriptionPlaceHolder" Visible="false">
                <tr>
                    <td>
                        Description
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" Columns="80" TextMode="MultiLine" Rows="8"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please input description"
                            ControlToValidate="txtDescription" ValidationGroup="gp1" CssClass="errorText" 
                            ForeColor=""></asp:RequiredFieldValidator>
                    </td>
                </tr>
<%--                <tr>
                    <td>
                        PDF Link
                    </td>
                    <td colspan="2">
                        <asp:TextBox runat="server" ID="txtLongDescription" Columns="80"></asp:TextBox><asp:Label runat="server" ID="Label4" Text=".htm" Visible="false"></asp:Label>
                        <asp:Button runat="server" ID="btnBrowse3" Text="Browse Server..." UseSubmitBehavior="false" />


                    </td>
                </tr>--%>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="HTMLDescriptionPlaceHolder" Visible="false">
                <tr>
                    <td>
                        Description
                    </td>
                    <td colspan="2">
                        <FCKeditorV2:FCKeditor ID="htmlDescription" runat="server" ToolbarSet="MyToolbar" BasePath="~/fckeditor/" Height="300"  >
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>

                </asp:PlaceHolder>

                <asp:PlaceHolder runat="server" ID="CollectionPlaceHolder" Visible="false">
                <tr>
                    <td>
                        Collection Info
                    </td>
                    <td colspan="2">
                        <FCKeditorV2:FCKeditor ID="htmlCollection" runat="server" ToolbarSet="MyToolbar" BasePath="~/fckeditor/" Height="600"  >
                        </FCKeditorV2:FCKeditor>

                    </td>
                </tr>

                </asp:PlaceHolder>                

                
                <asp:PlaceHolder runat="server" ID="TagPlaceHolder">
<%--                <tr>
                    <td>標韱</td>
                    <td>
                        <asp:UpdatePanel runat="server" ID="TagUpdatePanel" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button runat="server" ID="btnSelectTag" Text="..." /><br />
                                <asp:Label runat="server" ID="lblTag"></asp:Label>
                                <asp:HiddenField runat="server" ID="hfdTag" />
                                <asp:Button runat="server" ID="btnDummy1" style="display:none;" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnDummy1" PopupControlID="pnlTag" 
                                    BackgroundCssClass="modalBackground" CancelControlID="btnTagCancel" />
                                <asp:Panel runat="server" ID="pnlTag" CssClass="modalPopup" Width="450" Height="300" >
                                    <p><b>選擇標籤:</b></p>
                                    <asp:SqlDataSource runat="server" ID="SqlDataSourceTag" 
                                        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                                        SelectCommand="SELECT [TagID], [TagName] FROM [view_Tag] WHERE ([Lang] = @Lang) ORDER BY [TagName]">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="zh-hk" Name="Lang" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <div style="height:200px; overflow-y:auto;">
                                    <asp:CheckBoxList runat="server" ID="cblTag" DataSourceID="SqlDataSourceTag" RepeatDirection="Horizontal" RepeatColumns="4" RepeatLayout="Table" 
                                        DataTextField="TagName" DataValueField="TagID" Width="100%" />
                                    </div>
                                    <p align="right">
                                        <asp:Button runat="server" ID="btnTagOK" Text="確定" />&nbsp;
                                        <asp:Button runat="server" ID="btnTagCancel" Text="取消" />
                                    </p>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label1" Text="Image" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtImageEN" Columns="80" Visible="false"></asp:TextBox>
                        <asp:Button runat="server" ID="btnBrowseImageEN" Text="Browse Server..." UseSubmitBehavior="false" Visible="false" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
<%--                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label3" Text="圖片"></asp:Label> (中文)
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtImageTC" Columns="80"></asp:TextBox>
                        <asp:Button runat="server" ID="btnBrowseImageTC" Text="Browse Server..." UseSubmitBehavior="false" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>--%>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="FilePlaceHolder">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblFileUrl" Text="File" ></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox runat="server" ID="txtFileUrl" Columns="80" ></asp:TextBox><asp:Label runat="server" ID="lblExt" Text=".htm" Visible="false"></asp:Label>
                        <asp:Button runat="server" ID="btnBrowse" Text="Browse Server..." UseSubmitBehavior="false" />
                        <br />
                        
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label2" Text="Patent Document" Visible="false"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox runat="server" ID="txtFileUrl2" Columns="80" Visible="false"></asp:TextBox><asp:Label runat="server" ID="Label3" Text=".htm" Visible="false"></asp:Label>
                        <asp:Button runat="server" ID="btnBrowse2" Text="Browse Server..." UseSubmitBehavior="false" Visible="false" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
<%--                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblFileUrl2" Text="檔案"></asp:Label> (中文)
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFileUrl2" Columns="80"></asp:TextBox><asp:Label runat="server" ID="lblExt2" Text=".htm" Visible="false"></asp:Label>
                        <asp:Button runat="server" ID="btnBrowse2" Text="Browse Server..." UseSubmitBehavior="false" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>--%>
                </asp:PlaceHolder>
                
                



            </table>