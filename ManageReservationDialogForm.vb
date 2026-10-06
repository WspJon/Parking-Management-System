Imports System.Windows.Forms
Imports System.Drawing

Public Class ManageReservationDialogForm
    Public SelectedAction As String = ""
    Private slotName As String
    Private plateText As String
    Private reservedBy As String

    Public Sub New(slotName As String, plateText As String, reservedBy As String)
        InitializeComponent()
        Me.slotName = slotName
        Me.plateText = plateText
        Me.reservedBy = reservedBy
    End Sub

    Private Sub ManageReservationDialogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.MakeRoundedControl(Me, 10)
        ThemeManager.MakeRoundedControl(btnCheckIn, 6)
        ThemeManager.MakeRoundedControl(btnCancelRes, 6)
        ThemeManager.MakeRoundedControl(pnlInfo, 6)
        
        lblReserving.Text = "Slot " & slotName
        lblPlate.Text = plateText
        lblName.Text = "Reserved By: " & If(String.IsNullOrWhiteSpace(reservedBy), "Unknown", reservedBy)

        If Form1.CurrentUserRole = "Customer" Then
            btnCheckIn.Visible = False
            btnCancelRes.Location = New Point(CInt((Me.ClientSize.Width - btnCancelRes.Width) / 2), btnCancelRes.Location.Y)
        End If
    End Sub
    
    Private Sub ManageReservationDialogForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawRectangle(Pens.Gray, 0, 0, Me.Width - 1, Me.Height - 1)
    End Sub

    Private Sub btnCheckIn_Click(sender As Object, e As EventArgs) Handles btnCheckIn.Click
        SelectedAction = "CheckIn"
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancelRes_Click(sender As Object, e As EventArgs) Handles btnCancelRes.Click
        SelectedAction = "Cancel"
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
