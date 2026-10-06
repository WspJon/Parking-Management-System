using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] files = { "frmaddteller.vb", "RegisterForm.vb" };

        foreach (var file in files)
        {
            string content = File.ReadAllText(file);
            string role = file == "frmaddteller.vb" ? "Teller" : "Customer";
            
            string newLogic = @"
        Try
            Dim userExists As Boolean = False
            If txtUser.Text.Trim().ToLower() = ""admin"" Then
                userExists = True
            Else
                For Each row As DataRow In ParkingData.UsersTable.Rows
                    If row(""Username"").ToString() = txtUser.Text.Trim() Then
                        userExists = True
                        Exit For
                    End If
                Next
            End If

            If userExists Then
                MessageBox.Show(""Username already exists."", ""Error"", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim fullNameValue As String = txtUser.Text.Trim()
            If Controls.ContainsKey(""txtFullName"") Then
                fullNameValue = Controls(""txtFullName"").Text.Trim()
            End If

            Dim newRow As DataRow = ParkingData.UsersTable.NewRow()
            newRow(""FullName"") = fullNameValue
            newRow(""Username"") = txtUser.Text.Trim()
            newRow(""Password"") = txtPass.Text
            newRow(""Role"") = """ + role + @"""
            ParkingData.UsersTable.Rows.Add(newRow)

            DataStore.SaveUsers()

            MessageBox.Show(""" + role + @" account created successfully! You can now log in."", ""Success"", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(""An error occurred: "" & ex.Message, ""Error"", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
";

            string pattern = @"Try\s+Using conn As New LocalDbConnection.*?End Try\s+End Sub";
            content = Regex.Replace(content, pattern, newLogic, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            
            File.WriteAllText(file, content);
            Console.WriteLine("Fixed " + file);
        }
    }
}
