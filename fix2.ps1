$files = @("frmaddteller.vb", "RegisterForm.vb", "ReserveDialogForm.vb", "RateConfigDialogForm.vb")
foreach ($f in $files) {
    $c = [System.IO.File]::ReadAllText($f)
    $c = $c.Replace("MessageBox.Show(Me, ", "MessageBox.Show(New Form() With {.TopMost = True}, ")
    [System.IO.File]::WriteAllText($f, $c)
}
