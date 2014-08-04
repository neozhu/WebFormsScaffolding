Imports System.Data.Entity

Public Partial Class _Default4
	Inherits System.Web.UI.Page

	Protected _db As New Samples.ApplicationDbContext()

	Protected Sub Page_Load(sender As Object, e As EventArgs)
	End Sub

	' Model binding method to get List of Product entries
	' USAGE: <asp:ListView SelectMethod="GetData">
	Public Function GetData() As IQueryable(Of Samples.Product)
		Return _db.Products.Include(Function(m) m.Category)
	End Function
End Class

