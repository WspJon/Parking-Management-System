Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class ParkingManagerForm
    Dim foundRow As DataRow = Nothing
    Dim currentFee As Decimal = 0D
    Dim penaltyAmount As Decimal = 0D

    ' Kinukuha ang CustomerID o TellerID base sa Username, depende sa role
    Private Function GetCurrentUserID(role As String, username As String) As Object
        Try
            Using conn As MySqlConnection = GetConnection()
                If conn IsNot Nothing Then
                    If role = "Teller" Then
                        Dim cmd As New MySqlCommand("SELECT TellerID FROM tblteller WHERE Username = @user", conn)
                        cmd.Parameters.AddWithValue("@user", username)
                        Dim result = cmd.ExecuteScalar()
                        Return If(result Is Nothing, CType(DBNull.Value, Object), result)

                    ElseIf role = "Customer" Then
                        Dim cmd As New MySqlCommand("SELECT CustomerID FROM tblcustomer WHERE Username = @user", conn)
                        cmd.Parameters.AddWithValue("@user", username)
                        Dim result = cmd.ExecuteScalar()
                        Return If(result Is Nothing, CType(DBNull.Value, Object), result)
                    End If
                End If
            End Using
        Catch ex As MySqlException
        End Try

        Return DBNull.Value
    End Function

    Private Sub ParkingManagerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ParkingData.InitializeDatabase()

        ThemeManager.EnableDrag(pnlHeader, Me)
        ThemeManager.AddMinimizeButton(pnlHeader, Me)

        ThemeManager.MakeRoundedControl(pnlCardCheckIn, 10)
        ThemeManager.MakeRoundedControl(pnlCardCheckOut, 10)
        ThemeManager.MakeRoundedControl(btnPark, 6)
        ThemeManager.MakeRoundedControl(btnSearch, 6)
        ThemeManager.MakeRoundedControl(btnProcessPayment, 6)
        ThemeManager.MakeRoundedControl(btnExit, 6)

        ThemeManager.StyleCardPanel(pnlCardCheckIn, ThemeManager.TealAccent)
        ThemeManager.StyleCardPanel(pnlCardCheckOut, ThemeManager.TealAccent)

        ThemeManager.StyleButton(btnPark, ThemeManager.TealAccent)
        ThemeManager.StyleButton(btnSearch, ThemeManager.TealAccent)
        ThemeManager.StyleButton(btnProcessPayment, ThemeManager.TealAccent)
        ThemeManager.StyleButton(btnExit, ThemeManager.RedExit)

        cmbType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbType.AutoCompleteSource = AutoCompleteSource.ListItems

        cboSearchPlate.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cboSearchPlate.AutoCompleteSource = AutoCompleteSource.CustomSource

        txtPlateNumber.MaxLength = 8
        RefreshParkedPlates()
        UpdateManagerStats()
    End Sub

    Public Sub UpdateManagerStats()
        Dim availCar As Integer = 0
        Dim occCar As Integer = 0
        Dim availMoto As Integer = 0
        Dim occMoto As Integer = 0

        For i As Integer = 1 To 25
            If ParkingData.parkingDatabase($"G{i}") Then availCar += 1 Else occCar += 1
            If ParkingData.parkingDatabase($"U{i}") Then availCar += 1 Else occCar += 1
        Next
        For i As Integer = 26 To 35
            If ParkingData.parkingDatabase($"G{i}") Then availMoto += 1 Else occMoto += 1
            If ParkingData.parkingDatabase($"U{i}") Then availMoto += 1 Else occMoto += 1
        Next

        lblStatAvailCarVal.Text = availCar.ToString()
        lblStatOccCarVal.Text = occCar.ToString()
        lblStatAvailMotoVal.Text = availMoto.ToString()
        lblStatOccMotoVal.Text = occMoto.ToString()
    End Sub

    Public Sub RefreshParkedPlates()
        cboSearchPlate.Items.Clear()
        Dim autoSource As New AutoCompleteStringCollection()

        If ParkingData.ParkingTable IsNot Nothing Then
            For Each row As DataRow In ParkingData.ParkingTable.Rows
                If row("PaidStatus").ToString() = "Not Paid" Then
                    Dim plate As String = row("PlateNumber").ToString()
                    cboSearchPlate.Items.Add(plate)
                    autoSource.Add(plate)
                End If
            Next
        End If

        cboSearchPlate.AutoCompleteCustomSource = autoSource
    End Sub

    Private Sub cmbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbType.SelectedIndexChanged
        cboParkingSlots.Items.Clear()

        lblSlotStatus.Text = "Status: -"
        lblSlotStatus.ForeColor = Color.Black

        If cmbType.Text = "Car" Then
            For i As Integer = 1 To 25
                cboParkingSlots.Items.Add($"G{i}")
                cboParkingSlots.Items.Add($"U{i}")
            Next
        ElseIf cmbType.Text = "Motorcycle" Then
            For i As Integer = 26 To 35
                cboParkingSlots.Items.Add($"G{i}")
                cboParkingSlots.Items.Add($"U{i}")
            Next
        End If
    End Sub

    Private Sub cboParkingSlots_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboParkingSlots.SelectedIndexChanged
        If cboParkingSlots.SelectedItem Is Nothing Then Return

        Dim selectedSlot As String = cboParkingSlots.SelectedItem.ToString()
        Dim isAvailable As Boolean = ParkingData.parkingDatabase(selectedSlot)

        If isAvailable Then
            lblSlotStatus.Text = "AVAILABLE"
            lblSlotStatus.ForeColor = ThemeManager.GreenSuccess
        Else
            Dim isReserved As Boolean = False
            For Each row As DataRow In ParkingData.ParkingTable.Rows
                If row("Slot").ToString() = selectedSlot AndAlso row("PaidStatus").ToString() = "Reserved" Then
                    isReserved = True
                    Exit For
                End If
            Next

            If isReserved Then
                lblSlotStatus.Text = "RESERVED"
                lblSlotStatus.ForeColor = Color.FromArgb(240, 160, 40)
            Else
                lblSlotStatus.Text = "UNAVAILABLE"
                lblSlotStatus.ForeColor = ThemeManager.RedExit
            End If
        End If
    End Sub

    Private Sub ParkingManagerForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        DashBoardForm.ShowLobbyMode = False
        DashBoardForm.LoadDataFromDatabase()
        DashBoardForm.UpdateSlots()
        DashBoardForm.Show()
    End Sub

    Private Sub btnPark_Click(sender As Object, e As EventArgs) Handles btnPark.Click
        If String.IsNullOrWhiteSpace(txtPlateNumber.Text) OrElse cmbType.SelectedItem Is Nothing OrElse cboParkingSlots.SelectedItem Is Nothing Then
            MessageBox.Show("Please fill out all fields and select a slot.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim newPlate As String = DatabaseModule.NormalizePlate(txtPlateNumber.Text)
        If Not DatabaseModule.IsValidPlate(newPlate) Then
            MessageBox.Show("Plate number must be between 4 and 8 alphanumeric characters!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedSlot As String = cboParkingSlots.SelectedItem.ToString()
        Dim isCarSlot As Boolean = DatabaseModule.IsCarSlot(selectedSlot)
        Dim isSelectedCar As Boolean = (cmbType.Text = "Car")

        If isSelectedCar <> isCarSlot Then
            MessageBox.Show($"Slot {selectedSlot} is designated for {If(isCarSlot, "Cars (Four Wheels)", "Motorcycles (Two Wheels)")}. Please choose an appropriate slot.", "Slot Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Using conn As MySqlConnection = GetConnection()
                If conn IsNot Nothing Then
                    ' 1. Check if selected slot is currently occupied in tblparkingrecord
                    Dim checkSlotCmd As New MySqlCommand("SELECT COUNT(*) FROM tblparkingrecord WHERE `Parking Slot` = @slot AND `Paid Status` = 'Not Paid'", conn)
                    checkSlotCmd.Parameters.AddWithValue("@slot", selectedSlot)
                    If Convert.ToInt32(checkSlotCmd.ExecuteScalar()) > 0 Then
                        MessageBox.Show($"Slot {selectedSlot} is already occupied! Please choose another slot.", "Slot Occupied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    ' 2. Check if this plate is already actively parked in tblparkingrecord
                    Dim checkPlateCmd As New MySqlCommand("SELECT `Parking Slot` FROM tblparkingrecord WHERE PlateNumber = @plate AND `Paid Status` = 'Not Paid'", conn)
                    checkPlateCmd.Parameters.AddWithValue("@plate", newPlate)
                    Dim parkedSlotObj = checkPlateCmd.ExecuteScalar()
                    If parkedSlotObj IsNot Nothing Then
                        MessageBox.Show($"Vehicle {newPlate} is already parked in slot {parkedSlotObj}!", "Duplicate Plate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    ' 3. Check if this vehicle has an active reservation
                    Dim checkResCmd As New MySqlCommand("SELECT ReservationID, CustomerID, CustomerName, ParkingSlot, ReservationDate, VehicleType FROM tblreservation WHERE VehiclePlate = @plate AND Status = 'Reserved' ORDER BY ReservationDate ASC", conn)
                    checkResCmd.Parameters.AddWithValue("@plate", newPlate)
                    Dim resDt As New DataTable()
                    Dim da As New MySqlDataAdapter(checkResCmd)
                    da.Fill(resDt)

                    If resDt.Rows.Count > 0 Then
                        Dim resRow As DataRow = resDt.Rows(0)
                        Dim resDate As DateTime = Convert.ToDateTime(resRow("ReservationDate"))

                        If resDate.Date > DateTime.Today Then
                            MessageBox.Show($"This plate has an advance reservation for {resDate.ToString("yyyy-MM-dd")}. Early check-in is not permitted before the reservation date.", "Early Check-In", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim resSlot As String = resRow("ParkingSlot").ToString()
                        Dim resId As Integer = Convert.ToInt32(resRow("ReservationID"))
                        Dim resCustId As Object = resRow("CustomerID")
                        Dim claimMsg As String = $"This plate has an active reservation for Slot {resSlot}."

                        If resSlot <> selectedSlot Then
                            claimMsg &= vbCrLf & $"Reassign reservation from {resSlot} to selected slot {selectedSlot} and check in now?"
                        Else
                            claimMsg &= vbCrLf & "Claim reservation and check in now?"
                        End If

                        If MessageBox.Show(claimMsg, "Reservation Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            ' Check-in within database transaction
                            Using tran As MySqlTransaction = conn.BeginTransaction()
                                Try
                                    Dim updCmd As New MySqlCommand("UPDATE tblreservation SET Status = 'Checked In', ParkingSlot = @slot WHERE ReservationID = @resid AND Status = 'Reserved'", conn, tran)
                                    updCmd.Parameters.AddWithValue("@slot", selectedSlot)
                                    updCmd.Parameters.AddWithValue("@resid", resId)
                                    Dim aff As Integer = updCmd.ExecuteNonQuery()
                                    If aff = 0 Then
                                        tran.Rollback()
                                        MessageBox.Show("Reservation is no longer active or was already checked in.", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    End If

                                    Dim newCode As String = ParkingData.GenerateCode()
                                    Dim vTypeVal As String = DatabaseModule.GetSlotVehicleType(selectedSlot)
                                    Dim rateVal As Decimal = If(DatabaseModule.IsCarSlot(selectedSlot), ParkingData.CarBaseRate, ParkingData.MotorBaseRate)
                                    Dim rateNameVal As String = "Standard"
                                    Dim tellerID As Object = GetCurrentUserID("Teller", Form1.CurrentUsername)

                                    Dim insCmd As New MySqlCommand("INSERT INTO tblparkingrecord (code, PlateNumber, VehicleType, `Parking Slot`, RateName, Rate, Duration, `Paid Status`, CheckIn, ReservationID, CustomerID, TellerID, ProcessedByName) " &
                                        "VALUES (@code, @plate, @vtype, @slot, @ratename, @rate, '0 hour(s)', 'Not Paid', @checkin, @resid, @custid, @tellid, @proc)", conn, tran)
                                    insCmd.Parameters.AddWithValue("@code", newCode)
                                    insCmd.Parameters.AddWithValue("@plate", newPlate)
                                    insCmd.Parameters.AddWithValue("@vtype", vTypeVal)
                                    insCmd.Parameters.AddWithValue("@slot", selectedSlot)
                                    insCmd.Parameters.AddWithValue("@ratename", rateNameVal)
                                    insCmd.Parameters.AddWithValue("@rate", rateVal)
                                    insCmd.Parameters.AddWithValue("@checkin", DateTime.Now)
                                    insCmd.Parameters.AddWithValue("@resid", resId)
                                    insCmd.Parameters.AddWithValue("@custid", resCustId)
                                    insCmd.Parameters.AddWithValue("@tellid", tellerID)
                                    insCmd.Parameters.AddWithValue("@proc", Form1.CurrentFullName)
                                    insCmd.ExecuteNonQuery()

                                    tran.Commit()

                                    DashBoardForm.LoadDataFromDatabase()
                                    DashBoardForm.UpdateSlots()
                                    UpdateManagerStats()
                                    RefreshParkedPlates()

                                    SuccessDialogForm.ShowSuccess($"Reservation claimed! Vehicle {newPlate} is checked in at slot {selectedSlot}.")

                                    txtPlateNumber.Clear()
                                    cmbType.SelectedIndex = -1
                                    cboParkingSlots.SelectedIndex = -1
                                    lblSlotStatus.Text = "Status: -"
                                    lblSlotStatus.ForeColor = Color.Black
                                    Exit Sub
                                Catch exTran As Exception
                                    tran.Rollback()
                                    MessageBox.Show("Error claiming reservation: " & exTran.Message, "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End Try
                            End Using
                        Else
                            Exit Sub
                        End If
                    End If

                    ' 4. If no reservation for this plate: verify selected slot is not reserved today for someone else
                    Dim checkOtherResCmd As New MySqlCommand("SELECT VehiclePlate FROM tblreservation WHERE ParkingSlot = @slot AND Status = 'Reserved' AND ReservationDate = @today", conn)
                    checkOtherResCmd.Parameters.AddWithValue("@slot", selectedSlot)
                    checkOtherResCmd.Parameters.AddWithValue("@today", DateTime.Today)
                    Dim otherPlateObj = checkOtherResCmd.ExecuteScalar()
                    If otherPlateObj IsNot Nothing Then
                        MessageBox.Show($"Slot {selectedSlot} is reserved for another vehicle ({otherPlateObj}) today. Please choose a different slot.", "Slot Reserved", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    ' 5. Confirm ordinary check-in
                    Dim confirmPark As New ConfirmActionDialogForm("Confirm Check-In", $"Are you sure you want to park plate '{newPlate}' at slot {selectedSlot}?", "YES")
                    If OverlayHelper.ShowDialog(Me, confirmPark) <> DialogResult.Yes Then
                        Exit Sub
                    End If

                    Dim newCodeOrd As String = ParkingData.GenerateCode()
                    Dim vTypeValOrd As String = DatabaseModule.GetSlotVehicleType(selectedSlot)
                    Dim rateValOrd As Decimal = If(DatabaseModule.IsCarSlot(selectedSlot), ParkingData.CarBaseRate, ParkingData.MotorBaseRate)
                    Dim rateNameValOrd As String = "Standard"
                    Dim tellerIDOrd As Object = GetCurrentUserID("Teller", Form1.CurrentUsername)

                    Dim insOrdCmd As New MySqlCommand("INSERT INTO tblparkingrecord (code, PlateNumber, VehicleType, `Parking Slot`, RateName, Rate, Duration, `Paid Status`, CheckIn, CustomerID, TellerID, ProcessedByName) " &
                        "VALUES (@code, @plate, @vtype, @slot, @ratename, @rate, '0 hour(s)', 'Not Paid', @checkin, @custid, @tellid, @proc)", conn)
                    insOrdCmd.Parameters.AddWithValue("@code", newCodeOrd)
                    insOrdCmd.Parameters.AddWithValue("@plate", newPlate)
                    insOrdCmd.Parameters.AddWithValue("@vtype", vTypeValOrd)
                    insOrdCmd.Parameters.AddWithValue("@slot", selectedSlot)
                    insOrdCmd.Parameters.AddWithValue("@ratename", rateNameValOrd)
                    insOrdCmd.Parameters.AddWithValue("@rate", rateValOrd)
                    insOrdCmd.Parameters.AddWithValue("@checkin", DateTime.Now)
                    insOrdCmd.Parameters.AddWithValue("@custid", DBNull.Value)
                    insOrdCmd.Parameters.AddWithValue("@tellid", tellerIDOrd)
                    insOrdCmd.Parameters.AddWithValue("@proc", Form1.CurrentFullName)
                    insOrdCmd.ExecuteNonQuery()

                    DashBoardForm.LoadDataFromDatabase()
                    DashBoardForm.UpdateSlots()
                    UpdateManagerStats()
                    RefreshParkedPlates()

                    SuccessDialogForm.ShowSuccess($"{cmbType.Text} successfully parked at slot {selectedSlot}.")

                    txtPlateNumber.Clear()
                    cmbType.SelectedIndex = -1
                    cboParkingSlots.SelectedIndex = -1
                    lblSlotStatus.Text = "Status: -"
                    lblSlotStatus.ForeColor = Color.Black
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error processing check-in: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If cboSearchPlate.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a plate number from the dropdown first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim previousPlate As String = If(foundRow IsNot Nothing, foundRow("PlateNumber").ToString(), "")
        foundRow = Nothing
        Dim searchPlate As String = cboSearchPlate.SelectedItem.ToString()

        For Each row As DataRow In ParkingData.ParkingTable.Rows
            If row("PlateNumber").ToString() = searchPlate AndAlso row("PaidStatus").ToString() = "Not Paid" Then
                foundRow = row
                Exit For
            End If
        Next

        If foundRow IsNot Nothing Then
            If previousPlate.Trim().ToLower() <> searchPlate.Trim().ToLower() Then
                cmbDiscount.Text = "None"
                txtAmountPaid.Clear()
                lblChange.Text = "Change: ₱0.00"
            End If
            CalculateAndDisplayFee()
        Else
            MessageBox.Show("Vehicle not found or already checked out.", "No Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            lblFee.Text = "Total Fee: ₱0.00"
            currentFee = 0.0
            penaltyAmount = 0.0
        End If
    End Sub

    Private Sub CalculateAndDisplayFee()
        If foundRow IsNot Nothing Then
            Dim timeIn As DateTime = Convert.ToDateTime(foundRow("CheckIn"))
            Dim hours As Double = Math.Ceiling((DateTime.Now - timeIn).TotalHours)
            If hours < 1 Then hours = 1

            Dim isCar As Boolean = (foundRow("VehicleType").ToString() = "Four Wheels")
            Dim baseRate As Decimal = If(isCar, ParkingData.CarBaseRate, ParkingData.MotorBaseRate)
            Dim succeedingRate As Decimal = If(isCar, ParkingData.CarSucceedingRate, ParkingData.MotorSucceedingRate)

            Dim totalCalculatedFee As Decimal = baseRate
            If hours > ParkingData.FreeHours Then
                totalCalculatedFee = baseRate + (Convert.ToDecimal(hours - ParkingData.FreeHours) * succeedingRate)
            End If

            penaltyAmount = 0D
            Dim slotName As String = foundRow("Slot").ToString()
            For Each row As DataRow In ParkingData.ParkingTable.Rows
                If row("Slot").ToString() = slotName AndAlso row("PaidStatus").ToString() = "Reserved" Then
                    If Not IsDBNull(row("ReservationDate")) AndAlso Convert.ToDateTime(row("ReservationDate")).Date = DateTime.Now.Date Then
                        penaltyAmount = ParkingData.OverstayPenaltyFee
                        Exit For
                    End If
                End If
            Next

            totalCalculatedFee += penaltyAmount

            If cmbDiscount.Text = "PWD" Then
                currentFee = 0D
            ElseIf cmbDiscount.Text = "Senior Citizen" Then
                currentFee = Math.Round(totalCalculatedFee * 0.8D, 2)
            Else
                currentFee = totalCalculatedFee
            End If

            lblFee.Text = "Total Fee: ₱" & currentFee.ToString("F2")
        End If
    End Sub

    Private Sub cmbDiscount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDiscount.SelectedIndexChanged
        CalculateAndDisplayFee()
    End Sub

    Private Sub btnProcessPayment_Click(sender As Object, e As EventArgs) Handles btnProcessPayment.Click
        If foundRow Is Nothing Then
            MessageBox.Show("Please search for a parked vehicle first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        CalculateAndDisplayFee()

        Dim cashPaid As Decimal = 0D
        If Not Decimal.TryParse(txtAmountPaid.Text, cashPaid) OrElse cashPaid < 0D Then
            MessageBox.Show("Please enter a valid numeric payment amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If cashPaid < currentFee Then
            Dim shortage As Decimal = currentFee - cashPaid
            MessageBox.Show("Insufficient payment! The customer still owes ₱" & shortage.ToString("F2"), "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim calculatedChange As Decimal = cashPaid - currentFee
        lblChange.Text = "Change: ₱" & calculatedChange.ToString("F2")

        Dim confirmPay As New ConfirmActionDialogForm("Process Payment", $"Confirm payment of ₱{currentFee.ToString("F2")} for plate '{foundRow("PlateNumber")}'?", "YES")
        If OverlayHelper.ShowDialog(Me, confirmPay) <> DialogResult.Yes Then
            Exit Sub
        End If

        Dim timeIn As DateTime = Convert.ToDateTime(foundRow("CheckIn"))
        Dim timeOut As DateTime = DateTime.Now
        Dim hours As Double = Math.Ceiling((timeOut - timeIn).TotalHours)
        If hours < 1 Then hours = 1

        Dim plateStr As String = foundRow("PlateNumber").ToString()
        Dim timeInStr As String = timeIn.ToString("hh:mm tt")
        Dim timeOutStr As String = timeOut.ToString("hh:mm tt")
        Dim hoursStr As String = hours & " hr(s)"
        Dim isCar As Boolean = (foundRow("VehicleType").ToString() = "Four Wheels")
        Dim baseRate As Decimal = If(isCar, ParkingData.CarBaseRate, ParkingData.MotorBaseRate)
        Dim baseStr As String = "₱" & baseRate.ToString("F2") & " / hr"
        Dim discountStr As String = cmbDiscount.Text
        If discountStr = "None" Then discountStr = "-"

        Dim transId As Object = foundRow("TransactionID")
        Dim recordCode As String = foundRow("Code").ToString()
        Dim processedTellerID As Object = GetCurrentUserID("Teller", Form1.CurrentUsername)

        ' Commit payment to database first
        Try
            Using conn As MySqlConnection = GetConnection()
                If conn IsNot Nothing Then
                    Dim updCmd As New MySqlCommand("UPDATE tblparkingrecord SET CheckOut = @checkout, TotalAmount = @amount, Duration = @time, `Paid Status` = 'Paid', DiscountType = @disc, PenaltyAmount = @pen, AmountPaid = @paid, ChangeAmount = @change, ProcessedByName = @proc, TellerID = @tellid WHERE TransactionID = @transId", conn)
                    updCmd.Parameters.AddWithValue("@checkout", timeOut)
                    updCmd.Parameters.AddWithValue("@amount", currentFee)
                    updCmd.Parameters.AddWithValue("@time", hoursStr)
                    updCmd.Parameters.AddWithValue("@disc", If(cmbDiscount.Text = "None", DBNull.Value, CObj(cmbDiscount.Text)))
                    updCmd.Parameters.AddWithValue("@pen", penaltyAmount)
                    updCmd.Parameters.AddWithValue("@paid", cashPaid)
                    updCmd.Parameters.AddWithValue("@change", calculatedChange)
                    updCmd.Parameters.AddWithValue("@proc", Form1.CurrentFullName)
                    updCmd.Parameters.AddWithValue("@tellid", processedTellerID)
                    updCmd.Parameters.AddWithValue("@transId", transId)
                    Dim affected As Integer = updCmd.ExecuteNonQuery()
                    If affected = 0 Then
                        MessageBox.Show("Record could not be updated or was already checked out.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Payment transaction failed: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        DashBoardForm.LoadDataFromDatabase()
        DashBoardForm.UpdateSlots()
        UpdateManagerStats()

        ' Show final receipt dialog after database commit
        Dim checkoutDlg As New CheckoutDialog(plateStr, timeInStr, timeOutStr, hoursStr, baseStr, discountStr, penaltyAmount.ToString("F2"), currentFee.ToString("F2"), cashPaid.ToString("F2"), calculatedChange.ToString("F2"))
        OverlayHelper.ShowDialog(Me, checkoutDlg)

        cboSearchPlate.Items.Remove(plateStr)
        If cboSearchPlate.AutoCompleteCustomSource.Contains(plateStr) Then
            cboSearchPlate.AutoCompleteCustomSource.Remove(plateStr)
        End If

        cboSearchPlate.SelectedIndex = -1
        cboSearchPlate.Text = ""
        txtAmountPaid.Clear()
        lblFee.Text = "Total Fee: ₱0.00"
        lblChange.Text = "Change: ₱0.00"
        cmbDiscount.SelectedIndex = 0
        foundRow = Nothing
        currentFee = 0D
        penaltyAmount = 0D

        RefreshParkedPlates()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        DashBoardForm.ShowLobbyMode = False
        DashBoardForm.LoadDataFromDatabase()
        DashBoardForm.UpdateSlots()
        DashBoardForm.Show()
        Me.Close()
    End Sub
End Class