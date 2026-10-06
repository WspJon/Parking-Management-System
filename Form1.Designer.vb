<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlLogo = New ParkingSystemProject.RoundedPanel()
        Me.lblLogo = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCreateAccount = New System.Windows.Forms.Label()
        Me.lblFooter = New System.Windows.Forms.Label()
        Me.pnlPass = New ParkingSystemProject.RoundedPanel()
        Me.lblPassIcon = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.pnlUser = New ParkingSystemProject.RoundedPanel()
        Me.lblUserIcon = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.chkShowPassword = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.pnlLogo.SuspendLayout()
        Me.pnlPass.SuspendLayout()
        Me.pnlUser.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.Location = New System.Drawing.Point(67, 535)
        Me.btnLogin.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(200, 55)
        Me.btnLogin.TabIndex = 4
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Panel1.Controls.Add(Me.pnlLogo)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1360, 74)
        Me.Panel1.TabIndex = 5
        '
        'pnlLogo
        '
        Me.pnlLogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.pnlLogo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlLogo.BorderSize = 1
        Me.pnlLogo.Controls.Add(Me.lblLogo)
        Me.pnlLogo.CornerRadius = 8
        Me.pnlLogo.Location = New System.Drawing.Point(40, 18)
        Me.pnlLogo.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlLogo.Name = "pnlLogo"
        Me.pnlLogo.Size = New System.Drawing.Size(40, 37)
        Me.pnlLogo.TabIndex = 2
        '
        'lblLogo
        '
        Me.lblLogo.BackColor = System.Drawing.Color.Transparent
        Me.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLogo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblLogo.ForeColor = System.Drawing.Color.White
        Me.lblLogo.Location = New System.Drawing.Point(0, 0)
        Me.lblLogo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLogo.Name = "lblLogo"
        Me.lblLogo.Size = New System.Drawing.Size(40, 37)
        Me.lblLogo.TabIndex = 0
        Me.lblLogo.Text = "P"
        Me.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(91, 0)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(400, 74)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "JJRSP PARKING"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(1307, 20)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(37, 34)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "X"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(67, 314)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Username"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(67, 412)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Password"
        '
        'lblCreateAccount
        '
        Me.lblCreateAccount.AutoSize = True
        Me.lblCreateAccount.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblCreateAccount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Underline)
        Me.lblCreateAccount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.lblCreateAccount.Location = New System.Drawing.Point(67, 609)
        Me.lblCreateAccount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCreateAccount.Name = "lblCreateAccount"
        Me.lblCreateAccount.Size = New System.Drawing.Size(239, 20)
        Me.lblCreateAccount.TabIndex = 6
        Me.lblCreateAccount.Text = "Don't have an account? Create one"
        '
        'lblFooter
        '
        Me.lblFooter.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblFooter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.lblFooter.Location = New System.Drawing.Point(67, 640)
        Me.lblFooter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFooter.Name = "lblFooter"
        Me.lblFooter.Size = New System.Drawing.Size(440, 25)
        Me.lblFooter.TabIndex = 5
        Me.lblFooter.Text = "JJRSP Parking System © 2026"
        Me.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPass
        '
        Me.pnlPass.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlPass.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlPass.BorderSize = 1
        Me.pnlPass.Controls.Add(Me.lblPassIcon)
        Me.pnlPass.Controls.Add(Me.txtPassword)
        Me.pnlPass.CornerRadius = 8
        Me.pnlPass.Location = New System.Drawing.Point(67, 441)
        Me.pnlPass.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlPass.Name = "pnlPass"
        Me.pnlPass.Size = New System.Drawing.Size(440, 55)
        Me.pnlPass.TabIndex = 4
        '
        'lblPassIcon
        '
        Me.lblPassIcon.AutoSize = True
        Me.lblPassIcon.Font = New System.Drawing.Font("Segoe UI Emoji", 11.0!)
        Me.lblPassIcon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.lblPassIcon.Location = New System.Drawing.Point(16, 15)
        Me.lblPassIcon.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPassIcon.Name = "lblPassIcon"
        Me.lblPassIcon.Size = New System.Drawing.Size(38, 26)
        Me.lblPassIcon.TabIndex = 1
        Me.lblPassIcon.Text = "🔒"
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.txtPassword.Location = New System.Drawing.Point(56, 16)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txtPassword.Size = New System.Drawing.Size(367, 25)
        Me.txtPassword.TabIndex = 0
        '
        'pnlUser
        '
        Me.pnlUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlUser.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlUser.BorderSize = 1
        Me.pnlUser.Controls.Add(Me.lblUserIcon)
        Me.pnlUser.Controls.Add(Me.txtUsername)
        Me.pnlUser.CornerRadius = 8
        Me.pnlUser.Location = New System.Drawing.Point(67, 342)
        Me.pnlUser.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlUser.Name = "pnlUser"
        Me.pnlUser.Size = New System.Drawing.Size(440, 55)
        Me.pnlUser.TabIndex = 2
        '
        'lblUserIcon
        '
        Me.lblUserIcon.AutoSize = True
        Me.lblUserIcon.Font = New System.Drawing.Font("Segoe UI Emoji", 11.0!)
        Me.lblUserIcon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.lblUserIcon.Location = New System.Drawing.Point(16, 15)
        Me.lblUserIcon.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUserIcon.Name = "lblUserIcon"
        Me.lblUserIcon.Size = New System.Drawing.Size(38, 26)
        Me.lblUserIcon.TabIndex = 1
        Me.lblUserIcon.Text = "👤"
        '
        'txtUsername
        '
        Me.txtUsername.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.txtUsername.Location = New System.Drawing.Point(56, 16)
        Me.txtUsername.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(367, 25)
        Me.txtUsername.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ParkingSystemProject.My.Resources.Resources.ParkingBackground__1_
        Me.PictureBox1.InitialImage = Global.ParkingSystemProject.My.Resources.Resources.CarBG
        Me.PictureBox1.Location = New System.Drawing.Point(597, 20)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(759, 880)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'chkShowPassword
        '
        Me.chkShowPassword.AutoSize = True
        Me.chkShowPassword.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.chkShowPassword.Location = New System.Drawing.Point(67, 505)
        Me.chkShowPassword.Margin = New System.Windows.Forms.Padding(4)
        Me.chkShowPassword.Name = "chkShowPassword"
        Me.chkShowPassword.Size = New System.Drawing.Size(132, 24)
        Me.chkShowPassword.TabIndex = 8
        Me.chkShowPassword.Text = "Show Password"
        Me.chkShowPassword.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1360, 785)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.chkShowPassword)
        Me.Controls.Add(Me.lblCreateAccount)
        Me.Controls.Add(Me.lblFooter)
        Me.Controls.Add(Me.pnlPass)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pnlUser)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Parking System Login"
        Me.Panel1.ResumeLayout(False)
        Me.pnlLogo.ResumeLayout(False)
        Me.pnlPass.ResumeLayout(False)
        Me.pnlPass.PerformLayout()
        Me.pnlUser.ResumeLayout(False)
        Me.pnlUser.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnLogin As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents pnlLogo As ParkingSystemProject.RoundedPanel
    Friend WithEvents lblLogo As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents pnlUser As ParkingSystemProject.RoundedPanel
    Friend WithEvents lblUserIcon As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents pnlPass As ParkingSystemProject.RoundedPanel
    Friend WithEvents lblPassIcon As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblFooter As Label
    Friend WithEvents lblCreateAccount As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents chkShowPassword As System.Windows.Forms.CheckBox
End Class
