Imports System.IO

Partial Class backoffice_album
    Inherits System.Web.UI.Page

    Protected Shared AlbumPath As String
    Protected Shared AlbumTBPath As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrWhiteSpace(Request("album")) Then
                hfdAlbumID.Value = Request("album")
                LoadData()


                LoadCategoryTree()
            Else
                btnSave.Text = "建立"
                btnBack.Text = "取消"
                CheckTreeView()
                btnSaveBack.Visible = False
                btnPhotos.Visible = False
                UpdatePanel2.Visible = False
                btnDeleteAlbum.Visible = False
            End If
        End If
    End Sub

    Protected Sub LoadData()
        Dim db As New CMSDataContext
        Dim AlbumID As New Guid(hfdAlbumID.Value)

        AlbumPath = Utility.GetAlbumPath(hfdAlbumID.Value, ImageClass.ImageSize.Normal)
        AlbumTBPath = Utility.GetAlbumPath(hfdAlbumID.Value, ImageClass.ImageSize.Thumbnail)

        Try
            Dim q = (From a In db.Albums Where a.AlbumID = AlbumID Select a).Single()
            txtAlbumName.Text = q.AlbumName
            txtDescription.Text = q.Description
            txtAlbumDate.Text = String.Format("{0:yyyy-MM-dd}", q.AlbumDate)
            chkEnabled.Checked = q.Enabled
            If Not String.IsNullOrWhiteSpace(q.PreviewUrl) Then
                lnkCover.ImageUrl = Path.Combine(AlbumTBPath, q.PreviewUrl)
                lnkCover.NavigateUrl = Path.Combine(AlbumPath, q.PreviewUrl)
            End If
            btnPhotos.Visible = True

            BindPhotos()
        Catch ex As Exception
            Response.Redirect("~/backoffice/albums.aspx")
        End Try
    End Sub

    Protected Sub BindPhotos()
        Dim db As New CMSDataContext
        Dim AlbumID As New Guid(hfdAlbumID.Value)

        Dim q1 = From p In db.AlbumPhotos
        Where p.AlbumID = AlbumID
        Order By p.CreateDate
        Select p

        lstPhoto.DataSource = q1
        lstPhoto.DataBind()

        UpdatePanel2.Update()
    End Sub

    Protected Sub SaveData()
        Dim db As New CMSDataContext
        Dim AlbumID As Guid
        Dim PreviewUrl As String = ""

        If Not String.IsNullOrWhiteSpace(lnkCover.ImageUrl) Then
            PreviewUrl = Path.GetFileName(lnkCover.ImageUrl)
        End If

        If String.IsNullOrWhiteSpace(hfdAlbumID.Value) Then
            ' Create an album
            AlbumID = Guid.NewGuid()
            Dim Album As New Album With {
                .AlbumID = AlbumID,
                .AlbumName = txtAlbumName.Text,
                .Description = txtDescription.Text,
                .AlbumDate = CDate(txtAlbumDate.Text),
                .Enabled = chkEnabled.Checked,
                .PhotoCount = 0,
                .PreviewUrl = PreviewUrl,
                .SortOrder = 0,
                .CreateDate = Now(),
                .CreatedBy = User.Identity.Name,
                .UpdateDate = Now(),
                .UpdatedBy = User.Identity.Name
                }
            db.Albums.InsertOnSubmit(Album)
            db.SubmitChanges()
            hfdAlbumID.Value = AlbumID.ToString()
            btnPhotos.Visible = True
            btnSave.Text = "存儲"
            btnSaveBack.Visible = True
            btnBack.Text = "返回"
            CreateAlbumPath()

            'Insert category and album to AlbumCategory table
            checkAlbumCategory()
        Else
            ' Update the album
            AlbumID = New Guid(hfdAlbumID.Value)
            Dim q = (From a In db.Albums
                        Where a.AlbumID = AlbumID
                        Select a).Single()
            q.AlbumName = txtAlbumName.Text
            q.Description = txtDescription.Text
            q.AlbumDate = CDate(txtAlbumDate.Text)
            q.Enabled = chkEnabled.Checked
            q.PreviewUrl = PreviewUrl
            q.UpdateDate = Now()
            q.UpdatedBy = User.Identity.Name
            db.SubmitChanges()

            'Update category and album in AlbumCategory table
            checkAlbumCategory()
        End If
    End Sub


    Protected Sub checkAlbumCategory()
        Dim db As New CMSDataContext
        Dim AlbumID As New Guid(hfdAlbumID.Value)

        Dim A_C = (From a In db.AlbumCategories Where a.AlbumID = AlbumID Select a).FirstOrDefault
        If A_C IsNot Nothing Then
            'Update category and album in AlbumCategory table
            UpdateAlbumCategory()
        Else
            'Insert category and album to AlbumCategory table
            NewAlbumCategory()
        End If

    End Sub


    Protected Sub NewAlbumCategory()
        Dim db As New CMSDataContext
       Dim AlbumID As New Guid(hfdAlbumID.Value)
        Dim parentID = lblParentID.Text


        Dim AlbumCate As New AlbumCategory With {
                .CategoryID = parentID,
                .AlbumID = AlbumID
                }
        db.AlbumCategories.InsertOnSubmit(AlbumCate)
        db.SubmitChanges()
    End Sub


    Protected Sub UpdateAlbumCategory()
        Dim db As New CMSDataContext
        Dim AlbumID As New Guid(hfdAlbumID.Value)

        Dim parentID = lblParentID.Text

        If parentID = "" Then
            parentID = 0
        End If




        Dim q = (From a In db.AlbumCategories
                        Where a.AlbumID = AlbumID
                        Select a).Single()
        q.CategoryID = parentID

        db.SubmitChanges()



    End Sub



    Protected Sub btnPhotos_Click(sender As Object, e As System.EventArgs) Handles btnPhotos.Click
        Response.Redirect(String.Format("~/backoffice/albumphotos.aspx?album={0}", hfdAlbumID.Value))
    End Sub

    Protected Sub CreateAlbumPath()
        Dim BasePath As String = ConfigurationManager.AppSettings("AlbumPath")
        Dim AlbumPath As String = IO.Path.Combine(BasePath, hfdAlbumID.Value)
        If Not Directory.Exists(MapPath(AlbumPath)) Then
            Directory.CreateDirectory(MapPath(AlbumPath))
        End If
        AlbumPath = Path.combine(AlbumPath, "tb")
        If Not Directory.Exists(MapPath(AlbumPath)) Then
            Directory.CreateDirectory(MapPath(AlbumPath))
        End If
    End Sub

    Dim treeview_vaild As Boolean = False
    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click



        If Page.IsValid Then
            If CheckTreeView() Then
                Try
                    SaveData()
                    lblMessage.Text = "相簿成功存儲"
                Catch ex As Exception
                    lblMessage.Text = "相簿存儲失敗: " & ex.Message
                End Try
            End If



        End If
    End Sub

    Protected Sub btnSaveBack_Click(sender As Object, e As System.EventArgs) Handles btnSaveBack.Click
        If Page.IsValid Then
            If CheckTreeView() Then
                Try
                    SaveData()
                    Response.Redirect(String.Format("~/backoffice/albums.aspx"))
                Catch ex As Exception
                    lblMessage.Text = "相簿存儲失敗: " & ex.Message
                End Try
            End If

        End If
    End Sub

    Protected Function CheckTreeView() As Boolean
        If lblParentID.Text = "" Then
            lbl_treeViewCheck.Visible = True
            Return False
        Else
            lbl_treeViewCheck.Visible = False
            Return True
        End If
    End Function


    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(String.Format("~/backoffice/albums.aspx"))
    End Sub

    Protected Sub lstPhoto_ItemCanceling(sender As Object, e As System.Web.UI.WebControls.ListViewCancelEventArgs) Handles lstPhoto.ItemCanceling
        lstPhoto.EditIndex = -1
        BindPhotos()
    End Sub

    Protected Sub lstPhoto_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles lstPhoto.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "cover"
                Dim AlbumID As New Guid(hfdAlbumID.Value)
                lnkCover.ImageUrl = Path.Combine(Utility.GetAlbumPath(AlbumID, ImageClass.ImageSize.Thumbnail), e.CommandArgument)
                lnkCover.NavigateUrl = Path.Combine(Utility.GetAlbumPath(AlbumID, ImageClass.ImageSize.Normal), e.CommandArgument)
                UpdatePanel3.Update()
        End Select
    End Sub

    Protected Sub lstPhoto_ItemDeleting(sender As Object, e As System.Web.UI.WebControls.ListViewDeleteEventArgs) Handles lstPhoto.ItemDeleting
        Dim db As New CMSDataContext
        Dim PhotoID As New Guid(e.Keys(0).ToString())
        Dim q = (From p In db.AlbumPhotos Where p.PhotoID = PhotoID Select p).Single()
        Try
            File.Delete(MapPath(Path.Combine(Utility.GetAlbumPath(hfdAlbumID.Value, ImageClass.ImageSize.Normal), q.PhotoName)))
            File.Delete(MapPath(Path.Combine(Utility.GetAlbumPath(hfdAlbumID.Value, ImageClass.ImageSize.Thumbnail), q.PhotoName)))
        Catch ex As Exception
        End Try



        'Delete record on AlbumPhoto_info
        DeleteAlbumPhoto_info(PhotoID)

        db.AlbumPhotos.DeleteOnSubmit(q)
        db.SubmitChanges()
        BindPhotos()
    End Sub

    Protected Sub lstPhoto_ItemEditing(sender As Object, e As System.Web.UI.WebControls.ListViewEditEventArgs) Handles lstPhoto.ItemEditing
        lstPhoto.EditIndex = e.NewEditIndex
        BindPhotos()
    End Sub

    Protected Sub lstPhoto_ItemUpdating(sender As Object, e As System.Web.UI.WebControls.ListViewUpdateEventArgs) Handles lstPhoto.ItemUpdating
        Dim db As New CMSDataContext
        Dim PhotoID As New Guid(e.Keys(0).ToString())
        Dim q = (From p In db.AlbumPhotos Where p.PhotoID = PhotoID Select p).Single()


        Try
            q.Description = e.NewValues("Description").ToString()
        Catch ex As Exception
            q.Description = ""
        End Try

        


        q.UpdateDate = Now()
        q.UpdatedBy = User.Identity.Name
        db.SubmitChanges()

        lstPhoto.EditIndex = -1
        BindPhotos()
    End Sub

    Protected Sub btnDeleteAlbum_Click(sender As Object, e As System.EventArgs) Handles btnDeleteAlbum.Click
        Dim db As New CMSDataContext
        Dim AlbumID As New Guid(hfdAlbumID.Value)
        Dim q = (From p In db.AlbumPhotos Where p.AlbumID = AlbumID Select p)
        db.AlbumPhotos.DeleteAllOnSubmit(q)

        Dim q2 = (From a In db.Albums Where a.AlbumID = AlbumID Select a).Single()
        db.Albums.DeleteOnSubmit(q2)

        'Delete IDs on AlbumCategories
        Dim q3 = (From a In db.AlbumCategories Where a.AlbumID = q2.AlbumID Select a).Single()
        db.AlbumCategories.DeleteOnSubmit(q3)

        'Delete all records on AlbumPhotos

        If q IsNot Nothing Then

            For Each row In q
                'Delete all records on AlbumPhoto_info
                DeleteAlbumPhoto_info(row.PhotoID)
            Next
        End If


       

        db.SubmitChanges()

        Dim AlbumPath As String = MapPath(Utility.GetAlbumPath(AlbumID, ImageClass.ImageSize.Normal))
        Try
            Utility.DeleteFilesAndFolders(AlbumPath)
            Directory.Delete(AlbumPath)
        Catch
        End Try

        Response.Redirect("~/backoffice/albums.aspx")
    End Sub

    Protected Sub DeleteAlbumPhoto_info(PhotoID As Guid)
        Dim db As New CMSDataContext
        Dim q5 = (From a In db.AlbumPhoto_infos Where a.PhotoID = PhotoID Select a).Single()
        db.AlbumPhoto_infos.DeleteOnSubmit(q5)
        db.SubmitChanges()
    End Sub




