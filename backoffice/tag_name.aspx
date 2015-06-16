﻿<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="tag_name.aspx.vb" Inherits="backoffice_tag_name" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>
標韱名稱</h2>

<asp:HiddenField runat="server" ID="hfdTagID" />

<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
        
        SelectCommand="SELECT [TagName], [Lang], [TagNameID], [TagID] FROM [TagName] WHERE ([TagID] = @TagID) ORDER BY [Lang]" 
        UpdateCommand="UPDATE TagName SET TagName = @TagName WHERE (TagNameID = @TagNameID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfdTagID" Name="TagID" 
                PropertyName="Value" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="TagName" />
            <asp:Parameter Name="TagNameID" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="gridview" AllowSorting="false"
        DataKeyNames="TagNameID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="TagName" HeaderText="標籤名稱" ControlStyle-Width="200" ItemStyle-Width="200"
                SortExpression="TagName" />
            <asp:TemplateField HeaderText="語言" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblName" Text='<%# LanguageClass.GetLanguageName(Eval("Lang")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Button" ShowEditButton="True" ItemStyle-Width="100" />
            <asp:TemplateField ItemStyle-Width="60">
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnTranslate" Text="以此名稱翻譯" OnClick="btnTranslate_Click" />
                    <asp:HiddenField runat="server" ID="hfdTagID" Value='<%# Eval("TagID") %>' />
                    <asp:HiddenField runat="server" ID="hfdLang" Value='<%# Eval("Lang") %>' />
                    <asp:HiddenField runat="server" ID="hfdTagName" Value='<%# Eval("TagName") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

<p>
    <asp:Label runat="server" ID="lblMessage"></asp:Label>
</p>
<p>
<asp:Button runat="server" ID="btnBack" Text="返回" />
</p>

    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" >
    <ProgressTemplate>
        <div>刷新中...</div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" TargetControlID="UpdateProgress1" 
            ControlToOverlayID="UpdatePanel1" CssClass="updateProgress" OverlayType="Control" />
            
            
</asp:Content>

