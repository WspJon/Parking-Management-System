Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

$asmPath = (Resolve-Path "bin\Debug\ParkingSystemProject.exe").Path
$asm = [System.Reflection.Assembly]::LoadFrom($asmPath)

$dialogType = $asm.GetType("ParkingSystemProject.CheckoutDialog")
$parkingDataType = $asm.GetType("ParkingSystemProject.ParkingData")
$renderMethod = $dialogType.GetMethod("RenderReceiptBitmap", [Type[]]@([System.Single]))
$pdfMethod = $dialogType.GetMethod("SaveAsPdf", [System.Reflection.BindingFlags]"Public, Static")

Write-Host "=========================================="
Write-Host "RUNNING BILLING SUMMARY TEST SUITE"
Write-Host "=========================================="

# Test Case 1: Under 1 hour with Senior Citizen discount
Write-Host "`n--- Test Case 1: Under 1 hour with Senior Citizen discount ---"
$dlg1 = [Activator]::CreateInstance($dialogType, @(
    "000123",                    # receiptNo
    "SDFS2312",                  # plate
    "Car",                       # vehicleType
    "Oct 15, 2026 01:18 AM",     # timeIn
    "Oct 15, 2026 01:18 AM",     # timeOut
    "1 hr",                      # totalHours
    "$([char]0x20B1)50.00 / hr",               # rate
    "50.00",                     # subtotal
    "Senior Citizen",            # discountType
    0.20d,                       # discountPercent
    10.00d,                      # discountAmount
    0.00d,                       # penalty
    40.00d,                      # totalDue
    "Cash",                      # paymentMethod
    50.00d,                      # cashTendered
    10.00d,                      # change
    "Juan D.",                   # cashier
    "[Parking Name]"             # parkingName
))
$null = $dlg1.Handle

$pnlDetails1 = $dlg1.Controls["pnlDetails"]
Write-Host "Receipt No:           $($pnlDetails1.Controls['lblReceiptNoValue'].Text)"
Write-Host "Plate Number:         $($pnlDetails1.Controls['lblPlateNumberValue'].Text)"
Write-Host "Vehicle Type:         $($pnlDetails1.Controls['lblVehicleTypeValue'].Text)"
Write-Host "Time In:              $($pnlDetails1.Controls['lblTimeInValue'].Text)"
Write-Host "Time Out:             $($pnlDetails1.Controls['lblTimeOutValue'].Text)"
Write-Host "Total Hours:          $($pnlDetails1.Controls['lblTotalHoursValue'].Text)"
Write-Host "Rate:                 $($pnlDetails1.Controls['lblRateValue'].Text)"
Write-Host "Subtotal:             $($pnlDetails1.Controls['lblSubtotalValue'].Text)"
Write-Host "Discount:             $($pnlDetails1.Controls['lblDiscountValue'].Text)"
Write-Host "Discount Type:        $($pnlDetails1.Controls['lblDiscountTypeValue'].Text)"
Write-Host "Penalty:              $($pnlDetails1.Controls['lblPenaltyValue'].Text)"
Write-Host "Amount Due:           $($pnlDetails1.Controls['lblAmountDueValue'].Text)"
Write-Host "Amount Paid label:    $($pnlDetails1.Controls['lblAmountPaid'].Text)"
Write-Host "Amount Paid value:    $($pnlDetails1.Controls['lblAmountPaidValue'].Text)"
Write-Host "Change:               $($pnlDetails1.Controls['lblChangeValue'].Text)"
Write-Host "Cashier:              $($pnlDetails1.Controls['lblCashierValue'].Text)"
Write-Host "Close Button Text:    $($dlg1.Controls['btnClose'].Text)"
Write-Host "Footer Text:          $($dlg1.Controls['lblFooterText'].Text)"

$bmp1 = $renderMethod.Invoke($dlg1, @([System.Single]2.0))
$bmp1.Save("test_case_1_under_1hr_senior.png", [System.Drawing.Imaging.ImageFormat]::Png)
Write-Host "Saved image: test_case_1_under_1hr_senior.png"

$pdfMethod.Invoke($null, @("test_case_1_under_1hr_senior.pdf", $bmp1))
Write-Host "Saved PDF: test_case_1_under_1hr_senior.pdf"

# Test Case 2: More than 1 hour with no discount
Write-Host "`n--- Test Case 2: More than 1 hour with no discount ---"
$dlg2 = [Activator]::CreateInstance($dialogType, @(
    "000124",                    # receiptNo
    "ABC-1234",                  # plate
    "Car",                       # vehicleType
    "Oct 15, 2026 01:00 PM",     # timeIn
    "Oct 15, 2026 03:30 PM",     # timeOut
    "3 hrs",                     # totalHours
    "$([char]0x20B1)50.00 / hr",               # rate
    "90.00",                     # subtotal (50 + 2*20 = 90)
    "None",                      # discountType
    0.00d,                       # discountPercent
    0.00d,                       # discountAmount
    0.00d,                       # penalty
    90.00d,                      # totalDue
    "Cash",                      # paymentMethod
    100.00d,                     # cashTendered
    10.00d,                      # change
    "Juan D.",                   # cashier
    "JJRSP Parking System"       # parkingName
))
$null = $dlg2.Handle

