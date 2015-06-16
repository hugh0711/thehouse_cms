<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CommentControl.ascx.vb" Inherits="control_CommentControl" %>

<%@ Register assembly="Flan.Controls" namespace="Flan.Controls" tagprefix="cc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:UpdatePanel runat="server" ID="CommentUpdatePanel" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfdReferenceID" />
        <asp:HiddenField runat="server" ID="hfdType" />
        <div id="comment-post">
        <asp:Panel runat="server" ID="pnlPost" DefaultButton="btnPost">
            <asp:TextBox runat="server" ID="txtComment" Rows="3" Width="95%" TextMode="MultiLine"></asp:TextBox>
            <asp:Panel runat="server" ID="pnlCommentControl">
                <asp:LinkButton runat="server" ID="btnAddImage" Text="加入圖片" CausesValidation="false"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnAddVideo" Text="加入影片" CausesValidation="false" OnClientClick="preview();"></asp:LinkButton>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlImageUpload" CssClass="additional" Visible="false">
                <div style="float:left;">
                <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server"  OnClientUploadError="uploadError" OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete" 
                    OnUploadedComplete="AsyncFileUpload1_UploadedComplete" ClientIDMode="AutoID"  ThrobberID="Throbber" UploaderStyle="Modern"/>
                </div>
                <asp:Label ID="Throbber" runat="server" Style="display: none; float:left;">
                    <asp:Image runat="server" ImageUrl="~/images/wait.gif" />
                    <asp:Label runat="server" ID="lblImageMessage"></asp:Label>
                </asp:Label>
                <div style="clear:both;">
                <asp:Label runat="server" ID="lblImageUrl"></asp:Label>
                <asp:Image runat="server" ID="Image1" style="display:none;"/>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlVideoUrl" CssClass="additional" DefaultButton="btnPreviewVideo" Visible="false">
                YouTube Url: <asp:TextBox runat="server" ID="txtVideoUrl" Columns="50" CssClass="video-url"></asp:TextBox>
                <asp:Button runat="server" ID="btnPreviewVideo" Text="預覽" OnClientClick="preview();" UseSubmitBehavior="false" />
                <asp:TextBoxWatermarkExtender ID="txtVideoUrl_TextBoxWatermarkExtender" WatermarkCssClass="text watermark" 
                    WatermarkText="將YouTube連結覆制並貼上" runat="server" TargetControlID="txtVideoUrl">
                </asp:TextBoxWatermarkExtender>
                <div id="yt-error" class="error" style="display:none;">YouTube網址不正確</div>
                <div id="video-preview-panel" style="display:none;">
                    <div class="image-wrapper"><img class="thumbnail" /></div>
                    <asp:Label runat="server" ID="lblTitle" CssClass="title" EnableViewState="true"></asp:Label><br />
                    <asp:Label runat="server" ID="lblDesc" CssClass="desc" EnableViewState="true"></asp:Label>
                </div>
            </asp:Panel>
            <div class="clear"></div>
            <asp:Button runat="server" ID="btnPost" Text="提交" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlRegister">
            If you want to leave comment. Please <asp:HyperLink runat="server" NavigateUrl="~/register.aspx">sign up</asp:HyperLink> or <asp:HyperLink runat="server" NavigateUrl="~/login.aspx">login</asp:HyperLink>.</asp:Panel>
        </div>
        
        <div id="comment-list">
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                
                
                SelectCommand="SELECT CommentID, UserID, Name, CommentDate, CommentDescription, MediaUrl, MediaTitle, MediaDesc FROM view_Comment WHERE (CommentType = @CommentType) AND (ReferenceID = @ReferenceID) AND (IsDisable = @IsDisable) AND (IsInspected = @IsInspected) And ParentID = @ParentID ORDER BY CommentDate DESC"
                DeleteCommand="DELETE FROM Comment WHERE CommentID = @CommentID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfdType" Name="CommentType" 
                        PropertyName="Value" Type="String" />
                    <asp:ControlParameter ControlID="hfdReferenceID" Name="ReferenceID" 
                        PropertyName="Value" Type="Int32" />
                    <asp:Parameter DefaultValue="false" Name="IsDisable" Type="Boolean" />
                    <asp:Parameter DefaultValue="false" Name="IsInspected" />
                    <asp:Parameter DefaultValue="0" Name="ParentID" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="CommentID" />
                </DeleteParameters>
            </asp:SqlDataSource>
            
            <asp:ListView runat="server" ID="lvwComment" DataSourceID="SqlDataSource1" DataKeyNames="CommentID">
                <LayoutTemplate>
                    <asp:PlaceHolder runat='server' ID="itemPlaceHolder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder">
                    <div class="comment-item">
                        <asp:Label runat="server" Text='<%# Eval("Name") %>' CssClass="user"></asp:Label>
                        <asp:Label runat="server" Text='<%# Eval("CommentDescription") %>' CssClass="comment"></asp:Label>
                        <asp:Panel ID="Panel1" runat="server" CssClass="additional" Visible='<%# IsVideo(Eval("MediaUrl")) %>'>
                            <div class="image-wrapper"><asp:HyperLink runat="server" ID="Img1" CssClass="thumbnail" ImageUrl='<%# ShowThumbnail(Eval("MediaUrl")) %>' onclick='<%# "TINY.box.show({iframe:""" & GetEmbedVideoUrl(Eval("MediaUrl")) & """,width:640,height:360})" %>' /></div>
                            <asp:Label runat="server" CssClass="title" Text='<%# Eval("MediaTitle") %>'></asp:Label>
                            <asp:Label runat="server" CssClass="desc" Text='<%# Eval("MediaDesc") %>'></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" CssClass="additional" Visible='<%# IsImage(Eval("MediaUrl")) %>'>
                            <asp:HyperLink runat="server" CssClass="image-preview" ImageUrl='<%# GetImageUrl(Eval("MediaUrl"), ImageClass.ImageSize.Thumbnail) %>' onclick='<%# "TINY.box.show({image:""" & Page.ResolveClientUrl(GetImageUrl(Eval("MediaUrl"), ImageClass.ImageSize.Normal)) & """})" %>'></asp:HyperLink>
                        </asp:Panel>
                        
                        <br />
                        <asp:Label runat="server" Text='<%# Eval("UserID") %>' CssClass="user"></asp:Label>&nbsp;&nbsp;
                        <asp:Label runat="server" Text='<%# Utility.GetTime(Eval("CommentDate")) %>' CssClass="date"></asp:Label>
                        <asp:LinkButton runat="server" Text="檢舉" ID="btnInspect" CssClass="button" OnClick="btnInspect_Click" CausesValidation="false" Visible='<%# Page.User.Identity.IsAuthenticated %>'></asp:LinkButton>
                        <asp:LinkButton runat="server" Text="刪除" ID="btnDelete" CssClass="button" CommandName="delete" CommandArgument='<%# Eval("CommentID") %>' Visible='<%# IsCurrentUser(Eval("UserID")) %>' ></asp:LinkButton>
                        <asp:ConfirmButtonExtender runat="server" TargetControlID="btnDelete" ConfirmText="確定刪除?" />
                        
                        <asp:HiddenField runat="server" ID="hfdCommentID" Value='<%# Eval("CommentID") %>' />

                        <asp:SqlDataSource runat="server" ID="dsReply"
                            ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                            
                            
                            SelectCommand="SELECT CommentID, UserID, CommentDate, CommentDescription FROM view_Comment WHERE (IsDisable = @IsDisable) AND (IsInspected = @IsInspected) And ParentID = @ParentID ORDER BY CommentDate ASC"
                            DeleteCommand="DELETE FROM Comment WHERE CommentID = @CommentID">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="false" Name="IsDisable" Type="Boolean" />
                                <asp:Parameter DefaultValue="false" Name="IsInspected" />
                                <asp:ControlParameter Name="ParentID" Type="Int32" ControlID="hfdCommentID" PropertyName="Value" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="CommentID" />
                            </DeleteParameters>
                        </asp:SqlDataSource>
                        
                        <ul runat="server" class="reply" data-id='<%# Eval("CommentID") %>'>
                            <li class="first"></li>
                        <asp:Repeater runat="server" ID="Repeater1" DataSourceID="dsReply">
                            <ItemTemplate>
                                <li>
                                    <asp:Label runat="server" Text='<%# Eval("Name") %>' CssClass="user"></asp:Label>
                                    <asp:Label runat="server" Text='<%# Eval("CommentDescription") %>' CssClass="desc"></asp:Label>
                                    <asp:Label runat="server" Text='<%# Utility.GetTime(Eval("CommentDate")) %>' CssClass="date"></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                            <li>
                                <textarea class="watermark"><%=ReplyWatermarkText%></textarea>
                            </li>
                        </ul>
                    </div>
                    </asp:PlaceHolder>
                </ItemTemplate>
            </asp:ListView>
        </div>
    
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="CommentUpdatePanel" >
    <ProgressTemplate>
        <div></div>
    </ProgressTemplate>
