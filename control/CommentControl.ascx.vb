Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class control_CommentControl
    Inherits System.Web.UI.UserControl


    Protected ReplyWatermarkText As String = "發表回應..."

    Protected Shared _Type As String
    Protected Shared _ReferenceID As Integer

    Public WriteOnly Property ReferenceType() As String
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

    Public WriteOnly Property ReferenceID() As Integer
        Set(ByVal value As Integer)
            _ReferenceID = value
        End Set
    End Property

    Public ReadOnly Property Count() As String
        Get
            Dim c As Integer = 0
            If lvwComment IsNot Nothing Then
                c = lvwComment.Items.Count
            End If
            Return c
        End Get
    End Property


    Protected Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Dim CommentAdapter As New CommentDataSetTableAdapters.CommentTableAdapter()
        Dim UserID As String = Page.User.Identity.Name
        Dim MediaUrl As String = ""
        Dim MediaTitle As String = ""
        Dim MediaDesc As String = ""
        Dim VideoID As String
        If pnlVideoUrl.Visible Then
            VideoID = VideoClass.GetVideoID(txtVideoUrl.Text)

            Try
                Dim uri As New Uri("http://gdata.youtube.com/feeds/api/videos/" & VideoID & "?v=2&alt=json-in-script")
                Dim WebReq As HttpWebRequest = HttpWebRequest.Create(uri)
                WebReq.Method = WebRequestMethods.Http.Get
                Dim WebResp As HttpWebResponse = WebReq.GetResponse()
                Dim Reader As New StreamReader(WebResp.GetResponseStream())
                Dim JSONText As String = Reader.ReadToEnd()
                WebResp.Close()

                If JSONText.StartsWith("gdata.io.handleScriptLoaded(") Then
                    JSONText = JSONText.Substring(28)
                    Dim p As Integer = JSONText.LastIndexOf(")")
                    JSONText = JSONText.Substring(0, p)
                    Dim Data As JObject = JObject.Parse(JSONText)

                    MediaTitle = CType(Data.SelectToken("entry.media$group.media$title.$t"), String)
                    MediaDesc = CType(Data.SelectToken("entry.media$group.media$description.$t"), String)
                    If MediaDesc.Length > 300 Then
                        MediaDesc = MediaDesc.Substring(0, 300) & "..."
                    End If
                    MediaUrl = "youtube://" & VideoID
                End If
            Catch ex As WebException
                ' Not Valid YouTube ID
            End Try
        End If

        If pnlImageUpload.Visible Then
            If Session("ImageFilename") <> "" Then
                MediaUrl = "image://" & Session("ImageFilename")
            End If
        End If

        CommentAdapter.Insert(_Type, _ReferenceID, UserID, Now(), txtComment.Text, False, False, 0, MediaUrl, MediaTitle, MediaDesc)
        txtComment.Text = ""
        lvwComment.DataBind()
        CommentUpdatePanel.Update()
    End Sub

    Protected Sub btnInspect_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim CommentAdapter As New CommentDataSetTableAdapters.CommentTableAdapter()
        Dim Container As Control = CType(sender, LinkButton).Parent()
        Dim hfdCommentID As HiddenField = CType(Container.FindControl("hfdCommentID"), HiddenField)
        Dim CommentID As Integer = CInt(hfdCommentID.Value)
        CommentAdapter.UpdateInspected(True, CommentID)
        lvwComment.DataBind()
        CommentUpdatePanel.Update()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Page.User.Identity.Name = "" Then
                pnlRegister.Visible = True
                pnlPost.Visible = False
            Else
                pnlRegister.Visible = False
                pnlPost.Visible = True
            End If
            hfdType.Value = _Type
            hfdReferenceID.Value = _ReferenceID
        End If
    End Sub


    Protected Sub btnAddImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddImage.Click
        pnlImageUpload.Visible = True
        pnlVideoUrl.Visible = False
    End Sub

    Protected Sub btnAddVideo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddVideo.Click
        pnlImageUpload.Visible = False
        pnlVideoUrl.Visible = True
    End Sub


    Protected Function IsCurrentUser(ByVal UserID As String) As Boolean
        Dim Ret As Boolean = False

        If Not Page.User.Identity Is Nothing Then
            If Page.User.Identity.Name = UserID Then
                Ret = True
            End If
        End If
        Return Ret
    End Function

    Protected Function ShowThumbnail(ByVal Url As String) As String
        Dim RetUrl As String = ""
        Dim VideoID As String = GetVideoID(Url)
        If VideoID <> "" Then
            RetUrl = VideoClass.GetPreview(VideoID)
        End If
        Return RetUrl
    End Function

    Protected Function GetVideoID(ByVal Url As String) As String
        Dim Ret As String = ""
        If Url.StartsWith("youtube://") Then
            Ret = Url.Substring(10)
        End If
        Return Ret
    End Function

    Protected Function GetEmbedVideoUrl(ByVal Url As String) As String
        Dim VideoID As String = GetVideoID(Url)
        Return String.Format("http://www.youtube.com/embed/{0}?rel=0", VideoID)
    End Function

    Protected Function GetImageUrl(ByVal Url As String, ByVal ImageSize As ImageClass.ImageSize) As String
        Dim Ret As String = ""
        If Url.StartsWith("image://") Then
            Ret = Url.Substring(8)

            Select Case ImageSize
                Case ImageClass.ImageSize.Thumbnail
                    Ret = IO.Path.Combine(ConfigurationManager.AppSettings("CommentThumbnailPath"), Ret)
                Case ImageClass.ImageSize.Normal
                    Ret = IO.Path.Combine(ConfigurationManager.AppSettings("CommentImagePath"), Ret)
            End Select
        End If
        Return Ret
    End Function

    Protected Function IsVideo(ByVal MediaUrl As String) As Boolean
        Return MediaUrl.StartsWith("youtube://")
    End Function

    Protected Function IsImage(ByVal MediaUrl As String) As Boolean
        Return MediaUrl.StartsWith("image://")
    End Function

    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
        If AsyncFileUpload1.HasFile Then
            Dim Filename As String = Guid.NewGuid().ToString & IO.Path.GetExtension(e.FileName)
            Dim Url As String = IO.Path.Combine(ConfigurationManager.AppSettings("CommentImagePath"), Filename)
            'AsyncFileUpload1.SaveAs(Server.MapPath(Url))
            Dim Image As Drawing.Image = Drawing.Image.FromStream(AsyncFileUpload1.PostedFile.InputStream())
            Dim tbSize As Drawing.Size = New Drawing.Size(CInt(ConfigurationManager.AppSettings("CommentImageWidth")), CInt(ConfigurationManager.AppSettings("CommentImageHeight")))
            Dim tb As Drawing.Image = ImageClass.ResizeImage(Image, tbSize)
            Filename = IO.Path.GetFileNameWithoutExtension(Filename) & ".jpg"
            Url = IO.Path.Combine(ConfigurationManager.AppSettings("CommentImagePath"), Filename)
            ImageClass.SaveJPGWithCompressionSetting(tb, Server.MapPath(Url), CLng(ConfigurationManager.AppSettings("JpegCompression")))

            'Dim Image As Drawing.Image = Drawing.Image.FromFile(Server.MapPath(Url))
            tbSize = New Drawing.Size(CInt(ConfigurationManager.AppSettings("CommentThumbnailWidth")), CInt(ConfigurationManager.AppSettings("CommentThumbnailHeight")))
            tb = ImageClass.ResizeImage(tb, tbSize)
            Url = IO.Path.Combine(ConfigurationManager.AppSettings("CommentThumbnailPath"), Filename)
            ImageClass.SaveJPGWithCompressionSetting(tb, Server.MapPath(Url), CLng(ConfigurationManager.AppSettings("JpegCompression")))
            Session("ImageFilename") = Filename

            CommentUpdatePanel.Update()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "reply", "top.imagePreview('#" & Image1.ClientID & "', '" & Page.ResolveClientUrl(Url) & "');", True)

        End If
    End Sub

    <System.Web.Services.WebMethod()> _
    <System.Web.Script.Services.ScriptMethod(UseHttpGet:=True)> _
    Public Shared Function CommentReply(ByVal CommentID As Integer, ByVal Comment As String) As Boolean
        Dim User As MembershipUser = Membership.GetUser()
        Dim daComment As New CommentDataSetTableAdapters.CommentTableAdapter()
        Dim RowAffected As Integer
        RowAffected = daComment.Insert(_Type, _ReferenceID, User.UserName, Now(), Comment, False, False, CommentID, "", "", "")
        Return (RowAffected > 0)
    End Function

End Class
