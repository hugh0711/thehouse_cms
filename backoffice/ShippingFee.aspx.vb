
Partial Class backoffice_ShippingFee
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Protected Sub Bind()
		hfdShippingID.Value = 0
		If (Request.QueryString("ShippingID") & "").Length > 0 Then
			hfdShippingID.Value = Convert.ToInt32(Request.QueryString("ShippingID"))
		End If

		hfdProductID.Value = 0
		If (Request.QueryString("ProductID") & "").Length > 0 Then
			hfdProductID.Value = Convert.ToInt32(Request.QueryString("ProductID"))
		End If

		Dim r As ProductDataSet.ShippingFeeRow
		Dim t As New ProductDataSet.ShippingFeeDataTable
		r = t.NewRow

		If hfdShippingID.Value > 0 Then
			'Edit
			t = (New ProductDataSetTableAdapters.ShippingFeeTableAdapter).GetDataByShippingID(hfdShippingID.Value)
			If t.Rows.Count > 0 Then
				r = t.Rows(0)
				With r
					txtMinQty.Text = .MinQty
					txtMaxQty.Text = .MaxQty
					txtLocal.Text = .Local
					txtOverseas.Text = .OverSeas
					txtOverseasExpress.Text = .OverSeasExpress
				End With
			End If
		Else
			'Insert
			txtMinQty.Text = 0
			txtMaxQty.Text = 0
			txtLocal.Text = 0
			txtOverseas.Text = 0
			txtOverseasExpress.Text = 0
		End If

	End Sub

	Protected Sub Insert()
		Dim adapter As New ProductDataSetTableAdapters.ShippingFeeTableAdapter
		adapter.Insert(hfdProductID.Value, txtMinQty.Text, txtMaxQty.Text, txtLocal.Text, txtOverseas.Text, txtOverseasExpress.Text)
	End Sub

	Protected Sub Update()
		Dim adapter As New ProductDataSetTableAdapters.ShippingFeeTableAdapter
		adapter.Update(hfdProductID.Value, txtMinQty.Text, txtMaxQty.Text, txtLocal.Text, txtOverseas.Text, txtOverseasExpress.Text, hfdShippingID.Value)
	End Sub

	Protected Sub btnShippingCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShippingCancel.Click
		Response.Redirect(String.Format("~/backoffice/product.aspx?id={0}", hfdProductID.Value))
	End Sub

	Protected Sub btnShippingSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShippingSave.Click
		If hfdShippingID.Value > 0 Then
			Update()
		Else
			Insert()
		End If
		Response.Redirect(String.Format("~/backoffice/product.aspx?id={0}", hfdProductID.Value))
	End Sub


End Class
