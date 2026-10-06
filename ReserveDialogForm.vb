Imports System.Windows.Forms
Imports System.Drawing

Public Class ReserveDialogForm
    Public PlateNumber As String = ""
    Public ReserverName As String = ""
    Public ReservationDate As DateTime = DateTime.Now

    Private Const EM_SETCUEBANNER As Integer = &H1501
    <System.Runtime.InteropServices.DllImport("user32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)> lParam As String) As IntPtr
    End Function

    Public Sub New(slotName As String)
        InitializeComponent()
        lblSlot.Text = slotName
    End Sub

    Private Sub ReserveDialogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.MakeRoundedControl(Me, 10)
        ThemeManager.MakeRoundedControl(pnlInfo, 6)
        ThemeManager.MakeRoundedControl(btnCancel, 6)
        ThemeManager.MakeRoundedControl(btnConfirm, 6)

        txtPlate.MaxLength = 8
        SendMessage(txtPlate.Handle, EM_SETCUEBANNER, 0, "Enter plate number")
        SendMessage(txtName.Handle, EM_SETCUEBANNER, 0, "Enter customer name")
        If Form1.CurrentUserRole = "Customer" Then
            txtName.Text = Form1.CurrentFullName
            txtName.ReadOnly = True
        End If
        
        dtpDate.MinDate = DateTime.Now
    End Sub

    Private Sub ReserveDialogForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawRectangle(Pens.Gray, 0, 0, Me.Width - 1, Me.Height - 1)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim normalizedPlate As String = DatabaseModule.NormalizePlate(txtPlate.Text)
        If String.IsNullOrWhiteSpace(normalizedPlate) Then
            MessageBox.Show("Please enter a plate number.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        ElseIf Not DatabaseModule.IsValidPlate(normalizedPlate) Then
            MessageBox.Show("Plate number must be between 4 and 8 alphanumeric characters!", "Invalid Plate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        ElseIf String.IsNullOrWhiteSpace(txtName.Text) Then
            MessageBox.Show("Please enter the customer name.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        Else
            Me.PlateNumber = normalizedPlate
            Me.ReserverName = txtName.Text.Trim()
            Me.ReservationDate = dtpDate.Value.Date
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub
End Class
