Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace NorthWindEntityFrameworkCore
    ''' <summary>
    ''' 
    ''' </summary>
    Partial Public Class Contacts
        Public Sub New()
            Customers = New HashSet(Of Customers)()
        End Sub

        <Key>
        Public Property ContactId() As Integer
        Public Property FirstName() As String
        Public Property LastName() As String

        <InverseProperty("Contact")>
        Public Overridable Property Customers() As ICollection(Of Customers)
    End Class
End Namespace