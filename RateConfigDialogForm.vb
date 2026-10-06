Imports System.Windows.Forms
Imports System.Drawing

Public Class RateConfigDialogForm
    Private Sub RateConfigDialogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.MakeRoundedControl(Me, 10)
        ThemeManager.MakeRoundedControl(btnCancel, 6)
        ThemeManager.MakeRoundedControl(btnSave, 6)

        txtCarBase.Text = ParkingData.CarBaseRate.ToString("F2")
        txtMotorBase.Text = ParkingData.MotorBaseRate.ToString("F2")
        txtCarSucceeding.Text = ParkingData.CarSucceedingRate.ToString("F2")
        txtMotorSucceeding.Text = ParkingData.MotorSucceedingRate.ToString("F2")
        txtFreeHours.Text = ParkingData.FreeHours.ToString()
    End Sub

    Private Sub RateConfigDialogForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawRectangle(Pens.Gray, 0, 0, Me.Width - 1, Me.Height - 1)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim carBase As Decimal
        Dim motorBase As Decimal
        Dim carSucc As Decimal
        Dim motorSucc As Decimal
        Dim freeHrs As Integer

        If Not Decimal.TryParse(txtCarBase.Text.Trim(), carBase) OrElse carBase < 0D Then
            MessageBox.Show("Please enter a valid non-negative Car Base Rate.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCarBase.Focus()
            Return
        End If

        If Not Decimal.TryParse(txtMotorBase.Text.Trim(), motorBase) OrElse motorBase < 0D Then
            MessageBox.Show("Please enter a valid non-negative Motor Base Rate.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtMotorBase.Focus()
            Return
        End If

        If Not Decimal.TryParse(txtCarSucceeding.Text.Trim(), carSucc) OrElse carSucc < 0D Then
            MessageBox.Show("Please enter a valid non-negative Car Succeeding Rate.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCarSucceeding.Focus()
            Return
        End If

        If Not Decimal.TryParse(txtMotorSucceeding.Text.Trim(), motorSucc) OrElse motorSucc < 0D Then
            MessageBox.Show("Please enter a valid non-negative Motor Succeeding Rate.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtMotorSucceeding.Focus()
            Return
        End If

        If Not Integer.TryParse(txtFreeHours.Text.Trim(), freeHrs) OrElse freeHrs < 0 Then
            MessageBox.Show("Please enter a valid non-negative integer for Base/Free Hours.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFreeHours.Focus()
            Return
        End If

        Dim saved As Boolean = DatabaseModule.SaveRatesToDatabase(carBase, motorBase, carSucc, motorSucc, freeHrs, ParkingData.OverstayPenaltyFee)
        If saved Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("Could not save rates to the database. Please check your database connection.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class
