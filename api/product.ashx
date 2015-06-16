<%@ WebHandler Language="VB" Class="product" %>

Imports System.Web
Imports System.Security.Cryptography
Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class product : Implements IHttpHandler
    
    Dim errorString As String = ""
    Dim IsWine As Boolean = False
    Dim IsRest As Boolean = False
    Dim IsEvent As Boolean = False
    Dim ProductPerPage As Integer = Convert.ToInt32(ConfigurationManager.AppSettings("thehouse_ProductPerPage"))
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim typeRequest As String = IIf(context.Request.QueryString("type") Is Nothing, "", context.Request.QueryString("type"))
        'get which product function ID from web config file by QueryString "type".
        '*****Notice: case sensitive******
        Dim FunctionIDRequest As Integer = IIf(typeRequest Is Nothing Or typeRequest.Length = 0, 0, ConfigurationManager.AppSettings(String.Format("FunctionID_{0}", typeRequest)))
        Dim ProductIDRequest As Integer = IIf(context.Request.QueryString("PID") Is Nothing Or Not IsNumeric(context.Request.QueryString("PID")), 0, Convert.ToInt32(context.Request.QueryString("PID")))
        Dim CommentParentIDRequest As Integer = IIf(context.Request.QueryString("CommentParent") Is Nothing Or Not IsNumeric(context.Request.QueryString("CommentParent")), 0, Convert.ToInt32(context.Request.QueryString("CommentParent")))
        Dim FunctionIDWine As Integer = ConfigurationManager.AppSettings("FunctionID_Wine")
        Dim FunctionIDRest As Integer = ConfigurationManager.AppSettings("FunctionID_Restaurant")
        Dim FunctionIDEvent As Integer = ConfigurationManager.AppSettings("FunctionID_Event")
        Dim wineTypeTagRequest As String = IIf(context.Request.QueryString("wineTypeTag") Is Nothing, "", context.Request.QueryString("wineTypeTag"))
        Dim wineRegionTagRequest As Integer = IIf(context.Request.QueryString("wineRegionTag") Is Nothing Or Not IsNumeric(context.Request.QueryString("wineRegionTag")), 0, context.Request.QueryString("wineRegionTag"))
        'set which page needed 
        Dim pageRequest As Integer = IIf(context.Request.QueryString("page") Is Nothing Or Not IsNumeric(context.Request.QueryString("page")), 1, Convert.ToInt32(context.Request.QueryString("page")))
        
        If FunctionIDRequest = FunctionIDWine Then
            IsWine = True
        ElseIf FunctionIDRequest = FunctionIDRest Then
            IsRest = True
        ElseIf FunctionIDRequest = FunctionIDEvent Then
            IsEvent = True
        End If

        
        'set lang found in query string requested and check if reuest is null or not
        Dim langRequest As String = ""
        
        'check query string "lang" have value or null
        Try
            langRequest = IIf(context.Request.QueryString("lang") Is Nothing Or context.Request.QueryString("lang").Length = 0 Or context.Request.QueryString("lang") = "", "en-us", context.Request.QueryString("lang").ToLower())
        Catch ex As Exception
            langRequest = "en-us"
        End Try
        
        'set result for output
        Dim ResultString As New JObject
        If Not FunctionIDRequest = 0 And ProductIDRequest = 0 Then
            ResultString.Add(New JProperty("Products", SearchAllProduct(FunctionIDRequest, wineTypeTagRequest, wineRegionTagRequest, pageRequest, langRequest)))
        ElseIf Not FunctionIDRequest = 0 And Not ProductIDRequest = 0 Then
            ResultString.Add(New JProperty("Product", SearchSinglelProduct(FunctionIDRequest, typeRequest, ProductIDRequest, langRequest)))
        Else
            ResultString.Add(New JProperty("Result", "Error"))
            ResultString.Add(New JProperty("Error", "Missing Informations"))
        End If
            
        If errorString.Length > 0 Then
            ResultString.Add(New JProperty("Result", "Error"))
            ResultString.Add(New JProperty("Error", errorString))
        Else
            context.Response.Write(ResultString.ToString())
        End If
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

