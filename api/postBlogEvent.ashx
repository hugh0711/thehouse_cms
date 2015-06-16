<%@ WebHandler Language="VB" Class="postBlogEvent" %>

Imports System
Imports System.Web
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Web.Script.Serialization

Public Class postBlogEvent : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        'set result for output
        Dim ResultString As New JObject
        
        context.Response.ContentType = "application/json"
        
        Dim data = context.Request
        Dim sr As StreamReader = New StreamReader(data.InputStream)
        Dim stream = sr.ReadToEnd()
        Dim javaScriptSerializer = New JavaScriptSerializer()
        Dim PostBlogEvent = javaScriptSerializer.Deserialize(Of PostBlogEventClass)(stream)
        
        If PostBlogEvent IsNot Nothing Then
            Dim db As New ProductDbDataContext()
            
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
            
            End If
        End If
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class