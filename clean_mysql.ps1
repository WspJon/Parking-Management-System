$files = @(
    "DashBoardForm.vb",
    "frmadmindashboard.vb",
    "ParkingManagerForm.vb",
    "frmaddteller.vb",
    "RegisterForm.vb",
    "Form1.vb"
)

foreach ($file in $files) {
    if (Test-Path $file) {
        $content = Get-Content $file -Raw

        # Remove Imports MySql.Data.MySqlClient
        $content = $content -replace '(?m)^Imports MySql\.Data\.MySqlClient\s*$', ''

        # Remove Try...Catch MySqlException blocks
        $content = $content -replace '(?s)\s*Try\s*Using conn As MySqlConnection = GetConnection\(\).*?Catch ex As MySqlException.*?End Try\s*', "`r`n                                                        DataStore.SaveDatabase()`r`n"
        $content = $content -replace '(?s)\s*Try\s*Using conn As New MySqlConnection.*?Catch ex As MySqlException.*?End Try\s*', "`r`n                                                        DataStore.SaveDatabase()`r`n"

        # Replace ParkingData.SyncParkingFromDatabase() with DataStore.SaveDatabase()
        $content = $content -replace '(?m)^\s*ParkingData\.SyncParkingFromDatabase\(\)\s*$', ''
        
        # Remove GetCurrentUserID helper method
        $content = $content -replace '(?s)\s*Private Function GetCurrentUserID.*?End Function\s*', "`r`n"
        
        # Remove GetConnection helper method
        $content = $content -replace '(?s)\s*Private Function GetConnection.*?End Function\s*', "`r`n"

        Set-Content -Path $file -Value $content
    }
}
Write-Host "Cleanup complete."
