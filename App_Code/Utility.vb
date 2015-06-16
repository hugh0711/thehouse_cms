Imports Microsoft.VisualBasic
Imports System.IO

Public Class Utility
	Public Shared ReadOnly Lang_Total As Integer = 3
	Public Shared Lang_Array(Lang_Total - 1) As String

	Public Shared ReadOnly MyCulture_HK As String = "zh-hk"
	Public Shared ReadOnly MyCulture_EN As String = "en-us"
	Public Shared ReadOnly MyCulture_CN As String = "zh-cn"
	Public Shared ReadOnly Default_Culture As String = ConfigurationManager.AppSettings("DefaultLanguage")

	Public Shared ReadOnly FORMMODE_READ As String = "READ"
	Public Shared ReadOnly FORMMODE_EDIT As String = "EDIT"
	Public Shared ReadOnly FORMMODE_INSERT As String = "INSERT"

	Public Shared ReadOnly DateTimeFormat As String = "yyyy-MM-dd HH:mm"
	Public Shared ReadOnly DateFormat As String = "yyyy-MM-dd"
	Public Shared ReadOnly DateTimeNull As DateTime = DateTime.ParseExact("1753-01-01", DateFormat, Nothing)

	Public Shared ReadOnly FunctionId_News As Integer = 3

	Public Shared ReadOnly UPLOAD_PATH As String = ConfigurationManager.AppSettings("UploadPath")
	Public Shared ReadOnly PATH_SEPERATOR As String = " > "
	Public Shared ReadOnly NoImage As String = "~/images/noimage.gif"

	Public Shared Sub InitLang()
		Lang_Array(0) = MyCulture_EN
		Lang_Array(1) = MyCulture_HK
		Lang_Array(2) = MyCulture_CN
	End Sub

	Public Shared Function AmountToString(ByVal amt As Decimal) As String
		Return String.Format("{0:f2}", amt)
	End Function

	Public Shared Function DateTimeToString(ByVal datetimeValue As DateTime, Optional ByVal timeFormat As Boolean = True) As String
		Dim s As String = ""
		If datetimeValue = Utility.DateTimeNull Then
			s = ""
		Else
			If timeFormat Then
				s = String.Format("{0:" & DateTimeFormat & "}", datetimeValue)
			Else
				s = String.Format("{0:" & DateFormat & "}", datetimeValue)
			End If
		End If
		Return s
	End Function

	Public Shared Function StringToDateTime(ByVal s As String, Optional ByVal timeformat As Boolean = True) As DateTime
		Dim d As DateTime = DateTimeNull
		If s.Length > 0 Then
			If timeformat Then
				d = DateTime.ParseExact(s, DateTimeFormat, Nothing)
			Else
				d = DateTime.ParseExact(s, DateFormat, Nothing)
			End If
		End If
		Return d
	End Function

	'Public Shared Function PathGenerator(ByVal FunctionId As Integer, ByVal ParentId As Integer) As String
	'	Dim mainPath As String = ""
	'	Dim p As String = ""
	'	Dim r As CategoryDataSet.CategoryRow
	'	Dim t As New CategoryDataSet.CategoryDataTable

	'	mainPath = String.Format("<a href=""CategoryList.aspx?functionid={0}&parentid=0"">{1}</a>", FunctionId, CType((New SiteFunctionDataSetTableAdapters.SiteFunctionTableAdapter).GetDataByFunctionId(FunctionId).Rows(0), SiteFunctionDataSet.SiteFunctionRow).FunctionDescription)

	'	Do While ParentId > 0
	'		t = (New CategoryDataSetTableAdapters.CategoryTableAdapter).GetDataByCategoryId(ParentId)
	'		If t.Rows.Count > 0 Then
	'			r = t.Rows(0)
	'			p = PATH_SEPERATOR & String.Format("<a href=""CategoryList.aspx?functionid={0}&parentid={1}"">{2}</a>", FunctionId, r.CategoryId, r.CategoryName) & p
	'			ParentId = r.ParentId
	'		Else
	'			Exit Do
	'		End If
	'	Loop
	'	Return mainPath & p
	'End Function

	'Public Shared Function ProductCategoryPathGenerator(ByVal ParentId As Integer) As String
	'	Dim mainPath As String = ""
	'	Dim p As String = ""
	'	Dim r As ProductCategoryDataSet.ProductCategoryRow
	'	Dim t As New ProductCategoryDataSet.ProductCategoryDataTable

	'	mainPath = String.Format("<a href=""ProductCategoryList.aspx?Parentid=0"">{0}</a>", "Category List")

	'	Do While ParentId > 0
	'		t = (New ProductCategoryDataSetTableAdapters.ProductCategoryTableAdapter).GetDataByCategoryId(ParentId)
	'		If t.Rows.Count > 0 Then
	'			r = t.Rows(0)
	'			p = PATH_SEPERATOR & String.Format("<a href=""ProductCategoryList.aspx?Parentid={0}"">{1}</a>", r.ProductCategoryId, r.ProductCategoryName) & p
	'			ParentId = r.ProductParentId
	'		Else
	'			Exit Do
	'		End If
	'	Loop
	'	Return mainPath & p
	'End Function

	'Public Shared Function ProductPathGenerator(ByVal CategoryId As Integer, ByVal Culture As String) As String
	'	Dim mainPath As String = ""
	'	Dim p As String = ""
	'	Dim p_Seperator As String = ""
	'	Dim r As ProductCategoryDataSet.view_ProductCategoryRow
	'	Dim t As New ProductCategoryDataSet.view_ProductCategoryDataTable

	'	'If Culture = MyCulture_EN Then
	'	'	mainPath = String.Format("<a href=""Default.aspx"">{0}</a>", "Home")
	'	'ElseIf Culture = MyCulture_HK Then
	'	'	mainPath = String.Format("<a href=""Default.aspx"">{0}</a>", "首頁")
	'	'End If


	'	Do While CategoryId > 0
	'		t = (New ProductCategoryDataSetTableAdapters.view_ProductCategoryTableAdapter).GetDataByCategoryId(CategoryId, Culture)
	'		If t.Rows.Count > 0 Then
	'			r = t.Rows(0)
	'			p = p_Seperator & String.Format("<a href=""ProductList.aspx?CategoryId={0}"">{1}</a>", r.ProductCategoryId, r.LangProductCategoryName) & p
	'			p_Seperator = " > "
	'			CategoryId = r.ProductParentId
	'		Else
	'			Exit Do
	'		End If
	'	Loop
	'	Return mainPath & p
	'End Function


	Public Shared Function MyCulture() As String
		Dim s As String = Default_Culture
		If HttpContext.Current.Session("MyCulture") IsNot Nothing Then
			s = HttpContext.Current.Session("MyCulture")
		Else
			HttpContext.Current.Session("MyCulture") = s
		End If
		Return s.ToLower
	End Function

	Public Shared Sub SetMyCulture(ByVal Lang As String)
		HttpContext.Current.Session("MyCulture") = Lang
	End Sub


	'Shopping Cart
	'Public Shared Function GetShoppingCart() As DsCart
	'	Dim cart As New DsCart
	'	If HttpContext.Current.Session("Cart") IsNot Nothing Then
	'		cart = HttpContext.Current.Session("Cart")
	'	Else
	'		SetShoppingCart(cart)
	'	End If
	'	Return cart
	'End Function

	'Public Shared Sub SetShoppingCart(ByVal cart As DsCart)
	'	HttpContext.Current.Session("Cart") = cart
	'End Sub

	'Public Shared Sub ClearShoppingCart()
	'	Dim cart As DsCart = GetShoppingCart()
	'	cart.Clear()
	'	SetShoppingCart(cart)
	'End Sub

	'Public Shared Sub RemoveProductByCartId(ByVal CartId As Integer)
	'	Dim cart As DsCart = GetShoppingCart()
	'	cart.RemoveProductByCartId(CartId)
	'	SetShoppingCart(cart)
	'End Sub


	' File Save Delete 
	Public Shared Sub DeleteImage(ByVal ImageFile As String)
		Dim TheFile As FileInfo = New FileInfo(System.Web.HttpContext.Current.Server.MapPath(ImageFile))
		If TheFile.Exists Then
			File.Delete(System.Web.HttpContext.Current.Server.MapPath(ImageFile))
		End If
	End Sub

	Public Shared Function SaveFile(ByVal FileUpload As FileUpload, ByVal txtFileUrl As Label, ByVal Path As String, Optional ByVal returnFileNameOnly As Boolean = True) As String
		Dim s As String = System.Web.HttpContext.Current.Server.MapPath(Path)
		Dim dtFileName As String = ""
		If FileUpload.HasFile Then
			'dtFileName = DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & FileUpload1.FileName
			dtFileName = DateTime.Now.ToString("yyyyMMddHHmmss") & System.IO.Path.GetExtension(FileUpload.FileName).ToLower
			s = s & dtFileName
			FileUpload.SaveAs(s)
			's = Path & dtFileName
			If returnFileNameOnly Then
				s = dtFileName
			Else
				s = System.IO.Path.Combine(Path, dtFileName)
			End If

		Else
			s = txtFileUrl.Text
		End If
		System.Threading.Thread.Sleep(1200)
		Return s
	End Function

	Public Shared Function FileExtValidate(ByVal fileUpload As FileUpload) As Boolean
		Dim flag As Boolean = True
		If fileUpload.HasFile Then
			Dim FileExtension As String = Path.GetExtension(fileUpload.FileName).ToLower()
			Select Case FileExtension
				Case ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff"
					flag = True
				Case Else
					flag = False
			End Select
		End If
		Return flag
	End Function

	Public Shared Function ValidateDateTimeString(ByVal d As String, ByVal TimeFormat As Boolean) As Boolean
		Dim dt As DateTime
		Dim val As Boolean = True
		Try
			dt = StringToDateTime(d, TimeFormat)
		Catch ex As Exception
			val = False
		End Try
		Return val
	End Function

    Public Shared Sub IsLogined()
        Dim Refer As String = HttpContext.Current.Request.Url.AbsoluteUri.ToString
        If Membership.GetUser Is Nothing Then
            'Dim Refer As String = HttpContext.Current.Request.UrlReferrer.ToString()
            HttpContext.Current.Response.Redirect(String.Format("MemberLogin.aspx?ReturnUrl={0}", Refer), True)
        Else
            HttpContext.Current.Response.Redirect(Refer)
        End If
    End Sub

