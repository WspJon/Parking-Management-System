Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmadmindashboard

    Public ShowLobbyMode As Boolean = False

    Private currentReservingSlot As String = ""

    Private Const EM_SETCUEBANNER As Integer = &H1501

    <System.Runtime.InteropServices.DllImport("user32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Private Shared Function SendMessage(
        hWnd As IntPtr,
        msg As Integer,
        wParam As Integer,
        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)> lParam As String
    ) As IntPtr
    End Function

    '========================================================
    ' FORM LOAD
    '========================================================
    Private Sub frmtellerdashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ParkingData.InitializeDatabase()
        ParkingData.InitializeUsers()

        LoadUsersFromMySql()
        LoadParkingRecordsFromMySql()
        SyncSlotAvailabilityFromRecords()

        '====================================================
        ' EXPORT
        '====================================================
        AddHandler btnExport.Click,
            Sub()

                If dgvVehicles.Rows.Count = 0 Then
                    MessageBox.Show(
                        "No data to export.",
                        "Empty",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    )
                    Return
                End If

                Using sfd As New SaveFileDialog()

                    sfd.Filter = "CSV Excel File (*.csv)|*.csv"
                    sfd.FileName =
                        "Parking_Transaction_Log_" &
                        DateTime.Now.ToString("yyyyMMdd") &
                        ".csv"

                    If sfd.ShowDialog() = DialogResult.OK Then

                        Try

                            Dim sb As New System.Text.StringBuilder()

                            Dim headers As New List(Of String)

                            For Each col As DataGridViewColumn In dgvVehicles.Columns

                                If col.Visible Then

                                    headers.Add(
                                        """" &
                                        col.HeaderText.Replace("""", """""") &
                                        """"
                                    )

                                End If

                            Next

                            sb.AppendLine(String.Join(",", headers))

                            For Each row As DataGridViewRow In dgvVehicles.Rows

                                If Not row.IsNewRow Then

                                    Dim cells As New List(Of String)

                                    For Each cell As DataGridViewCell In row.Cells

                                        If dgvVehicles.Columns(cell.ColumnIndex).Visible Then

                                            Dim val As String =
                                                If(
                                                    cell.Value IsNot Nothing,
                                                    cell.Value.ToString(),
                                                    ""
                                                )

                                            cells.Add(
                                                """" &
                                                val.Replace("""", """""") &
                                                """"
                                            )

                                        End If

                                    Next

                                    sb.AppendLine(String.Join(",", cells))

                                End If

                            Next

                            System.IO.File.WriteAllText(
                                sfd.FileName,
                                sb.ToString()
                            )

                            MessageBox.Show(
                                "Successfully exported to Excel!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            )

                        Catch ex As Exception

                            MessageBox.Show(
                                "Error exporting: " & ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            )

                        End Try

                    End If

                End Using

            End Sub


        '====================================================
        ' CLEAR ACCOUNTS
        '====================================================
        AddHandler btnClearAccounts.Click,
            Sub()

                Dim confirmDlg As New ConfirmActionDialogForm(
                    "Clear Accounts",
                    "Are you sure you want to delete all non-admin accounts?",
                    "YES"
                )

                If OverlayHelper.ShowDialog(Me, confirmDlg) = DialogResult.Yes Then

                    Try

                        Using conn As New MySqlConnection(DatabaseModule.connStr)

                            conn.Open()

                            Using cmdCustomer As New MySqlCommand(
                                "DELETE FROM tblcustomer",
                                conn
                            )

                                cmdCustomer.ExecuteNonQuery()

                            End Using

                            Using cmdTeller As New MySqlCommand(
                                "DELETE FROM tblteller",
                                conn
                            )

                                cmdTeller.ExecuteNonQuery()

                            End Using

                        End Using

                        LoadUsersFromMySql()

                        dgvAccounts.DataSource = Nothing
                        dgvAccounts.DataSource = ParkingData.UsersTable

                        MessageBox.Show(
                            "All non-admin accounts have been deleted.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        )

                    Catch ex As Exception

                        MessageBox.Show(
                            "Unable to clear accounts: " & ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        )

                    End Try

                End If

            End Sub


        '====================================================
        ' CLEAR PAID LOG
        '====================================================
        AddHandler btnClearLog.Click,
            Sub()

                Dim clearDlg As New ClearDialogForm()

                If OverlayHelper.ShowDialog(Me, clearDlg) = DialogResult.Yes Then

                    Try

                        Using conn As New MySqlConnection(DatabaseModule.connStr)

                            conn.Open()

                            Using cmd As New MySqlCommand(
                                "DELETE FROM tblparkingrecord WHERE `Paid Status` = 'Paid'",
                                conn
                            )

                                Dim affected As Integer =
                                    cmd.ExecuteNonQuery()

                                If affected = 0 Then

                                    MessageBox.Show(
                                        "There are no completed transactions to clear.",
                                        "Empty",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information
                                    )

                                Else

                                    LoadParkingRecordsFromMySql()
                                    SyncSlotAvailabilityFromRecords()
                                    UpdateSlots()

                                    MessageBox.Show(
                                        "Completed transactions have been cleared.",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information
                                    )

                                End If

                            End Using

                        End Using

                    Catch ex As Exception

                        MessageBox.Show(
                            "Unable to clear transactions: " & ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        )

                    End Try

                End If

            End Sub


        '====================================================
        ' SEARCH PLACEHOLDER
        '====================================================
        AddHandler Me.HandleCreated,
            Sub()

                If txtSearchLog IsNot Nothing Then

                    SendMessage(
                        txtSearchLog.Handle,
                        EM_SETCUEBANNER,
                        0,
                        "Search..."
                    )

                End If

            End Sub


        '====================================================
        ' FILTER
        '====================================================
        Dim applyFilters As Action =
            Sub()

                If ParkingData.ParkingTable Is Nothing Then
                    Return
                End If

                Try

                    Dim filters As New List(Of String)

                    Dim fromDate As String =
                        dtpFrom.Value.ToString("yyyy-MM-dd")

                    Dim toDate As String =
                        dtpTo.Value.AddDays(1).ToString("yyyy-MM-dd")

                    filters.Add(
                        $"CheckIn >= '{fromDate}' AND CheckIn < '{toDate}'"
                    )

                    Dim searchText As String =
                        txtSearchLog.Text.Trim()

                    searchText =
                        searchText.
                        Replace("'", "''").
                        Replace("[", "[[]").
                        Replace("]", "[]]").
                        Replace("*", "[*]").
                        Replace("%", "[%]")

                    If searchText <> "" Then

                        filters.Add(
                            $"(PlateNumber LIKE '%{searchText}%' " &
                            $"OR Slot LIKE '%{searchText}%' " &
                            $"OR PaidStatus LIKE '%{searchText}%')"
                        )

                    End If

                    ParkingData.ParkingTable.DefaultView.RowFilter =
                        String.Join(" AND ", filters)

                Catch
                End Try

            End Sub


        AddHandler dtpFrom.ValueChanged,
            Sub()
                applyFilters()
            End Sub

        AddHandler dtpTo.ValueChanged,
            Sub()
                applyFilters()
            End Sub

        AddHandler txtSearchLog.TextChanged,
            Sub()
                applyFilters()
            End Sub


        '====================================================
        ' THEME
        '====================================================
        ThemeManager.EnableDrag(pnlHeader, Me)
        ThemeManager.AddMinimizeButton(pnlHeader, Me)

        pnlSidebar.Width =
            Math.Max(
                pnlSidebar.Width,
                pnlStatsCars.Right + 15
            )

        For Each pnl As Panel In {
            pnlStatsCars,
            pnlStatsMotors,
            pnlStatsSales
        }

            For Each c As Control In pnl.Controls

                If TypeOf c Is Label Then
                    c.BackColor = Color.White
                End If

            Next

        Next

        ThemeManager.MakeRoundedControl(btnManage, 6)
        ThemeManager.MakeRoundedControl(btnExit, 6)

        ThemeManager.StyleCardPanel(
            pnlStatsCars,
            ThemeManager.TealAccent
        )

        ThemeManager.StyleCardPanel(
            pnlStatsMotors,
            ThemeManager.TealAccent
        )

        ThemeManager.StyleCardPanel(
            pnlStatsSales,
            ThemeManager.TealAccent
        )

        ThemeManager.StyleButton(
            btnManage,
            ThemeManager.TealAccent
        )


        '====================================================
        ' SETTINGS
        '====================================================
        AddHandler btnSettings.Click,
            Sub()

                Dim configDlg As New RateConfigDialogForm()

                If OverlayHelper.ShowDialog(Me, configDlg) =
                   DialogResult.OK Then

                    UpdateSlots()

                    SuccessDialogForm.ShowSuccess(
                        "Rates updated successfully!"
                    )

                End If

            End Sub


        '====================================================
        ' SIDEBAR TITLE
        '====================================================
        If Form1.CurrentUserRole = "Admin" Then

            lblSidebarTitle.Text =
                "Admin POS System"

        ElseIf Form1.CurrentUserRole = "Teller" Then

            lblSidebarTitle.Text =
                "Teller POS System"

        ElseIf ShowLobbyMode Then

            lblSidebarTitle.Text =
                "Public Live Map"

            pnlStatsSales.Visible = False
            btnManage.Visible = False
            btnSettings.Visible = False
            Button1.Visible = False
            pnlTabBar.Visible = False
            pnlTableView.Visible = False
            pnlMapView.Visible = True

        End If


        '====================================================
        ' TABS
        '====================================================
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


        '====================================================
        ' MAP TAB
        '====================================================
        AddHandler btnTabMap.Click,
            Sub()

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
                btnTabMap.ForeColor =
                    ThemeManager.TextNavy

                btnTabTable.BackColor =
                    Color.Transparent

                btnTabTable.ForeColor =
                    ThemeManager.TextGray

                btnTabAccounts.BackColor =
                    Color.Transparent

                btnTabAccounts.ForeColor =
                    ThemeManager.TextGray

            End Sub


        '====================================================
        ' TABLE TAB
        '====================================================
        AddHandler btnTabTable.Click,
            Sub()

                pnlMapView.Visible = False
                pnlTableView.Visible = True
                pnlAccountsView.Visible = False

                btnExport.Visible = False
                btnClearAccounts.Visible = False
                btnClearLog.Visible = False

                txtSearchLog.Visible = True
                dtpFrom.Visible = False
                dtpTo.Visible = False
                lblDateSeparator.Visible = False

                btnTabTable.BackColor = Color.White
                btnTabTable.ForeColor =
                    ThemeManager.TextNavy

                btnTabMap.BackColor =
                    Color.Transparent

                btnTabMap.ForeColor =
                    ThemeManager.TextGray

                btnTabAccounts.BackColor =
                    Color.Transparent

                btnTabAccounts.ForeColor =
                    ThemeManager.TextGray

            End Sub


        '====================================================
        ' ACCOUNTS TAB
        '====================================================
        AddHandler btnTabAccounts.Click,
            Sub()

                pnlMapView.Visible = False
                pnlTableView.Visible = False
                pnlAccountsView.Visible = True

                btnExport.Visible = False

                btnClearAccounts.Visible =
                    (Form1.CurrentUserRole = "Admin")

                btnClearLog.Visible = False

                txtSearchLog.Visible = False
                dtpFrom.Visible = False
                dtpTo.Visible = False
                lblDateSeparator.Visible = False

                btnTabAccounts.BackColor = Color.White

                btnTabAccounts.ForeColor =
                    ThemeManager.TextNavy

                btnTabMap.BackColor =
                    Color.Transparent

                btnTabMap.ForeColor =
                    ThemeManager.TextGray

                btnTabTable.BackColor =
                    Color.Transparent

                btnTabTable.ForeColor =
                    ThemeManager.TextGray

                LoadUsersFromMySql()

                dgvAccounts.DataSource = Nothing
                dgvAccounts.DataSource =
                    ParkingData.UsersTable

                dgvAccounts.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill

            End Sub


        If Form1.CurrentUserRole <> "Admin" Then
            btnTabAccounts.Visible = False
        End If


        SetupContextMenus()

        UpdateSlots()


        '====================================================
        ' SALES CARD
        '====================================================
        AddHandler pnlStatsSales.Paint,
            Sub(senderPaint, ePaint)

                Dim g As Graphics =
                    ePaint.Graphics

                g.SmoothingMode =
                    Drawing2D.SmoothingMode.AntiAlias

                Using cashPen As New Pen(
                    Color.FromArgb(46, 186, 104),
                    1.5
                )

                    g.DrawRectangle(
                        cashPen,
                        130,
                        10,
                        20,
                        12
                    )

                    g.DrawEllipse(
                        cashPen,
                        136,
                        12,
                        8,
                        8
                    )

                    Using font As New Font(
                        "Segoe UI",
                        6,
                        FontStyle.Bold
                    )

                        g.DrawString(
                            "₱",
                            font,
                            New SolidBrush(
                                Color.FromArgb(
                                    46,
                                    186,
                                    104
                                )
                            ),
                            136.5F,
                            11
                        )

                    End Using

                End Using

                Using font As New Font(
                    "Segoe UI",
                    9
                )

                    Dim vehiclesText As String =
                        "Vehicles Today: 0"

                    If lblTotalSales.Tag IsNot Nothing Then

                        vehiclesText =
                            $"Vehicles Today: {lblTotalSales.Tag}"

                    End If

                    g.DrawString(
                        vehiclesText,
                        font,
                        New SolidBrush(
                            Color.FromArgb(
                                120,
                                125,
                                130
                            )
                        ),
                        11,
                        60
                    )

                End Using

            End Sub


        '====================================================
        ' RATE BUTTON DRAW
        '====================================================
        Dim drawRateBtn As Action(
            Of RadioButton,
               PaintEventArgs,
               String,
               String,
               String
        ) =
            Sub(
                rdo,
                ePaintBtn,
                icon,
                label,
                rate
            )

                Dim g As Graphics =
                    ePaintBtn.Graphics

                g.SmoothingMode =
                    Drawing2D.SmoothingMode.AntiAlias

                Dim isChecked As Boolean =
                    rdo.Checked

                Dim bgColor As Color =
                    If(
                        isChecked,
                        Color.FromArgb(
                            228,
                            240,
                            240
                        ),
                        Color.White
                    )

                Dim borderColor As Color =
                    If(
                        isChecked,
                        Color.FromArgb(
                            0,
                            139,
                            139
                        ),
                        Color.FromArgb(
                            220,
                            225,
                            230
                        )
                    )

                Dim textColor As Color =
                    If(
                        isChecked,
                        Color.FromArgb(
                            27,
                            42,
                            71
                        ),
                        Color.FromArgb(
                            80,
                            88,
                            102
                        )
                    )

                g.Clear(bgColor)

                Using p As New Pen(
                    borderColor,
                    1.5
                )

                    Dim radius As Integer = 8

                    Dim gp As New Drawing2D.GraphicsPath()

                    gp.AddArc(
                        0,
                        0,
                        radius,
                        radius,
                        180,
                        90
                    )

                    gp.AddArc(
                        rdo.Width - radius - 1,
                        0,
                        radius,
                        radius,
                        270,
                        90
                    )

                    gp.AddArc(
                        rdo.Width - radius - 1,
                        rdo.Height - radius - 1,
                        radius,
                        radius,
                        0,
                        90
                    )

                    gp.AddArc(
                        0,
                        rdo.Height - radius - 1,
                        radius,
                        radius,
                        90,
                        90
                    )

                    gp.CloseFigure()

                    g.DrawPath(p, gp)

                End Using

                Using iconFont As New Font(
                    "Segoe UI Emoji",
                    12
                )

                    g.DrawString(
                        icon,
                        iconFont,
                        New SolidBrush(textColor),
                        10,
                        8
                    )

                End Using

                Using f As New Font(
                    "Segoe UI",
                    8.25!,
                    FontStyle.Bold
                )

                    g.DrawString(
                        label,
                        f,
                        New SolidBrush(textColor),
                        33,
                        11
                    )

                    Dim rateSize =
                        g.MeasureString(rate, f)

                    g.DrawString(
                        rate,
                        f,
                        New SolidBrush(textColor),
                        rdo.Width -
                        rateSize.Width -
                        5,
                        11
                    )

                End Using

            End Sub


        AddHandler rdoCarr.Paint,
            Sub(sPaint, ePaintBtn)

                drawRateBtn(
                    rdoCarr,
                    ePaintBtn,
                    "🚗",
                    "Car",
                    "₱" &
                    ParkingData.CarBaseRate.ToString("F0") &
                    "/hr"
                )

            End Sub


        AddHandler rdoMotor.Paint,
            Sub(sPaint, ePaintBtn)

                drawRateBtn(
                    rdoMotor,
                    ePaintBtn,
                    "🏍️",
                    "Motor",
                    "₱" &
                    ParkingData.MotorBaseRate.ToString("F0") &
                    "/hr"
                )

            End Sub


        '====================================================
        ' MANAGE BUTTON ICON
        '====================================================
        AddHandler btnManage.Paint,
            Sub(sPaint, ePaintBtn)

                Dim g As Graphics =
                    ePaintBtn.Graphics

                g.SmoothingMode =
                    Drawing2D.SmoothingMode.AntiAlias

                Dim rect As New Rectangle(
                    25,
                    12,
                    16,
                    16
                )

                Using p As New Pen(
                    Color.White,
                    1.5
                )

                    g.DrawRectangle(
                        p,
                        rect.X,
                        rect.Y,
                        6,
                        6
                    )

                    g.DrawRectangle(
                        p,
                        rect.X + 8,
                        rect.Y,
                        6,
                        6
                    )

                    g.DrawRectangle(
                        p,
                        rect.X,
                        rect.Y + 8,
                        6,
                        6
                    )

                    g.DrawRectangle(
                        p,
                        rect.X + 8,
                        rect.Y + 8,
                        6,
                        6
                    )

                End Using

            End Sub


        Try

            If rdoCarr IsNot Nothing Then

                AddHandler rdoCarr.CheckedChanged,
                    Sub()
                        UpdateRateLabel()
                    End Sub

            End If

        Catch
        End Try


        Try

            If rdoMotor IsNot Nothing Then

                AddHandler rdoMotor.CheckedChanged,
                    Sub()
                        UpdateRateLabel()
                    End Sub

            End If

        Catch
        End Try


        UpdateRateLabel()

    End Sub


    '========================================================
    ' CONTEXT MENUS
    '========================================================
    Private Sub SetupContextMenus()

        '====================================================
        ' ACCOUNT CONTEXT MENU
        '====================================================
        Dim cmsAccounts As New ContextMenuStrip()

        Dim tsmiDeleteAccount As New ToolStripMenuItem(
            "Delete User"
        )

        cmsAccounts.Items.Add(
            tsmiDeleteAccount
        )

        dgvAccounts.ContextMenuStrip =
            cmsAccounts


        AddHandler tsmiDeleteAccount.Click,
            Sub(sender As Object, e As EventArgs)

                If dgvAccounts.SelectedRows.Count = 0 Then
                    Return
                End If

                Dim selectedRow =
                    dgvAccounts.SelectedRows(0)

                Dim username As String =
                    If(
                        selectedRow.Cells("Username").Value,
                        ""
                    ).ToString()

                Dim role As String =
                    If(
                        selectedRow.Cells("Role").Value,
                        ""
                    ).ToString()

                If username.ToLower() = "admin" Then

                    MessageBox.Show(
                        "Cannot delete the admin account.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )

                    Return

                End If

                Dim confirmDlg As New ConfirmActionDialogForm(
                    "Delete User",
                    $"Are you sure you want to delete user '{username}'?",
                    "YES"
                )

                If OverlayHelper.ShowDialog(
                    Me,
                    confirmDlg
                ) = DialogResult.Yes Then

                    Try

                        Using conn As New MySqlConnection(
                            DatabaseModule.connStr
                        )

                            conn.Open()

                            Dim tableName As String

                            If role.Equals(
                                "Teller",
                                StringComparison.OrdinalIgnoreCase
                            ) Then

                                tableName = "tblteller"

                            Else

                                tableName = "tblcustomer"

                            End If

                            Using cmd As New MySqlCommand(
                                "DELETE FROM " &
                                tableName &
                                " WHERE Username = @username",
                                conn
                            )

                                cmd.Parameters.AddWithValue(
                                    "@username",
                                    username
                                )

                                cmd.ExecuteNonQuery()

                            End Using

                        End Using

                        LoadUsersFromMySql()

                        dgvAccounts.DataSource = Nothing
                        dgvAccounts.DataSource =
                            ParkingData.UsersTable

                    Catch ex As Exception

                        MessageBox.Show(
                            "Unable to delete account: " &
                            ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        )

                    End Try

                End If

            End Sub


        '====================================================
        ' VEHICLE CONTEXT MENU
        '====================================================
        dgvVehicles.AllowUserToDeleteRows = False

        Dim cmsVehicles As New ContextMenuStrip()

        Dim tsmiDeleteVehicle As New ToolStripMenuItem(
            "Cancel Reservation"
        )

        cmsVehicles.Items.Add(
            tsmiDeleteVehicle
        )

        dgvVehicles.ContextMenuStrip =
            cmsVehicles


        AddHandler tsmiDeleteVehicle.Click,
            Sub(sender As Object, e As EventArgs)

                If dgvVehicles.SelectedRows.Count = 0 Then
                    Return
                End If

                Dim selectedRow =
                    dgvVehicles.SelectedRows(0)

                Dim paidStatus As String =
                    If(
                        selectedRow.Cells("PaidStatus").Value,
                        ""
                    ).ToString()

                If paidStatus <> "Reserved" Then

                    MessageBox.Show(
                        "Tellers are only allowed to cancel reservations. You cannot delete parking transaction records.",
                        "Permission Denied",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )

                    Exit Sub

                End If

                Dim plate As String =
                    If(
                        selectedRow.Cells("PlateNumber").Value,
                        ""
                    ).ToString()

                Dim slot As String =
                    If(
                        selectedRow.Cells("Slot").Value,
                        ""
                    ).ToString()

                Dim code As String =
                    If(
                        selectedRow.Cells("Code").Value,
                        ""
                    ).ToString()

                Dim confirmDlg As New ConfirmActionDialogForm(
                    "Cancel Reservation",
                    $"Are you sure you want to cancel the reservation for '{plate}' in slot {slot}?",
                    "YES"
                )

                If OverlayHelper.ShowDialog(
                    Me,
                    confirmDlg
                ) = DialogResult.Yes Then

                    Try

                        Using conn As New MySqlConnection(
                            DatabaseModule.connStr
                        )

                            conn.Open()

                            Using cmd As New MySqlCommand(
                                "DELETE FROM tblparkingrecord " &
                                "WHERE code = @code " &
                                "AND `Paid Status` = 'Reserved'",
                                conn
                            )

                                cmd.Parameters.AddWithValue(
                                    "@code",
                                    code
                                )

                                cmd.ExecuteNonQuery()

                            End Using

                        End Using

                        LoadParkingRecordsFromMySql()
                        SyncSlotAvailabilityFromRecords()
                        UpdateSlots()

                    Catch ex As Exception

                        MessageBox.Show(
                            "Unable to cancel reservation: " &
                            ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        )

                    End Try

                End If

            End Sub

    End Sub


    '========================================================
    ' RATE LABEL
    '========================================================
    Private Sub UpdateRateLabel()

        Try

            If rdoCarr IsNot Nothing AndAlso
               rdoCarr.Checked Then

                If lblPrice IsNot Nothing Then

                    lblPrice.Text =
                        "₱" &
                        ParkingData.CarBaseRate.ToString("F2")

                End If

                If rdoMotor IsNot Nothing Then
                    rdoMotor.Invalidate()
                End If

                rdoCarr.Invalidate()

                Return

            End If


            If rdoMotor IsNot Nothing AndAlso
               rdoMotor.Checked Then

                If lblPrice IsNot Nothing Then

                    lblPrice.Text =
                        "₱" &
                        ParkingData.MotorBaseRate.ToString("F2")

                End If

                If rdoCarr IsNot Nothing Then
                    rdoCarr.Invalidate()
                End If

                rdoMotor.Invalidate()

                Return

            End If

        Catch
        End Try

    End Sub


    '========================================================
    ' UPDATE SLOTS
    '========================================================
    Public Sub UpdateSlots()

        Dim maxCarSlots As Integer = 50
        Dim maxMotorSlots As Integer = 20

        ParkingData.InitializeDatabase()

        LoadParkingRecordsFromMySql()
        SyncSlotAvailabilityFromRecords()


        Dim occupiedCars As Integer = 0
        Dim occupiedMotors As Integer = 0

        Dim totalSales As Double = 0.0
        Dim todayRevenue As Double = 0.0

        Dim vehiclesToday As Integer = 0


        If ParkingData.ParkingTable IsNot Nothing Then

            For Each row As DataRow In
                ParkingData.ParkingTable.Rows

                Dim status As String =
                    row("PaidStatus").ToString()

                If status = "Not Paid" OrElse
                   (
                       status = "Reserved" AndAlso
                       Not IsDBNull(row("ReservationDate")) AndAlso
                       Convert.ToDateTime(
                           row("ReservationDate")
                       ).Date <= DateTime.Now.Date
                   ) Then

                    If row("VehicleType").ToString() =
                       "Four Wheels" Then

                        occupiedCars += 1

                    ElseIf row("VehicleType").ToString() =
                           "Two Wheels" Then

                        occupiedMotors += 1

                    End If

                End If


                Dim checkInDate As DateTime

                If DateTime.TryParse(
                    row("CheckIn").ToString(),
                    checkInDate
                ) Then

                    If checkInDate.Date =
                       DateTime.Today Then

                        vehiclesToday += 1

                    End If

                End If


                If Not IsDBNull(row("TotalAmount")) Then

                    totalSales +=
                        Convert.ToDouble(
                            row("TotalAmount")
                        )

                End If


                If status = "Paid" AndAlso
                   Not IsDBNull(row("TotalAmount")) Then

                    Dim checkOutDate As DateTime

                    If DateTime.TryParse(
                        row("CheckOut").ToString(),
                        checkOutDate
                    ) Then

                        If checkOutDate.Date =
                           DateTime.Today Then

                            todayRevenue +=
                                Convert.ToDouble(
                                    row("TotalAmount")
                                )

                        End If

                    End If

                End If

            Next

        End If


        Dim availableCars As Integer =
            maxCarSlots - occupiedCars

        Dim availableMotors As Integer =
            maxMotorSlots - occupiedMotors


        lblAvailableCars.Text =
            $"{availableCars} / {maxCarSlots}"

        lblOccupiedCars.Text =
            "available"


        lblAvailableMotors.Text =
            $"{availableMotors} / {maxMotorSlots}"

        lblOccupiedMotors.Text =
            "available"


        lblTotalSales.Text =
            $"₱{todayRevenue.ToString("F2")}"

        lblTotalSales.Tag =
            vehiclesToday


        pnlStatsCars.Invalidate()
        pnlStatsMotors.Invalidate()


        '====================================================
        ' TRANSACTION GRID
        '====================================================
        dgvVehicles.DataSource = Nothing

        dgvVehicles.DataSource =
            ParkingData.ParkingTable


        If dgvVehicles.Columns.Contains("Code") Then

            dgvVehicles.Columns("Code").Visible =
                False

        End If


        If dgvVehicles.Columns.Contains("PlateNumber") Then

            dgvVehicles.Columns("PlateNumber").HeaderText =
                "Plate Number"

        End If


        If dgvVehicles.Columns.Contains("CheckIn") Then

            dgvVehicles.Columns("CheckIn").HeaderText =
                "Time In"

            dgvVehicles.Columns("CheckIn").
                DefaultCellStyle.Format =
                "yyyy-MM-dd hh:mm tt"

        End If


        If dgvVehicles.Columns.Contains("CheckOut") Then

            dgvVehicles.Columns("CheckOut").HeaderText =
                "Time Out"

            dgvVehicles.Columns("CheckOut").
                DefaultCellStyle.Format =
                "yyyy-MM-dd hh:mm tt"

        End If


        If dgvVehicles.Columns.Contains("VehicleType") Then

            dgvVehicles.Columns("VehicleType").HeaderText =
                "Vehicle Type"

        End If


        If dgvVehicles.Columns.Contains("RateName") Then

            dgvVehicles.Columns("RateName").Visible =
                False

        End If


        If dgvVehicles.Columns.Contains("Rate") Then

            dgvVehicles.Columns("Rate").Visible =
                False

        End If


        If dgvVehicles.Columns.Contains("Slot") Then

            dgvVehicles.Columns("Slot").HeaderText =
                "Parking Slot"

        End If


        If dgvVehicles.Columns.Contains("TotalTime") Then

            dgvVehicles.Columns("TotalTime").HeaderText =
                "Duration"

        End If


        If dgvVehicles.Columns.Contains("TotalAmount") Then

            dgvVehicles.Columns("TotalAmount").
                DefaultCellStyle.Format =
                "₱0.00"

            dgvVehicles.Columns("TotalAmount").
                DefaultCellStyle.NullValue =
                "₱"

        End If


        If dgvVehicles.Columns.Contains("PaidStatus") Then

            dgvVehicles.Columns("PaidStatus").HeaderText =
                "Paid Status"

        End If


        dgvVehicles.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells


        '====================================================
        ' LOBBY MODE
        '====================================================
        If ShowLobbyMode Then

            If dgvVehicles IsNot Nothing Then
                dgvVehicles.Visible = False
            End If

            If lblTotalSales IsNot Nothing Then
                lblTotalSales.Visible = False
            End If

            If btnManage IsNot Nothing Then
                btnManage.Visible = False
            End If

            If Button1 IsNot Nothing Then
                Button1.Visible = False
            End If

        Else

            If dgvVehicles IsNot Nothing Then
                dgvVehicles.Visible = True
            End If

            If lblTotalSales IsNot Nothing Then
                lblTotalSales.Visible =
                    (Form1.CurrentUserRole = "Admin")
            End If

            If btnManage IsNot Nothing Then
                btnManage.Visible = True
            End If

            If btnSettings IsNot Nothing Then
                btnSettings.Visible =
                    (Form1.CurrentUserRole = "Admin")
            End If

            If Button1 IsNot Nothing Then
                Button1.Visible =
                    (Form1.CurrentUserRole = "Admin")
            End If

        End If


        '====================================================
        ' CLEAR OLD SLOT PANELS
        '====================================================
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


        '====================================================
        ' CREATE PARKING SLOTS
        '====================================================
        For Each slotEntry In ParkingData.parkingDatabase

            Dim slotName As String =
                slotEntry.Key

            Dim isAvailable As Boolean =
                slotEntry.Value


            Dim plateText As String = ""
            Dim isReserved As Boolean = False
            Dim reservedBy As String = ""
            Dim resDateStr As String = ""


            For Each row As DataRow In
                ParkingData.ParkingTable.Rows

                If row("Slot").ToString() =
                   slotName Then

                    Dim status As String =
                        row("PaidStatus").ToString()


                    If status = "Not Paid" Then

                        plateText =
                            row("PlateNumber").ToString()

                        Exit For


                    ElseIf status = "Reserved" Then

                        If Not IsDBNull(
                            row("ReservationDate")
                        ) AndAlso
                           Convert.ToDateTime(
                               row("ReservationDate")
                           ).Date <= DateTime.Now.Date Then

                            If row.Table.Columns.Contains(
                                "ReservedBy"
                            ) AndAlso
                               Not IsDBNull(
                                   row("ReservedBy")
                               ) Then

                                reservedBy =
                                    row("ReservedBy").ToString()

                            End If


                            resDateStr =
                                Convert.ToDateTime(
                                    row("ReservationDate")
                                ).ToString("MM/dd")

                            plateText =
                                "RESERVED"

                            isReserved =
                                True

                            Exit For

                        End If

                    End If

                End If

            Next


            '================================================
            ' SLOT PANEL
            '================================================
            Dim pnlSlot As New Panel()

            pnlSlot.Size =
                New Size(72, 85)

            pnlSlot.Margin =
                New Padding(4)

            pnlSlot.BackColor =
                Color.White


            Dim baseColor As Color
            Dim hoverColor As Color


            If isReserved Then

                baseColor =
                    Color.FromArgb(
                        240,
                        160,
                        40
                    )

                hoverColor =
                    Color.FromArgb(
                        250,
                        180,
                        60
                    )


            ElseIf isAvailable Then

                baseColor =
                    Color.FromArgb(
                        0,
                        168,
                        181
                    )

                hoverColor =
                    Color.FromArgb(
                        20,
                        188,
                        201
                    )


            Else

                baseColor =
                    Color.FromArgb(
                        220,
                        60,
                        60
                    )

                hoverColor =
                    Color.FromArgb(
                        240,
                        80,
                        80
                    )

            End If


            Dim currentBgColor =
                baseColor


            AddHandler pnlSlot.Paint,
                Sub(sSender, sEvent)

                    sEvent.Graphics.SmoothingMode =
                        Drawing2D.SmoothingMode.AntiAlias

                    Dim rect As New Rectangle(
                        0,
                        0,
                        pnlSlot.Width - 1,
                        pnlSlot.Height - 1
                    )

                    Dim path As New Drawing2D.GraphicsPath()

                    Dim r As Integer = 8

                    path.AddArc(
                        rect.X,
                        rect.Y,
                        r,
                        r,
                        180,
                        90
                    )

                    path.AddArc(
                        rect.Right - r,
                        rect.Y,
                        r,
                        r,
                        270,
                        90
                    )

                    path.AddArc(
                        rect.Right - r,
                        rect.Bottom - r,
                        r,
                        r,
                        0,
                        90
                    )

                    path.AddArc(
                        rect.X,
                        rect.Bottom - r,
                        r,
                        r,
                        90,
                        90
                    )

                    path.CloseFigure()


                    Using brush As New SolidBrush(
                        currentBgColor
                    )

                        sEvent.Graphics.FillPath(
                            brush,
                            path
                        )

                    End Using

                End Sub


            AddHandler pnlSlot.MouseEnter,
                Sub(s, ev)

                    currentBgColor =
                        hoverColor

                    pnlSlot.Invalidate()

                End Sub


            AddHandler pnlSlot.MouseLeave,
                Sub(s, ev)

                    currentBgColor =
                        baseColor

                    pnlSlot.Invalidate()

                End Sub


            '================================================
            ' SLOT LABELS
            '================================================
            Dim lblName As New Label()

            lblName.Text =
                slotName

            lblName.Font =
                New Font(
                    "Segoe UI",
                    9,
                    FontStyle.Bold
                )

            lblName.ForeColor =
                Color.White

            lblName.Location =
                New Point(0, 5)

            lblName.Size =
                New Size(72, 20)

            lblName.TextAlign =
                ContentAlignment.MiddleCenter

            lblName.BackColor =
                Color.Transparent

            pnlSlot.Controls.Add(lblName)


            Dim lblStatus As New Label()

            If isReserved Then

                lblStatus.Text =
                    "RESERVED"

            ElseIf isAvailable Then

                lblStatus.Text =
                    "AVAILABLE"

            Else

                lblStatus.Text =
                    If(
                        plateText = "",
                        "OCCUPIED",
                        plateText
                    )

            End If


            lblStatus.Font =
                New Font(
                    "Segoe UI",
                    7.5!,
                    FontStyle.Bold
                )

            lblStatus.ForeColor =
                Color.White

            If isReserved Then

                lblStatus.Location =
                    New Point(0, 25)

            Else

                lblStatus.Location =
                    New Point(0, 25)

            End If

            lblStatus.Size =
                New Size(72, 20)

            lblStatus.TextAlign =
                ContentAlignment.MiddleCenter

            lblStatus.BackColor =
                Color.Transparent

            pnlSlot.Controls.Add(lblStatus)


            Dim lblReserver As Label = Nothing
            Dim lblDate As Label = Nothing


            If isReserved Then

                If reservedBy <> "" Then

                    lblReserver =
                        New Label()

                    lblReserver.Text =
                        reservedBy

                    lblReserver.Font =
                        New Font(
                            "Segoe UI",
                            7.5!
                        )

                    lblReserver.ForeColor =
                        Color.FromArgb(
                            240,
                            240,
                            240
                        )

                    lblReserver.Location =
                        New Point(0, 40)

                    lblReserver.Size =
                        New Size(72, 20)

                    lblReserver.TextAlign =
                        ContentAlignment.MiddleCenter

                    lblReserver.BackColor =
                        Color.Transparent

                    pnlSlot.Controls.Add(
                        lblReserver
                    )

                End If


                If resDateStr <> "" Then

                    lblDate =
                        New Label()

                    lblDate.Text =
                        resDateStr

                    lblDate.Font =
                        New Font(
                            "Segoe UI",
                            7.5!,
                            FontStyle.Italic
                        )

                    lblDate.ForeColor =
                        Color.FromArgb(
                            220,
                            220,
                            220
                        )

                    lblDate.Location =
                        New Point(0, 60)

                    lblDate.Size =
                        New Size(72, 20)

                    lblDate.TextAlign =
                        ContentAlignment.MiddleCenter

                    lblDate.BackColor =
                        Color.Transparent

                    pnlSlot.Controls.Add(
                        lblDate
                    )

                End If

            End If


            '================================================
            ' LABEL HOVER
            '================================================
            AddHandler lblName.MouseEnter,
                Sub(s, ev)

                    currentBgColor =
                        hoverColor

                    pnlSlot.Invalidate()

                End Sub


            AddHandler lblName.MouseLeave,
                Sub(s, ev)

                    currentBgColor =
                        baseColor

                    pnlSlot.Invalidate()

                End Sub


            AddHandler lblStatus.MouseEnter,
                Sub(s, ev)

                    currentBgColor =
                        hoverColor

                    pnlSlot.Invalidate()

                End Sub


            AddHandler lblStatus.MouseLeave,
                Sub(s, ev)

                    currentBgColor =
                        baseColor

                    pnlSlot.Invalidate()

                End Sub


            If lblReserver IsNot Nothing Then

                AddHandler lblReserver.MouseEnter,
                    Sub(s, ev)

                        currentBgColor =
                            hoverColor

                        pnlSlot.Invalidate()

                    End Sub


                AddHandler lblReserver.MouseLeave,
                    Sub(s, ev)

                        currentBgColor =
                            baseColor

                        pnlSlot.Invalidate()

                    End Sub

            End If


            If lblDate IsNot Nothing Then

                AddHandler lblDate.MouseEnter,
                    Sub(s, ev)

                        currentBgColor =
                            hoverColor

                        pnlSlot.Invalidate()

                    End Sub


                AddHandler lblDate.MouseLeave,
                    Sub(s, ev)

                        currentBgColor =
                            baseColor

                        pnlSlot.Invalidate()

                    End Sub

            End If


            '================================================
            ' AVAILABLE SLOT
            '================================================
            If isAvailable Then

                pnlSlot.Cursor =
                    Cursors.Hand


                AddHandler pnlSlot.Click,
                    Sub()

                        Dim resDlg As New ReserveDialogForm(
                            slotName
                        )


                        If OverlayHelper.ShowDialog(
                            Me,
                            resDlg
                        ) = DialogResult.OK Then

                            Dim resPlate As String =
                                resDlg.PlateNumber


                            '================================
                            ' DUPLICATE PLATE CHECK
                            '================================
                            Dim isDuplicate As Boolean =
                                False


                            For Each row As DataRow In ParkingData.ParkingTable.Rows

                                Dim status As String = row("PaidStatus").ToString()
                                Dim plate As String = row("PlateNumber").ToString()

                                If plate.Equals(resPlate, StringComparison.OrdinalIgnoreCase) AndAlso
       (status = "Not Paid" OrElse status = "Reserved") Then

                                    isDuplicate = True
                                    Exit For

                                End If

                            Next


                            If isDuplicate Then

                                MessageBox.Show(
                                    "This plate number is already parked or has an active reservation.",
                                    "Duplicate Plate",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                )

                            Else

                                Dim resDate As DateTime =
                                    resDlg.ReservationDate


                                If resDate.Date =
                                   DateTime.Now.Date Then

                                    ParkingData.
                                        parkingDatabase(
                                            slotName
                                        ) = False

                                End If


                                Dim newCode As String =
                                    ParkingData.GenerateCode()


                                Dim vehicleType As String =
                                    If(
                                        slotName.StartsWith("C"),
                                        "Four Wheels",
                                        "Two Wheels"
                                    )


                                Dim rateValue As Double =
                                    If(
                                        vehicleType =
                                        "Four Wheels",
                                        ParkingData.CarBaseRate,
                                        ParkingData.MotorBaseRate
                                    )


                                Dim customerID As Object =
                                    DBNull.Value

                                Dim tellerID As Object =
                                    DBNull.Value


                                If Form1.CurrentUserRole.Equals(
                                    "Customer",
                                    StringComparison.OrdinalIgnoreCase
                                ) Then

                                    customerID =
                                        GetCurrentUserID(
                                            "Customer",
                                            Form1.CurrentUsername
                                        )

                                ElseIf Form1.CurrentUserRole.Equals(
                                    "Teller",
                                    StringComparison.OrdinalIgnoreCase
                                ) Then

                                    tellerID =
                                        GetCurrentUserID(
                                            "Teller",
                                            Form1.CurrentUsername
                                        )

                                End If


                                '================================
                                ' INSERT TO MYSQL
                                '================================
                                Try

                                    Using conn As New MySqlConnection(
                                        DatabaseModule.connStr
                                    )

                                        conn.Open()


                                        Dim sql As String =
                                            "INSERT INTO tblparkingrecord " &
                                            "(code, PlateNumber, VehicleType, " &
                                            "`Parking Slot`, RateName, Rate, " &
                                            "Duration, `Paid Status`, " &
                                            "ReservationDate, CustomerID, TellerID) " &
                                            "VALUES " &
                                            "(@code, @plate, @vtype, " &
                                            "@slot, @ratename, @rate, " &
                                            "@duration, @status, " &
                                            "@resdate, @customerID, @tellerID)"


                                        Using cmd As New MySqlCommand(
                                            sql,
                                            conn
                                        )

                                            cmd.Parameters.AddWithValue(
                                                "@code",
                                                newCode
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@plate",
                                                resPlate
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@vtype",
                                                vehicleType
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@slot",
                                                slotName
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@ratename",
                                                "Standard"
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@rate",
                                                rateValue
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@duration",
                                                "0 hour(s)"
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@status",
                                                "Reserved"
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@resdate",
                                                resDate
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@customerID",
                                                customerID
                                            )

                                            cmd.Parameters.AddWithValue(
                                                "@tellerID",
                                                tellerID
                                            )

                                            cmd.ExecuteNonQuery()

                                        End Using

                                    End Using


                                    LoadParkingRecordsFromMySql()

                                    SyncSlotAvailabilityFromRecords()

                                    UpdateSlots()


                                    SuccessDialogForm.ShowSuccess(
                                        "Vehicle successfully parked at slot " &
                                        slotName
                                    )


                                Catch ex As Exception

                                    MessageBox.Show(
                                        "Unable to save reservation: " &
                                        ex.Message,
                                        "Database Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error
                                    )

                                End Try

                            End If

                        End If

                    End Sub


                AddHandler lblName.Click,
                    Sub()

                        pnlSlot_ClickProxy(
                            pnlSlot
                        )

                    End Sub


                AddHandler lblStatus.Click,
                    Sub()

                        pnlSlot_ClickProxy(
                            pnlSlot
                        )

                    End Sub


                If lblReserver IsNot Nothing Then

                    AddHandler lblReserver.Click,
                        Sub()

                            pnlSlot_ClickProxy(
                                pnlSlot
                            )

                        End Sub

                End If


                '================================================
                ' RESERVED SLOT
                '================================================
            ElseIf isReserved Then

                pnlSlot.Cursor =
                    Cursors.Hand


                AddHandler pnlSlot.Click,
                    Sub()

                        Dim manageDlg As New ManageReservationDialogForm(
                            slotName,
                            plateText,
                            reservedBy
                        )


                        If OverlayHelper.ShowDialog(
                            Me,
                            manageDlg
                        ) = DialogResult.OK Then


                            '================================
                            ' CANCEL
                            '================================
                            If manageDlg.SelectedAction =
                               "Cancel" Then


                                Dim confirmDlg As New ConfirmActionDialogForm(
                                    "Cancel Reservation",
                                    $"Are you sure you want to cancel the reservation for slot {slotName}?",
                                    "YES"
                                )


                                If OverlayHelper.ShowDialog(
                                    Me,
                                    confirmDlg
                                ) = DialogResult.Yes Then


                                    Dim reservationCode As String =
                                        ""


                                    For Each row As DataRow In
                                        ParkingData.ParkingTable.Rows

                                        If row("Slot").
                                           ToString() =
                                           slotName AndAlso
                                           row("PaidStatus").
                                           ToString() =
                                           "Reserved" Then

                                            reservationCode =
                                                row("Code").ToString()

                                            Exit For

                                        End If

                                    Next


                                    If reservationCode <> "" Then

                                        Try

                                            Using conn As New MySqlConnection(
                                                DatabaseModule.connStr
                                            )

                                                conn.Open()


                                                Using cmd As New MySqlCommand(
                                                    "DELETE FROM tblparkingrecord " &
                                                    "WHERE code = @code " &
                                                    "AND `Paid Status` = 'Reserved'",
                                                    conn
                                                )

                                                    cmd.Parameters.AddWithValue(
                                                        "@code",
                                                        reservationCode
                                                    )

                                                    cmd.ExecuteNonQuery()

                                                End Using

                                            End Using


                                            LoadParkingRecordsFromMySql()

                                            SyncSlotAvailabilityFromRecords()

                                            UpdateSlots()


                                        Catch ex As Exception

                                            MessageBox.Show(
                                                "Unable to cancel reservation: " &
                                                ex.Message,
                                                "Database Error",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error
                                            )

                                        End Try

                                    End If

                                End If


                                '================================
                                ' CHECK IN
                                '================================
                            ElseIf manageDlg.SelectedAction =
                                   "CheckIn" Then


                                Dim checkedInCode As String =
                                    ""


                                For Each row As DataRow In
                                    ParkingData.ParkingTable.Rows

                                    If row("Slot").
                                       ToString() =
                                       slotName AndAlso
                                       row("PaidStatus").
                                       ToString() =
                                       "Reserved" Then

                                        checkedInCode =
                                            row("Code").ToString()

                                        Exit For

                                    End If

                                Next


                                If checkedInCode <> "" Then

                                    Try

                                        Dim checkInTime As DateTime =
                                            DateTime.Now


                                        Using conn As New MySqlConnection(
                                            DatabaseModule.connStr
                                        )

                                            conn.Open()


                                            Using cmd As New MySqlCommand(
                                                "UPDATE tblparkingrecord " &
                                                "SET `Paid Status` = 'Not Paid', " &
                                                "CheckIn = @checkin " &
                                                "WHERE code = @code",
                                                conn
                                            )

                                                cmd.Parameters.AddWithValue(
                                                    "@checkin",
                                                    checkInTime
                                                )

                                                cmd.Parameters.AddWithValue(
                                                    "@code",
                                                    checkedInCode
                                                )

                                                cmd.ExecuteNonQuery()

                                            End Using

                                        End Using


                                        LoadParkingRecordsFromMySql()

                                        SyncSlotAvailabilityFromRecords()

                                        UpdateSlots()


                                        SuccessDialogForm.ShowSuccess(
                                            $"Vehicle {plateText} successfully checked in at slot {slotName}!"
                                        )


                                    Catch ex As Exception

                                        MessageBox.Show(
                                            "Unable to check in vehicle: " &
                                            ex.Message,
                                            "Database Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error
                                        )

                                    End Try

                                End If

                            End If

                        End If

                    End Sub


                AddHandler lblName.Click,
                    Sub()

                        pnlSlot_ClickProxy(
                            pnlSlot
                        )

                    End Sub


                AddHandler lblStatus.Click,
                    Sub()

                        pnlSlot_ClickProxy(
                            pnlSlot
                        )

                    End Sub


                If lblReserver IsNot Nothing Then

                    AddHandler lblReserver.Click,
                        Sub()

                            pnlSlot_ClickProxy(
                                pnlSlot
                            )

                        End Sub

                End If


                If lblDate IsNot Nothing Then

                    AddHandler lblDate.Click,
                        Sub()

                            pnlSlot_ClickProxy(
                                pnlSlot
                            )

                        End Sub

                End If

            End If


            '================================================
            ' ADD SLOT TO CORRECT PANEL
            '================================================
            Dim slotNum As Integer = 0

            Dim numPart As String =
                slotName.Substring(1)

            Integer.TryParse(
                numPart,
                slotNum
            )


            If slotNum <= 25 Then

                flpCarSlots.Controls.Add(
                    pnlSlot
                )

            Else

                flpMotorSlots.Controls.Add(
                    pnlSlot
                )

            End If

        Next


        flpCarSlots.ResumeLayout(True)
        flpMotorSlots.ResumeLayout(True)

    End Sub


    '========================================================
    ' LOAD USERS FROM MYSQL
    '========================================================
    Private Sub LoadUsersFromMySql()

        ParkingData.InitializeUsers()


        Try

            Using conn As New MySqlConnection(
                DatabaseModule.connStr
            )

                conn.Open()


                Dim sql As String =
                    "SELECT Fullname AS FullName, " &
                    "Username, Password, " &
                    "'Customer' AS Role " &
                    "FROM tblcustomer " &
                    "UNION ALL " &
                    "SELECT FullName, Username, Password, " &
                    "'Teller' AS Role " &
                    "FROM tblteller " &
                    "ORDER BY Username"


                Using cmd As New MySqlCommand(
                    sql,
                    conn
                )

                    Using reader As MySqlDataReader =
                        cmd.ExecuteReader()


                        ParkingData.UsersTable.Clear()


                        While reader.Read()

                            Dim row As DataRow =
                                ParkingData.UsersTable.NewRow()


                            row("FullName") =
                                If(
                                    reader.IsDBNull(
                                        reader.GetOrdinal(
                                            "FullName"
                                        )
                                    ),
                                    "",
                                    reader("FullName").ToString()
                                )


                            row("Username") =
                                If(
                                    reader.IsDBNull(
                                        reader.GetOrdinal(
                                            "Username"
                                        )
                                    ),
                                    "",
                                    reader("Username").ToString()
                                )


                            row("Password") =
                                If(
                                    reader.IsDBNull(
                                        reader.GetOrdinal(
                                            "Password"
                                        )
                                    ),
                                    "",
                                    reader("Password").ToString()
                                )


                            row("Role") =
                                If(
                                    reader.IsDBNull(
                                        reader.GetOrdinal(
                                            "Role"
                                        )
                                    ),
                                    "",
                                    reader("Role").ToString()
                                )


                            ParkingData.UsersTable.Rows.Add(
                                row
                            )

                        End While


                        '====================================
                        ' ADMIN ACCOUNT
                        '====================================
                        Dim adminRow As DataRow =
                            ParkingData.UsersTable.NewRow()


                        adminRow("FullName") =
                            "Administrator"

                        adminRow("Username") =
                            "admin"

                        adminRow("Password") =
                            "1234"

                        adminRow("Role") =
                            "Admin"


                        ParkingData.UsersTable.Rows.InsertAt(
                            adminRow,
                            0
                        )


                    End Using

                End Using

            End Using


        Catch ex As Exception

            MessageBox.Show(
                "Unable to load accounts from MySQL: " &
                ex.Message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' LOAD PARKING RECORDS FROM MYSQL
    '========================================================
    Private Sub LoadParkingRecordsFromMySql()

        ParkingData.InitializeDatabase()


        Try

            Using conn As New MySqlConnection(
                DatabaseModule.connStr
            )

                conn.Open()


                Dim sql As String =
                    "SELECT " &
                    "p.TransactionID, " &
                    "p.code, " &
                    "p.PlateNumber, " &
                    "p.VehicleType, " &
                    "p.`Parking Slot`, " &
                    "p.CheckIn, " &
                    "p.CheckOut, " &
                    "p.RateName, " &
                    "p.Rate, " &
                    "p.Duration, " &
                    "p.TotalAmount, " &
                    "p.`Paid Status`, " &
                    "p.ReservationDate, " &
                    "p.CustomerID, " &
                    "p.TellerID, " &
                    "COALESCE(" &
                    "c.Fullname, " &
                    "t.FullName, " &
                    "'') AS DisplayName " &
                    "FROM tblparkingrecord p " &
                    "LEFT JOIN tblcustomer c " &
                    "ON p.CustomerID = c.CustomerID " &
                    "LEFT JOIN tblteller t " &
                    "ON p.TellerID = t.TellerID " &
                    "ORDER BY p.TransactionID DESC"


                Using cmd As New MySqlCommand(
                    sql,
                    conn
                )

                    Using reader As MySqlDataReader =
                        cmd.ExecuteReader()


                        ParkingData.ParkingTable.Clear()


                        While reader.Read()

                            Dim row As DataRow =
                                ParkingData.ParkingTable.NewRow()


                            row("Code") =
                                DbString(
                                    reader,
                                    "code"
                                )


                            row("PlateNumber") =
                                DbString(
                                    reader,
                                    "PlateNumber"
                                )


                            row("CheckIn") =
                                DbValue(
                                    reader,
                                    "CheckIn"
                                )


                            row("CheckOut") =
                                DbValue(
                                    reader,
                                    "CheckOut"
                                )


                            row("VehicleType") =
                                DbString(
                                    reader,
                                    "VehicleType"
                                )


                            row("RateName") =
                                DbString(
                                    reader,
                                    "RateName"
                                )


                            row("Rate") =
                                DbDouble(
                                    reader,
                                    "Rate"
                                )


                            row("Slot") =
                                DbString(
                                    reader,
                                    "Parking Slot"
                                )


                            row("TotalTime") =
                                DbString(
                                    reader,
                                    "Duration"
                                )


                            row("TotalAmount") =
                                DbDoubleOrNull(
                                    reader,
                                    "TotalAmount"
                                )


                            row("PaidStatus") =
                                DbString(
                                    reader,
                                    "Paid Status"
                                )


                            If row.Table.Columns.Contains(
                                "ReservedBy"
                            ) Then

                                row("ReservedBy") =
                                    DbString(
                                        reader,
                                        "DisplayName"
                                    )

                            End If


                            If row.Table.Columns.Contains(
                                "ProcessedBy"
                            ) Then

                                row("ProcessedBy") =
                                    DbString(
                                        reader,
                                        "DisplayName"
                                    )

                            End If


                            row("ReservationDate") =
                                DbValue(
                                    reader,
                                    "ReservationDate"
                                )


                            ParkingData.ParkingTable.Rows.Add(
                                row
                            )

                        End While


                    End Using

                End Using

            End Using


        Catch ex As Exception

            MessageBox.Show(
                "Unable to load parking records from MySQL: " &
                ex.Message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' SYNC SLOT AVAILABILITY
    '========================================================
    Private Sub SyncSlotAvailabilityFromRecords()

        ParkingData.InitializeDatabase()


        For Each key As String In
            New List(Of String)(
                ParkingData.parkingDatabase.Keys
            )

            ParkingData.parkingDatabase(key) =
                True

        Next


        For Each row As DataRow In
            ParkingData.ParkingTable.Rows

            Dim slot As String =
                row("Slot").ToString()

            Dim status As String =
                row("PaidStatus").ToString()


            If slot <> "" AndAlso
               ParkingData.parkingDatabase.ContainsKey(
                   slot
               ) Then


                If status = "Not Paid" Then

                    ParkingData.parkingDatabase(slot) =
                        False


                ElseIf status = "Reserved" AndAlso
                       Not IsDBNull(
                           row("ReservationDate")
                       ) Then

                    If Convert.ToDateTime(
                        row("ReservationDate")
                    ).Date <= DateTime.Today Then

                        ParkingData.parkingDatabase(slot) =
                            False

                    End If

                End If

            End If

        Next

    End Sub


    '========================================================
    ' GET CURRENT USER ID
    '========================================================
    Private Function GetCurrentUserID(
        role As String,
        username As String
    ) As Object

        If String.IsNullOrWhiteSpace(username) Then
            Return DBNull.Value
        End If


        Try

            Using conn As New MySqlConnection(
                DatabaseModule.connStr
            )

                conn.Open()


                If role.Equals(
                    "Customer",
                    StringComparison.OrdinalIgnoreCase
                ) Then


                    Using cmd As New MySqlCommand(
                        "SELECT CustomerID " &
                        "FROM tblcustomer " &
                        "WHERE Username = @username " &
                        "LIMIT 1",
                        conn
                    )

                        cmd.Parameters.AddWithValue(
                            "@username",
                            username
                        )


                        Dim result As Object =
                            cmd.ExecuteScalar()


                        If result IsNot Nothing AndAlso
                           result IsNot DBNull.Value Then

                            Return result

                        End If

                    End Using


                ElseIf role.Equals(
                    "Teller",
                    StringComparison.OrdinalIgnoreCase
                ) Then


                    Using cmd As New MySqlCommand(
                        "SELECT TellerID " &
                        "FROM tblteller " &
                        "WHERE Username = @username " &
                        "LIMIT 1",
                        conn
                    )

                        cmd.Parameters.AddWithValue(
                            "@username",
                            username
                        )


                        Dim result As Object =
                            cmd.ExecuteScalar()


                        If result IsNot Nothing AndAlso
                           result IsNot DBNull.Value Then

                            Return result

                        End If

                    End Using

                End If

            End Using


        Catch

        End Try


        Return DBNull.Value

    End Function


    '========================================================
    ' DATABASE HELPERS
    '========================================================
    Private Function DbValue(
        reader As MySqlDataReader,
        columnName As String
    ) As Object

        Dim ordinal As Integer =
            reader.GetOrdinal(columnName)


        If reader.IsDBNull(ordinal) Then
            Return DBNull.Value
        End If


        Return reader.GetValue(ordinal)

    End Function


    Private Function DbString(
        reader As MySqlDataReader,
        columnName As String
    ) As String

        Dim value As Object =
            DbValue(
                reader,
                columnName
            )


        If value Is DBNull.Value OrElse
           value Is Nothing Then

            Return ""

        End If


        Return value.ToString()

    End Function


    Private Function DbDouble(
        reader As MySqlDataReader,
        columnName As String
    ) As Double

        Dim value As Object =
            DbValue(
                reader,
                columnName
            )


        If value Is DBNull.Value OrElse
           value Is Nothing Then

            Return 0.0

        End If


        Return Convert.ToDouble(value)

    End Function


    Private Function DbDoubleOrNull(
        reader As MySqlDataReader,
        columnName As String
    ) As Object

        Dim value As Object =
            DbValue(
                reader,
                columnName
            )


        If value Is DBNull.Value OrElse
           value Is Nothing Then

            Return DBNull.Value

        End If


        Return Convert.ToDouble(value)

    End Function


    '========================================================
    ' MANAGE BUTTON
    '========================================================
    Private Sub btnManage_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnManage.Click

        ParkingManagerForm.Show()

        Me.Hide()

    End Sub


    '========================================================
    ' EXIT
    '========================================================
    Private Sub btnExit_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnExit.Click

        Dim exitDlg As New ConfirmExitDialog()


        If OverlayHelper.ShowDialog(
            Me,
            exitDlg
        ) = DialogResult.Yes Then

            Form1.Show()

            Me.Close()

        End If

    End Sub


    '========================================================
    ' FORM CLOSING
    '========================================================
    Private Sub frmtellerdashboard_FormClosing(
        sender As Object,
        e As FormClosingEventArgs
    ) Handles MyBase.FormClosing

        If e.CloseReason =
           CloseReason.UserClosing Then

            Form1.Show()

        End If

    End Sub


    '========================================================
    ' SLOT CLICK PROXY
    '========================================================
    Private Sub pnlSlot_ClickProxy(
        pnl As Panel
    )

        Dim method As System.Reflection.MethodInfo =
            GetType(Control).GetMethod(
                "OnClick",
                System.Reflection.BindingFlags.NonPublic Or
                System.Reflection.BindingFlags.Instance
            )


        If method IsNot Nothing Then

            method.Invoke(
                pnl,
                New Object() {
                    EventArgs.Empty
                }
            )

        End If

    End Sub


    '========================================================
    ' SALES LABEL
    '========================================================
    Private Sub lblTotalSales_Click(
        sender As Object,
        e As EventArgs
    ) Handles lblTotalSales.Click

    End Sub


    '========================================================
    ' CAR STATS PAINT
    '========================================================
    Private Sub pnlStatsCars_Paint(
        sender As Object,
        e As PaintEventArgs
    ) Handles pnlStatsCars.Paint

    End Sub


    '========================================================
    ' ADD TELLER
    '========================================================
    Private Sub Button1_Click(
        sender As Object,
        e As EventArgs
    ) Handles Button1.Click

        Dim addTellerForm As New frmaddteller()


        If OverlayHelper.ShowDialog(
            Me,
            addTellerForm
        ) = DialogResult.OK Then

            LoadUsersFromMySql()

            dgvAccounts.DataSource = Nothing

            dgvAccounts.DataSource =
                ParkingData.UsersTable

        End If

    End Sub


    '========================================================
    ' ACCOUNT TAB
    '========================================================
    Private Sub btnTabAccounts_Click(
        sender As Object,
        e As EventArgs
    )

    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub
End Class