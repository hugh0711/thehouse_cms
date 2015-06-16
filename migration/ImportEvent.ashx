<%@ WebHandler Language="VB" Class="ImportEvent" %>

Imports System
Imports System.Web
Imports MySql.Data.MySqlClient

Public Class ImportEvent : Implements IHttpHandler
    Dim constr As String = ConfigurationManager.ConnectionStrings("TheHouseMySql").ConnectionString
    Dim mysqlUserDict As New Dictionary(Of String, String)
    Dim TagIDProductTypeDict As New Dictionary(Of Integer, String)
    Dim PaymentMethodDict As New Dictionary(Of Integer, String)
    Dim OrderStatusDict As New Dictionary(Of string, String)
    Dim defaultLang As String = ConfigurationManager.AppSettings("DefaultLanguage")
    
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim bootlePurchaseTagName As String = "BOTTLE_PURCHASE"
        Dim wineOnlyTagName As String = "WINE_ONLY"
        Dim wineMealTagName As String = "WINE_AND_MEAL"
        Dim eventFunctionID = ConfigurationManager.AppSettings("FunctionID_Event")
        'mysqlProductIDDict used for store mysql ProductID and mssql productID for ProductRelated
        Dim mysqlProductIDDict As New Dictionary(Of Integer, Integer)
        Dim db As New ProductDbDataContext()
        
        Dim tagProductTypes = From t In db.view_Tags
                              Where t.FunctionID = eventFunctionID And t.Lang = defaultLang
                              Select t.TagID, t.TagName
                              
        For Each tagProductType In tagProductTypes
            TagIDProductTypeDict.Add(tagProductType.TagID, tagProductType.TagName.ToLower())
        Next
        
        Dim productImageSize = (From s In db.SiteFunctions
                             Where s.FunctionID = eventFunctionID
                             Select s.ProductImageHeight, s.ProductImageWidth, s.ProductThumbnailHeight, s.ProductThumbnailWidth).FirstOrDefault()
        
        
        Dim NextSortOrder As Integer = 1
             
        Try
            NextSortOrder = ((From p In db.Products
                           Where p.FunctionID = eventFunctionID
                           Select p.SortOrder).Max()) + 1
        Catch ex As Exception

        End Try
        
        Dim foundPaymentMethods = From p In db.PaymentMethods
                                Select p.PaymentMethodId,p.PaymentMethodName
                                
        For Each thePaymentMethod In foundPaymentMethods
            PaymentMethodDict.Add(thePaymentMethod.PaymentMethodId, thePaymentMethod.PaymentMethodName)
        Next
        
        OrderStatusDict.Add("ORDER_COMPLETED","Completed" )
        OrderStatusDict.Add("ORDER_CANCEL", "Cancelled")
        OrderStatusDict.Add("PROCESSING", "Processing payment")
        
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
            'Find Event
            Using EventCom As New MySqlCommand("select * FROM thehouse.th_event", con)
                Using EventRDR = EventCom.ExecuteReader()
                    If EventRDR.HasRows Then
                        Do While EventRDR.Read
                            db = New ProductDbDataContext()
                            
                            Dim eventID As String = EventRDR.Item("event_id").ToString()
                            Dim JoinedUserID As String = EventRDR.Item("user_id").ToString()
                            Dim productName As String = EventRDR.Item("event_title").ToString()
                            Dim ProductDesc As String = EventRDR.Item("event_content").ToString()
                            Dim IsBootlePurchase As Boolean = IIf(EventRDR.Item(bootlePurchaseTagName).ToString() = "Y", True, False)
                            Dim IsWineOnly As Boolean = IIf(EventRDR.Item(wineOnlyTagName).ToString() = "Y", True, False)
                            Dim IswineMeal As Boolean = IIf(EventRDR.Item(wineMealTagName).ToString() = "Y", True, False)
                            
                            Dim eventDetailCurrency As String = EventRDR.Item("currency_code").ToString()
                            Dim eventDetailPrice As Decimal = Convert.ToDecimal(EventRDR.Item("event_price").ToString())
                            Dim eventDetailGlassPrice1 As Decimal = Convert.ToDecimal(EventRDR.Item("glass_price_1").ToString())
                            Dim eventDetailVolume1 As Decimal = Convert.ToDecimal(EventRDR.Item("glass_volume_1").ToString())
                            Dim eventDetailGlassPrice2 As Decimal = Convert.ToDecimal(EventRDR.Item("glass_price_2").ToString())
                            Dim eventDetailVolume2 As Decimal = Convert.ToDecimal(EventRDR.Item("glass_volume_2").ToString())
                            Dim eventDetailGlassPrice3 As Decimal = Convert.ToDecimal(EventRDR.Item("glass_price_3").ToString())
                            Dim eventDetailVolume3 As Decimal = Convert.ToDecimal(EventRDR.Item("glass_volume_3").ToString())
                            Dim eventDetailMaxAttend As Decimal = CInt(EventRDR.Item("max_attend").ToString())
                            Dim EventMenuUrl As String = EventRDR.Item("event_menu").ToString()
                            Dim eventDetailFilenname As String = ""
                            If EventMenuUrl.Length > 0 Then
                                eventDetailFilenname = SaveProductImage.SaveProductImage(String.Format("http://thehouse.com.hk/{0}", EventMenuUrl), productImageSize.ProductImageWidth, productImageSize.ProductImageHeight, productImageSize.ProductThumbnailWidth, productImageSize.ProductThumbnailHeight)
                            End If
                            
                            Dim eventFromDate As String = Date.Parse(EventRDR.Item("event_start_dt").ToString()).ToString("yyyy/MM/dd")
                            Dim eventFromTime As String = EventRDR.Item("event_start_time").ToString()
                            Dim eventFromDateTime As String = DateTime.Parse(String.Format("{0} {1}", eventFromDate, eventFromTime))
                            Dim eventToDate As String = Date.Parse(EventRDR.Item("event_end_dt").ToString()).ToString("yyyy/MM/dd")
                            Dim eventToTime As String = EventRDR.Item("event_end_time").ToString()
                            Dim eventToDateTime As String = DateTime.Parse(String.Format("{0} {1}", eventToDate, eventToTime))
                            
                            Dim RestList As String() = EventRDR.Item("rest_id_list").ToString().Split(";")
                            
                            Dim ProductVideoUrl As String = EventRDR.Item("video_link").ToString()
                            Dim ProductViewCount As String = EventRDR.Item("view_cnt").ToString()
                            Dim ProductEnabled As Boolean = IIf(EventRDR.Item("status").ToString() = "Published", True, False)
                            Dim ProductSellingStartDate As Date = Date.Parse(EventRDR.Item("posting_date").ToString())
                            Dim ProductCreateDate As DateTime = DateTime.Parse(EventRDR.Item("added_dt").ToString())
                            Dim ProductCreatedBy As String = mysqlUserDict.Item(EventRDR.Item("added_by").ToString())
                            Dim ProducttModifyDate As DateTime = DateTime.Parse(EventRDR.Item("updated_dt").ToString())
                            Dim ProductModifyBy As String = mysqlUserDict.Item(EventRDR.Item("updated_by").ToString())
                            Dim ProductSellingEndDate As Date = Date.Parse(eventFromDate)
                            Dim ProductType = "Event"
                            Dim newProductID As Integer = 0
                            
                            
                            Dim newproduct As New Product With {
                               .FunctionID = eventFunctionID, _
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
                                context.Response.Write(String.Format("<br />error:{0}", eventID))
                                Exit Sub
                            Else
                                mysqlProductIDDict.Add(eventID, newProductID)
                            
                                'Save EventDetail
                                Dim newEventDetail As New EventDetail With {
                                    .ProductID = newProductID, _
                                    .CurrencyCode = eventDetailCurrency, _
                                    .EventPrice = eventDetailPrice, _
                                    .GlassPrice1 = eventDetailGlassPrice1, _
                                    .GlassVolume1 = eventDetailVolume1, _
                                    .GlassPrice2 = eventDetailGlassPrice2,
                                    .GlassVolume2 = eventDetailVolume2, _
                                    .GlassPrice3 = eventDetailGlassPrice3, _
                                    .GlassVolume3 = eventDetailVolume3, _
                                    .MaxAttend = eventDetailMaxAttend, _
                                    .MenuUrl = eventDetailFilenname}
                            
                                db.EventDetails.InsertOnSubmit(newEventDetail)
                            
                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception

                                End Try
                                'End Save EventDetail
                            
                            
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
                                
                                'Save ProductTag(bootlePurchaseTagName)
                                If IsBootlePurchase Then
                                    Dim foundBootlePurchaseagID = TagIDProductTypeDict.Where(Function(c) c.Value = bootlePurchaseTagName.ToLower()).Select(Function(c) c.Key).FirstOrDefault()
                                    Dim newBootlePurchase As New ProductTag With {
                                        .ProductID = newProductID, _
                                        .TagID = foundBootlePurchaseagID}
                                    db.ProductTags.InsertOnSubmit(newBootlePurchase)
                                    Try
                                        db.SubmitChanges()
                                    Catch ex As Exception
                                    End Try
                                End If
                                'End Save ProductTag(bootlePurchaseTagName)
                                
                                'Save ProductTag(wineMealTagName)
                                If IswineMeal Then
                                    Dim foundwineMealTagID = TagIDProductTypeDict.Where(Function(c) c.Value = wineMealTagName.ToLower()).Select(Function(c) c.Key).FirstOrDefault()
                                    Dim newBootlePurchase As New ProductTag With {
                                        .ProductID = newProductID, _
                                        .TagID = foundwineMealTagID}
                                    db.ProductTags.InsertOnSubmit(newBootlePurchase)
                                    Try
                                        db.SubmitChanges()
                                    Catch ex As Exception
                                    End Try
                                End If
                                'End Save ProductTag(wineMealTagName)
                                
                                'Save ProductTag(wineOnlyTagName)
                                If IsWineOnly Then
                                    Dim foundWineOnlyTagID = TagIDProductTypeDict.Where(Function(c) c.Value = wineOnlyTagName.ToLower()).Select(Function(c) c.Key).FirstOrDefault()
                                    Dim newBootlePurchase As New ProductTag With {
                                        .ProductID = newProductID, _
                                        .TagID = foundWineOnlyTagID}
                                    db.ProductTags.InsertOnSubmit(newBootlePurchase)
                                    Try
                                        db.SubmitChanges()
                                    Catch ex As Exception
                                    End Try
                                End If
                                'End Save ProductTag(wineOnlyTagName)
                            
                                'Save RestaurantEventDateTime
                                Dim newDatetime As New RestaurantEventDateTime With {
                                    .ProductID = newProductID,
                                    .Enabled = True,
                                    .FromDateTime = eventFromDateTime,
                                    .ToDateTime = eventToDateTime,
                                    .CreateDate = ProductCreateDate,
                                    .CreatedBy = ProductCreatedBy,
                                    .ModifyDate = ProducttModifyDate,
                                    .ModifiedBy = ProductModifyBy}
                                db.RestaurantEventDateTimes.InsertOnSubmit(newDatetime)
                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception
                                End Try
                                'Save RestaurantEventDateTime
                                
                                'Save ProductRelated(Rest)
                                SaveRestProductRelated(RestList, newProductID, ProductCreateDate, ProductCreatedBy, ProducttModifyDate, ProductModifyBy)
                                'End Save ProductRelated(Rest)
                            
                                SaveNewEventImage(eventID, newProductID, productImageSize.ProductImageWidth, productImageSize.ProductImageHeight, productImageSize.ProductThumbnailWidth, productImageSize.ProductThumbnailHeight)
                            
                                SaveOrder(eventID, newProductID, productName)
                            End If
                        Loop
                    End If
                End Using
            End Using
            'End Find Event
            con.Close()
        End Using
        
        'Save ProductRelated Wine
        Using con As New MySqlConnection(constr)
            con.Open()
            Using wineRelatedCom As New MySqlCommand("select * FROM thehouse.th_event_related where related_event_id !=0", con)
                Using wineRelatedRDR = wineRelatedCom.ExecuteReader()
                    If wineRelatedRDR.HasRows Then
                        Do While wineRelatedRDR.Read
                            Dim ProductID As String = mysqlProductIDDict.Item(wineRelatedRDR.Item("event_id").ToString())
                            Dim ProductRELATEDID As String = mysqlProductIDDict.Item(wineRelatedRDR.Item("related_event_id").ToString())
                            Dim newProductRelated As New ProductRelated With {
                                .ProductID = ProductID, _
                                .RelatedID = ProductRELATEDID, _
                                .CreatedBy = mysqlUserDict.Item(wineRelatedRDR.Item("added_by").ToString()), _
                                .CreateDate = DateTime.Parse(wineRelatedRDR.Item("added_dt").ToString()), _
                                .ModifiedBy = mysqlUserDict.Item(wineRelatedRDR.Item("updated_by").ToString()), _
                                .ModifyDate = DateTime.Parse(wineRelatedRDR.Item("updated_dt").ToString()), _
                                .Enabled = True,
                                .RelatedType = "Event"}
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
        'End ProductRelated Wine
        
        context.Response.Write("End")
    End Sub
    
    Protected Sub SaveRestProductRelated(ByVal RestList As String(), ByVal newProductID As String, ProductCreateDate As date, ProductCreatedBy As String, ProducttModifyDate As date, ProductModifyBy As String)
        Dim db As New ProductDbDataContext()
        For Each restID In RestList
            If Not restID = "" Then
                Using con As New MySqlConnection(constr)
                    con.Open()
                    'Find event image
                    Using restCom As New MySqlCommand(String.Format("select * FROM thehouse.th_restaurant where rest_id ={0}", restID), con)
                        Using restRDR = restCom.ExecuteReader()
                            If restRDR.HasRows Then
                                Do While restRDR.Read
                                    Dim restName As String = restRDR.Item("rest_name").ToString()
                                    Dim restAddr As String = restRDR.Item("addr").ToString()
                                    Dim restPhone As String = restRDR.Item("phone_num").ToString()
                                
                                    Dim relatedRestID = (From p In db.view_RestaurantDetails
                                                      Where p.ProductName = restName And p.Address = restAddr And p.PhoneNumber = restPhone And p.Lang = defaultLang
                                                      Select p.ProductID).FirstOrDefault()
                                    If Not relatedRestID = 0 Then
                                        Dim newProductRelated As New ProductRelated With {
                                            .ProductID = newProductID, _
                                            .RelatedID = relatedRestID, _
                                            .CreatedBy = ProductCreatedBy, _
                                            .CreateDate = ProductCreateDate, _
                                            .ModifiedBy = ProductModifyBy, _
                                            .ModifyDate = ProducttModifyDate, _
                                            .Enabled = True,
                                            .RelatedType = "Restaurant"}
                                        db.ProductRelateds.InsertOnSubmit(newProductRelated)
                                        Try
                                            db.SubmitChanges()
                                        Catch ex As Exception
                                        End Try
                                    End If
                                Loop
                            End If
                        End Using
                    End Using
                    'End find event image
                    con.Close()
                End Using
            End If
            
        Next
    End Sub
    
    Protected Sub SaveNewEventImage(eventID As String, newProductID As String, ByVal ProductImageWidth As Integer, ByVal ProductImageHeight As Integer, ByVal ProductThumbnailWidth As Integer, ByVal ProductThumbnailHeight As Integer)
        Dim db As New ProductDbDataContext()
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find event image
            Using eventImageCom As New MySqlCommand(String.Format("select * FROM thehouse.th_event_image where event_id ={0} and status='Active'", eventID), con)
                Using eventImageRDR = eventImageCom.ExecuteReader()
                    If eventImageRDR.HasRows Then
                        Do While eventImageRDR.Read
                            Dim ProductUrl As String = String.Format("http://thehouse.com.hk/{0}", eventImageRDR.Item("image_loc").ToString())
                            Dim ProductSort As Integer = 0
                            'run if picture format is jpg or png
                            If IO.Path.GetExtension(ProductUrl).ToString().Contains("jpg") Or IO.Path.GetExtension(ProductUrl).ToString().Contains("png") Then
                                Dim newProductFilenname = SaveProductImage.SaveProductImage(ProductUrl, ProductImageWidth, ProductImageHeight, ProductThumbnailWidth, ProductThumbnailHeight)
                                Dim primaryFlag As Boolean = IIf(eventImageRDR.Item("primary_flag").ToString() = "Y", True, False)
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
            'End find event image
            con.Close()
        End Using
    End Sub
    
    Protected Sub SaveOrder(eventID As String, newProductID As String, productName As String)
        Dim db As New ProductDbDataContext()
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find event order
            Using bookingCom As New MySqlCommand(String.Format("select * FROM thehouse.th_booking where event_id ={0}", eventID), con)
                Using eventOrderRDR = bookingCom.ExecuteReader()
                    If eventOrderRDR.HasRows Then
                        Do While eventOrderRDR.Read
                            Dim BookingID As string = eventOrderRDR.Item("booking_id").ToString()
                            Dim OrderDate As Date = Date.Parse(eventOrderRDR.Item("booking_dt").ToString())
                            Dim OrderPaidDate As Date = DateTime.Parse("1753-01-01 00:00:00.000")
                            Dim OrderUpdaedDate As DateTime = DateTime.Parse(eventOrderRDR.Item("updated_dt").ToString())
                            Dim OrderQuantity As Integer = CInt(eventOrderRDR.Item("quantity").ToString())
                            Dim OrderTotalPrice As Decimal = Convert.ToDecimal(eventOrderRDR.Item("total_price").ToString())
                            Dim OrderUserID As String = eventOrderRDR.Item("user_id").ToString()
                            Dim username = mysqlUserDict.Item(OrderUserID)
                            Dim OrderPaymentMethod As String = eventOrderRDR.Item("payment").ToString()
                            Dim OrderPaymentStatus As String = eventOrderRDR.Item("status").ToString()
                            Dim foundPaymentMethod As Integer = PaymentMethodDict.Where(Function(c) c.Value = OrderPaymentMethod).Select(Function(c) c.Key).FirstOrDefault()
                            Dim foundOrderPaymentStatus As String = OrderStatusDict.Where(Function(c) c.Value = OrderPaymentStatus).Select(Function(c) c.Key).FirstOrDefault()
                            Dim EventStatus As String = ""
                            If foundOrderPaymentStatus = "ORDER_COMPLETED" Then
                                OrderPaidDate = OrderUpdaedDate
                                EventStatus = "Paid"
                            ElseIf foundOrderPaymentStatus = "ORDER_CANCEL" Then
                                EventStatus = "Cancelled"
                            ElseIf foundOrderPaymentStatus = "PROCESSING" Then
                                EventStatus = "WaitForPayment"
                            End If
                            Dim nextOrderNo As New SeedClass
                            Dim orderNo As String = ""
                            orderNo = nextOrderNo.NextOrderNo
                            Dim newOrderItem As New OrderItem With {
                                .OrderNumber = orderNo,
                                .OrderProductId = newProductID,
                                .OrderProductCode = "",
                                .OrderProductName = productName,
                                .OrderQuantity = OrderQuantity,
                                .OrderPrice = OrderTotalPrice,
                                .OrderShippingFee = 0}
                            db.OrderItems.InsertOnSubmit(newOrderItem)
                            Try
                                db.SubmitChanges()
                            Catch ex As Exception
                            End Try
                            Dim UserInfo = (From m In db.MemberDetails
                                          Where m.UserID = username
                                          Select m).FirstOrDefault()
                            Dim orderCustomerName As String = ""
                            Dim orderCantactNo As String = ""
                            Dim orderEmail As String = ""
                            
                            If UserInfo Is Nothing Then
                                orderEmail = FindCusrtomerEmail(BookingID)
                            Else
                                orderCustomerName = UserInfo.Name
                                orderCantactNo = UserInfo.ContactNo
                                orderEmail = UserInfo.Email
                            End If
                            
                            Dim newOrderForm As New OrderForm With {
                                .OrderNumber = orderNo,
                                .OrderDate = OrderDate,
                                .CustomerName = orderCustomerName,
                                .ContactPhone = orderCantactNo,
                                .DeliveryAddress = "",
                                .Country = "",
                                .Email = orderEmail,
                                .Remark = "",
                                .PaymentMethod = foundPaymentMethod,
                                .DeliveryMethod = 0,
                                .OrderStatus = foundOrderPaymentStatus,
                                .TotalAmount = OrderTotalPrice,
                                .TransactionRefNo = "",
                                .PaidDate = OrderPaidDate,
                                .PayPalStatus = "",
                                .Lang = defaultLang,
                                .LastUpdateUser = "",
                                .LastUpdateTime = OrderUpdaedDate
                                }
                            db.OrderForms.InsertOnSubmit(newOrderForm)
                            Try
                                db.SubmitChanges()
                            Catch ex As Exception
                            End Try
                            
                            Dim newOrderID As Integer = newOrderForm.OrderID
                            If Not newOrderID = 0 Then
                                
                                Dim newEventUser As New EventUser With {
                                    .EventID = newProductID,
                                    .OrderID = newOrderID,
                                    .UserID = username,
                                    .Email = orderEmail,
                                    .InviteBy = "",
                                    .EventStatus = EventStatus,
                                    .UUID = Guid.Empty}
                                db.EventUsers.InsertOnSubmit(newEventUser)
                                Try
                                    db.SubmitChanges()
                                Catch ex As Exception
                                End Try
                                
                                Dim newEventUserID As Integer = newEventUser.ID
                                
                                If Not newEventUserID = 0 Then
                                    SaveEventUser(eventID, OrderUserID, newEventUserID)
                                End If
                                
                            End If
                        Loop
                    End If
                End Using
            End Using
            'End find event image
            con.Close()
        End Using
    End Sub
    
    Public function FindCusrtomerEmail(BookingID As string) as string
        Dim db As New ProductDbDataContext()
        Dim returnEmail As String = ""
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find event invitation
            Using bookingregCom As New MySqlCommand(String.Format("select * FROM thehouse.th_booking_reg where booking_id ={0}", BookingID), con)
                Using bookingRegRDR = bookingregCom.ExecuteReader()
                    If bookingRegRDR.HasRows Then
                        Do While bookingRegRDR.Read
                            returnEmail = bookingRegRDR.Item("email_addr").ToString()
                        Loop
                    End If
                End Using
            End Using
            'End find event invitation
            con.Close()
        End Using
        Return returnEmail
    End Function
    
    Public Sub SaveEventUser(eventID As String, userID As String, newEventUserID As String)
        Dim db As New ProductDbDataContext()
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find event invitation
            Using invitationCom As New MySqlCommand(String.Format("select * FROM thehouse.th_event_invitation where event_id ={0} and user_id ={1}", eventID, userID), con)
                Using eventUserRDR = invitationCom.ExecuteReader()
                    If eventUserRDR.HasRows Then
                        Do While eventUserRDR.Read
                            Dim EventUserUUID As String = eventUserRDR.Item("uuid").ToString()
                            Dim EventUserGuid As Guid = Guid.Parse(EventUserUUID)
                            Dim InviteUserID As String = eventUserRDR.Item("invite_by").ToString()
                            Dim InviteUsername = mysqlUserDict.Item(InviteUserID)
                            Dim CreatedBy = mysqlUserDict.Item(eventUserRDR.Item("added_by").ToString())
                            Dim CreateDate = DateTime.Parse(eventUserRDR.Item("added_dt").ToString())
                            Dim ModifiedBy = mysqlUserDict.Item(eventUserRDR.Item("updated_by").ToString())
                            Dim ModifyDate = DateTime.Parse(eventUserRDR.Item("updated_dt").ToString())
                            Dim foundEventUser = (From e In db.EventUsers
                                                Where e.ID = newEventUserID
                                                Select e).FirstOrDefault()
                            foundEventUser.UUID = EventUserGuid
                            foundEventUser.InviteBy = InviteUsername
                            foundEventUser.CreatedBy = CreatedBy
                            foundEventUser.CreateDate = CreateDate
                            foundEventUser.UpdateDate = ModifyDate
                            foundEventUser.UpdatedBy = ModifiedBy
                            Try
                                db.SubmitChanges()
                            Catch ex As Exception
                            End Try
                        Loop
                    End If
                End Using
            End Using
            'End find event invitation
            con.Close()
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class