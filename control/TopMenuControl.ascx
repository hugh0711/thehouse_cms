<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TopMenuControl.ascx.vb" Inherits="control_TopMenuControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--
<table border="0" cellpadding="0" cellspacing="0" width="772">
    <tr>
        <td><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" ImageUrl="~/images/menu_profile.jpg" Width="117" Height="35" /></td>
        <td><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="#" ImageUrl="~/images/menu_core_business.jpg" Width="131" Height="35" /></td>
        <td><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="#" ImageUrl="~/images/menu_innovative_expertise.jpg" Width="132" Height="35" /></td>
        <td><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="#" ImageUrl="~/images/menu_sustainable_development.jpg" Width="130" Height="35" /></td>
        <td><asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="#" ImageUrl="~/images/menu_news.jpg" Width="132" Height="35" /></td>
        <td><asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="#" ImageUrl="~/images/menu_join_us.jpg" Width="130" Height="35" /></td>
    </tr>
</table>
--%>

<script type="text/javascript">
    var left, top;

    $(document).ready(function() {
        var e = $("[type=level2]:first");
        left = $(e).offset().left;
        top = $(e).offset().top + $(e).height();
        $("[type=level2]").mouseover(function() {
            var pageid = $(this).attr("pageid");
            $("[type=level2]").removeClass("menu2-selected");
            $(this).addClass("menu2-selected");
            $("[type=level3]").hide();
            $("[type=level3][pageid=" + pageid + "]").css({ "left": (left - 10) + "px", "top": top + "px" }).show();
        });
        $("[type=level3]").mouseleave(function() {
            $("[type=level2]").removeClass("menu2-selected");
            $("[type=level3]").hide();
        });
    });

    $(window).resize(function() {
        var e = $("[type=level2]:first");
        left = $(e).offset().left;
        top = $(e).offset().top + $(e).height();
        $("[type=level3]").css({ "left": (left - 10) + "px", "top": top + "px" });
    });
</script>

<asp:HiddenField runat="server" ID="hfdLang" />
<asp:HiddenField runat="server" ID="hfdPageID" />
<asp:HiddenField runat="server" ID="hfdParentID" Value="0" />

<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
    
    SelectCommand="SELECT [url], [Page], [PageID], [Lang] FROM [view_Page] WHERE (([Lang] = @Lang) AND ([ParentPageID] = @ParentPageID)) ORDER BY [SortOrder]">
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
        <asp:HiddenField runat="server" ID="hfdLang2" Value='<%# Eval("Lang") %>' />
        <asp:HiddenField runat="server" ID="hfdPageID2" Value='<%# Eval("PageID") %>' />
        <asp:Panel runat="server" ID="pnlMenu" Width='<%# GetWidth(0) %>' pageid='<%# Eval("PageID") %>' type="level2" >
        <asp:HyperLink runat="server" ID="HyperLink1" Text='<%# Eval("Page") %>' NavigateUrl='<%# String.Format("~/{0}.htm", Eval("url")) %>' ></asp:HyperLink>
        </asp:Panel>
<%--        <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" PopupControlID="pnlPopup" TargetControlID="pnlMenu" PopupPosition="Bottom" OffsetY="3" >
        </cc1:HoverMenuExtender>--%>

        <asp:Panel runat="server" ID="pnlPopup" style="display:none;" CssClass="top-menu3" pageid='<%# Eval("PageID") %>' type="level3">

            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                
                SelectCommand="SELECT [url], [Page], [PageID], [Lang], [ParentPageID] FROM [view_Page] WHERE (([Lang] = @Lang) AND ([ParentPageID] = @ParentPageID)) ORDER BY [SortOrder]">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfdLang2" Name="Lang" PropertyName="Value" 
                        Type="String" />
                    <asp:ControlParameter ControlID="hfdPageID2" Name="ParentPageID" 
                        PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:ListView runat="server" ID="ListView1" DataSourceID="SqlDataSource2">
                <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <asp:Panel runat="server" ID="pnlMenu2" Width='<%# GetWidth(Eval("ParentPageID")) %>'>
                    <asp:HyperLink runat="server" ID="HyperLink1" Text='<%# Eval("Page") %>' NavigateUrl='<%# String.Format("~/{0}.htm", Eval("url")) %>' ></asp:HyperLink>
                    </asp:Panel>
                </ItemTemplate>
                <ItemSeparatorTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spacer.gif" Width="1" Height="30" BackColor="#aaaaaa" style="float:left" />
                </ItemSeparatorTemplate>
            </asp:ListView>
        
        </asp:Panel>
    </ItemTemplate>
    <ItemSeparatorTemplate>
        <asp:Image runat="server" ImageUrl="~/images/spacer.gif" Width="1" Height="35" BackColor="white" style="float:left" />
    </ItemSeparatorTemplate>
</asp:ListView>