#Region "COM-based client access methods"
	Public Shared Function GetShoppingCart() As DsCart
		Dim cart As New DsCart
		If HttpContext.Current.Session("Cart") IsNot Nothing Then
			cart = HttpContext.Current.Session("Cart")
		Else
			SetShoppingCart(cart)
		End If
		Return cart
	End Function

	'Shopping Cart
	Public Shared Sub SetShoppingCart(ByVal cart As DsCart)
		HttpContext.Current.Session("Cart") = cart
	End Sub

	Public Shared Sub ClearShoppingCart()
		Dim cart As DsCart = GetShoppingCart()
		cart.Clear()
		SetShoppingCart(cart)
	End Sub

	Public Shared Sub RemoveProductByCartId(ByVal CartId As Integer)
		Dim cart As DsCart = GetShoppingCart()
		cart.RemoveProductByCartId(CartId)
		SetShoppingCart(cart)
	End Sub
#End Region

#Region "Trim Text"

    Public Shared Function TrimHtmlText(ByVal strText As String, Optional ByVal Length As Integer = Integer.MaxValue) As String
        Dim ret As String = ""
        Dim p1 As Integer = -1
        Dim p2 As Integer
        Dim p0 As Integer = 0

        Do
            p1 = strText.IndexOf("<", p0)
            If p1 >= 0 Then
                p2 = strText.IndexOf(">", p1)
                If p2 >= 0 Then
                    ret &= strText.Substring(p0, p1 - p0)
                    p0 = p2 + 1
                    If p0 >= strText.Length Then
                        Exit Do
                    End If
                End If
            End If
        Loop Until ret.Length >= Length Or p1 < 0
        ret &= strText.Substring(p0, strText.Length - p0).Trim()

        Dim r0 As String = ret
        Try
            ret = ret.Substring(0, Length)
        Catch ex As Exception
        End Try

        If ret.Length < r0.Length Then
            ret &= "..."
        End If

        Return ret
    End Function

    Public Shared Function TrimHtml(ByVal strText As String, Optional ByVal Length As Integer = Integer.MaxValue) As String
        Dim ret As String = ""
        Dim retHtml As String = ""
        Dim p1 As Integer = -1
        Dim p2 As Integer
        Dim p0 As Integer = 0
        strText = strText.Replace("&nbsp;", " ")
        TagStack = New Stack(Of String)

        Do
            p1 = strText.IndexOf("<", p0)
            If p1 >= 0 Then
                GetTag(strText.Substring(p1))
                p2 = strText.IndexOf(">", p1)
                If p2 >= 0 Then
                    If strText.Substring(p2 - 1, 1) = "/" Then
                        TagStack.Pop()
                    End If
                    ret &= strText.Substring(p0, p1 - p0)
                    p0 = p2 + 1
                    If p0 >= strText.Length Then
                        Exit Do
                    End If
                End If
            End If
        Loop Until ret.Length >= Length Or p1 < 0
        ret &= strText.Substring(p0, strText.Length - p0).Trim()

        Try
            ret = ret.Substring(0, Length)
            ret &= "..."
        Catch ex As Exception
        End Try


        Return ret
    End Function

    Shared TagStack As New Stack(Of String)

    Private Shared Sub GetTag(ByVal Tag As String)
        If Tag.StartsWith("</") Then
            TagStack.Pop()
            Return
        End If

        If Tag.StartsWith("<") Then
            Tag = Tag.Substring(1)
        End If

        Dim p As Integer
        p = Tag.IndexOf(" ")
        If p <> -1 Then
            Tag = Tag.Substring(0, p - 1)
        End If

        TagStack.Push(Tag)
    End Sub
