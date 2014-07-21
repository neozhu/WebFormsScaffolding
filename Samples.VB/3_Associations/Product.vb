Imports System.ComponentModel.DataAnnotations

Public Class Product

    Public Property Id As Integer

    <Required(ErrorMessage:="Product name is required.")>
    Public Property Name As String

    Public Property Price As Decimal

    ' The CategoryId property is scaffolded. It's what
    ' gets set in the DropDownList.
    <Display(Name:="Category")>
    Public Property CategoryId As Integer


    ' The Category property is the navigation property.
    ' This property is not scaffolded but it creates the 
    ' association between products and categories.
    Public Property Category As Category

End Class
