<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default"  MasterPageFile="~/master/MainMasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <asp:HiddenField runat="server" ID="hfd_tag" />



    <style type="text/css">


        /* ****************List view pagging control******** */
    .PagerButtonCSS  
        {  
            color:Olive;  
            height:35px;  
            font-weight:bold;  
            font-family:Comic Sans MS;  
            }      
        .NumericButtonCSS  
        {  
            font-size:x-large;  
            font-family:Courier New;  
            color:SaddleBrown;  
            font-family:Comic Sans MS;  
            font-weight:bold;  
            }    
        .CurrentPageLabelCSS  
        {  
            font-size:xx-large;  
            font-family:Courier New;  
            color:Green;  
            font-weight:bold;  
            }   
        .NextPreviousButtonCSS  
        {  
            font-size:large;  
            font-family:Courier New;  
            color:Green;  
            font-weight:bold;  
            }                         




	</style>


   



 <div class="container demo-2">
		
			<!-- Codrops top bar --><!--/ Codrops top bar -->

           <!-- <header class="clearfix"></header>-->

            <div id="slider" class="sl-slider-wrapper">

				<div class="sl-slider">
				
					<div class="sl-slide" data-orientation="horizontal" data-slice1-rotation="-25" data-slice2-rotation="-25" data-slice1-scale="2" data-slice2-scale="2">
						<div class="sl-slide-inner">
							<div class="bg-img bg-img-1"></div>
							<h2>A bene placito.</h2>
							<blockquote><p>You have just dined, and however scrupulously the slaughterhouse is concealed in the graceful distance of miles, there is complicity.</p><cite>Ralph Waldo Emerson</cite></blockquote>
						</div>
					</div>
					
					<div class="sl-slide" data-orientation="vertical" data-slice1-rotation="10" data-slice2-rotation="-15" data-slice1-scale="1.5" data-slice2-scale="1.5">
						<div class="sl-slide-inner">
							<div class="bg-img bg-img-2"></div>
							<h2>Regula aurea.</h2>
							<blockquote><p>Until he extends the circle of his compassion to all living things, man will not himself find peace.</p><cite>Albert Schweitzer</cite></blockquote>
						</div>
					</div>
					
					<div class="sl-slide" data-orientation="horizontal" data-slice1-rotation="3" data-slice2-rotation="3" data-slice1-scale="2" data-slice2-scale="1">
						<div class="sl-slide-inner">
							<div class="bg-img bg-img-3"></div>
							<h2>Dum spiro, spero.</h2>
							<blockquote><p>Thousands of people who say they 'love' animals sit down once or twice a day to enjoy the flesh of creatures who have been utterly deprived of everything that could make their lives worth living and who endured the awful suffering and the terror of the abattoirs.</p><cite>Dame Jane Morris Goodall</cite></blockquote>
						</div>
					</div>
					
					<div class="sl-slide" data-orientation="vertical" data-slice1-rotation="-5" data-slice2-rotation="25" data-slice1-scale="2" data-slice2-scale="1">
						<div class="sl-slide-inner">
							<div class="bg-img bg-img-4"></div>
							<h2>Donna nobis pacem.</h2>
							<blockquote><p>The human body has no more need for cows' milk than it does for dogs' milk, horses' milk, or giraffes' milk.</p><cite>Michael Klaper M.D.</cite></blockquote>
						</div>
					</div>
					
					<div class="sl-slide" data-orientation="horizontal" data-slice1-rotation="-5" data-slice2-rotation="10" data-slice1-scale="2" data-slice2-scale="1">
						<div class="sl-slide-inner">
							<div class="bg-img bg-img-5"></div>
							<h2>Acta Non Verba.</h2>
							<blockquote><p>I think if you want to eat more meat you should kill it yourself and eat it raw so that you are not blinded by the hypocrisy of having it processed for you.</p><cite>Margi Clarke</cite></blockquote>
						</div>
					</div>
				</div><!-- /sl-slider -->

				<nav id="nav-dots" class="nav-dots">
					<span class="nav-dot-current"></span>
					<span></span>
					<span></span>
					<span></span>
					<span></span>
				</nav>

			</div><!-- /slider-wrapper -->

	 <!-- <div class="content-wrapper">
				<h2>About this slider</h2>
				<p>The Slit Slider is a slideshow with a twist: the idea is to slice open the current slide when navigating to the next or previous one. Using jQuery and CSS animations we can create unique slide transitions for the content elements. </p>
		</div>-->
            

        
        
        
      <section id="feature">
       


