Imports System.Windows.Forms
Imports System.Drawing

Public Class ConfirmActionDialogForm
    Public Sub New(title As String, message As String, confirmText As String)
        InitializeComponent()
        lblTitle.Text = title
        lblMessage.Text = message
        btnYes.Text = confirmText
    End Sub

    Private Sub ConfirmActionDialogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.MakeRoundedControl(Me, 10)
        ThemeManager.MakeRoundedControl(btnNo, 6)
        ThemeManager.MakeRoundedControl(btnYes, 6)
    End Sub

    Private Sub ConfirmActionDialogForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawRectangle(Pens.Gray, 0, 0, Me.Width - 1, Me.Height - 1)
    End Sub

    Private Sub pnlIcon_Paint(sender As Object, e As PaintEventArgs) Handles pnlIcon.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Using br As New SolidBrush(Color.FromArgb(211, 56, 51))
            e.Graphics.FillEllipse(br, 0, 0, 60, 60)
        End Using
        Using pn As New Pen(Color.White, 3)
            pn.LineJoin = Drawing2D.LineJoin.Round
            e.Graphics.DrawLine(pn, 30, 15, 30, 35)
            e.Graphics.FillEllipse(Brushes.White, 28, 42, 4, 4)
        End Using
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub
End Class
