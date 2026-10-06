Add-Type -Path "D:\Downloads\ParkingSystemProject 2\ParkingSystemProject\bin\Debug\MySql.Data.dll"
$conn = New-Object MySql.Data.MySqlClient.MySqlConnection("Server=127.0.0.1;Database=parkingdb;Uid=root;Pwd=;")
$conn.Open()
$cmd = $conn.CreateCommand()
$cmd.CommandText = (Get-Content -Path "fix_admin.sql" -Raw)
$cmd.ExecuteNonQuery()
$conn.Close()
Write-Host "SQL executed successfully!"