#Region "SearchAllProduct"
    Protected Function SearchAllProduct(ByVal FnID As String, ByVal wineTypeTagRequest As String, ByVal wineRegionTagRequest As Integer, ByVal pageRequest As String, ByVal langRequest As String) As JArray
        Dim skip_number As Integer = 0
        If Not pageRequest = 1 Then
            skip_number = (pageRequest - 1) * ProductPerPage
        End If
        Dim db As New ProductDbDataContext()
        Dim theProducts As JArray
        If IsWine And wineTypeTagRequest.Length > 0 Or wineRegionTagRequest > 0 Then
            Dim wineTypeTag() = wineTypeTagRequest.Split(",")
            If wineTypeTag.Count > 0 Then
                Dim foundProductImageTags As IQueryable(Of view_ProductImageTag)
                If wineRegionTagRequest > 0 Then
                    'found all product records that tag contain in wineTypeTagRequest and wineRegionTagRequest
                    foundProductImageTags = From p In db.view_ProductImageTags
                                Where p.Lang.ToLower = langRequest And p.Enabled = True And wineTypeTag.Contains(p.TagID.ToString()) Or p.TagID = wineRegionTagRequest
                                Select p
                    'found wineRegionTagRequest record only
                    theProducts = New JArray(From p In foundProductImageTags
                    Where p.TagID = wineRegionTagRequest And p.Lang.ToLower = langRequest
                      Order By p.ModifyDate Descending
                      Skip skip_number Take ProductPerPage
                      Select New JObject( _
                                   New JProperty("ProductID", p.ProductID), _
                                   New JProperty("Price", FindProductPrice( p.ProductID, String.Format("{0:C2}", Convert.ToDecimal(p.SellingPrice)))), _
                                   New JProperty("Lang", p.Lang), _
                                   New JProperty("ProductUrl", FindProductPic(p.ProductID)), _
                                   New JProperty("ProductName", p.ProductName) _
                                   ))
                Else
                    theProducts = New JArray(From p In db.view_ProductImageTags
                    Where p.Lang.ToLower = langRequest And p.Enabled = True And wineTypeTag.Contains(p.TagID.ToString())
                      Order By p.ModifyDate Descending
                      Skip skip_number Take ProductPerPage
                      Select New JObject( _
                                   New JProperty("ProductID", p.ProductID), _
                                   New JProperty("Price", FindProductPrice(p.ProductID, String.Format("{0:C2}", Convert.ToDecimal(p.SellingPrice)))), _
                                   New JProperty("Lang", p.Lang), _
                                   New JProperty("ProductUrl", FindProductPic(p.ProductID)), _
                                   New JProperty("ProductName", p.ProductName) _
                                   ))
                End If
            End If
        Else
            theProducts = New JArray(From p In db.view_ProductImages
            Where p.FunctionID = FnID And p.Lang.ToLower = langRequest And p.Enabled = True
                  Order By p.SortOrder Ascending Order By p.ModifyDate Descending
                  Skip skip_number Take ProductPerPage
                  Select New JObject( _
                               New JProperty("ProductID", p.ProductID), _
                               New JProperty("Author", Membership.GetUser(p.ModifiedBy).UserName), _
                               New JProperty("UserPic", UserPic(p.ModifiedBy)), _
                               New JProperty("Price",FindProductPrice( p.ProductID, String.Format("{0:C2}", Convert.ToDecimal(p.SellingPrice)))), _
                               New JProperty("Lang", p.Lang), _
                               New JProperty("ProductUrl", FindProductPic(p.ProductID)), _
                               New JProperty("ProductName", p.ProductName), _
                               New JProperty("SortOrder", p.SortOrder) _
                               ))
        End If
        Return theProducts
    End Function
#End Region
    
