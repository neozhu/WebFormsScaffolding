Imports System.ComponentModel.DataAnnotations
Imports System.Web.DynamicData
Imports System.Web

Class Boolean_EditField
    Inherits FieldTemplateUserControl

	Protected Sub Page_Init(sender As Object, e As EventArgs)
        CheckBox1.ToolTip = Column.Description
		Label1.Text = Column.DisplayName
	End Sub

    Protected Overrides Sub OnDataBinding(ByVal e As EventArgs)
        MyBase.OnDataBinding(e)
        Dim val As Object = FieldValue
        If val IsNot Nothing Then
            CheckBox1.Checked = CType(val, Boolean)
        End If
    End Sub
    
    Protected Overrides Sub ExtractValues(ByVal dictionary As IOrderedDictionary)
        dictionary(Column.Name) = CheckBox1.Checked
    End Sub

    Public Overrides ReadOnly Property DataControl As Control
        Get
            Return CheckBox1
        End Get
    End Property
    
    
End Class
