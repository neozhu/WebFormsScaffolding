Imports System.ComponentModel.DataAnnotations.Schema
Imports System.ComponentModel.DataAnnotations

Public Class Employee
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property Id As Guid

    <Required>
    <DataType(DataType.EmailAddress)>
    Public Property Email As String

    <DataType(DataType.Url)>
    <Display(Name:="Home Page URL")>
    Public Property HomePage As String

    <Required(ErrorMessage:="You must enter a salary level")>
    Public Property Salary As SalaryLevel

    ' selecting a region is optional
    Public Property Region As Region?

End Class
