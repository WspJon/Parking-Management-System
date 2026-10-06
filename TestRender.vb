Imports System.Windows.Forms
Imports System.Drawing

Module Program
    Sub Main()
        Dim pnlSlot As New Panel()
        pnlSlot.Size = New Size(72, 65)
        pnlSlot.BackColor = Color.FromArgb(240, 160, 40)
        
        Dim lblName As New Label()
        lblName.Text = "G24"
        lblName.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        lblName.ForeColor = Color.White
        lblName.Location = New Point(0, 0)
        lblName.Size = New Size(72, 16)
        lblName.TextAlign = ContentAlignment.MiddleCenter
        lblName.BackColor = Color.Transparent
        pnlSlot.Controls.Add(lblName)
        
        Dim lblStatus As New Label()
        lblStatus.Text = "RSVD"
        lblStatus.Font = New Font("Segoe UI Semibold", 7.5!)
        lblStatus.ForeColor = Color.FromArgb(240, 240, 240)
        lblStatus.Location = New Point(0, 16)
        lblStatus.Size = New Size(72, 16)
        lblStatus.TextAlign = ContentAlignment.MiddleCenter
        lblStatus.BackColor = Color.Transparent
        pnlSlot.Controls.Add(lblStatus)
        
        Dim lblReserver As New Label()
        lblReserver.Text = "test125"
        lblReserver.Font = New Font("Segoe UI", 6.5!)
        lblReserver.ForeColor = Color.FromArgb(240, 240, 240)
        lblReserver.Location = New Point(0, 32)
        lblReserver.Size = New Size(72, 14)
        lblReserver.TextAlign = ContentAlignment.MiddleCenter
        lblReserver.BackColor = Color.Transparent
        pnlSlot.Controls.Add(lblReserver)
        
        Dim lblDate As New Label()
        lblDate.Text = "08/07"
        lblDate.Font = New Font("Segoe UI", 6.5!, FontStyle.Italic)
        lblDate.ForeColor = Color.FromArgb(220, 220, 220)
        lblDate.Location = New Point(0, 46)
        lblDate.Size = New Size(72, 14)
        lblDate.TextAlign = ContentAlignment.MiddleCenter
        lblDate.BackColor = Color.Transparent
        pnlSlot.Controls.Add(lblDate)
        
        Dim bmp As New Bitmap(72, 65)
        pnlSlot.DrawToBitmap(bmp, New Rectangle(0, 0, 72, 65))
        bmp.Save("rendered_slot.png")
    End Sub
End Module
