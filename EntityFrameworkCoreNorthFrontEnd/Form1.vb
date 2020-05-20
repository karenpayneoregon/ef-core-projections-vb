Imports EntityFrameworkCoreNorthFrontEnd.Classes


Public Class Form1
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim test1 = Await Operations.ReadCustomers()
        Console.WriteLine()
    End Sub

    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim test1 = Await Operations.ReadCustomer(4)
        Console.WriteLine()
    End Sub

    Private Async Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim test1 = Await Operations.ReadCustomersProjected()
        Console.WriteLine()
    End Sub
End Class
