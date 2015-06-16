<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/Admin.master" AutoEventWireup="false" CodeFile="albumphoto.aspx.vb" Inherits="backoffice_albumphoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="css/default.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src='<%= ResolveClientUrl("~/swfupload/swfupload.js") %>'></script>
<script type="text/javascript" src="js/handlers.js"></script>
<script type="text/javascript">
    var swfu;
    window.onload = function () {
        swfu = new SWFUpload({
            // Backend Settings
            upload_url: "albumupload.aspx",
            post_params: {
                "ASPSESSID": "<%=Session.SessionID %>",
                "AUTHID": "<%=AuthCookie %>",
                "AlbumID": "<%=AlbumID %>"
            },

            // File Upload Settings
            file_size_limit: "2 MB",
            file_types: "*.jpg",
            file_types_description: "JPG Images",
            file_upload_limit: "0",    // Zero means unlimited

            // Event Handler Settings - these functions as defined in Handlers.js
            //  The handlers are not part of SWFUpload but are part of my website and control how
            //  my website reacts to the SWFUpload events.
            file_queue_error_handler: fileQueueError,
            file_dialog_complete_handler: fileDialogComplete,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: uploadSuccess,
            upload_complete_handler: uploadComplete,

            // Button settings
            button_image_url: "images/XPButtonNoText_160x22.png",
            button_placeholder_id: "spanButtonPlaceholder",
            button_width: 160,
            button_height: 22,
            button_text: '<span class="button">Select Images <span class="buttonSmall">(2 MB Max)</span></span>',
            button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
            button_text_top_padding: 1,
            button_text_left_padding: 5,

            // Flash Settings
            flash_url: '<%=ResolveClientUrl("~/swfupload/swfupload.swf") %>', // Relative to this file

            custom_settings: {
                upload_target: "divFileProgressContainer"
            },

            // Debug Settings
            debug: false
        });
    };
	</script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><asp:Label runat="server" ID="lblTitle" Text="新增簿相"></asp:Label></h2>
    <asp:HiddenField runat="server" ID="hfdAlbumID" />

	<div id="swfu_container" style="margin: 0px 10px;">
		<div>
			<span id="spanButtonPlaceholder"></span>
		</div>
		<div id="divFileProgressContainer" style="height: 75px;"></div>
		<div id="thumbnails"></div>
		<br />
	</div>

</asp:Content>

