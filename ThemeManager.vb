Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Module ThemeManager
    Public ReadOnly HeaderBg As Color = Color.FromArgb(30, 46, 90)
    Public ReadOnly TealAccent As Color = Color.FromArgb(0, 168, 181)
    Public ReadOnly RedExit As Color = Color.FromArgb(194, 59, 59)
    Public ReadOnly GreenSuccess As Color = Color.FromArgb(46, 158, 89)
    Public ReadOnly OrangeWarning As Color = Color.FromArgb(196, 94, 53)

    Public ReadOnly FormBg As Color = Color.White
    Public ReadOnly CardBg As Color = Color.FromArgb(245, 247, 250)
    Public ReadOnly TextNavy As Color = Color.FromArgb(30, 46, 90)
    Public ReadOnly TextDark As Color = Color.FromArgb(33, 37, 41)
    Public ReadOnly TextGray As Color = Color.FromArgb(120, 130, 140)

    Private styledButtons As New HashSet(Of Control)
    Private styledPanels As New HashSet(Of Control)
    Private styledDragControls As New HashSet(Of Control)

    Public Sub StyleButton(btn As Button, Optional customColor As Nullable(Of Color) = Nothing)
        If styledButtons.Contains(btn) Then Return
        styledButtons.Add(btn)

        Dim normalColor As Color = If(customColor.HasValue, customColor.Value, TealAccent)

        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.BackColor = normalColor
        btn.ForeColor = Color.White
        btn.Font = New Font("Segoe UI", 9.5!, FontStyle.Bold)
        btn.Cursor = Cursors.Hand

        AddHandler btn.MouseEnter, Sub(sender, e)
                                       If normalColor = TealAccent Then
                                           btn.BackColor = Color.FromArgb(0, 190, 205)
                                       ElseIf normalColor = RedExit Then
                                           btn.BackColor = Color.FromArgb(220, 80, 80)
                                       Else
                                           btn.BackColor = Color.FromArgb(
                                               Math.Min(normalColor.R + 20, 255),
                                               Math.Min(normalColor.G + 20, 255),
                                               Math.Min(normalColor.B + 20, 255)
                                           )
                                       End If
                                   End Sub
        AddHandler btn.MouseLeave, Sub(sender, e)
                                       btn.BackColor = normalColor
                                   End Sub
    End Sub

    Public Sub StyleCardPanel(pnl As Panel, Optional accentColor As Nullable(Of Color) = Nothing)
        If styledPanels.Contains(pnl) Then Return
        styledPanels.Add(pnl)

        pnl.BackColor = Color.FromArgb(241, 245, 249)
        pnl.BorderStyle = BorderStyle.None

        AddHandler pnl.Paint, Sub(sender, e)
                                  Dim g As Graphics = e.Graphics
                                  g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                                  Dim radius As Integer = 8
                                  Dim gp As New Drawing2D.GraphicsPath()
                                  gp.AddArc(0, 0, radius, radius, 180, 90)
                                  gp.AddArc(pnl.Width - radius - 1, 0, radius, radius, 270, 90)
                                  gp.AddArc(pnl.Width - radius - 1, pnl.Height - radius - 1, radius, radius, 0, 90)
                                  gp.AddArc(0, pnl.Height - radius - 1, radius, radius, 90, 90)
                                  gp.CloseFigure()

                                  Using brush As New SolidBrush(Color.White)
                                      g.FillPath(brush, gp)
                                  End Using

                                  Using pen As New Pen(Color.FromArgb(220, 225, 230), 1)
                                      g.DrawPath(pen, gp)
                                  End Using
                              End Sub
    End Sub

    Public Sub MakeRoundedControl(ctrl As Control, radius As Integer)
        If ctrl.Width <= 0 OrElse ctrl.Height <= 0 Then Exit Sub
        Dim gp As New GraphicsPath()
        gp.StartFigure()
        gp.AddArc(New Rectangle(0, 0, radius, radius), 180, 90)
        gp.AddArc(New Rectangle(ctrl.Width - radius, 0, radius, radius), 270, 90)
        gp.AddArc(New Rectangle(ctrl.Width - radius, ctrl.Height - radius, radius, radius), 0, 90)
        gp.AddArc(New Rectangle(0, ctrl.Height - radius, radius, radius), 90, 90)
        gp.CloseFigure()
        ctrl.Region = New Region(gp)
        gp.Dispose()
    End Sub

    Public Sub EnableDrag(headerControl As Control, frm As Form)
        If styledDragControls.Contains(headerControl) Then Return
        styledDragControls.Add(headerControl)

        Dim dragging As Boolean = False
        Dim dragOffset As Point = Point.Empty

        AddHandler headerControl.MouseDown, Sub(sender, e)
                                                If e.Button = MouseButtons.Left Then
                                                    dragging = True
                                                    dragOffset = New Point(e.X, e.Y)
                                                End If
                                            End Sub

        AddHandler headerControl.MouseMove, Sub(sender, e)
                                                If dragging Then
                                                    Dim mousePos = Control.MousePosition
                                                    frm.Location = New Point(mousePos.X - dragOffset.X, mousePos.Y - dragOffset.Y)
                                                End If
                                            End Sub

        AddHandler headerControl.MouseUp, Sub(sender, e)
                                              dragging = False
                                          End Sub
    End Sub

    Public Sub AddMinimizeButton(headerPanel As Panel, parentForm As Form)
        Dim btnMin As New Button()
        btnMin.Text = "—"
        btnMin.Font = New Font("Segoe UI", 9.5!, FontStyle.Bold)
        btnMin.FlatStyle = FlatStyle.Flat
        btnMin.FlatAppearance.BorderSize = 0
        btnMin.BackColor = Color.Transparent
        btnMin.ForeColor = Color.White
        btnMin.Size = New Size(30, 26)
        btnMin.Cursor = Cursors.Hand

        Dim rightMost As Integer = headerPanel.Width
        For Each ctrl As Control In headerPanel.Controls
            If TypeOf ctrl Is Button AndAlso ctrl.Right > btnMin.Width Then
                If ctrl.Left < rightMost Then rightMost = ctrl.Left
            End If
        Next

        If rightMost = headerPanel.Width Then rightMost -= 5

        btnMin.Location = New Point(rightMost - btnMin.Width - 5, (headerPanel.Height - btnMin.Height) \ 2)
        btnMin.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        AddHandler btnMin.MouseEnter, Sub() btnMin.BackColor = Color.FromArgb(100, 255, 255, 255)
        AddHandler btnMin.MouseLeave, Sub() btnMin.BackColor = Color.Transparent

        AddHandler btnMin.Click, Sub() parentForm.WindowState = FormWindowState.Minimized

        headerPanel.Controls.Add(btnMin)
    End Sub
End Module
