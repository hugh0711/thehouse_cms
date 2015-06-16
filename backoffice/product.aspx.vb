Imports System.IO
Imports System.Drawing
Imports System.Data
Imports Utility

Partial Class backoffice_product
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        CreateTab()
    End Sub

    Dim showProductDetail As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        

        If Not Page.IsPostBack Then


            ViewState("dtSize") = CreateDataTable()
            ViewState("TagIDs") = ""
            Bind()
            lblProductID.Text = "0"
            hfdLang.Value = ConfigurationManager.AppSettings("DefaultLanguage")

            'htmlDescription.EditorAreaCSS = "../css/StyleSheet2.css"
            'htmlDescription2.EditorAreaCSS = "../css/StyleSheet2_sc.css"

            If Request.QueryString("id") IsNot Nothing AndAlso Request.QueryString("id") <> "" Then
                lblProductID.Text = Request("id")
                LoadProduct(Convert.ToInt32(Request.QueryString("id")))
                LoadRestaurantOrEventDateTime(lblProductID.Text)

                'SetupTag()
            ElseIf Request.QueryString("category") IsNot Nothing AndAlso Request.QueryString("category") <> "" Then
                LoadCategory(Convert.ToInt32(Request.QueryString("category")))
                'LoadCategory(Convert.ToInt32(Request.QueryString("category")))
                If ViewState("FunctionSettings") IsNot Nothing Then
                    If ViewState("FunctionSettings").hasProductImage Then
                        btnSave.Text = "Next >"
                    End If
                End If
                SetupTab()

                cblTag.DataBind()
                'SetupTag()
            ElseIf Request("fn") IsNot Nothing AndAlso Request("fn") <> "" Then
                LoadSiteFunction(CInt(Request("fn")))

                'ViewState("fn") = CInt(Request("fn"))
                txtCategoryID.Text = "0"
                If ViewState("FunctionSettings") IsNot Nothing Then
                    If ViewState("FunctionSettings").hasProductImage Then
                        btnSave.Text = "Next >"
                    End If
                End If
                SetupTab()

                cblTag.DataBind()
                'SetupTag()
            Else
                'LoadCategory(15)
            End If

            IsProductDetailShowed()
            If hfdFunctionID.Value = ConfigurationManager.AppSettings("FunctionID_Restaurant") Then
                RestaurantPlaceHolder.Visible = True
            End If

            'dsTag.SelectCommand = String.Format("SELECT [TagID], [TagName] FROM [view_Tag] WHERE (([Lang] = @Lang) AND ([FunctionID] = @FunctionID) AND ([Enabled] = @Enabled){0}) ORDER BY [SortOrder]", tagWhereString)

        End If

        If lblProductID.Text = "0" Then
            btnDelete.Visible = False
            ImagePlaceHolder.Visible = False
            btnCreateNewShippingFee.Visible = False
        End If


    End Sub

    Protected Sub IsProductDetailShowed()
        Dim ShowProductDetailFunctionID() = ConfigurationManager.AppSettings("ShowProductDetailFunctionID").Split(",")
        For i As Integer = 0 To ShowProductDetailFunctionID.Length - 1
            If hfdFunctionID.Value = ShowProductDetailFunctionID(i) Then
                showProductDetail = True
                ProductDetailPlaceHolder.Visible = True
            End If
        Next
    End Sub


    Protected Sub SetupTag()
        'check user admin right and set tag
        Dim RoleDictionary = GetAllowUser()
        Dim selectTag As String = ""

        Dim tagWhereString As String = ""

        'set tag
        'Dim tag As ListItem


        setAllTagToNotEnabled()

        If RoleDictionary.Count = 1 Then

            'cblTag.Enabled = False

            Dim alltag As String = "HK,CN,MA"

            If RoleDictionary.ContainsKey("HK_Admin") Then
                'set SQL string
                'tagWhereString &= "and [Tag]='HK'"
                'set select tag 
                selectTag = "HK"

                EnabledTag(selectTag)

                'selected tag which user have right 
                'tag = cblTag.Items.FindByText(selectTag)
                'set tag selected to true
                'tag.Selected = True
            End If
            If RoleDictionary.ContainsKey("MA_Admin") Then
                'set SQL string
                'tagWhereString &= "and [Tag]='MA'"
                'set select tag 
                selectTag = "MA"

                EnabledTag(selectTag)

                'selected tag which user have right 
                'tag = cblTag.Items.FindByText(selectTag)
                'set tag selected to true
                'tag.Selected = True
            End If
            If RoleDictionary.ContainsKey("CN_Admin") Then
                'set SQL string
                'tagWhereString &= "and [Tag]='CN'"
                'set select tag 
                selectTag = "CN"

                EnabledTag(selectTag)

                'selected tag which user have right 
                'tag = cblTag.Items.FindByText(selectTag)
                'set tag selected to true
                'tag.Selected = True
            End If


        ElseIf RoleDictionary.Count > 1 Then

            If RoleDictionary.ContainsKey("HK_Admin") Then
                'set SQL string
                tagWhereString &= "OR ([Tag]='HK')"
                'set select tag 
                selectTag = "HK"

                EnabledTag(selectTag)
            End If
            If RoleDictionary.ContainsKey("MA_Admin") Then
                'set SQL string
                tagWhereString &= "OR ([Tag]='MA')"
                'set select tag 
                selectTag = "MA"


                EnabledTag(selectTag)
            End If
            If RoleDictionary.ContainsKey("CN_Admin") Then
                'set SQL string
                tagWhereString &= "OR ([Tag]='CN')"
                'set select tag 
                selectTag = "CN"

                EnabledTag(selectTag)
            End If
        End If
    End Sub


    Protected Sub setAllTagToNotEnabled()
        Dim allTag As String() = ConfigurationManager.AppSettings("AllTagName").Split(",")

        'set tag
        Dim tag As ListItem
        If cblTag.Items.Count > 0 Then
            For tagIndex As Integer = 0 To allTag.Length - 1
                tag = cblTag.Items.FindByText(allTag(tagIndex))
                tag.Enabled = False
            Next
        End If



    End Sub

    Protected Sub EnabledTag(ByVal enableTageName As String)

        Dim allTag As String() = ConfigurationManager.AppSettings("AllTagName").Split(",")


        Dim tagArray As String() = enableTageName.Split(",")
        'set tag
        Dim tag As ListItem

        For tagIndex As Integer = 0 To allTag.Length - 1

            For Each tagName In tagArray
                If tagName = allTag(tagIndex) Then
                    'selected tag which user have right 
                    tag = cblTag.Items.FindByText(allTag(tagIndex))
                    tag.Enabled = True
                Else
                    'selected tag which user have right 
                    tag = cblTag.Items.FindByText(allTag(tagIndex))
                    'set tag Enabled to False
                    'tag.Enabled = False
                End If
            Next

        Next



    End Sub

    Protected Function GetAllowUser() As Dictionary(Of String, String)



        Dim result As New Dictionary(Of String, String)


        Dim AllowRole As String = "admin,HK_Admin,CN_Admin,MA_Admin"

        Dim CurrentUsername As String = Membership.GetUser().UserName
        Dim Rs As String() = AllowRole.Split(",")


        Dim RoleDictionary As New Dictionary(Of String, String)
        RoleDictionary.Add("HK_Admin", "HK")
        RoleDictionary.Add("CN_Admin", "CN")
        RoleDictionary.Add("MA_Admin", "MA")


        For Each r As String In Rs
            'check current is "Admin" or not
            If Roles.IsUserInRole(CurrentUsername, "Admin") Then
                result = RoleDictionary
            Else
                'run if user is in role
                If Roles.IsUserInRole(CurrentUsername, r) Then
                    result.Add(r, RoleDictionary.Item(r))
                End If
            End If
        Next

        Return result

    End Function




    Protected Sub CreateTab()
        Dim Desc() = ConfigurationManager.AppSettings("LanguageSupportDescription").Split(",")
        Dim Lang() = ConfigurationManager.AppSettings("LanguageSupport").Split(",")

        

        For i As Integer = 0 To Desc.Count - 1
            Dim Tab As New AjaxControlToolkit.TabPanel
            Tab.HeaderText = Desc(i)

            Dim ProductDetail As UserControl = LoadControl("~/control/ProductLangControl.ascx")
            With CType(ProductDetail, control_ProductLangControl)
                .ID = String.Format("ProductLangControl{0}", i + 1)
                .SetLang(Lang(i))
            End With

            
            Tab.Controls.Add(ProductDetail)

            TabContainer1.Tabs.Add(Tab)




        Next

    End Sub

    Protected Sub SetupTab()
        Dim Lang() = ConfigurationManager.AppSettings("LanguageSupport").Split(",")
        Dim ProductID As Integer = CInt(lblProductID.Text)

        For i As Integer = 0 To TabContainer1.Tabs.Count - 1
            Dim Tab As AjaxControlToolkit.TabPanel = TabContainer1.Tabs(i)
            Dim c As UserControl = Tab.FindControl(String.Format("ProductLangControl{0}", i + 1))
            With CType(c, control_ProductLangControl)
                .LoadSiteFunction(ViewState("fn"))
                '.LoadProductLang(ProductID, Lang(i))
            End With
        Next

    End Sub

    Protected Sub LoadTab()
        Dim Lang() = ConfigurationManager.AppSettings("LanguageSupport").Split(",")
        Dim ProductID As Integer = CInt(lblProductID.Text)

        For i As Integer = 0 To TabContainer1.Tabs.Count - 1
            Dim Tab As AjaxControlToolkit.TabPanel = TabContainer1.Tabs(i)
            Dim c As UserControl = Tab.FindControl(String.Format("ProductLangControl{0}", i + 1))
            With CType(c, control_ProductLangControl)
                .LoadSiteFunction(ViewState("fn"))
                .LoadProductLang(ProductID, Lang(i))
            End With
        Next

    End Sub

    Protected Sub SaveTab()
        Dim ProductID As Integer = CInt(lblProductID.Text)

        For i As Integer = 0 To TabContainer1.Tabs.Count - 1
            Dim Tab As AjaxControlToolkit.TabPanel = TabContainer1.Tabs(i)
            Dim c As UserControl = Tab.FindControl(String.Format("ProductLangControl{0}", i + 1))
            CType(c, control_ProductLangControl).SaveProductLang(ProductID)
        Next
    End Sub


    Protected Sub LoadSiteFunction(ByVal FunctionID As Integer)
        Dim Site As New SiteFunctionClass(FunctionID)
        With Site
            lblFunctionName.Text = .FunctionName
            btnDelete_ConfirmButtonExtender.ConfirmText = String.Format("Are you sure to delete {0}?", .FunctionName)
            CategoryPlaceHolder.Visible = .HasCategory
            ProductCodePlaceHolder.Visible = .HasProductCode

            DetailsPlaceHolder.Visible = .HasDetails
            PricePlaceHolder.Visible = .HasPrice
            DatePlaceHolder.Visible = .HasDateRange
            TagPlaceHolder.Visible = .HasTag
            ImagePlaceHolder.Visible = (.HasProductImage And (lblProductID.Text <> "0" And lblProductID.Text <> ""))
            'FilePlaceHolder.Visible = .HasFile
            'If .HasFile Then
            '    btnBrowse.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtFileUrl.ClientID))
            '    btnBrowse2.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtFileUrl2.ClientID))
            'End If
            'If .HasTag Then
            '    btnBrowseImageEN.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtImageEN.ClientID))
            '    btnBrowseImageTC.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtImageTC.ClientID))
            'End If
        End With
        ViewState("FunctionSettings") = Site
        ViewState("fn") = FunctionID
        hfdFunctionID.Value = FunctionID

        IsProductDetailShowed()

    End Sub

    Protected Sub Bind()
        lvSize.DataSource = ViewState("dtSize")
        lvSize.DataBind()
        cblTag.DataBind()
    End Sub

    Protected Sub LoadCategory(ByVal CategoryID As Integer)
        With CategoryPathControl1
            .CategoryID = CategoryID
            .ShowPath()
        End With
        If CategoryID > 0 Then
            txtCategoryID.Text = CategoryID
            Dim CategoryAdapter As New CategoryDataSetTableAdapters.CategoryTableAdapter()
            ViewState("fn") = CategoryAdapter.GetFunctionID(CategoryID)
            LoadSiteFunction(ViewState("fn"))
        Else
            txtCategoryID.Text = ""
        End If
    End Sub

    Protected Sub LoadProduct(ByVal ProductID As Integer)


        Dim ProductAdaptor As New ProductDataSetTableAdapters.view_ProductTableAdapter()
        Dim ProductTable As ProductDataSet.view_ProductDataTable
        Dim ProductRow As ProductDataSet.view_ProductRow

        ProductTable = ProductAdaptor.GetDataByProductID(ProductID, hfdLang.Value)
        If ProductTable.Rows.Count > 0 Then
            ProductRow = ProductTable.Rows(0)

            With ProductRow
                LoadCategory(.CategoryID)
                txtProductCode.Text = .ProductCode
                'txtName.Text = .Name
                'txtDescription.Text = .Description
                'txtMOQ.Text = VideoClass.GetVideoUrl(.MOQUnit)
                'txtLeadTime.Text = VideoClass.GetFacebookLink(.ProductionLeadTime)
                lblSortOrder.Text = .SortOrder
                chkEnabled.Checked = .Enabled
                'ViewState("fn") = .FunctionID
                LoadSiteFunction(.FunctionID)
                txtStartSellDate.Text = .SellingStartDate.ToString("d/M/yyyy")
                txtEndSellDate.Text = .SellingEndDate.ToString("d/M/yyyy")
                'txtFileUrl.Text = .fileUrl
                txtPrice.Text = AmountToString(.SellingPrice)
                txtDiscountPrice.Text = AmountToString(.DiscountPrice)
                txtShippingFee.Text = AmountToString(.ShippingFee)
                txtShippingFeeOverseas.Text = AmountToString(.Weight)
                txtShippingFeeOverseasExpress.Text = AmountToString(.Height)
                'If Not .IsVideoUrlNull Then
                '    txtVideoUrl.Text = .VideoUrl
                '    chkVideo3D.Checked = .Video3D
                'End If

                If Not .IsAuthorNull Then
                    txtAutor.Text = .Author
                End If

                If Not .IsCameraModelNull Then
                    txtCameraM.Text = .CameraModel
                End If


                Dim Site As SiteFunctionClass = ViewState("FunctionSettings")
                Dim ImageAdapter As New ProductDataSetTableAdapters.ProductImageTableAdapter()
                Dim ImageTable As ProductDataSet.ProductImageDataTable
                Dim ImageRow As ProductDataSet.ProductImageRow
                ImageTable = ImageAdapter.GetDataByImage(ProductID, 1)
                If ImageTable.Rows.Count = 0 Then
                    imgPreview.ImageUrl = Site.ProductImageNoImage & "?" & Now.Ticks
                    imgPreview.Width = Site.ProductThumbnailWidth
                    imgPreview.Height = Site.ProductThumbnailHeight
                Else
                    ImageRow = ImageTable.Rows(0)
                    With ImageRow
                        imgPreview.ImageUrl = Path.Combine(ConfigurationManager.AppSettings("ProductThumbnailPath"), .Url) & "?" & Now().Ticks
                        imgPreview.Width = .ThumbnailWidth
                        imgPreview.Height = .ThumbnailHeight
                    End With
                End If

                'If DetailsPlaceHolder.Visible Then
                '    Dim dt As DataTable = CreateDataTable()
                '    Dim dr As DataRow
                '    Dim SizeAdapter As New ProductDataSetTableAdapters.ProductSizeTableAdapter()
                '    Dim SizeTable As ProductDataSet.ProductSizeDataTable
                '    Dim SizeRow As ProductDataSet.ProductSizeRow

                '    SizeTable = SizeAdapter.GetDataByProductID(ProductID)
                '    Dim i As Integer = 0
                '    For Each SizeRow In SizeTable.Rows
                '        With SizeRow
                '            dr = dt.NewRow()
                '            dr("SizeID") = .SizeID
                '            dr("Size") = .Size
                '            i += 1
                '            dr("SortOrder") = i
                '            dt.Rows.Add(dr)
                '        End With
                '    Next
                '    ViewState("dtSize") = dt
                '    Bind()
                'End If

                If TagPlaceHolder.Visible Then
                    ' Bind Tag
                    Dim TagAdapter As New TagDataSetTableAdapters.ProductTagTableAdapter()
                    Dim TagTable As TagDataSet.ProductTagDataTable
                    Dim TagRow As TagDataSet.ProductTagRow
                    Dim tag As ListItem
                    cblTag.DataBind()
                    TagTable = TagAdapter.GetDataByProductID(ProductID)
                    For Each TagRow In TagTable.Rows
                        tag = cblTag.Items.FindByValue(TagRow.TagID)
                        If tag IsNot Nothing Then
                            tag.Selected = True
                        End If
                    Next
                End If


                'LoadProductLang()

            End With
            Dim MultilanguageSupport As Boolean = CBool(ConfigurationManager.AppSettings("MultilanguageSupport"))
            'btnNameLanguage.Visible = MultilanguageSupport
            'btnDescLanguage.ValidationGroup = MultilanguageSupport

            'SetupTab()

            IsProductDetailShowed()

            Dim db As New ProductDbDataContext

            Dim foundProductDetail = From p In db.ProductDetails
                                    Where p.ProductID = ProductID
                                    Select p

            If showProductDetail Then
                txtVintage.Text = foundProductDetail.FirstOrDefault().Vintage
                txtGrape.Text = foundProductDetail.FirstOrDefault().Grape
                txtAlcohol.Text = foundProductDetail.FirstOrDefault().Alcohol
                txtRegion.Text = foundProductDetail.FirstOrDefault().Region
                txtBody.Text = foundProductDetail.FirstOrDefault().Body
                txtWinery.Text = foundProductDetail.FirstOrDefault().Winery
                txtWebsite.Text = foundProductDetail.FirstOrDefault().Website
                txtInternationalRatings.Text = foundProductDetail.First().InternationalRatings
            End If

            LoadTab()
        End If
    End Sub



    Protected Sub SaveProduct()
        IsProductDetailShowed()

        Dim ProductAdapter As New ProductDataSetTableAdapters.ProductTableAdapter()
        Dim ProductNameAdapter As New ProductDataSetTableAdapters.ProductNameTableAdapter()
        Dim CategoryAdatper As New ProductDataSetTableAdapters.CategoryProductTableAdapter()
        Dim SizeAdapter As New ProductDataSetTableAdapters.ProductSizeTableAdapter()
        Dim TagAdatper As New TagDataSetTableAdapters.ProductTagTableAdapter()
        Dim ProductID As Integer = CInt(lblProductID.Text)
        Dim SortOrder As Integer
        Dim DateFrom As Date = Now().Date
        Dim DateTo As Date = #1/1/2999#
        Dim FunctionID As Integer = CInt(ViewState("fn"))
        Dim SellingPrice As Decimal = 0
        Dim DiscountedPrice As Decimal = 0
        Dim ShippingFeeLocal As Decimal = 0
        Dim ShippingFeeOverseas As Decimal = 0
        Dim ShippingFeeExpress As Decimal = 0
        Dim VideoID As String = ""
        Dim FacebookID As String = ""
        Dim NavigateUrl As String = ""
        Dim VideoUrl As String = ""
        'Dim Video3D As Boolean = chkVideo3D.Checked
        Dim Video3D As Boolean = False

        Dim Vintage As String = ""
        Dim Grape As String = ""
        Dim Alcohol As Decimal = 0
        Dim Region As String = ""
        Dim Body As String = ""
        Dim Volume As Integer = 0
        Dim Winery As String = ""
        Dim Website As String = ""
        Dim InternationalRatings As String = ""

        Dim Autor As String = ""
        Dim camereaModel As String = ""

        If Not String.IsNullOrWhiteSpace(txtAutor.Text) Then
            Autor = txtAutor.Text
        End If
        If Not String.IsNullOrWhiteSpace(txtCameraM.Text) Then
            camereaModel = txtCameraM.Text
        End If


        'If Not String.IsNullOrWhiteSpace(txtVideoUrl.Text) Then
        '    VideoUrl = txtVideoUrl.Text
        'End If
        'If Not String.IsNullOrWhiteSpace(txtVideoUrl.Text) Then
        '    VideoUrl = txtVideoUrl.Text
        'End If


        'If FunctionID = CInt(ConfigurationManager.AppSettings("VideoFunctionID")) AndAlso txtMOQ.Text.IndexOf("youtu.be") <> 0 Then
        '    VideoID = VideoClass.GetVideoID(txtMOQ.Text)
        'Else
        '    VideoID = txtMOQ.Text
        'End If
        'If Not String.IsNullOrWhiteSpace(txtVideoUrl.Text) Then
        '    VideoUrl = txtVideoUrl.Text
        'End If
        'If FunctionID = CInt(ConfigurationManager.AppSettings("VideoFunctionID")) AndAlso txtLeadTime.Text.IndexOf("facebook.com") <> 0 Then
        '    FacebookID = VideoClass.GetFacebookID(txtLeadTime.Text)
        'Else
        '    FacebookID = txtLeadTime.Text
        'End If

        If PricePlaceHolder.Visible Or ProductDetailPlaceHolder.Visible Then
            SellingPrice = Convert.ToDecimal(txtPrice.Text)
            DiscountedPrice = Convert.ToDecimal(txtDiscountPrice.Text)
            ShippingFeeLocal = Convert.ToDecimal(txtShippingFee.Text)
            ShippingFeeOverseas = Convert.ToDecimal(txtShippingFeeOverseas.Text)
            ShippingFeeExpress = Convert.ToDecimal(txtShippingFeeOverseasExpress.Text)
        End If

        If IsDate(txtStartSellDate.Text) Then
            DateFrom = CDate(txtStartSellDate.Text)
        End If
        If IsDate(txtEndSellDate.Text) Then
            DateTo = CDate(txtEndSellDate.Text)
        End If

        ' Save Product
        If ProductID = 0 Then
            ProductAdapter.UpdateSortOrder(CategoryPathControl1.CategoryID)
            'SortOrder = ProductAdapter.GetNextSortOrder(FunctionID)
            SortOrder = 1
            ProductID = ProductAdapter.InsertQuery(FunctionID, txtProductCode.Text, "", "", 0, VideoID, FacebookID, Page.User.Identity.Name, chkEnabled.Checked, SortOrder, SellingPrice, 0, DiscountedPrice, ShippingFeeLocal, ShippingFeeOverseas, ShippingFeeExpress, 0, 0, DateFrom, DateTo, "", NavigateUrl, VideoUrl, Video3D, Autor, camereaModel)
            'ProductNameAdapter.Insert(ProductID, ConfigurationManager.AppSettings("DefaultLanguage"), txtName.Text)
            lblProductID.Text = ProductID

            If showProductDetail Then
                Dim db As New ProductDbDataContext()
                Dim foundProductDetail = From p In db.ProductDetails
                                    Where p.ProductID = ProductID
                                    Select p
                If foundProductDetail.Count = 0 Then

                    If txtAlcohol.Text.Length > 0 Then
                        If IsNumeric(Convert.ToDecimal(txtAlcohol.Text)) Then
                            Alcohol = Convert.ToDecimal(txtAlcohol.Text)
                        End If
                    End If

                    If txtVolume.Text.Length > 0 Then
                        If IsNumeric(CInt(txtVolume.Text)) Then
                            Volume = CInt(txtVolume.Text)
                        End If
                    End If

                    Dim newProductDetail As New ProductDetail
                    newProductDetail.ProductID = ProductID
                    newProductDetail.Vintage = txtVintage.Text
                    newProductDetail.Grape = txtGrape.Text
                    newProductDetail.Alcohol = Alcohol
                    newProductDetail.Region = txtRegion.Text
                    newProductDetail.Body = txtBody.Text
                    newProductDetail.Volume = Volume
                    newProductDetail.Winery = txtWinery.Text
                    newProductDetail.Website = txtWebsite.Text
                    newProductDetail.InternationalRatings = txtInternationalRatings.Text
                    Try
                        db.ProductDetails.InsertOnSubmit(newProductDetail)
                        db.SubmitChanges()
                    Catch ex As Exception
                        lblMessage.Text &= ex.Message
                    End Try
                Else
                    lblMessage.Text &= "Already exist"
                End If
            End If
            'SaveProductName()
            SaveTab()
        Else
            ProductAdapter.UpdateQuery(txtProductCode.Text, "", "", 0, VideoID, FacebookID, Page.User.Identity.Name, chkEnabled.Checked, CInt(lblSortOrder.Text), SellingPrice, 0, DiscountedPrice, ShippingFeeLocal, ShippingFeeOverseas, ShippingFeeExpress, 0, 0, DateFrom, DateTo, "", NavigateUrl, VideoUrl, Video3D, Autor, camereaModel, ProductID)
            'ProductNameAdapter.UpdateProductName(txtName.Text, ProductID, ConfigurationManager.AppSettings("DefaultLanguage"))
            'SaveProductName()

            Dim db As New ProductDbDataContext()
            Dim foundProductDetail = From p In db.ProductDetails
                                    Where p.ProductID = ProductID
                                    Select p
            If showProductDetail Then
                If foundProductDetail.Count = 1 Then
                    Dim theProductDetail = foundProductDetail.FirstOrDefault()
                    theProductDetail.ProductID = ProductID
                    theProductDetail.Vintage = txtVintage.Text
                    theProductDetail.Grape = txtGrape.Text
                    theProductDetail.Alcohol = txtAlcohol.Text
                    theProductDetail.Region = txtRegion.Text
                    theProductDetail.Body = txtBody.Text
                    theProductDetail.Winery = txtWinery.Text
                    theProductDetail.Website = txtWebsite.Text
                    theProductDetail.InternationalRatings = txtInternationalRatings.Text
                    Try
                        db.SubmitChanges()
                    Catch ex As Exception
                        lblMessage.Text &= ex.Message
                    End Try
                End If
            End If
            SaveTab()
        End If




        ' Save Product Category
        If CategoryPlaceHolder.Visible Then
            CategoryAdatper.DeleteByProductID(ProductID)
            CategoryAdatper.Insert(CategoryPathControl1.CategoryID, ProductID)
        End If

        ' Save Product Size
        'If DetailsPlaceHolder.Visible Then
        '    SortOrder = 1
        '    For Each item As ListViewItem In lvSize.Items
        '        Dim lblID As Label = CType(item.FindControl("lblID"), Label)
        '        Dim lblSizeID As Label = CType(item.FindControl("lblSizeID"), Label)
        '        Dim txtSize As TextBox = CType(item.FindControl("txtSize"), TextBox)
        '        If CInt(lblSizeID.Text) = -1 Then
        '            SizeAdapter.Insert(ProductID, txtSize.Text, SortOrder, True)
        '        Else
        '            SizeAdapter.Update(ProductID, txtSize.Text, SortOrder, True, CInt(lblSizeID.Text))
        '        End If
        '        SortOrder += 1
        '    Next
        'End If

        ' Save Product Tag
        If TagPlaceHolder.Visible Then
            ' Insert ProductTag
            Dim TagAdapter As New TagDataSetTableAdapters.ProductTagTableAdapter()
            TagAdapter.DeleteByProductID(ProductID)
            For Each tag As ListItem In cblTag.Items
                If tag.Selected Then
                    TagAdapter.Insert(ProductID, tag.Value)
                End If
            Next
        End If

        If RestaurantPlaceHolder.Visible Then
            SaveRestaurantOrEventDateTime(ProductID)
        End If


    End Sub

 


