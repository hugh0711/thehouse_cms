<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowProductImage.aspx.vb" Inherits="ShowProductImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

 
    <meta charset="UTF-8" />
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"> 
        <meta name="description" content="Elastislide - A Responsive Image Carousel" />
        <meta name="keywords" content="carousel, jquery, responsive, fluid, elastic, resize, thumbnail, slider" />
		<meta name="author" content="Codrops" />
		<link rel="shortcut icon" href="../favicon.ico"> 
        <link rel="stylesheet" type="text/css" href="css/show_image/demo.css" />
		<link rel="stylesheet" type="text/css" href="css/show_image/elastislide.css" />
		<link rel="stylesheet" type="text/css" href="css/show_image/custom.css" />
		<script src="js/show_image/modernizr.custom.17475.js"></script>

    




</head>
<body>
    <form id="form1" runat="server">


<asp:HiddenField runat="server" ID="hfdAlbumID" />



    <div>
 


        <div class="container demo-3">
			

            <div class="main">
				

				<div class="fixed-bar">


                    <div class="image-preview">
						<img id="preview" src="" width="100%" />
					</div>



					<!-- Elastislide Carousel -->
					<ul id="carousel" class="elastislide-list" style="max-height:130px;">
						<%--<li data-preview="images/show_image/large/4.jpg"><a href="#"><img src="images/show_image/small/4.jpg" alt="image04" /></a></li>
						<li data-preview="images/show_image/large/5.jpg"><a href="#"><img src="images/show_image/small/5.jpg" alt="image05" /></a></li>
						<li data-preview="images/show_image/large/6.jpg"><a href="#"><img src="images/show_image/small/6.jpg" alt="image06" /></a></li>
						<li data-preview="images/show_image/large/7.jpg"><a href="#"><img src="images/show_image/small/7.jpg" alt="image07" /></a></li>
						<li data-preview="images/show_image/large/11.jpg"><a href="#"><img src="images/show_image/small/11.jpg" alt="image11" /></a></li>
						<li data-preview="images/show_image/large/12.jpg"><a href="#"><img src="images/show_image/small/12.jpg" alt="image12" /></a></li>
						<li data-preview="images/show_image/large/13.jpg"><a href="#"><img src="images/show_image/small/13.jpg" alt="image13" /></a></li>
						<li data-preview="images/show_image/large/14.jpg"><a href="#"><img src="images/show_image/small/14.jpg" alt="image14" /></a></li>
						<li data-preview="images/show_image/large/15.jpg"><a href="#"><img src="images/show_image/small/15.jpg" alt="image15" /></a></li>
						<li data-preview="images/show_image/large/16.jpg"><a href="#"><img src="images/show_image/small/16.jpg" alt="image16" /></a></li>
						<li data-preview="images/show_image/large/17.jpg"><a href="#"><img src="images/show_image/small/17.jpg" alt="image17" /></a></li>
						<li data-preview="images/show_image/large/18.jpg"><a href="#"><img src="images/show_image/small/18.jpg" alt="image18" /></a></li>
						<li data-preview="images/show_image/large/19.jpg"><a href="#"><img src="images/show_image/small/19.jpg" alt="image19" /></a></li>
						<li data-preview="images/show_image/large/20.jpg"><a href="#"><img src="images/show_image/small/20.jpg" alt="image20" /></a></li>
						<li data-preview="images/show_image/large/1.jpg"><a href="#"><img src="images/show_image/small/1.jpg" alt="image01" /></a></li>
						<li data-preview="images/show_image/large/2.jpg"><a href="#"><img src="images/show_image/small/2.jpg" alt="image02" /></a></li>
						<li data-preview="images/show_image/large/3.jpg"><a href="#"><img src="images/show_image/small/3.jpg" alt="image03" /></a></li>
						<li data-preview="images/show_image/large/8.jpg"><a href="#"><img src="images/show_image/small/8.jpg" alt="image08" /></a></li>
						<li data-preview="images/show_image/large/9.jpg"><a href="#"><img src="images/show_image/small/9.jpg" alt="image09" /></a></li>
						<li data-preview="images/show_image/large/10.jpg"><a href="#"><img src="images/show_image/small/10.jpg" alt="image10" /></a></li>--%>


                     


                        <asp:Literal runat="server" ID="lit_imageItem" />

					</ul>
					<!-- End Elastislide Carousel -->

					
				</div>

				

			</div>
		</div>


        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
		<script type="text/javascript" src="js/show_image/jquerypp.custom.js"></script>
		<script type="text/javascript" src="js/show_image/jquery.elastislide.js"></script>
		<script type="text/javascript">

		    // example how to integrate with a previewer

		    var current = 0,
				$preview = $('#preview'),
				$carouselEl = $('#carousel'),
				$carouselItems = $carouselEl.children(),
				carousel = $carouselEl.elastislide({
				    current: current,
				    minItems: 4,
				    onClick: function (el, pos, evt) {

				        changeImage(el, pos);
				        evt.preventDefault();

				    },
				    onReady: function () {

				        changeImage($carouselItems.eq(current), current);

				    }
				});

		    function changeImage(el, pos) {

		        $preview.attr('src', el.data('preview'));
		        $carouselItems.removeClass('current-img');
		        el.addClass('current-img');
		        carousel.setCurrent(pos);

		    }

		</script>

    </div>
    </form>
</body>
</html>
