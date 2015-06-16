<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Portfolio_Sub.aspx.vb" Inherits="Portfolio_Sub" MasterPageFile="~/master/MainMasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <asp:HiddenField runat="server" ID="hfdCatID" />


   <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;" name="viewport" />
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<link href="css/photo_swipe/styles.css" type="text/css" rel="stylesheet" />
	
	<link href="css/photo_swipe/photoswipe.css" type="text/css" rel="stylesheet" />
	
	<script type="text/javascript" src="js/photo_swipe/klass.min.js"></script>
	<script type="text/javascript" src="js/photo_swipe/code.photoswipe-3.0.5.min.js"></script>

    <script type="text/javascript">

        (function (window, PhotoSwipe) {

            document.addEventListener('DOMContentLoaded', function () {

                var
					options = {},
					instance = PhotoSwipe.attach(window.document.querySelectorAll('#Gallery a'), options);

            }, false);


        }(window, window.Code.PhotoSwipe));





	</script>







     <!--******************** Portfolio Section ********************-->
    
    <!--******************** Portfolio Section ********************-->
    <section id="portfolio" class="single-page scrollblock">
      <div class="container">
        <h1 id="folio-headline"><img src="images/hkspaimg/portfolio/pofo.png"></h1>


          <asp:SqlDataSource runat="server" ID="dsFeature" 
                ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                
                
                SelectCommand="SELECT [Category],[PreviewUrl],[CategoryID],[AlbumID],[AlbumName] FROM [view_AlbumCategory] WHERE ([Enabled] = @Enabled) and [CategoryID]= @CategoryID">
                
              <SelectParameters>
                   <asp:ControlParameter ControlID="hfdCatID" Name="CategoryID" 
                        PropertyName="Value"/>
                    <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>



          <div class="row">
          <asp:ListView runat="server" ID="lvwFeature" DataSourceID="dsFeature">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>

                   
                        <div class="span4">
                            <div class="mask2"> 

                               

                                <%--<script type="text/javascript" charset="utf-8">
                                    $(document).ready(function () {
                                        $("area[rel^='prettyPhoto']").prettyPhoto();
                                    });
                                </script>

                                <script src="js/jquery.js" type="text/javascript" charset="utf-8"></script>
                                <link rel="stylesheet" href="css/hkspa/prettyPhoto.css" type="text/css" media="screen" charset="utf-8" />
                                <script src="js/nav/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>--%>


                                

                                <%--<asp:LinkButton runat="server" ID="btn_IamgeGalley" CommandArgument='<%#Eval("CategoryID")%>' OnClick="btn_IamgeGalley_Click1" ImageUrl='<%# String.Format("product_image/album/{0}/{1}", Eval("AlbumID"), Eval("PreviewUrl"))%>' data-toggle="modal" data-target="#myModal"
                                     
                                    >
                                    <img src='<%# String.Format("product_image/album/{0}/{1}", Eval("AlbumID"), Eval("PreviewUrl"))%>' alt="" width="90">
                                    </asp:LinkButton>--%>
                              

                                   
                                <a id="A2" data-toggle="modal" data-target='<%#String.Format("#myModal{0}", Eval("AlbumID"))%>' runat="server">
                                    <img src='<%# String.Format("product_image/album/{0}/{1}", Eval("AlbumID"), Eval("PreviewUrl"))%>' alt="" width="90">

                                   </a>




                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<!-- Modal --><div class="modal hide fade" id='<%#String.Format("myModal{0}", Eval("AlbumID"))%>' tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="display: none" aria-hidden="true" >
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h4 class="modal-title" id="myModalLabel"><%#Eval("AlbumName")%></h4>
      </div>
      <div class="modal-body">
       



         

           <div id="MainContent">

	
	
	<ul id="Gallery" class="gallery">
		

        <asp:Literal ID="lit_imageGallery" runat="server" Text='<%#ImageItem(Eval("AlbumID"))%>'  />

		<%--<li><a href="images/full/001.jpg"><img src="images/thumb/001.jpg" alt="Image 001" /></a></li>
		<li><a href="images/full/002.jpg"><img src="images/thumb/002.jpg" alt="Image 002" /></a></li>
		<li><a href="images/full/003.jpg"><img src="images/thumb/003.jpg" alt="Image 003" /></a></li>
		<li><a href="images/full/004.jpg"><img src="images/thumb/004.jpg" alt="Image 004" /></a></li>
		<li><a href="images/full/005.jpg"><img src="images/thumb/005.jpg" alt="Image 005" /></a></li>
		<li><a href="images/full/006.jpg"><img src="images/thumb/006.jpg" alt="Image 006" /></a></li>
		<li><a href="images/full/007.jpg"><img src="images/thumb/007.jpg" alt="Image 007" /></a></li>
		<li><a href="images/full/008.jpg"><img src="images/thumb/008.jpg" alt="Image 008" /></a></li>
		<li><a href="images/full/009.jpg"><img src="images/thumb/009.jpg" alt="Image 009" /></a></li>
		<li><a href="images/full/010.jpg"><img src="images/thumb/010.jpg" alt="Image 010" /></a></li>
		<li><a href="images/full/011.jpg"><img src="images/thumb/011.jpg" alt="Image 011" /></a></li>
		<li><a href="images/full/012.jpg"><img src="images/thumb/012.jpg" alt="Image 012" /></a></li>
		<li><a href="images/full/013.jpg"><img src="images/thumb/013.jpg" alt="Image 013" /></a></li>
		<li><a href="images/full/014.jpg"><img src="images/thumb/014.jpg" alt="Image 014" /></a></li>
		<li><a href="images/full/015.jpg"><img src="images/thumb/015.jpg" alt="Image 015" /></a></li>
		<li><a href="images/full/016.jpg"><img src="images/thumb/016.jpg" alt="Image 016" /></a></li>
		<li><a href="images/full/017.jpg"><img src="images/thumb/017.jpg" alt="Image 017" /></a></li>
		<li><a href="images/full/018.jpg"><img src="images/thumb/018.jpg" alt="Image 018" /></a></li>--%>
		
	</ul>
	
