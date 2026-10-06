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
            bool insideMySqlBlock = false;
            bool insideMySqlFunction = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string trimmed = line.Trim();

                if (trimmed.StartsWith("Imports MySql")) continue;

                // Handle whole functions like GetCurrentUserID, LoadDataFromDatabase, SyncParkingFromDatabase
                if (trimmed.StartsWith("Private Function GetCurrentUserID") ||
                    trimmed.StartsWith("Public Sub LoadDataFromDatabase") ||
                    trimmed.StartsWith("Public Sub SyncParkingFromDatabase") ||
                    trimmed.StartsWith("Public Sub SyncRatesFromDatabase") ||
                    trimmed.StartsWith("Public Sub SyncUsersFromDatabase") ||
                    trimmed.StartsWith("Private Function GetConnection"))
                {
                    insideMySqlFunction = true;
                    continue;
                }

                if (insideMySqlFunction)
                {
                    if (trimmed.StartsWith("End Function") || trimmed.StartsWith("End Sub"))
                    {
                        insideMySqlFunction = false;
                    }
                    continue;
                }

                // Handle Try blocks containing MySql
                if (trimmed.StartsWith("Try") || trimmed.StartsWith("' --- MySQL sync"))
                {
                    bool hasMySql = false;
                    // Look ahead up to 6 lines to see if it's a MySQL block
                    for (int j = i; j < i + 6 && j < lines.Length; j++)
                    {
                        if (lines[j].Contains("MySqlConnection") || 
                            lines[j].Contains("MySqlCommand") || 
                            lines[j].Contains("MySqlException") ||
                            lines[j].Contains("GetConnection"))
                        {
                            hasMySql = true;
                            break;
                        }
                    }

                    if (hasMySql)
                    {
                        insideMySqlBlock = true;
                        continue;
                    }
                }

                if (insideMySqlBlock)
                {
                    if (trimmed.StartsWith("End Try"))
                    {
                        insideMySqlBlock = false;
                    }
                    continue;
                }

                // Handle residual single lines like calls to Sync
                if (trimmed.Contains("ParkingData.SyncParkingFromDatabase()") ||
                    trimmed.Contains("ParkingData.SyncUsersFromDatabase()") ||
                    trimmed.Contains("ParkingData.SyncRatesFromDatabase()") ||
                    trimmed.StartsWith("MessageBox.Show(\"Error sa pagbura sa database") ||
                    trimmed.Contains("DataStore.SaveDatabase()") || 
                    trimmed.Contains("DataStore.SaveUsers()")) 
                {
                    // For save database, we actually WANT to keep it, but wait, the original files might have had it twice.
                    // Actually let's just keep DataStore.SaveDatabase() if it's there. So don't filter it.
                }

                if (trimmed.Contains("ParkingData.SyncParkingFromDatabase()") ||
                    trimmed.Contains("ParkingData.SyncUsersFromDatabase()") ||
                    trimmed.Contains("ParkingData.SyncRatesFromDatabase()"))
                {
                    continue;
                }

                newLines.Add(line);
            }

            File.WriteAllLines(file, newLines);
            Console.WriteLine("Cleaned: " + file);
        }
    }
}
