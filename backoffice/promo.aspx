<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="promo.aspx.vb" Inherits="backoffice_promo" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../control/CategorySelectionControl.ascx" tagname="CategorySelectionControl" tagprefix="uc1" %>

<%@ Register src="../control/UnitSelectionControl.ascx" tagname="UnitSelectionControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <style type="text/css">
        .banner-image { max-width:200px; max-height:200px; }
        .link-panel { padding-left:20px; width:95%; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><asp:Literal runat="server" ID="lblTitle"></asp:Literal></h2>

<asp:HiddenField runat="server" ID="hfdPromoSettingID" />
<asp:HiddenField runat="server" ID="hfdPromoID" />
<asp:HiddenField runat="server" ID="hfdUnitFunctionID" />
            

<div class="form-panel">

<table border="0" class="infoGrid">
    <tr>
        <td class="caption">Name *</td>
        <td>
            <asp:TextBox runat="server" ID="txtName" CssClass="textEntry"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Please enter Name"><br />Please enter Name</asp:RequiredFieldValidator>
        </td>
    </tr>
     <tr>
        <td class="caption">Description</td>
        <td>
            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="3" CssClass="textEntry"></asp:TextBox>
        </td>
    </tr>   
    <asp:PlaceHolder runat="server" ID="phdImage">
    <tr>
        <td class="caption">Image
            <br />
            <asp:Label runat="server" ID="lblImageSize"></asp:Label>
        </td>
        <td>
            <asp:HiddenField runat="server" ID="hfdImageUrl" />
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" Width="100%" OnClientUploadError="uploadError" OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete"/>
                    <asp:Label runat="server" ID="lblImageMessage"></asp:Label>
                    <asp:Image runat="server" ID="Image1" CssClass="banner-image"/>
                    <asp:Button runat="server" ID="btnRemoveImage" Text="Remove Image" Visible="false"/>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div></div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" CssClass="updateProgress" ControlToOverlayID="UpdatePanel1" TargetControlID="UpdateProgress1" />
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="caption">Start Date</td>
        <td>
            <asp:TextBox runat="server" ID="txtStartDate"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd">
            </asp:CalendarExtender>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate" Type="Date" Operator="DataTypeCheck"
                 Display="Dynamic" CssClass="invalid" ErrorMessage="Start Date is not valid"><br />Start Date is not valid</asp:CompareValidator>
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="pnlEndDate">
    <tr>
        <td class="caption">End Date</td>
        <td>
            <asp:TextBox runat="server" ID="txtEndDate"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="yyyy-MM-dd">
            </asp:CalendarExtender>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtEndDate" Type="Date" Operator="DataTypeCheck"
                 Display="Dynamic" CssClass="invalid" ErrorMessage="End Date is not valid"><br />End Date is not valid</asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtEndDate" Type="Date" Operator="GreaterThanEqual" 
                ControlToCompare="txtStartDate" CssClass="invalid" Display="Dynamic" ErrorMessage="Start Date must before End Date"><br />Start Date must before End Date</asp:CompareValidator>
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="caption"></td>
        <td>
            <asp:CheckBox runat="server" ID="chkEnabled" Text="Enabled" Checked="true"/>
            <asp:HiddenField runat="server" ID="hfdSortOrder" />
        </td>
    </tr>
    <tr>
        <td class="caption"></td>
        <td>
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>

</div>



    

<div class="button-panel" style="clear:both;text-align:left">
    <asp:Button runat="server" ID="btnSave" Text="Save" />
    <asp:Button runat="server" ID="btnSaveBack" Text="Save & Back" Visible="true" />
    <asp:Button runat="server" ID="btnBack" Text="Back" CausesValidation="false" />
    <asp:Button runat="server" ID="btnDelete" Text="Delete" CausesValidation="false" Visible="false" />
    <asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure to delete?"
        TargetControlID="btnDelete">
    </asp:ConfirmButtonExtender>
</div>

<script type="text/javascript">
    function uploadError(sender, args) {
        $("#<%= lblImageMessage.ClientID %>").html(args.get_fileName() + "<span style='color:red;'>" + args.get_errorMessage() + "</span>");
    }

    function StartUpload(sender, args) {
        $("#<%= lblImageMessage.ClientID %>").text("<img src='../images/wait.gif' />上傳中...");
    }

    function UploadComplete(sender, args) {

        /*
        var filename = args.get_fileName();
        var contentType = args.get_contentType();
        var text = filename + " (" + args.get_length() + " bytes) 已成功上傳";
        */
        $("#<%= lblImageMessage.ClientID %>").text('');
        //$("#<%= Image1.ClientID %>").attr("src",
    }

    function imagePreview(e, url) {
        $(e).load(function () {
            $(this).fadeIn("normal");
        }).attr("src", url);
    };

</script>  

</asp:Content>