</div>	











      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal" >Close</button>
        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

















                               <%--   <a id="A1" href='<%# String.Format("ShowProductImage.aspx?aID={0}&iframe=true&width=1200&height=900", Eval("AlbumID"))%>' rel="prettyPhoto[iframes]" runat="server" onclick="ImageGallery_Click">
                                    <img src='<%# String.Format("product_image/album/{0}/{1}", Eval("AlbumID"), Eval("PreviewUrl"))%>' alt="" width="90">

                                   </a>--%>


                              

                                <%--<asp:Literal runat="server" ID="lit_imageitems" Text='<%# ImageItem(Eval("Url"), Eval("ProductName"), Eval("ProductID"))%>' />--%>

                                

                            </div>
                                <div class="inside">

                                    <hgroup>

                                        <h2><%# Eval("AlbumName")%></h2>
                                        <%--<h2>Boxing</h2>--%>

                                    </hgroup>

                                        <div class="entry-content">

                                        <%--<p>作 者 : <%# Eval("Author")%>  相 機 型 號 : <%# Eval("CameraModel")%></p>--%>
                                        <%--<p>作 者 : Henry Mak  相 機 型 號 : Canon EOS-1D X</p>--%>

                                        <%--<a class="more-link" href="http://www.hkspa.org/showGallery.php?L=C&albumID=1028&s=1&Page=0&id=201989">view project</a> --%>
                                            <a class="more-link" href="<%# String.Format("Portfolio_Group.aspx?aID={0}", Eval("AlbumID"))%>">view project</a>
                                        </div>
                                </div>
                        </div>
                     




                  

                </ItemTemplate>

              <EmptyDataTemplate>

                  <asp:Label runat="server" ID="lbl_nodata">No photo on this category</asp:Label>

              </EmptyDataTemplate>


            </asp:ListView>
              </div>



           <!-- /.row -->



      </div>
      <!-- /.container -->
    </section>


    </asp:Content>