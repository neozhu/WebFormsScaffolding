Imports System.Data.Entity

Public Partial Class _Default1
	Inherits System.Web.UI.Page

	Protected _db As New Samples.ApplicationDbContext()

	Protected Sub Page_Load(sender As Object, e As EventArgs)
	End Sub

	' Model binding method to get List of Movie entries
	' USAGE: <asp:ListView SelectMethod="GetData">
	Public Function GetData() As IQueryable(Of Samples.Movie)
		Return _db.Movies
	End Function
End Class

