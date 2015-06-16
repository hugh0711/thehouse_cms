<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CategoryControl.ascx.vb" Inherits="backoffice_CategoryControl" %>
     <asp:SqlDataSource ID="SqlDataSourceLevel1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        SelectCommand="SELECT [CategoryID], [Category] FROM [Category] WHERE (([ParentID] = @ParentID) AND ([Enabled] = @Enabled)) ORDER BY [SortOrder]">
         <SelectParameters>
             <asp:Parameter DefaultValue="0" Name="ParentID" Type="Int32" />
             <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
         </SelectParameters>
    </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDataSourceLevel2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        SelectCommand="SELECT [CategoryID], [Category] FROM [Category] WHERE (([ParentID] = @ParentID) AND ([Enabled] = @Enabled)) ORDER BY [SortOrder]">
         <SelectParameters>
             <asp:ControlParameter ControlID="ddlLevel1" DefaultValue="" Name="ParentID" 
                 PropertyName="SelectedValue" Type="Int32" />
             <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
         </SelectParameters>
    </asp:SqlDataSource>     
    <asp:SqlDataSource ID="SqlDataSourceLevel3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        SelectCommand="SELECT [CategoryID], [Category] FROM [Category] WHERE (([ParentID] = @ParentID) AND ([Enabled] = @Enabled)) ORDER BY [SortOrder]">
         <SelectParameters>
             <asp:ControlParameter ControlID="ddlLevel2" DefaultValue="" Name="ParentID" 
                 PropertyName="SelectedValue" Type="Int32" />
             <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
         </SelectParameters>
    </asp:SqlDataSource>
     
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:PlaceHolder runat="server" ID="PanelLevel1">
                <asp:DropDownList runat="server" ID="ddlLevel1" AutoPostBack="True" 
                    AppendDataBoundItems="True" 
                    DataSourceID="SqlDataSourceLevel1" DataTextField="Category" 
                    DataValueField="CategoryID">
                    <asp:ListItem Value="" Text="-- 請選擇類別 --"></asp:ListItem>
                </asp:DropDownList>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="PanelLevel2">&nbsp;&gt;&nbsp;
                <asp:DropDownList runat="server" ID="ddlLevel2" AutoPostBack="True" 
                    AppendDataBoundItems="True" 
                    DataSourceID="SqlDataSourceLevel2" DataTextField="Category" 
                    DataValueField="CategoryID">
                    <asp:ListItem Value="" Text="-- 請選擇類別 --"></asp:ListItem>
                </asp:DropDownList>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="PanelLevel3">&nbsp;&gt;&nbsp;
                <asp:DropDownList runat="server" ID="ddlLevel3" AutoPostBack="True" 
                    AppendDataBoundItems="True" 
                    DataSourceID="SqlDataSourceLevel3" DataTextField="Category" 
                    DataValueField="CategoryID">
                    <asp:ListItem Value="" Text="-- 請選擇類別 --"></asp:ListItem>
                </asp:DropDownList>
            </asp:PlaceHolder>
        </ContentTemplate>
    </asp:UpdatePanel>