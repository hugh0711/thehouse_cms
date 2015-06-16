<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPageInner2.master" AutoEventWireup="false" CodeFile="myvideo.aspx.vb" Inherits="myvideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField runat="server" ID="hfdUsername" />
    <asp:HiddenField runat="server" ID="hfdTypeID" />

    <div class="section-title">我的影片</div>

    <asp:SqlDataSource runat="server" ID="dsVideo" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        SelectCommand="SELECT [ProductID], [ProductName], [Description], [MOQUnit] FROM [view_UserProductImage] WHERE (([UserName] = @UserName) AND ([TypeID] = @TypeID))">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfdUsername" Name="UserName" PropertyName="Value" 
                Type="String" />
            <asp:ControlParameter ControlID="hfdTypeID" Name="TypeID" PropertyName="Value" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
               <asp:ListView runat="server" ID="lvwVideo" DataSourceID="dsVideo">
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>
                        <div class="pager">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="16" PagedControlID="lvwVideo" QueryStringField="p">
                            <Fields>
                                <asp:NextPreviousPagerField ShowNextPageButton="false" PreviousPageText="上一頁"  RenderDisabledButtonsAsLabels="true" />
                                <asp:NumericPagerField ButtonCount="15" />
                                <asp:NextPreviousPagerField ShowPreviousPageButton="false" NextPageText="下一頁"  RenderDisabledButtonsAsLabels="true" />
                            </Fields>
                        </asp:DataPager>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <asp:Panel ID="Panel1" runat="server" CssClass='<%# "episode" & IIf(Container.DataItemIndex Mod 2 = 1, " even", "") %>'>
                            <asp:HyperLink ID="lnkImage" runat="server" NavigateUrl='<%# String.Format("~/episode.aspx?id={0}", Eval("ProductID")) %>' CssClass="tb"
                                ImageUrl='<%# VideoClass.GetHQPreview(Eval("MOQUnit")) %>' >
                            </asp:HyperLink>
                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="title" Text='<%# Eval("ProductName") %>'></asp:HyperLink>
                            <p class="desc"><asp:Literal ID="Literal2" runat="server" Text='<%# Utility.TrimHTML(Eval("Description"), 100) %>'></asp:Literal></p>
                            <asp:HyperLink ID="HyperLink2" runat="server" Text="Watch" NavigateUrl='<%# String.Format("~/episode.aspx?id={0}", Eval("ProductID")) %>' ImageUrl="~/images/more_blue.png" CssClass="more"></asp:HyperLink>
                        </asp:Panel>

                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <h2>找不到結果</h2>
                    </EmptyDataTemplate>
                </asp:ListView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