#Region "SearchSinglelProduct"
    Protected Function SearchSinglelProduct(ByVal FnID As String, ByVal productType As String, ByVal PID As String, ByVal langRequest As String) As JArray
        Dim db As New ProductDbDataContext()
        'add view for product
        AddViewCount(productType, PID)
        Dim returnProduct As JArray
        If IsWine Then
            returnProduct = New JArray(From p In db.view_productDetails
                  Where p.FunctionID = FnID And p.ProductID = PID And p.Lang.ToLower = langRequest And p.Enabled = True
                  Order By p.SortOrder Ascending Order By p.ModifyDate Descending
                  Select New JObject( _
                               New JProperty("ProductID", p.ProductID), _
                               New JProperty("Lang", p.Lang), _
                               New JProperty("ProductUrl", FindProductPic(p.ProductID)), _
                               New JProperty("ProductName", p.ProductName), _
                               New JProperty("Description", p.Description), _
                               New JProperty("Price", FindProductPrice( p.ProductID, String.Format("{0:C2}", Convert.ToDecimal(p.SellingPrice)))), _
                               New JProperty("Vintage", p.Vintage), _
                               New JProperty("Grape", p.Grape), _
                               New JProperty("Alcohol", p.Alcohol), _
                               New JProperty("Region", p.Region), _
                               New JProperty("Body", p.Body), _
                               New JProperty("Volume", p.Volume), _
                               New JProperty("Winery", p.Winery), _
                               New JProperty("Website", p.Website), _
                               New JProperty("InternationalRatings", p.InternationalRatings) _
                               ))
        ElseIf IsRest Then
            returnProduct = New JArray(From r In db.view_RestaurantDetails
                   Where r.Lang.ToLower = langRequest And r.Enabled = True And r.ProductID = PID
                     Order By r.ModifyDate Descending
                     Select New JObject( _
                                  New JProperty("ProductID", r.ProductID), _
                                  New JProperty("AvgExpense", String.Format("{0:C2}", Convert.ToDecimal(r.AvgExpense))), _
                                  New JProperty("Lang", r.Lang), _
                                  New JProperty("ProductUrl", FindProductPic(r.ProductID)), _
                                  New JProperty("ProductName", r.ProductName), _
                                  New JProperty("Description", r.Description), _
                                  New JProperty("Address", r.Address), _
                                  New JProperty("GoogleMap", r.GoogleMap), _
                                  New JProperty("PhoneNumber", r.PhoneNumber), _
                                  New JProperty("Email", r.Email), _
                                  New JProperty("Cuisine", r.Cuisine), _
                                  New JProperty("RestMenu", r.RestMenu), _
                                  New JProperty("SeatsNumber", r.SeatsNumber), _
                                  New JProperty("WineStock", r.WineStock), _
                                  New JProperty("PayMethod", r.PayMethod), _
                                  New JProperty("OpeningHour", RestOpeningHour(r.ProductID))
                                  ))
        Else
            returnProduct = New JArray(From p In db.view_ProductImages
                  Where p.FunctionID = FnID And p.ProductID = PID And p.Lang.ToLower = langRequest And p.Enabled = True
                  Order By p.SortOrder Ascending Order By p.ModifyDate Descending
                  Select New JObject( _
                               New JProperty("ProductID", p.ProductID), _
                               New JProperty("Author", Membership.GetUser(p.ModifiedBy).UserName), _
                               New JProperty("UserPic", UserPic(p.ModifiedBy)), _
                               New JProperty("ProductLikeCount", FoundLikeCount(productType, PID)), _
                               New JProperty("Lang", p.Lang), _
                               New JProperty("ProductUrl", FindProductPic(p.ProductID)), _
                               New JProperty("ProductName", p.ProductName), _
                               New JProperty("Description", p.Description), _
                               New JProperty("Comments", SearchAllProductComments(p.ProductID, productType, 0)) _
                               ))
        End If
        
        Return returnProduct
    End Function
    
    Protected Function RestOpeningHour(ByVal PID As String) As JArray
        Dim db As New ProductDbDataContext()
        
        'Dim returnOpeningHour = New JArray(From p In db.RestaurantEventDateTimes
        '                  Where p.ProductID = PID And p.Enabled = True
        '                  Order By p.FromDateTime Ascending
        '                  Select New JObject( _
        '                               New JProperty("FromDay", Weekday(p.FromDateTime))
        '                               ))
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US", False)
        
        Dim returnOpeningHour = New JArray(From p In db.RestaurantEventDateTimes
                  Where p.ProductID = PID And p.Enabled = True
                  Order By p.FromDateTime Ascending
                  Select New JObject( _
                               New JProperty("FromDay", WeekdayName(Weekday(p.FromDateTime))), _
                               New JProperty("FromTime", DateTime.Parse(p.FromDateTime).ToString("HH:mm tt")), _
                               New JProperty("ToDay", WeekdayName(Weekday(p.ToDateTime))), _
                               New JProperty("ToTime", DateTime.Parse(p.ToDateTime).ToString("HH:mm tt")) _
                               ))
        
        Return returnOpeningHour
    End Function
    
    Protected Function FoundLikeCount(ByVal productType As String, ByVal PID As String) As integer
        Dim db As New ProductDbDataContext()
        Dim LikeCount As Integer = 0
        Dim productLikeCount = From v In db.view_LikeTableCounts
                               Where v.LikeType = productType And v.ReferenceID = PID
                               Select v.Count
        If productLikeCount.Count = 1 Then
            LikeCount = productLikeCount.FirstOrDefault()
        End If
        Return LikeCount
    End Function
    
    Protected Sub AddViewCount(ByVal productType As String, ByVal PID As String)
        Dim db As New ProductDbDataContext()
        Dim productViewCount = From v In db.ViewCounts
                               Where v.Type = productType And v.ReferenceID = PID
                               Select v
        If productViewCount.Count = 0 Then
            Dim newViewCount As New ViewCount
            newViewCount.Type = productType
            newViewCount.ReferenceID = PID
            newViewCount.ViewCount = 1
            newViewCount.LastView = Date.Now
            Try
                db.ViewCounts.InsertOnSubmit(newViewCount)
                db.SubmitChanges()
            Catch ex As Exception
                errorString = ex.Message
            End Try
        Else
            productViewCount.First().ViewCount += 1
            productViewCount.First().LastView = Date.Now
            Try
                db.SubmitChanges()
            Catch ex As Exception
                errorString = ex.Message
            End Try
        End If
        
    End Sub
    
    Protected Function FindProductPrice(ByVal pID As String, ByVal ProductSellingPrice As Decimal) As Decimal
        Dim db As New ProductDbDataContext()
        If IsRest Then
            ProductSellingPrice = Convert.ToDecimal((From p In db.RestaurantDetails
                                Where p.ProductID = pID
                                Where p.AvgExpense).FirstOrDefault())
        ElseIf IsEvent Then
            ProductSellingPrice = Convert.ToDecimal((From p In db.EventDetails
                                Where p.ProductID = pID
                                Where p.EventPrice).FirstOrDefault())
        End If
        Return ProductSellingPrice
    End Function
    
