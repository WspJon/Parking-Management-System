<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReserveDialogForm
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlInfo = New System.Windows.Forms.Panel()
        Me.lblReserving = New System.Windows.Forms.Label()
        Me.lblSlot = New System.Windows.Forms.Label()
        Me.lblPlateLabel = New System.Windows.Forms.Label()
        Me.pnlTxtBorder = New System.Windows.Forms.Panel()
        Me.txtPlate = New System.Windows.Forms.TextBox()
        Me.lblDateLabel = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.lblNameLabel = New System.Windows.Forms.Label()
        Me.pnlNameTxtBorder = New System.Windows.Forms.Panel()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnConfirm = New System.Windows.Forms.Button()
        
        Me.pnlHeader.SuspendLayout()
        Me.pnlInfo.SuspendLayout()
        Me.pnlTxtBorder.SuspendLayout()
        Me.pnlNameTxtBorder.SuspendLayout()
        Me.SuspendLayout()
        
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(340, 40)
        Me.pnlHeader.TabIndex = 0
        
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(96, 20)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Reserve Slot"
        
        Me.pnlInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.pnlInfo.Controls.Add(Me.lblReserving)
        Me.pnlInfo.Controls.Add(Me.lblSlot)
        Me.pnlInfo.Location = New System.Drawing.Point(20, 55)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Size = New System.Drawing.Size(300, 70)
        Me.pnlInfo.TabIndex = 1
        
        Me.lblReserving.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblReserving.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblReserving.Location = New System.Drawing.Point(0, 12)
        Me.lblReserving.Name = "lblReserving"
        Me.lblReserving.Size = New System.Drawing.Size(300, 20)
        Me.lblReserving.TabIndex = 0
        Me.lblReserving.Text = "Reserving slot"
        Me.lblReserving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        
        Me.lblSlot.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblSlot.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.lblSlot.Location = New System.Drawing.Point(0, 32)
        Me.lblSlot.Name = "lblSlot"
        Me.lblSlot.Size = New System.Drawing.Size(300, 30)
        Me.lblSlot.TabIndex = 1
        Me.lblSlot.Text = "SLOT"
        Me.lblSlot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        
        Me.lblPlateLabel.AutoSize = True
        Me.lblPlateLabel.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblPlateLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblPlateLabel.Location = New System.Drawing.Point(20, 135)
        Me.lblPlateLabel.Name = "lblPlateLabel"
        Me.lblPlateLabel.Size = New System.Drawing.Size(89, 13)
        Me.lblPlateLabel.TabIndex = 2
        Me.lblPlateLabel.Text = "PLATE NUMBER"
        
        Me.pnlTxtBorder.BackColor = System.Drawing.Color.White
        Me.pnlTxtBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTxtBorder.Controls.Add(Me.txtPlate)
        Me.pnlTxtBorder.Location = New System.Drawing.Point(20, 155)
        Me.pnlTxtBorder.Name = "pnlTxtBorder"
        Me.pnlTxtBorder.Size = New System.Drawing.Size(300, 35)
        Me.pnlTxtBorder.TabIndex = 3
        
        Me.txtPlate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPlate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPlate.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtPlate.Location = New System.Drawing.Point(10, 6)
        Me.txtPlate.MaxLength = 7
        Me.txtPlate.Name = "txtPlate"
        Me.txtPlate.Size = New System.Drawing.Size(280, 20)
        Me.txtPlate.TabIndex = 0
        
        Me.lblDateLabel.AutoSize = True
        Me.lblDateLabel.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDateLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblDateLabel.Location = New System.Drawing.Point(20, 205)
        Me.lblDateLabel.Name = "lblDateLabel"
        Me.lblDateLabel.Size = New System.Drawing.Size(109, 13)
        Me.lblDateLabel.TabIndex = 4
        Me.lblDateLabel.Text = "RESERVATION DATE"
        
        Me.dtpDate.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpDate.Location = New System.Drawing.Point(20, 225)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(300, 27)
        Me.dtpDate.TabIndex = 5
        
        Me.lblNameLabel.AutoSize = True
        Me.lblNameLabel.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblNameLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblNameLabel.Location = New System.Drawing.Point(20, 265)
        Me.lblNameLabel.Name = "lblNameLabel"
        Me.lblNameLabel.Size = New System.Drawing.Size(94, 13)
        Me.lblNameLabel.TabIndex = 6
        Me.lblNameLabel.Text = "RESERVER NAME"
        
        Me.pnlNameTxtBorder.BackColor = System.Drawing.Color.White
        Me.pnlNameTxtBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlNameTxtBorder.Controls.Add(Me.txtName)
        Me.pnlNameTxtBorder.Location = New System.Drawing.Point(20, 285)
        Me.pnlNameTxtBorder.Name = "pnlNameTxtBorder"
        Me.pnlNameTxtBorder.Size = New System.Drawing.Size(300, 35)
        Me.pnlNameTxtBorder.TabIndex = 7
        
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtName.Location = New System.Drawing.Point(10, 6)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(280, 20)
        Me.txtName.TabIndex = 0
        
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(20, 345)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(140, 40)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        
        Me.btnConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConfirm.FlatAppearance.BorderSize = 0
        Me.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfirm.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnConfirm.ForeColor = System.Drawing.Color.White
        Me.btnConfirm.Location = New System.Drawing.Point(170, 345)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(150, 40)
        Me.btnConfirm.TabIndex = 9
        Me.btnConfirm.Text = "CONFIRM"
        Me.btnConfirm.UseVisualStyleBackColor = False
        
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(340, 415)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.pnlNameTxtBorder)
        Me.Controls.Add(Me.lblNameLabel)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.lblDateLabel)
        Me.Controls.Add(Me.pnlTxtBorder)
        Me.Controls.Add(Me.lblPlateLabel)
        Me.Controls.Add(Me.pnlInfo)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ReserveDialogForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reserve Slot"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlInfo.ResumeLayout(False)
        Me.pnlTxtBorder.ResumeLayout(False)
        Me.pnlTxtBorder.PerformLayout()
        Me.pnlNameTxtBorder.ResumeLayout(False)
        Me.pnlNameTxtBorder.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlInfo As System.Windows.Forms.Panel
    Friend WithEvents lblReserving As System.Windows.Forms.Label
    Friend WithEvents lblSlot As System.Windows.Forms.Label
    Friend WithEvents lblPlateLabel As System.Windows.Forms.Label
    Friend WithEvents pnlTxtBorder As System.Windows.Forms.Panel
    Friend WithEvents txtPlate As System.Windows.Forms.TextBox
    Friend WithEvents lblDateLabel As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblNameLabel As System.Windows.Forms.Label
    Friend WithEvents pnlNameTxtBorder As System.Windows.Forms.Panel
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
End Class
