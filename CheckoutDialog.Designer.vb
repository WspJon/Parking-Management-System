<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CheckoutDialog
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.pnlIcon = New ParkingSystemProject.RoundedPanel()
        Me.lblCheckoutConfirmed = New System.Windows.Forms.Label()
        Me.lblParkingName = New System.Windows.Forms.Label()
        Me.pnlDetails = New ParkingSystemProject.RoundedPanel()
        Me.lblReceiptNo = New System.Windows.Forms.Label()
        Me.lblReceiptNoValue = New System.Windows.Forms.Label()
        Me.lblPlateNumber = New System.Windows.Forms.Label()
        Me.lblPlateNumberValue = New System.Windows.Forms.Label()
        Me.lblVehicleType = New System.Windows.Forms.Label()
        Me.lblVehicleTypeValue = New System.Windows.Forms.Label()
        Me.lblTimeIn = New System.Windows.Forms.Label()
        Me.lblTimeInValue = New System.Windows.Forms.Label()
        Me.lblTimeOut = New System.Windows.Forms.Label()
        Me.lblTimeOutValue = New System.Windows.Forms.Label()
        Me.lblTotalHours = New System.Windows.Forms.Label()
        Me.lblTotalHoursValue = New System.Windows.Forms.Label()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.lblRateValue = New System.Windows.Forms.Label()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.lblSubtotalValue = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblDiscountValue = New System.Windows.Forms.Label()
        Me.lblDiscountType = New System.Windows.Forms.Label()
        Me.lblDiscountTypeValue = New System.Windows.Forms.Label()
        Me.lblPenalty = New System.Windows.Forms.Label()
        Me.lblPenaltyValue = New System.Windows.Forms.Label()
        Me.lblAmountDue = New System.Windows.Forms.Label()
        Me.lblAmountDueValue = New System.Windows.Forms.Label()
        Me.lblAmountPaid = New System.Windows.Forms.Label()
        Me.lblAmountPaidValue = New System.Windows.Forms.Label()
        Me.lblChange = New System.Windows.Forms.Label()
        Me.lblChangeValue = New System.Windows.Forms.Label()
        Me.lblCashier = New System.Windows.Forms.Label()
        Me.lblCashierValue = New System.Windows.Forms.Label()
        Me.btnSaveReceipt = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblFooterText = New System.Windows.Forms.Label()
        Me.HeaderPanel.SuspendLayout()
        Me.pnlDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.HeaderPanel.Controls.Add(Me.TitleLabel)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(489, 40)
        Me.HeaderPanel.TabIndex = 0
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.TitleLabel.ForeColor = System.Drawing.Color.White
        Me.TitleLabel.Location = New System.Drawing.Point(15, 10)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(125, 20)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "Billing Summary"
        '
        'pnlIcon
        '
        Me.pnlIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.pnlIcon.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlIcon.BorderSize = 0
        Me.pnlIcon.CornerRadius = 14
        Me.pnlIcon.Location = New System.Drawing.Point(219, 52)
        Me.pnlIcon.Name = "pnlIcon"
        Me.pnlIcon.Size = New System.Drawing.Size(50, 50)
        Me.pnlIcon.TabIndex = 1
        '
        'lblCheckoutConfirmed
        '
        Me.lblCheckoutConfirmed.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblCheckoutConfirmed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblCheckoutConfirmed.Location = New System.Drawing.Point(0, 108)
        Me.lblCheckoutConfirmed.Name = "lblCheckoutConfirmed"
        Me.lblCheckoutConfirmed.Size = New System.Drawing.Size(489, 24)
        Me.lblCheckoutConfirmed.TabIndex = 2
        Me.lblCheckoutConfirmed.Text = "Checkout Confirmed"
        Me.lblCheckoutConfirmed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblParkingName
        '
        Me.lblParkingName.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblParkingName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.lblParkingName.Location = New System.Drawing.Point(0, 132)
        Me.lblParkingName.Name = "lblParkingName"
        Me.lblParkingName.Size = New System.Drawing.Size(489, 18)
        Me.lblParkingName.TabIndex = 3
        Me.lblParkingName.Text = "[Parking Name]"
        Me.lblParkingName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlDetails
        '
        Me.pnlDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.pnlDetails.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlDetails.BorderSize = 1
        Me.pnlDetails.Controls.Add(Me.lblReceiptNo)
        Me.pnlDetails.Controls.Add(Me.lblReceiptNoValue)
        Me.pnlDetails.Controls.Add(Me.lblPlateNumber)
        Me.pnlDetails.Controls.Add(Me.lblPlateNumberValue)
        Me.pnlDetails.Controls.Add(Me.lblVehicleType)
        Me.pnlDetails.Controls.Add(Me.lblVehicleTypeValue)
        Me.pnlDetails.Controls.Add(Me.lblTimeIn)
        Me.pnlDetails.Controls.Add(Me.lblTimeInValue)
        Me.pnlDetails.Controls.Add(Me.lblTimeOut)
        Me.pnlDetails.Controls.Add(Me.lblTimeOutValue)
        Me.pnlDetails.Controls.Add(Me.lblTotalHours)
        Me.pnlDetails.Controls.Add(Me.lblTotalHoursValue)
        Me.pnlDetails.Controls.Add(Me.lblRate)
        Me.pnlDetails.Controls.Add(Me.lblRateValue)
        Me.pnlDetails.Controls.Add(Me.lblSubtotal)
        Me.pnlDetails.Controls.Add(Me.lblSubtotalValue)
        Me.pnlDetails.Controls.Add(Me.lblDiscount)
        Me.pnlDetails.Controls.Add(Me.lblDiscountValue)
        Me.pnlDetails.Controls.Add(Me.lblDiscountType)
        Me.pnlDetails.Controls.Add(Me.lblDiscountTypeValue)
        Me.pnlDetails.Controls.Add(Me.lblPenalty)
        Me.pnlDetails.Controls.Add(Me.lblPenaltyValue)
        Me.pnlDetails.Controls.Add(Me.lblAmountDue)
        Me.pnlDetails.Controls.Add(Me.lblAmountDueValue)
        Me.pnlDetails.Controls.Add(Me.lblAmountPaid)
        Me.pnlDetails.Controls.Add(Me.lblAmountPaidValue)
        Me.pnlDetails.Controls.Add(Me.lblChange)
        Me.pnlDetails.Controls.Add(Me.lblChangeValue)
        Me.pnlDetails.Controls.Add(Me.lblCashier)
        Me.pnlDetails.Controls.Add(Me.lblCashierValue)
        Me.pnlDetails.CornerRadius = 10
        Me.pnlDetails.Location = New System.Drawing.Point(26, 156)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(437, 396)
        Me.pnlDetails.TabIndex = 4
        '
        'lblReceiptNo
        '
        Me.lblReceiptNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblReceiptNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblReceiptNo.Location = New System.Drawing.Point(16, 12)
        Me.lblReceiptNo.Name = "lblReceiptNo"
        Me.lblReceiptNo.Size = New System.Drawing.Size(150, 20)
        Me.lblReceiptNo.TabIndex = 0
        Me.lblReceiptNo.Text = "Receipt No."
        Me.lblReceiptNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReceiptNoValue
        '
        Me.lblReceiptNoValue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblReceiptNoValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblReceiptNoValue.Location = New System.Drawing.Point(230, 12)
        Me.lblReceiptNoValue.Name = "lblReceiptNoValue"
        Me.lblReceiptNoValue.Size = New System.Drawing.Size(191, 20)
        Me.lblReceiptNoValue.TabIndex = 1
        Me.lblReceiptNoValue.Text = "000123"
        Me.lblReceiptNoValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPlateNumber
        '
        Me.lblPlateNumber.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPlateNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblPlateNumber.Location = New System.Drawing.Point(16, 34)
        Me.lblPlateNumber.Name = "lblPlateNumber"
        Me.lblPlateNumber.Size = New System.Drawing.Size(150, 20)
        Me.lblPlateNumber.TabIndex = 2
        Me.lblPlateNumber.Text = "Plate Number:"
        Me.lblPlateNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPlateNumberValue
        '
        Me.lblPlateNumberValue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPlateNumberValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblPlateNumberValue.Location = New System.Drawing.Point(230, 34)
        Me.lblPlateNumberValue.Name = "lblPlateNumberValue"
        Me.lblPlateNumberValue.Size = New System.Drawing.Size(191, 20)
        Me.lblPlateNumberValue.TabIndex = 3
        Me.lblPlateNumberValue.Text = "SDFS2312"
        Me.lblPlateNumberValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVehicleType
        '
        Me.lblVehicleType.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblVehicleType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblVehicleType.Location = New System.Drawing.Point(16, 56)
        Me.lblVehicleType.Name = "lblVehicleType"
        Me.lblVehicleType.Size = New System.Drawing.Size(150, 20)
        Me.lblVehicleType.TabIndex = 4
        Me.lblVehicleType.Text = "Vehicle Type"
        Me.lblVehicleType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVehicleTypeValue
        '
        Me.lblVehicleTypeValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblVehicleTypeValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblVehicleTypeValue.Location = New System.Drawing.Point(230, 56)
        Me.lblVehicleTypeValue.Name = "lblVehicleTypeValue"
        Me.lblVehicleTypeValue.Size = New System.Drawing.Size(191, 20)
        Me.lblVehicleTypeValue.TabIndex = 5
        Me.lblVehicleTypeValue.Text = "Car"
        Me.lblVehicleTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTimeIn
        '
        Me.lblTimeIn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTimeIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTimeIn.Location = New System.Drawing.Point(16, 90)
        Me.lblTimeIn.Name = "lblTimeIn"
        Me.lblTimeIn.Size = New System.Drawing.Size(150, 20)
        Me.lblTimeIn.TabIndex = 6
        Me.lblTimeIn.Text = "Time In"
        Me.lblTimeIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTimeInValue
        '
        Me.lblTimeInValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTimeInValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTimeInValue.Location = New System.Drawing.Point(170, 90)
        Me.lblTimeInValue.Name = "lblTimeInValue"
        Me.lblTimeInValue.Size = New System.Drawing.Size(251, 20)
        Me.lblTimeInValue.TabIndex = 7
        Me.lblTimeInValue.Text = "Oct 15, 2026 01:18 AM"
        Me.lblTimeInValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTimeOut
        '
        Me.lblTimeOut.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTimeOut.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTimeOut.Location = New System.Drawing.Point(16, 112)
        Me.lblTimeOut.Name = "lblTimeOut"
        Me.lblTimeOut.Size = New System.Drawing.Size(150, 20)
        Me.lblTimeOut.TabIndex = 8
        Me.lblTimeOut.Text = "Time Out"
        Me.lblTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTimeOutValue
        '
        Me.lblTimeOutValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTimeOutValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTimeOutValue.Location = New System.Drawing.Point(170, 112)
        Me.lblTimeOutValue.Name = "lblTimeOutValue"
        Me.lblTimeOutValue.Size = New System.Drawing.Size(251, 20)
        Me.lblTimeOutValue.TabIndex = 9
        Me.lblTimeOutValue.Text = "Oct 15, 2026 01:18 AM"
        Me.lblTimeOutValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalHours
        '
        Me.lblTotalHours.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTotalHours.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTotalHours.Location = New System.Drawing.Point(16, 134)
        Me.lblTotalHours.Name = "lblTotalHours"
        Me.lblTotalHours.Size = New System.Drawing.Size(150, 20)
        Me.lblTotalHours.TabIndex = 10
        Me.lblTotalHours.Text = "Total Hours"
        Me.lblTotalHours.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHoursValue
        '
        Me.lblTotalHoursValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTotalHoursValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTotalHoursValue.Location = New System.Drawing.Point(170, 134)
        Me.lblTotalHoursValue.Name = "lblTotalHoursValue"
        Me.lblTotalHoursValue.Size = New System.Drawing.Size(251, 20)
        Me.lblTotalHoursValue.TabIndex = 11
        Me.lblTotalHoursValue.Text = "1 hr"
        Me.lblTotalHoursValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRate
        '
        Me.lblRate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblRate.Location = New System.Drawing.Point(16, 168)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(150, 20)
        Me.lblRate.TabIndex = 12
        Me.lblRate.Text = "Rate"
        Me.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRateValue
        '
        Me.lblRateValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblRateValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblRateValue.Location = New System.Drawing.Point(230, 168)
        Me.lblRateValue.Name = "lblRateValue"
        Me.lblRateValue.Size = New System.Drawing.Size(191, 20)
        Me.lblRateValue.TabIndex = 13
        Me.lblRateValue.Text = "₱50.00 / hr"
        Me.lblRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSubtotal
        '
        Me.lblSubtotal.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSubtotal.Location = New System.Drawing.Point(16, 190)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(150, 20)
        Me.lblSubtotal.TabIndex = 14
        Me.lblSubtotal.Text = "Subtotal"
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSubtotalValue
        '
        Me.lblSubtotalValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSubtotalValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSubtotalValue.Location = New System.Drawing.Point(230, 190)
        Me.lblSubtotalValue.Name = "lblSubtotalValue"
        Me.lblSubtotalValue.Size = New System.Drawing.Size(191, 20)
        Me.lblSubtotalValue.TabIndex = 15
        Me.lblSubtotalValue.Text = "₱50.00"
        Me.lblSubtotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscount
        '
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.lblDiscount.Location = New System.Drawing.Point(16, 212)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Size = New System.Drawing.Size(150, 20)
        Me.lblDiscount.TabIndex = 16
        Me.lblDiscount.Text = "Discount (20%)"
        Me.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDiscountValue
        '
        Me.lblDiscountValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDiscountValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.lblDiscountValue.Location = New System.Drawing.Point(230, 212)
        Me.lblDiscountValue.Name = "lblDiscountValue"
        Me.lblDiscountValue.Size = New System.Drawing.Size(191, 20)
        Me.lblDiscountValue.TabIndex = 17
        Me.lblDiscountValue.Text = "-₱10.00"
        Me.lblDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscountType
        '
        Me.lblDiscountType.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDiscountType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.lblDiscountType.Location = New System.Drawing.Point(16, 234)
        Me.lblDiscountType.Name = "lblDiscountType"
        Me.lblDiscountType.Size = New System.Drawing.Size(150, 20)
        Me.lblDiscountType.TabIndex = 18
        Me.lblDiscountType.Text = "Discount Type"
        Me.lblDiscountType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDiscountTypeValue
        '
        Me.lblDiscountTypeValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDiscountTypeValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.lblDiscountTypeValue.Location = New System.Drawing.Point(230, 234)
        Me.lblDiscountTypeValue.Name = "lblDiscountTypeValue"
        Me.lblDiscountTypeValue.Size = New System.Drawing.Size(191, 20)
        Me.lblDiscountTypeValue.TabIndex = 19
        Me.lblDiscountTypeValue.Text = "Senior Citizen"
        Me.lblDiscountTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPenalty
        '
        Me.lblPenalty.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPenalty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblPenalty.Location = New System.Drawing.Point(16, 256)
        Me.lblPenalty.Name = "lblPenalty"
        Me.lblPenalty.Size = New System.Drawing.Size(150, 20)
        Me.lblPenalty.TabIndex = 20
        Me.lblPenalty.Text = "Penalty"
        Me.lblPenalty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPenaltyValue
        '
        Me.lblPenaltyValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPenaltyValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblPenaltyValue.Location = New System.Drawing.Point(230, 256)
        Me.lblPenaltyValue.Name = "lblPenaltyValue"
        Me.lblPenaltyValue.Size = New System.Drawing.Size(191, 20)
        Me.lblPenaltyValue.TabIndex = 21
        Me.lblPenaltyValue.Text = "₱0.00"
        Me.lblPenaltyValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmountDue
        '
        Me.lblAmountDue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblAmountDue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblAmountDue.Location = New System.Drawing.Point(16, 290)
        Me.lblAmountDue.Name = "lblAmountDue"
        Me.lblAmountDue.Size = New System.Drawing.Size(150, 20)
        Me.lblAmountDue.TabIndex = 22
        Me.lblAmountDue.Text = "Amount Due:"
        Me.lblAmountDue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAmountDueValue
        '
        Me.lblAmountDueValue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblAmountDueValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblAmountDueValue.Location = New System.Drawing.Point(230, 290)
        Me.lblAmountDueValue.Name = "lblAmountDueValue"
        Me.lblAmountDueValue.Size = New System.Drawing.Size(191, 20)
        Me.lblAmountDueValue.TabIndex = 23
        Me.lblAmountDueValue.Text = "₱40.00"
        Me.lblAmountDueValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmountPaid
        '
        Me.lblAmountPaid.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAmountPaid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblAmountPaid.Location = New System.Drawing.Point(16, 312)
        Me.lblAmountPaid.Name = "lblAmountPaid"
        Me.lblAmountPaid.Size = New System.Drawing.Size(180, 20)
        Me.lblAmountPaid.TabIndex = 24
        Me.lblAmountPaid.Text = "Amount Paid (Cash):"
        Me.lblAmountPaid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAmountPaidValue
        '
        Me.lblAmountPaidValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAmountPaidValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblAmountPaidValue.Location = New System.Drawing.Point(230, 312)
        Me.lblAmountPaidValue.Name = "lblAmountPaidValue"
        Me.lblAmountPaidValue.Size = New System.Drawing.Size(191, 20)
        Me.lblAmountPaidValue.TabIndex = 25
        Me.lblAmountPaidValue.Text = "₱50.00"
        Me.lblAmountPaidValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblChange
        '
        Me.lblChange.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblChange.Location = New System.Drawing.Point(16, 334)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(150, 20)
        Me.lblChange.TabIndex = 26
        Me.lblChange.Text = "Change:"
        Me.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblChangeValue
        '
        Me.lblChangeValue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblChangeValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblChangeValue.Location = New System.Drawing.Point(230, 334)
        Me.lblChangeValue.Name = "lblChangeValue"
        Me.lblChangeValue.Size = New System.Drawing.Size(191, 20)
        Me.lblChangeValue.TabIndex = 27
        Me.lblChangeValue.Text = "₱10.00"
        Me.lblChangeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCashier
        '
        Me.lblCashier.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblCashier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblCashier.Location = New System.Drawing.Point(16, 368)
        Me.lblCashier.Name = "lblCashier"
        Me.lblCashier.Size = New System.Drawing.Size(150, 20)
        Me.lblCashier.TabIndex = 28
        Me.lblCashier.Text = "Cashier"
        Me.lblCashier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCashierValue
        '
        Me.lblCashierValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblCashierValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblCashierValue.Location = New System.Drawing.Point(230, 368)
        Me.lblCashierValue.Name = "lblCashierValue"
        Me.lblCashierValue.Size = New System.Drawing.Size(191, 20)
        Me.lblCashierValue.TabIndex = 29
        Me.lblCashierValue.Text = "Juan D."
        Me.lblCashierValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSaveReceipt
        '
        Me.btnSaveReceipt.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(104, Byte), Integer))
        Me.btnSaveReceipt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSaveReceipt.FlatAppearance.BorderSize = 0
        Me.btnSaveReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveReceipt.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaveReceipt.ForeColor = System.Drawing.Color.White
        Me.btnSaveReceipt.Location = New System.Drawing.Point(84, 570)
        Me.btnSaveReceipt.Name = "btnSaveReceipt"
        Me.btnSaveReceipt.Size = New System.Drawing.Size(150, 40)
        Me.btnSaveReceipt.TabIndex = 5
        Me.btnSaveReceipt.Text = "SAVE RECEIPT"
        Me.btnSaveReceipt.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(104, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(254, 570)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(150, 40)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "CLOSE"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblFooterText
        '
        Me.lblFooterText.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblFooterText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.lblFooterText.Location = New System.Drawing.Point(0, 620)
        Me.lblFooterText.Name = "lblFooterText"
        Me.lblFooterText.Size = New System.Drawing.Size(489, 20)
        Me.lblFooterText.TabIndex = 7
        Me.lblFooterText.Text = "Thank you! Keep this receipt."
        Me.lblFooterText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CheckoutDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(489, 655)
        Me.Controls.Add(Me.lblFooterText)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSaveReceipt)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.lblParkingName)
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
        Me.pnlDetails.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents pnlIcon As ParkingSystemProject.RoundedPanel
    Friend WithEvents lblCheckoutConfirmed As System.Windows.Forms.Label
    Friend WithEvents lblParkingName As System.Windows.Forms.Label
    Friend WithEvents pnlDetails As ParkingSystemProject.RoundedPanel

    Friend WithEvents lblReceiptNo As System.Windows.Forms.Label
    Friend WithEvents lblReceiptNoValue As System.Windows.Forms.Label
    Friend WithEvents lblPlateNumber As System.Windows.Forms.Label
    Friend WithEvents lblPlateNumberValue As System.Windows.Forms.Label
    Friend WithEvents lblVehicleType As System.Windows.Forms.Label
    Friend WithEvents lblVehicleTypeValue As System.Windows.Forms.Label

    Friend WithEvents lblTimeIn As System.Windows.Forms.Label
    Friend WithEvents lblTimeInValue As System.Windows.Forms.Label
    Friend WithEvents lblTimeOut As System.Windows.Forms.Label
    Friend WithEvents lblTimeOutValue As System.Windows.Forms.Label
    Friend WithEvents lblTotalHours As System.Windows.Forms.Label
    Friend WithEvents lblTotalHoursValue As System.Windows.Forms.Label

    Friend WithEvents lblRate As System.Windows.Forms.Label
    Friend WithEvents lblRateValue As System.Windows.Forms.Label
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents lblSubtotalValue As System.Windows.Forms.Label
    Friend WithEvents lblDiscount As System.Windows.Forms.Label
    Friend WithEvents lblDiscountValue As System.Windows.Forms.Label
    Friend WithEvents lblDiscountType As System.Windows.Forms.Label
    Friend WithEvents lblDiscountTypeValue As System.Windows.Forms.Label
    Friend WithEvents lblPenalty As System.Windows.Forms.Label
    Friend WithEvents lblPenaltyValue As System.Windows.Forms.Label

    Friend WithEvents lblAmountDue As System.Windows.Forms.Label
    Friend WithEvents lblAmountDueValue As System.Windows.Forms.Label
    Friend WithEvents lblAmountPaid As System.Windows.Forms.Label
    Friend WithEvents lblAmountPaidValue As System.Windows.Forms.Label
    Friend WithEvents lblChange As System.Windows.Forms.Label
    Friend WithEvents lblChangeValue As System.Windows.Forms.Label

    Friend WithEvents lblCashier As System.Windows.Forms.Label
    Friend WithEvents lblCashierValue As System.Windows.Forms.Label

    Friend WithEvents btnSaveReceipt As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblFooterText As System.Windows.Forms.Label

    ' Backward compatibility properties for any legacy code
    Public Property btnOk As System.Windows.Forms.Button
        Get
            Return btnClose
        End Get
        Set(value As System.Windows.Forms.Button)
            btnClose = value
        End Set
    End Property

    Public ReadOnly Property lblBaseRate As System.Windows.Forms.Label
        Get
            Return lblRate
        End Get
    End Property

    Public ReadOnly Property lblBaseRateValue As System.Windows.Forms.Label
        Get
            Return lblRateValue
        End Get
    End Property

    Public ReadOnly Property lblDicountTypeValue As System.Windows.Forms.Label
        Get
            Return lblDiscountTypeValue
        End Get
    End Property

    Public ReadOnly Property Label1 As System.Windows.Forms.Label
        Get
            Return lblDiscountType
        End Get
    End Property

End Class