<div id="cbp-so-scroller" class="cbp-so-scroller" style="margin-top:0;">
				<section class="cbp-so-section">
					<article class="cbp-so-side cbp-so-side-left">
						<h2>This is Love</h2>
						<p>Fruitcake toffee jujubes. Topping biscuit sesame snaps jelly caramels jujubes tiramisu fruitcake. Marzipan tart lemon drops chocolate sesame snaps jelly beans.</p>
					</article>
					<figure class="cbp-so-side cbp-so-side-right">
						<img src="images/onscroll1.png" alt="img01" width="600px">
					</figure>
				</section>
				<section class="cbp-so-section">
					<figure class="cbp-so-side cbp-so-side-left" style="padding-top:0;">
						<img src="images/onscroll1.png" alt="img01" width="600px">
					</figure>
					<article class="cbp-so-side cbp-so-side-right" style="margin-top: 60px;">
						<h2>Plum caramels</h2>
						<p>Lollipop powder danish sugar plum caramels liquorice sweet cookie. Gummi bears caramels gummi bears candy canes cheesecake sweet roll icing dragée. Gummies jelly-o tart. Cheesecake unerdwear.com candy canes apple pie halvah chocolate tiramisu.</p>
					</article>
				</section>
				<br/><br/><br/><br/>
			</div>

        </div>

</section>




    <script type="text/javascript">
        $(function () {

            var Page = (function () {

                var $nav = $('#nav-dots > span'),
                    slitslider = $('#slider').slitslider({
                        onBeforeChange: function (slide, pos) {

                            $nav.removeClass('nav-dot-current');
                            $nav.eq(pos).addClass('nav-dot-current');

                        }
                    }),

                    init = function () {

                        initEvents();

                    },
                    initEvents = function () {

                        $nav.each(function (i) {

                            $(this).on('click', function (event) {

                                var $dot = $(this);

                                if (!slitslider.isActive()) {

                                    $nav.removeClass('nav-dot-current');
                                    $dot.addClass('nav-dot-current');

                                }

                                slitslider.jump(i + 1);
                                return false;

                            });

                        });

                    };

                return { init: init };

            })();

            Page.init();

            /**
             * Notes: 
             * 
             * example how to add items:
             */

            /*
            
            var $items  = $('<div class="sl-slide sl-slide-color-2" data-orientation="horizontal" data-slice1-rotation="-5" data-slice2-rotation="10" data-slice1-scale="2" data-slice2-scale="1"><div class="sl-slide-inner bg-1"><div class="sl-deco" data-icon="t"></div><h2>some text</h2><blockquote><p>bla bla</p><cite>Margi Clarke</cite></blockquote></div></div>');
            
            // call the plugin's add method
            ss.add($items);

            */

        });
		</script>


    <script>
        new cbpScroller(document.getElementById('cbp-so-scroller'));
		</script>



     <!--nav-->    <div id="headerwrap2">
      
        <h1><span>Legend!</span> We make web a beautiful place.</h1>
        <div class="container">
          <div class="row">
            <div class="span12">
              <div class="container">
                <div class="row">
                  <div class="span12">
                    <h2>Signup for our Newsletter to be updated</h2>
                    <input type="text" name="your-email" placeholder="you@yourmail.com" class="cform-text" size="40" title="your email">
                    <input type="submit" value="Notify me" class="cform-submit">
                  </div>
                </div>
                <div class="row">
                  <div class="span12">
                    <ul class="icon">
                      <li><a href="#" target="_blank"><i class="icon-pinterest-circled"></i></a></li>
                      <li><a href="#" target="_blank"><i class="icon-facebook-circled"></i></a></li>
                      <li><a href="#" target="_blank"><i class="icon-twitter-circled"></i></a></li>
                      <li><a href="#" target="_blank"><i class="icon-gplus-circled"></i></a></li>
                      <li><a href="#" target="_blank"><i class="icon-skype-circled"></i></a></li>
                    </ul>
                  </div>
                </div>
              </div>
              <h2>&nbsp;</h2>
            </div>
          </div>
        </div>
      
    </div>


    <section  id="services">
        
    <section class="col-3 color ss-style-zigzag" >
    

        

        
       
            

    	<div class="container demo-3"style="background: url('images/black2.png') repeat-x 0 0;">
			<!-- Top Navigation -->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

               
                <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ListView1" EventName="PagePropertiesChanged" />
                           <asp:AsyncPostBackTrigger ControlID="ListView1" EventName="PreRender" />
                        </Triggers>

   <ContentTemplate>
            
                			<section class="color-1" align="center">
                              最新消息 
                                &nbsp
                                <asp:Button Text="全部" BorderStyle="None" ID="btn_all" CssClass="css_btn_class" runat="server"/>
                                <asp:Button Text="香港" BorderStyle="None" ID="btn_hk_tag" CssClass="css_btn_class" runat="server"/>
                                <asp:Button Text="中國" BorderStyle="None" ID="btn_cn_tag" CssClass="css_btn_class" runat="server"/>
                                <asp:Button Text="馬來西亞" BorderStyle="None" ID="btn_ma_tag" CssClass="css_btn_class" runat="server"/>
                            <br/><br/>
				
			</section>
            
			



             <asp:SqlDataSource runat="server" ID="dsFeature" 
                ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 

               
              SelectCommand="SELECT Top 9 p.[ProductName],p.[Url],p.[ProductID],t.[TagName] FROM [view_ProductImageTag] p,[view_Tag] t WHERE (p.[TagID]=t.[TagID]) 
                 and (t.[Lang]='zh-hk') and (p.[Enabled] = @Enabled) and p.[FunctionID]=3 ORDER BY p.[SortOrder]">
              
              <SelectParameters>
