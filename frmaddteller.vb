Imports System.Windows.Forms
Imports System.Drawing
Imports MySql.Data.MySqlClient

Public Class frmaddteller

    Private connStr As String = DatabaseModule.connStr

    Private Sub frmaddteller_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            ThemeManager.MakeRoundedControl(Me.btnRegister, 6)
            ThemeManager.MakeRoundedControl(Me, 8)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub frmaddteller_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

        e.Graphics.DrawRectangle(
            Pens.Gray,
            0,
            0,
            Me.Width - 1,
            Me.Height - 1
        )

    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click

        '=========================================================
        ' VALIDATION
        '=========================================================

        Dim username As String = txtUser.Text.Trim()
        Dim password As String = txtPass.Text
        Dim confirmPassword As String = txtConfirm.Text

        If username = "" OrElse password = "" OrElse confirmPassword = "" Then

            MessageBox.Show(
                "Please fill out all fields.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Return

        End If

        If password <> confirmPassword Then

            MessageBox.Show(
                "Passwords do not match.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Return

        End If

        If username.ToLower() = "admin" Then

            MessageBox.Show(
                "The username 'admin' is reserved.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Return

        End If

        '=========================================================
        ' GET FULL NAME
        '=========================================================

        Dim fullNameValue As String = username

        If Controls.ContainsKey("txtFullName") Then

            Dim fullNameTextBox As Control = Controls("txtFullName")

            If fullNameTextBox IsNot Nothing Then

                If TypeOf fullNameTextBox Is TextBox Then

                    Dim enteredFullName As String =
                        DirectCast(fullNameTextBox, TextBox).Text.Trim()

                    If enteredFullName <> "" Then
                        fullNameValue = enteredFullName
                    End If

                End If

            End If

        End If

        '=========================================================
        ' DATABASE
        '=========================================================

        Try

            Using conn As New MySqlConnection(connStr)

                conn.Open()

                '-------------------------------------------------
                ' CHECK IF USERNAME ALREADY EXISTS
                ' IN TELLER, CUSTOMER, OR ADMIN
                '-------------------------------------------------

                Dim checkQuery As String =
                    "SELECT COUNT(*) FROM tblteller WHERE Username = @Username " &
                    "UNION ALL " &
                    "SELECT COUNT(*) FROM tblcustomer WHERE Username = @Username " &
                    "UNION ALL " &
                    "SELECT COUNT(*) FROM tbladmin WHERE Username = @Username"

                Using checkCmd As New MySqlCommand(checkQuery, conn)

                    checkCmd.Parameters.AddWithValue(
                        "@Username",
                        username
                    )

                    Using reader As MySqlDataReader =
                        checkCmd.ExecuteReader()

                        Dim usernameExists As Boolean = False

                        While reader.Read()

                            If Convert.ToInt32(reader(0)) > 0 Then
                                usernameExists = True
                                Exit While
                            End If

                        End While

                        reader.Close()

                        If usernameExists Then

                            MessageBox.Show(
                                "Username already exists.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            )

                            Return

                        End If

                    End Using

                End Using

                '-------------------------------------------------
                ' INSERT TELLER
                '-------------------------------------------------

                Dim insertQuery As String =
                    "INSERT INTO tblteller " &
                    "(FullName, Username, Password, Attempts, Status) " &
                    "VALUES " &
                    "(@FullName, @Username, @Password, 0, 1)"

                Using cmd As New MySqlCommand(
                    insertQuery,
                    conn
                )

                    cmd.Parameters.AddWithValue(
                        "@FullName",
                        fullNameValue
                    )

                    cmd.Parameters.AddWithValue(
                        "@Username",
                        username
                    )

                    cmd.Parameters.AddWithValue(
                        "@Password",
                        password
                    )

                    cmd.ExecuteNonQuery()

                End Using

            End Using

            '=========================================================
            ' SUCCESS
            '=========================================================

            MessageBox.Show(
                "Teller account created successfully!" &
                vbCrLf & vbCrLf &
                "Username: " & username &
                vbCrLf &
                "Status: Active",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As MySqlException

            MessageBox.Show(
                "Database error:" &
                vbCrLf & vbCrLf &
                ex.Message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        Catch ex As Exception

            MessageBox.Show(
                "An error occurred:" &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Private Sub btnCancel_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnCancel.Click

        Me.Close()

    End Sub

    Private Sub chkRegShow_CheckedChanged(
        sender As Object,
        e As EventArgs
    ) Handles chkRegShow.CheckedChanged

        If chkRegShow.Checked Then

            Me.txtPass.PasswordChar =
                ControlChars.NullChar

            Me.txtConfirm.PasswordChar =
                ControlChars.NullChar

        Else

            Me.txtPass.PasswordChar = "*"c
            Me.txtConfirm.PasswordChar = "*"c

        End If

    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub
End Class