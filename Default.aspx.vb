Imports System.Linq
Imports Newtonsoft.Json.Linq

Partial Class _Default
    Inherits System.Web.UI.Page

    Dim SQLData As JArray

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CheckTag()
        End If

    End Sub

    Protected Sub btn_all_Click(sender As Object, e As EventArgs) Handles btn_all.Click
        Session("tag") = "all"
        CheckTag()
    End Sub

    Protected Sub btn_hk_tag_Click(sender As Object, e As EventArgs) Handles btn_hk_tag.Click
        Session("tag") = "HK"
        CheckTag()
    End Sub

    Protected Sub btn_cn_tag_Click(sender As Object, e As EventArgs) Handles btn_cn_tag.Click
        Session("tag") = "CN"
        CheckTag()
    End Sub

    Protected Sub btn_ma_tag_Click(sender As Object, e As EventArgs) Handles btn_ma_tag.Click
        Session("tag") = "MA"
        CheckTag()
    End Sub

    Protected Function CheckDescAndFile(ByVal product_ID As Integer) As Boolean

        Dim return_result As Boolean = False

        Dim db As New ProductDbDataContext()

        Dim desc_file_result = (From j In db.ProductNames
                               Where j.ProductID = product_ID
                               Select j.Description, j.fileUrl).FirstOrDefault()

        If (desc_file_result.Description IsNot Nothing And desc_file_result.Description.Length > 0) Or (desc_file_result.fileUrl IsNot Nothing And desc_file_result.fileUrl.Length > 0) Then
            return_result = True
        End If


        Return return_result
    End Function


    Protected Function checkMobileOrNot() As Boolean

        Dim return_result As Boolean = True

        If (Request.Browser.IsMobileDevice) Then
            return_result = False
        End If

        Return return_result


    End Function

    Protected Sub CheckTag()


        Select Case Session("tag")
            Case "HK"
                dsFeature.SelectCommand = "SELECT p.[ProductName],p.[Url],p.[ProductID],t.[TagName] FROM [view_ProductImageTag] p,[view_Tag] t WHERE (p.[TagID]=t.[TagID]) and (t.[Lang]='zh-hk') and (p.[Enabled] = @Enabled) and p.[FunctionID]=3 and (t.[Tag]='HK') ORDER BY p.[SortOrder]"
            Case "CN"
                dsFeature.SelectCommand = "SELECT p.[ProductName],p.[Url],p.[ProductID],t.[TagName] FROM [view_ProductImageTag] p,[view_Tag] t WHERE (p.[TagID]=t.[TagID]) and (t.[Lang]='zh-hk') and (p.[Enabled] = @Enabled) and p.[FunctionID]=3 and  (t.[Tag]='CN') ORDER BY p.[SortOrder]"
            Case "MA"
                dsFeature.SelectCommand = "SELECT p.[ProductName],p.[Url],p.[ProductID],t.[TagName] FROM [view_ProductImageTag] p,[view_Tag] t WHERE (p.[TagID]=t.[TagID]) and (t.[Lang]='zh-hk') and (p.[Enabled] = @Enabled) and p.[FunctionID]=3 and  (t.[Tag]='MA') ORDER BY p.[SortOrder]"
            Case Else
                dsFeature.SelectCommand = "SELECT [ProductName],[Url],[ProductID],[SortOrder] FROM [view_ProductImage] WHERE ([Enabled] = @Enabled) and [FunctionID]=3 ORDER BY [SortOrder] "
        End Select

        ListView1.DataBind()


    End Sub

    'Protected Sub LoadSQLdata(ByVal cID As Integer)
    '    Dim db As New CategoryDataClassesDataContext
    '    '"SELECT [CategoryID], [Category],[Url] FROM [view_Category] WHERE ([Enabled] = @Enabled) and [ParentID]= @ParentID
    '    SQLData = New JArray(From u In db.view_Categories _
    '                                      Where u.ParentID = cID And u.Enabled = True And u.FunctionID = 2 _
    '                                                       Select New JObject( _
    '                                                       New JProperty("CategoryID", u.CategoryID), _
    '                                                       New JProperty("Category", u.Category), _
    '                                                       New JProperty("Url", u.Url)))
    'End Sub

    'Protected Function ImageItem(ByVal imageUrl As String, ByVal ProductName As String, ByVal cID As Integer) As String

    '    LoadSQLdata(cID)

    '    Dim image As String = ""


    '    image &= String.Format("<img src='{0}' width='387' height='85' usemap='#image_map' border='0'>", imageUrl)

    '    image &= " <map name='image_map'>"
    '    image &= String.Format("<area rel='prettyPhoto[image_map]' shape='rect' coords='6,11,720,730' href='{0}' title='{1}'>", imageUrl, ProductName)

    '    For Each row In SQLData
    '        image &= String.Format("<area rel='prettyPhoto[image_map]' shape='rect' coords='6,11,720,730' href='product_image/product/{0}' title='{1}'>", row.Item("Url"), row.Item("Category"))
    '    Next

    '    image &= "</map>"

    '    Return image

    'End Function



    Protected Sub ListView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DataBound
        Dim currentPage As Integer = (DataPagerProducts.StartRowIndex / DataPagerProducts.PageSize) + 1
        Dim totalPages As Integer = Math.Ceiling(DataPagerProducts.TotalRowCount / DataPagerProducts.PageSize)


        lit_page.Text = String.Format("Page {0} of {1}", currentPage, totalPages)

    End Sub



    Protected Sub DataPagerProducts_PreRender(ByVal sender As Object, ByVal e As EventArgs)

        CheckTag()
        ListView1.DataBind()
    End Sub

    'Protected Sub btn_More_Click(sender As Object, e As EventArgs) Handles btn_More.Click

    '    Response.Redirect("~/Portfolio.aspx")
    'End Sub

    'Protected Function CheckTagAndReturnClassName(ByVal productId As Integer) As String
    '    Dim result As String = ""

    '    Dim CssClassDictionary As New Dictionary(Of String, String)()
    '    CssClassDictionary.Add("HK", "ff-item-type-1")
    '    CssClassDictionary.Add("CN", "ff-item-type-2")
    '    CssClassDictionary.Add("MA", "ff-item-type-3")

    '    Dim justLoveDB As New JustLoveDataContext()
    '    'found tag name by product ID
    '    Dim found_TagName = (From t In justLoveDB.view_ProductTags
    '                Where t.ProductID = productId
    '                Select t.TagName).FirstOrDefault

    '    'found class name by tag
    '    result = CssClassDictionary.Item(found_TagName)

    '    'return css class name
    '    Return result
    'End Function



    Protected Sub ListView1_PagePropertiesChanging(sender As Object, e As PagePropertiesChangingEventArgs)
        DataPagerProducts.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)

        CheckTag()
        ListView1.DataBind()
    End Sub
End Class