#End Region


    Public Shared Function GetTime(ByVal CommentDate As DateTime, Optional ByVal Prefix As String = "", Optional ByVal Suffix As String = "發佈") As String
        Dim ret As String = ""
        Dim CurrentTime As Date = Now()

        If CommentDate.AddHours(1) > CurrentTime Then
            ret = String.Format("{0}分鐘前", DateDiff(DateInterval.Minute, CommentDate, CurrentTime))
        ElseIf CommentDate.AddDays(1) > CurrentTime Then
            ret = String.Format("{0}小時前", DateDiff(DateInterval.Hour, CommentDate, CurrentTime))
        ElseIf CommentDate.AddDays(6) > CurrentTime Then
            ret = String.Format("{0}日前", DateDiff(DateInterval.Day, CommentDate, CurrentTime))
        Else
            If CommentDate.Year = CurrentTime.Year Then
                ret = CommentDate.ToString("M月d日 h:mmtt")
            Else
                ret = CommentDate.ToString("yyyy年M月d日 h:mmtt")
            End If
        End If

        ret = String.Format("{0}{1}{2}", Prefix, ret, Suffix)
        Return ret
    End Function

    Public Shared Function GenerateRandomString(ByVal Length As Integer, Optional ByVal CharacterSet As String = "") As String
        If CharacterSet = "" Then
            CharacterSet = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
        End If

        Dim MaxNum As Integer = CharacterSet.Length - 1
        Dim Ran As New Random()
        Dim Ret As String = ""
        For i As Integer = 1 To Length
            Ret &= CharacterSet(Ran.Next(0, MaxNum))
        Next

        Return Ret
    End Function

