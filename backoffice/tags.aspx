<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="tags.aspx.vb" Inherits="backoffice_tags" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>
    <asp:Label runat="server" ID="lblFunctionName" ></asp:Label> &nbsp;Tag Management
    </h2>
<asp:HiddenField runat="server" ID="hfdFunctionID" />
<asp:HiddenField runat="server" ID="hfdLang" />

<p>
    <asp:Button runat="server" ID="btnCreate" Text="Add Tag" />&nbsp;
    <asp:Button runat="server" ID="btnRefresh" Text="Refresh" />
</p>


<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        SelectCommand="SELECT TagID, Tag, TagGroup, Enabled, TagName, SortOrder FROM view_Tag WHERE (FunctionID = @FunctionID) AND (Lang = @Lang) ORDER BY SortOrder" 
        DeleteCommand="DELETE FROM [Tag] WHERE [TagID] = @TagID; DELETE FROM [TagName] WHERE [TagID] = @TagID;"
        UpdateCommand="UPDATE [Tag] SET [SortOrder] = @SortOrder WHERE [TagID] = @TagID">
    <SelectParameters>
        <asp:ControlParameter ControlID="hfdFunctionID" Name="FunctionID" 
            PropertyName="Value" Type="Int32" />
        <asp:ControlParameter ControlID="hfdLang" Name="Lang" PropertyName="Value" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="SortOrder" Type="Int32" />
        <asp:Parameter Name="TagID" Type="Int32" />
    </UpdateParameters>
    <DeleteParameters>
        <asp:Parameter Name="TagID" Type="Int32" />
    </DeleteParameters>
    </asp:SqlDataSource>

            <cc1:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataKeyField="TagID" ClientIDMode="AutoID"
                SortOrderField="SortOrder" DataSourceID="SqlDataSource1" PostBackOnReorder="False" ItemInsertLocation="End"
                CssClass="reorderList" ShowInsertItem="False">
                <ItemTemplate>
                    <div id="itemArea">
                        <table border="0" width="100%">
                            <tr>
                                <td width="60%" valign="top">
                                    <asp:Label runat="server" ID="lblCategory" Text='<%# ShowTag(Eval("Tag"), Eval("TagGroup")) %>' Font-Strikeout='<%# Not Eval("Enabled") %>'></asp:Label>
                                </td>
                                <td valign="top" nowrap="nowrap">
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="edit1" UseSubmitBehavior="false" CommandArgument='<%# Eval("TagID") %>' />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete"  UseSubmitBehavior="false" CommandArgument='<%# Eval("TagID") %>'/>
                                    <cc1:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure to delete this tag?" TargetControlID="btnDelete" />
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
                            No Tag found</p>
                    </div>
                </EmptyListTemplate>
                <ReorderTemplate>
                    <asp:Panel ID="Panel2" runat="server" CssClass="reorderItem" />
                </ReorderTemplate>

            </cc1:ReorderList>


<p>
    <asp:Button runat="server" ID="btnCreate1" Text="Add Tag" />&nbsp;
</p>
</asp:Content>

