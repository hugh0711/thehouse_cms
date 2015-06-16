<%@ Page Language="VB" AutoEventWireup="false" CodeFile="p_2.aspx.vb" Inherits="p_2" MasterPageFile="~/master/MainMasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <asp:HiddenField runat="server" ID="hfdCatID" />


    <!-- Styles -->
    <link href="css/hkspa/bootstrap.css" rel="stylesheet">
    <link href="css/hkspa/style.css" rel="stylesheet">
    <link rel='stylesheet' id='prettyphoto-css'  href="css/hkspa/prettyPhoto.css" type='text/css' media='all'>
    <link href="css/hkspa/fontello.css" type="text/css" rel="stylesheet">



      <!-- Loading the javaScript at the end of the page -->
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/jquery.prettyPhoto.js"></script>
    <script type="text/javascript" src="js/site.js"></script>


    <!--ANALYTICS CODE-->
	<script type="text/javascript">
	    var _gaq = _gaq || [];
	    _gaq.push(['_setAccount', 'UA-29231762-1']);
	    _gaq.push(['_setDomainName', 'dzyngiri.com']);
	    _gaq.push(['_trackPageview']);

	    (function () {
	        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
	        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
	        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
	    })();
	</script>




     <!--******************** Portfolio Section ********************-->
    
    <!--******************** Portfolio Section ********************-->
    <section id="portfolio" class="single-page scrollblock">
      <div class="container">
        <h1 id="folio-headline"><img src="images/hkspaimg/portfolio/pofo.png"></h1>


          <asp:SqlDataSource runat="server" ID="dsFeature" 
                ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                
                
                SelectCommand="SELECT [ProductID], [ProductName],[Author],[CameraModel],[Url] FROM [view_ProductImage] WHERE ([ProductID] = 838) or ([ProductID] = 793) or ([ProductID] = 794) or ([ProductID] = 772) or ([ProductID] = 773) ">
                
              <SelectParameters>
                   <%--<asp:ControlParameter ControlID="hfdCatID" Name="CategoryID" 
                        PropertyName="Value"/>
                    <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />--%>
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

                                <%--<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# String.Format("~/portfolio.aspx?id={0}", Eval("ProductID"))%>'
                            ImageUrl='<%# String.Format("~/product_image/product/{0}", Eval("Url"))%>'  rel="prettyPhoto" />--%>



                                 <script type="text/javascript" charset="utf-8">
                                     $(document).ready(function () {
                                         $("area[rel^='prettyPhoto']").prettyPhoto();
                                     });
                                </script>
                                <script src="js/jquery.js" type="text/javascript" charset="utf-8"></script>
                                <link rel="stylesheet" href="css/hkspa/prettyPhoto.css" type="text/css" media="screen" charset="utf-8" />
                                <script src="js/nav/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>


                                <a href="<%# String.Format("product_image/product/{0}", Eval("Url"))%>" rel="prettyPhoto[pp_gal]" title="You can add caption to pictures.">
                                    <img src="<%# String.Format("product_image/product/{0}", Eval("Url"))%>" width="60" height="60" alt="ALT Content" />

                                </a>



                                <%--<a href="<%# String.Format("/product_image/product/{0}", Eval("Url"))%>" rel="prettyPhoto"><img src="<%# String.Format("/product_image/product/{0}", Eval("Url"))%>" alt=""></a>--%> 

                            </div>
                                <div class="inside">

                                    <hgroup>

                                        <h2><%# Eval("ProductName") %></h2>
                                        <%--<h2>Boxing</h2>--%>

                                    </hgroup>

                                        <div class="entry-content">

                                        <p>作 者 : <%# Eval("Author")%>  相 機 型 號 : <%# Eval("CameraModel")%></p>
                                        <%--<p>作 者 : Henry Mak  相 機 型 號 : Canon EOS-1D X</p>--%>

                                        <%--<a class="more-link" href="http://www.hkspa.org/showGallery.php?L=C&albumID=1028&s=1&Page=0&id=201989">view project</a> --%>

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