#Region "Date"

    Public Shared NoBirthday As Date = #1/1/1800#
    Public Shared NoStartDate As Date = #1/1/1800#
    Public Shared NoEndDate As Date = #1/1/2900#

    Public Shared Function GetStartDate(StringValue As String) As Date
        If String.IsNullOrWhiteSpace(StringValue) Then
            Return NoStartDate
        Else
            Return CDate(StringValue)
        End If
    End Function

    Public Shared Function GetEndDate(StringValue As String) As Date
        If String.IsNullOrWhiteSpace(StringValue) Then
            Return NoEndDate
        Else
            Return CDate(StringValue)
        End If
    End Function

#End Region

#Region "Promo"

    Public Shared Function GetNavigateUrl(UnitFunctionID As Integer, Value As String) As String
        Dim Url As String = ""

        Select Case UnitFunctionID
            Case CInt(ConfigurationManager.AppSettings("VideoFunctionID"))
                If Value.StartsWith("category://") Then
                    Dim CategoryID As Integer = CInt(Value.Substring(11))
                    Dim ParentID As Integer = (New CategoryDataSetTableAdapters.CategoryTableAdapter()).GetParentID(CategoryID)
                    If ParentID = 0 Then
                        Url = String.Format("~/channel.aspx?id={0}", CategoryID)
                    Else
                        Url = String.Format("~/program.aspx?id={0}", CategoryID)
                    End If
                ElseIf Value.StartsWith("unit://") Then
                    Dim UnitID As Integer = CInt(Value.Substring(7))
                    Url = String.Format("~/episode.aspx?id={0}", UnitID)
                ElseIf Value.StartsWith("tag://") Then
                    Dim TagID As Integer = CInt(Value.Substring(6))
                    Url = String.Format("~/videos.aspx?tag={0}", TagClass.EncryptID(TagID))
                ElseIf Value <> "" Then
                    Url = Value
                End If
        End Select

        Return Url
    End Function

