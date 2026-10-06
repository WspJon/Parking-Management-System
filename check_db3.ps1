Add-Type -Path 'bin\Debug\MySql.Data.dll'
$conn = New-Object MySql.Data.MySqlClient.MySqlConnection 'Server=127.0.0.1;Database=parkingdb;Uid=root;Pwd=;'
$conn.Open()
$cmd1 = New-Object MySql.Data.MySqlClient.MySqlCommand 'DESCRIBE tblteller;', $conn
$r1 = $cmd1.ExecuteReader()
while($r1.Read()){ Write-Host ('teller: ' + $r1.GetString(0) + ' ' + $r1.GetString(1)) }
$r1.Close()
$cmd2 = New-Object MySql.Data.MySqlClient.MySqlCommand 'DESCRIBE tblcustomer;', $conn
$r2 = $cmd2.ExecuteReader()
while($r2.Read()){ Write-Host ('customer: ' + $r2.GetString(0) + ' ' + $r2.GetString(1)) }
$r2.Close()
$conn.Close()
