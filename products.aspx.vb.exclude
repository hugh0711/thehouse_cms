Imports System.Data
Imports Utility

Partial Class products
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Bind()
		End If
	End Sub

	Protected Sub Bind()
		hfdProductID.Value = 0
		hfdCategoryID.Value = 0

		If (Request.QueryString("ProductID") & "").Length > 0 Then
			hfdProductID.Value = Request.QueryString("ProductID")
		End If
		If (Request.QueryString("CategoryID") & "").Length > 0 Then
			hfdCategoryID.Value = Request.QueryString("CategoryID")
		End If


		Dim r As ProductDataSet.view_ProductRow = Nothing
		Dim t As New ProductDataSet.view_ProductDataTable

		If hfdProductID.Value = 0 And hfdCategoryID.Value = 0 Then
			r = GetFirstProductByCategoryID(GetFirstCategory)
		ElseIf hfdCategoryID.Value > 0 And hfdProductID.Value = 0 Then
			r = GetFirstProductByCategoryID(hfdCategoryID.Value)
		ElseIf hfdProductID.Value > 0 Then
			t = (New ProductDataSetTableAdapters.view_ProductTableAdapter).GetDataByProductID(hfdProductID.Value, MyCulture)
			If t.Rows.Count > 0 Then
				r = t.Rows(0)
			End If
		End If

		If r IsNot Nothing Then
			hfdProductID.Value = r.ProductID
			hfdCategoryID.Value = r.CategoryID

			lblProductName.Text = r.ProductName
			imgProductPhoto.ImageUrl = GetImage(r.ProductID)
			ltrProductInformation.Text = Replace(r.Description, vbCrLf, "<br />")
			lblPrice.Text = AmountToString(r.SellingPrice)
			lblDiscountedPrice.Text = AmountToString(r.DiscountPrice)
			'lblShippingLocal.Text = AmountToString(r.ShippingFee)
			'lblShippingOverseas.Text = AmountToString(r.Weight)
			'lblShippingExpress.Text = AmountToString(r.Height)
			If Not r.DiscountPrice > 0 Then
				locDiscountPrice.Visible = False
				lblDiscountedPrice.Visible = False
			Else
				lblPrice.Font.Strikeout = True
			End If
			txtQuantity.Text = 1
		End If

		If Not Membership.GetUser Is Nothing Then
			pnlWriteComment.Visible = True
		Else
			pnlWriteComment.Visible = False
		End If

		If MyCulture() = MyCulture_EN Then
			locMenuTitle.Text = "Category"
			btnPostComment.Text = "Post"
			locProductInformation.Text = "Product Information"
			locComment.Text = "Comment"
			locPrice.Text = "Price"
			locDiscountPrice.Text = "Discount"
			locShippingFee.Text = "* Shipping Fee and Handling Charge is not included"
			locQuantity.Text = "Quantity"
			btnAddCart.Text = "Add to Cart"
			compvaltxtQuantity.ErrorMessage = "<br />Quantity incorrect"
			reqvaltxtQuantity.ErrorMessage = "<br />Quantity required"
			'locShippingLocal.Text = "Hong Kong"
			'locShippingOverseas.Text = "Overseas"
			'locShippingExpress.Text = "Overseas Express"
		End If
	End Sub

	Protected Function GetFirstProductByCategoryID(ByVal CategoryID As Integer) As ProductDataSet.view_ProductRow
		Dim r As ProductDataSet.view_ProductRow = Nothing
		Dim t As New ProductDataSet.view_ProductDataTable
		t = (New ProductDataSetTableAdapters.view_ProductTableAdapter).GetLastestProduct(CategoryID, 2, MyCulture)
		If t.Rows.Count > 0 Then
			r = t.Rows(0)
		End If
		Return r
	End Function

	Protected Function GetFirstCategory() As Integer
		Dim id As Integer = 0
		Dim r As CategoryDataSet.CategoryRow
		Dim t As New CategoryDataSet.CategoryDataTable
		t = (New CategoryDataSetTableAdapters.CategoryTableAdapter).GetDataByParentIDEnabled(0, 2)
		If t.Rows.Count > 0 Then
			r = t.Rows(0)
			id = r.CategoryID
		End If
		Return id
	End Function

	Protected Function GetImage(ByVal ProductID As Integer) As String
		Dim s As String = ""
		Dim imageRow As ProductDataSet.ProductImageRow
		Dim imageTable As New ProductDataSet.ProductImageDataTable
		imageTable = (New ProductDataSetTableAdapters.ProductImageTableAdapter).GetDataByProductID(ProductID)
		If imageTable.Rows.Count > 0 Then
			imageRow = imageTable.Rows(0)
			s = System.IO.Path.Combine(ConfigurationManager.AppSettings("ProductImagePath"), imageRow.Url)
		End If
		Return s
	End Function

	Protected Function IsCategorySelect(ByVal CategoryID As Integer) As String
		Dim s As String = ""
		If CategoryID = hfdCategoryID.Value Then
			s = "categoryselect"
		End If
		Return s
	End Function

	Protected Function IsDisplayProduct(ByVal CategoryID As Integer) As String
		Dim s As String = "false"
		If CategoryID = hfdCategoryID.Value Then
			s = "true"
		End If
		Return s
	End Function

	Protected Function DisplayUserName(ByVal UserID As String) As String
		Return UserID
	End Function

	Protected Sub btnPostComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPostComment.Click
		If txtWriteComment.Text.Trim.Length > 0 Then
			CommentClass.PostComment(hfdProductID.Value, CommentClass.CommentType_Product, txtWriteComment.Text, Membership.GetUser.UserName)
			txtWriteComment.Text = ""
			lstComment.DataBind()
		End If
	End Sub

	Protected Sub btnAddCart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCart.Click
		'If Page.IsValid Then
		'	Dim cart As DataTable = ShoppingCart.GetCart
		'	Dim price As Decimal = 0
		'	If Convert.ToDecimal(lblDiscountedPrice.Text) > 0 Then
		'		price = Convert.ToDecimal(lblDiscountedPrice.Text)
		'	Else
		'		price = Convert.ToDecimal(lblPrice.Text)
		'	End If
		'	ShoppingCart.AddToCart(cart, hfdProductID.Value, "", "", Convert.ToInt32(txtQuantity.Text), price, 0)
		'	Session("cart") = cart
		'End If

		Dim Cart As DsCart = GetShoppingCart()
		Dim CartRow As DrCart
		Dim r As ProductDataSet.view_ProductRow
		Dim t As New ProductDataSet.view_ProductDataTable
		Dim price As Decimal = 0

		t = (New ProductDataSetTableAdapters.view_ProductTableAdapter).GetDataByProductID(hfdProductID.Value, MyCulture)
		If t.Rows.Count > 0 Then
			CartRow = Cart.Table.NewRow
			CartRow.Reset()
			r = t.Rows(0)
			With CartRow
				.ProductId = r.ProductID
				.ProductCode = r.ProductCode
				.ProductName = r.ProductName
				.Quantity = Convert.ToInt32(txtQuantity.Text)

				If Convert.ToDecimal(r.DiscountPrice) > 0 Then
					price = r.DiscountPrice
				Else
					price = r.SellingPrice
				End If
				.Price = price * .Quantity
				'.ShippingFee = r.ShippingFee * .Quantity
				.ShippingFee = 0
			End With
			Cart.AddRow(CartRow)
			'Response.Redirect("~/ShoppingCart.aspx")

		End If
	End Sub
End Class
