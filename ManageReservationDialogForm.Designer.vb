<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManageReservationDialogForm
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
        Me.lblPlate = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.btnCancelRes = New System.Windows.Forms.Button()
        Me.btnCheckIn = New System.Windows.Forms.Button()
        Me.lblClose = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.pnlInfo.SuspendLayout()
        Me.SuspendLayout()
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblClose)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(340, 40)
        Me.pnlHeader.TabIndex = 0
        Me.lblClose.AutoSize = True
        Me.lblClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblClose.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClose.ForeColor = System.Drawing.Color.White
        Me.lblClose.Location = New System.Drawing.Point(310, 8)
        Me.lblClose.Name = "lblClose"
        Me.lblClose.Size = New System.Drawing.Size(20, 21)
        Me.lblClose.TabIndex = 1
        Me.lblClose.Text = "X"
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(144, 19)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Manage Reservation"
        Me.pnlInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.pnlInfo.Controls.Add(Me.lblName)
        Me.pnlInfo.Controls.Add(Me.lblPlate)
        Me.pnlInfo.Controls.Add(Me.lblReserving)
        Me.pnlInfo.Location = New System.Drawing.Point(20, 55)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Size = New System.Drawing.Size(300, 110)
        Me.pnlInfo.TabIndex = 1
        Me.lblReserving.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReserving.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblReserving.Location = New System.Drawing.Point(0, 12)
        Me.lblReserving.Name = "lblReserving"
        Me.lblReserving.Size = New System.Drawing.Size(300, 20)
        Me.lblReserving.TabIndex = 0
        Me.lblReserving.Text = "Slot G14"
        Me.lblReserving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPlate.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblPlate.Location = New System.Drawing.Point(0, 32)
        Me.lblPlate.Name = "lblPlate"
        Me.lblPlate.Size = New System.Drawing.Size(300, 30)
        Me.lblPlate.TabIndex = 1
        Me.lblPlate.Text = "RESERVED"
        Me.lblPlate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(0, 65)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(300, 25)
        Me.lblName.TabIndex = 2
        Me.lblName.Text = "Reserved By: Unknown"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancelRes.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnCancelRes.FlatAppearance.BorderSize = 0
        Me.btnCancelRes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelRes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelRes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnCancelRes.Location = New System.Drawing.Point(20, 185)
        Me.btnCancelRes.Name = "btnCancelRes"
        Me.btnCancelRes.Size = New System.Drawing.Size(300, 35)
        Me.btnCancelRes.TabIndex = 2
        Me.btnCancelRes.Text = "CANCEL RESERVATION"
        Me.btnCancelRes.UseVisualStyleBackColor = False
        Me.btnCheckIn.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.btnCheckIn.FlatAppearance.BorderSize = 0
        Me.btnCheckIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckIn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheckIn.ForeColor = System.Drawing.Color.White
        Me.btnCheckIn.Location = New System.Drawing.Point(20, 230)
        Me.btnCheckIn.Name = "btnCheckIn"
        Me.btnCheckIn.Size = New System.Drawing.Size(300, 40)
        Me.btnCheckIn.TabIndex = 3
        Me.btnCheckIn.Text = "CHECK-IN CUSTOMER"
        Me.btnCheckIn.UseVisualStyleBackColor = False
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(340, 280)
        Me.Controls.Add(Me.btnCheckIn)
        Me.Controls.Add(Me.btnCancelRes)
        Me.Controls.Add(Me.pnlInfo)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ManageReservationDialogForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manage Reservation"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlInfo.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblClose As System.Windows.Forms.Label
    Friend WithEvents pnlInfo As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblPlate As System.Windows.Forms.Label
    Friend WithEvents lblReserving As System.Windows.Forms.Label
    Friend WithEvents btnCancelRes As System.Windows.Forms.Button
    Friend WithEvents btnCheckIn As System.Windows.Forms.Button
End Class
