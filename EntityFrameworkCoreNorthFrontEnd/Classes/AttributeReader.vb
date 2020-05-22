Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Infrastructure

Namespace Classes
    Public NotInheritable Class AttributeReader

        Private Sub New()
        End Sub

        Public Shared Function GetTableName(Of T As Class)(context As DbContext) As String
            Dim models = context.Model

            Dim entityTypes = models.GetEntityTypes()
            Dim entityTypeOfT = entityTypes.First(Function(entityType) entityType.ClrType Is GetType(T))

            Dim tableNameAnnotation = entityTypeOfT.GetAnnotation("Relational:TableName")
            Dim tableName = tableNameAnnotation.Value.ToString()

            Return tableName

        End Function

    End Class
End NameSpace