#Region "Page Event"


    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Dim param As String = ""
        If ViewState("FunctionSettings").hasCategory Then
            If CategoryPathControl1.CategoryID <> 0 Then
                param = "?category=" & CategoryPathControl1.CategoryID
            Else
                param = "?fn=" & ViewState("fn")
            End If
        Else
            param = "?fn=" & ViewState("fn")
        End If

        Response.Redirect("~/backoffice/products.aspx" & param)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Page.IsValid Then
            Try
                Dim IsNew As Boolean = (lblProductID.Text = "0")
                SaveProduct()
                If IsNew And ViewState("FunctionSettings").hasProductImage Then
                    Response.Redirect(String.Format("~/backoffice/product_upload.aspx?id={0}", lblProductID.Text))
				End If

				If Not IsNew Then
					btnCreateNewShippingFee.Visible = True
				End If

				lblMessage.Text = String.Format("{0} is saved", CType(ViewState("FunctionSettings"), SiteFunctionClass).FunctionName)
				lblMessage.ForeColor = Color.Black
			Catch ex As Exception
				lblMessage.Text = ex.Message
				lblMessage.ForeColor = Color.Red
			End Try
        End If
    End Sub


#End Region

#Region "Tree View"

    Protected Sub TreeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.SelectedNodeChanged
        If TreeView1.SelectedNode Is Nothing Then
            lblParentID.Text = "0"
        Else
            Dim CategoryID As Integer = Convert.ToInt32(TreeView1.SelectedNode.Value)
            LoadCategory(CategoryID)
            btnSelectCategory_ModalPopupExtender.Hide()
        End If
        'lblCategory.Text = TreeView1.SelectedNode.Text
        'ReorderList1.DataBind()
    End Sub

    Protected Sub TreeView1_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodePopulate
        'lblCategory.Text = e.Node.Text
        LoadCategoryTree(e.Node, Convert.ToInt32(e.Node.Value))
    End Sub

    Protected Sub LoadCategoryTree(ByVal CurrentNode As TreeNode, ByVal ID As Integer)
        Dim CategoryAdaptor As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        Dim CategoryTable As CategoryDataSet.CategoryDataTable
        Dim CategoryRow As CategoryDataSet.CategoryRow
        Dim node As TreeNode

        CurrentNode.ChildNodes.Clear()
        CategoryTable = CategoryAdaptor.GetDataByParentID(CInt(ID), CInt(ViewState("fn")))
        For Each CategoryRow In CategoryTable.Rows
            With CategoryRow
                node = New TreeNode(.Category, .CategoryID)
                node.PopulateOnDemand = True
                node.SelectAction = TreeNodeSelectAction.SelectExpand
            End With
            CurrentNode.ChildNodes.Add(node)
        Next
        lblParentID.Text = ID
    End Sub

