Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class SeedClass

	Public Shared TYPE_ORDERNO As String = "ORDERNO"

	Public Function NextOrderNo() As String
		Dim number As String = ""
		number = String.Format("{0:D7}", NextNumber(TYPE_ORDERNO))
		Return number
	End Function

	Private Function NextNumber(ByVal type As String) As Integer
		Dim db As SqlConnection = New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("MySqlConnection").ConnectionString)

		Dim p As SqlParameter
		Dim cmd As SqlCommand = New SqlCommand("sp_NextOrderNumber", db)

		cmd.CommandType = CommandType.StoredProcedure
		p = cmd.CreateParameter()
		p.DbType = DbType.String
		p.ParameterName = "@seedType"
		p.Value = type
		p.Direction = ParameterDirection.Input
		cmd.Parameters.Add(p)

		Dim r As SqlParameter = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.VarChar, 250)
		r.Direction = ParameterDirection.ReturnValue

		Try
			db.Open()
			cmd.ExecuteNonQuery()
		Catch ex As Exception
			Throw ex
		Finally
			db.Close()
		End Try
		Return r.Value
	End Function

End Class
