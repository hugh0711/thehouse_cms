<%@ WebHandler Language="VB" Class="wine" %>

Imports System
Imports System.Web
Imports MySql.Data.MySqlClient

Public Class wine : Implements IHttpHandler
    Dim constr As String = ConfigurationManager.ConnectionStrings("TheHouseMySql").ConnectionString
    
    Dim mysqlUserDict As New Dictionary(Of String, String)
    Dim TagIDProductTypeDict As New Dictionary(Of Integer, String)
    
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim wineFunctionID = ConfigurationManager.AppSettings("FunctionID_Wine")
        Dim defaultLang = ConfigurationManager.AppSettings("DefaultLanguage")
        'mysqlProductIDDict used for store mysql ProductID and mssql productID for ProductRelated
        Dim mysqlProductIDDict As New Dictionary(Of Integer, Integer)
        Dim db As New ProductDbDataContext()
        
        Dim tagProductTypes = From t In db.view_Tags
                              Where t.FunctionID = wineFunctionID And t.Lang = defaultLang
                              Select t.TagID, t.TagName
                              
        For Each tagProductType In tagProductTypes
            TagIDProductTypeDict.Add(tagProductType.TagID,tagProductType.TagName.ToLower())
        Next
        
        Dim productImageSize = (From s In db.SiteFunctions
                             Where s.FunctionID = wineFunctionID
                             Select s.ProductImageHeight, s.ProductImageWidth, s.ProductThumbnailHeight, s.ProductThumbnailWidth).FirstOrDefault()
        
        
        Dim NextSortOrder As Integer = 1
             
        Try
            NextSortOrder = ((From p In db.Products
                           Where p.FunctionID = wineFunctionID
                           Select p.SortOrder).Max()) + 1
        Catch ex As Exception

        End Try
        
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find user ID and username
            Using UserAcctCom As New MySqlCommand("select * FROM thehouse.th_user_acct", con)
                Using RDR = UserAcctCom.ExecuteReader()
                    If RDR.HasRows Then
                        Do While RDR.Read
                            Dim userID As String = RDR.Item("user_id").ToString()
                            Dim userName As String = RDR.Item("user_login").ToString()
                            mysqlUserDict.Add(userID, userName)
                        Loop
                    End If
                End Using
            End Using
            'End find user ID and username
            con.Close()
        End Using
           
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find Wine
            Using WineCom As New MySqlCommand("select * FROM thehouse.th_item", con)
                Using WineRDR = WineCom.ExecuteReader()
                    If WineRDR.HasRows Then
                        Do While WineRDR.Read
                            
                            'ProductTypeID for search product tag
                            Dim ProductTypeID As String = WineRDR.Item("wine_type_id").ToString()
                            Dim ProductCountry As String = WineRDR.Item("item_country").ToString().ToLower()
                            
                            
                            Dim itemID As String = WineRDR.Item("item_id").ToString()
                            Dim productName As String = WineRDR.Item("item_name").ToString()
                            Dim ProductDesc As String = WineRDR.Item("item_descr").ToString()
                            Dim ProductCode As String = WineRDR.Item("product_code").ToString()
                            
                            Dim ProductDetailVinetage As String = WineRDR.Item("vinetage").ToString()
                            Dim ProductDetailGrape As String = WineRDR.Item("grape").ToString().Replace(";",",")
                            Dim ProductDetailRegion As String = WineRDR.Item("item_region").ToString()
                            Dim ProductDetailAlcoholString As String = WineRDR.Item("item_alcohol").ToString().Replace("%", "")
                            ProductDetailAlcoholString = IIf(IsNumeric(ProductDetailAlcoholString), ProductDetailAlcoholString, 0)
                            Dim ProductDetailAlcohol As Decimal = Convert.ToDecimal(ProductDetailAlcoholString)
                            
                            Dim ProductDetailBody As String = WineRDR.Item("item_body").ToString()
                            Dim ProductDetailVolume As String = WineRDR.Item("item_volume").ToString()
                            Dim ProductDetailRating As String = WineRDR.Item("intl_rating").ToString()
                            Dim ProductDetailWinery As String = WineRDR.Item("winery").ToString()
                            Dim ProductDetailWebsite As String = WineRDR.Item("winery_website").ToString()
                            
                            Dim ProductVideoUrl As String = WineRDR.Item("video_link").ToString()
                            Dim ProductViewCount As String = WineRDR.Item("view_cnt").ToString()
                            Dim ProductEnabled As Boolean = IIf(WineRDR.Item("status").ToString() = "Active", True, False)
                            Dim ProductSellingStartDate As Date = Date.Parse(IIf(WineRDR.Item("availability").ToString() = "NOW", #1/1/1999#, #1/1/2999#))
                            Dim ProductCreateDate As DateTime = DateTime.Parse(WineRDR.Item("added_dt").ToString())
                            Dim ProductCreatedBy As String = mysqlUserDict.Item(WineRDR.Item("added_by").ToString())
                            Dim ProducttModifyDate As DateTime = DateTime.Parse(WineRDR.Item("updated_dt").ToString())
                            Dim ProductModifyBy As String = mysqlUserDict.Item(WineRDR.Item("updated_by").ToString())
                            Dim ProductSellingEndDate As Date = #1/1/2999#
                            Dim ProductType = "Wine"
                            Dim newProductID As Integer = 0
                            
                            
                            Dim newproduct As New Product With {
                               .FunctionID = wineFunctionID, _
                               .ProductCode = ProductCode, _
                               .Name = "", _
                               .Description = "", _
                               .MOQ = 0, _
                               .MOQUnit = "", _
                               .ProductionLeadTime = "", _
                               .CreateDate = ProductCreateDate, _
                               .CreatedBy = ProductCreatedBy, _
                               .ModifyDate = ProducttModifyDate, _
                               .ModifiedBy = ProductModifyBy, _
                               .Enabled = ProductEnabled, _
                               .SortOrder = NextSortOrder, _
                               .SellingPrice = 0, _
                               .Cost = 0, _
                               .DiscountPrice = 0, _
                               .ShippingFee = 0, _
                               .Weight = 0, _
                               .Height = 0, _
                               .Width = 0, _
                               .Depth = 0, _
                               .SellingStartDate = ProductSellingStartDate, _
                               .SellingEndDate = ProductSellingEndDate, _
                               .fileUrl = "", _
                               .NavigateUrl = "", _
                               .VideoUrl = ProductVideoUrl, _
                               .Video3D = False, _
                               .Author = "", _
                               .CameraModel = ""}
                            
                            NextSortOrder += 1
                            
                            db.Products.InsertOnSubmit(newproduct)
                            
                            Try
                                db.SubmitChanges()
                                newProductID = newproduct.ProductID
                            Catch ex As Exception
                                context.Response.Write(ex.Message())
                            End Try
                            
                            
                            
                            If newProductID = 0 Then
                                context.Response.Write(String.Format("<br />error:{0}", itemID))
                                Exit Sub
                            Else
                                mysqlProductIDDict.Add(itemID, newProductID)
                            
                                'Save ProductDetail
                                Dim newProductDetail As New ProductDetail With {
                                    .ProductID = newProductID, _
                                    .Vintage = ProductDetailVinetage, _
                                    .Grape = ProductDetailGrape, _
                                    .Alcohol = ProductDetailAlcohol, _
                                    .Region = ProductDetailRegion, _
                                    .Body = ProductDetailBody,
                                    .Volume = ProductDetailVolume, _
                                    .Winery = ProductDetailWinery, _
                                    .Website = ProductDetailWebsite, _
                                    .InternationalRatings = ProductDetailRating}
                            
                                db.ProductDetails.InsertOnSubmit(newProductDetail)
                            
                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception

                                End Try
                                'End Save ProductDetail
                            
                            
                                'Save ProductName
                                Dim newProductName As New ProductName With {
                                    .ProductID = newProductID, _
                                    .Lang = defaultLang, _
                                    .ProductName = productName, _
                                    .Description = ProductDesc, _
                                    .fileUrl = "", _
                                    .LongDescription = "",
                                    .fileUrl2 = ""}
                            
                                db.ProductNames.InsertOnSubmit(newProductName)
                            
                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception

                                End Try
                                'End Save ProductName
                            
                                'Save ViewCount
                                Dim newProductViewCount As New ViewCount With {
                                    .Type = ProductType, _
                                    .ReferenceID = newProductID, _
                                    .LastView = Date.Now, _
                                    .ViewCount = ProductViewCount}
                            
                                db.ViewCounts.InsertOnSubmit(newProductViewCount)
                            
                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception

                                End Try
                                'End Save ViewCount
                            
                                'Save ProductTag(Type)
                                SaveProductTag(newProductID, ProductTypeID)
                            
                                Dim foundCountryTagID = TagIDProductTypeDict.Where(Function(c) c.Value = ProductCountry).Select(Function(c) c.Key).FirstOrDefault()
                                Dim newProductTag As New ProductTag With {
                                    .ProductID = newProductID, _
                                    .TagID = foundCountryTagID}
                                db.ProductTags.InsertOnSubmit(newProductTag)
                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception
                                End Try
                                'End Save ProductTag(Type)
                            
                                SaveNewWineImage(itemID, newProductID, productImageSize.ProductImageWidth, productImageSize.ProductImageHeight, productImageSize.ProductThumbnailWidth, productImageSize.ProductThumbnailHeight)
                            End If
                        Loop
                    End If
                End Using
            End Using
            'End Find Wine
            con.Close()
        End Using
        
        'Save ProductRelated
        Using con As New MySqlConnection(constr)
            con.Open()
            Using wineRelatedCom As New MySqlCommand("select * FROM thehouse.th_item_related where related_item_id !=0", con)
                Using wineRelatedRDR = wineRelatedCom.ExecuteReader()
                    If wineRelatedRDR.HasRows Then
                        Do While wineRelatedRDR.Read
                            Dim ProductID As String = mysqlProductIDDict.Item(wineRelatedRDR.Item("item_id").ToString())
                            Dim ProductRELATEDID As String = mysqlProductIDDict.Item(wineRelatedRDR.Item("related_item_id").ToString())
                                            
                            Dim newProductRelated As New ProductRelated With {
                                .ProductID = ProductID, _
                                .RelatedID = ProductRELATEDID, _
                                .CreatedBy = mysqlUserDict.Item(wineRelatedRDR.Item("added_by").ToString()), _
                                .CreateDate = DateTime.Parse(wineRelatedRDR.Item("added_dt").ToString()), _
                                .ModifiedBy = mysqlUserDict.Item(wineRelatedRDR.Item("updated_by").ToString()), _
                                .ModifyDate = DateTime.Parse(wineRelatedRDR.Item("updated_dt").ToString()), _
                                .Enabled = True,
                                .RelatedType="Wine"}
                                  
                            db.ProductRelateds.InsertOnSubmit(newProductRelated)
                            
                            Try
                                db.SubmitChanges()
                            Catch ex As Exception

                            End Try
                            
                        Loop
                    End If
                End Using
            End Using
            con.Close()
        End Using
        'End ProductRelated
        
        context.Response.Write("End")
        
    End Sub
    
    Protected Sub SaveProductTag(ByVal newProductID As String, ByVal ProductTypeID As String)
        Dim db As New ProductDbDataContext()
        
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find wine_type
            Using winetypeCommentCom As New MySqlCommand(String.Format("select * FROM thehouse.th_wine_type where wine_type_id ={0}",ProductTypeID ), con)
                Using winetypeCommentRDR = winetypeCommentCom.ExecuteReader()
                    If winetypeCommentRDR.HasRows Then
                        Do While winetypeCommentRDR.Read
                            Dim ProductTags As String() = winetypeCommentRDR.Item("wine_type").ToString().ToLower().Split(";")
                            If ProductTags.Count > 0 Then
                                For Each TagName In ProductTags
                                    Dim findTagID = TagIDProductTypeDict.Where(Function(c) c.Value = TagName).Select(Function(c) c.Key).FirstOrDefault()
                                
                                    If Not findTagID = 0 Then
                                        'Save ProductTag(Type)
                                        Dim newProductTag As New ProductTag With {
                                            .ProductID = newProductID, _
                                            .TagID = findTagID}
                                        db.ProductTags.InsertOnSubmit(newProductTag)
                                        Try
                                            db.SubmitChanges()
                                        Catch ex As Exception
                                        End Try
                                        'End Save ProductTag(Type)
                                    End If
                                
                                Next
                            End If
                            
                        Loop
                    End If
                End Using
            End Using
            'End find wine_type
            con.Close()
        End Using
        
    End Sub
    
    Protected Sub SaveNewWineImage(itemID As String, newProductID As String, ByVal ProductImageWidth As Integer, ByVal ProductImageHeight As Integer, ByVal ProductThumbnailWidth As Integer, ByVal ProductThumbnailHeight As Integer)
        
        Dim db As New ProductDbDataContext()
        
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find article image
            Using articleImageCom As New MySqlCommand(String.Format("select * FROM thehouse.th_item_image where item_id ={0} and status='Active'", itemID), con)
                Using articleImageRDR = articleImageCom.ExecuteReader()
                    If articleImageRDR.HasRows Then
                        Do While articleImageRDR.Read
                            Dim ProductUrl As String = String.Format("http://thehouse.com.hk/{0}", articleImageRDR.Item("image_loc").ToString())
                            Dim ProductSort As Integer = 0
                                
                            'run if picture format is jpg or png
                            If IO.Path.GetExtension(ProductUrl).ToString().Contains("jpg") Or IO.Path.GetExtension(ProductUrl).ToString().Contains("png") Then
                                Dim newProductFilenname = SaveProductImage.SaveProductImage(ProductUrl, ProductImageWidth, ProductImageHeight, ProductThumbnailWidth, ProductThumbnailHeight)
                                Dim primaryFlag As Boolean = IIf(articleImageRDR.Item("primary_flag").ToString() = "Y", True, False)
                                If primaryFlag Then
                                    ProductSort = 1
                                Else
                                    ProductSort = 2
                                End If
                                          
                                If Not newProductFilenname = "" Then
                                    Dim newProductImage As New ProductImage With {
                                    .ProductID = newProductID, _
                                    .Url = newProductFilenname, _
                                    .Width = ProductImageWidth, _
                                    .Height = ProductImageHeight, _
                                    .ThumbnailHeight = ProductThumbnailHeight, _
                                    .ThumbnailWidth = ProductThumbnailWidth, _
                                    .SortOrder = ProductSort}
                                            
                                    db.ProductImages.InsertOnSubmit(newProductImage)
                            
                                    Try
                                        db.SubmitChanges()
                                    Catch ex As Exception

                                    End Try
                                End If
                                
                                    
                                
                            End If
                            
                                            
                        Loop
                    End If
                End Using
            End Using
            'End find article image
            con.Close()
        End Using
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class