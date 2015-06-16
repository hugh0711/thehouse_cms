<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/admin.master" AutoEventWireup="false"
    CodeFile="product_upload.aspx.vb" Inherits="backoffice_product_upload" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/jquery.Jcrop.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="js/jquery.Jcrop.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("img[id$=imgCrop]").Jcrop({
                onChange: showPreview,
                onSelect: showPreview,
                aspectRatio: Number($("#ratio").text())
            });
        });

        function showPreview(coords) {
            $("input[type=hidden][id$=_X]").val(coords.x);
            $("input[type=hidden][id$=_Y]").val(coords.y);
            $("input[type=hidden][id$=_W]").val(coords.w);
            $("input[type=hidden][id$=_H]").val(coords.h);

            var rx = $("#divPreview").width() / coords.w;
            var ry = $("#divPreview").height() / coords.h;
            var rw = $("img[id$=imgCrop]").width();
            var rh = $("img[id$=imgCrop]").height();

            $("img[id$=imgPreview]").css({
                width: Math.round(rx * rw) + "px",
                height: Math.round(ry * rh) + "px",
                marginLeft: "-" + Math.round(rx * coords.x) + "px",
                marginTop: "-" + Math.round(ry * coords.y) + "px"
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        產品圖片</h2>
    <asp:HiddenField ID="hfdProductID" runat="server" />
    <asp:HiddenField ID="X" runat="server" />
    <asp:HiddenField ID="Y" runat="server" />
    <asp:HiddenField ID="W" runat="server" />
    <asp:HiddenField ID="H" runat="server" />
    <asp:Panel runat="server" ID="Panel1">
        <p>
            上傳圖片:<br />
            <asp:FileUpload ID="FileUpload1" runat="server" /><br />
            <asp:CheckBox ID="chkCrop" runat="server" Text="圖片需要裁剪" /><br />
            <br />
            <asp:Button ID="btnUpload" runat="server" Text=" 上傳 " />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text=" 取消 " />&nbsp;
            <asp:Button ID="btnNoImage" runat="server" Text="沒有圖片" />
            <cc1:ConfirmButtonExtender ID="btnNoImage_ConfirmButtonExtender" runat="server"  ConfirmText="是否確定沒有圖片?"
                TargetControlID="btnNoImage">
            </cc1:ConfirmButtonExtender>
            <br />
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
        </p>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel2">
        <table border="0">
            <tr>
                <td>
                    預覽:
                    <div id="divPreview" style='<%= String.Format("overflow: hidden; width: {0}px; height: {1}px; margin-left: 5px;", ViewState("FunctionSettings").ProductImageWidth, ViewState("FunctionSettings").ProductImageHeight)  %>'>
                        <asp:Image runat="server" ID="imgPreview" />
                    </div>
                    <span id="ratio" style="display:none;"><%=ViewState("FunctionSettings").ProductImageRatio%></span>
                </td>
                <td>
                    選取類別圖片顯示範圍:<br />
                    <asp:Image runat="server" ID="imgCrop" />
                </td>
            </tr>
        </table>
            <asp:Label runat="server" ID="lblMessage2" ForeColor="Red"></asp:Label>
        <div>
            <asp:Button runat="server" ID="btnSave" Text=" 儲存 " />&nbsp;
            <asp:Button runat="server" ID="btnAuto" Text="自動調整" />&nbsp;
            <asp:Button runat="server" ID="btnCancel1" Text="取消" />&nbsp;
            <asp:Button ID="btnNoImage1" runat="server" Text="沒有圖片" />
            <cc1:ConfirmButtonExtender ID="btnNoImage1_ConfirmButtonExtender"  ConfirmText="是否確定沒有圖片?"
                runat="server" TargetControlID="btnNoImage1">
            </cc1:ConfirmButtonExtender>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel3">
        <asp:Image runat="server" ID="imgSaved" /><br />
        <asp:Button runat="server" ID="btnRemove" Text="上傳圖片" />&nbsp;
        <cc1:ConfirmButtonExtender ID="btnRemove_ConfirmButtonExtender" runat="server" ConfirmText="確定上傳另一張圖片?"
            TargetControlID="btnRemove">
        </cc1:ConfirmButtonExtender>
        <asp:Button runat="server" ID="btnCrop" Text="裁剪圖片" />&nbsp;
        <asp:Button runat="server" ID="btnBack" Text="返回" />&nbsp;
            <asp:Button ID="btnNoImage2" runat="server" Text="沒有圖片" />
        <cc1:ConfirmButtonExtender ID="btnNoImage2_ConfirmButtonExtender"  ConfirmText="是否確定沒有圖片?"
            runat="server" TargetControlID="btnNoImage2">
        </cc1:ConfirmButtonExtender>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel4">
        <br />
        <br />
        <br />
        <center>
            找不到類別，
            <asp:HyperLink runat="server" NavigateUrl="~/backoffice/products.aspx">按此返回產品管理</asp:HyperLink>
            </center>
        <br />
        <br />
        <br />
    </asp:Panel>
    <br />
</asp:Content>
