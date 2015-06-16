<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="specials.aspx.vb" Inherits="backoffice_specials" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../control/CategoryPathControl.ascx" tagname="CategoryPathControl" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        td { vertical-align:top; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Label runat="server" ID="lblFunctionName"></asp:Label>管理</h2>
        <%--<asp:LinkButton runat="server" ID="btnCategory" Text="跳到類別"></asp:LinkButton>--%>
        <asp:HiddenField runat="server" ID="hfdLang" />
        <asp:HiddenField runat="server" ID="hfdFunctionID" />
        <asp:HiddenField runat="server" ID="hfdFunctionName" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblCategoryID" Visible="False" />
            
<%--            <asp:RadioButtonList runat="server" ID="rblCategory" RepeatDirection="Horizontal">
                <asp:ListItem Value="all">顯示所有產品</asp:ListItem>
                <asp:ListItem Value="cat" Selected="True">以類別顯示</asp:ListItem>
            </asp:RadioButtonList>--%>
            <asp:PlaceHolder runat="server" ID="CategoryPlaceHolder">
                <asp:Button runat="server" ID="btnSelectCategory" Text="..." />
                <span class="category">
                <asp:Label runat="server" ID="lblAllCategory" Text=" 所有類別"></asp:Label>
                <uc2:CategoryPathControl ID="CategoryPathControl1" runat="server" />
                </span>
                <asp:Button runat="server" ID="btnDummy" Visible="false" />
                <cc2:ModalPopupExtender ID="btnSelectCategory_ModalPopupExtender" runat="server" PopupControlID="pnlCategory" BackgroundCssClass="modalBackground"
                    TargetControlID="btnSelectCategory" CancelControlID="btnCategoryCancel" >
                </cc2:ModalPopupExtender>
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
            </asp:PlaceHolder>
            <asp:Panel runat="server" ID="pnlProductSearch" DefaultButton="btnSearch">
					專頁名稱:
					<asp:TextBox ID="txtProductName" runat="server" Width="300px"></asp:TextBox>
					<asp:Button ID="btnSearch" runat="server" Text="搜尋" Width="80px" />
					<asp:Button ID="btnReset" runat="server" Text="重設" Width="80px" />
            </asp:Panel>
            
            <br />
            <br />

            <asp:SqlDataSource ID="SqlDataSourceByCategory" runat="server" ConnectionString="<%$ ConnectionStrings:MySqlConnection %>"
                SelectCommand="SELECT SpecialID, Name, Enabled, Category FROM view_Special WHERE (CategoryID = @CategoryID) ORDER BY CategoryID, SortOrder"
                
                UpdateCommand="UPDATE Special SET SortOrder = @SortOrder WHERE (SpecialID = @SpecialID)" 
                DeleteCommand="DELETE FROM Special WHERE (SpecialID = @SpecialID)">
                <SelectParameters>
                    <asp:ControlParameter Name="CategoryID" ControlID="lblCategoryID" DefaultValue="1"
                        PropertyName="Text" Type="Int32" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="SpecialID" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SortOrder" />
                    <asp:Parameter Name="SpecialID" />
                </UpdateParameters>
            </asp:SqlDataSource>
            
            <asp:SqlDataSource ID="SqlDataSourceAll" runat="server" ConnectionString="<%$ ConnectionStrings:MySqlConnection %>"
                
                SelectCommand="SELECT SpecialID, Name, Enabled, Category FROM view_Special ORDER BY CategoryID, SortOrder" 
                UpdateCommand="UPDATE Special SET SortOrder = @SortOrder WHERE (SpecialID = @SpecialID)" 
                DeleteCommand="DELETE FROM Special WHERE (SpecialID = @SpecialID)">
                
                <SelectParameters>
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="SpecialID" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SortOrder" />
                    <asp:Parameter Name="SpecialID" />
                </UpdateParameters>
            </asp:SqlDataSource>
                
            <asp:Button ID="btnNew" runat="server" Text="新增專頁" />&nbsp;
            <asp:Button ID="btnRefresh" runat="server" Text="刷新" />
            <br />
            <cc2:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataKeyField="SpecialID"
                SortOrderField="SortOrder" PostBackOnReorder="false" ClientIDMode="AutoID"
                CssClass="reorderList" ShowInsertItem="false">
                <ItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="95%">
                        <tr>
                            <td valign="top">
                                <table border="0">
                                    <asp:PlaceHolder ID="PlaceHolder3" runat="server" Visible='<%# Not ViewState("ByCategory") And ViewState("FunctionSettings").hasCategory %>'>
                                    <tr>
                                        <td nowrap="nowrap"><b>類別:</b></td>
                                        <td>
                                            <asp:Label runat="server" ID="Label2" Text='<%# Eval("Category") %>' Font-Bold="true" />
                                        </td>
                                    </tr>
                                    </asp:PlaceHolder>
                                    <tr>
                                        <td>名稱:</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblName" Text='<%# Eval("Name") %>' /> <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <asp:Button runat="server" ID="btnEdit" Text="編緝" UseSubmitBehavior="false" CommandName="edit1" CommandArgument='<%# Eval("SpecialID") %>' Width="60" />
                                <br />
                                <asp:Button runat="server" ID="btnDelete" Text="刪除" UseSubmitBehavior="false" CommandName="delete1" CommandArgument='<%# Eval("SpecialID") %>' Width="60" />
                                <cc2:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText='<%# String.Format("確定刪除{0}?", hfdFunctionName.Value) %>' TargetControlID="btnDelete" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <DragHandleTemplate>
                    <div class="reorderhandle">
                    </div>
                </DragHandleTemplate>
                <EmptyListTemplate>
                    <div style="width: 100%; height: 100px; margin-top: 30px;">
                        <p align="center">
                           暫時沒有專頁</p>
                    </div>
                </EmptyListTemplate>
                <ReorderTemplate>
                    <asp:Panel ID="Panel2" runat="server" CssClass="reorderItem" />
                </ReorderTemplate>
            </cc2:ReorderList>
            <br />
            <asp:Button ID="btnNew2" runat="server" Text="新增專頁" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server"
        ControlToOverlayID="UpdatePanel2" TargetControlID="UpdateProgress1" CssClass="updateProgress" />
    <p>
        &nbsp;</p>
</asp:Content>

