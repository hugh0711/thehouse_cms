<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false"
    CodeFile="pages.aspx.vb" Inherits="backoffice_pages" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../control/PagePathControl.ascx" tagname="PagePathControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Label runat="server" ID="lblFunctionName"></asp:Label>網頁管理</h2>
    <p>
    </p>
    <asp:HiddenField runat="server" ID="hfdLang" />

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div>
            <span class="category">
                <uc1:PagePathControl ID="PagePathControl1" runat="server" />
            </span>
            </div>
                <p>
                    <asp:Button runat="server" ID="btnUp" Text="到上層網頁" />&nbsp;<asp:Button runat="server" ID="btnRefresh" Text="  刷新 " /></p>
                <p>
                    <asp:Button runat="server" ID="btnCreate" Text="新增網頁" />&nbsp;</p>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MySqlConnection %>"
                
                SelectCommand="SELECT PageID, Page, Title, url, Enabled, SortOrder, Visible FROM view_Page WHERE (ParentPageID = @ParentPageID) AND Lang = @Lang ORDER BY SortOrder" 
                DeleteCommand="DELETE FROM [Page] WHERE (PageID = @PageID)" 
                InsertCommand="INSERT INTO [Page] (Page, ParentPageID, Enabled, SortOrder) VALUES (@Page, @ParentPageID, @Enabled, @SortOrder)" 
                
                
                UpdateCommand="UPDATE [Page] SET Enabled = @Enabled, SortOrder = @SortOrder WHERE (PageID = @PageID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblParentID" Name="ParentPageID" 
                        PropertyName="Text" Type="Int32" />
                    <asp:ControlParameter ControlID="hfdLang" Name="Lang" PropertyName="Value" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="PageID" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Enabled" />
                    <asp:Parameter Name="SortOrder" />
                    <asp:Parameter Name="PageID" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Page" />
                    <asp:ControlParameter ControlID="lblParentID" Name="ParentPageID" 
                        PropertyName="Text" Type="Int32" />
                    <asp:Parameter Name="Enabled" />
                    <asp:Parameter Name="SortOrder" />
                </InsertParameters>
            </asp:SqlDataSource>

            <asp:Label ID="lblLevel" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblParentID" runat="server" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblLang" Visible="false" ></asp:Label>
            <cc2:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataKeyField="PageID" ClientIDMode="AutoID"
                SortOrderField="SortOrder" DataSourceID="SqlDataSource1" PostBackOnReorder="False" ItemInsertLocation="End"
                CssClass="reorderList" ShowInsertItem="False">
                <ItemTemplate>
                    <div id="itemArea">
                        <table border="0" width="100%">
                            <tr>
                                <td width="60%" valign="top">
                                    <asp:Image runat="server" ImageUrl='<%# String.Format("~/product_image/category/{0}", Eval("Url")) %>' Height="30" Width="20"  ImageAlign="Middle" Visible="false" />
                                    <%--<asp:Label runat="server" ID="lblOrder" Text='<%# Bind("SortOrder") %>' Visible="false"></asp:Label>--%><asp:Label runat="server" ID="PageID" Text='<%# Eval("PageID") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblPage" Text='<%# Eval("Title") %>' Font-Strikeout='<%# Not Eval("Enabled") %>'></asp:Label>
                                    <asp:Label runat="server" Text=" (invisible)" Visible='<%# Not Eval("Visible") %>'></asp:Label>
                                </td>
                                <td valign="top" nowrap="nowrap">
                                    <asp:Button ID="btnEdit" runat="server" Text="編緝" CommandName="edit1" UseSubmitBehavior="false" CommandArgument='<%# Eval("PageID") %>' />
                                    <asp:Button ID="btnDelete" runat="server" Text="刪除" CommandName="delete"  UseSubmitBehavior="false" CommandArgument='<%# Eval("PageID") %>' Enabled='<%# EnabledFirstLevel() %>'/>
                                    <cc2:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText="確定刪除網頁?" TargetControlID="btnDelete" />
                                </td>
                                <td valign="top" nowrap="nowrap">
                                    <asp:HyperLink runat="server" ID="HyperLink2" Text="子網頁" NavigateUrl='<%# String.Format("~/backoffice/pages.aspx?page={0}", Eval("PageID")) %>' ></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                </ItemTemplate>
                <EditItemTemplate>
                    <table border="0" width="100%">
                        <tr>
                            <td width="50%" valign="top">
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.Format("~/product_image/category/{0}", Eval("Url")) %>' Height="30" Width="30"  ImageAlign="Middle" />
                                <asp:Label runat="server" ID="lblOrder" Text='<%# Bind("SortOrder") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblPageID" Text='<%# Bind("PageID") %>' Visible="false" />
                                <asp:TextBox runat="server" ID="txtPage" Text='<%# Bind("Page") %>' Width="85%" />
                            </td>
                            <td width="10%" valign="top">
                                <asp:CheckBox runat="server" ID="chkEnabled" Checked='<%# Bind("Enabled") %>' Text="使用" />
                            </td>
                            <td valign="top">
                                <asp:Button runat="server" ID="btnUpdate" CommandName="update" Text="更新" UseSubmitBehavior="false" CommandArgument='<%# Eval("PageID") %>' />
                                <asp:Button runat="server" ID="btnCancel" CommandName="cancel" Text="取消" UseSubmitBehavior="false" />
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table border="0" width="100%" bgcolor="#dddddd">
                        <tr>
                            <td width="20">&nbsp;</td>
                            <td width="50%" valign="top">
                                <asp:TextBox runat="server" ID="txtPage" Text='<%# Bind("Page") %>' Columns="30" Width="95%" />
                            </td>
                            <td width="10%" valign="top">
                                <asp:CheckBox runat="server" ID="chkEnabled" Checked='<%# Bind("Enabled") %>' Text="使用" />
                            </td>
                            <td valign="top">
                                <asp:Button runat="server" ID="btnInsert" CommandName="insert" Text="新增" UseSubmitBehavior="false" />
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
                            暫時沒有網頁</p>
                    </div>
                </EmptyListTemplate>
                <ReorderTemplate>
                    <asp:Panel ID="Panel2" runat="server" CssClass="reorderItem" />
                </ReorderTemplate>

            </cc2:ReorderList>
                <p>
                    <asp:Button runat="server" ID="btnCreate1" Text="新增網頁" />
                </p>

        </ContentTemplate>
        
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div>
                載入中...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgress2_UpdateProgressOverlayExtender"
        runat="server" ControlToOverlayID="UpdatePanel2" TargetControlID="UpdateProgress2"
        CssClass="updateProgress" />

    <p>
    </p>
</asp:Content>
