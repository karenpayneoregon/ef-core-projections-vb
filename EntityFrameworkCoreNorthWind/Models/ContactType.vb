Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace NorthWindEntityFrameworkCore
    Partial Public Class ContactType
        Public Sub New()
            Customers = New HashSet(Of Customers)()
        End Sub

        <Key>
        Public Property ContactTypeIdentifier() As Integer
        Public Property ContactTitle() As String

        <InverseProperty("ContactTypeNavigation")>
        Public Overridable Property Customers() As ICollection(Of Customers)
    End Class
End Namespace