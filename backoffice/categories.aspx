<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false"
    CodeFile="categories.aspx.vb" Inherits="backoffice_categories" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../control/CategoryPathControl.ascx" tagname="CategoryPathControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:PlaceHolder runat="server" ID="phdDefaultTitle">
            <asp:Label runat="server" ID="lblFunctionName"></asp:Label>類別管理
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phdTVTitle">
            <asp:Label runat="server" ID="lblTVChannel"></asp:Label>
        </asp:PlaceHolder>
    </h2>
    <p>
    </p>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div>
            <span class="category">
                <uc1:CategoryPathControl ID="CategoryPathControl1" runat="server" />
            </span>
            </div>
                <p>
                    <asp:Button runat="server" ID="btnUp" Text="到上層類別" />&nbsp;<asp:Button runat="server" ID="btnRefresh" Text="  刷新 " /></p>
                <p>
                    <asp:Button runat="server" ID="btnCreate" Text="新增類別" />&nbsp;</p>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MySqlConnection %>"
                
                SelectCommand="SELECT [CategoryID], [Category], [ParentID], [SortOrder], [Enabled], [Url], [FunctionID],[CategoryName] FROM [view_Category] WHERE ([ParentID] = @ParentID) AND [Lang] = @Lang and [FunctionID] = @FunctionID ORDER BY [SortOrder]" 
                DeleteCommand="DELETE FROM Category WHERE (CategoryID = @CategoryID)" 
                InsertCommand="INSERT INTO Category(Category, ParentID, Enabled, SortOrder) VALUES (@Category, @ParentID, @Enabled, @SortOrder)" 
                
                UpdateCommand="UPDATE Category SET Category = @Category, Enabled = @Enabled, SortOrder = @SortOrder WHERE (CategoryID = @CategoryID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblParentID" Name="ParentID" 
                        PropertyName="Text" Type="Int32" />
                    <asp:ControlParameter ControlID="lblLang" Name="Lang" 
                        PropertyName="Text" Type="String" />
                    <asp:QueryStringParameter DefaultValue="1" Name="FunctionID" 
                        QueryStringField="fn" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="CategoryID" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Category" />
                    <asp:Parameter Name="Enabled" />
                    <asp:Parameter Name="SortOrder" />
                    <asp:Parameter Name="CategoryID" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Category" />
                    <asp:ControlParameter ControlID="lblParentID" Name="ParentID" 
                        PropertyName="Text" Type="Int32" />
                    <asp:Parameter Name="Enabled" />
                    <asp:Parameter Name="SortOrder" />
                </InsertParameters>
            </asp:SqlDataSource>

            <asp:Label ID="lblLevel" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblParentID" runat="server" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblLang" Visible="false" ></asp:Label>
            <cc2:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataKeyField="CategoryID" ClientIDMode="AutoID"
                SortOrderField="SortOrder" DataSourceID="SqlDataSource1" PostBackOnReorder="False" ItemInsertLocation="End"
                CssClass="reorderList" ShowInsertItem="False">
                <ItemTemplate>
                    <div id="itemArea">
                        <table border="0" width="100%">
                            <tr>
                                <td width="60%" valign="top">
                                    <asp:Image runat="server" ImageUrl='<%# GetThumbnail(Eval("URL")) %>' Height="30" ImageAlign="Middle" Visible='<%# ViewState("FunctionSettings").HasCategoryImage %>' />
                                    <%--<asp:Label runat="server" ID="lblOrder" Text='<%# Bind("SortOrder") %>' Visible="false"></asp:Label>--%><asp:Label runat="server" ID="CategoryID" Text='<%# Eval("CategoryID") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblCategory" Text='<%# Eval("CategoryName").ToString() %>' Font-Strikeout='<%# Not Eval("Enabled") %>'></asp:Label>
                                </td>
                                <td valign="top" nowrap="nowrap">
                                    <asp:Button ID="btnEdit" runat="server" Text="編緝" CommandName="edit1" UseSubmitBehavior="false" CommandArgument='<%# Eval("CategoryID") %>' />
                                    <asp:Button ID="btnDelete" runat="server" Text="刪除" CommandName="delete"  UseSubmitBehavior="false" CommandArgument='<%# Eval("CategoryID") %>'/>
                                    <cc2:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText="確定刪除類別?" TargetControlID="btnDelete" />
                                </td>
                                <td valign="top" nowrap="nowrap">
                                    <asp:HyperLink runat="server" ID="HyperLink1" Text='<%# GetSubCategory() %>' NavigateUrl='<%# String.Format("~/backoffice/categories.aspx?category={0}&fn={1}", Eval("CategoryID"), Eval("FunctionID")) %>' Visible='<%# (Convert.toInt32(lblLevel.Text) < ViewState("FunctionSettings").CategoryMaxLevel) %>'></asp:HyperLink>
                                    <asp:HyperLink runat="server" ID="HyperLink2" Text='<%# GetProduct() %>' NavigateUrl='<%# String.Format("~/backoffice/albums.aspx?category={0}&fn={1}", Eval("CategoryID"), Eval("FunctionID")) %>' Visible='<%# IsShowEpisode() %>'></asp:HyperLink>
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
                                <asp:Label runat="server" ID="lblCategoryID" Text='<%# Bind("CategoryID") %>' Visible="false" />
                                <asp:TextBox runat="server" ID="txtCategory" Text='<%# Bind("CategoryName") %>' Width="85%" />
                            </td>
                            <td width="10%" valign="top">
                                <asp:CheckBox runat="server" ID="chkEnabled" Checked='<%# Bind("Enabled") %>' Text="使用" />
                            </td>
                            <td valign="top">
                                <asp:Button runat="server" ID="btnUpdate" CommandName="update" Text="更新" UseSubmitBehavior="false" CommandArgument='<%# Eval("CategoryID") %>' />
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
                                <asp:TextBox runat="server" ID="txtCategory" Text='<%# Bind("CategoryName") %>' Columns="30" Width="95%" />
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
                            暫時沒有類別</p>
                    </div>
                </EmptyListTemplate>
                <ReorderTemplate>
                    <asp:Panel ID="Panel2" runat="server" CssClass="reorderItem" />
                </ReorderTemplate>

            </cc2:ReorderList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div>
                Loading...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgress2_UpdateProgressOverlayExtender"
        runat="server" ControlToOverlayID="UpdatePanel2" TargetControlID="UpdateProgress2"
        CssClass="updateProgress" />
                <p>
                    <asp:Button runat="server" ID="btnCreate1" Text="新增類別" />
                </p>
    <p>
    </p>
</asp:Content>