<%--SelectCommand="SELECT Top 9 [ProductID], [ProductName],[Author],[CameraModel],[Url],[CategoryID] FROM [view_ProductImage] WHERE ([Enabled] = @Enabled) and [FunctionID]=2 ORDER BY [ModifyDate] DESC">--%>
                   <%-- <asp:ControlParameter ControlID="hfdVideoFunctionID" Name="FunctionID" 
                        PropertyName="Value" Type="Int32" />--%>
                    <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
<%--                  <asp:ControlParameter ControlID="hfd_tag" Name="Tag" 
                                            PropertyName="Value" Type="String" />--%>
                </SelectParameters>
            </asp:SqlDataSource>

          

			<ul class="grid cs-style-3">



           
                              
                        

                <asp:ListView runat="server" ID="ListView1" DataSourceID="dsFeature" OnPagePropertiesChanging="ListView1_PagePropertiesChanging" >
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>

				<li>
					<figure>
						<%--<img src='<%# String.Format("product_image/product_original/{0}", IIf(Eval("Url") Is Nothing Or Eval("Url").ToString.Length = 0, "no_image.png", Eval("Url")))%>' alt='<%# Eval("ProductName")%>' width="400" height="300">--%>
						
                        <a href='<%# String.Format("product_image/product_original/{0}", IIf(Eval("Url") Is Nothing Or Eval("Url").ToString.Length = 0, "no_image.png", Eval("Url")))%>' 
                           class="fancybox" rel="group" title='<%# Eval("ProductName")%>'>

                            <img src='<%# String.Format("product_image/product_thumbnail/{0}", IIf(Eval("Url") Is Nothing Or Eval("Url").ToString.Length = 0, "no_image.png", Eval("Url")))%>' alt='<%# Eval("ProductName")%>' 
                                width="300" height="300" alt='<%# Eval("ProductName")%>' />

                        </a>
                        
                        <asp:PlaceHolder runat="server" ID="place_details" Visible='<%#checkMobileOrNot() %>'>
                        <figcaption>
							<h3><%# Eval("ProductName")%></h3>
							<span>Jacob Cummings</span>


                            <%--<a class="fancybox" data-fancybox-type="iframe" href="http://samplepdf.com/sample.pdf" >Test pdf</a>--%>

                            <asp:HyperLink runat="server" ID="link_detail" class="various" 
                                data-fancybox-type="iframe"  Visible='<%# CheckDescAndFile(Eval("ProductID"))%>'
                                NavigateUrl='<%# String.Format("detail.aspx?id={0}", Eval("ProductID"))%>' >Detail</asp:HyperLink>
                            <%--<a class="various" data-fancybox-type="iframe" href='<%# String.Format("detail.aspx?id={0}", Eval("ProductID"))%>'>Detail</a>--%>

						</figcaption>
                    </asp:PlaceHolder>

                         <asp:PlaceHolder runat="server" ID="place_Mobile" Visible='<%# Not checkMobileOrNot()%>'>
                             <div>

                                 <h3><%# Eval("ProductName")%></h3>

                                  <asp:HyperLink runat="server" ID="link_outside" class="various" 
                                data-fancybox-type="iframe"  Visible='<%# CheckDescAndFile(Eval("ProductID"))%>'
                                NavigateUrl='<%# String.Format("detail.aspx?id={0}", Eval("ProductID"))%>' >Detail</asp:HyperLink>
                             </div>


                         </asp:PlaceHolder>


					</figure>
				</li>



                </ItemTemplate>
            </asp:ListView>









                        <%--/****************Listview pagging*************************/--%>
                                <div align="center">

                             <asp:Literal ID="lit_page" runat="server" />


                                    <br />
                                <asp:DataPager ID="DataPagerProducts" runat="server" PagedControlID="ListView1" 
                    PageSize="6" OnPreRender="DataPagerProducts_PreRender"  >
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ButtonCssClass="PagerButtonCSS"   />

                        <asp:NumericPagerField 
                            NumericButtonCssClass="NumericButtonCSS"  
                            NextPreviousButtonCssClass="NextPreviousButtonCSS"  
                            CurrentPageLabelCssClass="CurrentPageLabelCSS"   />

                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True"  ShowPreviousPageButton="False" ButtonCssClass="PagerButtonCSS"/>

                    </Fields>

                 <%--<Fields>


                    <asp:NextPreviousPagerField RenderDisabledButtonsAsLabels="True" 
                        ShowFirstPageButton="True" ShowLastPageButton="True" />

                </Fields>--%>

                </asp:DataPager>
                </div>


				
			</ul>


</ContentTemplate>
</asp:UpdatePanel>  

             
		</div>
    
    
           
    
    
    
  
	  <div class="column">
					<span class="icon icon-upload"></span>
				</div>



			</section>
       
        </section>


        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

        
       
        
        
        
        
        
    

    
    
    
    



</div>
       


       
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

        
       
        
        
        
        
        
    

    
    
    
    



</asp:Content>
