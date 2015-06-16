<%@ WebHandler Language="VB" Class="restaurant" %>

Imports System
Imports System.Web
Imports MySql.Data.MySqlClient

Public Class restaurant : Implements IHttpHandler
    
    Dim constr As String = ConfigurationManager.ConnectionStrings("TheHouseMySql").ConnectionString
    Dim mysqlUserDict As New Dictionary(Of String, String)
    Dim defaultLang As string= ConfigurationManager.AppSettings("DefaultLanguage")
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim restaurantFunctionID = ConfigurationManager.AppSettings("FunctionID_Restaurant")
        'mysqlProductIDDict used for store mysql ProductID and mssql restID for ProductRelated
        Dim mysqlProductIDDict As New Dictionary(Of Integer, Integer)
        Dim db As New ProductDbDataContext()
        
        Dim productImageSize = (From s In db.SiteFunctions
                             Where s.FunctionID = restaurantFunctionID
                             Select s.ProductImageHeight, s.ProductImageWidth, s.ProductThumbnailHeight, s.ProductThumbnailWidth).FirstOrDefault()
        
        
        Dim NextSortOrder As Integer = 1
             
        Try
            NextSortOrder = ((From p In db.Products
                           Where p.FunctionID = restaurantFunctionID
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
            Using restaurantCom As New MySqlCommand("select * FROM thehouse.th_restaurant", con)
                Using restaurantRDR = restaurantCom.ExecuteReader()
                    If restaurantRDR.HasRows Then
                        Do While restaurantRDR.Read
                            Dim restID As String = restaurantRDR.Item("rest_id").ToString()
                            
                            Dim RestDetailAddr As String = restaurantRDR.Item("addr").ToString()
                            Dim RestDetailGoogleMap As String = restaurantRDR.Item("google_map").ToString()
                            Dim RestDetailSingleUse As Boolean = IIf(restaurantRDR.Item("single_use").ToString() = "Y", True, False)
                            Dim RestDetailPhoneNum As String = restaurantRDR.Item("phone_num").ToString()
                            Dim RestDetailEmail As String = restaurantRDR.Item("email_addr").ToString()
                            Dim RestDetailMenu As String = restaurantRDR.Item("rest_menu").ToString()
                            Dim RestDetailMenuFilenname = SaveProductImage.SaveProductImage(String.Format("http://thehouse.com.hk/{0}", RestDetailMenu), productImageSize.ProductImageWidth, productImageSize.ProductImageHeight, productImageSize.ProductThumbnailWidth, productImageSize.ProductThumbnailHeight)
                            Dim RestDetailCuisine As String = restaurantRDR.Item("cuisine").ToString()
                            Dim RestDetailAvgExpense As Decimal = Convert.ToDecimal(restaurantRDR.Item("avg_expense").ToString())
                            Dim RestDetailSeats As Integer = IIf(restaurantRDR.Item("num_of_seats").ToString() = "-1", 0, Convert.ToInt32(restaurantRDR.Item("num_of_seats").ToString()))
                            Dim RestDetailWineStock As Integer = IIf(restaurantRDR.Item("wine_stock").ToString() = "-1", 0, Convert.ToInt32(restaurantRDR.Item("wine_stock").ToString()))
                            Dim RestDetailPayMethod As String = restaurantRDR.Item("pay_method").ToString()
                            
                            Dim productName As String = restaurantRDR.Item("rest_name").ToString()
                            Dim ProductDesc As String = restaurantRDR.Item("rest_descr").ToString()
                            Dim ProductNavigateUrl As String = restaurantRDR.Item("rest_website").ToString()
                            Dim ProductVideoUrl As String = restaurantRDR.Item("video_link").ToString()
                            Dim ProductViewCount As String = restaurantRDR.Item("view_cnt").ToString()
                            Dim ProductEnabled As Boolean = IIf(restaurantRDR.Item("status").ToString() = "Published", True, False)
                            Dim ProductCreateDate As DateTime = DateTime.Parse(restaurantRDR.Item("added_dt").ToString())
                            Dim ProductCreatedBy As String = mysqlUserDict.Item(restaurantRDR.Item("added_by").ToString())
                            Dim ProducttModifyDate As DateTime = DateTime.Parse(restaurantRDR.Item("updated_dt").ToString())
                            Dim ProductModifyBy As String = mysqlUserDict.Item(restaurantRDR.Item("updated_by").ToString())
                            Dim ProductSellingStartDate As Date = #1/1/1999#
                            Dim ProductSellingEndDate As Date = #1/1/2999#
                            Dim ProductType = "Restaurant"
                            Dim newProductID As Integer = 0
                            
                            
                            Dim newproduct As New Product With {
                               .FunctionID = restaurantFunctionID, _
                               .ProductCode = "", _
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
                               .NavigateUrl = ProductNavigateUrl, _
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
                                context.Response.Write(String.Format("<br />error:{0}", restID))
                                Exit Sub
                            Else
                                mysqlProductIDDict.Add(restID, newProductID)
                            
                                'Save RestaurantDetail
                                Dim newRestaurantDetail As New RestaurantDetail With {
                                    .ProductID = newProductID, _
                                    .Address = RestDetailAddr, _
                                    .GoogleMap = RestDetailGoogleMap, _
                                    .SingleUse = RestDetailSingleUse, _
                                    .PhoneNumber = RestDetailPhoneNum, _
                                    .Email = RestDetailEmail,
                                    .RestMenu = RestDetailMenuFilenname, _
                                    .Cuisine = RestDetailCuisine, _
                                    .AvgExpense = RestDetailAvgExpense, _
                                    .SeatsNumber = RestDetailSeats, _
                                    .WineStock = RestDetailWineStock, _
                                    .PayMethod = RestDetailPayMethod}
                            
                                db.RestaurantDetails.InsertOnSubmit(newRestaurantDetail)
                            
                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception

                                End Try
                                'End Save RestaurantDetail
                            
                            
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
                            
                                SaveRestOpeningHours(restID, newProductID)
                                
                                SaveNewRestImage(restID, newProductID, productImageSize.ProductImageWidth, productImageSize.ProductImageHeight, productImageSize.ProductThumbnailWidth, productImageSize.ProductThumbnailHeight)
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
            Using wineRelatedCom As New MySqlCommand("select * FROM thehouse.th_restaurant_item where item_id !=0", con)
                Using wineRelatedRDR = wineRelatedCom.ExecuteReader()
                    If wineRelatedRDR.HasRows Then
                        Do While wineRelatedRDR.Read
                            Dim ProductID As String = mysqlProductIDDict.Item(wineRelatedRDR.Item("rest_id").ToString())
                            Dim ProductWineID As String = FindWineID(wineRelatedRDR.Item("item_id").ToString())
                                            
                            Dim newProductRelated As New ProductRelated With {
                                .ProductID = ProductID, _
                                .RelatedID = ProductWineID, _
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
    
    Protected Function FindWineID(ByVal wineID As String) As Integer
        Dim db As New ProductDbDataContext()
        Dim returnProductID As Integer = 0
        
        'Save th_item
        Using con As New MySqlConnection(constr)
            con.Open()
            Using wineRelatedCom As New MySqlCommand(String.Format("select * FROM thehouse.th_item where item_id ={0}", wineID), con)
                Using wineRelatedRDR = wineRelatedCom.ExecuteReader()
                    If wineRelatedRDR.HasRows Then
                        Do While wineRelatedRDR.Read
                            Dim ProductName As String = wineRelatedRDR.Item("item_name").ToString()
                            Dim ProductCode As String = wineRelatedRDR.Item("product_code").ToString()
                            Dim ProductVolume As String = wineRelatedRDR.Item("item_volume").ToString()
                            
                            returnProductID = (From p In db.view_productDetails
                                                Where p.ProductName = ProductName And p.Lang = defaultLang And p.ProductCode = ProductCode And p.Volume = ProductVolume
                                                Select p.ProductID).FirstOrDefault()
                            
                        Loop
                    End If
                End Using
            End Using
            con.Close()
        End Using
        'End th_item
        
        Return returnProductID
    End Function
 
    Protected Sub SaveRestOpeningHours(restID As String, newProductID As String)
        Dim db As New ProductDbDataContext()
        'use day for key, mysql day column for value
        Dim restaurantDictionary As New Dictionary(Of String, String)
        restaurantDictionary.Add("1990/01/01", "mon")
        restaurantDictionary.Add("1990/01/02", "tue")
        restaurantDictionary.Add("1990/01/03", "wed")
        restaurantDictionary.Add("1990/01/04", "thu")
        restaurantDictionary.Add("1990/01/05", "fri")
        restaurantDictionary.Add("1990/01/06", "sat")
        restaurantDictionary.Add("1990/01/07", "sun")
        
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find th_restaurant_hrs
            Using restOpeningHoursCommentCom As New MySqlCommand(String.Format("select * FROM thehouse.th_restaurant_hrs where rest_id ={0}", restID), con)
                Using restOpeningHoursCommentRDR = restOpeningHoursCommentCom.ExecuteReader()
                    If restOpeningHoursCommentRDR.HasRows Then
                        Do While restOpeningHoursCommentRDR.Read

                            For Each restaurantDatetime In restaurantDictionary
                                Dim fromDt As New DateTime
                                Dim toDt As New DateTime
                                Dim foundOpeningHour As String = restOpeningHoursCommentRDR.Item(restaurantDatetime.Value).ToString().ToUpper()
                                
                                If foundOpeningHour.Length = 0 Then
                                    fromDt = ConvertToDatetime(restaurantDatetime.Key, "00:00am")
                                    toDt = ConvertToDatetime(restaurantDatetime.Key, "00:00am")
                                Else
                                    'restaurantDatetime format: 12:00pm-10:00pm
                                    Dim splitOpeningHour As String() = foundOpeningHour.Split("-")
                                    
                                    fromDt = ConvertToDatetime(restaurantDatetime.Key, splitOpeningHour(0))
                                    toDt = ConvertToDatetime(restaurantDatetime.Key, splitOpeningHour(1))
                                End If
                                
                                Dim RestOpeningHourCreateDate As DateTime = DateTime.Parse(restOpeningHoursCommentRDR.Item("added_dt").ToString())
                                Dim RestOpeningHourCreatedBy As String = mysqlUserDict.Item(restOpeningHoursCommentRDR.Item("added_by").ToString())
                                Dim RestOpeningHourModifyDate As DateTime = DateTime.Parse(restOpeningHoursCommentRDR.Item("updated_dt").ToString())
                                Dim RestOpeningHourModifyBy As String = mysqlUserDict.Item(restOpeningHoursCommentRDR.Item("updated_by").ToString())
                                
                                Dim newDatetime As New RestaurantEventDateTime With {
                                    .ProductID = newProductID,
                                    .Enabled = True,
                                    .FromDateTime = fromDt,
                                    .ToDateTime = toDt,
                                    .CreateDate = RestOpeningHourCreateDate,
                                    .CreatedBy = RestOpeningHourCreatedBy,
                                    .ModifyDate = RestOpeningHourModifyDate,
                                    .ModifiedBy = RestOpeningHourModifyBy}

                                db.RestaurantEventDateTimes.InsertOnSubmit(newDatetime)

                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception
                                    
                                End Try
                            Next
                        Loop
                    End If
                End Using
            End Using
            'End find th_restaurant_hrs
            con.Close()
        End Using
    End Sub
    
    Protected Function ConvertToDatetime(ByVal dateString As String, ByVal timeString As String) As DateTime
        Dim returnDatetime As DateTime

        Try
            'returnDatetime = DateTime.ParseExact(String.Format("{0} {1}", dateString, timeString), "yyyyMMddHH:mmtt", Nothing)
            returnDatetime = DateTime.Parse(String.Format("{0} {1}", dateString, timeString))
        Catch ex As Exception
            'set time string to "0000" if timeString format not correct 
            returnDatetime = DateTime.ParseExact(String.Format("{0} 12:00am", dateString), "yyyyMMddHHmm", Nothing)
        End Try

        Return returnDatetime
    End Function
    
    Protected Sub SaveNewRestImage(restID As String, newProductID As String, ByVal ProductImageWidth As Integer, ByVal ProductImageHeight As Integer, ByVal ProductThumbnailWidth As Integer, ByVal ProductThumbnailHeight As Integer)
        
        Dim db As New ProductDbDataContext()
        
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find article image
            Using articleImageCom As New MySqlCommand(String.Format("select * FROM thehouse.th_restaurant_image where rest_id={0} and status='Active'", restID), con)
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