Imports System.Data
Imports System.Collections.Generic

Module ParkingData
    Public ParkingTable As New DataTable()

    Public UsersTable As New DataTable()

    Public parkingDatabase As New Dictionary(Of String, Boolean)()

    Public CarBaseRate As Decimal = 50.0D
    Public MotorBaseRate As Decimal = 50.0D
    Public CarSucceedingRate As Decimal = 20.0D
    Public MotorSucceedingRate As Decimal = 10.0D
    Public FreeHours As Integer = 1
    Public OverstayPenaltyFee As Decimal = 200.0D

    Public MaxSlots As Integer = 70

    Public Function GenerateCode() As String
        Return "PK-" & Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper()
    End Function

    Public Sub InitializeDatabase()
        If ParkingTable.Columns.Count = 0 Then
            ParkingTable.TableName = "ParkingRecord"

            ParkingTable.Columns.Add("TransactionID", GetType(Integer))
            ParkingTable.Columns.Add("Code", GetType(String))
            ParkingTable.Columns.Add("PlateNumber", GetType(String))
            ParkingTable.Columns.Add("CheckIn", GetType(DateTime))
            ParkingTable.Columns.Add("CheckOut", GetType(DateTime))
            ParkingTable.Columns.Add("VehicleType", GetType(String))
            ParkingTable.Columns.Add("RateName", GetType(String))
            ParkingTable.Columns.Add("Rate", GetType(Decimal))
            ParkingTable.Columns.Add("Slot", GetType(String))
            ParkingTable.Columns.Add("TotalTime", GetType(String))
            ParkingTable.Columns.Add("TotalAmount", GetType(Decimal))
            ParkingTable.Columns.Add("PaidStatus", GetType(String))
            ParkingTable.Columns.Add("ReservedBy", GetType(String))
            ParkingTable.Columns.Add("ProcessedBy", GetType(String))
            ParkingTable.Columns.Add("ReservationDate", GetType(DateTime))
            ParkingTable.Columns.Add("ReservationID", GetType(Integer))
            ParkingTable.Columns.Add("CustomerID", GetType(Integer))
            ParkingTable.Columns.Add("TellerID", GetType(Integer))
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
            UsersTable.Columns.Add("UserID", GetType(Integer))
            UsersTable.Columns.Add("FullName", GetType(String))
            UsersTable.Columns.Add("Username", GetType(String))
            UsersTable.Columns.Add("Password", GetType(String))
            UsersTable.Columns.Add("Role", GetType(String))
        End If
    End Sub
End Module
