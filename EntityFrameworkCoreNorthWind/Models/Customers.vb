Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace NorthWindEntityFrameworkCore
    Partial Public Class Customers
        <Key>
        Public Property CustomerIdentifier() As Integer
        <Required, StringLength(40)>
        Public Property CompanyName() As String
        <StringLength(30)>
        Public Property ContactName() As String
        Public Property ContactId() As Integer?
        <StringLength(60)>
        Public Property Address() As String
        <StringLength(15)>
        Public Property City() As String
        <StringLength(15)>
        Public Property Region() As String
        <StringLength(10)>
        Public Property PostalCode() As String
        Public Property CountryIdentifier() As Integer?
        <StringLength(24)>
        Public Property Phone() As String
        <StringLength(24)>
        Public Property Fax() As String
        Public Property ContactTypeIdentifier() As Integer?
        Public Property ModifiedDate() As Date?

        <ForeignKey(NameOf(ContactId)), InverseProperty(NameOf(Contacts.Customers))>
        Public Overridable Property Contact() As Contacts
        <ForeignKey(NameOf(ContactTypeIdentifier)), InverseProperty(NameOf(ContactType.Customers))>
        Public Overridable Property ContactTypeNavigation() As ContactType
        <ForeignKey(NameOf(CountryIdentifier)), InverseProperty(NameOf(Countries.Customers))>
        Public Overridable Property CountryIdentifierNavigation() As Countries

        Public Overrides Function ToString() As String
            Return CompanyName
        End Function
    End Class
End Namespace