Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class RoundedPanel
    Inherits Panel

    Public Property CornerRadius As Integer = 8
    Public Property BorderColor As Color = Color.FromArgb(220, 225, 230)
    Public Property BorderSize As Integer = 1

    Public Sub New()
        Me.BackColor = Color.FromArgb(241, 245, 249)
        Me.DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim rect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Dim path As New GraphicsPath()
        
        Dim radius As Integer = CornerRadius
        
        If radius > 0 Then
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90)
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90)
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90)
            path.CloseFigure()
        Else
            path.AddRectangle(rect)
        End If

        Me.Region = New Region(path)

        Using brush As New SolidBrush(Me.BackColor)
            g.FillPath(brush, path)
        End Using

        If BorderSize > 0 Then
            Using pen As New Pen(BorderColor, BorderSize)
                g.DrawPath(pen, path)
            End Using
        End If
    End Sub
End Class
