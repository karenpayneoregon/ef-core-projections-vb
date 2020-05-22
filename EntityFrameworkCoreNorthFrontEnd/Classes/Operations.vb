Imports EntityFrameworkCoreNorthWind.Extensions
Imports EntityFrameworkCoreNorthWind.NorthWindEntityFrameworkCore
Imports EntityFrameworkCoreNorthWind.Projections
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata
Imports Microsoft.EntityFrameworkCore.Metadata.Internal

Namespace Classes

    Public Class Operations
        ''' <summary>
        ''' Select all customers with contact and country details
        ''' </summary>
        ''' <returns></returns>
        Public Shared Async Function ReadCustomers() As Task(Of List(Of Customers))

            Return Await Task.Run(
                Async Function()
                    Using context As New CustomerContext
                        Return Await context.Customers.IncludeContactCountry.ToListAsync()
                    End Using
                End Function)

        End Function
        ''' <summary>
        ''' Select a customer with contact and country details by customer identifier
        ''' </summary>
        ''' <param name="identifier">customer identifier to find by</param>
        ''' <returns></returns>
        Public Shared Async Function ReadCustomer(identifier As Integer) As Task(Of Customers)

            Return Await Task.Run(
                Async Function()
                    Using context As New CustomerContext
                        Return Await context.Customers.IncludeContactCountry(identifier)
                    End Using
                End Function)

        End Function
        Public Shared Async Function ReadCustomersProjected() As Task(Of List(Of CustomerItem))

            Return Await Task.Run(
                Async Function()
                    Using context As New CustomerContext
                        Return Await context.Customers.IncludeContactCountry.Select(CustomerItem.Projection).ToListAsync()
                    End Using
                End Function)

        End Function
        ''' <summary>
        ''' Traversing entity model, not part of article
        ''' </summary>
        Public Shared Sub Experiments()

            Using context As New CustomerContext

                Dim typeList = context.Model.GetEntityTypes().Select(Function(entityType) entityType.ClrType).ToList()

                For Each type As Type In typeList

                    Console.WriteLine($"{type.Name}")

                    Dim entityType = context.Model.FindEntityType(type)

                    For Each prop As IProperty In entityType.GetProperties()
                        Console.WriteLine($"   {prop.GetColumnName()}, {prop.GetColumnType()}, {prop.IsNullable}")
                        If prop.IsForeignKey() Then
                            Dim foreignKey As IForeignKey = entityType.FindForeignKeys(prop).Select(Function(x) x).FirstOrDefault()
                            If foreignKey IsNot Nothing Then
                                Console.WriteLine($"      {foreignKey.PrincipalEntityType}, {foreignKey.ToDebugString(False)}")
                            End If
                        End If
                    Next
                Next
            End Using
        End Sub
    End Class
End Namespace