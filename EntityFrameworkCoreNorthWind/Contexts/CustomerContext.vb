Imports System
Imports EntityFrameworkCoreNorthWind.Classes
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata
Imports Microsoft.Extensions.Logging

Namespace NorthWindEntityFrameworkCore
    Partial Public Class CustomerContext
        Inherits DbContext

        Private ConnectionString As String
        ''' <summary>
        ''' Get database connection string from app.config
        ''' </summary>
        Public Sub New()
            Dim configurationHelper = New ConfigurationHelper
            ConnectionString = configurationHelper.ConnectionString
        End Sub

        Public Sub New(options As DbContextOptions(Of CustomerContext))
            MyBase.New(options)
        End Sub

        Public Overridable Property Customers() As DbSet(Of Customers)
        ''' <summary>
        ''' Connection string is obtained from app.config
        ''' Logging is only enabled with the Visual Studio debugger
        ''' </summary>
        ''' <param name="optionsBuilder"></param>
        Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)

#If DEBUG Then
            If Not optionsBuilder.IsConfigured Then
                optionsBuilder.UseSqlServer(ConnectionString).UseLoggerFactory(ConsoleLoggerFactory).EnableSensitiveDataLogging()
            End If
#Else
            If Not optionsBuilder.IsConfigured Then
                optionsBuilder.UseSqlServer(ConnectionString)
            End If
#End If
        End Sub
        ''' <summary>
        ''' Configure logging for app
        ''' https://docs.microsoft.com/en-us/ef/core/miscellaneous/logging?tabs=v3
        ''' https://github.com/dotnet/EntityFramework.Docs/blob/master/entity-framework/core/miscellaneous/logging.md
        ''' </summary>
        Public Shared ReadOnly ConsoleLoggerFactory As ILoggerFactory = LoggerFactory.
            Create(Function(builder) As ILoggerFactory
                       builder.AddFilter(
                           Function(category, level)
                               Return category = DbLoggerCategory.Database.Command.Name AndAlso level = LogLevel.Information
                           End Function)

                       builder.AddConsole()

                       Return Nothing

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