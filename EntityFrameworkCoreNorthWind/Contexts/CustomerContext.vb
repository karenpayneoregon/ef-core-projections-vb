﻿Imports System
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata
Imports Microsoft.Extensions.Logging

Namespace NorthWindEntityFrameworkCore
    Partial Public Class CustomerContext
        Inherits DbContext

        Public Sub New()
        End Sub

        Public Sub New(options As DbContextOptions(Of CustomerContext))
            MyBase.New(options)
        End Sub

        'Public Overridable Property ContactType() As DbSet(Of ContactType)
        'Public Overridable Property Contacts() As DbSet(Of Contacts)
        'Public Overridable Property Countries() As DbSet(Of Countries)
        Public Overridable Property Customers() As DbSet(Of Customers)

        Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
            If Not optionsBuilder.IsConfigured Then
                optionsBuilder.
                    UseSqlServer("Data Source=.\SQLEXPRESS;Initial Catalog=NorthWindAzureForInserts;Integrated Security=True").
                    UseLoggerFactory(ConsoleLoggerFactory).
                    EnableSensitiveDataLogging()
            End If
        End Sub

        ''' <summary>
        ''' Configure logging.
        ''' </summary>
        Public Shared ReadOnly ConsoleLoggerFactory As ILoggerFactory = LoggerFactory.
            Create(Function(builder) As ILoggerFactory
                       builder.AddFilter(
                           Function(category, level)
                               Return category = DbLoggerCategory.Database.Command.Name AndAlso level = LogLevel.Information
                           End Function)

                       builder.AddConsole()

                   End Function)
        Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)

            modelBuilder.Entity(Of Customers)(
                Sub(entity)
                    entity.HasKey(Function(e) e.CustomerIdentifier).
                                                 HasName("PK_Customers_1")

                    entity.Property(Function(e) e.ModifiedDate).
                                                 HasDefaultValueSql("(getdate())")

                    entity.HasOne(Function(d) d.Contact).
                                                 WithMany(Function(p) p.Customers).
                                                 HasForeignKey(Function(d) d.ContactId).
                                                 HasConstraintName("FK_Customers_Contacts")

                    entity.HasOne(Function(d) d.ContactTypeNavigation).
                                                 WithMany(Function(p) p.Customers).
                                                 HasForeignKey(Function(d) d.ContactTypeIdentifier).
                                                 HasConstraintName("FK_Customers_ContactType")

                    entity.HasOne(Function(d) d.CountryIdentifierNavigation).
                                                 WithMany(Function(p) p.Customers).
                                                 HasForeignKey(Function(d) d.CountryIdentifier).
                                                 HasConstraintName("FK_Customers_Countries")
                End Sub)

            OnModelCreatingPartial(modelBuilder)

        End Sub

        Partial Private Sub OnModelCreatingPartial(modelBuilder As ModelBuilder)
        End Sub
    End Class
End Namespace