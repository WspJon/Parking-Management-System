Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data
Imports LocalDb.Data.LocalDbClient

Public Class ParkingManagerForm
    Dim foundRow As DataRow = Nothing
    Dim currentFee As Double = 0.0
    Dim penaltyAmount As Double = 0.0

    ' Kinukuha ang CustomerID o TellerID base sa Username, depende sa role
    Private Function GetCurrentUserID(role As String, username As String) As Object
        Try
            Using conn As LocalDbConnection = GetConnection()
                If conn IsNot Nothing Then
                    If role = "Teller" Then
                        Dim cmd As New LocalDbCommand("SELECT TellerID FROM tblteller WHERE Username = @user", conn)
                        cmd.Parameters.AddWithValue("@user", username)
                        Dim result = cmd.ExecuteScalar()
                        Return If(result Is Nothing, CType(DBNull.Value, Object), result)

                    ElseIf role = "Customer" Then
                        Dim cmd As New LocalDbCommand("SELECT CustomerID FROM tblcustomer WHERE Username = @user", conn)
                        cmd.Parameters.AddWithValue("@user", username)
                        Dim result = cmd.ExecuteScalar()
                        Return If(result Is Nothing, CType(DBNull.Value, Object), result)
                    End If
                End If
            End Using
        Catch ex As LocalDbException
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
        If Form1.CurrentUserRole = "Teller" Then
            frmadmindashboard.ShowLobbyMode = False
            frmadmindashboard.UpdateSlots()
            frmadmindashboard.Show()
        Else
            DashBoardForm.ShowLobbyMode = False
            DashBoardForm.UpdateSlots()
            DashBoardForm.Show()
        End If
    End Sub

    Private Sub btnPark_Click(sender As Object, e As EventArgs) Handles btnPark.Click
        ParkingData.InitializeDatabase()

        If txtPlateNumber.Text = "" Or cmbType.Text = "" Or cboParkingSlots.SelectedItem Is Nothing Then
            MessageBox.Show("Please fill out all fields and select a slot.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If txtPlateNumber.Text.Trim().Length < 4 OrElse txtPlateNumber.Text.Trim().Length > 8 Then
            MessageBox.Show("Plate number must be between 4 and 8 characters!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedSlot As String = cboParkingSlots.SelectedItem.ToString()
        If ParkingData.parkingDatabase(selectedSlot) = False Then
            MessageBox.Show("This slot is already occupied! Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim newPlate As String = txtPlateNumber.Text.ToUpper().Trim()

        For Each r As DataRow In ParkingData.ParkingTable.Rows
            If r("Slot").ToString() = selectedSlot AndAlso r("PaidStatus").ToString() = "Reserved" Then
                If Not IsDBNull(r("ReservationDate")) AndAlso Convert.ToDateTime(r("ReservationDate")).Date = DateTime.Now.Date Then
                    If r("PlateNumber").ToString().ToUpper() <> newPlate Then
                        MessageBox.Show("This slot is reserved for another vehicle today. Please choose a different slot.", "Slot Reserved", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If
            End If
        Next

        If ParkingData.ParkingTable IsNot Nothing Then
            For Each existing As DataRow In ParkingData.ParkingTable.Rows
                If existing("PlateNumber").ToString().ToUpper() = newPlate Then
                    If existing("PaidStatus").ToString() = "Reserved" Then
                        Dim resSlot As String = existing("Slot").ToString()
                        Dim claimMsg As String = "This plate has an ADVANCE RESERVATION for Slot " & resSlot & "."
                        If resSlot <> selectedSlot Then
                            claimMsg &= vbCrLf & "You have selected a different slot (" & selectedSlot & "). Reassign reservation to " & selectedSlot & " and check-in now?"
                        Else
                            claimMsg &= vbCrLf & "Claim this reservation now?"
                        End If

                        If MessageBox.Show(claimMsg, "Reservation Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            existing("Slot") = selectedSlot
                            existing("PaidStatus") = "Not Paid"
                            existing("CheckIn") = DateTime.Now
                            ParkingData.parkingDatabase(selectedSlot) = False

                            DataStore.SaveDatabase()

                            ' --- LocalDb Sync: I-update ang reservation record sa LocalStore ---
                            Try
                                Using conn As LocalDbConnection = GetConnection()
                                    If conn IsNot Nothing Then
                                        Dim updCmd As New LocalDbCommand("UPDATE tblparkingrecord SET `Parking Slot` = @slot, `Paid Status` = 'Not Paid', CheckIn = @checkin WHERE code = @code", conn)
                                        updCmd.Parameters.AddWithValue("@slot", selectedSlot)
                                        updCmd.Parameters.AddWithValue("@checkin", DateTime.Now)
                                        updCmd.Parameters.AddWithValue("@code", existing("Code").ToString())
                                        updCmd.ExecuteNonQuery()
                                    End If
                                End Using
                            Catch ex As LocalDbException
                                MessageBox.Show("Warning: Hindi na-sync sa database. " & ex.Message, "Database Sync Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End Try

                            If Form1.CurrentUserRole = "Teller" Then
                                frmadmindashboard.UpdateSlots()
                            Else
                                DashBoardForm.UpdateSlots()
                            End If
                            UpdateManagerStats()

                            MessageBox.Show("Reservation successfully claimed! Vehicle is now checked in at slot " & selectedSlot, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            txtPlateNumber.Clear()
                            cmbType.SelectedIndex = -1
                            cboParkingSlots.SelectedIndex = -1
                            lblSlotStatus.Text = "Status: -"
                            lblSlotStatus.ForeColor = Color.Black
                            RefreshParkedPlates()
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    ElseIf existing("PaidStatus").ToString() = "Not Paid" Then
                        MessageBox.Show("Duplicate plate number is not possible.", "Duplicate Plate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If
            Next
        End If

        Dim confirmPark As New ConfirmActionDialogForm("Confirm Check-In", $"Are you sure you want to park plate '{newPlate}' at slot {selectedSlot}?", "YES")
        If OverlayHelper.ShowDialog(Me, confirmPark) <> DialogResult.Yes Then
            Exit Sub
        End If

        ParkingData.parkingDatabase(selectedSlot) = False
        lblSlotStatus.Text = "UNAVAILABLE"
        lblSlotStatus.ForeColor = ThemeManager.RedExit

        Dim newCode As String = ParkingData.GenerateCode()
        Dim checkInTime As DateTime = DateTime.Now
        Dim vehicleTypeVal As String = If(cmbType.Text = "Car", "Four Wheels", "Two Wheels")
        Dim rateNameVal As String = If(cmbType.Text = "Car", ParkingData.CarBaseRate & " per Hour", ParkingData.MotorBaseRate & " per Hour")
        Dim rateVal As Double = If(cmbType.Text = "Car", ParkingData.CarBaseRate, ParkingData.MotorBaseRate)

        Dim newRow As DataRow = ParkingData.ParkingTable.NewRow()
        newRow("Code") = newCode
        newRow("PlateNumber") = txtPlateNumber.Text.ToUpper()
        newRow("CheckIn") = checkInTime
        newRow("CheckOut") = DBNull.Value
        newRow("VehicleType") = vehicleTypeVal
        newRow("RateName") = rateNameVal
        newRow("Rate") = rateVal
        newRow("Slot") = selectedSlot
        newRow("TotalTime") = "0 hour(s)"
        newRow("TotalAmount") = DBNull.Value
        newRow("PaidStatus") = "Not Paid"

        ParkingData.ParkingTable.Rows.Add(newRow)

        ' --- LocalDb Sync: I-insert sa LocalStore (`tblparkingrecord`) ---
        Try
            Dim tellerID As Object = DBNull.Value
            Dim customerID As Object = DBNull.Value

            If Form1.CurrentUserRole = "Teller" Then
                tellerID = GetCurrentUserID("Teller", Form1.CurrentUsername)
            ElseIf Form1.CurrentUserRole = "Customer" Then
                customerID = GetCurrentUserID("Customer", Form1.CurrentUsername)
            End If

            Using conn As LocalDbConnection = GetConnection()
                If conn IsNot Nothing Then
                    Dim insCmd As New LocalDbCommand("INSERT INTO tblparkingrecord (code, PlateNumber, VehicleType, `Parking Slot`, CheckIn, RateName, Rate, Duration, `Paid Status`, CustomerID, TellerID) " &
                        "VALUES (@code, @plate, @vtype, @slot, @checkin, @ratename, @rate, @totaltime, @paidstatus, @custid, @tellid)", conn)
                    insCmd.Parameters.AddWithValue("@code", newCode)
                    insCmd.Parameters.AddWithValue("@plate", txtPlateNumber.Text.ToUpper())
                    insCmd.Parameters.AddWithValue("@vtype", vehicleTypeVal)
                    insCmd.Parameters.AddWithValue("@slot", selectedSlot)
                    insCmd.Parameters.AddWithValue("@checkin", checkInTime)
                    insCmd.Parameters.AddWithValue("@ratename", rateNameVal)
                    insCmd.Parameters.AddWithValue("@rate", rateVal)
                    insCmd.Parameters.AddWithValue("@totaltime", "0 hour(s)")
                    insCmd.Parameters.AddWithValue("@paidstatus", "Not Paid")
                    insCmd.Parameters.AddWithValue("@custid", customerID)
                    insCmd.Parameters.AddWithValue("@tellid", tellerID)
                    insCmd.ExecuteNonQuery()
                End If
            End Using
        Catch ex As LocalDbException
            MessageBox.Show("Warning: Hindi na-sync sa database. " & ex.Message, "Database Sync Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        If Form1.CurrentUserRole = "Teller" Then
            frmadmindashboard.UpdateSlots()
        Else
            DashBoardForm.UpdateSlots()
        End If
        DataStore.SaveDatabase()

        SuccessDialogForm.ShowSuccess(cmbType.Text & " successfully parked at slot " & cboParkingSlots.Text)

        txtPlateNumber.Clear()
        cmbType.SelectedIndex = -1
        cboParkingSlots.SelectedIndex = -1
        lblSlotStatus.Text = "Status: -"
        lblSlotStatus.ForeColor = Color.Black

        UpdateManagerStats()
        RefreshParkedPlates()
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
            Dim baseRate As Double = If(isCar, ParkingData.CarBaseRate, ParkingData.MotorBaseRate)
            Dim succeedingRate As Double = If(isCar, ParkingData.CarSucceedingRate, ParkingData.MotorSucceedingRate)

            Dim totalCalculatedFee As Double = baseRate
            If hours > ParkingData.FreeHours Then
                totalCalculatedFee = baseRate + ((hours - ParkingData.FreeHours) * succeedingRate)
            End If

            penaltyAmount = 0.0
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
                currentFee = 0.0
            ElseIf cmbDiscount.Text = "Senior Citizen" Then
                currentFee = totalCalculatedFee * 0.8
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

        Dim cashPaid As Double = 0.0

        If Not Double.TryParse(txtAmountPaid.Text, cashPaid) OrElse cashPaid < 0 Then
            MessageBox.Show("Please enter a valid numeric payment amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If cashPaid < currentFee Then
            Dim shortage As Double = currentFee - cashPaid
            MessageBox.Show("Insufficient payment! The customer still owes ₱" & shortage.ToString("F2"), "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim calculatedChange As Double = cashPaid - currentFee
        lblChange.Text = "Change: ₱" & calculatedChange.ToString("F2")

        Dim timeIn As DateTime = Convert.ToDateTime(foundRow("CheckIn"))
        Dim timeOut As DateTime = DateTime.Now
        Dim hours As Double = Math.Ceiling((timeOut - timeIn).TotalHours)
        If hours < 1 Then hours = 1

        Dim plateStr As String = foundRow("PlateNumber").ToString()
        Dim timeInStr As String = timeIn.ToString("hh:mm tt")
        Dim timeOutStr As String = timeOut.ToString("hh:mm tt")
        Dim hoursStr As String = hours & " hr(s)"

        Dim isCar As Boolean = (foundRow("VehicleType").ToString() = "Four Wheels")
        Dim baseRate As Double = If(isCar, ParkingData.CarBaseRate, ParkingData.MotorBaseRate)
        Dim baseStr As String = "₱" & baseRate.ToString("F2") & " / hr"

        Dim discountStr As String = cmbDiscount.Text
        If discountStr = "None" Then discountStr = "-"

        Dim checkoutDlg As New CheckoutDialog(plateStr, timeInStr, timeOutStr, hoursStr, baseStr, discountStr, penaltyAmount.ToString("F2"), currentFee.ToString("F2"), cashPaid.ToString("F2"), calculatedChange.ToString("F2"))

        If OverlayHelper.ShowDialog(Me, checkoutDlg) = DialogResult.OK Then
            Dim recordCode As String = foundRow("Code").ToString()

            foundRow("CheckOut") = timeOut
            foundRow("TotalAmount") = currentFee
            foundRow("TotalTime") = hours & " hour(s)"
            foundRow("PaidStatus") = "Paid"
            foundRow("ProcessedBy") = Form1.CurrentFullName

            ' --- LocalDb Sync: I-update sa LocalStore bilang 'Paid' ---
            Try
                Dim processedTellerID As Object = DBNull.Value
                If Form1.CurrentUserRole = "Teller" Then
                    processedTellerID = GetCurrentUserID("Teller", Form1.CurrentUsername)
                End If

                Using conn As LocalDbConnection = GetConnection()
                    If conn IsNot Nothing Then
                        Dim updCmd As New LocalDbCommand("UPDATE tblparkingrecord SET CheckOut = @checkout, TotalAmount = @amount, Duration = @time, `Paid Status` = 'Paid', TellerID = @tellid WHERE code = @code", conn)
                        updCmd.Parameters.AddWithValue("@checkout", timeOut)
                        updCmd.Parameters.AddWithValue("@amount", currentFee)
                        updCmd.Parameters.AddWithValue("@time", hours & " hour(s)")
                        updCmd.Parameters.AddWithValue("@tellid", processedTellerID)
                        updCmd.Parameters.AddWithValue("@code", recordCode)
                        updCmd.ExecuteNonQuery()
                    End If
                End Using
            Catch ex As LocalDbException
                MessageBox.Show("Warning: Hindi na-sync sa database. " & ex.Message, "Database Sync Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            If Form1.CurrentUserRole = "Teller" Then
                frmadmindashboard.UpdateSlots()
            Else
                DashBoardForm.UpdateSlots()
            End If

            Dim freedSlot As String = foundRow("Slot").ToString()

            If ParkingData.parkingDatabase.ContainsKey(freedSlot) Then
                ParkingData.parkingDatabase(freedSlot) = True
            End If

            DataStore.SaveDatabase()

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
            currentFee = 0.0

            RefreshParkedPlates()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If Form1.CurrentUserRole = "Teller" Then
            frmadmindashboard.Show()
        Else
            DashBoardForm.Show()
        End If
        Me.Close()
    End Sub
End Class