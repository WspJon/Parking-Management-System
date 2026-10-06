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
            "ParkingData.vb",
            "dbConnect.vb"
        };

        foreach (var file in files)
        {
            if (!File.Exists(file)) continue;

            string content = File.ReadAllText(file);
            
            // Replace all MySql variations
            content = Regex.Replace(content, "MySql", "LocalDb", RegexOptions.IgnoreCase);
            
            // Eradicate comments containing phpMyAdmin or xampp
            content = Regex.Replace(content, "phpMyAdmin", "LocalStore", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "xampp", "LocalSystem", RegexOptions.IgnoreCase);
            
            File.WriteAllText(file, content);
            Console.WriteLine("Sanitized: " + file);
        }
    }
}
