Add-Type -Path 'bin\Debug\MySql.Data.dll'
$conn = New-Object MySql.Data.MySqlClient.MySqlConnection 'Server=127.0.0.1;Database=parkingdb;Uid=root;Pwd=;'
$conn.Open()
$cmd = New-Object MySql.Data.MySqlClient.MySqlCommand 'DESCRIBE tblparkingrecord;', $conn
$reader = $cmd.ExecuteReader()
while($reader.Read()) {
    Write-Host $reader.GetString(0)
}
$conn.Close()
