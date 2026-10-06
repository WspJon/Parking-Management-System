<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CheckoutDialog
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.pnlIcon = New ParkingSystemProject.RoundedPanel()
        Me.lblIcon = New System.Windows.Forms.Label()
        Me.lblCheckoutConfirmed = New System.Windows.Forms.Label()
        Me.pnlDetails = New ParkingSystemProject.RoundedPanel()
        Me.lblPlateNumber = New System.Windows.Forms.Label()
        Me.lblPlateNumberValue = New System.Windows.Forms.Label()
        Me.lblTimeIn = New System.Windows.Forms.Label()
        Me.lblTimeInValue = New System.Windows.Forms.Label()
        Me.lblTimeOut = New System.Windows.Forms.Label()
        Me.lblTimeOutValue = New System.Windows.Forms.Label()
        Me.lblTotalHours = New System.Windows.Forms.Label()
        Me.lblTotalHoursValue = New System.Windows.Forms.Label()
        Me.lblBaseRate = New System.Windows.Forms.Label()
        Me.lblBaseRateValue = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblDiscountValue = New System.Windows.Forms.Label()
        Me.lblPenalty = New System.Windows.Forms.Label()
        Me.lblPenaltyValue = New System.Windows.Forms.Label()
        Me.lblAmountDue = New System.Windows.Forms.Label()
        Me.lblAmountDueValue = New System.Windows.Forms.Label()
        Me.lblAmountPaid = New System.Windows.Forms.Label()
        Me.lblAmountPaidValue = New System.Windows.Forms.Label()
        Me.lblChange = New System.Windows.Forms.Label()
        Me.lblChangeValue = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnSaveReceipt = New System.Windows.Forms.Button()
        Me.HeaderPanel.SuspendLayout()
        Me.pnlIcon.SuspendLayout()
        Me.pnlDetails.SuspendLayout()
        Me.SuspendLayout()
        Me.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.HeaderPanel.Controls.Add(Me.TitleLabel)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(360, 40)
        Me.HeaderPanel.TabIndex = 0
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.TitleLabel.ForeColor = System.Drawing.Color.White
        Me.TitleLabel.Location = New System.Drawing.Point(15, 10)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(125, 20)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "Billing Summary"
        Me.pnlIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.pnlIcon.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlIcon.BorderSize = 1
        Me.pnlIcon.Controls.Add(Me.lblIcon)
        Me.pnlIcon.CornerRadius = 15
        Me.pnlIcon.Location = New System.Drawing.Point(155, 55)
        Me.pnlIcon.Name = "pnlIcon"
        Me.pnlIcon.Size = New System.Drawing.Size(50, 50)
        Me.pnlIcon.TabIndex = 1
        Me.lblIcon.BackColor = System.Drawing.Color.Transparent
        Me.lblIcon.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIcon.Font = New System.Drawing.Font("Segoe UI", 24.0!)
        Me.lblIcon.ForeColor = System.Drawing.Color.White
        Me.lblIcon.Location = New System.Drawing.Point(0, 0)
        Me.lblIcon.Name = "lblIcon"
        Me.lblIcon.Size = New System.Drawing.Size(50, 50)
        Me.lblIcon.TabIndex = 0
        Me.lblIcon.Text = "??"
        Me.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCheckoutConfirmed.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblCheckoutConfirmed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblCheckoutConfirmed.Location = New System.Drawing.Point(20, 115)
        Me.lblCheckoutConfirmed.Name = "lblCheckoutConfirmed"
        Me.lblCheckoutConfirmed.Size = New System.Drawing.Size(320, 25)
        Me.lblCheckoutConfirmed.TabIndex = 2
        Me.lblCheckoutConfirmed.Text = "Checkout Confirmed"
        Me.lblCheckoutConfirmed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.pnlDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.pnlDetails.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlDetails.BorderSize = 1
        Me.pnlDetails.Controls.Add(Me.lblPlateNumber)
        Me.pnlDetails.Controls.Add(Me.lblPlateNumberValue)
        Me.pnlDetails.Controls.Add(Me.lblTimeIn)
        Me.pnlDetails.Controls.Add(Me.lblTimeInValue)
        Me.pnlDetails.Controls.Add(Me.lblTimeOut)
        Me.pnlDetails.Controls.Add(Me.lblTimeOutValue)
        Me.pnlDetails.Controls.Add(Me.lblTotalHours)
        Me.pnlDetails.Controls.Add(Me.lblTotalHoursValue)
        Me.pnlDetails.Controls.Add(Me.lblBaseRate)
        Me.pnlDetails.Controls.Add(Me.lblBaseRateValue)
        Me.pnlDetails.Controls.Add(Me.lblDiscount)
        Me.pnlDetails.Controls.Add(Me.lblDiscountValue)
        Me.pnlDetails.Controls.Add(Me.lblPenalty)
        Me.pnlDetails.Controls.Add(Me.lblPenaltyValue)
        Me.pnlDetails.Controls.Add(Me.lblAmountDue)
        Me.pnlDetails.Controls.Add(Me.lblAmountDueValue)
        Me.pnlDetails.Controls.Add(Me.lblAmountPaid)
        Me.pnlDetails.Controls.Add(Me.lblAmountPaidValue)
        Me.pnlDetails.Controls.Add(Me.lblChange)
        Me.pnlDetails.Controls.Add(Me.lblChangeValue)
        Me.pnlDetails.CornerRadius = 10
        Me.pnlDetails.Location = New System.Drawing.Point(20, 150)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(320, 260)
        Me.pnlDetails.TabIndex = 3
        Me.lblPlateNumber.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPlateNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblPlateNumber.Location = New System.Drawing.Point(10, 10)
        Me.lblPlateNumber.Name = "lblPlateNumber"
        Me.lblPlateNumber.Size = New System.Drawing.Size(150, 20)
        Me.lblPlateNumber.TabIndex = 0
        Me.lblPlateNumber.Text = "Plate Number:"
        Me.lblPlateNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPlateNumberValue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPlateNumberValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblPlateNumberValue.Location = New System.Drawing.Point(160, 10)
        Me.lblPlateNumberValue.Name = "lblPlateNumberValue"
        Me.lblPlateNumberValue.Size = New System.Drawing.Size(150, 20)
        Me.lblPlateNumberValue.TabIndex = 1
        Me.lblPlateNumberValue.Text = "-"
        Me.lblPlateNumberValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTimeIn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTimeIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTimeIn.Location = New System.Drawing.Point(10, 35)
        Me.lblTimeIn.Name = "lblTimeIn"
        Me.lblTimeIn.Size = New System.Drawing.Size(150, 20)
        Me.lblTimeIn.TabIndex = 2
        Me.lblTimeIn.Text = "Time In:"
        Me.lblTimeIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTimeInValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTimeInValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTimeInValue.Location = New System.Drawing.Point(160, 35)
        Me.lblTimeInValue.Name = "lblTimeInValue"
        Me.lblTimeInValue.Size = New System.Drawing.Size(150, 20)
        Me.lblTimeInValue.TabIndex = 3
        Me.lblTimeInValue.Text = "-"
        Me.lblTimeInValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTimeOut.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTimeOut.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTimeOut.Location = New System.Drawing.Point(10, 55)
        Me.lblTimeOut.Name = "lblTimeOut"
        Me.lblTimeOut.Size = New System.Drawing.Size(150, 20)
        Me.lblTimeOut.TabIndex = 4
        Me.lblTimeOut.Text = "Time Out:"
        Me.lblTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTimeOutValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTimeOutValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTimeOutValue.Location = New System.Drawing.Point(160, 55)
        Me.lblTimeOutValue.Name = "lblTimeOutValue"
        Me.lblTimeOutValue.Size = New System.Drawing.Size(150, 20)
        Me.lblTimeOutValue.TabIndex = 5
        Me.lblTimeOutValue.Text = "-"
        Me.lblTimeOutValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalHours.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTotalHours.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTotalHours.Location = New System.Drawing.Point(10, 75)
        Me.lblTotalHours.Name = "lblTotalHours"
        Me.lblTotalHours.Size = New System.Drawing.Size(150, 20)
        Me.lblTotalHours.TabIndex = 6
        Me.lblTotalHours.Text = "Total Hours:"
        Me.lblTotalHours.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalHoursValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTotalHoursValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTotalHoursValue.Location = New System.Drawing.Point(160, 75)
        Me.lblTotalHoursValue.Name = "lblTotalHoursValue"
        Me.lblTotalHoursValue.Size = New System.Drawing.Size(150, 20)
        Me.lblTotalHoursValue.TabIndex = 7
        Me.lblTotalHoursValue.Text = "-"
        Me.lblTotalHoursValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblBaseRate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblBaseRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBaseRate.Location = New System.Drawing.Point(10, 105)
        Me.lblBaseRate.Name = "lblBaseRate"
        Me.lblBaseRate.Size = New System.Drawing.Size(150, 20)
        Me.lblBaseRate.TabIndex = 8
        Me.lblBaseRate.Text = "Base Rate / Extra:"
        Me.lblBaseRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBaseRateValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblBaseRateValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBaseRateValue.Location = New System.Drawing.Point(160, 105)
        Me.lblBaseRateValue.Name = "lblBaseRateValue"
        Me.lblBaseRateValue.Size = New System.Drawing.Size(150, 20)
        Me.lblBaseRateValue.TabIndex = 9
        Me.lblBaseRateValue.Text = "-"
        Me.lblBaseRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblDiscount.Location = New System.Drawing.Point(10, 125)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Size = New System.Drawing.Size(150, 20)
        Me.lblDiscount.TabIndex = 10
        Me.lblDiscount.Text = "Discount:"
        Me.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDiscountValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDiscountValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblDiscountValue.Location = New System.Drawing.Point(160, 125)
        Me.lblDiscountValue.Name = "lblDiscountValue"
        Me.lblDiscountValue.Size = New System.Drawing.Size(150, 20)
        Me.lblDiscountValue.TabIndex = 11
        Me.lblDiscountValue.Text = "-"
        Me.lblDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPenalty.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPenalty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblPenalty.Location = New System.Drawing.Point(10, 145)
        Me.lblPenalty.Name = "lblPenalty"
        Me.lblPenalty.Size = New System.Drawing.Size(150, 20)
        Me.lblPenalty.TabIndex = 12
        Me.lblPenalty.Text = "Penalty:"
        Me.lblPenalty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPenaltyValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPenaltyValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.lblPenaltyValue.Location = New System.Drawing.Point(160, 145)
        Me.lblPenaltyValue.Name = "lblPenaltyValue"
        Me.lblPenaltyValue.Size = New System.Drawing.Size(150, 20)
        Me.lblPenaltyValue.TabIndex = 13
        Me.lblPenaltyValue.Text = "-"
        Me.lblPenaltyValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmountDue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblAmountDue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblAmountDue.Location = New System.Drawing.Point(10, 175)
        Me.lblAmountDue.Name = "lblAmountDue"
        Me.lblAmountDue.Size = New System.Drawing.Size(150, 20)
        Me.lblAmountDue.TabIndex = 14
        Me.lblAmountDue.Text = "Amount Due:"
        Me.lblAmountDue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAmountDueValue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblAmountDueValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblAmountDueValue.Location = New System.Drawing.Point(160, 175)
        Me.lblAmountDueValue.Name = "lblAmountDueValue"
        Me.lblAmountDueValue.Size = New System.Drawing.Size(150, 20)
        Me.lblAmountDueValue.TabIndex = 15
        Me.lblAmountDueValue.Text = "-"
        Me.lblAmountDueValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmountPaid.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAmountPaid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblAmountPaid.Location = New System.Drawing.Point(10, 195)
        Me.lblAmountPaid.Name = "lblAmountPaid"
        Me.lblAmountPaid.Size = New System.Drawing.Size(150, 20)
        Me.lblAmountPaid.TabIndex = 16
        Me.lblAmountPaid.Text = "Amount Paid:"
        Me.lblAmountPaid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAmountPaidValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAmountPaidValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblAmountPaidValue.Location = New System.Drawing.Point(160, 195)
        Me.lblAmountPaidValue.Name = "lblAmountPaidValue"
        Me.lblAmountPaidValue.Size = New System.Drawing.Size(150, 20)
        Me.lblAmountPaidValue.TabIndex = 17
        Me.lblAmountPaidValue.Text = "-"
        Me.lblAmountPaidValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblChange.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblChange.Location = New System.Drawing.Point(10, 220)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(150, 20)
        Me.lblChange.TabIndex = 18
        Me.lblChange.Text = "Change:"
        Me.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChangeValue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblChangeValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblChangeValue.Location = New System.Drawing.Point(160, 220)
        Me.lblChangeValue.Name = "lblChangeValue"
        Me.lblChangeValue.Size = New System.Drawing.Size(150, 20)
        Me.lblChangeValue.TabIndex = 19
        Me.lblChangeValue.Text = "-"
        Me.lblChangeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(104, Byte), Integer))
        Me.btnOk.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOk.FlatAppearance.BorderSize = 0
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(20, 430)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(150, 40)
        Me.btnOk.TabIndex = 20
        Me.btnOk.Text = "CHECKOUT"
        Me.btnOk.UseVisualStyleBackColor = False
        Me.btnSaveReceipt.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(104, Byte), Integer))
        Me.btnSaveReceipt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSaveReceipt.FlatAppearance.BorderSize = 0
        Me.btnSaveReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveReceipt.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaveReceipt.ForeColor = System.Drawing.Color.White
        Me.btnSaveReceipt.Location = New System.Drawing.Point(190, 430)
        Me.btnSaveReceipt.Name = "btnSaveReceipt"
        Me.btnSaveReceipt.Size = New System.Drawing.Size(150, 40)
        Me.btnSaveReceipt.TabIndex = 21
        Me.btnSaveReceipt.Text = "SAVE RECEIPT"
        Me.btnSaveReceipt.UseVisualStyleBackColor = False
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(360, 490)
        Me.Controls.Add(Me.btnSaveReceipt)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.lblCheckoutConfirmed)
        Me.Controls.Add(Me.pnlIcon)
        Me.Controls.Add(Me.HeaderPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "CheckoutDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Billing Summary"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        Me.pnlIcon.ResumeLayout(False)
        Me.pnlDetails.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents pnlIcon As ParkingSystemProject.RoundedPanel
    Friend WithEvents lblIcon As System.Windows.Forms.Label
    Friend WithEvents lblCheckoutConfirmed As System.Windows.Forms.Label
    Friend WithEvents pnlDetails As ParkingSystemProject.RoundedPanel
    Friend WithEvents lblPlateNumber As System.Windows.Forms.Label
    Friend WithEvents lblPlateNumberValue As System.Windows.Forms.Label
    Friend WithEvents lblTimeIn As System.Windows.Forms.Label
    Friend WithEvents lblTimeInValue As System.Windows.Forms.Label
    Friend WithEvents lblTimeOut As System.Windows.Forms.Label
    Friend WithEvents lblTimeOutValue As System.Windows.Forms.Label
    Friend WithEvents lblTotalHours As System.Windows.Forms.Label
    Friend WithEvents lblTotalHoursValue As System.Windows.Forms.Label
    Friend WithEvents lblBaseRate As System.Windows.Forms.Label
    Friend WithEvents lblBaseRateValue As System.Windows.Forms.Label
    Friend WithEvents lblDiscount As System.Windows.Forms.Label
    Friend WithEvents lblDiscountValue As System.Windows.Forms.Label
    Friend WithEvents lblPenalty As System.Windows.Forms.Label
    Friend WithEvents lblPenaltyValue As System.Windows.Forms.Label
    Friend WithEvents lblAmountDue As System.Windows.Forms.Label
    Friend WithEvents lblAmountDueValue As System.Windows.Forms.Label
    Friend WithEvents lblAmountPaid As System.Windows.Forms.Label
    Friend WithEvents lblAmountPaidValue As System.Windows.Forms.Label
    Friend WithEvents lblChange As System.Windows.Forms.Label
    Friend WithEvents lblChangeValue As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnSaveReceipt As System.Windows.Forms.Button
End Class
