Imports System.ComponentModel.DataAnnotations

Public Class Category
    Public Property Id As Integer

    ' Use the display attribute to indicate the data text field for the DropDownList
    <Display>
    Public Property Name As String
End Class
