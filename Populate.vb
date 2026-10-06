Imports System.Data
Imports System.IO

Module Program
    Sub Main()
        Dim DataFilePath As String = "D:\Downloads\ParkingSystemProject 1 1\ParkingSystemProject 1 1\ParkingSystemProject 1\ParkingSystemProject\bin\Debug\parking_data.xml"
        Dim ParkingTable As New DataTable()
        ParkingTable.TableName = "ParkingRecord"

        ParkingTable.Columns.Add("Code", GetType(String))
        ParkingTable.Columns.Add("PlateNumber", GetType(String))
        ParkingTable.Columns.Add("CheckIn", GetType(DateTime))
        ParkingTable.Columns.Add("CheckOut", GetType(DateTime))
        ParkingTable.Columns.Add("VehicleType", GetType(String))
        ParkingTable.Columns.Add("RateName", GetType(String))
        ParkingTable.Columns.Add("Rate", GetType(Double))
        ParkingTable.Columns.Add("Slot", GetType(String))
        ParkingTable.Columns.Add("TotalTime", GetType(String))
        ParkingTable.Columns.Add("TotalAmount", GetType(Double))
        ParkingTable.Columns.Add("PaidStatus", GetType(String))
        ParkingTable.Columns.Add("ReservedBy", GetType(String))
        ParkingTable.Columns.Add("ProcessedBy", GetType(String))
        ParkingTable.Columns.Add("ReservationDate", GetType(DateTime))

        ParkingTable.Rows.Add("PK-TEST01", "WLK-IN", DateTime.Now.AddHours(-2), DBNull.Value, "Four Wheels", "50 per Hour", 50.0, "G1", "", 0.0, "Not Paid", "", "", DBNull.Value)

        ParkingTable.Rows.Add("PK-RESV01", "", DBNull.Value, DBNull.Value, "Four Wheels", "", 0.0, "G1", "", 0.0, "Reserved", "Juan Dela Cruz", "Administrator", DateTime.Now.Date)

        For i As Integer = 2 To 35
            ParkingTable.Rows.Add("PK-G" & i, "PLT-G" & i, DateTime.Now.AddHours(-1), DBNull.Value, "Four Wheels", "50 per Hour", 50.0, "G" & i, "", 0.0, "Not Paid", "", "", DBNull.Value)
        Next
        For i As Integer = 1 To 35
            ParkingTable.Rows.Add("PK-U" & i, "PLT-U" & i, DateTime.Now.AddHours(-1), DBNull.Value, "Two Wheels", "50 per Hour", 50.0, "U" & i, "", 0.0, "Not Paid", "", "", DBNull.Value)
        Next

        ParkingTable.WriteXml(DataFilePath, XmlWriteMode.WriteSchema)
        Console.WriteLine("Data generated successfully.")
    End Sub
End Module
