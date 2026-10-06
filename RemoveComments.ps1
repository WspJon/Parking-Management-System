$files = Get-ChildItem -Path . -Filter *.vb -Recurse
foreach ($file in $files) {
    $content = Get-Content $file.FullName
    $newContent = $content | Where-Object { $_ -notmatch '^\s*''' }
    Set-Content -Path $file.FullName -Value $newContent -Encoding UTF8
}
