<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BannerUpload.aspx.vb" Inherits="backoffice_BannerUpload" MasterPageFile="~/backoffice/master/Admin.master"%>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>--%>
    


    <h2>上傳相片</h2>
    
    <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" OnUploadComplete="AjaxFileUpload1_UploadComplete"
        AllowedFileTypes="jpeg,jpg,gif,png"  />


    <div>
        <asp:Button runat="server" ID="btnBack" Text="返回" />
    </div>

 
</asp:Content>
