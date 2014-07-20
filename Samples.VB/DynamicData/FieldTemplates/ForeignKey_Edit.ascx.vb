
Public Partial Class ForeignKey_EditField
	Inherits System.Web.DynamicData.FieldTemplateUserControl

	Protected _db As New Samples.ApplicationDbContext()

	Public Property DataTypeName() As String
		Get
			Return m_DataTypeName
		End Get
		Set
			m_DataTypeName = Value
		End Set
	End Property
	Private m_DataTypeName As String

	Public Property DataTextField() As String
		Get
			Return m_DataTextField
		End Get
		Set
			m_DataTextField = Value
		End Set
	End Property
	Private m_DataTextField As String

	Public Property DataValueField() As String
		Get
			Return m_DataValueField
		End Get
		Set
			m_DataValueField = Value
		End Set
	End Property
	Private m_DataValueField As String

	Public Function GetData() As IQueryable
		Dim entityType = Type.[GetType](Me.DataTypeName)
		Return _db.[Set](entityType).AsQueryable()
	End Function

	Protected Sub Page_Init(sender As Object, e As EventArgs)
		Label1.Text = Column.DisplayName
	End Sub

	' show bootstrap has-error
	Protected Sub Page_PreRender(sender As Object, e As EventArgs)
		' if validation error then apply bootstrap has-error CSS class
		Dim isValid = Me.Page.ModelState.IsValidField(Column.Name)
		Div1.Attributes("class") = If(isValid, "form-group", "form-group has-error")
	End Sub


	Protected Overrides Sub ExtractValues(dictionary As IOrderedDictionary)
		' If it's an empty string, change it to null
		Dim value As String = DropDownList1.SelectedValue
		If [String].IsNullOrEmpty(value) Then
			value = Nothing
		End If

		dictionary(Column.Name) = ConvertEditedValue(value)
	End Sub


	Public Overrides ReadOnly Property DataControl() As Control
		Get
			Return DropDownList1
		End Get
	End Property

End Class