#End Region
     
#Region "SearchAllProductComments"
    Protected Function SearchAllProductComments(ByVal pID As Integer, ByVal commentType As String, ByVal cID As Integer) As JArray
        Dim db As New ProductDbDataContext()
        Dim theComments = New JArray(From c In db.view_Comments
              Where c.CommentType = commentType And c.ReferenceID = pID And c.IsDisable = False And c.ParentID = cID
              Order By c.LikeCount, c.CommentDate Descending
              Select New JObject( _
                           New JProperty("Name", c.Name), _
                           New JProperty("UserPic", UserPic(c.UserID)), _
                           New JProperty("UserID", c.UserID), _
                           New JProperty("CommentID", c.CommentID), _
                           New JProperty("ReferenceID", c.ReferenceID), _
                           New JProperty("CommentDate", c.CommentDate), _
                           New JProperty("CommentDescription", c.CommentDescription), _
                           New JProperty("LikeCount", c.LikeCount), _
                           New JProperty("ChildComments", IIf(HaveChildComment(c.ReferenceID, commentType, c.CommentID), SearchAllProductComments(c.ReferenceID, commentType, c.CommentID), New JArray)) _
                           ))
        Return theComments
    End Function
    
    Protected Function HaveChildComment(ByVal pID As Integer, ByVal commentType As String, ByVal cID As Integer) As Boolean
        Dim db As New ProductDbDataContext()
        
        Dim Comment = From c In db.view_Comments
              Where c.CommentType = commentType And c.ReferenceID = pID And c.IsDisable = False And c.ParentID = cID
              Select c
        
        If Comment.Count > 0 Then
            Return True
        Else
            Return False
        End If
        
    End Function
#End Region
    
#Region "UserPic"
    Protected Function UserPic(ByVal UserName As String) As String
        Dim foundUserPic As String = ""
        Dim r As MemberDetailDataSet.MemberDetailRow
        Dim t As New MemberDetailDataSet.MemberDetailDataTable
        t = (New MemberDetailDataSetTableAdapters.MemberDetailTableAdapter).GetDataByUserID(UserName)
        If t.Rows.Count > 0 Then
            r = t.Rows(0)
            With r
                If Not .IsUserPicUrlNull Then
                    foundUserPic = .UserPicUrl
                End If
            End With
        End If
        Return foundUserPic
    End Function
#End Region
    
#Region "ProductPic"
    Protected Function FindProductPic(ByVal pID As String) As String
        Dim db As New ProductDbDataContext()
        
        Dim findPic = (From i In db.ProductImages
                    Where i.ProductID = pID And i.SortOrder = 1
                    Select i.Url).FirstOrDefault()
        Return findPic
    End Function
#End Region
    
        

    
End Class