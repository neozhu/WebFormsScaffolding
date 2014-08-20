Imports Microsoft.AspNet.FriendlyUrls.ModelBinding
Imports System.Data.Entity

Public Partial Class Details4
	Inherits System.Web.UI.Page

	Protected _db As New Samples.ApplicationDbContext()

	Protected Sub Page_Load(sender As Object, e As EventArgs)
	End Sub

	' This is the Select methd to selects a single Employee item with the id
	' USAGE: <asp:FormView SelectMethod="GetItem">
	Public Function GetItem(<FriendlyUrlSegmentsAttribute(0)> Id As System.Nullable(Of Guid)) As Samples.Employee
		If Id Is Nothing Then
			Return Nothing
		End If

		Using _db
			Return _db.Employees.Where(Function(m) m.Id = Id).FirstOrDefault()
		End Using
	End Function

	Protected Sub ItemCommand(sender As Object, e As FormViewCommandEventArgs)
		If e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase) Then
			Response.Redirect("../Default")
		End If
	End Sub
End Class

