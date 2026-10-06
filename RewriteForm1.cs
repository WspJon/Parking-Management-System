using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string filePath = "Form1.vb";
        string content = File.ReadAllText(filePath);
        
        string newLoginLogic = @"
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text
        Dim isAuthenticated As Boolean = False
        Dim role As String = """"
        Dim fullName As String = """"

        If username = ""admin"" AndAlso password = ""1234"" Then
            isAuthenticated = True
            role = ""Admin""
            CurrentUsername = ""admin""
            CurrentFullName = ""Administrator""
        Else
            Dim userFound As Boolean = False
            For Each row As DataRow In ParkingData.UsersTable.Rows
                If row(""Username"").ToString() = username Then
                    userFound = True
                    If row(""Password"").ToString() = password Then
                        isAuthenticated = True
                        role = row(""Role"").ToString()
                        CurrentUsername = username
                        CurrentFullName = row(""FullName"").ToString()
                    Else
                        MessageBox.Show(""Invalid password."", ""Login Failed"", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                    Exit For
                End If
            Next

            If Not userFound Then
                MessageBox.Show(""Username does not exist."", ""Login Failed"", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        If isAuthenticated Then
            CurrentUserRole = role
            Me.Hide()
            txtUsername.Clear()
            txtPassword.Clear()

            Using successForm As New LoginSuccessForm()
                successForm.StartPosition = FormStartPosition.CenterScreen
                successForm.ShowDialog()
            End Using

            If CurrentUserRole = ""Customer"" Then
                DashBoardForm.ShowLobbyMode = True
                DashBoardForm.UpdateSlots()
                DashBoardForm.Show()
            ElseIf CurrentUserRole = ""Teller"" Then
                frmadmindashboard.ShowLobbyMode = False
                frmadmindashboard.UpdateSlots()
                frmadmindashboard.Show()
            Else
                DashBoardForm.ShowLobbyMode = False
                DashBoardForm.UpdateSlots()
                DashBoardForm.Show()
            End If
        End If
    End Sub
";
        
        string pattern = @"\s*' Naghahanap ng account sa isang table.*?End Sub";
        content = Regex.Replace(content, pattern, newLoginLogic, RegexOptions.Singleline);
        
        File.WriteAllText(filePath, content);
        Console.WriteLine("Replaced successfully!");
    }
}
