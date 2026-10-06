Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class RegisterForm
    Inherits Form

    Private ReadOnly connStr As String =
        "Server=127.0.0.1;Database=parkingdb;Uid=root;Pwd=;"

    Public Sub New()

        InitializeComponent()

        Me.lblTitle.Text = "Create Customer Account"

    End Sub


    '========================================================
    ' FORM LOAD
    '========================================================
    Private Sub RegisterForm_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load

        ThemeManager.MakeRoundedControl(
            Me.btnRegister,
            6
        )

        ThemeManager.MakeRoundedControl(
            Me,
            8
        )

    End Sub


    '========================================================
    ' FORM PAINT
    '========================================================
    Private Sub RegisterForm_Paint(
        sender As Object,
        e As PaintEventArgs
    ) Handles Me.Paint

        e.Graphics.DrawRectangle(
            Pens.Gray,
            0,
            0,
            Me.Width - 1,
            Me.Height - 1
        )

    End Sub


    '========================================================
    ' REGISTER
    '========================================================
    Private Sub btnRegister_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnRegister.Click

        '----------------------------------------------------
        ' GET USERNAME
        '----------------------------------------------------
        Dim username As String =
            txtUser.Text.Trim()


        '----------------------------------------------------
        ' GET PASSWORD
        '----------------------------------------------------
        Dim password As String =
            txtPass.Text


        '----------------------------------------------------
        ' GET CONFIRM PASSWORD
        '----------------------------------------------------
        Dim confirmPassword As String =
            txtConfirm.Text


        '----------------------------------------------------
        ' BASIC VALIDATION
        '----------------------------------------------------
        If username = "" Then

            MessageBox.Show(
                "Please enter a username.",
                "Registration",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtUser.Focus()

            Return

        End If


        If password = "" Then

            MessageBox.Show(
                "Please enter a password.",
                "Registration",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtPass.Focus()

            Return

        End If


        If confirmPassword = "" Then

            MessageBox.Show(
                "Please confirm your password.",
                "Registration",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtConfirm.Focus()

            Return

        End If


        '----------------------------------------------------
        ' PASSWORD MATCH
        '----------------------------------------------------
        If password <> confirmPassword Then

            MessageBox.Show(
                "Passwords do not match.",
                "Registration",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtConfirm.Clear()
            txtConfirm.Focus()

            Return

        End If


        '----------------------------------------------------
        ' ADMIN USERNAME RESERVED
        '----------------------------------------------------
        If username.Equals(
            "admin",
            StringComparison.OrdinalIgnoreCase
        ) Then

            MessageBox.Show(
                "The username 'admin' is reserved and cannot be registered.",
                "Registration",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtUser.Focus()

            Return

        End If


        Try

            '================================================
            ' CONNECT TO MYSQL
            '================================================
            Using conn As New MySqlConnection(connStr)

                conn.Open()


                '================================================
                ' CHECK IF USERNAME ALREADY EXISTS
                ' IN CUSTOMER OR TELLER
                '================================================
                Dim checkQuery As String =
                    "SELECT COUNT(*) " &
                    "FROM (" &
                    "SELECT Username FROM tblcustomer " &
                    "UNION ALL " &
                    "SELECT Username FROM tblteller" &
                    ") AS users " &
                    "WHERE Username = @Username"


                Dim usernameExists As Boolean = False


                Using checkCmd As New MySqlCommand(
                    checkQuery,
                    conn
                )

                    checkCmd.Parameters.AddWithValue(
                        "@Username",
                        username
                    )


                    Dim result As Object =
                        checkCmd.ExecuteScalar()


                    If result IsNot Nothing AndAlso
                       result IsNot DBNull.Value Then

                        usernameExists =
                            Convert.ToInt32(result) > 0

                    End If

                End Using


                '================================================
                ' DUPLICATE USERNAME
                '================================================
                If usernameExists Then

                    MessageBox.Show(
                        "Username already exists. Please choose another username.",
                        "Registration Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )

                    txtUser.Focus()

                    Return

                End If


                '================================================
                ' FULL NAME
                '================================================
                Dim fullNameValue As String = ""


                ' Try to find txtFullName without assuming
                ' it exists in every version of the form.
                Dim fullNameControl As Control =
                    Me.Controls.Find(
                        "txtFullName",
                        True
                    ).FirstOrDefault()


                If fullNameControl IsNot Nothing Then

                    fullNameValue =
                        fullNameControl.Text.Trim()

                End If


                ' If there is no fullname textbox or it is
                ' empty, use username as fallback.
                If fullNameValue = "" Then

                    fullNameValue =
                        username

                End If


                '================================================
                ' INSERT CUSTOMER ACCOUNT
                '================================================
                Dim insertQuery As String =
                    "INSERT INTO tblcustomer " &
                    "(Fullname, Username, Password, Attempts, Status) " &
                    "VALUES " &
                    "(@Fullname, @Username, @Password, @Attempts, @Status)"


                Using insertCmd As New MySqlCommand(
                    insertQuery,
                    conn
                )

                    insertCmd.Parameters.AddWithValue(
                        "@Fullname",
                        fullNameValue
                    )


                    insertCmd.Parameters.AddWithValue(
                        "@Username",
                        username
                    )


                    insertCmd.Parameters.AddWithValue(
                        "@Password",
                        password
                    )


                    insertCmd.Parameters.AddWithValue(
                        "@Attempts",
                        0
                    )


                    insertCmd.Parameters.AddWithValue(
                        "@Status",
                        1
                    )


                    insertCmd.ExecuteNonQuery()

                End Using


                '================================================
                ' SUCCESS
                '================================================
                MessageBox.Show(
                    "Customer account created successfully.",
                    "Registration Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )


                '================================================
                ' CLEAR FIELDS
                '================================================
                txtUser.Clear()
                txtPass.Clear()
                txtConfirm.Clear()


                If fullNameControl IsNot Nothing Then
                    fullNameControl.Text = ""
                End If


                '================================================
                ' CLOSE REGISTRATION FORM
                '================================================
                Me.DialogResult =
                    DialogResult.OK

                Me.Close()

            End Using


        Catch ex As MySqlException

            MessageBox.Show(
                "A database error occurred." &
                Environment.NewLine &
                Environment.NewLine &
                ex.Message,
                "MySQL Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )


        Catch ex As Exception

            MessageBox.Show(
                "The account could not be created." &
                Environment.NewLine &
                Environment.NewLine &
                ex.Message,
                "Registration Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' CLOSE BUTTON
    '========================================================
    Private Sub btnClose_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnCancel.Click

        Me.DialogResult =
            DialogResult.Cancel

        Me.Close()

    End Sub


End Class