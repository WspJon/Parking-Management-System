Imports System.Drawing
Imports System.Windows.Forms

Public Class SuccessDialogForm

    Public Sub New(message As String)
        InitializeComponent()
        lblMessage.Text = message
        
        AddHandler Me.Paint, Sub(s, e)
                                 e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, Me.Width - 1, Me.Height - 1)
                             End Sub
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.Close()
    End Sub

    Public Shared Sub ShowSuccess(message As String)
        Using dlg As New SuccessDialogForm(message)
            dlg.ShowDialog()
        End Using
    End Sub

    Private Sub lblMessage_Click(sender As Object, e As EventArgs) Handles lblMessage.Click

    End Sub
End Class
