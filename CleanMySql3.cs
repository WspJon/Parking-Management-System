using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] files = {
            "DashBoardForm.vb",
            "frmadmindashboard.vb",
            "ParkingManagerForm.vb",
            "frmaddteller.vb",
            "RegisterForm.vb",
            "Form1.vb",
            "ParkingData.vb"
        };

        foreach (var file in files)
        {
            if (!File.Exists(file)) continue;

            string[] lines = File.ReadAllLines(file);
            List<string> newLines = new List<string>();
            List<string> tryBuffer = new List<string>();
            List<string> functionBuffer = new List<string>();
            
            bool insideFunction = false;
            bool functionHasMySql = false;

            bool insideTry = false;
            bool tryHasMySql = false;
            
            // To handle nested Trys, we need a simple counter (though rarely used here)
            int tryDepth = 0;
            int funcDepth = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string trimmed = line.Trim();

                if (trimmed.StartsWith("Imports MySql")) continue;
                if (trimmed.StartsWith("' --- MySQL") || trimmed.StartsWith("' MySQL Sync")) continue;
                if (trimmed.StartsWith("ParkingData.SyncParkingFromDatabase()") ||
                    trimmed.StartsWith("ParkingData.SyncUsersFromDatabase()") ||
                    trimmed.StartsWith("ParkingData.SyncRatesFromDatabase()"))
                {
                    continue;
                }

                // Function handling
                if (!insideFunction && (trimmed.StartsWith("Private Function GetCurrentUserID") ||
                    trimmed.StartsWith("Public Sub LoadDataFromDatabase") ||
                    trimmed.StartsWith("Public Sub SyncParkingFromDatabase") ||
                    trimmed.StartsWith("Public Sub SyncRatesFromDatabase") ||
                    trimmed.StartsWith("Public Sub SyncUsersFromDatabase") ||
                    trimmed.StartsWith("Private Function GetConnection")))
                {
                    insideFunction = true;
                    functionHasMySql = true; // We explicitly know these are MySQL functions to delete
                    functionBuffer.Clear();
                    funcDepth = 1;
                    continue;
                }

                if (insideFunction)
                {
                    if (trimmed.StartsWith("End Function") || trimmed.StartsWith("End Sub"))
                    {
                        funcDepth--;
                        if (funcDepth == 0)
                        {
                            insideFunction = false;
                        }
                    }
                    continue;
                }

                // Try block handling
                if (!insideTry && trimmed.StartsWith("Try"))
                {
                    insideTry = true;
                    tryHasMySql = false;
                    tryBuffer.Clear();
                    tryDepth = 1;
                    tryBuffer.Add(line);
                    continue;
                }

                if (insideTry)
                {
                    tryBuffer.Add(line);
                    
                    if (trimmed.StartsWith("Try")) tryDepth++;
                    if (trimmed.StartsWith("End Try")) tryDepth--;

                    if (trimmed.Contains("MySql") || trimmed.Contains("GetConnection"))
                    {
                        tryHasMySql = true;
                    }

                    if (tryDepth == 0)
                    {
                        insideTry = false;
                        if (!tryHasMySql)
                        {
                            // Output the buffer if it didn't contain MySQL
                            newLines.AddRange(tryBuffer);
                        }
                        continue;
                    }
                    continue;
                }

                newLines.Add(line);
            }

            File.WriteAllLines(file, newLines);
            Console.WriteLine("Cleaned: " + file);
        }
    }
}
