Imports Microsoft.AspNet.FriendlyUrls.ModelBinding
Imports System.Data.Entity

Public Partial Class Delete
	Inherits System.Web.UI.Page

	Protected _db As New Samples.ApplicationDbContext()

	Protected Sub Page_Load(sender As Object, e As EventArgs)
	End Sub

	' This is the Delete method to delete the selected Movie item
	' USAGE: <asp:FormView DeleteMethod="DeleteItem">
	Public Sub DeleteItem(Id As Integer)
		Using _db
			Dim item = _db.Movies.Find(Id)

			If item IsNot Nothing Then
				_db.Movies.Remove(item)
				_db.SaveChanges()
			End If
		End Using
		Response.Redirect("../Default")
	End Sub

	' This is the Select methd to selects a single Movie item with the id
	' USAGE: <asp:FormView SelectMethod="GetItem">
	Public Function GetItem(<FriendlyUrlSegmentsAttribute(0)> Id As System.Nullable(Of Integer)) As Samples.Movie
		If Id Is Nothing Then
			Return Nothing
		End If

		Using _db
			Return _db.Movies.Where(Function(m) m.Id = Id).FirstOrDefault()
		End Using
	End Function

	Protected Sub ItemCommand(sender As Object, e As FormViewCommandEventArgs)
		If e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase) Then
			Response.Redirect("../Default")
		End If
	End Sub
End Class

