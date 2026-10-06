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
        Try
            ParkingData.CarBaseRate = Convert.ToDouble(txtCarBase.Text)
            ParkingData.MotorBaseRate = Convert.ToDouble(txtMotorBase.Text)
            ParkingData.CarSucceedingRate = Convert.ToDouble(txtCarSucceeding.Text)
            ParkingData.MotorSucceedingRate = Convert.ToDouble(txtMotorSucceeding.Text)
            ParkingData.FreeHours = Convert.ToInt32(txtFreeHours.Text)
            DataStore.SaveRates()
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Please enter valid numbers for all fields.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
