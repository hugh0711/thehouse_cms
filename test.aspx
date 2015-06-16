<%@ Page Title="" Language="VB" MasterPageFile="~/master/MainMasterPage.master" AutoEventWireup="false" CodeFile="test.aspx.vb" Inherits="test" %>

<%@ Register src="control/UnitSelectionControl.ascx" tagname="CategorySelectionControl" tagprefix="uc1" %>




<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">



    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

     <%--touchSwipe --%>
    <link href="http://twitter.github.com/bootstrap/assets/js/google-code-prettify/prettify.css" rel="stylesheet" />
    <link href="./css/touchSwipe/main.css" type="text/css" rel="stylesheet" />

     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
     <script type="text/javascript" src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js"></script>
    <script type="text/javascript" src="js/touchSwipe/jquery.touchSwipe.min.js"></script>
        <script type="text/javascript" src="js/touchSwipe/main.js"></script>

    








    <div id="div_mainPage" class="box">

    

 

        <script id='code_1'>
            $(function () {
                $("#div_mainPage").swipe({
                    swipeStatus: function (event, phase, direction, distance, duration, fingers) {

                        var str = "<h4>Swipe Phase : " + phase + "<br/>";
                        str += "Direction from inital touch: " + direction + "<br/>";
                        str += "Distance from inital touch: " + distance + "<br/>";
                        str += "Duration of swipe: " + duration + "<br/>";
                        str += "Fingers used: " + fingers + "<br/></h4>";

                        //Here we can check the:
                        //phase : 'start', 'move', 'end', 'cancel'
                        //direction : 'left', 'right', 'up', 'down'
                        //distance : Distance finger is from initial touch point in px
                        //duration : Length of swipe in MS 
                        //fingerCount : the number of fingers used

                        if (phase != "cancel" && phase != "end") {
                            if (duration < 5000)
                                str += "Under maxTimeThreshold.<h3>Swipe handler will be triggered if you release at this point.</h3>"
                            else
                                str += "Over maxTimeThreshold. <h3>Swipe handler will be canceled if you release at this point.</h3>"

                            if (distance < 200)
                                str += "Not yet reached threshold.  <h3>Swipe will be canceled if you release at this point.</h3>"
                            else
                                str += "Threshold reached <h3>Swipe handler will be triggered if you release at this point.</h3>"
                        }

                        if (phase == "cancel")
                            str += "<br/>Handler not triggered. <br/> One or both of the thresholds was not met "
                        if (phase == "end")
                            str += "<br/>Handler was triggered."

                        $("#test").html(str);

                        if (fingers == 2 && direction == "up") {
                            window.location = "#top_menu";
                        }
                    },
                    threshold: 200,
                    maxTimeThreshold: 5000,
                    fingers: 'all'
                });
            });
			</script>


   

    <br />
  
         <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br />

        <asp:Label ID="lbl_test" runat="server" Text="Test content"/>
        </div>
    
             
</asp:Content>


