Public Partial Class FieldLabelField
	Inherits System.Web.DynamicData.FieldTemplateUserControl

	Protected Sub Page_Init(sender As Object, e As EventArgs)
		Label1.Text = Column.DisplayName
	End Sub

	Public Overrides ReadOnly Property DataControl() As Control
		Get
			Return Label1
		End Get
	End Property
End Class