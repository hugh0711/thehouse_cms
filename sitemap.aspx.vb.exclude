Imports System.Threading
Imports System.Globalization

Partial Class sitemap
    Inherits System.Web.UI.Page

    Protected Overrides Sub InitializeCulture()
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(Session("MyCulture").ToString())
        MyBase.InitializeCulture()
    End Sub

End Class
