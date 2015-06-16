<%@ Page Title="" Language="VB" MasterPageFile="~/backoffice/master/admin.master" AutoEventWireup="false"
    CodeFile="product.aspx.vb" Inherits="backoffice_product" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="cc2" %>
<%@ Register src="~/control/CategoryPathControl.ascx" tagname="CategoryPathControl" tagprefix="uc3" %>
<%@ Register src="~/control/ProductLangControl.ascx" tagname="ProductLangControl" tagprefix="uc4" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>

<%@ Reference control="~/control/ProductLangControl.ascx"  %>
<script runat="server">

    Protected Sub cblTag_Load(sender As Object, e As EventArgs)

    End Sub
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type = "text/javascript">
        var atLeast = 1
        function Validate() {
            var CHK = document.getElementById("<%=cblTag.ClientID%>");
    var checkbox = CHK.getElementsByTagName("input");
    var counter = 0;
    for (var i = 0; i < checkbox.length; i++) {
        if (checkbox[i].checked) {
            counter++;
        }
    }
    //if (atLeast > counter) {
    //    alert("Please select atleast " + atLeast + " item(s)");
    //    return false;
    //}
    if (counter < atLeast)
    {
        alert("Please select at least " + atLeast + " tag!");
       return false;
     }

    return true;
}
</script>







    <script src="../ckfinder/ckfinder.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function BrowseServer(inputId) {
            //CKFinder.Popup('../ckfinder/', null, null, SetFileField);
            var finder = new CKFinder();
            finder.BasePath = '../ckfinder/';
            finder.SelectFunction = SetFileField;
            finder.SelectFunctionData = inputId;
            finder.Popup();

        }

        function SetFileField(fileUrl, data) {
            $("#" + data["selectFunctionData"]).val(fileUrl);
        }

