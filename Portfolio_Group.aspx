<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Portfolio_Group.aspx.vb" Inherits="Portfolio_Group"  MasterPageFile="~/master/MainMasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <asp:HiddenField runat="server" ID="hfdAlbumID" />


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
                
                
                SelectCommand="SELECT [PhotoName],[AlbumName],[Author],[camera_model],[iso_speed] FROM [view_AlbumPhotoInfo] WHERE ([Enabled] = @Enabled) and [AlbumID]= @AlbumID ">
                <SelectParameters>
                   <asp:ControlParameter ControlID="hfdAlbumID" Name="AlbumID" 
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

                               

                               <%-- <a href="<%# String.Format("product_image/product_original/{0}", Eval("Url"))%>"  rel="prettyPhoto">

                                    <img src="<%# String.Format("product_image/product/{0}", Eval("Url"))%>" alt=""></a>
                                </a>--%>

                                <%--<script type="text/javascript" charset="utf-8">
                                    $(document).ready(function () {
                                        $("area[rel^='prettyPhoto']").prettyPhoto();
                                    });
                                </script>

                                <script src="js/jquery.js" type="text/javascript" charset="utf-8"></script>
                                <link rel="stylesheet" href="css/hkspa/prettyPhoto.css" type="text/css" media="screen" charset="utf-8" />
                                <script src="js/nav/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>--%>


                                <ul id="Gallery" class="gallery">

                                <li>

                                    <a href='<%# String.Format("product_image/album/{0}/{1}", hfdAlbumID.Value, Eval("PhotoName"))%>'>
                                    <img src='<%#String.Format("product_image/album/{0}/tb/{1}", hfdAlbumID.Value, Eval("PhotoName"))%>' alt='<%#String.Format("Photo By {0}", Eval("Author"))%>'/>

                                    </a>

                                </li>

                                </ul>


                               <%-- <a href='<%# String.Format("product_image/album/{0}/{1}", hfdAlbumID.Value, Eval("PhotoName"))%>' rel="prettyPhoto[pp_gal]" title='<%# Eval("AlbumName")%>'><img src='<%# String.Format("product_image/album/{0}/{1}", hfdAlbumID.Value, Eval("PhotoName"))%>'  width="60" height="60" alt='<%# Eval("AlbumName")%>' /></a>--%>

                                <%--<asp:Literal runat="server" ID="lit_imageitems" Text='<%# ImageItem(String.Format("product_image/product/{0}", Eval("Url")), Eval("CategoryID"), Eval("ProductName"))%>' />--%>

                               


                            </div>
                                <div class="inside">

                                    <hgroup>

                                        <%--<h2><%# Eval("ProductName")%></h2>--%>
                                        <%--<h2>Boxing</h2>--%>

                                    </hgroup>

                                        <div class="entry-content">

                                        <p>作 者 : <%# Eval("Author")%>  <br />相 機 型 號 : <%# Eval("camera_model")%><br />I S O : <%# Eval("iso_speed")%></p>
                                        <%--<p>作 者 : Henry Mak  相 機 型 號 : Canon EOS-1D X</p>--%>

                                        <%--<a class="more-link" href="<%# String.Format("portfolio.aspx?cID={0}", Eval("CategoryID"))%>">view project</a>--%> 

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
