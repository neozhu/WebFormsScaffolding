Imports System.Data.Entity

Public Partial Class _Default5
	Inherits System.Web.UI.Page

	Protected _db As New Samples.ApplicationDbContext()

	Protected Sub Page_Load(sender As Object, e As EventArgs)
	End Sub

	' Model binding method to get List of Employee entries
	' USAGE: <asp:ListView SelectMethod="GetData">
	Public Function GetData() As IQueryable(Of Samples.Employee)
		Return _db.Employees
	End Function
End Class

