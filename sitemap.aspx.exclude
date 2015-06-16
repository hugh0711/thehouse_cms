<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPageInner.master" AutoEventWireup="false" CodeFile="sitemap.aspx.vb" Inherits="sitemap" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link media="screen" href="css/sitemap.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
        $(function() {
            $("li:has(ul)").click(function(event) {
                if (this == event.target) {
                    if ($(this).children("ul").is(":hidden")) {
                        $(this).children("ul").show();
                        if ($(this).hasClass("n1-close")) {
                            $(this).removeClass("n1-close").addClass("n1-open");
                        };
                        if ($(this).hasClass("n2-close")) {
                            $(this).removeClass("n2-close").addClass("n2-open");
                        };
                    } else {
                        $(this).children("ul").hide();
                        if ($(this).hasClass("n1-open")) {
                            $(this).removeClass("n1-open").addClass("n1-close");
                        };
                        if ($(this).hasClass("n2-open")) {
                            $(this).removeClass("n2-open").addClass("n2-close");
                        };
                    };
                };
            });
        })
    </script>
</asp:Content>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="BannerPlaceHolder" Runat="Server">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/asset/title/banner_join_us.jpg" 
        meta:resourcekey="Image1Resource1" />
</asp:Content>--%>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <p><asp:Image runat="server" ID="imgTitle" ImageUrl="~/asset/title/site_map_E.gif" meta:resourcekey="imgTitleResource1" /></p>

<%--
<div id="teq-col-content">
<div id="teq-col-content-page-type">
<div class="col-centre-contenu">
--%>
<div id="sitemap">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        SelectCommand="SELECT [url], [Title], [PageID], [Children] FROM [view_PageChildren] WHERE (([Visible] = @Visible) AND ([Enabled] = @Enabled) AND ([ParentPageID] = @ParentPageID) AND ([Lang] = @Lang)) ORDER BY [SortOrder]">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="Visible" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
            <asp:Parameter DefaultValue="0" Name="ParentPageID" Type="Int32" />
            <asp:SessionParameter DefaultValue="us-en" Name="Lang" SessionField="MyCulture" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <ul>
    <asp:Repeater runat="server" ID="Repeater1" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <li class="n1-close">
                <asp:PlaceHolder runat="server" Visible='<%# (Eval("Children") = 0).ToString() %>'>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("~/{0}.htm", Eval("url")) %>' ToolTip='<%# Eval("Title") %>' Text='<%# Eval("Title") %>' />
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" Visible='<%# (Eval("Children") > 0).ToString() %>'>
                    <asp:Literal runat="server" Text='<%# Eval("Title") %>' />
                    
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                        SelectCommand="SELECT [url], [Title], [PageID], [Children] FROM [view_PageChildren] WHERE (([Visible] = @Visible) AND ([Enabled] = @Enabled) AND ([ParentPageID] = @ParentPageID) AND ([Lang] = @Lang)) ORDER BY [SortOrder]">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="true" Name="Visible" Type="Boolean" />
                            <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
                            <asp:ControlParameter Type="Int32" Name="ParentPageID" PropertyName="Value" ControlID="hfdPageID" />
                            <asp:SessionParameter DefaultValue="us-en" Name="Lang" SessionField="MyCulture" 
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <ul style="display:none;">
                    <asp:Repeater runat="server" ID="Repeater2" DataSourceID="SqlDataSource2">
                        <ItemTemplate>
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible='<%# (Eval("Children") = 0).ToString() %>'>
                                    <li class="n2-open-only">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("~/{0}.htm", Eval("url")) %>' ToolTip='<%# Eval("Title") %>' Text='<%# Eval("Title") %>' />
                                    </li>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible='<%# (Eval("Children") > 0).ToString() %>'>
                                    <li class="n2-close">
                                        <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("Title") %>' />
                                        <asp:HiddenField runat="server" ID="hfdPageID" Value='<%# Eval("PageID") %>' />
                                    
                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                                        SelectCommand="SELECT [url], [Title], [PageID], [Children] FROM [view_PageChildren] WHERE (([Visible] = @Visible) AND ([Enabled] = @Enabled) AND ([ParentPageID] = @ParentPageID) AND ([Lang] = @Lang)) ORDER BY [SortOrder]">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="true" Name="Visible" Type="Boolean" />
                                            <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
                                            <asp:ControlParameter Type="Int32" Name="ParentPageID" PropertyName="Value" ControlID="hfdPageID" />
                                            <asp:SessionParameter DefaultValue="us-en" Name="Lang" SessionField="MyCulture" 
                                                Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                
                                        <ul style="display:none;">
                                        <asp:Repeater runat="server" ID="Repeater3" DataSourceID="SqlDataSource3">
                                            <ItemTemplate>
                                                <li class="n-3">
                                                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("~/{0}.htm", Eval("url")) %>' ToolTip='<%# Eval("Title") %>' Text='<%# Eval("Title") %>' />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        </ul>
                    
                                    </li>        
                                                        
                                </asp:PlaceHolder>
                        </ItemTemplate>
                    </asp:Repeater>
                    </ul>
                    <asp:HiddenField runat="server" ID="hfdPageID" Value='<%# Eval("PageID") %>' />
                    
                </asp:PlaceHolder>
            </li>        
        </ItemTemplate>
    </asp:Repeater>
    </ul>
    
</div>


</asp:Content>

