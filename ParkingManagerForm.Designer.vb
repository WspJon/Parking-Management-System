<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParkingManagerForm
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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeaderTitle = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.pnlStatAvailCar = New System.Windows.Forms.Panel()
        Me.lblStatAvailCarTitle = New System.Windows.Forms.Label()
        Me.lblStatAvailCarVal = New System.Windows.Forms.Label()
        Me.pnlStatOccCar = New System.Windows.Forms.Panel()
        Me.lblStatOccCarTitle = New System.Windows.Forms.Label()
        Me.lblStatOccCarVal = New System.Windows.Forms.Label()
        Me.pnlStatAvailMoto = New System.Windows.Forms.Panel()
        Me.lblStatAvailMotoTitle = New System.Windows.Forms.Label()
        Me.lblStatAvailMotoVal = New System.Windows.Forms.Label()
        Me.pnlStatOccMoto = New System.Windows.Forms.Panel()
        Me.lblStatOccMotoTitle = New System.Windows.Forms.Label()
        Me.lblStatOccMotoVal = New System.Windows.Forms.Label()
        Me.pnlCardCheckOut = New ParkingSystemProject.RoundedPanel()
        Me.lblCheckOutTitle = New System.Windows.Forms.Label()
        Me.cboSearchPlate = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.cmbDiscount = New System.Windows.Forms.ComboBox()
        Me.lblFee = New System.Windows.Forms.Label()
        Me.lblAmtPaid = New System.Windows.Forms.Label()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.lblChange = New System.Windows.Forms.Label()
        Me.btnProcessPayment = New System.Windows.Forms.Button()
        Me.pnlCardCheckIn = New ParkingSystemProject.RoundedPanel()
        Me.lblCheckInTitle = New System.Windows.Forms.Label()
        Me.lblPlateNum = New System.Windows.Forms.Label()
        Me.txtPlateNumber = New System.Windows.Forms.TextBox()
        Me.lblVehType = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.lblSelectSlot = New System.Windows.Forms.Label()
        Me.cboParkingSlots = New System.Windows.Forms.ComboBox()
        Me.lblSlotStatusTitle = New System.Windows.Forms.Label()
        Me.lblSlotStatus = New System.Windows.Forms.Label()
        Me.btnPark = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlStatAvailCar.SuspendLayout()
        Me.pnlStatOccCar.SuspendLayout()
        Me.pnlStatAvailMoto.SuspendLayout()
        Me.pnlStatOccMoto.SuspendLayout()
        Me.pnlCardCheckOut.SuspendLayout()
        Me.pnlCardCheckIn.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeaderTitle)
        Me.pnlHeader.Controls.Add(Me.btnExit)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1020, 60)
        Me.pnlHeader.TabIndex = 6
        '
        'lblHeaderTitle
        '
        Me.lblHeaderTitle.AutoSize = True
        Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeaderTitle.ForeColor = System.Drawing.Color.White
        Me.lblHeaderTitle.Location = New System.Drawing.Point(15, 15)
        Me.lblHeaderTitle.Name = "lblHeaderTitle"
        Me.lblHeaderTitle.Size = New System.Drawing.Size(288, 25)
        Me.lblHeaderTitle.TabIndex = 0
        Me.lblHeaderTitle.Text = "Teller terminal — POS manager"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.btnExit.FlatAppearance.BorderSize = 0
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(920, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 36)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "Back"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'pnlStatAvailCar
        '
        Me.pnlStatAvailCar.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlStatAvailCar.Controls.Add(Me.lblStatAvailCarTitle)
        Me.pnlStatAvailCar.Controls.Add(Me.lblStatAvailCarVal)
        Me.pnlStatAvailCar.Location = New System.Drawing.Point(20, 80)
        Me.pnlStatAvailCar.Name = "pnlStatAvailCar"
        Me.pnlStatAvailCar.Size = New System.Drawing.Size(200, 80)
        Me.pnlStatAvailCar.TabIndex = 5
        '
        'lblStatAvailCarTitle
        '
        Me.lblStatAvailCarTitle.AutoSize = True
        Me.lblStatAvailCarTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStatAvailCarTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblStatAvailCarTitle.Location = New System.Drawing.Point(10, 10)
        Me.lblStatAvailCarTitle.Name = "lblStatAvailCarTitle"
        Me.lblStatAvailCarTitle.Size = New System.Drawing.Size(93, 19)
        Me.lblStatAvailCarTitle.TabIndex = 0
        Me.lblStatAvailCarTitle.Text = "Available (car)"
        '
        'lblStatAvailCarVal
        '
        Me.lblStatAvailCarVal.AutoSize = True
        Me.lblStatAvailCarVal.Font = New System.Drawing.Font("Segoe UI", 22.0!)
        Me.lblStatAvailCarVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.lblStatAvailCarVal.Location = New System.Drawing.Point(5, 30)
        Me.lblStatAvailCarVal.Name = "lblStatAvailCarVal"
        Me.lblStatAvailCarVal.Size = New System.Drawing.Size(30, 41)
        Me.lblStatAvailCarVal.TabIndex = 1
        Me.lblStatAvailCarVal.Text = "-"
        '
        'pnlStatOccCar
        '
        Me.pnlStatOccCar.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlStatOccCar.Controls.Add(Me.lblStatOccCarTitle)
        Me.pnlStatOccCar.Controls.Add(Me.lblStatOccCarVal)
        Me.pnlStatOccCar.Location = New System.Drawing.Point(240, 80)
        Me.pnlStatOccCar.Name = "pnlStatOccCar"
        Me.pnlStatOccCar.Size = New System.Drawing.Size(200, 80)
        Me.pnlStatOccCar.TabIndex = 4
        '
        'lblStatOccCarTitle
        '
        Me.lblStatOccCarTitle.AutoSize = True
        Me.lblStatOccCarTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStatOccCarTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblStatOccCarTitle.Location = New System.Drawing.Point(10, 10)
        Me.lblStatOccCarTitle.Name = "lblStatOccCarTitle"
        Me.lblStatOccCarTitle.Size = New System.Drawing.Size(96, 19)
        Me.lblStatOccCarTitle.TabIndex = 0
        Me.lblStatOccCarTitle.Text = "Occupied (car)"
        '
        'lblStatOccCarVal
        '
        Me.lblStatOccCarVal.AutoSize = True
        Me.lblStatOccCarVal.Font = New System.Drawing.Font("Segoe UI", 22.0!)
        Me.lblStatOccCarVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.lblStatOccCarVal.Location = New System.Drawing.Point(5, 30)
        Me.lblStatOccCarVal.Name = "lblStatOccCarVal"
        Me.lblStatOccCarVal.Size = New System.Drawing.Size(30, 41)
        Me.lblStatOccCarVal.TabIndex = 1
        Me.lblStatOccCarVal.Text = "-"
        '
        'pnlStatAvailMoto
        '
        Me.pnlStatAvailMoto.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlStatAvailMoto.Controls.Add(Me.lblStatAvailMotoTitle)
        Me.pnlStatAvailMoto.Controls.Add(Me.lblStatAvailMotoVal)
        Me.pnlStatAvailMoto.Location = New System.Drawing.Point(460, 80)
        Me.pnlStatAvailMoto.Name = "pnlStatAvailMoto"
        Me.pnlStatAvailMoto.Size = New System.Drawing.Size(200, 80)
        Me.pnlStatAvailMoto.TabIndex = 3
        '
        'lblStatAvailMotoTitle
        '
        Me.lblStatAvailMotoTitle.AutoSize = True
        Me.lblStatAvailMotoTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStatAvailMotoTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblStatAvailMotoTitle.Location = New System.Drawing.Point(10, 10)
        Me.lblStatAvailMotoTitle.Name = "lblStatAvailMotoTitle"
        Me.lblStatAvailMotoTitle.Size = New System.Drawing.Size(108, 19)
        Me.lblStatAvailMotoTitle.TabIndex = 0
        Me.lblStatAvailMotoTitle.Text = "Available (moto)"
        '
        'lblStatAvailMotoVal
        '
        Me.lblStatAvailMotoVal.AutoSize = True
        Me.lblStatAvailMotoVal.Font = New System.Drawing.Font("Segoe UI", 22.0!)
        Me.lblStatAvailMotoVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.lblStatAvailMotoVal.Location = New System.Drawing.Point(5, 30)
        Me.lblStatAvailMotoVal.Name = "lblStatAvailMotoVal"
        Me.lblStatAvailMotoVal.Size = New System.Drawing.Size(30, 41)
        Me.lblStatAvailMotoVal.TabIndex = 1
        Me.lblStatAvailMotoVal.Text = "-"
        '
        'pnlStatOccMoto
        '
        Me.pnlStatOccMoto.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlStatOccMoto.Controls.Add(Me.lblStatOccMotoTitle)
        Me.pnlStatOccMoto.Controls.Add(Me.lblStatOccMotoVal)
        Me.pnlStatOccMoto.Location = New System.Drawing.Point(680, 80)
        Me.pnlStatOccMoto.Name = "pnlStatOccMoto"
        Me.pnlStatOccMoto.Size = New System.Drawing.Size(200, 80)
        Me.pnlStatOccMoto.TabIndex = 2
        '
        'lblStatOccMotoTitle
        '
        Me.lblStatOccMotoTitle.AutoSize = True
        Me.lblStatOccMotoTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStatOccMotoTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblStatOccMotoTitle.Location = New System.Drawing.Point(10, 10)
        Me.lblStatOccMotoTitle.Name = "lblStatOccMotoTitle"
        Me.lblStatOccMotoTitle.Size = New System.Drawing.Size(111, 19)
        Me.lblStatOccMotoTitle.TabIndex = 0
        Me.lblStatOccMotoTitle.Text = "Occupied (moto)"
        '
        'lblStatOccMotoVal
        '
        Me.lblStatOccMotoVal.AutoSize = True
        Me.lblStatOccMotoVal.Font = New System.Drawing.Font("Segoe UI", 22.0!)
        Me.lblStatOccMotoVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.lblStatOccMotoVal.Location = New System.Drawing.Point(5, 30)
        Me.lblStatOccMotoVal.Name = "lblStatOccMotoVal"
        Me.lblStatOccMotoVal.Size = New System.Drawing.Size(30, 41)
        Me.lblStatOccMotoVal.TabIndex = 1
        Me.lblStatOccMotoVal.Text = "-"
        '
        'pnlCardCheckOut
        '
        Me.pnlCardCheckOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlCardCheckOut.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlCardCheckOut.BorderSize = 1
        Me.pnlCardCheckOut.Controls.Add(Me.lblCheckOutTitle)
        Me.pnlCardCheckOut.Controls.Add(Me.cboSearchPlate)
        Me.pnlCardCheckOut.Controls.Add(Me.btnSearch)
        Me.pnlCardCheckOut.Controls.Add(Me.lblDiscount)
        Me.pnlCardCheckOut.Controls.Add(Me.cmbDiscount)
        Me.pnlCardCheckOut.Controls.Add(Me.lblFee)
        Me.pnlCardCheckOut.Controls.Add(Me.lblAmtPaid)
        Me.pnlCardCheckOut.Controls.Add(Me.txtAmountPaid)
        Me.pnlCardCheckOut.Controls.Add(Me.lblChange)
        Me.pnlCardCheckOut.Controls.Add(Me.btnProcessPayment)
        Me.pnlCardCheckOut.CornerRadius = 8
        Me.pnlCardCheckOut.Location = New System.Drawing.Point(460, 180)
        Me.pnlCardCheckOut.Name = "pnlCardCheckOut"
        Me.pnlCardCheckOut.Size = New System.Drawing.Size(420, 400)
        Me.pnlCardCheckOut.TabIndex = 0
        '
        'lblCheckOutTitle
        '
        Me.lblCheckOutTitle.AutoSize = True
        Me.lblCheckOutTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblCheckOutTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblCheckOutTitle.Location = New System.Drawing.Point(15, 15)
        Me.lblCheckOutTitle.Name = "lblCheckOutTitle"
        Me.lblCheckOutTitle.Size = New System.Drawing.Size(187, 21)
        Me.lblCheckOutTitle.TabIndex = 0
        Me.lblCheckOutTitle.Text = "Checkout and payment"
        '
        'cboSearchPlate
        '
        Me.cboSearchPlate.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cboSearchPlate.Location = New System.Drawing.Point(18, 55)
        Me.cboSearchPlate.MaxLength = 7
        Me.cboSearchPlate.Name = "cboSearchPlate"
        Me.cboSearchPlate.Size = New System.Drawing.Size(280, 29)
        Me.cboSearchPlate.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(308, 54)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(90, 31)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'lblDiscount
        '
        Me.lblDiscount.AutoSize = True
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblDiscount.Location = New System.Drawing.Point(15, 105)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Size = New System.Drawing.Size(87, 15)
        Me.lblDiscount.TabIndex = 3
        Me.lblDiscount.Text = "Select discount"
        '
        'cmbDiscount
        '
        Me.cmbDiscount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDiscount.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbDiscount.Items.AddRange(New Object() {"None", "Senior Citizen", "PWD"})
        Me.cmbDiscount.Location = New System.Drawing.Point(18, 125)
        Me.cmbDiscount.Name = "cmbDiscount"
        Me.cmbDiscount.Size = New System.Drawing.Size(380, 29)
        Me.cmbDiscount.TabIndex = 4
        '
        'lblFee
        '
        Me.lblFee.AutoSize = True
        Me.lblFee.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblFee.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblFee.Location = New System.Drawing.Point(15, 175)
        Me.lblFee.Name = "lblFee"
        Me.lblFee.Size = New System.Drawing.Size(119, 20)
        Me.lblFee.TabIndex = 5
        Me.lblFee.Text = "Total fee: ₱0.00"
        '
        'lblAmtPaid
        '
        Me.lblAmtPaid.AutoSize = True
        Me.lblAmtPaid.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAmtPaid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblAmtPaid.Location = New System.Drawing.Point(15, 215)
        Me.lblAmtPaid.Name = "lblAmtPaid"
        Me.lblAmtPaid.Size = New System.Drawing.Size(77, 15)
        Me.lblAmtPaid.TabIndex = 6
        Me.lblAmtPaid.Text = "Amount paid"
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtAmountPaid.Location = New System.Drawing.Point(18, 235)
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.Size = New System.Drawing.Size(380, 29)
        Me.txtAmountPaid.TabIndex = 7
        '
        'lblChange
        '
        Me.lblChange.AutoSize = True
        Me.lblChange.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.lblChange.Location = New System.Drawing.Point(15, 275)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(104, 19)
        Me.lblChange.TabIndex = 8
        Me.lblChange.Text = "Change: ₱0.00"
        '
        'btnProcessPayment
        '
        Me.btnProcessPayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.btnProcessPayment.FlatAppearance.BorderSize = 0
        Me.btnProcessPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProcessPayment.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnProcessPayment.ForeColor = System.Drawing.Color.White
        Me.btnProcessPayment.Location = New System.Drawing.Point(18, 305)
        Me.btnProcessPayment.Name = "btnProcessPayment"
        Me.btnProcessPayment.Size = New System.Drawing.Size(380, 45)
        Me.btnProcessPayment.TabIndex = 9
        Me.btnProcessPayment.Text = "Process payment"
        Me.btnProcessPayment.UseVisualStyleBackColor = False
        '
        'pnlCardCheckIn
        '
        Me.pnlCardCheckIn.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlCardCheckIn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlCardCheckIn.BorderSize = 1
        Me.pnlCardCheckIn.Controls.Add(Me.lblCheckInTitle)
        Me.pnlCardCheckIn.Controls.Add(Me.lblPlateNum)
        Me.pnlCardCheckIn.Controls.Add(Me.txtPlateNumber)
        Me.pnlCardCheckIn.Controls.Add(Me.lblVehType)
        Me.pnlCardCheckIn.Controls.Add(Me.cmbType)
        Me.pnlCardCheckIn.Controls.Add(Me.lblSelectSlot)
        Me.pnlCardCheckIn.Controls.Add(Me.cboParkingSlots)
        Me.pnlCardCheckIn.Controls.Add(Me.lblSlotStatusTitle)
        Me.pnlCardCheckIn.Controls.Add(Me.lblSlotStatus)
        Me.pnlCardCheckIn.Controls.Add(Me.btnPark)
        Me.pnlCardCheckIn.CornerRadius = 8
        Me.pnlCardCheckIn.Location = New System.Drawing.Point(20, 180)
        Me.pnlCardCheckIn.Name = "pnlCardCheckIn"
        Me.pnlCardCheckIn.Size = New System.Drawing.Size(420, 400)
        Me.pnlCardCheckIn.TabIndex = 1
        '
        'lblCheckInTitle
        '
        Me.lblCheckInTitle.AutoSize = True
        Me.lblCheckInTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblCheckInTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblCheckInTitle.Location = New System.Drawing.Point(15, 15)
        Me.lblCheckInTitle.Name = "lblCheckInTitle"
        Me.lblCheckInTitle.Size = New System.Drawing.Size(135, 21)
        Me.lblCheckInTitle.TabIndex = 0
        Me.lblCheckInTitle.Text = "Check-in vehicle"
        '
        'lblPlateNum
        '
        Me.lblPlateNum.AutoSize = True
        Me.lblPlateNum.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPlateNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblPlateNum.Location = New System.Drawing.Point(15, 55)
        Me.lblPlateNum.Name = "lblPlateNum"
        Me.lblPlateNum.Size = New System.Drawing.Size(78, 15)
        Me.lblPlateNum.TabIndex = 1
        Me.lblPlateNum.Text = "Plate number"
        '
        'txtPlateNumber
        '
        Me.txtPlateNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPlateNumber.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtPlateNumber.Location = New System.Drawing.Point(18, 75)
        Me.txtPlateNumber.MaxLength = 7
        Me.txtPlateNumber.Name = "txtPlateNumber"
        Me.txtPlateNumber.Size = New System.Drawing.Size(380, 29)
        Me.txtPlateNumber.TabIndex = 2
        '
        'lblVehType
        '
        Me.lblVehType.AutoSize = True
        Me.lblVehType.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblVehType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblVehType.Location = New System.Drawing.Point(15, 125)
        Me.lblVehType.Name = "lblVehType"
        Me.lblVehType.Size = New System.Drawing.Size(70, 15)
        Me.lblVehType.TabIndex = 3
        Me.lblVehType.Text = "Vehicle type"
        '
        'cmbType
        '
        Me.cmbType.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbType.Items.AddRange(New Object() {"Car", "Motorcycle"})
        Me.cmbType.Location = New System.Drawing.Point(18, 145)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(380, 29)
        Me.cmbType.TabIndex = 4
        '
        'lblSelectSlot
        '
        Me.lblSelectSlot.AutoSize = True
        Me.lblSelectSlot.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSelectSlot.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblSelectSlot.Location = New System.Drawing.Point(15, 195)
        Me.lblSelectSlot.Name = "lblSelectSlot"
        Me.lblSelectSlot.Size = New System.Drawing.Size(60, 15)
        Me.lblSelectSlot.TabIndex = 5
        Me.lblSelectSlot.Text = "Select slot"
        '
        'cboParkingSlots
        '
        Me.cboParkingSlots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboParkingSlots.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cboParkingSlots.Location = New System.Drawing.Point(18, 215)
        Me.cboParkingSlots.Name = "cboParkingSlots"
        Me.cboParkingSlots.Size = New System.Drawing.Size(380, 29)
        Me.cboParkingSlots.TabIndex = 6
        '
        'lblSlotStatusTitle
        '
        Me.lblSlotStatusTitle.AutoSize = True
        Me.lblSlotStatusTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSlotStatusTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblSlotStatusTitle.Location = New System.Drawing.Point(15, 260)
        Me.lblSlotStatusTitle.Name = "lblSlotStatusTitle"
        Me.lblSlotStatusTitle.Size = New System.Drawing.Size(67, 15)
        Me.lblSlotStatusTitle.TabIndex = 7
        Me.lblSlotStatusTitle.Text = "Slot status: "
        '
        'lblSlotStatus
        '
        Me.lblSlotStatus.AutoSize = True
        Me.lblSlotStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblSlotStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.lblSlotStatus.Location = New System.Drawing.Point(85, 260)
        Me.lblSlotStatus.Name = "lblSlotStatus"
        Me.lblSlotStatus.Size = New System.Drawing.Size(12, 15)
        Me.lblSlotStatus.TabIndex = 8
        Me.lblSlotStatus.Text = "-"
        '
        'btnPark
        '
        Me.btnPark.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.btnPark.FlatAppearance.BorderSize = 0
        Me.btnPark.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPark.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnPark.ForeColor = System.Drawing.Color.White
        Me.btnPark.Location = New System.Drawing.Point(18, 305)
        Me.btnPark.Name = "btnPark"
        Me.btnPark.Size = New System.Drawing.Size(380, 45)
        Me.btnPark.TabIndex = 9
        Me.btnPark.Text = "Park vehicle"
        Me.btnPark.UseVisualStyleBackColor = False
        '
        'ParkingManagerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1020, 641)
        Me.Controls.Add(Me.pnlCardCheckOut)
        Me.Controls.Add(Me.pnlCardCheckIn)
        Me.Controls.Add(Me.pnlStatOccMoto)
        Me.Controls.Add(Me.pnlStatAvailMoto)
        Me.Controls.Add(Me.pnlStatOccCar)
        Me.Controls.Add(Me.pnlStatAvailCar)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ParkingManagerForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Teller terminal"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlStatAvailCar.ResumeLayout(False)
        Me.pnlStatAvailCar.PerformLayout()
        Me.pnlStatOccCar.ResumeLayout(False)
        Me.pnlStatOccCar.PerformLayout()
        Me.pnlStatAvailMoto.ResumeLayout(False)
        Me.pnlStatAvailMoto.PerformLayout()
        Me.pnlStatOccMoto.ResumeLayout(False)
        Me.pnlStatOccMoto.PerformLayout()
        Me.pnlCardCheckOut.ResumeLayout(False)
        Me.pnlCardCheckOut.PerformLayout()
        Me.pnlCardCheckIn.ResumeLayout(False)
        Me.pnlCardCheckIn.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button

    Friend WithEvents pnlStatAvailCar As System.Windows.Forms.Panel
    Friend WithEvents lblStatAvailCarTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatAvailCarVal As System.Windows.Forms.Label

    Friend WithEvents pnlStatOccCar As System.Windows.Forms.Panel
    Friend WithEvents lblStatOccCarTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatOccCarVal As System.Windows.Forms.Label

    Friend WithEvents pnlStatAvailMoto As System.Windows.Forms.Panel
    Friend WithEvents lblStatAvailMotoTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatAvailMotoVal As System.Windows.Forms.Label

    Friend WithEvents pnlStatOccMoto As System.Windows.Forms.Panel
    Friend WithEvents lblStatOccMotoTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatOccMotoVal As System.Windows.Forms.Label

    Friend WithEvents pnlCardCheckIn As ParkingSystemProject.RoundedPanel
    Friend WithEvents lblCheckInTitle As System.Windows.Forms.Label
    Friend WithEvents lblPlateNum As System.Windows.Forms.Label
    Friend WithEvents txtPlateNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblVehType As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectSlot As System.Windows.Forms.Label
    Friend WithEvents cboParkingSlots As System.Windows.Forms.ComboBox
    Friend WithEvents lblSlotStatusTitle As System.Windows.Forms.Label
    Friend WithEvents lblSlotStatus As System.Windows.Forms.Label
    Friend WithEvents btnPark As System.Windows.Forms.Button

    Friend WithEvents pnlCardCheckOut As ParkingSystemProject.RoundedPanel
    Friend WithEvents lblCheckOutTitle As System.Windows.Forms.Label
    Friend WithEvents cboSearchPlate As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblDiscount As System.Windows.Forms.Label
    Friend WithEvents cmbDiscount As System.Windows.Forms.ComboBox
    Friend WithEvents lblFee As System.Windows.Forms.Label
    Friend WithEvents lblAmtPaid As System.Windows.Forms.Label
    Friend WithEvents txtAmountPaid As System.Windows.Forms.TextBox
    Friend WithEvents lblChange As System.Windows.Forms.Label
    Friend WithEvents btnProcessPayment As System.Windows.Forms.Button
End Class
