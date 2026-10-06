$files = @("frmaddteller.vb", "RegisterForm.vb", "ReserveDialogForm.vb", "RateConfigDialogForm.vb")
foreach ($f in $files) {
    $c = [System.IO.File]::ReadAllText($f)
    $c = $c.Replace("MessageBox.Show(New Form() With {.TopMost = True}, ", "MessageBox.Show(New Form() With {.TopMost = True}, ")
    $c = $c.Replace("MessageBox.Show(New Form() With {.TopMost = True}, ", "MessageBox.Show(New Form() With {.TopMost = True}, ")
    $c = $c.Replace("MessageBox.Show(New Form() With {.TopMost = True}, ", "MessageBox.Show(Me, ")
    [System.IO.File]::WriteAllText($f, $c)
}
