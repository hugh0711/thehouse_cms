<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="album.aspx.vb" Inherits="backoffice_album" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<%@ Register src="../control/CategoryPathControl.ascx" tagname="CategoryPathControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/lightbox.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/lightbox.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>




    <asp:Label runat="server" ID="lblCategoryID" Visible="False" />

    <asp:PlaceHolder runat="server" ID="CategoryPlaceHolder">
                <asp:Button runat="server" ID="btnSelectCategory" Text="..." />
                <span class="category">
                <asp:Label runat="server" ID="lblAllCategory" Text=" 所有類別"></asp:Label>
                <uc2:CategoryPathControl ID="CategoryPathControl1" runat="server" />
                </span>
                <asp:Button runat="server" ID="btnDummy" Visible="false" />
                <asp:ModalPopupExtender ID="btnSelectCategory_ModalPopupExtender" runat="server" PopupControlID="pnlCategory" BackgroundCssClass="modalBackground"
                    TargetControlID="btnSelectCategory" CancelControlID="btnCategoryCancel" >
                </asp:ModalPopupExtender>
                <asp:Panel runat="server" ID="pnlCategory" CssClass="modalPopup" style="display:none;" >
                    <b>選擇類別:</b><br />
                    <br />
                    <asp:Label runat="server" ID="lblParentID" Visible="false"></asp:Label>
                    <div style="overflow: auto;">
                        <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="0" Width="250" Height="250">
                            <Nodes>
                                <asp:TreeNode Text="主目錄" Value="0" PopulateOnDemand="true" SelectAction="Expand">
                                </asp:TreeNode>
                            </Nodes>
                        </asp:TreeView>
                        
                    </div>


                    <p align="right">
                        <asp:Button runat="server" ID="btnAllCategory" Text="所有類別" />&nbsp;
                        <asp:Button runat="server" ID="btnCategoryCancel" Text="取消" />
                    </p>

                </asp:Panel>
<br />
 <asp:Label runat="server" ID="lbl_treeViewCheck" Text="請選舉類別" Visible="false" ForeColor="Red"></asp:Label>
            </asp:PlaceHolder>
    
   




<h2><asp:Label runat="server" ID="lblTitle" Text="簿相"></asp:Label></h2>


<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfdAlbumID" />
        <table border="0" width="100%">
            <colgroup>
                <col width="100" />
                <col />
                <col />
            </colgroup>
            <tr>
                <td>相簿名稱</td>
                <td>
                    <asp:TextBox runat="server" ID="txtAlbumName" Columns="60"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAlbumName"  ValidationGroup="gp1" Display="Dynamic" CssClass="errorText"
                        ErrorMessage="請輸入相簿名稱"><br />請輸入相簿名稱</asp:RequiredFieldValidator>
                </td>
                <td rowspan="4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:HyperLink runat="server" rel="lightbox" ID="lnkCover"></asp:HyperLink>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>相簿介紹</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDescription" Columns="60" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"  ValidationGroup="gp1" Display="Dynamic" Enabled="false" CssClass="errorText"
                        ErrorMessage="請輸入相簿名稱"><br />請輸入相簿介紹</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>相簿日期</td>
                <td>
                    <asp:TextBox runat="server" ID="txtAlbumDate"></asp:TextBox> <em>例: 2012-09-23</em>
                    <asp:RequiredFieldValidator  runat="server" ControlToValidate="txtAlbumDate" ValidationGroup="gp1" Display="Dynamic" CssClass="errorText"
                        ErrorMessage="請輸入相簿日期"><br />請輸入相簿日期</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtAlbumDate" ValidationGroup="gp1" Display="Dynamic" CssClass="errorText"
                        Type="Date" Operator="DataTypeCheck" ErrorMessage="日期格式錯誤"><br />日期格式錯誤</asp:CompareValidator>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAlbumDate" Format="yyyy-MM-dd">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td><asp:CheckBox runat="server" ID="chkEnabled" Text="啟用" /></td>
            </tr>
        </table>

        <div>
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:Button runat="server" ID="btnSave" Text="儲存" ValidationGroup="gp1" />
            <asp:Button runat="server" ID="btnSaveBack" Text="儲存並返回" ValidationGroup="gp1" />
            <asp:Button runat="server" ID="btnBack" Text="返回" CausesValidation="false" />
            <asp:Button runat="server" ID="btnPhotos" Text="上傳相片" CausesValidation="false" />
            <asp:Button runat="server" ID="btnDeleteAlbum" Text="刪除相簿" CausesValidation="false" style="float:right;" />
            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnDeleteAlbum" ConfirmText="是否確定刪除整個相簿? (刪除後不能回復)" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" ControlToOverlayID="UpdatePanel1" TargetControlID="UpdateProgress1" CssClass="updateProgress" />


       <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="AjaxFileUpload1" EventName="UploadComplete" />
        </Triggers>--%>
        <ContentTemplate>
            <asp:ListView runat="server" ID="lstPhoto" DataKeyNames="PhotoID">
                <LayoutTemplate>
                    <fieldset>
                        <legend>相片</legend>
                        <div id="album-photos">
                        <ul >
                        <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>
                        </ul>
                        </div>
                    </fieldset>
                </LayoutTemplate>
                <ItemTemplate>
                    <li>
                        <div class="photo">
                            <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl='<%# IO.Path.Combine(Utility.GetAlbumPath(hfdAlbumID.Value, ImageClass.ImageSize.Thumbnail), Eval("PhotoName")) %>' 
                                NavigateUrl='<%# IO.Path.Combine(AlbumPath, Eval("PhotoName")) %>'  rel="lightbox[photo]" 
                                ToolTip='<%# Eval("Description") %>' />
                        </div>
                        <div>
                            <asp:Button runat="server" CommandName="cover" Text="封面" CommandArgument='<%# Eval("PhotoName") %>' CausesValidation="false" />
                            <asp:Button runat="server" CommandName="edit" Text="編輯" CausesValidation="false" />
                            <asp:Button runat="server" CommandName="delete" CommandArgument='<%# Eval("PhotoID") %>' Text="刪除" ID="btnDelete" CausesValidation="false" />
                            <asp:ConfirmButtonExtender runat="server" TargetControlID="btnDelete" ConfirmText="是否確定刪除相片?" />
                        </div>
                    </li>
                </ItemTemplate>
                <EditItemTemplate>
                    <li>
                        <div class="photo">
                            <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl='<%# IO.Path.Combine(Utility.GetAlbumPath(hfdAlbumID.Value, ImageClass.ImageSize.Thumbnail), Eval("PhotoName")) %>' 
                                    NavigateUrl='<%# IO.Path.Combine(AlbumPath, Eval("PhotoName")) %>'  rel="lightbox[photo]" 
                                    ToolTip='<%# Eval("Description") %>' CssClass="edit-mode" /><br />
                            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="3" Text='<%# Bind("Description") %>'></asp:TextBox>
                        
                        </div>
                        <div>
                            <asp:Button ID="Button1" runat="server" CommandName="update" Text="更新" CausesValidation="false" />
                            <asp:Button ID="Button2" runat="server" CommandName="cancel" Text="取消" CausesValidation="false" />
                        </div>
                    </li>
                </EditItemTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate></ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender2" runat="server" TargetControlID="UpdateProgress2" ControlToOverlayID="UpdatePanel2" CssClass="updateProgress" />

</asp:Content>