#Region "Tree View"

    Protected Sub TreeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.SelectedNodeChanged
        If TreeView1.SelectedNode Is Nothing Then
            lblParentID.Text = "0"
            lblParentID.Visible = False
        Else
            Dim CategoryID As Integer = Convert.ToInt32(TreeView1.SelectedNode.Value)
            LoadCategory(CategoryID)
            ViewState("ByCategory") = True
            lblCategoryID.Text = CategoryID
            BindList()
            'ReorderList1.DataBind()
            btnSelectCategory_ModalPopupExtender.Hide()
            lblParentID.Visible = False
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


        CategoryTable = CategoryAdaptor.GetDataByParentID(CInt(ID), 2)
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

    Protected Sub LoadCategory(ByVal CategoryID As Integer)
        With CategoryPathControl1
            .CategoryID = CategoryID
            .ShowPath()
        End With

        Dim CategoryAdatper As New CategoryDataSetTableAdapters.CategoryTableAdapter()
        'ViewState("fn") = CategoryAdatper.GetFunctionID(CategoryID)

    End Sub

   

    Protected Sub BindList()
        If ViewState("ByCategory") Then
            
            lblAllCategory.Visible = False
            lblParentID.Visible = False
        Else
            
            lblAllCategory.Visible = True
            
        End If

    End Sub

    Protected Sub LoadCategoryTree()
        BindList()
        Dim db As New CMSDataContext
        Dim AlbumID As New Guid(hfdAlbumID.Value)


        Dim cID = (From a In db.AlbumCategories
                        Where a.AlbumID = AlbumID
                        Select a).FirstOrDefault

        If cID IsNot Nothing Then
            With CategoryPathControl1
                .CategoryID = cID.CategoryID
                .ShowPath()
            End With

            Dim CategoryAdatper As New CategoryDataSetTableAdapters.CategoryTableAdapter()

            ViewState("ByCategory") = True
            BindList()
            lblParentID.Text = cID.CategoryID
        Else

        End If





    End Sub

End Class
