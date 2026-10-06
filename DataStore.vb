Imports System
Imports System.IO
Imports System.Data
Imports System.Windows.Forms

Module DataStore
    Private ReadOnly DataFilePath As String = Path.Combine(Application.StartupPath, "parking_data.xml")
    Private ReadOnly UsersFilePath As String = Path.Combine(Application.StartupPath, "users_data.xml")
    Private ReadOnly RatesFilePath As String = Path.Combine(Application.StartupPath, "rates_config.xml")

    Public Sub LoadDatabase()
        ParkingData.InitializeDatabase()
        ParkingData.InitializeUsers()

        Try
            If File.Exists(DataFilePath) Then
                ParkingData.ParkingTable.Clear()
                ParkingData.ParkingTable.ReadXml(DataFilePath)
                
                If Not ParkingData.ParkingTable.Columns.Contains("ReservedBy") Then
                    ParkingData.ParkingTable.Columns.Add("ReservedBy", GetType(String))
                End If
                If Not ParkingData.ParkingTable.Columns.Contains("ProcessedBy") Then
                    ParkingData.ParkingTable.Columns.Add("ProcessedBy", GetType(String))
                End If

                Dim slotKeys As New Collections.Generic.List(Of String)(ParkingData.parkingDatabase.Keys)
                For Each key In slotKeys
                    ParkingData.parkingDatabase(key) = True
                Next

                For Each row As DataRow In ParkingData.ParkingTable.Rows
                    If row("PaidStatus").ToString() = "Not Paid" OrElse row("PaidStatus").ToString() = "Reserved" Then
                        Dim slot As String = row("Slot").ToString()
                        If ParkingData.parkingDatabase.ContainsKey(slot) Then
                            ParkingData.parkingDatabase(slot) = False
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading database: " & ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        Try
            If File.Exists(UsersFilePath) Then
                ParkingData.UsersTable.Clear()
                ParkingData.UsersTable.ReadXml(UsersFilePath)
                
                If Not ParkingData.UsersTable.Columns.Contains("FullName") Then
                    ParkingData.UsersTable.Columns.Add("FullName", GetType(String))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading user data: " & ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        LoadRates()
    End Sub

    Public Sub SaveDatabase()
        Try
            ParkingData.ParkingTable.AcceptChanges()
            ParkingData.ParkingTable.WriteXml(DataFilePath, XmlWriteMode.WriteSchema)
        Catch ex As Exception
            MessageBox.Show("Error saving database: " & ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Public Sub SaveUsers()
        Try
            ParkingData.UsersTable.AcceptChanges()
            ParkingData.UsersTable.WriteXml(UsersFilePath, XmlWriteMode.WriteSchema)
        Catch ex As Exception
            MessageBox.Show("Error saving user data: " & ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Public Sub SaveRates()
        Try
            Dim doc As New Xml.XmlDocument()
            Dim root = doc.CreateElement("Rates")
            doc.AppendChild(root)
            
            Dim addElement = Sub(name As String, value As String)
                Dim el = doc.CreateElement(name)
                el.InnerText = value
                root.AppendChild(el)
            End Sub
            
            addElement("CarBaseRate", ParkingData.CarBaseRate.ToString())
            addElement("MotorBaseRate", ParkingData.MotorBaseRate.ToString())
            addElement("CarSucceedingRate", ParkingData.CarSucceedingRate.ToString())
            addElement("MotorSucceedingRate", ParkingData.MotorSucceedingRate.ToString())
            addElement("FreeHours", ParkingData.FreeHours.ToString())
            
            doc.Save(RatesFilePath)
        Catch ex As Exception
            MessageBox.Show("Error saving rates: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Public Sub LoadRates()
        Try
            If File.Exists(RatesFilePath) Then
                Dim doc As New Xml.XmlDocument()
                doc.Load(RatesFilePath)
                
                Dim root = doc.DocumentElement
                If root IsNot Nothing Then
                    Dim getVal = Function(name As String, defaultVal As Double) As Double
                        Dim node = root.SelectSingleNode(name)
                        If node IsNot Nothing Then Return Convert.ToDouble(node.InnerText)
                        Return defaultVal
                    End Function
                    
                    ParkingData.CarBaseRate = getVal("CarBaseRate", 50.0)
                    ParkingData.MotorBaseRate = getVal("MotorBaseRate", 50.0)
                    ParkingData.CarSucceedingRate = getVal("CarSucceedingRate", 20.0)
                    ParkingData.MotorSucceedingRate = getVal("MotorSucceedingRate", 10.0)
                    
                    Dim freeNode = root.SelectSingleNode("FreeHours")
                    If freeNode IsNot Nothing Then 
                        ParkingData.FreeHours = Convert.ToInt32(freeNode.InnerText)
                    Else
                        ParkingData.FreeHours = 1
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading rates: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Module
