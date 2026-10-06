Imports System
Imports System.Data
Imports System.Collections.Generic
Imports MySql.Data.MySqlClient

Namespace Global.LocalDb.Data.LocalDbClient

    Public Class LocalDbConnection
        Implements IDisposable

        Private ReadOnly _connection As MySqlConnection

        Public Sub New(connectionString As String)
            _connection = New MySqlConnection(connectionString)
        End Sub

        Public Sub Open()
            Try
                _connection.Open()
            Catch ex As MySqlException
                Throw New LocalDbException("Hindi makakonekta sa MySQL: " & ex.Message, ex)
            End Try
        End Sub

        Public Sub Close()
            If _connection.State <> ConnectionState.Closed Then
                _connection.Close()
            End If
        End Sub

        Public ReadOnly Property State As ConnectionState
            Get
                Return _connection.State
            End Get
        End Property

        Friend ReadOnly Property MySqlConnection As MySqlConnection
            Get
                Return _connection
            End Get
        End Property

        Public Sub Dispose() Implements IDisposable.Dispose
            If _connection IsNot Nothing Then
                _connection.Dispose()
            End If
        End Sub

    End Class


    Public Class LocalDbCommand
        Implements IDisposable

        Private ReadOnly _query As String
        Private ReadOnly _connection As LocalDbConnection
        Private ReadOnly _command As MySqlCommand

        Public ReadOnly Parameters As New LocalDbParameterCollection()

        Public Sub New(query As String, connection As LocalDbConnection)
            _query = query
            _connection = connection
            _command = New MySqlCommand(_query, _connection.MySqlConnection)
        End Sub

        Private Sub ApplyParameters()
            _command.Parameters.Clear()
            For Each parameter As KeyValuePair(Of String, Object) In Parameters.Items
                _command.Parameters.AddWithValue(
                    parameter.Key,
                    If(parameter.Value Is Nothing, DBNull.Value, parameter.Value)
                )
            Next
        End Sub

        Public Function ExecuteScalar() As Object
            ApplyParameters()
            Try
                Return _command.ExecuteScalar()
            Catch ex As MySqlException
                Throw New LocalDbException(ex.Message, ex)
            End Try
        End Function

        Public Function ExecuteReader() As LocalDbDataReader
            ApplyParameters()
            Dim reader As MySqlDataReader
            Try
                reader = _command.ExecuteReader()
            Catch ex As MySqlException
                Throw New LocalDbException(ex.Message, ex)
            End Try
            Return New LocalDbDataReader(reader)
        End Function

        Public Function ExecuteNonQuery() As Integer
            ApplyParameters()
            Try
                Return _command.ExecuteNonQuery()
            Catch ex As MySqlException
                Throw New LocalDbException(ex.Message, ex)
            End Try
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            If _command IsNot Nothing Then
                _command.Dispose()
            End If
        End Sub

    End Class


    Public Class LocalDbParameterCollection

        Private ReadOnly _parameters As New Dictionary(Of String, Object)()

        Public Sub AddWithValue(name As String, value As Object)
            If _parameters.ContainsKey(name) Then
                _parameters(name) = value
            Else
                _parameters.Add(name, value)
            End If
        End Sub

        Friend ReadOnly Property Items As Dictionary(Of String, Object)
            Get
                Return _parameters
            End Get
        End Property

    End Class


    Public Class LocalDbDataAdapter

        Private ReadOnly _query As String
        Private ReadOnly _connection As LocalDbConnection

        Public Sub New(query As String, conn As LocalDbConnection)
            _query = query
            _connection = conn
        End Sub

        Public Function Fill(dt As DataTable) As Integer
            Using adapter As New MySqlDataAdapter(_query, _connection.MySqlConnection)
                Return adapter.Fill(dt)
            End Using
        End Function

    End Class


    Public Class LocalDbException
        Inherits Exception

        Public Sub New()
            MyBase.New("LocalDb Exception")
        End Sub

        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

    End Class


    Public Class LocalDbDataReader
        Implements IDisposable

        Private ReadOnly _reader As MySqlDataReader

        Public Sub New()
            _reader = Nothing
        End Sub

        Friend Sub New(reader As MySqlDataReader)
            _reader = reader
        End Sub

        Public Function Read() As Boolean
            If _reader Is Nothing Then Return False
            Return _reader.Read()
        End Function

        Public Function GetInt32(name As String) As Integer
            If _reader Is Nothing Then Return 0
            Dim value As Object = _reader(name)
            If value Is Nothing OrElse value Is DBNull.Value Then Return 0
            Return Convert.ToInt32(value)
        End Function

        Public Function GetString(name As String) As String
            If _reader Is Nothing Then Return ""
            Dim value As Object = _reader(name)
            If value Is Nothing OrElse value Is DBNull.Value Then Return ""
            Return value.ToString()
        End Function

        Public Function GetValue(name As String) As Object
            If _reader Is Nothing Then Return Nothing
            Dim value As Object = _reader(name)
            If value Is DBNull.Value Then Return Nothing
            Return value
        End Function

        Public Function IsDBNull(name As String) As Boolean
            If _reader Is Nothing Then Return True
            Return _reader(name) Is DBNull.Value
        End Function

        Public Function GetDateTime(name As String) As DateTime
            If _reader Is Nothing Then Return DateTime.MinValue
            Dim value As Object = _reader(name)
            If value Is Nothing OrElse value Is DBNull.Value Then Return DateTime.MinValue
            Return Convert.ToDateTime(value)
        End Function

        Public Function GetDecimal(name As String) As Decimal
            If _reader Is Nothing Then Return 0D
            Dim value As Object = _reader(name)
            If value Is Nothing OrElse value Is DBNull.Value Then Return 0D
            Return Convert.ToDecimal(value)
        End Function

        Public Function GetDouble(name As String) As Double
            If _reader Is Nothing Then Return 0.0
            Dim value As Object = _reader(name)
            If value Is Nothing OrElse value Is DBNull.Value Then Return 0.0
            Return Convert.ToDouble(value)
        End Function

        Public Sub Close()
            If _reader IsNot Nothing Then
                If Not _reader.IsClosed Then
                    _reader.Close()
                End If
            End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            If _reader IsNot Nothing Then
                _reader.Dispose()
            End If
        End Sub

    End Class

End Namespace