$files = @("DashBoardForm.vb", "frmadmindashboard.vb")
foreach ($f in $files) {
    if (Test-Path $f) {
        $content = Get-Content $f -Raw
        
        # We find instances of ParkingData.SyncParkingFromDatabase() that occur BEFORE the MySQL block
        # and move them AFTER the MySQL block.

        $pattern1 = "(?s)ParkingData\.SyncParkingFromDatabase\(\)\r?\n(\s*If ParkingData\.parkingDatabase\.ContainsKey.*?End If\r?\n\r?\n\s*'.*?MySQL.*?\r?\n\s*If .*?<> `"`" Then\r?\n\s*Try\r?\n.*?End Try\r?\n\s*End If)"
        $replacement1 = "`$1`r`n                                                        ParkingData.SyncParkingFromDatabase()"
        $content = [regex]::Replace($content, $pattern1, $replacement1)
        
        Set-Content -Path $f -Value $content -NoNewline
    }
}
