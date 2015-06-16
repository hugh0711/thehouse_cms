Imports Utility

Partial Class backoffice_PaymentMethod
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Protected Sub Bind()
		Dim PaymentMethodId As Integer = 0

		If (Request("PaymentMethodId") & "").Length > 0 Then
			PaymentMethodId = Request("PaymentMethodId")
		End If
		If (Request("FormMode") & "").Length > 0 Then
			txtFormMode.Value = Request("FormMode")
		End If

		' --- retrieve Data and Setup Screen
		Dim r As PaymentMethodDataSet.PaymentMethodRow = Nothing
		Dim t As New PaymentMethodDataSet.PaymentMethodDataTable
		' Lang
		Dim rEn As PaymentMethodDataSet.PaymentMethodLangRow = Nothing
		Dim tEn As New PaymentMethodDataSet.PaymentMethodLangDataTable

		Dim rHk As PaymentMethodDataSet.PaymentMethodLangRow = Nothing
		Dim tHk As New PaymentMethodDataSet.PaymentMethodLangDataTable

		Select Case txtFormMode.Value
			Case FORMMODE_EDIT
				t = (New PaymentMethodDataSetTableAdapters.PaymentMethodTableAdapter).GetDataByPaymentMethodid(PaymentMethodId)
				If t.Rows.Count > 0 Then
					r = t.Rows(0)

					'Lang
					tEn = (New PaymentMethodDataSetTableAdapters.PaymentMethodLangTableAdapter).GetDataByPaymentMethodID(r.PaymentMethodId, MyCulture_EN)
					rEn = tEn.Rows(0)
					tHk = (New PaymentMethodDataSetTableAdapters.PaymentMethodLangTableAdapter).GetDataByPaymentMethodID(r.PaymentMethodId, MyCulture_HK)
					rHk = tHk.Rows(0)

					btnSave.Visible = True
					btnDelete.Visible = True
				End If
			Case FORMMODE_INSERT
				PaymentMethodId = 0
				r = t.NewRow
				PaymentMethodClass.InitRow(r)

				'Lang
				rEn = tEn.NewRow
				PaymentMethodClass.InitRow(rEn, MyCulture_EN)
				rHk = tHk.NewRow
				PaymentMethodClass.InitRow(rHk, MyCulture_HK)

				btnSave.Visible = True
				btnDelete.Visible = False
		End Select

		' --- Bind data to textbox
		With r
			hfdPaymentMethodId.Value = .PaymentMethodId
			txtPaymentPage.Text = .PaymentPage
			txtImageUrl.Text = .ImageUrl
			txtSortOrder.Text = .Sort
		End With

		'Lang
		With rEn
			txtPaymentMethodNameEn.Text = .LangPaymentMethod
			txtPaymentMethodDescriptionEn.Value = .LangDescription
		End With
		With rHk
			txtPaymentMethodNameHk.Text = .LangPaymentMethod
			txtPaymentMethodDescriptionHk.Value = .LangDescription
		End With
	End Sub


	Public Function Insert() As Integer
		'Dim i As Integer

		'Dim adapterLang As New PaymentMethodDataSetTableAdapters.PaymentMethodLangTableAdapter
		'Dim adapter As New PaymentMethodDataSetTableAdapters.PaymentMethodTableAdapter
		'txtSortOrder.Text = adapter.GetMaxSortOrder + 1

		'i = adapter.InsertQuery(txtPaymentMethodNameEn.Text, Convert.ToInt32(txtSortOrder.Text))

		''Lang
		'adapterLang.InsertQuery(i, MyCulture_EN, txtPaymentMethodNameEn.Text, txtPaymentMethodDescriptionEn.Text, txtPaymentMethodImageUrlEn.Text)
		'adapterLang.InsertQuery(i, MyCulture_HK, txtPaymentMethodNameHk.Text, txtPaymentMethodDescriptionHk.Text, txtPaymentMethodImageUrlHk.Text)

		'Return i
	End Function

	Public Sub Update()
		Dim adapterLang As New PaymentMethodDataSetTableAdapters.PaymentMethodLangTableAdapter
		Dim adapter As New PaymentMethodDataSetTableAdapters.PaymentMethodTableAdapter
		adapter.UpdateQuery(txtPaymentMethodNameEn.Text, txtPaymentPage.Text, txtImageUrl.Text, Convert.ToInt32(txtSortOrder.Text), hfdPaymentMethodId.Value)

		' Lang
		adapterLang.UpdateLang(txtPaymentMethodNameEn.Text, txtPaymentMethodDescriptionEn.Value, hfdPaymentMethodId.Value, MyCulture_EN)
		adapterLang.UpdateLang(txtPaymentMethodNameHk.Text, txtPaymentMethodDescriptionHk.Value, hfdPaymentMethodId.Value, MyCulture_HK)
	End Sub

	Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
		Dim CategoryId As Integer
		Select Case txtFormMode.Value
			Case FORMMODE_INSERT
				'Lang
				CategoryId = Insert()
			Case FORMMODE_EDIT
				'Lang
				Update()
		End Select
		BackPageAction()

	End Sub

	Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

		'Dim adapter As New PaymentMethodDataSetTableAdapters.PaymentMethodTableAdapter
		'Dim adapterLang As New PaymentMethodDataSetTableAdapters.PaymentMethodLangTableAdapter

		'adapter.Delete(hfdPaymentMethodId.Value)
		'adapterLang.DeleteLang(hfdPaymentMethodId.Value, MyCulture_EN)
		'adapterLang.DeleteLang(hfdPaymentMethodId.Value, MyCulture_HK)

		'BackPageAction()
	End Sub

	Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
		BackPageAction()
	End Sub

	Protected Sub BackPageAction()
		Response.Redirect("~/backoffice/PaymentMethodList.aspx")
	End Sub

End Class