#End Region

#Region "Size"

    Protected Function CreateDataTable() As DataTable
        Dim dtSize As New DataTable
        With dtSize
            .Columns.Add("ID", GetType(Integer))
            .Columns("ID").AutoIncrement = True
            .Columns("ID").AutoIncrementSeed = 1
            .Columns.Add("SizeID", GetType(Integer))
            .Columns("SizeID").DefaultValue = -1
            .Columns.Add("Size", GetType(String))
            .Columns.Add("SortOrder", GetType(Integer))
        End With
        Return dtSize
    End Function


    Protected Sub lvSize_ItemDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewDeleteEventArgs) Handles lvSize.ItemDeleting
        Dim dtSize As DataTable = ViewState("dtSize")
        Dim item As ListViewItem = lvSize.Items(e.ItemIndex)
        Dim ID As Integer = CInt(CType(item.FindControl("lblID"), Label).Text)

        For Each dr As DataRow In dtSize.Rows
            If dr("ID") = ID Then
                dtSize.Rows.Remove(dr)
                Exit For
            End If
        Next
        ViewState("dtSize") = dtSize
        Bind()
    End Sub


    Protected Sub lvSize_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewInsertEventArgs) Handles lvSize.ItemInserting
        Dim dtSize As DataTable = ViewState("dtSize")
        Dim item As ListViewItem = e.Item
        Dim txtSize As TextBox = CType(item.FindControl("txtSize"), TextBox)
        Dim row As DataRow = dtSize.NewRow()
        row("Size") = txtSize.Text
        row("SortOrder") = dtSize.Rows.Count + 1
        dtSize.Rows.Add(row)
        ViewState("dtSize") = dtSize
        Bind()
    End Sub
