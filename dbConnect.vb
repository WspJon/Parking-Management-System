Imports LocalDb.Data.LocalDbClient
Imports System.Windows.Forms

Public Module DatabaseModule

    Public connStr As String = "Server=127.0.0.1;Database=parkingdb;Uid=root;Pwd=;"

    Public Function GetConnection() As LocalDbConnection
        Dim conn As New LocalDbConnection(connStr)
        conn.Open()
        Return conn
    End Function

End Module