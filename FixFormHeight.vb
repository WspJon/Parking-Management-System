Imports System.IO

Module Program
    Sub Main()
        For Each file As String In {"DashBoardForm.vb", "frmadmindashboard.vb"}
            Dim txt = IO.File.ReadAllText(file)
            
            txt = txt.Replace("pnlSlot.Size = New Size(72, 65)", "pnlSlot.Size = New Size(72, 85)")
            
            txt = txt.Replace("lblName.Size = New Size(72, 15)", "lblName.Size = New Size(72, 20)")
            txt = txt.Replace("lblStatus.Location = New Point(0, 15)", "lblStatus.Location = New Point(0, 20)")
            txt = txt.Replace("lblStatus.Size = New Size(72, 15)", "lblStatus.Size = New Size(72, 20)")
            
            txt = txt.Replace("lblReserver.Location = New Point(0, 30)", "lblReserver.Location = New Point(0, 40)")
            txt = txt.Replace("lblReserver.Size = New Size(72, 15)", "lblReserver.Size = New Size(72, 20)")
            
            txt = txt.Replace("lblDate.Location = New Point(0, 45)", "lblDate.Location = New Point(0, 60)")
            txt = txt.Replace("lblDate.Size = New Size(72, 15)", "lblDate.Size = New Size(72, 20)")
            
            txt = txt.Replace("New Font(""Segoe UI"", 6.0!)", "New Font(""Segoe UI"", 7.5!)")
            txt = txt.Replace("New Font(""Segoe UI"", 6.0!, FontStyle.Italic)", "New Font(""Segoe UI"", 7.5!, FontStyle.Italic)")
            
            txt = txt.Replace("plateText = ""RSVD""", "plateText = ""RESERVED""")
            
            IO.File.WriteAllText(file, txt)
        Next
    End Sub
End Module
