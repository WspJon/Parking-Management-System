Add-Type -Path 'bin\Debug\MySql.Data.dll'
$conn = New-Object MySql.Data.MySqlClient.MySqlConnection 'Server=127.0.0.1;Database=parkingdb;Uid=root;Pwd=;'
$conn.Open()
$cmd1 = New-Object MySql.Data.MySqlClient.MySqlCommand 'SELECT Status FROM tblteller LIMIT 3;', $conn
$r1 = $cmd1.ExecuteReader()
while($r1.Read()){ Write-Host ('teller status: ' + $r1.GetValue(0).ToString()) }
$r1.Close()
$cmd2 = New-Object MySql.Data.MySqlClient.MySqlCommand 'SELECT Status FROM tblcustomer LIMIT 3;', $conn
$r2 = $cmd2.ExecuteReader()
while($r2.Read()){ Write-Host ('customer status: ' + $r2.GetValue(0).ToString()) }
$r2.Close()
$conn.Close()
