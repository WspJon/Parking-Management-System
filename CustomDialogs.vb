Imports System.Windows.Forms
Imports System.Drawing

Public Class OverlayHelper
    Public Shared Function ShowDialog(owner As Form, dialogForm As Form) As DialogResult
        Dim overlay As New Form()
        overlay.FormBorderStyle = FormBorderStyle.None
        overlay.BackColor = Color.Black
        overlay.Opacity = 0.6
        overlay.StartPosition = FormStartPosition.Manual
        overlay.Bounds = owner.Bounds
        overlay.ShowInTaskbar = False

        overlay.Show(owner)

        dialogForm.StartPosition = FormStartPosition.CenterParent
        Dim result As DialogResult = dialogForm.ShowDialog(overlay)

        overlay.Close()
        overlay.Dispose()
        Return result
    End Function
End Class

Public Class CustomDialogBase
    Inherits Form

    Public HeaderPanel As Panel
    Public TitleLabel As Label

    Public Sub New(title As String, targetSize As Size)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackColor = Color.White
        Me.Size = targetSize
        Me.ShowInTaskbar = False

        HeaderPanel = New Panel()
        HeaderPanel.BackColor = Color.FromArgb(27, 42, 71)
        HeaderPanel.Height = 45
        HeaderPanel.Dock = DockStyle.Top

        TitleLabel = New Label()
        TitleLabel.Text = title
        TitleLabel.ForeColor = Color.White
        TitleLabel.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        TitleLabel.Location = New Point(15, 12)
        TitleLabel.AutoSize = True

        HeaderPanel.Controls.Add(TitleLabel)
        Me.Controls.Add(HeaderPanel)

        AddHandler Me.Load, Sub(s, e)
                                ThemeManager.MakeRoundedControl(Me, 10)
                            End Sub
    End Sub

    Protected Function CreateButton(text As String, loc As Point, size As Size, bgColor As Color, fgColor As Color, Optional isPrimary As Boolean = False) As Button
        Dim btn As New Button()
        btn.Text = text
        btn.Location = loc
        btn.Size = size
        btn.BackColor = bgColor
        btn.ForeColor = fgColor
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btn.Cursor = Cursors.Hand

        AddHandler btn.Paint, Sub(s, e)
                                  ThemeManager.MakeRoundedControl(btn, 6)
                              End Sub
        Return btn
    End Function
End Class


