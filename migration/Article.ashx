<%@ WebHandler Language="VB" Class="Article" %>

Imports System
Imports System.Web
Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class Article : Implements IHttpHandler
    Dim constr As String = ConfigurationManager.ConnectionStrings("TheHouseMySql").ConnectionString
    Dim mysqlUserDict As New Dictionary(Of String, String)
    
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim cdb As New CategoryDataClassesDataContext()
        Dim db As New ProductDbDataContext()
        
        Dim categoryDict As New Dictionary(Of Integer, String)
        'mysqlProductIDDict used for store mysql ProductID and mssql productID for ProductRelated
        Dim mysqlProductIDDict As New Dictionary(Of integer, integer)
        Dim articelFunctionID = ConfigurationManager.AppSettings("FunctionID_Article")
        Dim defaultLang = ConfigurationManager.AppSettings("DefaultLanguage")
        Dim productImageSize = (From s In db.SiteFunctions
                             Where s.FunctionID = articelFunctionID
                             Select s.ProductImageHeight, s.ProductImageWidth, s.ProductThumbnailHeight, s.ProductThumbnailWidth).FirstOrDefault()
        
        Dim articleCatgories = From c In cdb.view_Categories
                              Where c.FunctionID = articelFunctionID And c.Lang = defaultLang
                              Select c
                              
        Dim NextSortOrder As Integer = 1
             
        Try
            NextSortOrder = ((From p In db.Products
                           Where p.FunctionID = articelFunctionID
                           Select p.SortOrder).Max()) + 1
        Catch ex As Exception

        End Try
        
        
        For Each articleCategory In articleCatgories
            categoryDict.Add(articleCategory.CategoryID, articleCategory.CategoryName)
        Next
        
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
            
            'Find article
            Using ArticleCom As New MySqlCommand("select * FROM thehouse.th_article", con)
                Using ArticleRDR = ArticleCom.ExecuteReader()
                    If ArticleRDR.HasRows Then
                        Do While ArticleRDR.Read
                            Dim articleID As String = ArticleRDR.Item("article_id").ToString()
                            Dim productName As String = ArticleRDR.Item("article_title").ToString()
                            Dim ProductDesc As String = ArticleRDR.Item("article_content").ToString()
                            Dim ProductCategory As String = ArticleRDR.Item("article_type").ToString()
                            Dim ProductSellingStartDate As Date = Date.Parse(ArticleRDR.Item("posting_date").ToString())
                            Dim ProductNavigateUrl As String = ArticleRDR.Item("video_link").ToString()
                            Dim ProductViewCount As String = ArticleRDR.Item("view_cnt").ToString()
                            Dim ProductEnabled As Boolean = IIf(ArticleRDR.Item("status").ToString() = "Published", True, False)
                            Dim ProductCreateDate As DateTime = DateTime.Parse(ArticleRDR.Item("added_dt").ToString())
                            Dim ProductCreatedBy As String = mysqlUserDict.Item(ArticleRDR.Item("added_by").ToString())
                            Dim ProducttModifyDate As DateTime = DateTime.Parse(ArticleRDR.Item("updated_dt").ToString())
                            Dim ProductModifyBy As String = mysqlUserDict.Item(ArticleRDR.Item("updated_by").ToString())
                            Dim ProductSellingEndDate As Date = #1/1/2999#
                            Dim ProductType = "Article"
                            Dim newProductID As Integer = 0
                            
                            Dim newproduct As New Product With {
                                .FunctionID = articelFunctionID, _
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
                                .VideoUrl = ProductNavigateUrl, _
                                .Video3D = False, _
                                .Author = "", _
                                .CameraModel = ""}
                            
                            NextSortOrder += 1
                            
                            db.Products.InsertOnSubmit(newproduct)
                            
                            Try
                                db.SubmitChanges()
                            Catch ex As Exception

                            End Try
                            
                            newProductID = newproduct.ProductID
                            
                            mysqlProductIDDict.Add(articleID, newProductID)
                            
                            'Save ProductName
                            Dim newProductName As New ProductName With {
                                .ProductID = newProductID, _
                                .Lang = defaultLang, _
                                .ProductName = productName, _
                                .Description = ProductDesc, _
                                .fileUrl = "", _
                                .LongDescription="", 
                                .fileUrl2=""}
                            
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
                            
                            'Save CategoryProduct
                            Dim ProductCategoryID = categoryDict.Where(Function(c) c.Value = ProductCategory).Select(Function(c) c.Key).FirstOrDefault()
                            Dim newProductCategory As New CategoryProduct
                            newProductCategory.ProductID = newProductID
                            newProductCategory.CategoryID = ProductCategoryID
                            cdb.CategoryProducts.InsertOnSubmit(newProductCategory)
                            Try
                                cdb.SubmitChanges()
                            Catch ex As Exception
                            End Try
                            'End Save CategoryProduct
                            SaveProductComment(articleID, ProductType, newProductID)
                            SaveNewProductImage(articleID, newProductID, productImageSize.ProductImageWidth, productImageSize.ProductImageHeight, productImageSize.ProductThumbnailWidth, productImageSize.ProductThumbnailHeight)
                        Loop
                    End If
                End Using
            End Using
            'End find article
            
            con.Close()
        End Using
        
        'Save ProductRelated
        Using con As New MySqlConnection(constr)
            con.Open()
            Using articleRelatedCom As New MySqlCommand("select * FROM thehouse.th_article_related where status='Active' and RELATED_ARTICLE_ID !=0", con)
                Using articleRelatedRDR = articleRelatedCom.ExecuteReader()
                    If articleRelatedRDR.HasRows Then
                        Do While articleRelatedRDR.Read
                            Dim ProductID As String = mysqlProductIDDict.Item(articleRelatedRDR.Item("ARTICLE_ID").ToString())
                            Dim ProductRELATEDID As String = mysqlProductIDDict.Item(articleRelatedRDR.Item("RELATED_ARTICLE_ID").ToString())
                                            
                            Dim newProductRelated As New ProductRelated With {
                                .ProductID = ProductID, _
                                .RelatedID = ProductRELATEDID, _
                                .CreatedBy = mysqlUserDict.Item(articleRelatedRDR.Item("added_by").ToString()), _
                                .CreateDate = DateTime.Parse(articleRelatedRDR.Item("added_dt").ToString()), _
                                .ModifiedBy = mysqlUserDict.Item(articleRelatedRDR.Item("updated_by").ToString()), _
                                .ModifyDate = DateTime.Parse(articleRelatedRDR.Item("updated_dt").ToString()), _
                                .Enabled = True,
                                .RelatedType = "Article"}
                                  
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
 
    Public Sub UserDict()
        
    End Sub
    
    Protected Sub SaveProductComment(ByVal articleID As String, ByVal ProductType As String,ByVal newProductID As String)
        Dim db As New ProductDbDataContext()
        
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find article comment
            Using articleCommentCom As New MySqlCommand(String.Format("select * FROM thehouse.th_article_comment where status='Active' and article_id ={0}", articleID), con)
                Using articleCommentRDR = articleCommentCom.ExecuteReader()
                    If articleCommentRDR.HasRows Then
                        Do While articleCommentRDR.Read
                            Dim ProductComment As String = articleCommentRDR.Item("comment").ToString()
                            Dim ProductCommentUserID As String = articleCommentRDR.Item("user_id").ToString()
                            Dim ProductCommentDate As DateTime = DateTime.Parse(articleCommentRDR.Item("added_dt").ToString())
                                            
                            Dim newProductComment As New Comment With {
                                .CommentType = ProductType, _
                                .ReferenceID = newProductID, _
                                .CommentDate = ProductCommentDate, _
                                .UserID = mysqlUserDict.Item(ProductCommentUserID), _
                                .CommentDescription = ProductComment, _
                                .IsInspected = False, _
                                .IsDisable = False, _
                                .ParentID = 0, _
                                .MediaUrl = "", _
                                .MediaTitle = "", _
                                .MediaDesc = ""}
                                            
                            Try
                                db.SubmitChanges()
                            Catch ex As Exception

                            End Try
                        Loop
                    End If
                End Using
            End Using
            'End find article comment
            
            con.Close()
        End Using
    End Sub
    
    Protected Sub SaveNewProductImage(articleID As String, newProductID As String, ByVal ProductImageWidth As Integer, ByVal ProductImageHeight As Integer, ByVal ProductThumbnailWidth As Integer, ByVal ProductThumbnailHeight As Integer)
        Dim db As New ProductDbDataContext()
        
        Using con As New MySqlConnection(constr)
            con.Open()
            'Find article image
            Using articleImageCom As New MySqlCommand(String.Format("select * FROM thehouse.th_article_image where article_id ={0} and status='Active'", articleID), con)
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