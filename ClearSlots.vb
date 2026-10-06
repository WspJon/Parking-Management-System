Imports System.Data
Imports System.IO

Module Program
    Sub Main()
        Dim path As String = "bin\Debug\parking_data.xml"
        If File.Exists(path) Then
            Dim table As New DataTable("ParkingRecord")
            table.ReadXml(path)
            
            Dim rowsToDelete As New System.Collections.Generic.List(Of DataRow)
            For Each row As DataRow In table.Rows
                If row("PaidStatus").ToString() = "Not Paid" OrElse row("PaidStatus").ToString() = "Reserved" Then
                    rowsToDelete.Add(row)
                End If
            Next
            
            For Each row As DataRow In rowsToDelete
                table.Rows.Remove(row)
            Next
            
            table.WriteXml(path, XmlWriteMode.WriteSchema)
            Console.WriteLine("Removed occupied slots successfully.")
        Else
            Console.WriteLine("File not found.")
        End If
    End Sub
End Module
