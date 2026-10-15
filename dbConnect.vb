Imports System
Imports System.Data
Imports MySql.Data.MySqlClient

Public Module DatabaseModule

    Public connStr As String = "Server=127.0.0.1;Database=parking_db;Uid=root;Pwd=;"

    Public Function GetConnection() As MySqlConnection
        Dim conn As New MySqlConnection(connStr)
        conn.Open()
        Return conn
    End Function

    Public Function TryGetConnection(ByRef outConn As MySqlConnection, ByRef outErrorMessage As String) As Boolean
        Try
            outConn = New MySqlConnection(connStr)
            outConn.Open()
            outErrorMessage = ""
            Return True
        Catch ex As Exception
            outConn = Nothing
            outErrorMessage = ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Normalizes vehicle plate numbers by trimming and capitalizing.
    ''' </summary>
    Public Function NormalizePlate(plate As String) As String
        If String.IsNullOrWhiteSpace(plate) Then Return ""
        Return plate.Trim().ToUpper()
    End Function

    ''' <summary>
    ''' Validates vehicle plate length (4 to 8 characters).
    ''' </summary>
    Public Function IsValidPlate(plate As String) As Boolean
        Dim p = NormalizePlate(plate)
        Return p.Length >= 4 AndAlso p.Length <= 8
    End Function

    ''' <summary>
    ''' Vehicle classification rule:
    ''' G1-G25 and U1-U25 are for Cars ("Four Wheels").
    ''' G26-G35 and U26-U35 are for Motorcycles ("Two Wheels").
    ''' </summary>
    Public Function IsCarSlot(slotName As String) As Boolean
        If String.IsNullOrWhiteSpace(slotName) OrElse slotName.Length < 2 Then Return True
        Dim prefix As Char = Char.ToUpper(slotName(0))
        If prefix <> "G"c AndAlso prefix <> "U"c Then Return True

        Dim num As Integer = 0
        If Integer.TryParse(slotName.Substring(1), num) Then
            Return num <= 25
        End If
        Return True
    End Function

    Public Function GetSlotVehicleType(slotName As String) As String
        Return If(IsCarSlot(slotName), "Four Wheels", "Two Wheels")
    End Function

    ''' <summary>
    ''' Loads system rates and settings from MySQL tblrates into ParkingData shared variables.
    ''' </summary>
    Public Sub LoadRatesFromDatabase()
        Try
            Using conn As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT * FROM tblrates ORDER BY RateID LIMIT 1", conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ParkingData.CarBaseRate = Convert.ToDecimal(reader("CarBaseRate"))
                            ParkingData.MotorBaseRate = Convert.ToDecimal(reader("MotorBaseRate"))
                            ParkingData.CarSucceedingRate = Convert.ToDecimal(reader("CarSucceedingRate"))
                            ParkingData.MotorSucceedingRate = Convert.ToDecimal(reader("MotorSucceedingRate"))
                            ParkingData.FreeHours = Convert.ToInt32(reader("FreeHours"))
                            If Not reader.IsDBNull(reader.GetOrdinal("OverstayPenaltyFee")) Then
                                ParkingData.OverstayPenaltyFee = Convert.ToDecimal(reader("OverstayPenaltyFee"))
                            End If
                            Try
                                Dim pNameCol = reader.GetOrdinal("ParkingName")
                                If Not reader.IsDBNull(pNameCol) Then
                                    Dim loadedName = reader.GetString(pNameCol).Trim()
                                    If Not String.IsNullOrEmpty(loadedName) Then
                                        ParkingData.ParkingName = loadedName
                                    End If
                                End If
                            Catch
                            End Try
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Fallback to in-memory defaults if database read fails
        End Try
    End Sub

    ''' <summary>
    ''' Saves system rates and settings to MySQL tblrates.
    ''' </summary>
    Public Function SaveRatesToDatabase(carBase As Decimal, motorBase As Decimal, carSucc As Decimal, motorSucc As Decimal, freeHrs As Integer, penalty As Decimal, Optional pName As String = "") As Boolean
        Try
            Using conn As MySqlConnection = GetConnection()
                Dim effectiveName As String = If(Not String.IsNullOrWhiteSpace(pName), pName, ParkingData.GetParkingName())
                Dim sql As String = "INSERT INTO tblrates (RateID, CarBaseRate, MotorBaseRate, CarSucceedingRate, MotorSucceedingRate, FreeHours, OverstayPenaltyFee, ParkingName) " &
                                    "VALUES (1, @cb, @mb, @cs, @ms, @fh, @pen, @pn) " &
                                    "ON DUPLICATE KEY UPDATE " &
                                    "CarBaseRate = VALUES(CarBaseRate), " &
                                    "MotorBaseRate = VALUES(MotorBaseRate), " &
                                    "CarSucceedingRate = VALUES(CarSucceedingRate), " &
                                    "MotorSucceedingRate = VALUES(MotorSucceedingRate), " &
                                    "FreeHours = VALUES(FreeHours), " &
                                    "OverstayPenaltyFee = VALUES(OverstayPenaltyFee), " &
                                    "ParkingName = VALUES(ParkingName)"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@cb", carBase)
                    cmd.Parameters.AddWithValue("@mb", motorBase)
                    cmd.Parameters.AddWithValue("@cs", carSucc)
                    cmd.Parameters.AddWithValue("@ms", motorSucc)
                    cmd.Parameters.AddWithValue("@fh", freeHrs)
                    cmd.Parameters.AddWithValue("@pen", penalty)
                    cmd.Parameters.AddWithValue("@pn", effectiveName)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ParkingData.CarBaseRate = carBase
            ParkingData.MotorBaseRate = motorBase
            ParkingData.CarSucceedingRate = carSucc
            ParkingData.MotorSucceedingRate = motorSucc
            ParkingData.FreeHours = freeHrs
            ParkingData.OverstayPenaltyFee = penalty
            If Not String.IsNullOrWhiteSpace(pName) Then
                ParkingData.ParkingName = pName
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Auto-generates a unique, sequential, zero-padded receipt number (e.g. 000123).
    ''' </summary>
    Public Function GetNextReceiptNumber() As String
        Try
            Using conn As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COALESCE(MAX(CAST(ReceiptNo AS UNSIGNED)), 0) + 1 FROM tblparkingrecord WHERE ReceiptNo REGEXP '^[0-9]+$'", conn)
                    Dim res As Object = cmd.ExecuteScalar()
                    Dim nextSeq As Integer = 1
                    If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                        Integer.TryParse(res.ToString(), nextSeq)
                    End If
                    If nextSeq < 1 Then nextSeq = 1
                    Return nextSeq.ToString("D6")
                End Using
            End Using
        Catch ex As Exception
            Return "000001"
        End Try
    End Function

    ''' <summary>
    ''' Automatically verifies and applies required database schema columns if missing.
    ''' Compatible with all MySQL and MariaDB versions.
    ''' </summary>
    Public Sub EnsureDatabaseSchema()
        Try
            Using conn As MySqlConnection = GetConnection()
                AddColumnIfNotExists(conn, "tblparkingrecord", "ReceiptNo", "VARCHAR(20) NULL DEFAULT NULL")
                AddColumnIfNotExists(conn, "tblparkingrecord", "PaymentMethod", "VARCHAR(50) NULL DEFAULT 'Cash'")
                AddColumnIfNotExists(conn, "tblrates", "ParkingName", "VARCHAR(100) NULL DEFAULT 'JJRSP Parking System'")
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddColumnIfNotExists(conn As MySqlConnection, tableName As String, columnName As String, columnDef As String)
        Try
            Dim exists As Boolean = False
            Using checkCmd As New MySqlCommand("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @tbl AND COLUMN_NAME = @col", conn)
                checkCmd.Parameters.AddWithValue("@tbl", tableName)
                checkCmd.Parameters.AddWithValue("@col", columnName)
                Dim cnt As Object = checkCmd.ExecuteScalar()
                If cnt IsNot Nothing AndAlso Convert.ToInt32(cnt) > 0 Then
                    exists = True
                End If
            End Using

            If Not exists Then
                Using alterCmd As New MySqlCommand($"ALTER TABLE `{tableName}` ADD COLUMN `{columnName}` {columnDef}", conn)
                    alterCmd.ExecuteNonQuery()
                End Using
            End If
        Catch
        End Try
    End Sub

End Module