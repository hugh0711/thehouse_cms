<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="special.aspx.vb" Inherits="backoffice_special" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc2" %>
<%@ Register src="~/control/CategoryPathControl.ascx" tagname="CategoryPathControl" tagprefix="uc3" %>
<%@ Register src="~/control/ProductLangControl.ascx" tagname="ProductLangControl" tagprefix="uc4" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>

<%@ Reference control="~/control/ProductLangControl.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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

        //        function FCKUpdateLinkedField(id) {
        //            try {
        //                if (typeof (FCKeditorAPI) == "object") {
        //                    FCKeditorAPI.GetInstance(id).UpdateLinkedField();
        //                }
        //            }
        //            catch (err) {
        //            }
        //        }
    </script>
    
    <style type="text/css">
        td
        {
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<h2>
        <asp:Label runat="server" ID="lblFunctionName"></asp:Label></h2>
    <asp:HiddenField runat="server" ID="hfdSpecialID" />
    <asp:HiddenField runat="server" ID="hfdFunctionID" />
    <asp:HiddenField runat="server" ID="hfdLang" />
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="gp1" ShowSummary="false" ShowMessageBox="true" />
    <div id="admin-content-container">
        <div id="admin-content">
            <table border="0" width="100%">
                <colgroup>
                    <col class="tableCaption" width="100" />
                    <col width="500"/>
                    <col />
                </colgroup>
                <asp:PlaceHolder runat="server" ID="CategoryPlaceHolder">
                        <tr>
                            <td>
                                類別
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnSelectCategory" Text="..." />
                                <uc3:CategoryPathControl ID="CategoryPathControl1" runat="server" />
                                <asp:TextBox runat="server" ID="txtCategoryID" style="display:none;" />
                                <asp:Button runat="server" ID="btnDummy" Visible="false" />
                                <asp:ModalPopupExtender ID="btnSelectCategory_ModalPopupExtender" runat="server" PopupControlID="pnlCategory" BackgroundCssClass="modalBackground"
                                    TargetControlID="btnSelectCategory" CancelControlID="btnCategoryCancel" >
                                </asp:ModalPopupExtender>
                                <asp:Panel runat="server" ID="pnlCategory" CssClass="modalPopup" style="display:none;" >
                                    <b>選擇類別:</b><br />
                                    <br />
                                    <asp:Label runat="server" ID="lblParentID" Visible="false"></asp:Label>
                                    <div style="overflow: auto;">
                                        <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="0" Width="250" Height="250" EnableClientScript="true">
                                            <Nodes>
                                                <asp:TreeNode Text="Root" Value="0" PopulateOnDemand="true" SelectAction="Expand">
                                                </asp:TreeNode>
                                            </Nodes>
                                        </asp:TreeView>
                                    </div>
                                    <p align="right">
                                    <asp:Button runat="server" ID="btnCategoryCancel" Text="Cancel" /></p>
                                </asp:Panel>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtCategoryID" Display="Dynamic"
                                    CssClass="errorText" ErrorMessage="請選擇類別" ForeColor="" ValidationGroup="gp1"><br />請選擇類別</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                </asp:PlaceHolder>
                
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblProductCode">專頁名稱</asp:Label>
                    
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Columns="80"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="請輸入專頁名稱"
                            ControlToValidate="txtName" ValidationGroup="gp1" EnableClientScript="false"
                            CssClass="errorText" ForeColor=""><br />請輸入專頁名稱</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label4">網址</asp:Label>
                    
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblUrl"></asp:Label>
                        <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
                        <asp:Label runat="server" ID="lblUrlExt" Text=".htm"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="請輸入網址"
                            ControlToValidate="txtUrl" ValidationGroup="gp1" EnableClientScript="false"
                            CssClass="errorText" ForeColor=""><br />請輸入網址</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="valUrl" runat="server" ErrorMessage="網址已使用，請選用其他網址" 
                            CssClass="errorText" ValidationGroup="gp1"></asp:CustomValidator>
                        <asp:Image runat="server" ID="imgUrl" Visible="false"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label1">內容</asp:Label>
                    
                    </td>
                    <td>
                        <FCKeditorV2:FCKeditor ID="htmContent" runat="server" ToolbarSet="MyToolbar" BasePath="~/fckeditor/" Height="500"  >
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>

                <asp:PlaceHolder runat="server" Visible="false">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label2">內容顏色</asp:Label>
                    
                    </td>
                    <td>
                        背景顏色: <asp:TextBox runat="server" ID="txtContentBGColor"></asp:TextBox>
                    </td>
                </tr>
                </asp:PlaceHolder>

                <asp:PlaceHolder runat="server" ID="DatePlaceHolder" Visible="false">
                <tr>
                    <td>
                        展出日期</td>
                    <td>
                        <asp:TextBox ID="txtStartSellDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtStartSellDate_CalendarExtender" runat="server" TargetControlID="txtStartSellDate"
                            Format="d/M/yyyy">
                        </asp:CalendarExtender>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtStartSellDate" Display="Dynamic"
                            ErrorMessage="展出日期不正確" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Date"
                            ValidationGroup="gp1" CssClass="errorText" ForeColor=""><br />展出日期不正確</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                       結束日期</td>
                    <td>
                        <asp:TextBox ID="txtEndSellDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtEndSellDate_CalendarExtender" runat="server" TargetControlID="txtEndSellDate"
                            Format="d/M/yyyy">
                        </asp:CalendarExtender>
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtEndSellDate"
                            ErrorMessage="結束日期不正確" Operator="DataTypeCheck" Display="Dynamic"
                            SetFocusOnError="True" Type="Date"
                            ValidationGroup="gp1" CssClass="errorText"><br />結束日期不正確</asp:CompareValidator>
                    </td>
                </tr>
                </asp:PlaceHolder>

                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:CheckBox ID="chkEnabled" runat="server" Text="Enabled" Checked="true" />
                        <asp:Label runat="server" ID="lblSortOrder" Visible="false"></asp:Label>
                    </td>
                </tr>
                <asp:PlaceHolder runat="server" ID="ExtendPlaceHolder" Visible="false">
	            <tr>
		            <td colspan="2">
                        <table border="0" width="100%">
                            <tr>
                                <td width="50%">
                                                    影片
			                        <asp:SqlDataSource runat="server" ID="dsVideo" 
                                        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                                        
                                        InsertCommand="INSERT INTO SpecialVideo (SpecialID, Title, VideoUrl, SortOrder) VALUES (@SpecialID, @Title, @VideoUrl, @SortOrder)"
                                        SelectCommand="SELECT VideoUrl, Title, id FROM [SpecialVideo] WHERE (SpecialID = @SpecialID) ORDER BY [SortOrder]"
                                        UpdateCommand="UPDATE SpecialVideo SET VideoUrl = @VideoUrl, Title = @Title, SortOrder = @SortOrder WHERE id = @id"
                                        DeleteCommand="DELETE SpecialVideo WHERE id = @id">
                                        <InsertParameters>
                                            <asp:Parameter Name="SortOrder" />
                                            <asp:ControlParameter ControlID="hfdSpecialID" Name="SpecialID" PropertyName="Value" Type="Int32" />
                                            <asp:Parameter Name="VideoUrl" />
                                            <asp:Parameter Name="Title" />
                                        </InsertParameters>
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="hfdSpecialID" Name="SpecialID" PropertyName="Value" Type="Int32" />
                                        </SelectParameters>
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" />
                                        </DeleteParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="SortOrder" />
                                            <asp:Parameter Name="id" />
                                            <asp:Parameter Name="VideoUrl" />
                                            <asp:Parameter Name="Title" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>

                                   <asp:ReorderList ID="rlVideo" runat="server" AllowReorder="True" DataKeyField="id" Width="100%"
                                        SortOrderField="SortOrder" PostBackOnReorder="false" ClientIDMode="AutoID" DataSourceID="dsVideo"
                                        CssClass="reorderList" ShowInsertItem="true">
                                        <ItemTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="95%">

                                                <tr>
                                                    <td valign="top">
                                                        <asp:HyperLink runat="server" ImageUrl='<%# VideoClass.GetPreview(VideoClass.GetVideoID(Eval("VideoUrl"))) %>'
                                                            NavigateUrl='<%# VideoClass.GetVideoUrl(VideoClass.GetVideoID(Eval("VideoUrl")))%>' Target="_blank"></asp:HyperLink>
                                                        <asp:Label runat="server" Text='<%# Eval("Title") %>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <asp:Button runat="server" ID="btnEdit" Text="編緝" UseSubmitBehavior="false" CommandName="edit" CommandArgument='<%# Eval("id") %>' Width="60" />
                                                        <asp:Button runat="server" ID="Button1" Text="刪除" UseSubmitBehavior="false" CommandName="delete" CommandArgument='<%# Eval("id") %>' Width="60" />
                                                        <asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText='<%# String.Format("確定刪除{0}?", Eval("Title")) %>' TargetControlID="btnDelete" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="95%">
                                                <tr>
                                                    <td valign="top">
                                                        影片網址: <asp:TextBox runat="server" ID="txtVideoUrl" Text='<%# Bind("VideoUrl") %>' Columns="40"></asp:TextBox><br />
                                                        <asp:TextBox ID="Label3" runat="server" Text='<%# Bind("Title") %>' Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <asp:Button runat="server" ID="btnUpdate" Text="更新" UseSubmitBehavior="false" CommandName="update" Width="60" />
                                                        <asp:Button runat="server" ID="btnCancel" Text="取消" UseSubmitBehavior="false" CommandName="cancel" Width="60" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="95%">
                                                <tr>
                                                    <td valign="top">
                                                        影片網址: <asp:TextBox runat="server" ID="txtVideoUrl" Text='<%# Bind("VideoUrl") %>' Columns="40"></asp:TextBox><br />
                                                        <asp:TextBox ID="Label3" runat="server" Text='<%# Bind("Title") %>' Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <asp:Button runat="server" ID="btnUpdate" Text="新增" UseSubmitBehavior="false" CommandName="insert" Width="60" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </InsertItemTemplate>
                                        <DragHandleTemplate>
                                            <div class="reorderhandle">
                                            </div>
                                        </DragHandleTemplate>
                                        <EmptyListTemplate>
                                            <div style="width: 100%; height: 100px; margin-top: 30px;">
                                                <p align="center">
                                                   沒有影片</p>
                                            </div>
                                        </EmptyListTemplate>
                                        <ReorderTemplate>
                                            <asp:Panel ID="Panel2" runat="server" CssClass="reorderItem" />
                                        </ReorderTemplate>
                                    </asp:ReorderList>


                                </td>
                                <td width="50%">
                                
                                                    相片
			                        <asp:SqlDataSource runat="server" ID="dsImage" 
                                        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                                        
                                        InsertCommand="INSERT INTO SpecialImage (SpecialID, Title, ImageUrl, SortOrder) VALUES (@SpecialID, @Title, @ImageUrl, @SortOrder)"
                                        SelectCommand="SELECT ImageUrl, Title, id, SpecialID FROM [SpecialImage] WHERE (SpecialID = @SpecialID) ORDER BY [SortOrder]"
                                        UpdateCommand="UPDATE SpecialImage SET Title = @Title, SortOrder = @SortOrder WHERE id = @id"
                                        DeleteCommand="DELETE SpecialImage WHERE id = @id">
                                        <InsertParameters>
                                            <asp:Parameter Name="SortOrder" />
                                            <asp:ControlParameter ControlID="hfdSpecialID" Name="SpecialID" PropertyName="Value" Type="Int32" />
                                            <asp:Parameter Name="ImageUrl" />
                                            <asp:Parameter Name="Title" />
                                        </InsertParameters>
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="hfdSpecialID" Name="SpecialID" PropertyName="Value" Type="Int32" />
                                        </SelectParameters>
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" />
                                        </DeleteParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="SortOrder" />
                                            <asp:Parameter Name="id" />
                                            <asp:Parameter Name="Title" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>

                                   <asp:ReorderList ID="rlImage" runat="server" AllowReorder="True" DataKeyField="id" Width="100%"
                                        SortOrderField="SortOrder" PostBackOnReorder="false" ClientIDMode="AutoID" DataSourceID="dsImage"
                                        CssClass="reorderList" ShowInsertItem="true">
                                        <ItemTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="95%">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:HyperLink runat="server" ImageUrl='<%# IO.Path.Combine(ConfigurationManager.AppSettings("SpecialThumbnailPath") ,(Eval("ImageUrl"))) %>' 
                                                            NavigateUrl='<%# String.Format("~/backoffice/special_upload.aspx?imgid={0}", Eval("id") ) %>' />
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Title") %>' ></asp:Label>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <asp:Button runat="server" ID="btnEdit" Text="編緝" UseSubmitBehavior="false" CommandName="edit" CommandArgument='<%# Eval("id") %>' Width="60" />
                                                        <asp:Button runat="server" ID="Button1" Text="刪除" UseSubmitBehavior="false" CommandName="delete" CommandArgument='<%# Eval("id") %>' Width="60" />
                                                        <asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText='<%# String.Format("確定刪除{0}?", Eval("Title")) %>' TargetControlID="btnDelete" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="95%">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# IO.Path.Combine(ConfigurationManager.AppSettings("SpecialThumbnailPath") ,(Eval("ImageUrl"))) %>' />
                                                        <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>' Columns="30"></asp:TextBox>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <asp:Button runat="server" ID="btnUpdate" Text="更新" UseSubmitBehavior="false" CommandName="update" Width="60" />
                                                        <asp:Button runat="server" ID="btnCancel" Text="取消" UseSubmitBehavior="false" CommandName="cancel" Width="60" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="95%">
                                                <tr>
                                                    <td valign="top">
                                                     </td>
                                                    <td align="right" valign="top">
                                                        <asp:Button runat="server" ID="btnEdit" Text="新增" UseSubmitBehavior="false" CommandName="insert1" Width="60" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </InsertItemTemplate>
                                        <DragHandleTemplate>
                                            <div class="reorderhandle">
                                            </div>
                                        </DragHandleTemplate>
                                        <EmptyListTemplate>
                                            <div style="width: 100%; height: 100px; margin-top: 30px;">
                                                <p align="center">
                                                   沒有相片</p>
                                            </div>
                                        </EmptyListTemplate>
                                        <ReorderTemplate>
                                            <asp:Panel ID="Panel2" runat="server" CssClass="reorderItem" />
                                        </ReorderTemplate>
                                    </asp:ReorderList>


                                </td>
                            </tr>
                        </table>
                    </td>
	            </tr>
	            </asp:PlaceHolder>

                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="儲存" ValidationGroup="gp1" />
                        <asp:Button ID="btnSaveBack" runat="server" Text="儲存並返回" ValidationGroup="gp1" />
                        <asp:Button ID="btnClose" runat="server" Text="返回" />
                        <asp:Button runat="server" ID="btnDelete" Text="刪除" />
                        <asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText="是否確定刪除?"
                            TargetControlID="btnDelete">
                        </asp:ConfirmButtonExtender>
                    </td>
                </tr>
                
                

            </table>
        </div>
    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
		<ProgressTemplate></ProgressTemplate>
    </asp:UpdateProgress>
    <cc2:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" TargetControlID="UpdateProgress1" ControlToOverlayID="UpdatePanel1" CssClass="updateProgress" OverlayType="Control" />
    <p>
    </p>
<br />
<br />
<br />

</asp:Content>
