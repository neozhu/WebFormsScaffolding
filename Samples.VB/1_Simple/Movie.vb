Imports System.ComponentModel.DataAnnotations

Public Class Movie

    Public Property Id As Integer

    <Required(ErrorMessage:="Movie title is required.")>
    Public Property Title As String

    <Required(ErrorMessage:="Movie director is required.")>
    Public Property Director As String

    <DataType(DataType.MultilineText)>
    Public Property Comments As String

    Public Property Count As Integer

    Public Property Price As Decimal

    <Display(Name:="Release Date")>
    Public Property ReleaseDate As DateTime


End Class
