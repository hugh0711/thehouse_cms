Imports System.IO

Partial Class detail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            If Request.QueryString("id") IsNot Nothing Then
                Dim db As New ProductDbDataContext()

                Dim product_desc = (From j In db.ProductNames
                                  Where j.ProductID = Request.QueryString("id")
                                  Select j).FirstOrDefault

                If product_desc IsNot Nothing Then



                    lit_desc.Text = product_desc.Description

                    If product_desc.fileUrl IsNot Nothing And product_desc.fileUrl.Length > 0 Then

                        Dim file_extension As String = Path.GetExtension(product_desc.fileUrl)

                        Dim image_PDF_string As String = ""

                        If file_extension = ".jpg" Or file_extension = ".png" Or file_extension = ".jpeg" Then

                            '/Love_dev/assets/files/pdf-sample.pdf
                            image_PDF_string = String.Format("<img src='{0}' alt=''>", product_desc.fileUrl.Replace("/Love_dev/", ""))

                        ElseIf file_extension = ".pdf" Then

                            Dim pdf_string As String = "<object data='{0}' type='application/pdf' width='100%' height='900'>"
                            pdf_string &= "<p>Please click <a href='{0}'>here</a>to download the PDF!</p></object>"

                            image_PDF_string = String.Format(pdf_string, product_desc.fileUrl.Replace("/Love_dev/", ""))
                        End If

                        If image_PDF_string.Length > 0 Then
                            lit_desc.Text &= String.Format("{0}<br />{1}", lit_desc.Text, image_PDF_string)
                        End If


                    End If

                End If

            End If


        End If






    End Sub
End Class
