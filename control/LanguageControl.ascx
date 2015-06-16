<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LanguageControl.ascx.vb" Inherits="control_LanguageControl" %>
<%--<ul id="site_language">
    <li><a href="../eng/showroom.html">English</a></li>
    <!--li class="site_language_sept">|</li>
    <li><a href="#">中文</a></li-->
</ul>--%>

<%--<asp:ListView runat="server" ID="lvLanguage">
    <LayoutTemplate>
        <ul id="site_language">
            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
        </ul>
    </LayoutTemplate>
    <ItemSeparatorTemplate>
        <li runat="server" class="site_language_sept">|</li>
    </ItemSeparatorTemplate>
    <ItemTemplate>
        <li runat="server"><asp:LinkButton runat="server" Text='<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="btnLanguage_Click"  /></li>
    </ItemTemplate>
</asp:ListView>--%>

                <table border="0" align="right">
                    <tr>
                        <td><asp:ImageButton ID="btnLangEn" runat="server" ImageUrl="~/images/lang_eng.jpg" Width="43px" 
                                Height="24px" CommandArgument="en-us" /></td>
                        <td><asp:Image ID="Image14" runat="server" ImageUrl="~/images/separator1.jpg" 
                                Width="15px" Height="24px" meta:resourcekey="Image14Resource1"/></td>
                        <td><asp:ImageButton ID="btnLangTC" runat="server" ImageUrl="~/images/lang_tc.jpg" Width="24px" 
                                Height="24px" CommandArgument="zh-hk"  /></td>
                        <td><asp:Image ID="Image16" runat="server" ImageUrl="~/images/separator2.jpg" 
                                Width="16px" Height="24px" meta:resourcekey="Image16Resource1"/></td>
                        <td><asp:ImageButton ID="btnLangSC" runat="server" ImageUrl="~/images/lang_sc.jpg" Width="33px" 
                                Height="24px" CommandArgument="zh-cn" /></td>
                    </tr>
                </table>