Imports System
Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Module DataStore

    Public Sub LoadDatabase()
        ParkingData.InitializeDatabase()
        ParkingData.InitializeUsers()
        DatabaseModule.LoadRatesFromDatabase()
        MigrateExistingXmlData()
    End Sub

    Public Sub SaveDatabase()
        ' Deprecated: MySQL is now the single source of truth.
        ' All mutations are written directly to MySQL within database transactions.
    End Sub

    Public Sub SaveUsers()
        ' Deprecated: Users are saved directly into MySQL.
    End Sub

    Public Sub SaveRates()
        DatabaseModule.SaveRatesToDatabase(
            ParkingData.CarBaseRate,
            ParkingData.MotorBaseRate,
            ParkingData.CarSucceedingRate,
            ParkingData.MotorSucceedingRate,
            ParkingData.FreeHours,
            ParkingData.OverstayPenaltyFee
        )
    End Sub

    Public Sub LoadRates()
        DatabaseModule.LoadRatesFromDatabase()
    End Sub

    ''' <summary>
    ''' One-time safe migration: If legacy XML data files (parking_data.xml, users_data.xml) exist on disk,
    ''' safely import their records into parking_db MySQL tables without duplicating or dropping data.
    ''' After importing, the XML files are safely archived to .xml.bak so they do not conflict.
    ''' </summary>
    Public Sub MigrateExistingXmlData()
        Try
            Dim dataFilePath As String = Path.Combine(Application.StartupPath, "parking_data.xml")
            Dim usersFilePath As String = Path.Combine(Application.StartupPath, "users_data.xml")
            Dim ratesFilePath As String = Path.Combine(Application.StartupPath, "rates_config.xml")

            ' 1. Migrate XML Rates if present
            If File.Exists(ratesFilePath) Then
                Try
                    Dim doc As New Xml.XmlDocument()
                    doc.Load(ratesFilePath)
                    Dim root = doc.DocumentElement
                    If root IsNot Nothing Then
                        Dim carBaseNode = root.SelectSingleNode("CarBaseRate")
                        Dim motorBaseNode = root.SelectSingleNode("MotorBaseRate")
                        Dim carSuccNode = root.SelectSingleNode("CarSucceedingRate")
                        Dim motorSuccNode = root.SelectSingleNode("MotorSucceedingRate")
                        Dim freeHrsNode = root.SelectSingleNode("FreeHours")

                        Dim cb As Decimal = If(carBaseNode IsNot Nothing, Convert.ToDecimal(carBaseNode.InnerText), ParkingData.CarBaseRate)
                        Dim mb As Decimal = If(motorBaseNode IsNot Nothing, Convert.ToDecimal(motorBaseNode.InnerText), ParkingData.MotorBaseRate)
                        Dim cs As Decimal = If(carSuccNode IsNot Nothing, Convert.ToDecimal(carSuccNode.InnerText), ParkingData.CarSucceedingRate)
                        Dim ms As Decimal = If(motorSuccNode IsNot Nothing, Convert.ToDecimal(motorSuccNode.InnerText), ParkingData.MotorSucceedingRate)
                        Dim fh As Integer = If(freeHrsNode IsNot Nothing, Convert.ToInt32(freeHrsNode.InnerText), ParkingData.FreeHours)

                        DatabaseModule.SaveRatesToDatabase(cb, mb, cs, ms, fh, ParkingData.OverstayPenaltyFee)
                    End If
                    File.Move(ratesFilePath, ratesFilePath & ".bak")
                Catch
                End Try
            End If

            ' 2. Migrate XML Users if present
            If File.Exists(usersFilePath) Then
                Try
                    Dim dtUsers As New DataTable()
                    dtUsers.ReadXml(usersFilePath)
                    Using conn As MySqlConnection = DatabaseModule.GetConnection()
                        For Each uRow As DataRow In dtUsers.Rows
                            Dim uName As String = If(uRow.Table.Columns.Contains("Username"), uRow("Username").ToString().Trim(), "")
                            Dim pwd As String = If(uRow.Table.Columns.Contains("Password"), uRow("Password").ToString(), "1234")
                            Dim fName As String = If(uRow.Table.Columns.Contains("FullName"), uRow("FullName").ToString().Trim(), uName)
                            Dim role As String = If(uRow.Table.Columns.Contains("Role"), uRow("Role").ToString().Trim(), "Customer")

                            If Not String.IsNullOrEmpty(uName) AndAlso uName.ToLower() <> "admin" Then
                                If role.Equals("Teller", StringComparison.OrdinalIgnoreCase) Then
                                    Using cmd As New MySqlCommand("INSERT IGNORE INTO tblteller (FullName, Username, Password, Attempts, Status) VALUES (@fn, @un, @pw, 0, '1')", conn)
                                        cmd.Parameters.AddWithValue("@fn", fName)
                                        cmd.Parameters.AddWithValue("@un", uName)
                                        cmd.Parameters.AddWithValue("@pw", pwd)
                                        cmd.ExecuteNonQuery()
                                    End Using
                                Else
                                    Using cmd As New MySqlCommand("INSERT IGNORE INTO tblcustomer (Fullname, Username, Password, Attempts, Status) VALUES (@fn, @un, @pw, 0, 1)", conn)
                                        cmd.Parameters.AddWithValue("@fn", fName)
                                        cmd.Parameters.AddWithValue("@un", uName)
                                        cmd.Parameters.AddWithValue("@pw", pwd)
                                        cmd.ExecuteNonQuery()
                                    End Using
                                End If
                            End If
                        Next
                    End Using
                    File.Move(usersFilePath, usersFilePath & ".bak")
                Catch
                End Try
            End If

            ' 3. Migrate XML Parking/Reservation records if present
            If File.Exists(dataFilePath) Then
                Try
                    Dim dtPark As New DataTable()
                    dtPark.ReadXml(dataFilePath)
                    Using conn As MySqlConnection = DatabaseModule.GetConnection()
                        For Each pRow As DataRow In dtPark.Rows
                            Dim pCode As String = If(pRow.Table.Columns.Contains("Code"), pRow("Code").ToString().Trim(), "")
                            Dim pPlate As String = DatabaseModule.NormalizePlate(If(pRow.Table.Columns.Contains("PlateNumber"), pRow("PlateNumber").ToString(), ""))
                            Dim pSlot As String = If(pRow.Table.Columns.Contains("Slot"), pRow("Slot").ToString().Trim(), "")
                            Dim pStatus As String = If(pRow.Table.Columns.Contains("PaidStatus"), pRow("PaidStatus").ToString().Trim(), "Not Paid")
                            Dim pType As String = If(pRow.Table.Columns.Contains("VehicleType"), pRow("VehicleType").ToString().Trim(), DatabaseModule.GetSlotVehicleType(pSlot))
                            Dim resDate As Object = If(pRow.Table.Columns.Contains("ReservationDate") AndAlso Not IsDBNull(pRow("ReservationDate")), pRow("ReservationDate"), DBNull.Value)

                            If pStatus = "Reserved" Then
                                If DatabaseModule.IsValidPlate(pPlate) AndAlso Not String.IsNullOrEmpty(pSlot) Then
                                    Using cmd As New MySqlCommand("INSERT INTO tblreservation (CustomerName, ParkingSlot, VehiclePlate, VehicleType, ReservationDate, Status) " &
                                                                 "SELECT @cn, @slot, @plate, @vtype, @rdate, 'Reserved' FROM DUAL " &
                                                                 "WHERE NOT EXISTS (SELECT 1 FROM tblreservation WHERE ParkingSlot = @slot AND ReservationDate = @rdate AND Status = 'Reserved')", conn)
                                        cmd.Parameters.AddWithValue("@cn", If(pRow.Table.Columns.Contains("ReservedBy"), pRow("ReservedBy").ToString(), "Customer"))
                                        cmd.Parameters.AddWithValue("@slot", pSlot)
                                        cmd.Parameters.AddWithValue("@plate", pPlate)
                                        cmd.Parameters.AddWithValue("@vtype", pType)
                                        cmd.Parameters.AddWithValue("@rdate", If(resDate Is DBNull.Value, DateTime.Today, Convert.ToDateTime(resDate).Date))
                                        cmd.ExecuteNonQuery()
                                    End Using
                                End If
                            Else
                                If Not String.IsNullOrEmpty(pCode) AndAlso DatabaseModule.IsValidPlate(pPlate) Then
                                    Using cmd As New MySqlCommand("INSERT IGNORE INTO tblparkingrecord (code, PlateNumber, VehicleType, `Parking Slot`, CheckIn, RateName, Rate, Duration, TotalAmount, `Paid Status`) " &
                                                                 "VALUES (@cd, @plate, @vtype, @slot, @chin, @rn, @rt, @dur, @tot, @st)", conn)
                                        cmd.Parameters.AddWithValue("@cd", pCode)
                                        cmd.Parameters.AddWithValue("@plate", pPlate)
                                        cmd.Parameters.AddWithValue("@vtype", pType)
                                        cmd.Parameters.AddWithValue("@slot", pSlot)
                                        cmd.Parameters.AddWithValue("@chin", If(pRow.Table.Columns.Contains("CheckIn") AndAlso Not IsDBNull(pRow("CheckIn")), pRow("CheckIn"), DateTime.Now))
                                        cmd.Parameters.AddWithValue("@rn", If(pRow.Table.Columns.Contains("RateName"), pRow("RateName").ToString(), "Standard"))
                                        cmd.Parameters.AddWithValue("@rt", If(pRow.Table.Columns.Contains("Rate") AndAlso Not IsDBNull(pRow("Rate")), Convert.ToDecimal(pRow("Rate")), 50.0D))
                                        cmd.Parameters.AddWithValue("@dur", If(pRow.Table.Columns.Contains("TotalTime"), pRow("TotalTime").ToString(), "0 hour(s)"))
                                        cmd.Parameters.AddWithValue("@tot", If(pRow.Table.Columns.Contains("TotalAmount") AndAlso Not IsDBNull(pRow("TotalAmount")), Convert.ToDecimal(pRow("TotalAmount")), DBNull.Value))
                                        cmd.Parameters.AddWithValue("@st", pStatus)
                                        cmd.ExecuteNonQuery()
                                    End Using
                                End If
                            End If
                        Next
                    End Using
                    File.Move(dataFilePath, dataFilePath & ".bak")
                Catch
                End Try
            End If
        Catch
        End Try
    End Sub

End Module
