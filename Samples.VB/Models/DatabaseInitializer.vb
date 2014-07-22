Imports System.Data.Entity

Public Class DatabaseInitializer
    Inherits DropCreateDatabaseAlways(Of ApplicationDbContext)
    Protected Overrides Sub Seed(context As ApplicationDbContext)
        Dim categories = New List(Of Category)() From { _
            New Category() With { _
                .Name = "Beverages" _
            }, _
            New Category() With { _
                .Name = "Fruit" _
            } _
        }

        categories.ForEach(Function(c) context.Categories.Add(c))

    End Sub
End Class
