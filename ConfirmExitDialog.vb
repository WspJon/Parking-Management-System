Imports System.Windows.Forms

Public Class ConfirmExitDialog

    Private Sub ConfirmExitDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.MakeRoundedControl(lblIcon, 25)
        ThemeManager.MakeRoundedControl(btnYes, 6)
        ThemeManager.MakeRoundedControl(btnNo, 6)
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
