Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports MySql.Data.MySqlClient

Public Class Form1

    Public Shared CurrentUserRole As String = ""
    Public Shared CurrentUsername As String = ""
    Public Shared CurrentFullName As String = ""

    Private connStr As String = "Server=127.0.0.1;Database=parkingdb;Uid=root;Pwd=;"

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(
        hWnd As IntPtr,
        msg As Integer,
        wParam As Integer,
        <MarshalAs(UnmanagedType.LPWStr)> lParam As String
    ) As Int32
    End Function

    Private Const EM_SETCUEBANNER As Integer = &H1501

    '=========================================================
    ' LOGIN
    '=========================================================
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text

        If username = "" Then

            MessageBox.Show(
                "Please enter your username.",
                "Login Failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtUsername.Focus()
            Return

        End If

        If password = "" Then

            MessageBox.Show(
                "Please enter your password.",
                "Login Failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtPassword.Focus()
            Return

        End If

        '=====================================================
        ' ADMIN LOGIN
        '=====================================================
        If username = "admin" AndAlso password = "1234" Then

            CurrentUserRole = "Admin"
            CurrentUsername = "admin"
            CurrentFullName = "Administrator"

            OpenDashboard()

            Return

        End If

        Try

            Dim loginSuccessful As Boolean = False
            Dim role As String = ""
            Dim fullName As String = ""

            Using conn As New MySqlConnection(connStr)

                conn.Open()

                '=================================================
                ' CHECK CUSTOMER
                '=================================================
                Dim customerQuery As String =
                    "SELECT Fullname, Username, Password, Status " &
                    "FROM tblcustomer " &
                    "WHERE Username = @Username " &
                    "LIMIT 1"

                Using cmd As New MySqlCommand(customerQuery, conn)

                    cmd.Parameters.AddWithValue("@Username", username)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        If reader.Read() Then

                            Dim dbPassword As String =
                                reader("Password").ToString()

                            Dim status As String =
                                reader("Status").ToString()

                            ' Status 1 = Active
                            If status <> "1" Then

                                MessageBox.Show(
                                    "This account is not active.",
                                    "Login Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                )

                                Return

                            End If

                            If dbPassword = password Then

                                loginSuccessful = True
                                role = "Customer"
                                fullName = reader("Fullname").ToString()

                            Else

                                MessageBox.Show(
                                    "Invalid password.",
                                    "Login Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                )

                                Return

                            End If

                        End If

                    End Using

                End Using

                '=================================================
                ' CHECK TELLER
                '=================================================
                If Not loginSuccessful Then

                    Dim tellerQuery As String =
                        "SELECT FullName, Username, Password, Status " &
                        "FROM tblteller " &
                        "WHERE Username = @Username " &
                        "LIMIT 1"

                    Using cmd As New MySqlCommand(tellerQuery, conn)

                        cmd.Parameters.AddWithValue("@Username", username)

                        Using reader As MySqlDataReader = cmd.ExecuteReader()

                            If reader.Read() Then

                                Dim dbPassword As String =
                                    reader("Password").ToString()

                                Dim status As String =
                                    reader("Status").ToString()

                                ' Status 1 = Active
                                If status <> "1" Then

                                    MessageBox.Show(
                                        "This account is not active.",
                                        "Login Failed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    )

                                    Return

                                End If

                                If dbPassword = password Then

                                    loginSuccessful = True
                                    role = "Teller"
                                    fullName = reader("FullName").ToString()

                                Else

                                    MessageBox.Show(
                                        "Invalid password.",
                                        "Login Failed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    )

                                    Return

                                End If

                            End If

                        End Using

                    End Using

                End If

            End Using

            '=====================================================
            ' USERNAME NOT FOUND
            '=====================================================
            If Not loginSuccessful Then

                MessageBox.Show(
                    "Username does not exist.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                Return

            End If

            '=====================================================
            ' SAVE USER INFORMATION
            '=====================================================
            CurrentUserRole = role
            CurrentUsername = username
            CurrentFullName = fullName

            '=====================================================
            ' OPEN DASHBOARD
            '=====================================================
            OpenDashboard()

        Catch ex As MySqlException

            MessageBox.Show(
                "Database connection error." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        Catch ex As Exception

            MessageBox.Show(
                "An unexpected error occurred." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    '=========================================================
    ' OPEN DASHBOARD
    '=========================================================
    Private Sub OpenDashboard()

        Me.Hide()

        txtUsername.Clear()
        txtPassword.Clear()

        Using successForm As New LoginSuccessForm()

            successForm.StartPosition =
                FormStartPosition.CenterScreen

            successForm.ShowDialog()

        End Using

        If CurrentUserRole = "Customer" Then

            DashBoardForm.ShowLobbyMode = True
            DashBoardForm.UpdateSlots()
            DashBoardForm.Show()

        ElseIf CurrentUserRole = "Teller" Then

            frmadmindashboard.ShowLobbyMode = False
            frmadmindashboard.UpdateSlots()
            frmadmindashboard.Show()

        ElseIf CurrentUserRole = "Admin" Then

            DashBoardForm.ShowLobbyMode = False
            DashBoardForm.UpdateSlots()
            DashBoardForm.Show()

        End If

    End Sub

    '=========================================================
    ' FORM LOAD
    '=========================================================
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ThemeManager.EnableDrag(Panel1, Me)
        ThemeManager.MakeRoundedControl(Me, 16)
        ThemeManager.MakeRoundedControl(btnLogin, 8)
        ThemeManager.MakeRoundedControl(btnClose, 6)

        Dim lblHeading As New Label()

        lblHeading.Text =
            "Welcome Back!" & vbCrLf &
            "Sign In to Continue"

        lblHeading.Font =
            New Font(
                "Segoe UI",
                22,
                FontStyle.Bold
            )

        lblHeading.ForeColor =
            Color.FromArgb(30, 46, 90)

        lblHeading.Location =
            New Point(50, 90)

        lblHeading.Size =
            New Size(370, 140)

        Me.Controls.Add(lblHeading)
        lblHeading.BringToFront()

        AddHandler lblHeading.Paint,
            Sub(s, pe)

                Dim lbl = DirectCast(s, Label)

                pe.Graphics.TextRenderingHint =
                    System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

                Dim line1 = "Welcome!"
                Dim line2 = "Sign In to Continue"
                Dim fnt = lbl.Font

                Dim sz1 =
                    TextRenderer.MeasureText(
                        pe.Graphics,
                        line1,
                        fnt,
                        New Size(
                            lbl.Width,
                            Integer.MaxValue
                        ),
                        TextFormatFlags.WordBreak
                    )

                TextRenderer.DrawText(
                    pe.Graphics,
                    line1,
                    fnt,
                    New Point(0, 0),
                    Color.FromArgb(30, 46, 90)
                )

                TextRenderer.DrawText(
                    pe.Graphics,
                    line2,
                    fnt,
                    New Point(
                        0,
                        sz1.Height - 4
                    ),
                    Color.FromArgb(0, 168, 181)
                )

            End Sub

        lblHeading.ForeColor = Color.Transparent

        AddHandler pnlUser.Paint,
            Sub(s, pe)

                pe.Graphics.DrawLine(
                    New Pen(
                        Color.FromArgb(220, 220, 220),
                        1
                    ),
                    0,
                    44,
                    330,
                    44
                )

            End Sub

        AddHandler pnlPass.Paint,
            Sub(s, pe)

                pe.Graphics.DrawLine(
                    New Pen(
                        Color.FromArgb(220, 220, 220),
                        1
                    ),
                    0,
                    44,
                    330,
                    44
                )

            End Sub

        SendMessage(
            txtUsername.Handle,
            EM_SETCUEBANNER,
            1,
            "Enter your username"
        )

        SendMessage(
            txtPassword.Handle,
            EM_SETCUEBANNER,
            1,
            "Enter your password"
        )

        AddHandler lblCreateAccount.Click,
            Sub(s, ev)

                Dim regForm As New RegisterForm()
                regForm.ShowDialog(Me)

            End Sub

        AddHandler btnClose.Click,
            Sub()

                Me.Close()

            End Sub

        AddHandler Me.Paint,
            Sub(s, pe)

                pe.Graphics.SmoothingMode =
                    Drawing2D.SmoothingMode.AntiAlias

                Using p As New Pen(
                    Color.FromArgb(230, 230, 230),
                    1
                )

                    pe.Graphics.DrawRectangle(
                        p,
                        0,
                        0,
                        Me.Width - 1,
                        Me.Height - 1
                    )

                End Using

            End Sub

    End Sub

    '=========================================================
    ' SHOW PASSWORD
    '=========================================================
    Private Sub chkShowPassword_CheckedChanged(
        sender As Object,
        e As EventArgs
    ) Handles chkShowPassword.CheckedChanged

        If chkShowPassword.Checked Then

            txtPassword.PasswordChar =
                ControlChars.NullChar

        Else

            txtPassword.PasswordChar =
                Global.Microsoft.VisualBasic.ChrW(8226)

        End If

    End Sub

    '=========================================================
    ' FORM CLOSING
    '=========================================================
    Private Sub Form1_FormClosing(
        sender As Object,
        e As FormClosingEventArgs
    ) Handles Me.FormClosing

        If e.CloseReason =
            CloseReason.UserClosing Then

            Application.Exit()

        End If

    End Sub

    '=========================================================
    ' EMPTY EVENTS
    '=========================================================
    Private Sub Label3_Click(
        sender As Object,
        e As EventArgs
    ) Handles Label3.Click

    End Sub

    Private Sub Panel1_Paint(
        sender As Object,
        e As PaintEventArgs
    ) Handles Panel1.Paint

    End Sub

    Private Sub txtUsername_TextChanged(
        sender As Object,
        e As EventArgs
    ) Handles txtUsername.TextChanged

    End Sub

    Private Sub PictureBox1_Click(
        sender As Object,
        e As EventArgs
    ) Handles PictureBox1.Click

    End Sub

End Class


'=============================================================
' LOGIN SUCCESS FORM
'=============================================================
Public Class LoginSuccessForm

    Inherits Form

    Private pnlHeader As Panel
    Private lblTitle As Label
    Private pnlIcon As Panel
    Private lblHeading As Label
    Private lblSubtext As Label
    Private WithEvents btnContinue As Button

    Public Sub New()

        InitializeComponent()

    End Sub

    Private Sub InitializeComponent()

        Me.pnlHeader = New Panel()
        Me.lblTitle = New Label()
        Me.pnlIcon = New Panel()
        Me.lblHeading = New Label()
        Me.lblSubtext = New Label()
        Me.btnContinue = New Button()

        Me.Size = New Size(350, 280)
        Me.BackColor = Color.White
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterParent

        ' HEADER
        Me.pnlHeader.BackColor =
            Color.FromArgb(30, 46, 90)

        Me.pnlHeader.Size =
            New Size(350, 50)

        Me.pnlHeader.Dock =
            DockStyle.Top

        Me.pnlHeader.Controls.Add(
            Me.lblTitle
        )

        ' TITLE
        Me.lblTitle.Text =
            "Login Success"

        Me.lblTitle.Font =
            New Font(
                "Segoe UI",
                11.0!,
                FontStyle.Bold
            )

        Me.lblTitle.ForeColor =
            Color.White

        Me.lblTitle.Location =
            New Point(15, 15)

        Me.lblTitle.AutoSize =
            True

        ' ICON
        Me.pnlIcon.Size =
            New Size(80, 80)

        Me.pnlIcon.Location =
            New Point(135, 60)

        AddHandler Me.pnlIcon.Paint,
            AddressOf DrawCheckmark

        ' HEADING
        Me.lblHeading.Text =
            "Login Successful!"

        Me.lblHeading.Font =
            New Font(
                "Segoe UI",
                12.0!,
                FontStyle.Bold
            )

        Me.lblHeading.ForeColor =
            Color.FromArgb(30, 46, 90)

        Me.lblHeading.Size =
            New Size(350, 25)

        Me.lblHeading.Location =
            New Point(0, 150)

        Me.lblHeading.TextAlign =
            ContentAlignment.MiddleCenter

        ' SUBTEXT
        Me.lblSubtext.Text =
            "You have been logged in to the Parking System."

        Me.lblSubtext.Font =
            New Font(
                "Segoe UI",
                9.5!
            )

        Me.lblSubtext.ForeColor =
            Color.FromArgb(80, 90, 100)

        Me.lblSubtext.Size =
            New Size(350, 25)

        Me.lblSubtext.Location =
            New Point(0, 175)

        Me.lblSubtext.TextAlign =
            ContentAlignment.MiddleCenter

        ' BUTTON
        Me.btnContinue.Text =
            "CONTINUE"

        Me.btnContinue.Font =
            New Font(
                "Segoe UI",
                10.5!,
                FontStyle.Bold
            )

        Me.btnContinue.BackColor =
            Color.FromArgb(0, 168, 181)

        Me.btnContinue.ForeColor =
            Color.White

        Me.btnContinue.FlatStyle =
            FlatStyle.Flat

        Me.btnContinue.FlatAppearance.BorderSize =
            0

        Me.btnContinue.Size =
            New Size(290, 45)

        Me.btnContinue.Location =
            New Point(30, 215)

        ' ADD CONTROLS
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlIcon)
        Me.Controls.Add(Me.lblHeading)
        Me.Controls.Add(Me.lblSubtext)
        Me.Controls.Add(Me.btnContinue)

    End Sub

    Private Sub LoginSuccessForm_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load

        ThemeManager.MakeRoundedControl(
            Me.btnContinue,
            6
        )

        AddHandler Me.Paint,
            Sub(s, pe)

                pe.Graphics.DrawRectangle(
                    Pens.LightGray,
                    0,
                    0,
                    Me.Width - 1,
                    Me.Height - 1
                )

            End Sub

    End Sub

    Private Sub DrawCheckmark(
        sender As Object,
        e As PaintEventArgs
    )

        e.Graphics.SmoothingMode =
            SmoothingMode.AntiAlias

        Dim rect As New Rectangle(
            10,
            10,
            60,
            60
        )

        Using brush As New SolidBrush(
            Color.FromArgb(40, 167, 69)
        )

            e.Graphics.FillEllipse(
                brush,
                rect
            )

        End Using

        Using pen As New Pen(
            Color.White,
            3.5F
        )

            pen.StartCap =
                LineCap.Round

            pen.EndCap =
                LineCap.Round

            pen.LineJoin =
                LineJoin.Round

            Dim path As New GraphicsPath()

            path.AddLine(
                28,
                40,
                36,
                49
            )

            path.AddLine(
                36,
                49,
                53,
                29
            )

            e.Graphics.DrawPath(
                pen,
                path
            )

        End Using

    End Sub

    Private Sub btnContinue_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnContinue.Click

        Me.DialogResult =
            DialogResult.OK

        Me.Close()

    End Sub

End Class