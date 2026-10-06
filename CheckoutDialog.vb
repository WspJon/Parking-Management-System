Public Class CheckoutDialog
    Private _plateNumber As String
    Private _timeIn As String
    Private _timeOut As String
    Private _duration As String
    Private _baseRate As String
    Private _discount As String
    Private _totalDue As String
    Private _amountPaid As String
    Private _change As String
    Private _penalty As String

    Public Sub New(plate As String, timeIn As String, timeOut As String, totalHours As String, baseRate As String, discount As String, penalty As String, amountDue As String, cashTendered As String, change As String)
        InitializeComponent()

        _plateNumber = plate
        _timeIn = timeIn
        _timeOut = timeOut
        _duration = totalHours
        _baseRate = baseRate
        _discount = discount
        _penalty = penalty
        _totalDue = amountDue
        _amountPaid = cashTendered
        _change = change

        lblPlateNumberValue.Text = plate
        lblTimeInValue.Text = timeIn
        lblTimeOutValue.Text = timeOut
        lblTotalHoursValue.Text = totalHours
        lblBaseRateValue.Text = baseRate
        lblDiscountValue.Text = discount
        Dim parsedPenalty As Decimal = 0D
        Decimal.TryParse(_penalty, parsedPenalty)
        lblPenaltyValue.Text = If(parsedPenalty > 0D, "₱" & penalty, "-")
        lblAmountDueValue.Text = "₱" & amountDue
        lblAmountPaidValue.Text = "₱" & cashTendered
        lblChangeValue.Text = "₱" & change

        btnOk.Size = New Size(150, 40)
        btnOk.Location = New Point(20, 430)

        Dim btnSaveReceipt As New Button()
        btnSaveReceipt.Text = "SAVE RECEIPT"
        btnSaveReceipt.Size = New Size(150, 40)
        btnSaveReceipt.Location = New Point(190, 430)
        btnSaveReceipt.BackColor = Color.FromArgb(46, 186, 104)
        btnSaveReceipt.ForeColor = Color.White
        btnSaveReceipt.FlatStyle = FlatStyle.Flat
        btnSaveReceipt.FlatAppearance.BorderSize = 0
        btnSaveReceipt.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnSaveReceipt.Cursor = Cursors.Hand
        Me.Controls.Add(btnSaveReceipt)

        AddHandler btnSaveReceipt.Click, Sub()
            Using sfd As New SaveFileDialog()
                sfd.Filter = "Text File (*.txt)|*.txt"
                sfd.FileName = "Receipt_" & _plateNumber & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".txt"
                If sfd.ShowDialog() = DialogResult.OK Then
                    Try
                        Dim sb As New System.Text.StringBuilder()
                        sb.AppendLine("========================================")
                        sb.AppendLine("        PARKING SYSTEM RECEIPT")
                        sb.AppendLine("========================================")
                        sb.AppendLine($"Date:          {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}")
                        sb.AppendLine($"Plate Number:  {_plateNumber}")
                        sb.AppendLine("----------------------------------------")
                        sb.AppendLine($"Time In:       {_timeIn}")
                        sb.AppendLine($"Time Out:      {_timeOut}")
                        sb.AppendLine($"Duration:      {_duration}")
                        sb.AppendLine($"Base Rate:     {_baseRate}")
                        sb.AppendLine($"Discount:      {_discount}")
                        If parsedPenalty > 0D Then
                            sb.AppendLine($"Penalty:       ₱{_penalty}")
                        End If
                        sb.AppendLine("----------------------------------------")
                        sb.AppendLine($"Total Due:     ₱{_totalDue}")
                        sb.AppendLine($"Amount Paid:   ₱{_amountPaid}")
                        sb.AppendLine($"Change:        ₱{_change}")
                        sb.AppendLine("========================================")
                        sb.AppendLine("       Thank you for parking!")
                        sb.AppendLine("========================================")
                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString())
                        MessageBox.Show("Receipt saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show("Failed to save receipt file: " & ex.Message, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End Sub

        AddHandler Me.Load, Sub(s, e)
                                ThemeManager.MakeRoundedControl(Me, 10)
                                ThemeManager.MakeRoundedControl(btnOk, 6)
                                ThemeManager.MakeRoundedControl(btnSaveReceipt, 6)
                            End Sub
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.DialogResult = DialogResult.OK
    End Sub
End Class
