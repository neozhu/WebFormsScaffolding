Imports System.ComponentModel.DataAnnotations

Public Class Book
    Implements IValidatableObject

    Public Property Id() As Integer
        Get
            Return m_Id
        End Get
        Set(value As Integer)
            m_Id = value
        End Set
    End Property
    Private m_Id As Integer

    <Required(ErrorMessage:="You must enter a book title.")> _
    Public Property Title() As String
        Get
            Return m_Title
        End Get
        Set(value As String)
            m_Title = value
        End Set
    End Property
    Private m_Title As String

    <Range(0, 500, ErrorMessage:="Units in stock must be between 0 and 500.")> _
    <Display(Name:="Units In Stock")> _
    Public Property UnitsInStock() As Integer
        Get
            Return m_UnitsInStock
        End Get
        Set(value As Integer)
            m_UnitsInStock = value
        End Set
    End Property
    Private m_UnitsInStock As Integer

    <Required(ErrorMessage:="You must enter comments about the book.")> _
    <DataType(DataType.MultilineText)> _
    Public Property Comments() As String
        Get
            Return m_Comments
        End Get
        Set(value As String)
            m_Comments = value
        End Set
    End Property
    Private m_Comments As String

    <Display(Name:="In Stock")> _
    Public Property InStock() As Boolean
        Get
            Return m_InStock
        End Get
        Set(value As Boolean)
            m_InStock = value
        End Set
    End Property
    Private m_InStock As Boolean

    Public Function Validate(validationContext As ValidationContext) As IEnumerable(Of ValidationResult) Implements IValidatableObject.Validate
        Dim errors = New List(Of ValidationResult)()
        If Not [String].IsNullOrWhiteSpace(Me.Comments) Then
            If Me.Comments.Contains("great") Then
                errors.Add(New ValidationResult("You can't enter the word great in comments!", New String() {"Comments"}))
            End If
        End If
        Return errors
    End Function
End Class

