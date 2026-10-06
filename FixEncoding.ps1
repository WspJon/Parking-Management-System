$files = Get-ChildItem -Path . -Filter *.vb -Recurse
foreach ($file in $files) {
    try {
        $text = [IO.File]::ReadAllText($file.FullName, [System.Text.Encoding]::UTF8)
        [IO.File]::WriteAllText($file.FullName, $text, [System.Text.Encoding]::GetEncoding(1252))
    } catch {
        Write-Host "Failed to process $($file.FullName)"
    }
}
