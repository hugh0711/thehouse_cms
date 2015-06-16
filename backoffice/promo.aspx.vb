Imports System.IO

Partial Class backoffice_promo
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("PromoImageFilename") = ""
            If Not String.IsNullOrWhiteSpace(Request("pm")) Then
                hfdPromoSettingID.Value = Request("pm")
                LoadSettingData()
            End If
            If Not String.IsNullOrWhiteSpace(Request("id")) Then
                hfdPromoID.Value = Request("id")
                hfdPromoSettingID.Value = (New PromoDataSetTableAdapters.PromoTableAdapter()).GetPromoSettingID(CInt(hfdPromoID.Value))
                LoadSettingData()
                LoadData()
                btnSaveBack.Visible = True
                btnDelete.Visible = True
            End If

        End If
    End Sub

    Protected Sub LoadSettingData()
        Dim PromoSettingID As Integer = CInt(hfdPromoSettingID.Value)
        Dim UnitFunctionID As Integer
        Dim dt As PromoDataSet.PromoSettingDataTable = (New PromoDataSetTableAdapters.PromoSettingTableAdapter()).GetDataByID(PromoSettingID)
        If dt.Rows.Count > 0 Then
            Dim dr As PromoDataSet.PromoSettingRow = dt.Rows(0)
            With dr
                lblTitle.Text = .Name
                hfdUnitFunctionID.Value = .UnitFunctionID
                UnitFunctionID = .UnitFunctionID

                If .hasImage Then
                    Dim d As New List(Of String)
                    If .Width > 0 Then d.Add(String.Format("{0}", .Width))
                    If .Height > 0 Then d.Add(String.Format("{0}", .Height))
                    If d.Count > 0 Then
                        lblImageSize.Text = String.Format("({0} px)", Join(d.ToArray(), " x "))
                    End If
                Else
                    phdImage.Visible = False
                End If
                If .SingleDayEvent Then
                    pnlEndDate.Visible = False
                End If
                Dim Site As New SiteFunctionClass(UnitFunctionID)
                'If UnitFunctionID > 0 Then
                '    rblLink.Items.Add(New ListItem(Site.FunctionName, LinkClass.Options.Unit))
                '    UnitSelectionControl1.FunctionID = .UnitFunctionID
                '    If Site.HasCategory Then
                '        rblLink.Items.Add(New ListItem("Category", LinkClass.Options.Category))
                '        CategorySelectionControl1.FunctionID = .UnitFunctionID
                '    End If
                '    If Site.HasTag Then
                '        rblLink.Items.Add(New ListItem("Tag", LinkClass.Options.Tag))
                '    End If
                'End If
                'rblLink.Items.Add(New ListItem("Url", LinkClass.Options.Url))
                'rblLink.Items.Add(New ListItem("None", LinkClass.Options.None))

                'pnlLinkUnit.GroupingText = String.Format("Select {0}", Site.FunctionName)

                'rblTag.DataBind()
            End With
        End If

        'ShowOption(LinkClass.Options.None)
    End Sub

    Protected Sub LoadData()
        Dim PromoID As Integer = CInt(hfdPromoID.Value)
        Dim dt As PromoDataSet.PromoDataTable = (New PromoDataSetTableAdapters.PromoTableAdapter()).GetDataByPromoID(PromoID)
        If dt.Rows.Count > 0 Then
            Dim dr As PromoDataSet.PromoRow = dt.Rows(0)
            With dr
                txtName.Text = .Name
                If .StartDate <> Utility.NoStartDate Then
                    txtStartDate.Text = .StartDate.ToString("yyyy-MM-dd")
                End If
                If .EndDate <> Utility.NoEndDate Then
                    txtEndDate.Text = .EndDate.ToString("yyyy-MM-dd")
                End If
                hfdSortOrder.Value = .SortOrder
                chkEnabled.Checked = .Enabled
                If Not String.IsNullOrWhiteSpace(.PromoImageUrl) Then
                    hfdImageUrl.Value = .PromoImageUrl
                    Image1.ImageUrl = IO.Path.Combine(ConfigurationManager.AppSettings("PromoImagePath"), .PromoImageUrl)
                    'AsyncFileUpload1.Visible = False
                Else
                    btnRemoveImage.Visible = False
                End If
                Dim oLink As LinkClass = LinkClass.GetFromDBValue(.PromoUrl)
                ' SetOption(oLink)
            End With
            LoadLangData(PromoID)
        Else

        End If
    End Sub

    Protected Sub LoadLangData(PromoID As Integer)
        Dim dt As PromoDataSet.PromoLangDataTable = (New PromoDataSetTableAdapters.PromoLangTableAdapter()).GetDataByPromoID(PromoID, Session("MyCulture"))
        If dt.Rows.Count > 0 Then
            Dim dr As PromoDataSet.PromoLangRow = dt.Rows(0)
            With dr
                txtDescription.Text = .PromoDesc.Replace("<br />", vbCrLf)
            End With
        End If
    End Sub

    Protected Sub SaveData()
        Dim PromoID As Integer
        Dim da As New PromoDataSetTableAdapters.PromoTableAdapter()
        Dim PromoSettingID As Integer = CInt(hfdPromoSettingID.Value)
        Dim UnitFunctionID As Integer = CInt(hfdUnitFunctionID.Value)
        Dim ImageUrl As String = hfdImageUrl.Value
        Dim StartDate As Date = Utility.NoStartDate
        Dim EndDate As Date = Utility.NoEndDate
        'Dim PromoUrl As String = GetOption()
        Dim SortOrder As Integer
        Dim CurrentUser As String = Page.User.Identity.Name

        If IsDate(txtStartDate.Text) Then
            StartDate = Utility.GetStartDate(txtStartDate.Text)
        End If
        If IsDate(txtEndDate.Text) Then
            EndDate = Utility.GetEndDate(txtEndDate.Text)
        End If

        If Not String.IsNullOrWhiteSpace(Session("PromoImageFilename")) Then
            ImageUrl = Session("PromoImageFilename")
            Image1.ImageUrl = Path.Combine(ConfigurationManager.AppSettings("PromoImagePath"), ImageUrl)
            If Image1.ImageUrl <> "" Then
                File.Move(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("UploadPath"), ImageUrl)), Server.MapPath(Image1.ImageUrl))
            End If
        End If


        If hfdPromoID.Value = "" Then
            SortOrder = da.GetNextSortOrder(PromoSettingID)
            PromoID = da.InsertQuery(txtName.Text, PromoSettingID, ImageUrl, "", UnitFunctionID, StartDate, EndDate, False, SortOrder, chkEnabled.Checked, CurrentUser, Now())
        Else
            SortOrder = CInt(hfdSortOrder.Value)
            PromoID = CInt(hfdPromoID.Value)
            da.UpdateQuery(txtName.Text, PromoSettingID, ImageUrl, "", UnitFunctionID, StartDate, EndDate, False, SortOrder, chkEnabled.Checked, CurrentUser, Now(), PromoID)
        End If

        If Not String.IsNullOrWhiteSpace(Session("PromoImageFilename")) Then
            If hfdImageUrl.Value <> Session("PromoImageFilename") AndAlso hfdImageUrl.Value <> "" Then
                Try
                    File.Delete(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings("PromoImagePath"), hfdImageUrl.Value)))
                Catch ex As Exception
                End Try
            End If
        End If

        SaveLangData(PromoID)
        Session("PromoImageFilename") = ""
    End Sub

    Protected Sub SaveLangData(PromoID As Integer)
        Dim RowAffected As Integer = 0
        Dim da As New PromoDataSetTableAdapters.PromoLangTableAdapter()
        Dim ImageUrl As String = ""
        Dim PromoUrl As String = ""
        Dim Description As String = txtDescription.Text.Replace(vbCrLf, "<br />")

        If 0 = da.UpdateQuery(txtName.Text, Description, ImageUrl, PromoUrl, PromoID, Session("MyCulture")) Then
            da.Insert(PromoID, Session("MyCulture"), txtName.Text, Description, ImageUrl, PromoUrl)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        If Page.IsValid Then
            Try
                SaveData()
                lblMessage.Text = "Save Successful"
            Catch ex As Exception
                lblMessage.Text = "Save Failed: " & ex.Message
            End Try
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(String.Format("~/backoffice/promos.aspx?pm={0}", hfdPromoSettingID.Value))
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim RowAffected As Integer
        Dim PromoID As Integer = CInt(hfdPromoID.Value)

        'Delete image file
        Dim db As New CMSDataContext
        Dim Filename = (From p In db.Promos Where p.PromoID = PromoID.ToString Select p.PromoImageUrl).FirstOrDefault
        Dim bannerPath As String = "~/product_image/banner/"
        Dim FilePath As String = Path.Combine(bannerPath, Filename)
        Try
            File.Delete(MapPath(FilePath))
        Catch
        End Try

        'Delete record
        RowAffected = (New PromoDataSetTableAdapters.PromoLangTableAdapter()).DeleteQuery(PromoID)
        RowAffected = (New PromoDataSetTableAdapters.PromoTableAdapter()).Delete(PromoID)



        Response.Redirect(String.Format("~/backoffice/promos.aspx?pm={0}", hfdPromoSettingID.Value))
    End Sub

    Protected Sub btnSaveBack_Click(sender As Object, e As System.EventArgs) Handles btnSaveBack.Click
        If Page.IsValid Then
            Try
                SaveData()
                Response.Redirect(String.Format("~/backoffice/promos.aspx?pm={0}", hfdPromoSettingID.Value))
            Catch ex As Exception
                lblMessage.Text = "Save Failed: " & ex.Message
            End Try
        End If
    End Sub

    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
        If AsyncFileUpload1.HasFile Then
            Dim Filename As String = Guid.NewGuid().ToString & IO.Path.GetExtension(e.FileName)
            Dim Url As String = IO.Path.Combine(ConfigurationManager.AppSettings("UploadPath"), Filename)
            AsyncFileUpload1.SaveAs(Server.MapPath(Url))

            'Dim Image As Drawing.Image = Drawing.Image.FromStream(AsyncFileUpload1.PostedFile.InputStream())
            'Dim tbSize As Drawing.Size = New Drawing.Size(CInt(ConfigurationManager.AppSettings("CommentImageWidth")), CInt(ConfigurationManager.AppSettings("CommentImageHeight")))
            'Dim tb As Drawing.Image = ImageClass.ResizeImage(Image, tbSize)
            'Filename = IO.Path.GetFileNameWithoutExtension(Filename) & ".jpg"
            'Url = IO.Path.Combine(ConfigurationManager.AppSettings("CommentImagePath"), Filename)
            'ImageClass.SaveJPGWithCompressionSetting(tb, Server.MapPath(Url), CLng(ConfigurationManager.AppSettings("JpegCompression")))

            ''Dim Image As Drawing.Image = Drawing.Image.FromFile(Server.MapPath(Url))
            'tbSize = New Drawing.Size(CInt(ConfigurationManager.AppSettings("CommentThumbnailWidth")), CInt(ConfigurationManager.AppSettings("CommentThumbnailHeight")))
            'tb = ImageClass.ResizeImage(tb, tbSize)
            'Url = IO.Path.Combine(ConfigurationManager.AppSettings("CommentThumbnailPath"), Filename)
            'ImageClass.SaveJPGWithCompressionSetting(tb, Server.MapPath(Url), CLng(ConfigurationManager.AppSettings("JpegCompression")))
            Session("PromoImageFilename") = Filename

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "showImage", "top.imagePreview('#" & Image1.ClientID & "', '" & Page.ResolveClientUrl(Url) & "');", True)

        End If
    End Sub

    'Protected Sub rbLinkCategory_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbLinkCategory.CheckedChanged
    '    ShowOption(LinkClass.Options.Category)
    'End Sub

    'Protected Sub rbLinkNone_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbLinkNone.CheckedChanged
    '    ShowOption(LinkClass.Options.None)
    'End Sub

    'Protected Sub rbLinkTag_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbLinkTag.CheckedChanged
    '    ShowOption(LinkClass.Options.Tag)
    'End Sub

    'Protected Sub rbLinkUnit_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbLinkUnit.CheckedChanged
    '    ShowOption(LinkClass.Options.Unit)
    'End Sub

    'Protected Sub rbLinkUrl_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbLinkUrl.CheckedChanged
    '    ShowOption(LinkClass.Options.Url)
    'End Sub

    'Protected Sub SetOption(objLink As LinkClass)
    '    rblLink.SelectedValue = objLink.LinkOption
    '    Select Case objLink.LinkOption
    '        Case LinkClass.Options.Category
    '            'rbLinkCategory.Checked = True
    '            CategorySelectionControl1.CategoryID = CInt(objLink.LinkValue)

    '        Case LinkClass.Options.Tag
    '            'rbLinkTag.Checked = True
    '            rblTag.SelectedValue = objLink.LinkValue
    '        Case LinkClass.Options.Unit
    '            'rbLinkUnit.Checked = True
    '            UnitSelectionControl1.UnitID = CInt(objLink.LinkValue)

    '        Case LinkClass.Options.Url
    '            'rbLinkUrl.Checked = True
    '            txtBannerUrl.Text = objLink.LinkValue
    '        Case LinkClass.Options.None
    '            'rbLinkNone.Checked = True
    '    End Select
    '    ShowOption(objLink.LinkOption)
    'End Sub

    'Protected Sub ShowOption(LinkOption As LinkClass.Options)
    '    pnlLinkCategory.Visible = False
    '    pnlLinkTag.Visible = False
    '    pnlLinkUnit.Visible = False
    '    pnlLinkUrl.Visible = False
    '    'pnlLinkCategory.CssClass = "disabled"
    '    'pnlLinkTag.CssClass = "disabled"
    '    'pnlLinkUnit.CssClass = "disabled"
    '    'pnlLinkUrl.CssClass = "disabled"

    '    Select Case LinkOption
    '        Case LinkClass.Options.Category
    '            pnlLinkCategory.Visible = True
    '            'pnlLinkCategory.CssClass = ""

    '        Case LinkClass.Options.Tag
    '            pnlLinkTag.Visible = True
    '            'pnlLinkTag.CssClass = ""
    '            If rblTag.SelectedIndex = -1 Then rblTag.SelectedIndex = 0

    '        Case LinkClass.Options.Unit
    '            pnlLinkUnit.Visible = True
    '            'pnlLinkUnit.CssClass = ""

    '        Case LinkClass.Options.Url
    '            pnlLinkUrl.Visible = True
    '            'pnlLinkUrl.CssClass = ""

    '        Case LinkClass.Options.None

    '    End Select
    'End Sub

    'Protected Function GetOption() As String
    '    Dim Value As String = ""

    '    Select Case rblLink.SelectedValue
    '        Case LinkClass.Options.Category
    '            Value = LinkClass.GetDBValue(LinkClass.Options.Category, CategorySelectionControl1.CategoryID)
    '        Case LinkClass.Options.Tag
    '            Value = LinkClass.GetDBValue(LinkClass.Options.Tag, rblTag.SelectedValue)
    '        Case LinkClass.Options.Unit
    '            Value = LinkClass.GetDBValue(LinkClass.Options.Unit, UnitSelectionControl1.UnitID)
    '        Case LinkClass.Options.Url
    '            Value = LinkClass.GetDBValue(LinkClass.Options.Url, txtBannerUrl.Text)
    '        Case LinkClass.Options.None
    '            Value = LinkClass.GetDBValue(LinkClass.Options.None, "")
    '    End Select

    '    Return Value
    'End Function

    'Protected Sub rblLink_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblLink.SelectedIndexChanged
    '    'ShowOption(rblLink.SelectedValue)
    'End Sub

    Protected Sub btnRemoveImage_Click(sender As Object, e As System.EventArgs) Handles btnRemoveImage.Click
        Image1.ImageUrl = ""
        Session("PromoImageFilename") = ""
    End Sub
End Class
