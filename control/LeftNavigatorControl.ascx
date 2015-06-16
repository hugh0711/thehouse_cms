<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LeftNavigatorControl.ascx.vb" Inherits="control_LeftNavigatorControl" %>

<asp:HiddenField runat="server" ID="hfdLang" />
<asp:HiddenField runat="server" ID="hfdPageID" />
<asp:HiddenField runat="server" ID="hfdParentID" />
<asp:HiddenField runat="server" ID="hfdPageID3" />

<%--<asp:Label runat="server" ID="lblTitle" CssClass="title" ></asp:Label>--%>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
    
    SelectCommand="SELECT [url], [Page], [PageID], [Lang] FROM [view_Page] WHERE (([Lang] = @Lang) AND ([ParentPageID] = @ParentPageID)) AND Visible = 1 ORDER BY [SortOrder]">
    <SelectParameters>
        <asp:ControlParameter ControlID="hfdLang" Name="Lang" PropertyName="Value" 
            Type="String" />
        <asp:ControlParameter ControlID="hfdParentID" Name="ParentPageID" 
            PropertyName="Value" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:ListView runat="server" ID="ListView1" DataSourceID="SqlDataSource1">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>            
    </LayoutTemplate>
    <ItemTemplate>
        <div class="level2">
            <table border="0" cellpadding="0" cellspacing="0" style="height:54px; ">
                <tr>
<%--
                    <td width="20" valign="top" class='<%# "level2-bullet" &  IIf(Eval("PageID") = CInt(hfdPageID.Value), " selected", "") %>'>
                        <asp:Image runat="server" ImageUrl="~/images/spacer.gif" Width="20" />
                    </td>--%>
                    <td valign="middle">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("~/{0}.htm", Eval("url")) %>' Text='<%# Eval("Page") %>' CssClass='<%# IIf(Eval("PageID") = CInt(hfdPageID.Value), "selected", "") %>'></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
        <%--<div class="level2separator"></div>--%>
        
        <asp:PlaceHolder runat="server" ID="PlaceHolder1" Visible='<%# IIf(Eval("PageID") = CInt(hfdPageID.Value), true, false) %>'>
            <asp:HiddenField runat="server" ID="hfdLang2" Value='<%# Eval("Lang") %>' />
            <asp:HiddenField runat="server" ID="hfdPageID2" Value='<%# Eval("PageID") %>' />
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                SelectCommand="SELECT [url], [Page], [PageID] FROM [view_Page] WHERE (([Lang] = @Lang) AND ([ParentPageID] = @ParentPageID)) AND Visible = 1 ORDER BY [SortOrder]">
                <SelectParameters>
            <asp:ControlParameter ControlID="hfdLang2" Name="Lang" PropertyName="Value" 
                Type="String" />
            <asp:ControlParameter ControlID="hfdPageID2" Name="ParentPageID" 
                PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            
            <asp:ListView runat="server" ID="ListView2" DataSourceID="SqlDataSource2">
                <LayoutTemplate>
                    <div class="level3" >
                    <table border="0" cellpadding="0" cellspacing="0" width="140" >
                        <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>
                    </table>
                    </div>
                    <%--<div class="level2separator"></div>--%>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td width="20" valign="top"><span class="level3-bullet">&gt;</span></td>
                        <td valign="top">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("~/{0}.htm", Eval("url")) %>' Text='<%# Eval("Page") %>' CssClass='<%# IIf(Eval("PageID") = CInt(hfdPageID3.Value), "selected", "") %>'></asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
                <ItemSeparatorTemplate>
                    <tr>
                        <td colspan="2">
                            <hr width="100%" />
                        </td>
                    </tr>
                </ItemSeparatorTemplate>
            </asp:ListView>
        </asp:PlaceHolder>
    </ItemTemplate>
</asp:ListView>

