Imports Microsoft.AspNet.FriendlyUrls.ModelBinding

Public Partial Class Edit3
	Inherits System.Web.UI.Page

	Protected _db As New Samples.ApplicationDbContext()

	Protected Sub Page_Load(sender As Object, e As EventArgs)
	End Sub

	' This is the Update methd to update the selected Product item
	' USAGE: <asp:FormView UpdateMethod="UpdateItem">
	Public Sub UpdateItem(Id As Integer)
		Using _db
			Dim item = _db.Products.Find(Id)

			If item Is Nothing Then
				' The item wasn't found
				ModelState.AddModelError("", [String].Format("Item with id {0} was not found", Id))
				Return
			End If

			TryUpdateModel(item)

			If ModelState.IsValid Then
				' Save changes here
				_db.SaveChanges()
				Response.Redirect("../Default")
			End If
		End Using
	End Sub

	' This is the Select method to selects a single Product item with the id
	' USAGE: <asp:FormView SelectMethod="GetItem">
	Public Function GetItem(<FriendlyUrlSegmentsAttribute(0)> Id As System.Nullable(Of Integer)) As Samples.Product
		If Id Is Nothing Then
			Return Nothing
		End If

		Using _db
			Return _db.Products.Find(Id)
		End Using
	End Function

	Protected Sub ItemCommand(sender As Object, e As FormViewCommandEventArgs)
		If e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase) Then
			Response.Redirect("../Default")
		End If
	End Sub
End Class
