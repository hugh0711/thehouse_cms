Imports Utility
Imports System.IO

Partial Class backoffice_Banner
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        hfdTagFunctionID.Value = ConfigurationManager.AppSettings("ProductFunctionID")
    End Sub

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Public Sub Bind()
		hfdBannerID.Value = 0
		If (Request("BannerID") & "").Length > 0 Then
			hfdBannerID.Value = Convert.ToInt32(Request("BannerID"))
		End If

        hfdPositionID.Value = 0
        If (Request("pos") & "").Length > 0 Then
            hfdPositionID.Value = Request("pos")
        End If

		hfdFormMode.Value = FORMMODE_INSERT
		If (Request("FormMode") & "").Length > 0 Then
			hfdFormMode.Value = Request("FormMode")
		End If
		hfdBackPanel.Value = ""
		If (Request("BackPanel") & "").Length > 0 Then
			hfdBackPanel.Value = Request("BackPanel")
		End If

		InitControl()

		'Get Data
		Dim r As BannerDataSet.BannerRow
		Dim t As New BannerDataSet.BannerDataTable
		r = t.NewRow
		BannerClass.InitRow(r)
		Dim rEN As BannerDataSet.BannerLangRow
		Dim rHK As BannerDataSet.BannerLangRow
		Dim tLang As New BannerDataSet.BannerLangDataTable
		rEN = tLang.NewRow
		rHK = tLang.NewRow
		BannerClass.InitRow(rEN, Utility.MyCulture_EN)
		BannerClass.InitRow(rHK, Utility.MyCulture_HK)

		Select Case hfdFormMode.Value
			Case FORMMODE_INSERT
				btnDelete.Visible = False
                btnSave.Visible = True
                ddlBannerPosition.SelectedValue = hfdPositionID.Value

			Case FORMMODE_EDIT
				'If hfdBannerType.Value = BannerClass.BannerType_ADVERTISING Then
				'	If (Request("Position") & "").Length > 0 Then
				'		t = (New BannerDataSetTableAdapters.BannerTableAdapter).GetDataByBannerPosition(Request("Position"))
				'	End If
				'Else
				t = (New BannerDataSetTableAdapters.BannerTableAdapter).GetDataByBannerID(hfdBannerID.Value)
				'End If

				If t.Rows.Count > 0 Then
					r = t.Rows(0)
					hfdBannerID.Value = r.BannerID
					tLang = (New BannerDataSetTableAdapters.BannerLangTableAdapter).GetDataByBannerID(hfdBannerID.Value, Utility.MyCulture_EN)
					If tLang.Rows.Count > 0 Then
						rEN = tLang.Rows(0)
					End If
					tLang = (New BannerDataSetTableAdapters.BannerLangTableAdapter).GetDataByBannerID(hfdBannerID.Value, Utility.MyCulture_HK)
					If tLang.Rows.Count > 0 Then
						rHK = tLang.Rows(0)
					End If

				End If
				btnDelete.Visible = True
				btnSave.Visible = True
			Case Else
				btnDelete.Visible = False
				btnSave.Visible = False
		End Select

		With r
			txtBannerCreateDate.Text = DateTimeToString(.BannerCreateDate, False)
			'txtBannerName.Text = .BannerName
            ddlBannerPosition.SelectedValue = .PositionID
			'txtBannerImagePath.Text = .BannerImagePath
			'If .BannerImagePath.Length > 0 Then
			'	imgBanner.ImageUrl = Path.Combine(BannerClass.BannerImagePath, .BannerImagePath)
			'Else
			'	imgBanner.ImageUrl = NoImage
			'End If
			'txtBannerWidth.Text = .BannerWidth
			'txtBannerHeight.Text = .BannerHeight
            Dim Url As String = .BannerUrl
            If Url.StartsWith("tag://") Then
                rblTag.SelectedValue = Url.Substring(6)
                rbLinkTag.Checked = True
                rbLinkTag_CheckedChanged(Nothing, Nothing)
            Else
                txtBannerUrl.Text = Url
                rbLinkUrl.Checked = True
                rbLinkUrl_CheckedChanged(Nothing, Nothing)
            End If
            txtBannerOrder.Text = .SortOrder
            chkEnabled.Checked = .Enabled
		End With
		With rEN
			txtLangNameEN.Text = .LangName
			txtUrlEN.Text = .LangUrl
			If txtUrlEN.Text.Length > 0 Then
				ImgEN.ImageUrl = Path.Combine(BannerClass.BannerImagePath, .LangUrl)
			Else
				ImgEN.ImageUrl = NoImage
			End If

		End With
		With rHK
			txtLangNameHK.Text = .LangName
			txtUrlHK.Text = .LangUrl
			If txtUrlEN.Text.Length > 0 Then
				ImgHK.ImageUrl = Path.Combine(BannerClass.BannerImagePath, .LangUrl)
			Else
				ImgHK.ImageUrl = NoImage
			End If
		End With

		'For Advertising
		'If hfdBannerType.Value = BannerClass.BannerType_ADVERTISING Then
		'	'If (Request("Position") & "").Length > 0 Then
		'	'	ddlBannerPosition.SelectedValue = Request("Position")
		'	'End If
		'	btnDelete.Visible = False
		'End If
	End Sub

	Protected Sub InitControl()
		ddlBannerPosition.DataBind()
	End Sub

    Protected Function Update() As Integer
        Dim BannerID As Integer
        Dim adapter As New BannerDataSetTableAdapters.BannerTableAdapter
        Dim PositionID As Integer = CInt(ddlBannerPosition.SelectedValue)
        Dim SortOrder As Integer = adapter.GetNextSortOrder(PositionID)
        Dim Url As String = ""

        If rbLinkTag.Checked Then
            Url = String.Format("tag://{0}", rblTag.SelectedValue)
        Else
            Url = txtBannerUrl.Text
        End If

        Select Case hfdFormMode.Value
            Case FORMMODE_INSERT
                BannerID = adapter.InsertQuery(StringToDateTime(txtBannerCreateDate.Text, False), txtLangNameEN.Text, hfdBannerType.Value, txtUrlEN.Text _
                  , Convert.ToInt32(0), Convert.ToInt32(0), Url, PositionID, SortOrder, chkEnabled.Checked)
                Dim adapterLang As New BannerDataSetTableAdapters.BannerLangTableAdapter
                adapterLang.Insert(BannerID, Utility.MyCulture_EN, txtUrlEN.Text, txtLangNameEN.Text)
                adapterLang.Insert(BannerID, Utility.MyCulture_HK, txtUrlHK.Text, txtLangNameHK.Text)
            Case FORMMODE_EDIT
                BannerID = CInt(hfdBannerID.Value)
                adapter.Update(StringToDateTime(txtBannerCreateDate.Text, False), txtLangNameEN.Text, ddlBannerPosition.SelectedValue, hfdBannerType.Value, txtUrlEN.Text _
                  , Convert.ToInt32(0), Convert.ToInt32(0), Url, Convert.ToInt32(txtBannerOrder.Text), chkEnabled.Checked, BannerID)
                Dim adapterLang As New BannerDataSetTableAdapters.BannerLangTableAdapter
                adapterLang.UpdateBanner(txtUrlEN.Text, txtLangNameEN.Text, BannerID, Utility.MyCulture_EN)
                adapterLang.UpdateBanner(txtUrlHK.Text, txtLangNameHK.Text, BannerID, Utility.MyCulture_HK)
        End Select

        Return BannerID
    End Function


    Protected Sub BackPage()
        'If hfdBannerType.Value = BannerClass.BannerType_ADVERTISING Then
        '	Response.Redirect(String.Format("~/backoffice/AdvertisingList.aspx?BackPanel={0}", hfdBackPanel.Value))
        'Else
        Response.Redirect(String.Format("~/backoffice/BannerList.aspx?pos={0}", ddlBannerPosition.SelectedValue))
        'End If
    End Sub

	Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
		BackPage()
	End Sub

	Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
		BannerClass.DeleteByBannerID(hfdBannerID.Value)
		BackPage()
	End Sub

	Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
		If Page.IsValid Then
			If FileUploadEN.HasFile Then
				txtUrlEN.Text = PhotoClass.SaveFile(FileUploadEN, txtUrlEN, BannerClass.BannerImagePath)
				'PhotoClass.ResizeUploadedPhoto(txtBannerImagePath.Text, ViewState("FunctionSettings").ProductImageWidth, ViewState("FunctionSettings").ProductImageHeight, BannerClass.BannerImagePath)
			End If
			If FileUploadHK.HasFile Then
				txtUrlHK.Text = PhotoClass.SaveFile(FileUploadHK, txtUrlHK, BannerClass.BannerImagePath)
            End If
            Update()
            BackPage()
		End If
	End Sub

	Protected Sub custvaltxtBannerCreateDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles custvaltxtBannerCreateDate.ServerValidate
		args.IsValid = ValidateDateTimeString(txtBannerCreateDate.Text, False)
	End Sub

	'Protected Sub btnBannerClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBannerClear.Click
	'	txtBannerImagePath.Text = ""
	'End Sub


    Protected Sub rbLinkUrl_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbLinkUrl.CheckedChanged
        If rbLinkUrl.Checked Then
            pnlLinkUrl.Enabled = True
            pnlLinkTag.Enabled = False
        End If
    End Sub

    Protected Sub rbLinkTag_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbLinkTag.CheckedChanged
        If rbLinkTag.Checked Then
            pnlLinkUrl.Enabled = False
            pnlLinkTag.Enabled = True
        End If
    End Sub

End Class
