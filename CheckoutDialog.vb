Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Public Class CheckoutDialog

    Private _receiptNo As String = "000001"
    Private _plateNumber As String = ""
    Private _vehicleType As String = "Car"
    Private _timeIn As String = ""
    Private _timeOut As String = ""
    Private _duration As String = ""
    Private _rate As String = "₱50.00 / hr"
    Private _subtotal As Decimal = 50.0D
    Private _discountType As String = "None"
    Private _discountPercent As Decimal = 0D
    Private _discountAmount As Decimal = 0D
    Private _penalty As Decimal = 0D
    Private _totalDue As Decimal = 50.0D
    Private _paymentMethod As String = "Cash"
    Private _amountPaid As Decimal = 50.0D
    Private _change As Decimal = 0D
    Private _cashier As String = "Admin"
    Private _parkingName As String = ""

    Private _hasDiscount As Boolean = False
    Private _dividerPositions As New List(Of Integer)()

    Public Sub New()
        InitializeComponent()
        SetupEventHandlers()
    End Sub

    ''' <summary>
    ''' Full constructor with all receipt details.
    ''' </summary>
    Public Sub New(receiptNo As String,
                   plate As String,
                   vehicleType As String,
                   timeIn As String,
                   timeOut As String,
                   totalHours As String,
                   rate As String,
                   subtotal As String,
                   discountType As String,
                   discountPercent As Decimal,
                   discountAmount As Decimal,
                   penalty As Decimal,
                   totalDue As Decimal,
                   paymentMethod As String,
                   cashTendered As Decimal,
                   change As Decimal,
                   cashier As String,
                   Optional parkingName As String = "")

        InitializeComponent()

        _receiptNo = If(String.IsNullOrWhiteSpace(receiptNo), "000123", receiptNo)
        _plateNumber = If(plate, "").Trim()
        _vehicleType = If(String.IsNullOrWhiteSpace(vehicleType), "Car", vehicleType)
        _timeIn = FormatDateTimeString(timeIn)
        _timeOut = FormatDateTimeString(timeOut)
        _duration = If(totalHours, "")
        _rate = CleanCurrencyString(rate)

        _subtotal = ParseCurrency(subtotal)
        _discountType = If(discountType, "None")
        _discountPercent = discountPercent
        _discountAmount = discountAmount
        _penalty = penalty
        _totalDue = totalDue
        _paymentMethod = If(String.IsNullOrWhiteSpace(paymentMethod), "Cash", paymentMethod)
        _amountPaid = cashTendered
        _change = change
        _cashier = If(String.IsNullOrWhiteSpace(cashier), "Juan D.", cashier)
        _parkingName = If(Not String.IsNullOrWhiteSpace(parkingName), parkingName, ParkingData.GetParkingName())

        _hasDiscount = (Not String.IsNullOrWhiteSpace(_discountType) AndAlso _discountType <> "None" AndAlso _discountType <> "-" AndAlso _discountAmount > 0D)

        PopulateAndLayout()
        SetupEventHandlers()
    End Sub

    ''' <summary>
    ''' Backward-compatible constructor for legacy callers.
    ''' </summary>
    Public Sub New(plate As String,
                   timeIn As String,
                   timeOut As String,
                   totalHours As String,
                   baseRate As String,
                   discount As String,
                   penalty As String,
                   amountDue As String,
                   cashTendered As String,
                   change As String)

        InitializeComponent()

        _plateNumber = If(plate, "").Trim()
        _timeIn = FormatDateTimeString(timeIn)
        _timeOut = FormatDateTimeString(timeOut)
        _duration = If(totalHours, "")
        _rate = CleanCurrencyString(baseRate)
        _discountType = If(String.IsNullOrWhiteSpace(discount) OrElse discount = "-", "None", discount)

        _penalty = ParseCurrency(penalty)
        _totalDue = ParseCurrency(amountDue)
        _amountPaid = ParseCurrency(cashTendered)
        _change = ParseCurrency(change)

        ' Calculate subtotal and discount breakdown
        If _discountType = "Senior Citizen" Then
            _discountPercent = 0.2D
            If _totalDue > 0D Then
                _subtotal = Math.Round((_totalDue - _penalty) / 0.8D, 2)
                _discountAmount = Math.Round(_subtotal * _discountPercent, 2)
            Else
                _subtotal = _totalDue
                _discountAmount = 0D
            End If
        ElseIf _discountType = "PWD" Then
            _discountPercent = 1.0D
            _subtotal = _totalDue + _penalty
            _discountAmount = _subtotal
        Else
            _subtotal = _totalDue - _penalty
            _discountAmount = 0D
        End If

        _hasDiscount = (_discountAmount > 0D AndAlso _discountType <> "None")
        _receiptNo = DatabaseModule.GetNextReceiptNumber()
        _cashier = If(Not String.IsNullOrWhiteSpace(Form1.CurrentFullName), Form1.CurrentFullName, If(Not String.IsNullOrWhiteSpace(Form1.CurrentUsername), Form1.CurrentUsername, "Juan D."))
        _parkingName = ParkingData.GetParkingName()

        PopulateAndLayout()
        SetupEventHandlers()
    End Sub

    ''' <summary>
    ''' Safely parses currency values from formatted strings (e.g. "₱50.00", "50.00", "₱1,200.00").
    ''' Handles DBNull and Nothing safely without throwing exceptions.
    ''' </summary>
    Public Shared Function ParseCurrency(raw As String) As Decimal
        If String.IsNullOrWhiteSpace(raw) Then Return 0D
        Dim cleaned As String = raw.Replace("₱", "").Replace("â,±", "").Replace("â‚±", "").Replace("Â", "").Replace(",", "").Trim()
        Dim result As Decimal = 0D
        If Decimal.TryParse(cleaned, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, result) Then
            Return result
        End If
        Return 0D
    End Function

    Public Shared Function CleanCurrencyString(s As String) As String
        If String.IsNullOrWhiteSpace(s) Then Return "₱0.00"
        s = s.Replace("â,±", "₱").Replace("â‚±", "₱").Replace("Â₱", "₱")
        Return s
    End Function

    Private Shared Function FormatDateTimeString(rawStr As String) As String
        If String.IsNullOrWhiteSpace(rawStr) Then
            Dim now As DateTime = DateTime.Now
            Return now.ToString("MMM dd, yyyy  hh:mm ", Globalization.CultureInfo.InvariantCulture) & now.ToString("tt", Globalization.CultureInfo.InvariantCulture).ToUpper()
        End If

        Dim dt As DateTime
        If DateTime.TryParse(rawStr, dt) Then
            Return dt.ToString("MMM dd, yyyy  hh:mm ", Globalization.CultureInfo.InvariantCulture) & dt.ToString("tt", Globalization.CultureInfo.InvariantCulture).ToUpper()
        End If

        Return rawStr
    End Function

    Private Sub SetupEventHandlers()
        AddHandler Me.Load, AddressOf CheckoutDialog_Load
        AddHandler Me.Resize, Sub(s, ev) ThemeManager.MakeRoundedControl(Me, 10)
        AddHandler pnlIcon.Paint, AddressOf pnlIcon_Paint
        AddHandler pnlDetails.Paint, AddressOf pnlDetails_Paint
        AddHandler btnSaveReceipt.Click, AddressOf SaveReceiptButton_Click
        AddHandler btnClose.Click, AddressOf btnClose_Click
        ThemeManager.EnableDrag(HeaderPanel, Me)
    End Sub

    Private Sub CheckoutDialog_Load(sender As Object, e As EventArgs)
        PopulateAndLayout()
        ThemeManager.MakeRoundedControl(Me, 10)
        ThemeManager.MakeRoundedControl(btnClose, 6)
        ThemeManager.MakeRoundedControl(btnSaveReceipt, 6)
    End Sub

    Private Sub PopulateAndLayout()
        ' Set Parking Name subtitle
        Dim displayParkingName = If(Not String.IsNullOrWhiteSpace(_parkingName), _parkingName, ParkingData.GetParkingName())
        lblParkingName.Text = displayParkingName

        ' Group 1: Receipt No., Plate Number, Vehicle Type
        lblReceiptNoValue.Text = _receiptNo
        lblPlateNumberValue.Text = _plateNumber
        lblVehicleTypeValue.Text = _vehicleType

        ' Group 2: Time In, Time Out, Total Hours
        lblTimeInValue.Text = _timeIn
        lblTimeOutValue.Text = _timeOut
        lblTotalHoursValue.Text = _duration

        ' Group 3: Rate, Subtotal, Discount (with percentage), Discount Type, Penalty
        lblRateValue.Text = _rate
        lblSubtotalValue.Text = "₱" & _subtotal.ToString("F2")

        If _hasDiscount Then
            Dim pctStr As String = If(_discountPercent > 0D, $" ({CInt(_discountPercent * 100D)}%)", "")
            lblDiscount.Text = "Discount" & pctStr
            lblDiscountValue.Text = "-₱" & _discountAmount.ToString("F2")
            lblDiscountType.Text = "Discount Type"
            lblDiscountTypeValue.Text = _discountType

            lblDiscount.Visible = True
            lblDiscountValue.Visible = True
            lblDiscountType.Visible = True
            lblDiscountTypeValue.Visible = True
        Else
            lblDiscount.Visible = False
            lblDiscountValue.Visible = False
            lblDiscountType.Visible = False
            lblDiscountTypeValue.Visible = False
        End If

        ' Penalty: Always show ₱0.00 when there is no penalty
        lblPenaltyValue.Text = "₱" & _penalty.ToString("F2")

        ' Group 4: Amount Due (bold), Amount Paid (with payment method), Change (bold)
        lblAmountDueValue.Text = "₱" & _totalDue.ToString("F2")
        lblAmountPaid.Text = $"Amount Paid ({_paymentMethod}):"
        lblAmountPaidValue.Text = "₱" & _amountPaid.ToString("F2")
        lblChangeValue.Text = "₱" & _change.ToString("F2")

        ' Group 5: Cashier
        lblCashierValue.Text = _cashier

        ' Arrange layout dynamically
        ArrangeRows()
    End Sub

    Private Sub ArrangeRows()
        Dim curY As Integer = 12
        Dim rowH As Integer = 20
        Dim rowSpacing As Integer = 2
        Dim dividerSpacing As Integer = 10

        _dividerPositions.Clear()

        ' --- Group 1 ---
        lblReceiptNo.Top = curY
        lblReceiptNoValue.Top = curY
        curY += rowH + rowSpacing

        lblPlateNumber.Top = curY
        lblPlateNumberValue.Top = curY
        curY += rowH + rowSpacing

        lblVehicleType.Top = curY
        lblVehicleTypeValue.Top = curY
        curY += rowH + (dividerSpacing \ 2)

        ' Divider 1
        _dividerPositions.Add(curY)
        curY += (dividerSpacing \ 2) + 4

        ' --- Group 2 ---
        lblTimeIn.Top = curY
        lblTimeInValue.Top = curY
        curY += rowH + rowSpacing

        lblTimeOut.Top = curY
        lblTimeOutValue.Top = curY
        curY += rowH + rowSpacing

        lblTotalHours.Top = curY
        lblTotalHoursValue.Top = curY
        curY += rowH + (dividerSpacing \ 2)

        ' Divider 2
        _dividerPositions.Add(curY)
        curY += (dividerSpacing \ 2) + 4

        ' --- Group 3 ---
        lblRate.Top = curY
        lblRateValue.Top = curY
        curY += rowH + rowSpacing

        lblSubtotal.Top = curY
        lblSubtotalValue.Top = curY
        curY += rowH + rowSpacing

        If _hasDiscount Then
            lblDiscount.Top = curY
            lblDiscountValue.Top = curY
            curY += rowH + rowSpacing

            lblDiscountType.Top = curY
            lblDiscountTypeValue.Top = curY
            curY += rowH + rowSpacing
        End If

        lblPenalty.Top = curY
        lblPenaltyValue.Top = curY
        curY += rowH + (dividerSpacing \ 2)

        ' Divider 3
        _dividerPositions.Add(curY)
        curY += (dividerSpacing \ 2) + 4

        ' --- Group 4 ---
        lblAmountDue.Top = curY
        lblAmountDueValue.Top = curY
        curY += rowH + rowSpacing

        lblAmountPaid.Top = curY
        lblAmountPaidValue.Top = curY
        curY += rowH + rowSpacing

        lblChange.Top = curY
        lblChangeValue.Top = curY
        curY += rowH + (dividerSpacing \ 2)

        ' Divider 4
        _dividerPositions.Add(curY)
        curY += (dividerSpacing \ 2) + 4

        ' --- Group 5 ---
        lblCashier.Top = curY
        lblCashierValue.Top = curY
        curY += rowH + 12

        ' Set details panel height
        pnlDetails.Height = curY

        ' Position buttons and footer
        Dim btnY As Integer = pnlDetails.Bottom + 16
        btnSaveReceipt.Top = btnY
        btnClose.Top = btnY

        lblFooterText.Top = btnSaveReceipt.Bottom + 12

        ' Adjust form height to fit content cleanly
        Me.ClientSize = New Size(Me.ClientSize.Width, lblFooterText.Bottom + 18)
        ThemeManager.MakeRoundedControl(Me, 10)

        pnlDetails.Invalidate()
        pnlIcon.Invalidate()
    End Sub

    ''' <summary>
    ''' Draws the crisp inline vector checkmark (white on dark navy rounded square).
    ''' Does not use an emoji or icon font.
    ''' </summary>
    Private Sub pnlIcon_Paint(sender As Object, e As PaintEventArgs)
        DrawCheckmark(e.Graphics, pnlIcon.ClientRectangle)
    End Sub

    Public Shared Sub DrawCheckmark(g As Graphics, rect As Rectangle)
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.PixelOffsetMode = PixelOffsetMode.HighQuality

        Dim scaleX As Single = rect.Width / 50.0F
        Dim scaleY As Single = rect.Height / 50.0F

        Dim p1 As New PointF(rect.X + 15.0F * scaleX, rect.Y + 26.0F * scaleY)
        Dim p2 As New PointF(rect.X + 22.0F * scaleX, rect.Y + 33.0F * scaleY)
        Dim p3 As New PointF(rect.X + 35.0F * scaleX, rect.Y + 18.0F * scaleY)

        Using pen As New Pen(Color.White, 3.8F * Math.Min(scaleX, scaleY))
            pen.StartCap = LineCap.Round
            pen.EndCap = LineCap.Round
            pen.LineJoin = LineJoin.Round
            g.DrawLines(pen, New PointF() {p1, p2, p3})
        End Using
    End Sub

    ''' <summary>
    ''' Draws the dashed divider lines separating the groups in the details panel.
    ''' </summary>
    Private Sub pnlDetails_Paint(sender As Object, e As PaintEventArgs)
        Using pen As New Pen(Color.FromArgb(220, 225, 230), 1.0F)
            pen.DashStyle = DashStyle.Dash
            pen.DashPattern = New Single() {3.0F, 3.0F}
            Dim leftX As Integer = 12
            Dim rightX As Integer = pnlDetails.ClientSize.Width - 12
            For Each y In _dividerPositions
                e.Graphics.DrawLine(pen, leftX, y, rightX, y)
            Next
        End Using
    End Sub

    ''' <summary>
    ''' Event handler to close the modal.
    ''' </summary>
    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' Renders the complete receipt layout as a high-resolution Bitmap.
    ''' </summary>
    Public Function RenderReceiptBitmap(Optional scale As Single = 2.0F) As Bitmap
        Dim width As Integer = CInt(Me.ClientSize.Width * scale)
        Dim height As Integer = CInt(Me.ClientSize.Height * scale)
        Dim bmp As New Bitmap(width, height)

        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.Clear(Color.White)

            ' 1. Navy Header
            Dim headerH As Single = HeaderPanel.Height * scale
            Using headerBrush As New SolidBrush(HeaderPanel.BackColor)
                g.FillRectangle(headerBrush, 0, 0, width, headerH)
            End Using
            Using titleFont As New Font("Segoe UI", 11.0F * scale, FontStyle.Bold, GraphicsUnit.Pixel)
                Using textBrush As New SolidBrush(Color.White)
                    g.DrawString(TitleLabel.Text, titleFont, textBrush, 15.0F * scale, 10.0F * scale)
                End Using
            End Using

            ' 2. Navy Rounded Square Icon with Vector Checkmark
            Dim iconRect As New Rectangle(CInt(pnlIcon.Left * scale), CInt(pnlIcon.Top * scale), CInt(pnlIcon.Width * scale), CInt(pnlIcon.Height * scale))
            Dim iconRadius As Single = 14.0F * scale
            Using iconBrush As New SolidBrush(pnlIcon.BackColor)
                Using path As GraphicsPath = GetRoundedPath(iconRect, iconRadius)
                    g.FillPath(iconBrush, path)
                End Using
            End Using
            DrawCheckmark(g, iconRect)

            ' 3. Checkout Confirmed Title
            Using titleFont As New Font("Segoe UI", 12.0F * scale, FontStyle.Bold, GraphicsUnit.Pixel)
                Using textBrush As New SolidBrush(Color.FromArgb(27, 42, 71))
                    Dim sf As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                    Dim titleRect As New RectangleF(0, lblCheckoutConfirmed.Top * scale, width, lblCheckoutConfirmed.Height * scale)
                    g.DrawString(lblCheckoutConfirmed.Text, titleFont, textBrush, titleRect, sf)
                End Using
            End Using

            ' 4. Parking Name Subtitle
            Using subFont As New Font("Segoe UI", 9.0F * scale, FontStyle.Regular, GraphicsUnit.Pixel)
                Using textBrush As New SolidBrush(Color.FromArgb(120, 128, 140))
                    Dim sf As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                    Dim subRect As New RectangleF(0, lblParkingName.Top * scale, width, lblParkingName.Height * scale)
                    g.DrawString(lblParkingName.Text, subFont, textBrush, subRect, sf)
                End Using
            End Using

            ' 5. Details Panel (Rounded Light-Gray Container)
            Dim panelRect As New Rectangle(CInt(pnlDetails.Left * scale), CInt(pnlDetails.Top * scale), CInt(pnlDetails.Width * scale), CInt(pnlDetails.Height * scale))
            Dim panelRadius As Single = 10.0F * scale
            Using panelBrush As New SolidBrush(pnlDetails.BackColor)
                Using borderPen As New Pen(Color.FromArgb(220, 225, 230), 1.0F * scale)
                    Using path As GraphicsPath = GetRoundedPath(panelRect, panelRadius)
                        g.FillPath(panelBrush, path)
                        g.DrawPath(borderPen, path)
                    End Using
                End Using
            End Using

            ' Draw Dashed Dividers inside Details Panel
            Using dashPen As New Pen(Color.FromArgb(220, 225, 230), 1.0F * scale)
                dashPen.DashStyle = DashStyle.Dash
                dashPen.DashPattern = New Single() {3.0F, 3.0F}
                Dim leftX As Single = (pnlDetails.Left + 12) * scale
                Dim rightX As Single = (pnlDetails.Right - 12) * scale
                For Each divY In _dividerPositions
                    Dim py As Single = (pnlDetails.Top + divY) * scale
                    g.DrawLine(dashPen, leftX, py, rightX, py)
                Next
            End Using

            ' Render all text labels inside Details Panel
            Dim sfLeft As New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center}
            Dim sfRight As New StringFormat() With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center}

            For Each ctrl As Control In pnlDetails.Controls
                If TypeOf ctrl Is Label Then
                    Dim lbl As Label = DirectCast(ctrl, Label)
                    If (lbl Is lblDiscount OrElse lbl Is lblDiscountValue OrElse lbl Is lblDiscountType OrElse lbl Is lblDiscountTypeValue) AndAlso Not _hasDiscount Then
                        Continue For
                    End If
                    Dim rx As Single = (pnlDetails.Left + lbl.Left) * scale
                    Dim ry As Single = (pnlDetails.Top + lbl.Top) * scale
                    Dim rw As Single = lbl.Width * scale
                    Dim rh As Single = lbl.Height * scale
                    Dim destRect As New RectangleF(rx, ry, rw, rh)

                    Using f As New Font(lbl.Font.FontFamily, lbl.Font.Size * scale, lbl.Font.Style, GraphicsUnit.Pixel)
                        Using b As New SolidBrush(lbl.ForeColor)
                            Dim sf = If(lbl.TextAlign = ContentAlignment.MiddleRight, sfRight, sfLeft)
                            g.DrawString(lbl.Text, f, b, destRect, sf)
                        End Using
                    End Using
                End If
            Next

            ' 6. Buttons
            RenderButtonBitmap(g, btnSaveReceipt, scale)
            RenderButtonBitmap(g, btnClose, scale)

            ' 7. Footer text
            Using footerFont As New Font("Segoe UI", 8.5F * scale, FontStyle.Regular, GraphicsUnit.Pixel)
                Using textBrush As New SolidBrush(Color.FromArgb(120, 128, 140))
                    Dim sf As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                    Dim footerRect As New RectangleF(0, lblFooterText.Top * scale, width, lblFooterText.Height * scale)
                    g.DrawString(lblFooterText.Text, footerFont, textBrush, footerRect, sf)
                End Using
            End Using
        End Using

        Return bmp
    End Function

    Private Shared Sub RenderButtonBitmap(g As Graphics, btn As Button, scale As Single)
        Dim rect As New Rectangle(CInt(btn.Left * scale), CInt(btn.Top * scale), CInt(btn.Width * scale), CInt(btn.Height * scale))
        Dim radius As Single = 6.0F * scale
        Using brush As New SolidBrush(btn.BackColor)
            Using path As GraphicsPath = GetRoundedPath(rect, radius)
                g.FillPath(brush, path)
            End Using
        End Using
        Using f As New Font(btn.Font.FontFamily, btn.Font.Size * scale, btn.Font.Style, GraphicsUnit.Pixel)
            Using b As New SolidBrush(btn.ForeColor)
                Dim sf As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                g.DrawString(btn.Text, f, b, New RectangleF(rect.X, rect.Y, rect.Width, rect.Height), sf)
            End Using
        End Using
    End Sub

    Private Shared Function GetRoundedPath(rect As Rectangle, radius As Single) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Single = radius * 2.0F
        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    ''' <summary>
    ''' Saves the receipt as an Image (PNG/JPG) or PDF with the exact layout as the modal.
    ''' </summary>
    Private Sub SaveReceiptButton_Click(sender As Object, e As EventArgs)
        Using sfd As New SaveFileDialog()
            sfd.Filter = "PNG Image (*.png)|*.png|PDF Document (*.pdf)|*.pdf|JPEG Image (*.jpg)|*.jpg"
            sfd.FilterIndex = 1
            Dim safePlate = If(String.IsNullOrWhiteSpace(_plateNumber), "Receipt", _plateNumber.Trim())
            For Each c As Char In Path.GetInvalidFileNameChars()
                safePlate = safePlate.Replace(c, "_"c)
            Next
            sfd.FileName = $"Receipt_{_receiptNo}_{safePlate}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.png"

            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim ext = Path.GetExtension(sfd.FileName).ToLowerInvariant()
                    Using bmp As Bitmap = RenderReceiptBitmap(2.0F)
                        If ext = ".pdf" Then
                            SaveAsPdf(sfd.FileName, bmp)
                        ElseIf ext = ".jpg" OrElse ext = ".jpeg" Then
                            bmp.Save(sfd.FileName, ImageFormat.Jpeg)
                        Else
                            bmp.Save(sfd.FileName, ImageFormat.Png)
                        End If
                    End Using

                    MessageBox.Show($"Receipt saved successfully to:{Environment.NewLine}{sfd.FileName}",
                                    "Receipt Saved",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Failed to save receipt file: " & ex.Message, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Writes a valid, standard PDF 1.4 document embedding the receipt image.
    ''' Requires no third-party libraries.
    ''' </summary>
    Public Shared Sub SaveAsPdf(pdfPath As String, bmp As Bitmap)
        Dim jpegBytes As Byte()
        Using ms As New MemoryStream()
            ' Save image as JPEG for PDF stream embedding
            Dim jgpEncoder As ImageCodecInfo = GetEncoder(ImageFormat.Jpeg)
            Dim myEncoderParameters As New EncoderParameters(1)
            myEncoderParameters.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L)
            bmp.Save(ms, jgpEncoder, myEncoderParameters)
            jpegBytes = ms.ToArray()
        End Using

        ' Calculate page size in PDF points (72 points per inch)
        ' bmp is rendered at 2.0 scale (approx 192 DPI), so points = pixels * 72 / 192
        Dim ptWidth As Single = CSng(Math.Round(bmp.Width * 72.0F / 192.0F, 2))
        Dim ptHeight As Single = CSng(Math.Round(bmp.Height * 72.0F / 192.0F, 2))

        Dim contentStreamText As String = $"q{vbLf}{ptWidth:0.00} 0 0 {ptHeight:0.00} 0 0 cm{vbLf}/Im1 Do{vbLf}Q{vbLf}"
        Dim contentBytes As Byte() = Encoding.ASCII.GetBytes(contentStreamText)

        Using fs As New FileStream(pdfPath, FileMode.Create, FileAccess.Write)
            Using writer As New BinaryWriter(fs)
                Dim offsets As New List(Of Long)()

                ' Header
                writer.Write(Encoding.ASCII.GetBytes("%PDF-1.4" & vbLf))

                ' Obj 1: Catalog
                offsets.Add(fs.Position)
                writer.Write(Encoding.ASCII.GetBytes("1 0 obj" & vbLf & "<< /Type /Catalog /Pages 2 0 R >>" & vbLf & "endobj" & vbLf))

                ' Obj 2: Pages
                offsets.Add(fs.Position)
                writer.Write(Encoding.ASCII.GetBytes("2 0 obj" & vbLf & "<< /Type /Pages /Kids [3 0 R] /Count 1 >>" & vbLf & "endobj" & vbLf))

                ' Obj 3: Page
                offsets.Add(fs.Position)
                Dim pageObjStr As String = $"3 0 obj{vbLf}<< /Type /Page /Parent 2 0 R /MediaBox [0 0 {ptWidth:0.00} {ptHeight:0.00}] /Resources << /XObject << /Im1 4 0 R >> >> /Contents 5 0 R >>{vbLf}endobj{vbLf}"
                writer.Write(Encoding.ASCII.GetBytes(pageObjStr))

                ' Obj 4: XObject Image
                offsets.Add(fs.Position)
                Dim imgObjHeader As String = $"4 0 obj{vbLf}<< /Type /XObject /Subtype /Image /Width {bmp.Width} /Height {bmp.Height} /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length {jpegBytes.Length} >>{vbLf}stream{vbLf}"
                writer.Write(Encoding.ASCII.GetBytes(imgObjHeader))
                writer.Write(jpegBytes)
                writer.Write(Encoding.ASCII.GetBytes(vbLf & "endstream" & vbLf & "endobj" & vbLf))

                ' Obj 5: Contents
                offsets.Add(fs.Position)
                Dim contentObjHeader As String = $"5 0 obj{vbLf}<< /Length {contentBytes.Length} >>{vbLf}stream{vbLf}"
                writer.Write(Encoding.ASCII.GetBytes(contentObjHeader))
                writer.Write(contentBytes)
                writer.Write(Encoding.ASCII.GetBytes("endstream" & vbLf & "endobj" & vbLf))

                ' Cross-reference table (strict 20-byte standard: 10 digits + space + 5 digits + space + 'f'/'n' + CRLF)
                Dim xrefPos As Long = fs.Position
                writer.Write(Encoding.ASCII.GetBytes("xref" & vbLf))
                writer.Write(Encoding.ASCII.GetBytes($"0 {offsets.Count + 1}{vbLf}"))
                writer.Write(Encoding.ASCII.GetBytes("0000000000 65535 f " & vbCrLf))
                For Each off In offsets
                    writer.Write(Encoding.ASCII.GetBytes(off.ToString("D10") & " 00000 n " & vbCrLf))
                Next

                ' Trailer
                writer.Write(Encoding.ASCII.GetBytes("trailer" & vbLf))
                writer.Write(Encoding.ASCII.GetBytes($"<< /Size {offsets.Count + 1} /Root 1 0 R >>{vbLf}"))
                writer.Write(Encoding.ASCII.GetBytes("startxref" & vbLf))
                writer.Write(Encoding.ASCII.GetBytes(xrefPos.ToString() & vbLf))
                writer.Write(Encoding.ASCII.GetBytes("%%EOF" & vbLf))
            End Using
        End Using
    End Sub

    Private Shared Function GetEncoder(format As ImageFormat) As ImageCodecInfo
        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders()
        For Each codec In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next
        Return Nothing
    End Function

    ' Dummy handler for legacy event wireups if needed
    Private Sub lblIcon_Click(sender As Object, e As EventArgs)
    End Sub

End Class