//        function FCKUpdateLinkedField(id) {
//            try {
//                if (typeof (FCKeditorAPI) == "object") {
//                    FCKeditorAPI.GetInstance(id).UpdateLinkedField();
//                }
//            }
//            catch (err) {
//            }
//        }
    </script>
    
    <style type="text/css">
        td
        {
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<h2>
        <asp:Label runat="server" ID="lblFunctionName"></asp:Label></h2>
    <asp:Label ID="lblProductID" runat="server" Visible="false"></asp:Label>
    <asp:HiddenField runat="server" ID="hfdFunctionID" />
    <asp:HiddenField runat="server" ID="hfdLang" />
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="gp1" ShowSummary="false" ShowMessageBox="true" />
    <div id="admin-content-container">
        <div id="admin-content">
            <table border="0" width="100%">
                <colgroup>
                    <col class="tableCaption" width="100" />
                    <col width="500"/>
                    <col />
                </colgroup>
                <asp:PlaceHolder runat="server" ID="CategoryPlaceHolder">
                        <tr>
                            <td>
                                Category
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnSelectCategory" Text="..." />
                                <uc3:CategoryPathControl ID="CategoryPathControl1" runat="server" />
                                <asp:TextBox runat="server" ID="txtCategoryID" style="display:none;" />
                                <asp:Button runat="server" ID="btnDummy" Visible="false" />
                                <asp:ModalPopupExtender ID="btnSelectCategory_ModalPopupExtender" runat="server" PopupControlID="pnlCategory" BackgroundCssClass="modalBackground"
                                    TargetControlID="btnSelectCategory" CancelControlID="btnCategoryCancel" >
                                </asp:ModalPopupExtender>
                                <asp:Panel runat="server" ID="pnlCategory" CssClass="modalPopup" style="display:none;" >
                                    <b>Select Category:</b><br />
                                    <br />
                                    <asp:Label runat="server" ID="lblParentID" Visible="false"></asp:Label>
                                    <div style="overflow: auto;">
                                        <asp:TreeView ID="TreeView1" runat="server" ClientIDMode="AutoID" ExpandDepth="FullyExpand" Width="250" Height="250" EnableClientScript="true"> 
                                            <Nodes>
                                                <asp:TreeNode Text="Root" Value="0" PopulateOnDemand="true" SelectAction="Expand">
                                                </asp:TreeNode>
                                            </Nodes>
                                        </asp:TreeView>
                                    </div>
                                    <p align="right">
                                    <asp:Button runat="server" ID="btnCategoryCancel" Text="Cancel" /></p>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtCategoryID"
                                    CssClass="errorText" ErrorMessage="Please select category" ForeColor="" ValidationGroup="gp1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                </asp:PlaceHolder>
                
                <asp:PlaceHolder runat="server" ID="ProductCodePlaceHolder">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblProductCode" Text="Code"></asp:Label>
                    
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductCode" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please input product code"
                            ControlToValidate="txtProductCode" ValidationGroup="gp1" EnableClientScript="false"
                            CssClass="errorText" ForeColor=""></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtProductCode"
                            Display="Dynamic" ErrorMessage="Product code is existed. Please use another code" SetFocusOnError="True"
                            ValidationGroup="gp1" CssClass="errorText" ForeColor=""></asp:CustomValidator>
                    </td>
                </tr>
                </asp:PlaceHolder>
                
                <tr>
                    <td colspan="3" style="background-color:#fff;">
                    <br />
                 <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                </asp:TabContainer>
                    <br />
                    </td>
                </tr>

            <asp:PlaceHolder runat="server" ID="DetailsPlaceHolder">
               <%-- <tr>
                    <td>
                        Video (YouTube) URL</td>
                    <td>
                        <asp:TextBox ID="txtMOQ" runat="server" Columns="80"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="請輸入Video ID"
                            ControlToValidate="txtMOQ" ValidationGroup="gp1" CssClass="errorText" Enabled="false"
                            ForeColor=""></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Video (WebTV) URL</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtVideoUrl" runat="server" Columns="80"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="請輸入Video Url"
                            ControlToValidate="txtVideoUrl" ValidationGroup="gp1" CssClass="errorText" Enabled="false" Display="Dynamic"
                            ForeColor=""></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkVideo3D" Text="3D版" />
                    </td>
                </tr>
               <tr>
                    <td>
                        Facebook URL</td>
                    <td>
                        <asp:TextBox ID="txtLeadTime" runat="server" Columns="80"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="請輸入Video ID"
                            ControlToValidate="txtLeadTime" ValidationGroup="gp1" CssClass="errorText" Enabled="false"
                            ForeColor=""></asp:RequiredFieldValidator>
                    </td>
                </tr>--%>

                 <tr>
                    <td>
                        作者</td>
                    <td>
                        <asp:TextBox ID="txtAutor" runat="server" Columns="80"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="請輸入作者名稱"
                            ControlToValidate="txtAutor" ValidationGroup="gp1" CssClass="errorText" Enabled="false"
                            ForeColor=""></asp:RequiredFieldValidator>
                    </td>
                </tr>


                <tr>
                    <td>
                        相機型號</td>
                    <td>
                        <asp:TextBox ID="txtCameraM" runat="server" Columns="80"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="請輸入相機型號"
                            ControlToValidate="txtCameraM" ValidationGroup="gp1" CssClass="errorText" Enabled="false"
                            ForeColor=""></asp:RequiredFieldValidator>
                    </td>
                </tr>


            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="phdDetails2" Visible="false">
                <tr>
                    <td>
                        Size</td>
                    <td>
                        <asp:UpdatePanel runat="server" ID="SizeUpdatePanel" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:ListView runat="server" ID="lvSize" InsertItemPosition="LastItem">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder runat="server" ID="ItemPlaceHolder"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>' Visible="false" />
                                            <asp:Label runat="server" ID="lblSizeID" Text='<%# Bind("SizeID") %>' Visible="false" />
                                            <asp:TextBox runat="server" ID="txtSize" Text='<%# Bind("Size") %>'></asp:TextBox>
                                            <asp:Button runat="server" ID="btnSizeDelete" Text="Delete" CommandName="delete" />
                                        </div>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtSize" Text='<%# Bind("Size") %>'></asp:TextBox>                          
                                            <asp:Button runat="server" ID="btnSizeInsert" Text="Add" CommandName="insert" />
                                        </div>
                                    </InsertItemTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                </asp:PlaceHolder>
                

                <asp:PlaceHolder runat="server" ID="RestaurantPlaceHolder" Visible="False">

                    <tr>
                    <td>
                        Open Hours <br />
                        (24hr Time Sample: 2000)
                    </td>
                    <td>
                        <b>Monday:</b> From<asp:textbox id="txtMonFrom" runat="server" MaxLength="4" Text="1100"></asp:textbox> To<asp:textbox id="txtMonTo" runat="server" MaxLength="4" Text="2000"></asp:textbox><br />
                        <b>Tuesday:</b> From<asp:textbox id="txtTueFrom" runat="server" MaxLength="4" Text="1100"></asp:textbox> To<asp:textbox id="txtTueTo" runat="server" MaxLength="4" Text="2000"></asp:textbox><br />
                        <b>Wednesday:</b> From<asp:textbox id="txtWedFrom" runat="server" MaxLength="4" Text="1100"></asp:textbox> To<asp:textbox id="txtWedTo" runat="server" MaxLength="4" Text="2000"></asp:textbox><br />
                        <b>Thursday:</b> From<asp:textbox id="txtThuFrom" runat="server" MaxLength="4" Text="1100"></asp:textbox> To<asp:textbox id="txtThuTo" runat="server" MaxLength="4" Text="2000"></asp:textbox><br />
                        <b>Friday:</b> From<asp:textbox id="txtFriFrom" runat="server" MaxLength="4" Text="1100"></asp:textbox> To<asp:textbox id="txtFriTo" runat="server" MaxLength="4" Text="2000"></asp:textbox><br />
                        <b>Saturday:</b> From<asp:textbox id="txtSatFrom" runat="server" MaxLength="4" Text="1100"></asp:textbox> To<asp:textbox id="txtSatTo" runat="server" MaxLength="4" Text="2000"></asp:textbox><br />
                        <b>Sunday: </b> From<asp:textbox id="txtSunFrom" runat="server" MaxLength="4" Text="1100"></asp:textbox> To<asp:textbox id="txtSunTo" runat="server" MaxLength="4" Text="2000"></asp:textbox><br />
                    </td>
                    
                </tr>
                </asp:PlaceHolder>

                <asp:PlaceHolder runat="server" ID="ProductDetailPlaceHolder" Visible="False">
                <tr>
                    <td>
                        Price</td>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please input price"
                            ControlToValidate="txtPrice" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor=""></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPrice"
                            ErrorMessage="Invalid price" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Currency"
                            ValidationGroup="gp1" Display="Dynamic" CssClass="errorText" ForeColor=""></asp:CompareValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Vintage</td>
                    <td>
                        <asp:TextBox ID="txtVintage" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please input Vintage"
                            ControlToValidate="txtVintage" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor="" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Grape</td>
                    <td>
                        <asp:TextBox ID="txtGrape" runat="server" Columns="80"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please input Grape"
                            ControlToValidate="txtGrape" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor="" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Alcohol</td>
                    <td>
                        <asp:TextBox ID="txtAlcohol" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please input Alcohol"
                            ControlToValidate="txtAlcohol" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor="" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Region</td>
                    <td>
                        <asp:TextBox ID="txtRegion" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please input Region"
                            ControlToValidate="txtRegion" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor="" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Body</td>
                    <td>
                        <asp:TextBox ID="txtBody" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Please input Body"
                            ControlToValidate="txtBody" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor="" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>


               <tr>
                    <td>
                        Volume</td>
                    <td>
                        <asp:TextBox ID="txtVolume" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Please input Volume"
                            ControlToValidate="txtVolume" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor="" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Winery</td>
                    <td>
                        <asp:TextBox ID="txtWinery" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please input Winery"
                            ControlToValidate="txtWinery" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor="" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>

               <tr>
                    <td>
                        Website</td>
                    <td>
                        <asp:TextBox ID="txtWebsite" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Please input Website"
                            ControlToValidate="txtWebsite" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor="" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        International Ratings</td>
                    <td>
                        <asp:TextBox ID="txtInternationalRatings" runat="server" Columns="80" TextMode="MultiLine" Rows="8"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Please input International Ratings"
                            ControlToValidate="txtInternationalRatings" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor="" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                </asp:PlaceHolder>

                <asp:PlaceHolder runat="server" ID="PricePlaceHolder" Visible="false">
                <tr>
                    <td>
                        Discounted Price</td>
                    <td>
                        <asp:TextBox ID="txtDiscountPrice" runat="server" text="0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="½Ð¿é¤J§é¦©»ù"
                            ControlToValidate="txtDiscountPrice" ValidationGroup="gp1" 
                            Display="Dynamic" CssClass="errorText" ForeColor=""></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtDiscountPrice"
                            ErrorMessage="§é¦©»ù¤£¦Xªk" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Currency"
                            ValidationGroup="gp1" Display="Dynamic" CssClass="errorText" ForeColor=""></asp:CompareValidator>
						<asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="§é¦©»ù¥²¶·¤p©ó»ù®æ" ControlToValidate="txtDiscountPrice" ControlToCompare="txtPrice" Operator="LessThan" ValidationGroup="gp1" Display="Dynamic" CssClass="errorText" ForeColor=""></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--Cost--%></td>
                    <td>
                        <asp:TextBox ID="txtShippingFee0" runat="server" Visible="false" Text="0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="½Ð¿é¤J²£«~¦¨¥»"
                            ControlToValidate="txtShippingFee" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor=""></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtShippingFee"
                            ErrorMessage="²£«~¦¨¥»¤£¦Xªk" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Currency"
                            ValidationGroup="gp1" Display="Dynamic" CssClass="errorText" ForeColor=""></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Base Shipping Fee (Local)</td>
                    <td>
                        <asp:TextBox ID="txtShippingFee" runat="server" Text="0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="½Ð¿é¤J¹B¶O"
                            ControlToValidate="txtShippingFee" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor=""></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtShippingFee"
                            ErrorMessage="¹B¶O¤£¦Xªk" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Currency"
                            ValidationGroup="gp1" Display="Dynamic" CssClass="errorText" ForeColor=""></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Base Shipping Fee (Overseas)</td>
                    <td>
                        <asp:TextBox ID="txtShippingFeeOverseas" runat="server" Text="0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="½Ð¿é¤J¹B¶O"
                            ControlToValidate="txtShippingFeeOverseas" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor=""></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtShippingFeeOverseas"
                            ErrorMessage="¹B¶O¤£¦Xªk" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Currency"
                            ValidationGroup="gp1" Display="Dynamic" CssClass="errorText" ForeColor=""></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Base Shipping Fee (Overseas Express)</td>
                    <td>
                        <asp:TextBox ID="txtShippingFeeOverseasExpress" runat="server" Text="0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="½Ð¿é¤J¹B¶O"
                            ControlToValidate="txtShippingFeeOverseasExpress" ValidationGroup="gp1" Display="Dynamic" 
                            CssClass="errorText" ForeColor=""></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtShippingFeeOverseasExpress"
                            ErrorMessage="¹B¶O¤£¦Xªk" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Currency"
                            ValidationGroup="gp1" Display="Dynamic" CssClass="errorText" ForeColor=""></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Advance Shipping Fee
                    </td>
                    <td>
						<asp:Button ID="btnCreateNewShippingFee" runat="server" Text="Create New Shipping Fee" />
						<br /><br />
						<asp:GridView ID="gvShippingFee" runat="server" AutoGenerateColumns="False" 
							DataSourceID="dsShippingFee">
							<Columns>
								<asp:BoundField DataField="MinQty" HeaderText="Min Qty" 
									SortExpression="MinQty" />
								<asp:BoundField DataField="MaxQty" HeaderText="Max Qty" 
									SortExpression="MaxQty" />
								<asp:BoundField DataField="Local" HeaderText="Local" SortExpression="Local" />
								<asp:BoundField DataField="OverSeas" HeaderText="OverSeas" 
									SortExpression="OverSeas" />
								<asp:BoundField DataField="OverSeasExpress" HeaderText="OverSeasExpress" 
									SortExpression="OverSeasExpress" />
								<asp:TemplateField>
									<ItemTemplate>
										<asp:Button ID="btnShippingEdit" runat="server" Text=" Edit " CommandName="btnShippingEdit" CommandArgument='<%# Eval("ShippingID") %>'/>&nbsp;
										<asp:Button ID="btnShippingDelete" runat="server" Text="Delete" CommandName="btnShippingDelete" CommandArgument='<%# Eval("ShippingID") %>' />
										<asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Delete ?" TargetControlID="btnShippingDelete">
										</asp:ConfirmButtonExtender>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
						<asp:SqlDataSource ID="dsShippingFee" runat="server" 
							ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
							SelectCommand="SELECT [ShippingID], [MinQty], [MaxQty], [Local], [OverSeas], [OverSeasExpress] FROM [ShippingFee] WHERE ([ProductID] = @ProductID) ORDER BY [MinQty], [MaxQty]">
							<SelectParameters>
								<asp:ControlParameter ControlID="lblProductID" Name="ProductID" PropertyName="Text" Type="Int32" />
							</SelectParameters>
						</asp:SqlDataSource>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="DatePlaceHolder" Visible="false">
                <tr>
                    <td>
                        <%--Start --%>Date</td>
                    <td>
                        <asp:TextBox ID="txtStartSellDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtStartSellDate_CalendarExtender" runat="server" TargetControlID="txtStartSellDate"
                            Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtStartSellDate"
                            ErrorMessage="Invalide Start Date" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Date"
                            ValidationGroup="gp1" CssClass="errorText" ForeColor=""></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                       <%-- End Date--%></td>
                    <td>
                        <asp:TextBox ID="txtEndSellDate" runat="server" Visible="false"></asp:TextBox>
                        <asp:CalendarExtender ID="txtEndSellDate_CalendarExtender" runat="server" TargetControlID="txtEndSellDate"
                            Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtEndSellDate"
                            ErrorMessage="Invalid End Date" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Date"
                            ValidationGroup="gp1" CssClass="errorText" ForeColor=""></asp:CompareValidator>
                    </td>
                </tr>
                </asp:PlaceHolder>

                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:CheckBox ID="chkEnabled" runat="server" Text="Enabled" Checked="true" />
                        <asp:Label runat="server" ID="lblSortOrder" Visible="false"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <asp:PlaceHolder runat="server" ID="TagPlaceHolder" >
	            <tr>
		            <td>Tag</td>
		            <td colspan="2">
			            <asp:SqlDataSource runat="server" ID="dsTag" 
                            ConnectionString="<%$ ConnectionStrings:MySqlConnection %>" 
                            
                            SelectCommand="SELECT [TagID], [TagName] FROM [view_Tag] WHERE (([Lang] = @Lang) AND ([FunctionID] = @FunctionID) AND ([Enabled] = @Enabled)) ORDER BY [SortOrder]">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfdLang" Name="Lang" PropertyName="Value" 
                                    Type="String" />
                                <asp:ControlParameter ControlID="hfdFunctionID" Name="FunctionID" 
                                    PropertyName="Value" Type="Int32" />
                                <asp:Parameter DefaultValue="true" Name="Enabled" Type="Boolean" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:CheckBoxList runat="server" ID="cblTag" DataSourceID="dsTag" DataTextField="TagName" DataValueField="TagID" RepeatDirection="Horizontal" RepeatLayout="Flow" ></asp:CheckBoxList>
		            </td>
	            </tr>
	            </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ImagePlaceHolder">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Image runat="server" ID="imgPreview" />
                        <br />
                        <asp:Button runat="server" ID="btnImage" Text="Edit Image" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                </asp:PlaceHolder>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Update" ValidationGroup="gp1" OnClientClick ="return Validate()"/>
                        &nbsp;<asp:Button ID="btnClose" runat="server" Text="Back" />
                        <asp:ConfirmButtonExtender ID="btnClose_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure to close without saving?"
                            TargetControlID="btnClose" Enabled="false">
                        </asp:ConfirmButtonExtender>&nbsp;
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" />
                        <asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" 
                            TargetControlID="btnDelete">
                        </asp:ConfirmButtonExtender>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                
                

            </table>
        </div>
    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
		<ProgressTemplate></ProgressTemplate>
    </asp:UpdateProgress>
    <cc2:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server" TargetControlID="UpdateProgress1" ControlToOverlayID="UpdatePanel1" CssClass="updateProgress" OverlayType="Control" />
    <p>
    </p>
<br />
<br />
<br />

</asp:Content>