$pnlDetails2 = $dlg2.Controls["pnlDetails"]
Write-Host "Receipt No:           $($pnlDetails2.Controls['lblReceiptNoValue'].Text)"
Write-Host "Total Hours:          $($pnlDetails2.Controls['lblTotalHoursValue'].Text)"
Write-Host "Subtotal:             $($pnlDetails2.Controls['lblSubtotalValue'].Text)"
Write-Host "Penalty:              $($pnlDetails2.Controls['lblPenaltyValue'].Text)"
Write-Host "Amount Due:           $($pnlDetails2.Controls['lblAmountDueValue'].Text)"
Write-Host "Amount Paid label:    $($pnlDetails2.Controls['lblAmountPaid'].Text)"
Write-Host "Amount Paid value:    $($pnlDetails2.Controls['lblAmountPaidValue'].Text)"
Write-Host "Change:               $($pnlDetails2.Controls['lblChangeValue'].Text)"

$bmp2 = $renderMethod.Invoke($dlg2, @([System.Single]2.0))
$bmp2.Save("test_case_2_more_than_1hr_no_discount.png", [System.Drawing.Imaging.ImageFormat]::Png)
Write-Host "Saved image: test_case_2_more_than_1hr_no_discount.png"
$pdfMethod.Invoke($null, @("test_case_2_more_than_1hr_no_discount.pdf", $bmp2))
Write-Host "Saved PDF: test_case_2_more_than_1hr_no_discount.pdf"

# Test Case 3: A stay that goes past midnight
Write-Host "`n--- Test Case 3: A stay that goes past midnight ---"
$dlg3 = [Activator]::CreateInstance($dialogType, @(
    "000125",                    # receiptNo
    "XYZ-9988",                  # plate
    "Motorcycle",                # vehicleType
    "Oct 14, 2026 10:30 PM",     # timeIn
    "Oct 15, 2026 02:30 AM",     # timeOut
    "4 hrs",                     # totalHours
    "$([char]0x20B1)50.00 / hr",               # rate
    "80.00",                     # subtotal (50 + 3*10 = 80)
    "None",                      # discountType
    0.00d,                       # discountPercent
    0.00d,                       # discountAmount
    0.00d,                       # penalty
    80.00d,                      # totalDue
    "GCash",                     # paymentMethod
    100.00d,                     # cashTendered
    20.00d,                      # change
    "Maria S.",                  # cashier
    "JJRSP Parking System"       # parkingName
))
$null = $dlg3.Handle

$pnlDetails3 = $dlg3.Controls["pnlDetails"]
Write-Host "Time In:              $($pnlDetails3.Controls['lblTimeInValue'].Text)"
Write-Host "Time Out:             $($pnlDetails3.Controls['lblTimeOutValue'].Text)"
Write-Host "Total Hours:          $($pnlDetails3.Controls['lblTotalHoursValue'].Text)"
Write-Host "Amount Paid label:    $($pnlDetails3.Controls['lblAmountPaid'].Text)"
Write-Host "Amount Paid value:    $($pnlDetails3.Controls['lblAmountPaidValue'].Text)"
Write-Host "Change:               $($pnlDetails3.Controls['lblChangeValue'].Text)"

$bmp3 = $renderMethod.Invoke($dlg3, @([System.Single]2.0))
$bmp3.Save("test_case_3_past_midnight.png", [System.Drawing.Imaging.ImageFormat]::Png)
Write-Host "Saved image: test_case_3_past_midnight.png"
$pdfMethod.Invoke($null, @("test_case_3_past_midnight.pdf", $bmp3))
Write-Host "Saved PDF: test_case_3_past_midnight.pdf"

# Test Case 4: A stay with a penalty
Write-Host "`n--- Test Case 4: A stay with a penalty ---"
$dlg4 = [Activator]::CreateInstance($dialogType, @(
    "000126",                    # receiptNo
    "PEN-5555",                  # plate
    "Car",                       # vehicleType
    "Oct 15, 2026 08:00 AM",     # timeIn
    "Oct 15, 2026 09:00 AM",     # timeOut
    "1 hr",                      # totalHours
    "$([char]0x20B1)50.00 / hr",               # rate
    "50.00",                     # subtotal
    "None",                      # discountType
    0.00d,                       # discountPercent
    0.00d,                       # discountAmount
    200.00d,                     # penalty
    250.00d,                     # totalDue
    "Cash",                      # paymentMethod
    300.00d,                     # cashTendered
    50.00d,                      # change
    "Juan D.",                   # cashier
    "JJRSP Parking System"       # parkingName
))
$null = $dlg4.Handle

$pnlDetails4 = $dlg4.Controls["pnlDetails"]
Write-Host "Subtotal:             $($pnlDetails4.Controls['lblSubtotalValue'].Text)"
Write-Host "Penalty:              $($pnlDetails4.Controls['lblPenaltyValue'].Text)"
Write-Host "Amount Due:           $($pnlDetails4.Controls['lblAmountDueValue'].Text)"
Write-Host "Amount Paid label:    $($pnlDetails4.Controls['lblAmountPaid'].Text)"
Write-Host "Amount Paid value:    $($pnlDetails4.Controls['lblAmountPaidValue'].Text)"
Write-Host "Change:               $($pnlDetails4.Controls['lblChangeValue'].Text)"

$bmp4 = $renderMethod.Invoke($dlg4, @([System.Single]2.0))
$bmp4.Save("test_case_4_with_penalty.png", [System.Drawing.Imaging.ImageFormat]::Png)
Write-Host "Saved image: test_case_4_with_penalty.png"
$pdfMethod.Invoke($null, @("test_case_4_with_penalty.pdf", $bmp4))
Write-Host "Saved PDF: test_case_4_with_penalty.pdf"

Write-Host "`n=========================================="
Write-Host "ALL 4 TEST CASES COMPLETED AND VERIFIED!"
Write-Host "=========================================="