#End Region

    Public Shared Function GetDomain() As String
        Dim Domain As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
        If HttpContext.Current.Request.ApplicationPath <> "" Then
            Domain &= HttpContext.Current.Request.ApplicationPath & "/"
        End If
        Return Domain
    End Function

    Public Shared Function ReplaceLf(value As String) As String
        value = value.Replace(vbCrLf, "<br />")
        value = value.Replace(vbCr, "<br />")
        value = value.Replace(vbLf, "<br />")
        Return value
    End Function



#Region "Image Path"

    Public Shared Function GetAlbumPath(AlbumID As Guid, ImageSize As ImageClass.ImageSize) As String
        Return GetAlbumPath(AlbumID.ToString(), ImageSize)
    End Function

    Public Shared Function GetAlbumPath(AlbumID As String, ImageSize As ImageClass.ImageSize) As String
        Dim BasePath As String = ConfigurationManager.AppSettings("AlbumPath")
        BasePath = Path.Combine(BasePath, AlbumID)
        Select Case ImageSize
            Case ImageClass.ImageSize.Thumbnail
                BasePath = Path.Combine(BasePath, "tb")
        End Select
        Return BasePath
    End Function

#End Region

#Region "Directory & Files"
    Public Shared Sub DeleteFilesAndFolders(DirectoryName As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(DirectoryName)
        Dim subDirs As DirectoryInfo() = dirInfo.GetDirectories()
        For Each dir As DirectoryInfo In subDirs
            FindAndDeleteFolder(dir)
        Next
        DeleteFilesInFolder(dirInfo)
    End Sub

    Private Shared Sub FindAndDeleteFolder(ByVal drinfo As DirectoryInfo)
        Dim subDirs As DirectoryInfo() = drinfo.GetDirectories()
        For Each dir As DirectoryInfo In subDirs
            FindAndDeleteFolder(dir)
        Next
        DeleteFilesInFolder(drinfo)
        If drinfo.GetFiles().Length = 0 Then
            drinfo.Delete()
        End If
    End Sub

    Private Shared Sub DeleteFilesInFolder(ByVal di As DirectoryInfo)
        Dim fileInfo As FileInfo() = di.GetFiles()
        For Each File As FileInfo In fileInfo
            File.Attributes = FileAttributes.Normal
            File.Delete()
        Next
    End Sub

    Public Shared Sub CopyDirectory(ByVal sourceDir As String, ByVal destDir As String)
        Try
            If Not System.IO.Directory.Exists(destDir) Then
                System.IO.Directory.CreateDirectory(destDir)
            End If

            For Each strEntry As String In System.IO.Directory.GetFiles(sourceDir)
                Dim fileNew As System.IO.FileInfo
                fileNew = New System.IO.FileInfo(strEntry)
                'disableReadOnly(fileNew)

                If fileNew.Exists Then
                    fileNew.CopyTo(destDir & "\" & fileNew.Name, True)
                End If
            Next

            For Each strEntry As String In System.IO.Directory.GetDirectories(sourceDir)
                Dim strDest As String = System.IO.Path.Combine(destDir, System.IO.Path.GetFileName(strEntry))
                CopyDirectory(strEntry, strDest)
            Next
        Catch ex As Exception
            'WriteToEventLog(ex.ToString())
        End Try
    End Sub

#End Region


End Class