Imports System.IO

Partial Class control_ProductLangControl
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim _FileBrowser As New CKFinder.FileBrowser()
        _FileBrowser.BasePath = ResolveClientUrl("~/ckfinder") & "/"
        _FileBrowser.SetupFCKeditor(htmlDescription)
        _FileBrowser.SetupFCKeditor(htmlCollection)

        Dim Key As String = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
        Page.ClientScript.RegisterOnSubmitStatement(htmlDescription.GetType(), Key, "FCKeditorAPI.GetInstance('" + htmlDescription.ClientID + "').UpdateLinkedField();")
        Key = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
        Page.ClientScript.RegisterOnSubmitStatement(htmlCollection.GetType(), Key, "FCKeditorAPI.GetInstance('" + htmlCollection.ClientID + "').UpdateLinkedField();")
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Public Sub LoadSiteFunction(ByVal FunctionID As Integer)
        Dim Site As New SiteFunctionClass(FunctionID)
        With Site
            'lblFunctionName.Text = .FunctionName
            'btnDelete_ConfirmButtonExtender.ConfirmText = String.Format("確定刪除{0}?", .FunctionName)
            'CategoryPlaceHolder.Visible = .HasCategory
			'ProductCodePlaceHolder.Visible = .HasProductCode

			DescriptionPlaceHolder.Visible = .HasDescription
			HTMLDescriptionPlaceHolder.Visible = .HTMLDescription

            If .HasDescription Then
                If .HTMLDescription Then
                    HTMLDescriptionPlaceHolder.Visible = True
                    'txtDescription.Visible = False
                    'txtDescription2.Visible = False
                    'RequiredFieldValidator3.Enabled = False
                    'RequiredFieldValidator4.Enabled = False
                Else
                    DescriptionPlaceHolder.Visible = True
                    'htmlDescription.Visible = False
                    'htmlDescription2.Visible = False
                    'RequiredFieldValidator1.Enabled = False
                End If
            End If
            If .FunctionID = CInt(ConfigurationManager.AppSettings("VideoFunctionID")) Then
                CollectionPlaceHolder.Visible = True
            Else
                CollectionPlaceHolder.Visible = False
            End If

			'DetailsPlaceHolder.Visible = .HasDetails
			'PricePlaceHolder.Visible = .HasPrice
			'DatePlaceHolder.Visible = .HasDateRange
			TagPlaceHolder.Visible = .HasTag
			'ImagePlaceHolder.Visible = (.HasProductImage And (lblProductID.Text <> "0" And lblProductID.Text <> ""))
			FilePlaceHolder.Visible = .HasFile
            'btnBrowse3.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtLongDescription.ClientID))
			If .HasFile Then
				btnBrowse.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtFileUrl.ClientID))
				btnBrowse2.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtFileUrl2.ClientID))
			End If
			If .HasTag Then
				btnBrowseImageEN.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtImageEN.ClientID))
				'btnBrowseImageTC.Attributes.Add("onclick", String.Format("BrowseServer(""{0}"");return false;", txtImageTC.ClientID))
			End If
		End With
        ViewState("FunctionSettings") = Site
        ViewState("fn") = FunctionID

        If FunctionID = CInt(ConfigurationManager.AppSettings("BannerFunctionID")) Then
            lblName.Text = "<b>Url</b><br/>You can use ~ to represent home page. e.g. ~/channel.aspx?id=1"
        End If
    End Sub

    Public Sub LoadProductLang(ByVal ProductID As Integer, ByVal Lang As String)
        Dim ProductAdapter As New ProductDataSetTableAdapters.ProductNameTableAdapter()
        Dim ProductTable As ProductDataSet.ProductNameDataTable
        Dim ProductRow As ProductDataSet.ProductNameRow

        ProductTable = ProductAdapter.GetDataByProductID(ProductID, Lang)
        If ProductTable.Rows.Count > 0 Then
            ProductRow = ProductTable.Rows(0)

            With ProductRow
				txtName.Text = .ProductName
                If DescriptionPlaceHolder.Visible Then
                    txtDescription.Text = .Description
                Else
                    htmlDescription.Value = .Description
                End If
                'txtLongDescription.Text = .LongDescription
                txtFileUrl.Text = .fileUrl
                txtFileUrl2.Text = .fileUrl2
            End With
        End If

        hfdLang.Value = Lang
    End Sub


    Public Function SaveProductLang(ByVal ProductID As Integer) As Boolean
		Dim Success As Boolean = True
        Dim Desc As String
        Dim CollectionInfo As String

        If DescriptionPlaceHolder.Visible Then
            Desc = txtDescription.Text
        Else
            Desc = htmlDescription.Value
        End If
        If ViewState("fn") = CInt(ConfigurationManager.AppSettings("VideoFunctionID")) Then
            CollectionInfo = htmlCollection.Value
        Else
            CollectionInfo = ""
        End If

        'If Me.IsValid() Then
        Dim ProductAdapter As New ProductDataSetTableAdapters.ProductNameTableAdapter()

        ProductAdapter.DeleteByProductIDLang(ProductID, hfdLang.Value)
        'ProductAdapter.Insert(ProductID, hfdLang.Value, txtName.Text, txtDescription.Text, txtFileUrl.Text, txtFileUrl2.Text, htmlDesciption.Value)
        ProductAdapter.Insert(ProductID, hfdLang.Value, txtName.Text, Desc, txtFileUrl.Text, txtFileUrl2.Text, CollectionInfo)

        'Else
        'Success = False
        'End If

        Return Success
    End Function

    Public Sub SetLang(ByVal Lang As String)
        hfdLang.Value = Lang
    End Sub

End Class
