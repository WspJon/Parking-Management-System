Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class DashBoardForm
    Public ShowLobbyMode As Boolean = False
    Private currentReservingSlot As String = ""
    Private Const EM_SETCUEBANNER As Integer = &H1501

    <System.Runtime.InteropServices.DllImport("user32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)> lParam As String) As IntPtr
    End Function

    ' Kinukuha ang CustomerID o TellerID base sa Username, depende sa role
    Public Function GetCurrentUserID(role As String, username As String) As Object
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

    ' Function para i-load ang data mula parking_db papuntang ParkingData.ParkingTable
    Public Sub LoadDataFromDatabase()
        Try
            Using conn As MySqlConnection = GetConnection()
                If conn IsNot Nothing Then
                    Dim query As String = "SELECT p.TransactionID, p.code AS Code, p.PlateNumber, p.CheckIn, p.CheckOut, " &
                                         "p.VehicleType, p.RateName, p.Rate, p.`Parking Slot` AS Slot, " &
                                         "p.Duration AS TotalTime, p.TotalAmount, p.`Paid Status` AS PaidStatus, " &
                                         "p.DiscountType, p.PenaltyAmount, p.AmountPaid, p.ChangeAmount, " &
                                         "p.ReservationID, p.CustomerID, p.TellerID, " &
                                         "COALESCE(c.Fullname, '') AS CustomerName, " &
                                         "COALESCE(p.ProcessedByName, t.FullName, '') AS ProcessedBy " &
                                         "FROM tblparkingrecord p " &
                                         "LEFT JOIN tblcustomer c ON p.CustomerID = c.CustomerID " &
                                         "LEFT JOIN tblteller t ON p.TellerID = t.TellerID " &
                                         "ORDER BY p.TransactionID DESC"

                    Dim adapter As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    If ParkingData.ParkingTable IsNot Nothing Then
                        ParkingData.ParkingTable.Clear()
                        ParkingData.ParkingTable.Merge(dt)
                    Else
                        ParkingData.ParkingTable = dt
                    End If
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading records from database: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetActiveReservations() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As MySqlConnection = GetConnection()
                If conn IsNot Nothing Then
                    Dim query As String = "SELECT r.ReservationID, r.CustomerID, COALESCE(r.CustomerName, c.Fullname, '') AS CustomerName, " &
                                         "r.ParkingSlot, r.VehiclePlate, r.VehicleType, r.ReservationDate, r.StartTime, r.EndTime, r.Status, r.CreatedAt " &
                                         "FROM tblreservation r " &
                                         "LEFT JOIN tblcustomer c ON r.CustomerID = c.CustomerID " &
                                         "WHERE r.Status = 'Reserved' " &
                                         "ORDER BY r.ReservationDate ASC, r.ReservationID ASC"
                    Dim adapter As New MySqlDataAdapter(query, conn)
                    adapter.Fill(dt)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading reservations: " & ex.Message, "Reservation Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        Return dt
    End Function

    Public Sub ConfigureRoleInterface()
        If Form1.CurrentUserRole = "Admin" Then
            lblSidebarTitle.Text = "Admin POS System"
            pnlStatsSales.Visible = True
            lblTotalSales.Visible = True
            btnManage.Visible = True
            btnSettings.Visible = True
            Button1.Visible = True
            pnlTabBar.Visible = True
            btnTabAccounts.Visible = True
            btnClearLog.Visible = True
            btnClearAccounts.Visible = True
        ElseIf Form1.CurrentUserRole = "Teller" Then
            lblSidebarTitle.Text = "Teller POS System"
            pnlStatsSales.Visible = False
            lblTotalSales.Visible = False
            btnManage.Visible = True
            btnSettings.Visible = False
            Button1.Visible = False
            pnlTabBar.Visible = True
            btnTabAccounts.Visible = False
            btnClearLog.Visible = False
            btnClearAccounts.Visible = False
            If pnlAccountsView IsNot Nothing AndAlso pnlAccountsView.Visible Then
                pnlMapView.Visible = True
                pnlTableView.Visible = False
                pnlAccountsView.Visible = False
            End If
        Else
            lblSidebarTitle.Text = If(Form1.CurrentUserRole = "Customer", "Customer Lobby", "Public Live Map")
            pnlStatsSales.Visible = False
            lblTotalSales.Visible = False
            btnManage.Visible = False
            btnSettings.Visible = False
            Button1.Visible = False
            pnlTabBar.Visible = False
            pnlTableView.Visible = False
            pnlAccountsView.Visible = False
            pnlMapView.Visible = True
            btnTabAccounts.Visible = False
            btnClearLog.Visible = False
            btnClearAccounts.Visible = False
        End If
    End Sub

    Private Sub DashBoardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureRoleInterface()
        ' I-load muna ang database data bago mag-update ng UI
        LoadDataFromDatabase()

        AddHandler btnExport.Click, Sub()
                                        If dgvVehicles.Rows.Count = 0 Then
                                            MessageBox.Show("No data to export.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Return
                                        End If

                                        Using sfd As New SaveFileDialog()
                                            sfd.Filter = "CSV Excel File (*.csv)|*.csv"
                                            sfd.FileName = "Parking_Transaction_Log_" & DateTime.Now.ToString("yyyyMMdd") & ".csv"
                                            If sfd.ShowDialog() = DialogResult.OK Then
                                                Try
                                                    Dim sb As New System.Text.StringBuilder()

                                                    Dim headers As New List(Of String)()
                                                    For Each col As DataGridViewColumn In dgvVehicles.Columns
                                                        If col.Visible Then
                                                            headers.Add("""" & col.HeaderText.Replace("""", """""") & """")
                                                        End If
                                                    Next
                                                    sb.AppendLine(String.Join(",", headers))

                                                    For Each row As DataGridViewRow In dgvVehicles.Rows
                                                        If Not row.IsNewRow Then
                                                            Dim cells As New List(Of String)()
                                                            For Each cell As DataGridViewCell In row.Cells
                                                                If dgvVehicles.Columns(cell.ColumnIndex).Visible Then
                                                                    Dim val As String = If(cell.Value IsNot Nothing, cell.Value.ToString(), "")
                                                                    cells.Add("""" & val.Replace("""", """""") & """")
                                                                End If
                                                            Next
                                                            sb.AppendLine(String.Join(",", cells))
                                                        End If
                                                    Next

                                                    System.IO.File.WriteAllText(sfd.FileName, sb.ToString())
                                                    MessageBox.Show("Successfully exported to Excel!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                Catch ex As Exception
                                                    MessageBox.Show("Error exporting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                End Try
                                            End If
                                        End Using
                                    End Sub

        AddHandler btnClearLog.Click, Sub()
                                          If Form1.CurrentUserRole <> "Admin" Then
                                              MessageBox.Show("Only administrators can clear completed parking records.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Return
                                          End If

                                          Dim clearDlg As New ClearDialogForm()
                                          If OverlayHelper.ShowDialog(Me, clearDlg) = DialogResult.Yes Then
                                              Try
                                                  Dim affected As Integer = 0
                                                  Using conn As MySqlConnection = GetConnection()
                                                      If conn IsNot Nothing Then
                                                          Dim delCmd As New MySqlCommand("DELETE FROM tblparkingrecord WHERE `Paid Status` = 'Paid'", conn)
                                                          affected = delCmd.ExecuteNonQuery()
                                                      End If
                                                  End Using

                                                  LoadDataFromDatabase()
                                                  UpdateSlots()

                                                  If affected > 0 Then
                                                      SuccessDialogForm.ShowSuccess("Completed parking transactions cleared successfully.")
                                                  Else
                                                      MessageBox.Show("There are no completed transactions to clear.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                  End If
                                              Catch ex As MySqlException
                                                  MessageBox.Show("Error clearing records from database: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              End Try
                                          End If
                                      End Sub

        AddHandler Me.HandleCreated, Sub() SendMessage(txtSearchLog.Handle, EM_SETCUEBANNER, 0, "Search...")

        Dim applyFilters As Action = Sub()
                                         If ParkingData.ParkingTable IsNot Nothing Then
                                             Dim filters As New List(Of String)

                                             Dim fromDate As String = dtpFrom.Value.ToString("yyyy-MM-dd")
                                             Dim toDate As String = dtpTo.Value.AddDays(1).ToString("yyyy-MM-dd")
                                             filters.Add($"CheckIn >= '{fromDate}' AND CheckIn < '{toDate}'")

                                             Dim searchText As String = txtSearchLog.Text.Trim().Replace("'", "''").Replace("[", "[[]").Replace("]", "[]]").Replace("*", "[*]").Replace("%", "[%]")
                                             If searchText <> "" Then
                                                 filters.Add($"(PlateNumber LIKE '%{searchText}%' OR Slot LIKE '%{searchText}%' OR PaidStatus LIKE '%{searchText}%')")
                                             End If

                                             Try
                                                 ParkingData.ParkingTable.DefaultView.RowFilter = String.Join(" AND ", filters)
                                             Catch ex As Exception
                                             End Try
                                         End If
                                     End Sub

        AddHandler dtpFrom.ValueChanged, Sub() applyFilters()
        AddHandler dtpTo.ValueChanged, Sub() applyFilters()
        AddHandler txtSearchLog.TextChanged, Sub() applyFilters()

        ThemeManager.EnableDrag(pnlHeader, Me)
        ThemeManager.AddMinimizeButton(pnlHeader, Me)

        pnlSidebar.Width = Math.Max(pnlSidebar.Width, pnlStatsCars.Right + 15)

        For Each pnl As Panel In {pnlStatsCars, pnlStatsMotors, pnlStatsSales}
            For Each c As Control In pnl.Controls
                If TypeOf c Is Label Then
                    c.BackColor = Color.White
                End If
            Next
        Next

        ThemeManager.MakeRoundedControl(btnManage, 6)
        ThemeManager.MakeRoundedControl(btnExit, 6)

        ThemeManager.StyleCardPanel(pnlStatsCars, ThemeManager.TealAccent)
        ThemeManager.StyleCardPanel(pnlStatsMotors, ThemeManager.TealAccent)
        ThemeManager.StyleCardPanel(pnlStatsSales, ThemeManager.TealAccent)

        ThemeManager.StyleButton(btnManage, ThemeManager.TealAccent)

        AddHandler btnSettings.Click, Sub()
                                          Dim configDlg As New RateConfigDialogForm()
                                          If OverlayHelper.ShowDialog(Me, configDlg) = DialogResult.OK Then
                                              UpdateSlots()
                                              SuccessDialogForm.ShowSuccess("Rates updated successfully!")
                                          End If
                                      End Sub

        ConfigureRoleInterface()

        btnTabMap.Text = "Visual Slot"
        btnTabMap.Width = 112
        btnTabMap.Top = 0

        btnTabTable.Text = "Transaction"
        btnTabTable.Width = 112
        btnTabTable.Left = btnTabMap.Right
        btnTabTable.Top = 0

        btnTabAccounts.Text = "Account"
        btnTabAccounts.Width = 112
        btnTabAccounts.Left = btnTabTable.Right
        btnTabAccounts.Top = 0

        AddHandler btnTabMap.Click, Sub()
                                        pnlMapView.Visible = True
                                        pnlTableView.Visible = False
                                        pnlAccountsView.Visible = False

                                        btnExport.Visible = False
                                        btnClearAccounts.Visible = False
                                        btnClearLog.Visible = False
                                        txtSearchLog.Visible = False
                                        dtpFrom.Visible = False
                                        dtpTo.Visible = False
                                        lblDateSeparator.Visible = False

                                        btnTabMap.BackColor = Color.White
                                        btnTabMap.ForeColor = ThemeManager.TextNavy

                                        btnTabTable.BackColor = Color.Transparent
                                        btnTabTable.ForeColor = ThemeManager.TextGray

                                        btnTabAccounts.BackColor = Color.Transparent
                                        btnTabAccounts.ForeColor = ThemeManager.TextGray
                                    End Sub

        AddHandler btnTabTable.Click, Sub()
                                          pnlMapView.Visible = False
                                          pnlTableView.Visible = True
                                          pnlAccountsView.Visible = False
                                          btnExport.Visible = True
                                          btnClearAccounts.Visible = False
                                          btnClearLog.Visible = (Form1.CurrentUserRole = "Admin")
                                          txtSearchLog.Visible = True
                                          dtpFrom.Visible = True
                                          dtpTo.Visible = True
                                          lblDateSeparator.Visible = True

                                          btnTabTable.BackColor = Color.White
                                          btnTabTable.ForeColor = ThemeManager.TextNavy

                                          btnTabMap.BackColor = Color.Transparent
                                          btnTabMap.ForeColor = ThemeManager.TextGray

                                          btnTabAccounts.BackColor = Color.Transparent
                                          btnTabAccounts.ForeColor = ThemeManager.TextGray
                                      End Sub

        AddHandler btnTabAccounts.Click, Sub()
                                             pnlMapView.Visible = False
                                             pnlTableView.Visible = False
                                             pnlAccountsView.Visible = True

                                             btnExport.Visible = False
                                             btnClearAccounts.Visible = (Form1.CurrentUserRole = "Admin")
                                             btnClearLog.Visible = False
                                             txtSearchLog.Visible = False
                                             dtpFrom.Visible = False
                                             dtpTo.Visible = False
                                             lblDateSeparator.Visible = False

                                             btnTabAccounts.BackColor = Color.White
                                             btnTabAccounts.ForeColor = ThemeManager.TextNavy

                                             btnTabMap.BackColor = Color.Transparent
                                             btnTabMap.ForeColor = ThemeManager.TextGray

                                             btnTabTable.BackColor = Color.Transparent
                                             btnTabTable.ForeColor = ThemeManager.TextGray

                                             LoadUsersFromMySql()
                                         End Sub

        If Form1.CurrentUserRole <> "Admin" Then
            btnTabAccounts.Visible = False
        End If

        SetupContextMenus()
        UpdateSlots()

        AddHandler pnlStatsSales.Paint, Sub(senderPaint, ePaint)
                                            Dim g As Graphics = ePaint.Graphics
                                            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                                            Using cashPen As New Pen(Color.FromArgb(46, 186, 104), 1.5)
                                                g.DrawRectangle(cashPen, 130, 10, 20, 12)
                                                g.DrawEllipse(cashPen, 136, 12, 8, 8)
                                                Using font As New Font("Segoe UI", 6, FontStyle.Bold)
                                                    g.DrawString("₱", font, New SolidBrush(Color.FromArgb(46, 186, 104)), 136.5F, 11)
                                                End Using
                                            End Using

                                            Using font As New Font("Segoe UI", 9)
                                                Dim vehiclesText As String = "Vehicles Today: 0"
                                                If lblTotalSales.Tag IsNot Nothing Then
                                                    vehiclesText = $"Vehicles Today: {lblTotalSales.Tag.ToString()}"
                                                End If
                                                g.DrawString(vehiclesText, font, New SolidBrush(Color.FromArgb(120, 125, 130)), 11, 60)
                                            End Using
                                        End Sub

        Dim drawRateBtn As Action(Of RadioButton, PaintEventArgs, String, String, String) =
            Sub(rdo, ePaintBtn, icon, label, rate)
                Dim g As Graphics = ePaintBtn.Graphics
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                Dim isChecked As Boolean = rdo.Checked

                Dim bgColor As Color = If(isChecked, Color.FromArgb(228, 240, 240), Color.White)
                Dim borderColor As Color = If(isChecked, Color.FromArgb(0, 139, 139), Color.FromArgb(220, 225, 230))
                Dim textColor As Color = If(isChecked, Color.FromArgb(27, 42, 71), Color.FromArgb(80, 88, 102))

                g.Clear(bgColor)

                Using p As New Pen(borderColor, 1.5)
                    Dim radius As Integer = 8
                    Dim gp As New Drawing2D.GraphicsPath()
                    gp.AddArc(0, 0, radius, radius, 180, 90)
                    gp.AddArc(rdo.Width - radius - 1, 0, radius, radius, 270, 90)
                    gp.AddArc(rdo.Width - radius - 1, rdo.Height - radius - 1, radius, radius, 0, 90)
                    gp.AddArc(0, rdo.Height - radius - 1, radius, radius, 90, 90)
                    gp.CloseFigure()
                    g.DrawPath(p, gp)
                End Using

                Using iconFont As New Font("Segoe UI Emoji", 12)
                    g.DrawString(icon, iconFont, New SolidBrush(textColor), 10, 8)
                End Using

                Using f As New Font("Segoe UI", 8.25!, FontStyle.Bold)
                    g.DrawString(label, f, New SolidBrush(textColor), 33, 11)
                    Dim rateSize = g.MeasureString(rate, f)
                    g.DrawString(rate, f, New SolidBrush(textColor), rdo.Width - rateSize.Width - 5, 11)
                End Using
            End Sub

        AddHandler rdoCarr.Paint, Sub(sPaint, ePaintBtn) drawRateBtn(rdoCarr, ePaintBtn, "🚗", "Car", "₱" & ParkingData.CarBaseRate.ToString("F0") & "/hr")
        AddHandler rdoMotor.Paint, Sub(sPaint, ePaintBtn) drawRateBtn(rdoMotor, ePaintBtn, "🏍️", "Motor", "₱" & ParkingData.MotorBaseRate.ToString("F0") & "/hr")

        AddHandler btnManage.Paint, Sub(sPaint, ePaintBtn)
                                        Dim g As Graphics = ePaintBtn.Graphics
                                        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                                        Dim rect As New Rectangle(25, 12, 16, 16)
                                        Using p As New Pen(Color.White, 1.5)
                                            g.DrawRectangle(p, rect.X, rect.Y, 6, 6)
                                            g.DrawRectangle(p, rect.X + 8, rect.Y, 6, 6)
                                            g.DrawRectangle(p, rect.X, rect.Y + 8, 6, 6)
                                            g.DrawRectangle(p, rect.X + 8, rect.Y + 8, 6, 6)
                                        End Using
                                    End Sub

        Try
            If rdoCarr IsNot Nothing Then
                AddHandler rdoCarr.CheckedChanged, Sub() UpdateRateLabel()
            End If
        Catch
        End Try

        Try
            If rdoMotor IsNot Nothing Then
                AddHandler rdoMotor.CheckedChanged, Sub() UpdateRateLabel()
            End If
        Catch
        End Try

        UpdateRateLabel()
    End Sub

    Private Sub LoadUsersFromMySql()
        Try
            Dim dt As New DataTable("UserRecord")

            Dim sql As String =
                "SELECT Fullname AS FullName, Username, Password, 'Customer' AS Role " &
                "FROM tblcustomer " &
                "UNION ALL " &
                "SELECT FullName, Username, Password, 'Teller' AS Role " &
                "FROM tblteller " &
                "ORDER BY Username"

            Using conn As MySqlConnection = GetConnection()
                Dim adapter As New MySqlDataAdapter(sql, conn)
                adapter.Fill(dt)
            End Using

            Dim adminRow As DataRow = dt.NewRow()
            adminRow("FullName") = "Administrator"
            adminRow("Username") = "admin"
            adminRow("Password") = "1234"
            adminRow("Role") = "Admin"
            dt.Rows.InsertAt(adminRow, 0)

            ParkingData.UsersTable = dt

            dgvAccounts.DataSource = Nothing
            dgvAccounts.DataSource = dt
            dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Catch ex As Exception
            MessageBox.Show("Unable to load accounts from MySQL: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupContextMenus()
        Dim cmsAccounts As New ContextMenuStrip()
        Dim tsmiDeleteAccount As New ToolStripMenuItem("Delete User")
        cmsAccounts.Items.Add(tsmiDeleteAccount)
        dgvAccounts.ContextMenuStrip = cmsAccounts

        AddHandler tsmiDeleteAccount.Click, Sub(sender As Object, e As EventArgs)
                                                If dgvAccounts.SelectedRows.Count > 0 Then
                                                    Dim selectedRow = dgvAccounts.SelectedRows(0)
                                                    Dim username As String = selectedRow.Cells("Username").Value.ToString()

                                                    If username.ToLower() = "admin" Then
                                                        MessageBox.Show("Cannot delete the admin account.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                        Return
                                                    End If

                                                    Dim confirmDlg As New ConfirmActionDialogForm("Delete User", $"Are you sure you want to delete user '{username}'?", "YES")
                                                    If OverlayHelper.ShowDialog(Me, confirmDlg) = DialogResult.Yes Then
                                                        Dim role As String = If(selectedRow.Cells("Role").Value, "").ToString()
                                                        Dim tableName As String = If(role.Equals("Teller", StringComparison.OrdinalIgnoreCase), "tblteller", "tblcustomer")

                                                        Try
                                                            Using conn As MySqlConnection = GetConnection()
                                                                Dim delCmd As New MySqlCommand("DELETE FROM " & tableName & " WHERE Username = @username", conn)
                                                                delCmd.Parameters.AddWithValue("@username", username)
                                                                delCmd.ExecuteNonQuery()
                                                            End Using
                                                            LoadUsersFromMySql()
                                                        Catch ex As MySqlException
                                                            MessageBox.Show("Unable to delete account: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        End Try
                                                    End If
                                                End If
                                            End Sub

        dgvVehicles.AllowUserToDeleteRows = False
        Dim cmsVehicles As New ContextMenuStrip()
        Dim tsmiDeleteVehicle As New ToolStripMenuItem("Delete Parking Record")
        cmsVehicles.Items.Add(tsmiDeleteVehicle)
        dgvVehicles.ContextMenuStrip = cmsVehicles

        AddHandler tsmiDeleteVehicle.Click, Sub(sender As Object, e As EventArgs)
                                                If Form1.CurrentUserRole <> "Admin" Then
                                                    MessageBox.Show("Only administrators can delete parking records.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                    Return
                                                End If

                                                If dgvVehicles.SelectedRows.Count > 0 Then
                                                    Dim selectedRow = dgvVehicles.SelectedRows(0)
                                                    Dim plate As String = If(selectedRow.Cells("PlateNumber").Value, "").ToString()
                                                    Dim slot As String = If(selectedRow.Cells("Slot").Value, "").ToString()
                                                    Dim paidStatus As String = If(selectedRow.Cells("PaidStatus").Value, "").ToString()
                                                    Dim transId As Object = selectedRow.Cells("TransactionID").Value

                                                    If paidStatus = "Not Paid" Then
                                                        MessageBox.Show("Cannot delete record for an actively parked vehicle. Please process check-out first.", "Active Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                        Return
                                                    End If

                                                    Dim confirmDlg As New ConfirmActionDialogForm("Delete Record", $"Are you sure you want to delete the record for '{plate}' in slot {slot}?", "YES")
                                                    If OverlayHelper.ShowDialog(Me, confirmDlg) = DialogResult.Yes Then
                                                        Try
                                                            Using conn As MySqlConnection = GetConnection()
                                                                If conn IsNot Nothing Then
                                                                    Dim delCmd As New MySqlCommand("DELETE FROM tblparkingrecord WHERE TransactionID = @id", conn)
                                                                    delCmd.Parameters.AddWithValue("@id", transId)
                                                                    delCmd.ExecuteNonQuery()
                                                                End If
                                                            End Using

                                                            LoadDataFromDatabase()
                                                            UpdateSlots()
                                                            SuccessDialogForm.ShowSuccess("Record deleted successfully.")
                                                        Catch ex As MySqlException
                                                            MessageBox.Show("Error deleting record from database: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        End Try
                                                    End If
                                                End If
                                            End Sub
    End Sub

    Private Sub UpdateRateLabel()
        Try
            If rdoCarr IsNot Nothing AndAlso rdoCarr.Checked Then
                If lblPrice IsNot Nothing Then lblPrice.Text = "₱" & ParkingData.CarBaseRate.ToString("F2")
                If rdoMotor IsNot Nothing Then rdoMotor.Invalidate()
                rdoCarr.Invalidate()
                Return
            End If

            If rdoMotor IsNot Nothing AndAlso rdoMotor.Checked Then
                If lblPrice IsNot Nothing Then lblPrice.Text = "₱" & ParkingData.MotorBaseRate.ToString("F2")
                If rdoCarr IsNot Nothing Then rdoCarr.Invalidate()
                rdoMotor.Invalidate()
                Return
            End If
        Catch
        End Try
    End Sub

    Public Sub UpdateSlots()
        Dim maxCarSlots As Integer = 50
        Dim maxMotorSlots As Integer = 20

        ParkingData.InitializeDatabase()

        Dim occupiedCars As Integer = 0
        Dim occupiedMotors As Integer = 0
        Dim todayRevenue As Decimal = 0D
        Dim vehiclesToday As Integer = 0
        Dim activeReservations As DataTable = GetActiveReservations()

        If ParkingData.ParkingTable IsNot Nothing Then
            For Each row As DataRow In ParkingData.ParkingTable.Rows
                Dim slot As String = If(row("Slot"), "").ToString()
                Dim paidStatus As String = If(row("PaidStatus"), "").ToString()

                If paidStatus = "Not Paid" AndAlso slot <> "" Then
                    If ParkingData.parkingDatabase.ContainsKey(slot) Then
                        ParkingData.parkingDatabase(slot) = False
                    End If
                    If DatabaseModule.IsCarSlot(slot) Then
                        occupiedCars += 1
                    Else
                        occupiedMotors += 1
                    End If
                End If

                Dim checkInDate As DateTime
                If DateTime.TryParse(If(row("CheckIn"), "").ToString(), checkInDate) Then
                    If checkInDate.Date = DateTime.Today Then
                        vehiclesToday += 1
                    End If
                End If

                If paidStatus = "Paid" AndAlso Not IsDBNull(row("TotalAmount")) Then
                    Dim checkOutDate As DateTime
                    If DateTime.TryParse(If(row("CheckOut"), "").ToString(), checkOutDate) Then
                        If checkOutDate.Date = DateTime.Today Then
                            Dim amt As Decimal = 0D
                            If Decimal.TryParse(row("TotalAmount").ToString(), amt) Then
                                todayRevenue += amt
                            End If
                        End If
                    End If
                End If
            Next
        End If

        For Each resRow As DataRow In activeReservations.Rows
            Dim slotName As String = If(resRow("ParkingSlot"), "").ToString()
            Dim resDate As DateTime
            If DateTime.TryParse(If(resRow("ReservationDate"), "").ToString(), resDate) Then
                If resDate.Date = DateTime.Today Then
                    If ParkingData.parkingDatabase.ContainsKey(slotName) AndAlso ParkingData.parkingDatabase(slotName) = True Then
                        ParkingData.parkingDatabase(slotName) = False
                        If DatabaseModule.IsCarSlot(slotName) Then
                            occupiedCars += 1
                        Else
                            occupiedMotors += 1
                        End If
                    End If
                End If
            End If
        Next

        Dim availableCars As Integer = Math.Max(0, maxCarSlots - occupiedCars)
        Dim availableMotors As Integer = Math.Max(0, maxMotorSlots - occupiedMotors)

        lblAvailableCars.Text = $"{availableCars} / {maxCarSlots}"
        lblOccupiedCars.Text = "available"

        lblAvailableMotors.Text = $"{availableMotors} / {maxMotorSlots}"
        lblOccupiedMotors.Text = "available"

        lblTotalSales.Text = $"₱{todayRevenue.ToString("N2")}"
        lblTotalSales.Tag = vehiclesToday

        pnlStatsCars.Invalidate()
        pnlStatsMotors.Invalidate()

        dgvVehicles.DataSource = Nothing
        dgvVehicles.DataSource = ParkingData.ParkingTable

        If dgvVehicles.Columns.Contains("Code") Then dgvVehicles.Columns("Code").Visible = False
        If dgvVehicles.Columns.Contains("PlateNumber") Then dgvVehicles.Columns("PlateNumber").HeaderText = "Plate Number"

        If dgvVehicles.Columns.Contains("CheckIn") Then
            dgvVehicles.Columns("CheckIn").HeaderText = "Time In"
            dgvVehicles.Columns("CheckIn").DefaultCellStyle.Format = "yyyy-MM-dd hh:mm tt"
        End If

        If dgvVehicles.Columns.Contains("CheckOut") Then
            dgvVehicles.Columns("CheckOut").HeaderText = "Time Out"
            dgvVehicles.Columns("CheckOut").DefaultCellStyle.Format = "yyyy-MM-dd hh:mm tt"
        End If

        If dgvVehicles.Columns.Contains("VehicleType") Then dgvVehicles.Columns("VehicleType").HeaderText = "Vehicle Type"
        If dgvVehicles.Columns.Contains("RateName") Then dgvVehicles.Columns("RateName").Visible = False
        If dgvVehicles.Columns.Contains("Rate") Then dgvVehicles.Columns("Rate").Visible = False

        If dgvVehicles.Columns.Contains("Slot") Then dgvVehicles.Columns("Slot").HeaderText = "Parking Slot"
        If dgvVehicles.Columns.Contains("TotalTime") Then dgvVehicles.Columns("TotalTime").HeaderText = "Duration"

        If dgvVehicles.Columns.Contains("TotalAmount") Then
            dgvVehicles.Columns("TotalAmount").DefaultCellStyle.Format = "₱0.00"
            dgvVehicles.Columns("TotalAmount").DefaultCellStyle.NullValue = "₱"
        End If

        If dgvVehicles.Columns.Contains("PaidStatus") Then dgvVehicles.Columns("PaidStatus").HeaderText = "Paid Status"

        dgvVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        If ShowLobbyMode OrElse Form1.CurrentUserRole = "Customer" Then
            If dgvVehicles IsNot Nothing Then dgvVehicles.Visible = False
            If lblTotalSales IsNot Nothing Then lblTotalSales.Visible = False
            If btnManage IsNot Nothing Then btnManage.Visible = False
            If Button1 IsNot Nothing Then Button1.Visible = False
            If btnSettings IsNot Nothing Then btnSettings.Visible = False
        Else
            If dgvVehicles IsNot Nothing Then dgvVehicles.Visible = True
            If lblTotalSales IsNot Nothing Then lblTotalSales.Visible = (Form1.CurrentUserRole = "Admin")
            If btnManage IsNot Nothing Then btnManage.Visible = True
            If btnSettings IsNot Nothing Then btnSettings.Visible = (Form1.CurrentUserRole = "Admin")
            If Button1 IsNot Nothing Then Button1.Visible = (Form1.CurrentUserRole = "Admin")
        End If

        For Each ctrl As Control In flpCarSlots.Controls
            ctrl.Dispose()
        Next
        For Each ctrl As Control In flpMotorSlots.Controls
            ctrl.Dispose()
        Next
        flpCarSlots.Controls.Clear()
        flpMotorSlots.Controls.Clear()

        flpCarSlots.SuspendLayout()
        flpMotorSlots.SuspendLayout()

        For Each slotEntry In ParkingData.parkingDatabase
            Dim slotName As String = slotEntry.Key

            Dim isOccupiedParked As Boolean = False
            Dim parkedPlate As String = ""

            If ParkingData.ParkingTable IsNot Nothing Then
                For Each row As DataRow In ParkingData.ParkingTable.Rows
                    If row("Slot").ToString() = slotName AndAlso row("PaidStatus").ToString() = "Not Paid" Then
                        isOccupiedParked = True
                        parkedPlate = row("PlateNumber").ToString()
                        Exit For
                    End If
                Next
            End If

            Dim isReservedToday As Boolean = False
            Dim resId As Integer = 0
            Dim resCustId As Object = DBNull.Value
            Dim resCustName As String = ""
            Dim resPlate As String = ""
            Dim resDateVal As DateTime = DateTime.MinValue

            If Not isOccupiedParked Then
                For Each resRow As DataRow In activeReservations.Rows
                    If resRow("ParkingSlot").ToString() = slotName Then
                        Dim rDate As DateTime
                        If DateTime.TryParse(resRow("ReservationDate").ToString(), rDate) Then
                            If rDate.Date = DateTime.Today Then
                                isReservedToday = True
                                resId = Convert.ToInt32(resRow("ReservationID"))
                                resCustId = resRow("CustomerID")
                                resCustName = resRow("CustomerName").ToString()
                                resPlate = resRow("VehiclePlate").ToString()
                                resDateVal = rDate
                                Exit For
                            End If
                        End If
                    End If
                Next
            End If

            Dim pnlSlot As New Panel()
            pnlSlot.Size = New Size(72, 85)
            pnlSlot.Margin = New Padding(4)
            pnlSlot.BackColor = Color.White

            Dim baseColor As Color
            Dim hoverColor As Color

            If isReservedToday Then
                baseColor = Color.FromArgb(240, 160, 40)
                hoverColor = Color.FromArgb(250, 180, 60)
            ElseIf isOccupiedParked Then
                baseColor = Color.FromArgb(220, 60, 60)
                hoverColor = Color.FromArgb(240, 80, 80)
            Else
                baseColor = Color.FromArgb(0, 168, 181)
                hoverColor = Color.FromArgb(20, 188, 201)
            End If

            Dim currentBgColor = baseColor

            AddHandler pnlSlot.Paint, Sub(sSender, sEvent)
                                          sEvent.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                                          Dim rect As New Rectangle(0, 0, pnlSlot.Width - 1, pnlSlot.Height - 1)
                                          Dim path As New Drawing2D.GraphicsPath()
                                          Dim r As Integer = 8
                                          path.AddArc(rect.X, rect.Y, r, r, 180, 90)
                                          path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90)
                                          path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90)
                                          path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90)
                                          path.CloseFigure()

                                          Using brush As New SolidBrush(currentBgColor)
                                              sEvent.Graphics.FillPath(brush, path)
                                          End Using
                                      End Sub

            AddHandler pnlSlot.MouseEnter, Sub(s, ev)
                                               currentBgColor = hoverColor
                                               pnlSlot.Invalidate()
                                           End Sub
            AddHandler pnlSlot.MouseLeave, Sub(s, ev)
                                               currentBgColor = baseColor
                                               pnlSlot.Invalidate()
                                           End Sub

            Dim lblName As New Label()
            lblName.Text = slotName
            lblName.Font = New Font("Segoe UI", 8.5!, FontStyle.Bold)
            lblName.ForeColor = Color.White
            lblName.Location = New Point(0, 4)
            lblName.Size = New Size(72, 20)
            lblName.TextAlign = ContentAlignment.MiddleCenter
            lblName.BackColor = Color.Transparent
            pnlSlot.Controls.Add(lblName)

            Dim lblStatus As New Label()
            If isOccupiedParked Then
                lblStatus.Text = parkedPlate
            ElseIf isReservedToday Then
                lblStatus.Text = "RESERVED"
            Else
                lblStatus.Text = "VACANT"
            End If
            lblStatus.Font = New Font("Segoe UI Semibold", 7.0!)
            lblStatus.ForeColor = Color.FromArgb(240, 240, 240)
            lblStatus.Location = If(isReservedToday, New Point(0, 20), New Point(0, 25))
            lblStatus.Size = New Size(72, 20)
            lblStatus.TextAlign = ContentAlignment.MiddleCenter
            lblStatus.BackColor = Color.Transparent
            pnlSlot.Controls.Add(lblStatus)

            Dim lblReserver As Label = Nothing
            Dim lblDate As Label = Nothing
            If isReservedToday Then
                lblReserver = New Label()
                lblReserver.Text = resPlate
                lblReserver.Font = New Font("Segoe UI", 7.5!, FontStyle.Bold)
                lblReserver.ForeColor = Color.White
                lblReserver.Location = New Point(0, 40)
                lblReserver.Size = New Size(72, 20)
                lblReserver.TextAlign = ContentAlignment.MiddleCenter
                lblReserver.BackColor = Color.Transparent
                pnlSlot.Controls.Add(lblReserver)

                lblDate = New Label()
                lblDate.Text = If(String.IsNullOrWhiteSpace(resCustName), resDateVal.ToString("MM/dd"), resCustName)
                lblDate.Font = New Font("Segoe UI", 7.0!, FontStyle.Italic)
                lblDate.ForeColor = Color.FromArgb(230, 230, 230)
                lblDate.Location = New Point(0, 60)
                lblDate.Size = New Size(72, 20)
                lblDate.TextAlign = ContentAlignment.MiddleCenter
                lblDate.BackColor = Color.Transparent
                pnlSlot.Controls.Add(lblDate)
            End If

            AddHandler lblName.MouseEnter, Sub(s, ev)
                                               currentBgColor = hoverColor
                                               pnlSlot.Invalidate()
                                           End Sub
            AddHandler lblName.MouseLeave, Sub(s, ev)
                                               currentBgColor = baseColor
                                               pnlSlot.Invalidate()
                                           End Sub
            AddHandler lblStatus.MouseEnter, Sub(s, ev)
                                                 currentBgColor = hoverColor
                                                 pnlSlot.Invalidate()
                                             End Sub
            AddHandler lblStatus.MouseLeave, Sub(s, ev)
                                                 currentBgColor = baseColor
                                                 pnlSlot.Invalidate()
                                             End Sub
            If lblReserver IsNot Nothing Then
                AddHandler lblReserver.MouseEnter, Sub(s, ev)
                                                       currentBgColor = hoverColor
                                                       pnlSlot.Invalidate()
                                                   End Sub
                AddHandler lblReserver.MouseLeave, Sub(s, ev)
                                                       currentBgColor = baseColor
                                                       pnlSlot.Invalidate()
                                                   End Sub
            End If
            If lblDate IsNot Nothing Then
                AddHandler lblDate.MouseEnter, Sub(s, ev)
                                                   currentBgColor = hoverColor
                                                   pnlSlot.Invalidate()
                                               End Sub
                AddHandler lblDate.MouseLeave, Sub(s, ev)
                                                   currentBgColor = baseColor
                                                   pnlSlot.Invalidate()
                                               End Sub
            End If

            If Not isOccupiedParked AndAlso Not isReservedToday Then
                pnlSlot.Cursor = Cursors.Hand
                AddHandler pnlSlot.Click, Sub()
                                              Dim resDlg As New ReserveDialogForm(slotName)
                                              If OverlayHelper.ShowDialog(Me, resDlg) = DialogResult.OK Then
                                                  Dim resPlateInput As String = DatabaseModule.NormalizePlate(resDlg.PlateNumber)
                                                  If Not DatabaseModule.IsValidPlate(resPlateInput) Then
                                                      MessageBox.Show("Invalid plate number. Must be 4 to 8 alphanumeric characters.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                      Return
                                                  End If

                                                  Dim resDate As DateTime = resDlg.ReservationDate.Date
                                                  Dim expectedType As String = DatabaseModule.GetSlotVehicleType(slotName)

                                                  Try
                                                      Using conn As MySqlConnection = GetConnection()
                                                          If conn IsNot Nothing Then
                                                              Dim checkParked As New MySqlCommand("SELECT COUNT(*) FROM tblparkingrecord WHERE PlateNumber = @plate AND `Paid Status` = 'Not Paid'", conn)
                                                              checkParked.Parameters.AddWithValue("@plate", resPlateInput)
                                                              If Convert.ToInt32(checkParked.ExecuteScalar()) > 0 Then
                                                                  MessageBox.Show("This vehicle is already actively parked in the facility.", "Duplicate Plate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                  Return
                                                              End If

                                                              Dim checkSlotRes As New MySqlCommand("SELECT COUNT(*) FROM tblreservation WHERE ParkingSlot = @slot AND ReservationDate = @resdate AND Status = 'Reserved'", conn)
                                                              checkSlotRes.Parameters.AddWithValue("@slot", slotName)
                                                              checkSlotRes.Parameters.AddWithValue("@resdate", resDate)
                                                              If Convert.ToInt32(checkSlotRes.ExecuteScalar()) > 0 Then
                                                                  MessageBox.Show($"Slot {slotName} is already reserved on {resDate.ToString("yyyy-MM-dd")}.", "Slot Reserved", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                  Return
                                                              End If

                                                              Dim checkPlateRes As New MySqlCommand("SELECT COUNT(*) FROM tblreservation WHERE VehiclePlate = @plate AND ReservationDate = @resdate AND Status = 'Reserved'", conn)
                                                              checkPlateRes.Parameters.AddWithValue("@plate", resPlateInput)
                                                              checkPlateRes.Parameters.AddWithValue("@resdate", resDate)
                                                              If Convert.ToInt32(checkPlateRes.ExecuteScalar()) > 0 Then
                                                                  MessageBox.Show($"Vehicle {resPlateInput} already has a reservation on {resDate.ToString("yyyy-MM-dd")}.", "Duplicate Reservation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                  Return
                                                              End If

                                                              Dim customerID As Object = DBNull.Value
                                                              If Form1.CurrentUserRole = "Customer" Then
                                                                  customerID = GetCurrentUserID("Customer", Form1.CurrentUsername)
                                                              End If

                                                              Dim insCmd As New MySqlCommand("INSERT INTO tblreservation (CustomerID, CustomerName, ParkingSlot, VehiclePlate, VehicleType, ReservationDate, Status, CreatedAt) VALUES (@custid, @custname, @slot, @plate, @vtype, @resdate, 'Reserved', @created)", conn)
                                                              insCmd.Parameters.AddWithValue("@custid", customerID)
                                                              insCmd.Parameters.AddWithValue("@custname", resDlg.ReserverName)
                                                              insCmd.Parameters.AddWithValue("@slot", slotName)
                                                              insCmd.Parameters.AddWithValue("@plate", resPlateInput)
                                                              insCmd.Parameters.AddWithValue("@vtype", expectedType)
                                                              insCmd.Parameters.AddWithValue("@resdate", resDate)
                                                              insCmd.Parameters.AddWithValue("@created", DateTime.Now)
                                                              insCmd.ExecuteNonQuery()
                                                          End If
                                                      End Using

                                                      LoadDataFromDatabase()
                                                      UpdateSlots()
                                                      SuccessDialogForm.ShowSuccess($"Reservation successfully confirmed for slot {slotName} on {resDate.ToString("yyyy-MM-dd")}!")
                                                  Catch ex As Exception
                                                      MessageBox.Show("Error saving reservation: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                  End Try
                                              End If
                                          End Sub

                AddHandler lblName.Click, Sub() pnlSlot_ClickProxy(pnlSlot)
                AddHandler lblStatus.Click, Sub() pnlSlot_ClickProxy(pnlSlot)

            ElseIf isReservedToday Then
                pnlSlot.Cursor = Cursors.Hand
                AddHandler pnlSlot.Click, Sub()
                                              If Form1.CurrentUserRole = "Customer" Then
                                                  Dim myCustId = GetCurrentUserID("Customer", Form1.CurrentUsername)
                                                  If resCustId IsNot DBNull.Value AndAlso (myCustId Is DBNull.Value OrElse Convert.ToInt32(myCustId) <> Convert.ToInt32(resCustId)) Then
                                                      MessageBox.Show("This slot is reserved by another customer.", "Reservation Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                      Return
                                                  End If
                                              End If

                                              Dim manageDlg As New ManageReservationDialogForm(slotName, resPlate, resCustName)
                                              If OverlayHelper.ShowDialog(Me, manageDlg) = DialogResult.OK Then
                                                  If manageDlg.SelectedAction = "Cancel" Then
                                                      Dim confirmDlg As New ConfirmActionDialogForm("Cancel Reservation", $"Are you sure you want to cancel the reservation for slot {slotName}?", "YES")
                                                      If OverlayHelper.ShowDialog(Me, confirmDlg) = DialogResult.Yes Then
                                                          Try
                                                              Using conn As MySqlConnection = GetConnection()
                                                                  If conn IsNot Nothing Then
                                                                      Dim updCmd As New MySqlCommand("UPDATE tblreservation SET Status = 'Cancelled' WHERE ReservationID = @id AND Status = 'Reserved'", conn)
                                                                      updCmd.Parameters.AddWithValue("@id", resId)
                                                                      Dim aff As Integer = updCmd.ExecuteNonQuery()
                                                                      If aff > 0 Then
                                                                          LoadDataFromDatabase()
                                                                          UpdateSlots()
                                                                          SuccessDialogForm.ShowSuccess("Reservation cancelled successfully.")
                                                                      Else
                                                                          MessageBox.Show("Reservation was already modified or cancelled.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                                          LoadDataFromDatabase()
                                                                          UpdateSlots()
                                                                      End If
                                                                  End If
                                                              End Using
                                                          Catch ex As Exception
                                                              MessageBox.Show("Error cancelling reservation: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                          End Try
                                                      End If

                                                  ElseIf manageDlg.SelectedAction = "CheckIn" Then
                                                      If Form1.CurrentUserRole = "Customer" Then
                                                          MessageBox.Show("Reservation check-in must be processed by a Teller or Admin at the gate.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                          Return
                                                      End If

                                                      If resDateVal.Date > DateTime.Today Then
                                                          MessageBox.Show($"This reservation is scheduled for {resDateVal.ToString("yyyy-MM-dd")}. Early check-in is not permitted before the reservation date.", "Early Check-In", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                          Return
                                                      End If

                                                      Using conn As MySqlConnection = GetConnection()
                                                          conn.Open()
                                                          Using tran As MySqlTransaction = conn.BeginTransaction()
                                                              Try
                                                                  Dim updCmd As New MySqlCommand("UPDATE tblreservation SET Status = 'Checked In' WHERE ReservationID = @id AND Status = 'Reserved'", conn, tran)
                                                                  updCmd.Parameters.AddWithValue("@id", resId)
                                                                  Dim aff As Integer = updCmd.ExecuteNonQuery()
                                                                  If aff = 0 Then
                                                                      tran.Rollback()
                                                                      MessageBox.Show("Reservation is no longer active or was already checked in.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                      LoadDataFromDatabase()
                                                                      UpdateSlots()
                                                                      Return
                                                                  End If

                                                                  Dim newCode As String = ParkingData.GenerateCode()
                                                                  Dim vType As String = DatabaseModule.GetSlotVehicleType(slotName)
                                                                  Dim isCar As Boolean = DatabaseModule.IsCarSlot(slotName)
                                                                  Dim rateVal As Decimal = If(isCar, ParkingData.CarBaseRate, ParkingData.MotorBaseRate)
                                                                  Dim tellerIdVal As Object = GetCurrentUserID("Teller", Form1.CurrentUsername)

                                                                  Dim insParking As New MySqlCommand("INSERT INTO tblparkingrecord (code, PlateNumber, VehicleType, `Parking Slot`, RateName, Rate, Duration, `Paid Status`, CheckIn, ReservationID, CustomerID, TellerID, ProcessedByName) VALUES (@code, @plate, @vtype, @slot, 'Standard', @rate, '0 hour(s)', 'Not Paid', @checkin, @resid, @custid, @tellid, @proc)", conn, tran)
                                                                  insParking.Parameters.AddWithValue("@code", newCode)
                                                                  insParking.Parameters.AddWithValue("@plate", resPlate)
                                                                  insParking.Parameters.AddWithValue("@vtype", vType)
                                                                  insParking.Parameters.AddWithValue("@slot", slotName)
                                                                  insParking.Parameters.AddWithValue("@rate", rateVal)
                                                                  insParking.Parameters.AddWithValue("@checkin", DateTime.Now)
                                                                  insParking.Parameters.AddWithValue("@resid", resId)
                                                                  insParking.Parameters.AddWithValue("@custid", resCustId)
                                                                  insParking.Parameters.AddWithValue("@tellid", tellerIdVal)
                                                                  insParking.Parameters.AddWithValue("@proc", Form1.CurrentFullName)
                                                                  insParking.ExecuteNonQuery()

                                                                  tran.Commit()
                                                                  LoadDataFromDatabase()
                                                                  UpdateSlots()
                                                                  SuccessDialogForm.ShowSuccess($"Vehicle {resPlate} successfully checked in at slot {slotName}!")
                                                              Catch ex As Exception
                                                                  tran.Rollback()
                                                                  MessageBox.Show("Check-in transaction failed: " & ex.Message, "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                              End Try
                                                          End Using
                                                      End Using
                                                  End If
                                              End If
                                          End Sub

                AddHandler lblName.Click, Sub() pnlSlot_ClickProxy(pnlSlot)
                AddHandler lblStatus.Click, Sub() pnlSlot_ClickProxy(pnlSlot)
                If lblReserver IsNot Nothing Then AddHandler lblReserver.Click, Sub() pnlSlot_ClickProxy(pnlSlot)
                If lblDate IsNot Nothing Then AddHandler lblDate.Click, Sub() pnlSlot_ClickProxy(pnlSlot)
            End If

            If DatabaseModule.IsCarSlot(slotName) Then
                flpCarSlots.Controls.Add(pnlSlot)
            Else
                flpMotorSlots.Controls.Add(pnlSlot)
            End If
        Next

        flpCarSlots.ResumeLayout(True)
        flpMotorSlots.ResumeLayout(True)
    End Sub

    Private Sub btnManage_Click(sender As Object, e As EventArgs) Handles btnManage.Click
        ParkingManagerForm.Show()
        Me.Hide()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim exitDlg As New ConfirmExitDialog()
        If OverlayHelper.ShowDialog(Me, exitDlg) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub

    Private Sub DashBoardForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Form1.Show()
        End If
    End Sub

    Private Sub pnlSlot_ClickProxy(pnl As Panel)
        Dim method As System.Reflection.MethodInfo = GetType(Control).GetMethod("OnClick", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
        If method IsNot Nothing Then
            method.Invoke(pnl, New Object() {EventArgs.Empty})
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim addTellerForm As New frmaddteller()
        If OverlayHelper.ShowDialog(Me, addTellerForm) = DialogResult.OK Then
            LoadUsersFromMySql()
        End If
    End Sub

    Private Sub btnClearAccounts_Click(sender As Object, e As EventArgs) Handles btnClearAccounts.Click
        Dim confirmDlg As New ConfirmActionDialogForm("Clear Accounts", "Are you sure you want to delete all non-admin accounts?", "YES")
        If OverlayHelper.ShowDialog(Me, confirmDlg) = DialogResult.Yes Then
            Try
                Using conn As MySqlConnection = GetConnection()
                    Dim c1 As New MySqlCommand("DELETE FROM tblcustomer", conn)
                    c1.ExecuteNonQuery()
                    Dim c2 As New MySqlCommand("DELETE FROM tblteller", conn)
                    c2.ExecuteNonQuery()
                End Using
                LoadUsersFromMySql()
            Catch ex As MySqlException
                MessageBox.Show("Unable to clear accounts: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub
End Class