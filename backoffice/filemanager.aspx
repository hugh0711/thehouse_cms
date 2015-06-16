<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="filemanager.aspx.vb" Inherits="backoffice_filemanager" %>

<%@ Register Assembly="CKFinder" Namespace="CKFinder" TagPrefix="CKFinder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CKFinder:FileBrowser ID="FileBrowser1" runat="server" BasePath="../ckfinder/" Height="500">
    </CKFinder:FileBrowser>
</asp:Content>