#End Region

    'Protected Sub btnSelectTag_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectTag.Click
    '    cblTag.DataBind()
    '    For Each item As ListItem In cblTag.Items
    '        item.Selected = False
    '    Next
    '    Dim TagAdapter As New ProductDataSetTableAdapters.view_ProductTagTableAdapter()
    '    Dim TagTable As ProductDataSet.view_ProductTagDataTable
    '    Dim TagRow As ProductDataSet.view_ProductTagRow
    '    TagTable = TagAdapter.GetDataByProductID(CInt(lblProductID.Text), hfdLang.Value)
    '    For Each TagRow In TagTable.Rows
    '        For Each item As ListItem In cblTag.Items
    '            If item.Value = TagRow.TagID Then
    '                item.Selected = True
    '                Exit For
    '            End If
    '        Next
    '    Next
    '    ModalPopupExtender1.Show()
    'End Sub

    'Protected Sub btnTagOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTagOK.Click
    '    'Dim TagAdapter As New TagDataSetTableAdapters.ProductTagTableAdapter()
    '    Dim ProductID As Integer = CInt(lblProductID.Text)
    '    Dim TagList As New List(Of String)
    '    Dim TagIDList As New List(Of Integer)

    '    'TagAdapter.DeleteByProductID(ProductID)
    '    For Each item As ListItem In cblTag.Items
    '        If item.Selected Then
    '            'TagAdapter.Insert(ProductID, item.Value)
    '            TagList.Add(item.Text)
    '            TagIDList.Add(item.Value)
    '        End If
    '    Next

    '    lblTag.Text = Join(TagList.ToArray(), ", ")
    '    ViewState("TagIDs") = TagIDList
    '    ModalPopupExtender1.Hide()
    'End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ProductClass.Delete(CInt(lblProductID.Text))
        'If RestaurantPlaceHolder.Visible Then
        '    deleteRestaurantDatetime(CInt(lblProductID.Text))
        'End If
        'Dim db As New ProductDbDataContext()
        'Dim foundProductDetail = From p In db.ProductDetails
        '                        Where p.ProductID = lblProductID.Text
        '                        Select p
        'If foundProductDetail.Count > 0 Then
        '    For Each productDetailrow In foundProductDetail
        '        db.ProductDetails.DeleteOnSubmit(productDetailrow)
        '    Next
        '    Try
        '        db.SubmitChanges()
        '    Catch ex As Exception
        '        lblMessage.Text = ex.Message()
        '    End Try
        'End If
        Response.Redirect(String.Format("~/backoffice/products.aspx?category={0}&fn={1}", CategoryPathControl1.CategoryID, ViewState("fn")))
    End Sub

    Protected Sub btnImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImage.Click
        Response.Redirect(String.Format("~/backoffice/product_upload.aspx?id={0}", lblProductID.Text))
    End Sub

    'Protected Sub btnLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNameLanguage.Click
    '    Response.Redirect("~/backoffice/product_name.aspx?id=" & lblProductID.Text)
    'End Sub

    'Protected Sub SaveProductName()
    '    ' Create CategoryName to other languages
    '    Dim ProductNameAdapter As New ProductDataSetTableAdapters.ProductNameTableAdapter()
    '    Dim LanguageSupport() As String = ConfigurationManager.AppSettings("LanguageSupport").ToString().Split(",")
    '    Dim DefaultLanguage As String = ConfigurationManager.AppSettings("DefaultLanguage")
    '    Dim ProductName, Descriptoin, FileUrl As String
    '    'Dim Translate As New GoogleTranslateClass
    '    Dim ProductID As Integer = CInt(lblProductID.Text)

    '    For Each ToLang In LanguageSupport
    '        'ProductName = Translate.Translate(txtName.Text, DefaultLanguage, ToLang)
    '        Select Case ToLang.ToLower()
    '            Case "zh-cn"
    '                ProductName = txtName2.Text
    '                If htmlDescription2.Visible Then
    '                    txtDescription2.Text = htmlDescription2.Value
    '                End If
    '                Descriptoin = txtDescription2.Text
    '                If TagPlaceHolder.Visible Then
    '                    Descriptoin = txtImageTC.Text
    '                End If
    '                FileUrl = txtFileUrl2.Text
    '            Case Else
    '                ProductName = txtName.Text
    '                If htmlDescription.Visible Then
    '                    txtDescription.Text = htmlDescription.Value
    '                End If
    '                Descriptoin = txtDescription.Text
    '                If TagPlaceHolder.Visible Then
    '                    Descriptoin = txtImageEN.Text
    '                End If
    '                FileUrl = txtFileUrl.Text
    '        End Select

    '        If ProductNameAdapter.UpdateQuery(ProductName, Descriptoin, FileUrl, ProductID, ToLang) = 0 Then
    '            ProductNameAdapter.Insert(ProductID, ToLang, ProductName, Descriptoin, FileUrl)
    '        End If
    '    Next
    'End Sub

    'Protected Sub LoadProductLang()
    '    Dim ProductLangApdater As New ProductDataSetTableAdapters.ProductNameTableAdapter()
    '    Dim ProductLangTable As ProductDataSet.ProductNameDataTable
    '    Dim ProductLangRow As ProductDataSet.ProductNameRow
    '    Dim ProductID As Integer = CInt(lblProductID.Text)

    '    ProductLangTable = ProductLangApdater.GetDataByProductID(ProductID, "en-us")
    '    If ProductLangTable.Rows.Count > 0 Then
    '        ProductLangRow = ProductLangTable.Rows(0)
    '        With ProductLangRow
    '            txtName.Text = .ProductName
    '            txtDescription.Text = .Description
    '            If htmlDescription.Visible Then
    '                htmlDescription.Value = .Description
    '            End If
    '            txtFileUrl.Text = .fileUrl
    '            If TagPlaceHolder.Visible Then
    '                txtImageEN.Text = .Description
    '            End If
    '        End With
    '    End If

    '    ProductLangTable = ProductLangApdater.GetDataByProductID(ProductID, "zh-cn")
    '    If ProductLangTable.Rows.Count > 0 Then
    '        ProductLangRow = ProductLangTable.Rows(0)
    '        With ProductLangRow
    '            txtName2.Text = .ProductName
    '            txtDescription2.Text = .Description
    '            If htmlDescription2.Visible Then
    '                htmlDescription2.Value = .Description
    '            End If
    '            txtFileUrl2.Text = .fileUrl
    '            If TagPlaceHolder.Visible Then
    '                txtImageTC.Text = .Description
    '            End If
    '        End With
    '    End If

    'End Sub

	Protected Sub btnCreateNewShippingFee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateNewShippingFee.Click
		Response.Redirect(String.Format("~/backoffice/ShippingFee.aspx?ProductID={0}", lblProductID.Text))
	End Sub

	Protected Sub gvShippingFee_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShippingFee.RowCommand
		If e.CommandName = "btnShippingEdit" Then
			Response.Redirect(String.Format("~/backoffice/ShippingFee.aspx?ShippingID={0}&ProductID={1}", e.CommandArgument, lblProductID.Text))
		ElseIf e.CommandName = "btnShippingDelete" Then
			Dim adapter As New ProductDataSetTableAdapters.ShippingFeeTableAdapter
			adapter.Delete(e.CommandArgument)
			gvShippingFee.DataBind()
		End If

    End Sub



    Protected Sub LoadRestaurantOrEventDateTime(ByVal pID As Integer)
        'use day for key, textbox for value
        Dim restaurantDictionary As New Dictionary(Of DayOfWeek, restaurantTextboxClass)
        restaurantDictionary.Add(DayOfWeek.Monday, New restaurantTextboxClass With {.txtFrom = txtMonFrom, .txtTo = txtMonTo})
        restaurantDictionary.Add(DayOfWeek.Tuesday, New restaurantTextboxClass With {.txtFrom = txtTueFrom, .txtTo = txtTueTo})
        restaurantDictionary.Add(DayOfWeek.Wednesday, New restaurantTextboxClass With {.txtFrom = txtWedFrom, .txtTo = txtWedTo})
        restaurantDictionary.Add(DayOfWeek.Thursday, New restaurantTextboxClass With {.txtFrom = txtThuFrom, .txtTo = txtThuTo})
        restaurantDictionary.Add(DayOfWeek.Friday, New restaurantTextboxClass With {.txtFrom = txtFriFrom, .txtTo = txtFriTo})
        restaurantDictionary.Add(DayOfWeek.Saturday, New restaurantTextboxClass With {.txtFrom = txtSatFrom, .txtTo = txtSatTo})
        restaurantDictionary.Add(DayOfWeek.Sunday, New restaurantTextboxClass With {.txtFrom = txtSunFrom, .txtTo = txtSunTo})

        For Each restaurantDatetime In restaurantDictionary
            Dim db As New ProductDbDataContext()
            Dim foundDatetime = From r In db.RestaurantEventDateTimes
                              Where r.ProductID = pID And r.FromDateTime.DayOfWeek = restaurantDatetime.Key
                              Select r

            If foundDatetime.Count = 1 Then
                restaurantDatetime.Value.txtFrom.Text = foundDatetime.FirstOrDefault.FromDateTime.ToString("HHmm")
                restaurantDatetime.Value.txtTo.Text = foundDatetime.FirstOrDefault.FromDateTime.ToString("HHmm")
            End If
        Next


    End Sub

    Protected Sub SaveRestaurantOrEventDateTime(ByVal pID As Integer)
        deleteRestaurantDatetime(pID)

        'use day for key, textbox for value
        Dim restaurantDictionary As New Dictionary(Of String, restaurantTextboxClass)
        restaurantDictionary.Add("19900101", New restaurantTextboxClass With {.txtFrom = txtMonFrom, .txtTo = txtMonTo})
        restaurantDictionary.Add("19900102", New restaurantTextboxClass With {.txtFrom = txtTueFrom, .txtTo = txtTueTo})
        restaurantDictionary.Add("19900103", New restaurantTextboxClass With {.txtFrom = txtWedFrom, .txtTo = txtWedTo})
        restaurantDictionary.Add("19900104", New restaurantTextboxClass With {.txtFrom = txtThuFrom, .txtTo = txtThuTo})
        restaurantDictionary.Add("19900105", New restaurantTextboxClass With {.txtFrom = txtFriFrom, .txtTo = txtFriTo})
        restaurantDictionary.Add("19900106", New restaurantTextboxClass With {.txtFrom = txtSatFrom, .txtTo = txtSatTo})
        restaurantDictionary.Add("19900107", New restaurantTextboxClass With {.txtFrom = txtSunFrom, .txtTo = txtSunTo})

        For Each restaurantDatetime In restaurantDictionary
            Dim db As New ProductDbDataContext()
            Dim fromDt As New DateTime
            Dim toDt As New DateTime
            fromDt = ConvertToDatetime(restaurantDatetime.Key, restaurantDatetime.Value.txtFrom.Text)
            toDt = ConvertToDatetime(restaurantDatetime.Key, restaurantDatetime.Value.txtTo.Text)
            Dim newDatetime As New RestaurantEventDateTime With {
                .ProductID = pID,
                .Enabled = True,
                .FromDateTime = fromDt,
                .ToDateTime = toDt}

            db.RestaurantEventDateTimes.InsertOnSubmit(newDatetime)

            Try
                db.SubmitChanges()
            Catch ex As Exception
                lblMessage.Text = ex.Message()
            End Try
        Next

    End Sub

    Protected Sub deleteRestaurantDatetime(ByVal pID As Integer)
        Dim db As New ProductDbDataContext()
        Dim allDatetime = From r In db.RestaurantEventDateTimes
                         Where r.ProductID = pID
                         Select r
        db.RestaurantEventDateTimes.DeleteAllOnSubmit(allDatetime)

        Try
            db.SubmitChanges()
        Catch ex As Exception
            lblMessage.Text = ex.Message()
        End Try
    End Sub

    Protected Function ConvertToDatetime(ByVal dateString As String, ByVal timeString As String) As DateTime
        Dim returnDatetime As DateTime

        Try
            returnDatetime = DateTime.ParseExact(String.Format("{0}{1}", dateString, timeString), "yyyyMMddHHmm", Nothing)
        Catch ex As Exception
            'set time string to "0000" if timeString format not correct 
            returnDatetime = DateTime.ParseExact(String.Format("{0}{1}", dateString, "0000"), "yyyyMMddHHmm", Nothing)
        End Try

        Return returnDatetime
    End Function

    Public Class restaurantTextboxClass


        Private txtFromValue As TextBox
        Public Property txtFrom() As TextBox
            Get
                ' Gets the property value. 
                Return txtFromValue
            End Get
            Set(ByVal Value As TextBox)
                ' Sets the property value.
                txtFromValue = Value
            End Set
        End Property

        Private txtToValue As TextBox
        Public Property txtTo() As TextBox
            Get
                ' Gets the property value. 
                Return txtToValue
            End Get
            Set(ByVal Value As TextBox)
                ' Sets the property value.
                txtToValue = Value
            End Set
        End Property
    End Class


End Class