</asp:UpdateProgress>
<cc1:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" CssClass="updateProgress" ControlToOverlayID="CommentUpdatePanel" OverlayType="Control" TargetControlID="UpdateProgress1" 
            runat="server" />
            
<script type="text/javascript">
    
    function getYTID(e) {
        var url = $(e).val();
        var regex = /^.*((youtu.be\/)|(v\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*/;
        try {
            var t = url.split(regex);
            return t[6];
        }
        catch (err) {
            return undefined;
        };
    }

    function preview() {
        var videoId = getYTID("#comment-post .video-url");
        if (videoId != undefined) {
            var reqUrl = "http://gdata.youtube.com/feeds/api/videos/" + videoId + "?v=2&alt=json-in-script&callback=?";
            $.getJSON(reqUrl, function(data) {

        })
            .success(function(data) {
                var media = data.entry.media$group;
                $("#video-preview-panel .thumbnail").attr("src", media.media$thumbnail[0].url);
                $("#video-preview-panel .title").text(media.media$title.$t);
                $("#video-preview-panel .desc").text(media.media$description.$t);
                $("#video-preview-panel").fadeIn();
                $("#yt-error").fadeOut();
            })
            .error(function() {
                alert("error");
                $("#yt-error").fadeIn();
            });
        }
        else {
            $("#yt-error").fadeIn();
        };
    }

    function imagePreview(e, url) {
        $(e).load(function() {
            $(this).fadeIn();
        }).attr("src", url);
    };

    var replyWatermarkText = "<%= ReplyWatermarkText %>";

    $(document).ready(function() {
        $("#comment-post .video-url").live("blur", function() {
            preview();
        });
        $("ul.reply textarea").autoResize({ extraSpace: 20 })
        .focus(function() {
            $(this).parent().find("textarea").filter(function() {
                return $(this).val() == "" || $(this).val() == replyWatermarkText
            }).removeClass("watermark").val("");
        })
        .blur(function() {
            $(this).parent().find("textarea").filter(function() {
                return $(this).val() == ""
            }).addClass("watermark").val(replyWatermarkText);
        })
        .bind("keypress", function(e) {
            if ((e.keyCode || e.which) == 13) {
                var commentId = $(this).parents("ul:first").attr("data-id");
                var comment = $(this).val();
                postReply(commentId, comment, OnCommentReplyComplete);
            };
        });

    });

    // Callback from CommentReply
    function OnCommentReplyComplete(result) {
        alert(result);
    };
    
/*
    function pageLoad(sender, args) {
        $(".comment-item .additional[data-ytid]").each(function(i, e) {
            var videoId = $(e).attr("data-ytid")
            var reqUrl = "http://gdata.youtube.com/feeds/api/videos/" + videoId + "?v=2&alt=json-in-script&callback=?";
            $.getJSON(reqUrl, function(data) {
                var media = data.entry.media$group;
                //$("#video-preview-panel .thumbnail").attr("src", media.media$thumbnail[0].url);
                var desc = media.media$description.$t;
                if (desc.length > 100) {
                    desc = desc.substring(0, 100) + "...";
                }
                $(e).find(".title").text(media.media$title.$t);
                $(e).find(".desc").text(desc);
                //$("#video-preview-panel").fadeIn();
            });
        });
    }
*/
    function uploadError(sender, args) {
        $("#<%= lblImageMessage.ClientID %>").html(args.get_fileName() + "<span style='color:red;'>" + args.get_errorMessage() + "</span>");
    }

    function StartUpload(sender, args) {
        $("#<%= lblImageMessage.ClientID %>").text("上傳中...");
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
</script>