Imports System.IO

Module Program
    Sub Main()
        Dim txt1 = File.ReadAllText("DashBoardForm.vb")
        Dim txt2 = File.ReadAllText("frmadmindashboard.vb")
        
        Dim startStr = "        For Each slotEntry In ParkingData.parkingDatabase"
        Dim endStr = "        flpMotorSlots.ResumeLayout(True)"
        
        Dim s1 = txt1.IndexOf(startStr)
        Dim e1 = txt1.IndexOf(endStr) + endStr.Length
        Dim repl = txt1.Substring(s1, e1 - s1)
        
        Dim s2 = txt2.IndexOf(startStr)
        Dim e2 = txt2.IndexOf(endStr) + endStr.Length
        
        Dim newTxt2 = txt2.Substring(0, s2) + repl + txt2.Substring(e2)
        File.WriteAllText("frmadmindashboard.vb", newTxt2)
    End Sub
End Module
