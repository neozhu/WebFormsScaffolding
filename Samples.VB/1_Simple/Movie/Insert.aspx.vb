
Public Partial Class Insert
	Inherits System.Web.UI.Page
	Protected _db As New Samples.ApplicationDbContext()

	Protected Sub Page_Load(sender As Object, e As EventArgs)

	End Sub

	' This is the Insert method to insert the entered Movie item
	' USAGE: <asp:FormView InsertMethod="InsertItem">
	Public Sub InsertItem()
		Using _db
			Dim item = New Samples.Movie()

			TryUpdateModel(item)

			If ModelState.IsValid Then
				' Save changes
				_db.Movies.Add(item)
				_db.SaveChanges()

				Response.Redirect("Default")
			End If
		End Using
	End Sub

	Protected Sub ItemCommand(sender As Object, e As FormViewCommandEventArgs)
		If e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase) Then
			Response.Redirect("Default")
		End If
	End Sub
End Class
