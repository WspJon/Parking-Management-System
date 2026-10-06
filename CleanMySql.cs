using System;
using System.IO;
using System.Text.RegularExpressions;

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
            if (File.Exists(file))
            {
                string content = File.ReadAllText(file);
                
                // Remove Imports MySql
                content = Regex.Replace(content, @"Imports MySql\.Data\.MySqlClient\r?\n?", "");

                // Remove Try Using conn As MySqlConnection ... End Try
                content = Regex.Replace(content, @"\s*(?:' --- MySQL sync.*?\r?\n)?\s*Try\s*Dim (?:tellerID|customerID).*?Catch ex As MySqlException.*?End Try\s*", "\r\n", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                
                // Other variants
                content = Regex.Replace(content, @"\s*(?:' --- MySQL sync.*?\r?\n)?\s*Try\s*Using conn As MySqlConnection.*?Catch ex As MySqlException.*?End Try\s*", "\r\n", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"\s*(?:' --- MySQL.*?\r?\n)?\s*Try\s*Using conn As New MySql\.Data\.MySqlClient\.MySqlConnection.*?Catch ex As Exception.*?End Try\s*", "\r\n", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                
                content = Regex.Replace(content, @"\s*(?:' --- MySQL.*?\r?\n)?\s*Try\s*Dim connStr As String.*?Catch ex As Exception.*?End Try\s*", "\r\n", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                
                // Remove Sync methods in ParkingData.vb
                content = Regex.Replace(content, @"Public Sub SyncParkingFromDatabase\(\).*?End Sub\s*", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"Public Sub SyncRatesFromDatabase\(\).*?End Sub\s*", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"Public Sub SyncUsersFromDatabase\(\).*?End Sub\s*", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                
                // Remove helper functions in forms
                content = Regex.Replace(content, @"Private Function GetCurrentUserID.*?End Function\s*", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"Private Function GetConnection\(\).*?End Function\s*", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                // Re-replace sync calls with empty or save
                content = content.Replace("ParkingData.SyncParkingFromDatabase()", "");
                content = content.Replace("ParkingData.SyncUsersFromDatabase()", "DataStore.SaveUsers()");

                File.WriteAllText(file, content);
                Console.WriteLine("Cleaned: " + file);
            }
        }
    }
}
