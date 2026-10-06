<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RateConfigDialogForm
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
        Me.lblCarBase = New System.Windows.Forms.Label()
        Me.txtCarBase = New System.Windows.Forms.TextBox()
        Me.lblMotorBase = New System.Windows.Forms.Label()
        Me.txtMotorBase = New System.Windows.Forms.TextBox()
        Me.lblCarSucceeding = New System.Windows.Forms.Label()
        Me.txtCarSucceeding = New System.Windows.Forms.TextBox()
        Me.lblMotorSucceeding = New System.Windows.Forms.Label()
        Me.txtMotorSucceeding = New System.Windows.Forms.TextBox()
        Me.lblFreeHours = New System.Windows.Forms.Label()
        Me.txtFreeHours = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(400, 40)
        Me.pnlHeader.TabIndex = 0
        
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(147, 20)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Rate Configuration"
        
        Me.lblCarBase.AutoSize = True
        Me.lblCarBase.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCarBase.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblCarBase.Location = New System.Drawing.Point(20, 55)
        Me.lblCarBase.Name = "lblCarBase"
        Me.lblCarBase.Size = New System.Drawing.Size(117, 15)
        Me.lblCarBase.TabIndex = 1
        Me.lblCarBase.Text = "Car Base Rate (?):"
        
        Me.txtCarBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarBase.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtCarBase.Location = New System.Drawing.Point(230, 52)
        Me.txtCarBase.Name = "txtCarBase"
        Me.txtCarBase.Size = New System.Drawing.Size(140, 25)
        Me.txtCarBase.TabIndex = 2
        Me.txtCarBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        
        Me.lblMotorBase.AutoSize = True
        Me.lblMotorBase.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblMotorBase.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblMotorBase.Location = New System.Drawing.Point(20, 100)
        Me.lblMotorBase.Name = "lblMotorBase"
        Me.lblMotorBase.Size = New System.Drawing.Size(133, 15)
        Me.lblMotorBase.TabIndex = 3
        Me.lblMotorBase.Text = "Motor Base Rate (?):"
        
        Me.txtMotorBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMotorBase.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtMotorBase.Location = New System.Drawing.Point(230, 97)
        Me.txtMotorBase.Name = "txtMotorBase"
        Me.txtMotorBase.Size = New System.Drawing.Size(140, 25)
        Me.txtMotorBase.TabIndex = 4
        Me.txtMotorBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        
        Me.lblCarSucceeding.AutoSize = True
        Me.lblCarSucceeding.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCarSucceeding.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblCarSucceeding.Location = New System.Drawing.Point(20, 145)
        Me.lblCarSucceeding.Name = "lblCarSucceeding"
        Me.lblCarSucceeding.Size = New System.Drawing.Size(126, 15)
        Me.lblCarSucceeding.TabIndex = 5
        Me.lblCarSucceeding.Text = "Car Extra Rate (?/hr):"
        
        Me.txtCarSucceeding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarSucceeding.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtCarSucceeding.Location = New System.Drawing.Point(230, 142)
        Me.txtCarSucceeding.Name = "txtCarSucceeding"
        Me.txtCarSucceeding.Size = New System.Drawing.Size(140, 25)
        Me.txtCarSucceeding.TabIndex = 6
        Me.txtCarSucceeding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        
        Me.lblMotorSucceeding.AutoSize = True
        Me.lblMotorSucceeding.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblMotorSucceeding.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblMotorSucceeding.Location = New System.Drawing.Point(20, 190)
        Me.lblMotorSucceeding.Name = "lblMotorSucceeding"
        Me.lblMotorSucceeding.Size = New System.Drawing.Size(142, 15)
        Me.lblMotorSucceeding.TabIndex = 7
        Me.lblMotorSucceeding.Text = "Motor Extra Rate (?/hr):"
        
        Me.txtMotorSucceeding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMotorSucceeding.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtMotorSucceeding.Location = New System.Drawing.Point(230, 187)
        Me.txtMotorSucceeding.Name = "txtMotorSucceeding"
        Me.txtMotorSucceeding.Size = New System.Drawing.Size(140, 25)
        Me.txtMotorSucceeding.TabIndex = 8
        Me.txtMotorSucceeding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        
        Me.lblFreeHours.AutoSize = True
        Me.lblFreeHours.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblFreeHours.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblFreeHours.Location = New System.Drawing.Point(20, 235)
        Me.lblFreeHours.Name = "lblFreeHours"
        Me.lblFreeHours.Size = New System.Drawing.Size(74, 15)
        Me.lblFreeHours.TabIndex = 9
        Me.lblFreeHours.Text = "Base Hours:"
        
        Me.txtFreeHours.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFreeHours.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtFreeHours.Location = New System.Drawing.Point(230, 232)
        Me.txtFreeHours.Name = "txtFreeHours"
        Me.txtFreeHours.Size = New System.Drawing.Size(140, 25)
        Me.txtFreeHours.TabIndex = 10
        Me.txtFreeHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(20, 320)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(170, 40)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(200, 320)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(180, 40)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "SAVE"
        Me.btnSave.UseVisualStyleBackColor = False
        
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(400, 380)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtFreeHours)
        Me.Controls.Add(Me.lblFreeHours)
        Me.Controls.Add(Me.txtMotorSucceeding)
        Me.Controls.Add(Me.lblMotorSucceeding)
        Me.Controls.Add(Me.txtCarSucceeding)
        Me.Controls.Add(Me.lblCarSucceeding)
        Me.Controls.Add(Me.txtMotorBase)
        Me.Controls.Add(Me.lblMotorBase)
        Me.Controls.Add(Me.txtCarBase)
        Me.Controls.Add(Me.lblCarBase)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "RateConfigDialogForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Rate Configuration"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblCarBase As System.Windows.Forms.Label
    Friend WithEvents txtCarBase As System.Windows.Forms.TextBox
    Friend WithEvents lblMotorBase As System.Windows.Forms.Label
    Friend WithEvents txtMotorBase As System.Windows.Forms.TextBox
    Friend WithEvents lblCarSucceeding As System.Windows.Forms.Label
    Friend WithEvents txtCarSucceeding As System.Windows.Forms.TextBox
    Friend WithEvents lblMotorSucceeding As System.Windows.Forms.Label
    Friend WithEvents txtMotorSucceeding As System.Windows.Forms.TextBox
    Friend WithEvents lblFreeHours As System.Windows.Forms.Label
    Friend WithEvents txtFreeHours As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
End Class
