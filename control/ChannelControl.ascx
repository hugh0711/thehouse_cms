            <div>&nbsp;</div>
            <div class="section-title">Popular Channel <span class="section-title-quot">&raquo;</span>
            </div>
            
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChannelControl.ascx.vb" Inherits="control_ChannelControl" %>
            <asp:HiddenField runat="server" ID="hfdVideoFunctionID" />
            <asp:SqlDataSource runat="server" ID="dsChannel" 
                ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                
                
                SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [view_Category] WHERE (([FunctionID] = @FunctionID) AND ([Enabled] = @Enabled) AND ([ParentID] = @ParentID))">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfdVideoFunctionID" Name="FunctionID" 
                        PropertyName="Value" Type="Int32" />
                    <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
                    <asp:Parameter DefaultValue="0" Name="ParentID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:ListView runat="server" ID="lvwChannel" DataSourceID="dsChannel">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="news-story3">
                        <div class="title"><asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("CategoryName") %>' NavigateUrl='<%# String.Format("~/channel.aspx?id={0}", Eval("CategoryID")) %>'></asp:HyperLink></div>
                        <p><asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label></p>
                    </div>
                </ItemTemplate>
            </asp:ListView>