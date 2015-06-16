Imports System.Linq
Imports Newtonsoft.Json.Linq

Partial Class master_MainMasterPage
    Inherits System.Web.UI.MasterPage







    'Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    FormsAuthentication.SignOut()
    '    Response.Redirect("~/")
    'End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        
    End Sub

    
    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FormsAuthentication.SignOut()
        Response.Redirect("~/")
    End Sub

End Class

