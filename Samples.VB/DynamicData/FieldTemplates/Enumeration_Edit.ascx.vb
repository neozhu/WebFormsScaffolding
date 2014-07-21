Imports System.Web.DynamicData

Public Partial Class Enumeration_EditField
	Inherits System.Web.DynamicData.FieldTemplateUserControl
	Private _enumType As Type

	Protected Sub Page_Init(sender As Object, e As EventArgs)
		Label1.Text = Column.DisplayName
		DropDownList1.ToolTip = Column.Description

		If DropDownList1.Items.Count = 0 Then
			If Mode = DataBoundControlMode.Insert OrElse Not Column.IsRequired Then
				DropDownList1.Items.Add(New ListItem("Select An Option", [String].Empty))
			End If
			PopulateListControl(DropDownList1)
		End If
	End Sub

	' show bootstrap has-error
	Protected Sub Page_PreRender(sender As Object, e As EventArgs)
		' if validation error then apply bootstrap has-error CSS class
		Dim isValid = Me.Page.ModelState.IsValidField(Column.Name)
		Div1.Attributes("class") = If(isValid, "form-group", "form-group has-error")
	End Sub

	Protected Overrides Sub OnDataBinding(e As EventArgs)
		MyBase.OnDataBinding(e)

		If Mode = DataBoundControlMode.Edit AndAlso FieldValue IsNot Nothing Then
			Dim selectedValueString As String = GetSelectedValueString()
			Dim item As ListItem = DropDownList1.Items.FindByValue(selectedValueString)
			If item IsNot Nothing Then
				DropDownList1.SelectedValue = selectedValueString
			End If
		End If
	End Sub

	Private ReadOnly Property EnumType() As Type
		Get
			If _enumType Is Nothing Then
				_enumType = Column.GetEnumType()
			End If
			Return _enumType
		End Get
	End Property

	Protected Overrides Sub ExtractValues(dictionary As IOrderedDictionary)
		Dim value As String = DropDownList1.SelectedValue
		If value = [String].Empty Then
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
