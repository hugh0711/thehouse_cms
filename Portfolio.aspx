<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Portfolio.aspx.vb" Inherits="Portfolio"   MasterPageFile="~/master/MainMasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <asp:HiddenField runat="server" ID="hfdCatID" />


    




     <!--******************** Portfolio Section ********************-->
    
    <!--******************** Portfolio Section ********************-->
    <section id="portfolio" class="single-page scrollblock">
      <div class="container">
        <h1 id="folio-headline"><img src="images/hkspaimg/portfolio/pofo.png"></h1>


          <asp:SqlDataSource runat="server" ID="dsFeature" 
                ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                
                
                SelectCommand="SELECT [CategoryName],[Url],[CategoryID],[FunctionID] FROM [view_Category] WHERE ([Enabled] = @Enabled) and [ParentID]= @CategoryID ORDER BY [SortOrder]">
                
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

                   
                        <div class="span4" style="padding-bottom: 10px;">
                            <div class="mask2"> 

                               

                                <script type="text/javascript" charset="utf-8">
                                     $(document).ready(function () {
                                         $("area[rel^='prettyPhoto']").prettyPhoto();
                                     });
                                </script>

                                <script src="js/jquery.js" type="text/javascript" charset="utf-8"></script>
                                <link rel="stylesheet" href="css/hkspa/prettyPhoto.css" type="text/css" media="screen" charset="utf-8" />
                                <script src="js/nav/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>


                                <a class="more-link" href='<%# String.Format("Portfolio_Sub.aspx?cID={0}#portfolio", Eval("CategoryID"))%>'>
                                    <img src='<%# String.Format("product_image/category/{0}", Eval("Url"))%>' alt="" width="90">

                                </a><%--<asp:Literal runat="server" ID="lit_imageitems" Text='<%# ImageItem(Eval("Url"), Eval("ProductName"), Eval("ProductID"))%>' />--%></div>
                                <div class="inside" style="text-align: center;padding: 10px;">

                                    <hgroup>

                                        <h2><%# Eval("CategoryName") %></h2>
                                        <%--<h2>Boxing</h2>--%>

                                    </hgroup>

                                      <%--  <div class="entry-content">

                                        <%--<p>作 者 : <%# Eval("Author")%>  相 機 型 號 : <%# Eval("CameraModel")%></p>--%>
                                        <%--<p>作 者 : Henry Mak  相 機 型 號 : Canon EOS-1D X</p>--%>

                                        <%--<a class="more-link" href="http://www.hkspa.org/showGallery.php?L=C&albumID=1028&s=1&Page=0&id=201989">view project</a> --%>

                                      <%--  </div>--%>
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