Imports System.Data.Entity
Imports Microsoft.AspNet.Identity.EntityFramework



Public Class VBContext
    Inherits IdentityDbContext(Of ApplicationUser)

    ' You can add custom code to this file. Changes will not be overwritten.
    ' 
    ' If you want Entity Framework to drop and regenerate your database
    ' automatically whenever you change your model schema, please use data migrations.
    ' For more information refer to the documentation:
    ' http://msdn.microsoft.com/en-us/data/jj591621.aspx

    Public Sub New()
        MyBase.New("name=VBContext")
    End Sub

    Public Shared Function Create() As VBContext
        Return New VBContext()
    End Function


End Class


