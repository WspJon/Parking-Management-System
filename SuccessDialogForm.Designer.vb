<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuccessDialogForm
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
        Me.pnlIcon = New ParkingSystemProject.RoundedPanel()
        Me.lblIcon = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.pnlIcon.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlIcon
        '
        Me.pnlIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.pnlIcon.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlIcon.BorderSize = 1
        Me.pnlIcon.Controls.Add(Me.lblIcon)
        Me.pnlIcon.CornerRadius = 20
        Me.pnlIcon.Location = New System.Drawing.Point(27, 49)
        Me.pnlIcon.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlIcon.Name = "pnlIcon"
        Me.pnlIcon.Size = New System.Drawing.Size(53, 49)
        Me.pnlIcon.TabIndex = 0
        '
        'lblIcon
        '
        Me.lblIcon.BackColor = System.Drawing.Color.Transparent
        Me.lblIcon.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIcon.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblIcon.ForeColor = System.Drawing.Color.White
        Me.lblIcon.Location = New System.Drawing.Point(0, 0)
        Me.lblIcon.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIcon.Name = "lblIcon"
        Me.lblIcon.Size = New System.Drawing.Size(53, 49)
        Me.lblIcon.TabIndex = 0
        Me.lblIcon.Text = "i"
        Me.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMessage
        '
        Me.lblMessage.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.lblMessage.Location = New System.Drawing.Point(93, 55)
        Me.lblMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(347, 49)
        Me.lblMessage.TabIndex = 1
        Me.lblMessage.Text = "Car successfully parked at slot U14"
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.White
        Me.btnOk.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.Location = New System.Drawing.Point(333, 129)
        Me.btnOk.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(107, 37)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'SuccessDialogForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(467, 185)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.pnlIcon)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "SuccessDialogForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SuccessDialogForm"
        Me.pnlIcon.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlIcon As ParkingSystemProject.RoundedPanel
    Friend WithEvents lblIcon As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
End Class
