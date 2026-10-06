Imports System.Data
Imports System.Collections.Generic

Module ParkingData
    Public ParkingTable As New DataTable()

    Public UsersTable As New DataTable()

    Public parkingDatabase As New Dictionary(Of String, Boolean)()

    Public CarBaseRate As Double = 50.0
    Public MotorBaseRate As Double = 50.0
    Public CarSucceedingRate As Double = 20.0
    Public MotorSucceedingRate As Double = 10.0
    Public FreeHours As Integer = 1
    Public OverstayPenaltyFee As Double = 200.0

    Public MaxSlots As Integer = 70

    Public Function GenerateCode() As String
        Return "PK-" & Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper()
    End Function

    Public Sub InitializeDatabase()
        If ParkingTable.Columns.Count = 0 Then
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
        End If

        If parkingDatabase.Count = 0 Then
            For i As Integer = 1 To 25
                parkingDatabase.Add($"G{i}", True)
                parkingDatabase.Add($"U{i}", True)
            Next

            For i As Integer = 26 To 35
                parkingDatabase.Add($"G{i}", True)
                parkingDatabase.Add($"U{i}", True)
            Next
        End If
    End Sub

    Public Sub InitializeUsers()
        If UsersTable.Columns.Count = 0 Then
            UsersTable.TableName = "UserRecord"
            UsersTable.Columns.Add("FullName", GetType(String))
            UsersTable.Columns.Add("Username", GetType(String))
            UsersTable.Columns.Add("Password", GetType(String))
            UsersTable.Columns.Add("Role", GetType(String))
        End If
    End Sub
End Module
