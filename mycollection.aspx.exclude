<%@ Page Title="" Language="VB" MasterPageFile="~/master/MasterPageInner2.master" AutoEventWireup="false" CodeFile="mycollection.aspx.vb" Inherits="mycollection" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField runat="server" ID="hfdUsername" />
    <asp:HiddenField runat="server" ID="hfdTypeID" />
    <div class="section-title">我的資訊</div>

    Group: <asp:DropDownList runat="server" ID="ddlGroup" DataTextField="GroupName" DataValueField="GroupID" CssClass="textEntry" AutoPostBack="true"></asp:DropDownList>
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>

    <asp:SqlDataSource runat="server" ID="dsVideo" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        SelectCommand="SELECT [ProductID], [ProductName], [Description], [LongDescription], [MOQUnit] FROM [view_UserProductImage] WHERE (([UserName] = @UserName) AND ([TypeID] = @TypeID) AND ([GroupID] = @GroupID))">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfdUsername" Name="UserName" PropertyName="Value" Type="String" />
            <asp:ControlParameter ControlID="hfdTypeID" Name="TypeID" PropertyName="Value" Type="Int32" />
            <asp:ControlParameter ControlID="ddlGroup" Name="GroupID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
               <asp:ListView runat="server" ID="lvwVideo" DataSourceID="dsVideo">
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>
                        <div class="pager">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="15" PagedControlID="lvwVideo" QueryStringField="p">
                            <Fields>
                                <asp:NextPreviousPagerField ShowNextPageButton="false" PreviousPageText="上一頁"  RenderDisabledButtonsAsLabels="true" />
                                <asp:NumericPagerField ButtonCount="15" />
                                <asp:NextPreviousPagerField ShowPreviousPageButton="false" NextPageText="下一頁"  RenderDisabledButtonsAsLabels="true" />
                            </Fields>
                        </asp:DataPager>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="collection">
                        <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("LongDescription") %>'></asp:Literal>
                        <%--<asp:Literal ID="Literal2" runat="server" Text='<%# Utility.TrimHTML(Eval("LongDescription"), 200) %>'></asp:Literal>--%>
                        相關影片: <%--<asp:HyperLink runat="server" Text='<%# Eval("ProductName") %>' NavigateUrl='<%# String.Format("~/episode.aspx?id={0}", Eval("ProductID")) %>'></asp:HyperLink>--%>
                        <br />
                        <asp:HyperLink runat="server" ImageUrl='<%# VideoClass.GetPreview(Eval("MOQUnit")) %>' CssClass="tb"
                            NavigateUrl='<%# String.Format("~/episode.aspx?id={0}", Eval("ProductID")) %>'></asp:HyperLink>

                        </div>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <h2>找不到我的資訊</h2>
                    </EmptyDataTemplate>
                </asp:ListView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlGroup" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" CssClass="updateProgress" ControlToOverlayID="UpdatePanel1" TargetControlID="UpdateProgress1" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

