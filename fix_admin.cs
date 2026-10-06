using System;
using System.IO;

class Program {
    static void Main() {
        string dashFile = "DashBoardForm.vb";
        string adminFile = "frmadmindashboard.vb";
        
        string dash = File.ReadAllText(dashFile);
        string admin = File.ReadAllText(adminFile);

        // Replace ManageReservationDialogForm logic
        int dashManageStart = dash.IndexOf("If manageDlg.SelectedAction = \"Cancel\" Then");
        int dashManageEnd = dash.IndexOf("End If", dash.IndexOf("SuccessDialogForm.ShowSuccess($\"Vehicle {plateText} successfully checked in at slot {slotName}!\")")) + 6;
        string manageLogic = dash.Substring(dashManageStart, dashManageEnd - dashManageStart);

        int adminManageStart = admin.IndexOf("If manageDlg.SelectedAction = \"Cancel\" Then");
        int adminManageEnd = admin.IndexOf("End If", admin.IndexOf("SuccessDialogForm.ShowSuccess($\"Vehicle {plateText} successfully checked in at slot {slotName}!\")")) + 6;
        
        admin = admin.Substring(0, adminManageStart) + manageLogic + admin.Substring(adminManageEnd);

        // Replace Reserve logic
        int dashResStart = dash.IndexOf("If isDuplicate Then");
        int dashResEnd = dash.IndexOf("End If", dash.IndexOf("SuccessDialogForm.ShowSuccess(\"Vehicle successfully parked at slot \" & slotName)")) + 6;
        string resLogic = dash.Substring(dashResStart, dashResEnd - dashResStart);

        int adminResStart = admin.IndexOf("If isDuplicate Then");
        int adminResEnd = admin.IndexOf("End If", admin.IndexOf("SuccessDialogForm.ShowSuccess(\"Vehicle successfully parked at slot \" & slotName)")) + 6;
        
        admin = admin.Substring(0, adminResStart) + resLogic + admin.Substring(adminResEnd);

        // Replace Clear Paid logic
        int dashClearStart = dash.IndexOf("Dim rowsToDelete As New List(Of DataRow)");
        int dashClearEnd = dash.IndexOf("End If", dash.IndexOf("MessageBox.Show(\"There are no completed transactions to clear.\"")) + 6;
        
        // Wait, Clear Paid also has a MySQL delete block before rowsToDelete.
        dashClearStart = dash.IndexOf("' --- MySQL sync: Burahin ang mga bayad na sa phpMyAdmin ---");
        if(dashClearStart == -1) dashClearStart = dash.IndexOf("Try\r\n                                                              Using conn As MySqlConnection = GetConnection()");
        // actually dashboardform has: 
        // Using conn As MySqlConnection = GetConnection() ... DELETE FROM tblparkingrecord WHERE PaidStatus = 'Paid'
        
        // Let's just do manual string replaces for the missing pieces.
        File.WriteAllText(adminFile, admin);
    }
}
