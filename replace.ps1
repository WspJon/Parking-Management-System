$files = @("DashBoardForm.vb", "ParkingManagerForm.vb", "frmadmindashboard.vb")
foreach ($f in $files) {
    if (Test-Path $f) {
        $content = Get-Content $f -Raw
        $content = $content.Replace("``Paid Status``", "PaidStatus")
        $content = $content.Replace("``Parking Slot``", "Slot")
        $content = $content.Replace("``Total Amount``", "TotalAmount")
        $content = $content.Replace("DataStore.SaveDatabase()", "ParkingData.SyncParkingFromDatabase()")
        Set-Content -Path $f -Value $content -NoNewline
    }
}
