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

    Private _subtotal As Decimal = 0D
    Private _discountAmount As Decimal = 0D

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

        Dim parsedPenalty As Decimal = 0D
        Decimal.TryParse(_penalty, parsedPenalty)

        Dim parsedTotalDue As Decimal = 0D
        Decimal.TryParse(_totalDue, parsedTotalDue)

        ' --- LOGIC PARA SA DISCOUNT BREAKDOWN ---
        If _discount = "Senior Citizen" Then
            If parsedTotalDue > 0D Then
                _subtotal = Math.Round(parsedTotalDue / 0.8D, 2)
                _discountAmount = _subtotal - parsedTotalDue
            Else
                _subtotal = parsedPenalty
                _discountAmount = 0D
            End If
        ElseIf _discount = "PWD" Then
            _subtotal = parsedTotalDue + parsedPenalty
            _discountAmount = _subtotal
        Else
            _subtotal = parsedTotalDue
            _discountAmount = 0D
        End If

        ' --- PAG-ASSIGN NG MGA VALUES SA UI LABELS ---
        lblPlateNumberValue.Text = plate
        lblTimeInValue.Text = timeIn
        lblTimeOutValue.Text = timeOut
        lblTotalHoursValue.Text = totalHours
        lblBaseRateValue.Text = baseRate

        lblDiscountValue.Text = If(_discountAmount > 0D, "-₱" & _discountAmount.ToString("F2"), "₱0.00")
        lblDicountTypeValue.Text = If(String.IsNullOrEmpty(discount) OrElse discount = "None", "None", discount)

        lblPenaltyValue.Text = If(parsedPenalty > 0D, "₱" & penalty, "-")
        lblAmountDueValue.Text = "₱" & amountDue
        lblAmountPaidValue.Text = "₱" & cashTendered
        lblChangeValue.Text = "₱" & change

        ' Ikabit ang Save functionality sa existing na btnSaveReceipt mula sa Designer
        AddHandler btnSaveReceipt.Click, AddressOf SaveReceiptButton_Click

        AddHandler Me.Load, Sub(s, e)
                                ThemeManager.MakeRoundedControl(Me, 10)
                                ThemeManager.MakeRoundedControl(btnOk, 6)
                                ThemeManager.MakeRoundedControl(btnSaveReceipt, 6)
                            End Sub
    End Sub

    ' Event handler para sa pag-save ng resibo
    Private Sub SaveReceiptButton_Click(sender As Object, e As EventArgs)
        Using sfd As New SaveFileDialog()
            sfd.Filter = "Text File (*.txt)|*.txt"
            sfd.FileName = "Receipt_" & _plateNumber & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".txt"
            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim parsedPenalty As Decimal = 0D
                    Decimal.TryParse(_penalty, parsedPenalty)

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

                    If parsedPenalty > 0D Then
                        sb.AppendLine($"Penalty:       ₱{_penalty}")
                    End If

                    sb.AppendLine("----------------------------------------")
                    sb.AppendLine($"Subtotal:      ₱{_subtotal.ToString("F2")}")
                    sb.AppendLine($"Discount Type: {_discount}")
                    sb.AppendLine($"Discount Amt: -₱{_discountAmount.ToString("F2")}")
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

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub lblIcon_Click(sender As Object, e As EventArgs) Handles lblIcon.Click
    End Sub

    Private Sub lblDicountType_Click(sender As Object, e As EventArgs) Handles lblDicountTypeValue.Click
    End Sub

    Private Sub lblTotalHoursValue_Click(sender As Object, e As EventArgs) Handles lblTotalHoursValue.Click
    End Sub
End Class