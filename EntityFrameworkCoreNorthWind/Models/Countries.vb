Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace NorthWindEntityFrameworkCore
    Partial Public Class Countries
        Public Sub New()
            Customers = New HashSet(Of Customers)()
        End Sub

        <Key>
        Public Property CountryIdentifier() As Integer
        Public Property Name() As String

        <InverseProperty("CountryIdentifierNavigation")>
        Public Overridable Property Customers() As ICollection(Of Customers)
    End Class
End Namespace