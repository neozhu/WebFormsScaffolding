Public Partial Class UrlField
	Inherits System.Web.DynamicData.FieldTemplateUserControl
	Protected Overrides Sub OnDataBinding(e As EventArgs)
		HyperLinkUrl.NavigateUrl = ProcessUrl(FieldValueString)
	End Sub

	Private Function ProcessUrl(url As String) As String
		If url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) OrElse url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) Then
			Return url
		End If

		Return Convert.ToString("http://") & url
	End Function

	Public Overrides ReadOnly Property DataControl() As Control
		Get
			Return HyperLinkUrl
		End Get
	End Property

End Class
